using contratosimples_api.Application.Core.Models.DTO.Contrato;
using contratosimples_api.Application.Core.Models.DTO.ContratoItem;
using contratosimples_api.Application.Core.Models.Entities;
using contratosimples_api.Application.Core.Repositories;
using contratosimples_api.Application.Management.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ContratoController : ControllerBase
	{
		private readonly ContratoSimplesUnitOfWork uow;

		public ContratoController(ContratoSimplesUnitOfWork uow) {
			this.uow = uow;
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		public async Task<IActionResult> CreateContrato([FromBody] CreateContratoRequestDto request) {
			var centroDeCusto = await uow.CentroDeCustoRepository.GetByIdAsync(request.CentroDeCustoId);
			var fornecedor = await uow.FornecedorRepository.GetByIdAsync(request.FornecedorId);
			var cliente = await uow.ClienteRepository.GetByIdAsync(request.ClienteId);

			var contrato = request.MapToEntity();
			contrato.CentroDeCusto = centroDeCusto;
			contrato.Fornecedor = fornecedor;
			contrato.Cliente = cliente;

			Dictionary<int, ContratoItem>[] hashMap = CreateContratoItemRequestDto.MapRequestItens(request.Itens);

			var hashGrupos = hashMap[0];
			var hashItens = hashMap[1];

			foreach (var item in request.Itens) {
				var contratoItem = hashItens[item.TemporaryId];
				if (item.GrupoId is not null)
					contratoItem.Grupo = hashGrupos[(int)item.GrupoId];
				contrato.Itens.Add(contratoItem);
			}

			contrato = await uow.ContratoRepository.InsertAsync(contrato);
			await uow.SaveAsync();

			var response = ContratoCabecalhoDto.MapFromEntity(contrato);
			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		public async Task<IActionResult> GetAllContrato() {
			var contratos = await uow.ContratoRepository.GetAsync(null, (q => q.OrderBy(c => c.Id)), "CentroDeCusto,Fornecedor,Cliente");

			var response = new List<ContratoCabecalhoDto>();
			foreach (var c in contratos)
				response.Add(ContratoCabecalhoDto.MapFromEntity(c));
			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idContrato:int}/cabecalho")]
		public async Task<IActionResult> GetContratoCabecalhoById([FromRoute] int idContrato) {
			var contrato = (await uow.ContratoRepository.GetAsync(c => c.Id == idContrato, null, "CentroDeCusto,Fornecedor,Cliente")).FirstOrDefault();

			if (contrato == null)
				return NotFound();

			var response = ContratoCabecalhoDto.MapFromEntity(contrato);
			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idContrato:int}/itens")]
		public async Task<IActionResult> GetItensContrato([FromRoute] int idContrato) {
			var itensContrato = await uow.ContratoItemRepository.GetAsync(i => i.ContratoId == idContrato, null, "Grupo");

			var response = new List<ContratoItemDto>();
			foreach (var i in itensContrato)
				response.Add(ContratoItemDto.MapFromEntity(i));

			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idContrato:int}")]
		public async Task<IActionResult> UpdateContrato([FromRoute] int idContrato, [FromBody] UpdateContratoRequestDto request) {
			var contrato = await uow.ContratoRepository.GetByIdAsync(idContrato);
			if (contrato == null)
				return NotFound();

			var centroDeCusto = await uow.CentroDeCustoRepository.GetByIdAsync(request.CentroDeCustoId);
			var fornecedor = await uow.FornecedorRepository.GetByIdAsync(request.FornecedorId);
			var cliente = await uow.ClienteRepository.GetByIdAsync(request.ClienteId);

			contrato.CentroDeCusto = centroDeCusto;
			contrato.Fornecedor = fornecedor;
			contrato.Cliente = cliente;
			contrato.DtFim = request.DtFim;
			contrato.DtInicio = request.DtInicio;
			contrato.Obs = request.Obs;

			Dictionary<int, ContratoItem>[] hashMap = CreateContratoItemRequestDto.MapRequestItens(request.ItensToAdd);

			var hashGrupos = hashMap[0];
			var hashItens = hashMap[1];

			foreach (var item in request.ItensToAdd) {
				var contratoItem = hashItens[item.TemporaryId];
				if (item.GrupoId is not null) {
					//Se o id do grupo não esta no hash de grupos,
					//significa que o grupo não é um item novo e já deve
					//existir no banco de dados
					if (hashGrupos.ContainsKey((int)item.GrupoId))
						contratoItem.Grupo = hashGrupos[(int)item.GrupoId];
					else
						contratoItem.Grupo = await uow.ContratoItemRepository.GetByIdAsync(item.GrupoId);
				}

				contratoItem.ContratoId = contrato.Id;
				await uow.ContratoItemRepository.InsertAsync(contratoItem);
			}

			foreach (var item in request.ItensToUpdate) {
				var contratoItem = (await uow.ContratoItemRepository.GetAsync(i => i.Id == item.Id && i.ContratoId == contrato.Id, null, "Grupo")).FirstOrDefault();

				if (contratoItem == null)
					continue;

				contratoItem.CodEscopo = item.CodEscopo;
				contratoItem.Descricao = item.Descricao;
				contratoItem.UnMedida = item.UnMedida;
				contratoItem.Quantidade = item.Quantidade;
				contratoItem.ValorUnit = item.ValorUnit;
				contratoItem.ValorTotal = item.ValorTotal;

				if (item.GrupoId != null)
					contratoItem.Grupo = await uow.ContratoItemRepository.GetByIdAsync(item.GrupoId);
				else
					contratoItem.Grupo = null;


				await uow.ContratoItemRepository.UpdateAsync(contratoItem.Id, contratoItem);
			}

			foreach (var id in request.ItensToDelete) {
				var contratoItem = (await uow.ContratoItemRepository.GetAsync(i => i.Id == id && i.ContratoId == contrato.Id, null, "Grupo")).FirstOrDefault();
				if (contratoItem != null)
					await uow.ContratoItemRepository.DeleteAsync(id);
			}


			await uow.ContratoRepository.UpdateAsync(contrato.Id, contrato);
			await uow.SaveAsync();


			var response = ContratoCabecalhoDto.MapFromEntity(contrato);

			return Ok(response);
		}

		[HttpDelete]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idContrato:int}")]
		public async Task<IActionResult> DeleteContrato([FromRoute] int idContrato) {
			await uow.ContratoRepository.DeleteAsync(idContrato);
			await uow.SaveAsync();
			return Ok(idContrato);
		}

	}
}
