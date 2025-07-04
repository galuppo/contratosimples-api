using contratosimples_api.Application.Core.Models.DTO.Aditivo;
using contratosimples_api.Application.Core.Models.DTO.AditivoItem;
using contratosimples_api.Application.Core.Models.DTO.ContratoItem;
using contratosimples_api.Application.Core.Models.Entities;
using contratosimples_api.Application.Core.Repositories;
using contratosimples_api.Application.Management.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class AditivoController : ControllerBase
	{
		private readonly ContratoSimplesUnitOfWork uow;

		public AditivoController(ContratoSimplesUnitOfWork uow) {
			this.uow = uow;
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		public async Task<IActionResult> CreateAditivo([FromBody] CreateAditivoRequestDto request) {

			//Busca o contrato do aditivo
			var contrato = (await uow.ContratoRepository.GetAsync((c) => c.Id == request.IdContrato, null, "CentroDeCusto,Fornecedor,Cliente")).FirstOrDefault();
			if (contrato == null) {
				ModelState.AddModelError("Contrato", "Contrato id=" + request.IdContrato + " não encontrado!");
				return ValidationProblem(ModelState);
			}

			//Cria o cabeçalho do aditivo
			var aditivo = request.MapToEntity();
			aditivo.Contrato = contrato;

			await uow.AditivoRepository.InsertAsync(aditivo);

			//Cria os itens do aditivo
			//Salva os itens em um hash para uso posterior
			var hashItensAditivo = new Dictionary<int, AditivoItem>();
			for (int i = 0; i < request.ItensAditivoToAdd.Count; i++) {
				var iAditivoReq = request.ItensAditivoToAdd[i];
				var iAditivo = iAditivoReq.MapToEntity();

				iAditivo.Aditivo = aditivo;
				hashItensAditivo.Add(iAditivoReq.IdItemContrato, iAditivo);
			}

			//Atualiza os itens do contrato
			foreach (var item in request.ItensContratoToUpdate) {
				var iContrato = await uow.ContratoItemRepository.GetByIdAsync(item.Id);
				if (iContrato == null) {
					ModelState.AddModelError("ContratoItem", "ContratoItem id="+item.Id+"(Cod.escopo = "+item.CodEscopo+" não encontrado!");
					return ValidationProblem(ModelState);
				}
				iContrato.Quantidade = item.Quantidade;
				iContrato.ValorTotal = item.ValorTotal;

				//Relaciona o item do contrato com o novo item de aditivo
				if(hashItensAditivo.ContainsKey(iContrato.Id))
					hashItensAditivo[iContrato.Id].ContratoItem = iContrato;

				await uow.ContratoItemRepository.UpdateAsync(iContrato.Id, iContrato);
			}


			//Cria um hash com os grupos e itens a serem adicionados no contrato
			var hashRequest = CreateContratoItemRequestDto.MapRequestItens(request.ItensContratoToAdd);
			var hashGrupos = hashRequest[0];
			var hashItens = hashRequest[1];

			//Insere os novos itens
			foreach (var iContratoReq in request.ItensContratoToAdd) {
				var iContrato = hashItens[iContratoReq.TemporaryId];
				if(iContratoReq.GrupoId is not null) {
					//Verifica se o grupo esta no hash, senão, deve estar no banco
					if (hashGrupos.ContainsKey((int)iContratoReq.GrupoId))
						iContrato.Grupo = hashGrupos[(int)iContratoReq.GrupoId];
					else
						iContrato.Grupo = await uow.ContratoItemRepository.GetByIdAsync(iContratoReq.GrupoId);
					
					//Se o grupo estiver null significa que não foi encontrato o
					//o item referenciado pelo GrupoId
					if(iContrato.Grupo is null) {
						ModelState.AddModelError("ContratoItem", "Grupo id="+iContratoReq.GrupoId+" não encontrado para o item com cód. escopo: "+iContrato.CodEscopo);
						return ValidationProblem(ModelState);
					}
				}

				iContrato.ContratoId = contrato.Id;

				//Relaciona o item do contrato com o novo item de aditivo
				//Caso não encontre item de aditivo, gera erro
				if(hashItensAditivo.ContainsKey(iContratoReq.TemporaryId))
					hashItensAditivo[iContratoReq.TemporaryId].ContratoItem = iContrato;
				else {
					ModelState.AddModelError("AditivoItem", "Não foi encontrado aditivo para o item com cod. escopo "+iContrato.CodEscopo);
					return ValidationProblem(ModelState);
				}
				await uow.ContratoItemRepository.InsertAsync(iContrato);
			}

			//Insere os itens
			foreach (var iAditivo in hashItensAditivo.Values) {
				await uow.AditivoItemRepository.InsertAsync(iAditivo);
			}

			await uow.SaveAsync();

			return Ok(AditivoCabecalhoDto.MapFromEntity(aditivo));
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("contrato/{idContrato:int}/cabecalho")]
		public async Task<IActionResult> GetAllCabecalhoAditivosContrato([FromRoute] int idContrato) {

			var aditivos = await uow.AditivoRepository.GetAsync((a) => a.Contrato.Id == idContrato, ((q) => q.OrderBy(a => a.Id)), "Contrato");

			var response = new List<AditivoCabecalhoDto>();

			foreach (var adtv in aditivos) {
				response.Add(AditivoCabecalhoDto.MapFromEntity(adtv));
			}

			return Ok(response);

		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("contrato/{idContrato:int}")]
		public async Task<IActionResult> GetAllAditivosContrato([FromRoute] int idContrato) {
			var aditivos = await uow.AditivoRepository.GetAsync((a) => a.Contrato.Id == idContrato, ((q) => q.OrderBy(a => a.Id)), "Contrato");
			var itensAditivos = await uow.AditivoItemRepository.GetAsync((i)=> i.Aditivo.Contrato.Id == idContrato, null, "Aditivo,ContratoItem");

			Dictionary<int, AditivoDto> hashAditivos = new Dictionary<int, AditivoDto>();

			foreach (var adtv in aditivos) {
				hashAditivos.Add(adtv.Id, new AditivoDto {
					Cabecalho = AditivoCabecalhoDto.MapFromEntity(adtv),
					Itens = new List<AditivoItemDto>()
				});
			}

			foreach (var iAdtv in itensAditivos) {
				if(hashAditivos.ContainsKey(iAdtv.Aditivo.Id))
					hashAditivos[iAdtv.Aditivo.Id].Itens.Add(AditivoItemDto.MapFromEntity(iAdtv));
			}
			return Ok(hashAditivos.Values.ToList());
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idAditivo:int}/itens")]
		public async Task<IActionResult> GetItensAditivo([FromRoute] int idAditivo) {
			var itensAditivo = await uow.AditivoItemRepository.GetAsync((i) => i.Aditivo.Id == idAditivo);
			var response = new List<AditivoItemDto>();

			foreach (var iAditivo in itensAditivo) {
				response.Add(AditivoItemDto.MapFromEntity(iAditivo));
			}

			return Ok(response);
		} 

		[HttpPut]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idAditivo:int}/delete")]
		public async Task<IActionResult> DeleteAditivo([FromRoute] int idAditivo, [FromBody] DeleteAditivoRequestDto request) {

			//Busca o aditivo para verificar se ele existe
			var aditivo = await uow.AditivoRepository.GetByIdAsync(idAditivo);
			if (aditivo == null)
				return Ok(idAditivo);

			//Atualiza os itens do contrato
			foreach (var iContrato in request.ItensToUpdate) {
				var contratoItem = await uow.ContratoItemRepository.GetByIdAsync(iContrato.Id);
				contratoItem.Quantidade = iContrato.Quantidade;
				contratoItem.ValorTotal = iContrato.ValorTotal;

				await uow.ContratoItemRepository.UpdateAsync(iContrato.Id, contratoItem);
			}

			await uow.AditivoRepository.DeleteAsync(idAditivo);
			await uow.SaveAsync();
			return Ok(idAditivo);
		}
	
	}
}
