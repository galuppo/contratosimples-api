using contratosimples_api.Application.Core.Models.DTO.Aditivo;
using contratosimples_api.Application.Core.Models.DTO.Medicao;
using contratosimples_api.Application.Core.Models.DTO.MedicaoItem;
using contratosimples_api.Application.Core.Models.Entities;
using contratosimples_api.Application.Core.Repositories;
using contratosimples_api.Application.Management.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;




namespace contratosimples_api.Application.Core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class MedicaoController : ControllerBase
	{

		private struct SaldoMedicaoItem
		{
			public decimal Valor { get; set; }
			public decimal Quantidade { get; set; }
		}

		private readonly ContratoSimplesUnitOfWork uow;

		public MedicaoController(ContratoSimplesUnitOfWork uow) {
			this.uow = uow;
		}

		private async Task<SaldoMedicaoItem> GetSaldoMedicaoItem(int idItemContrato) {
			var result = new SaldoMedicaoItem {
				Quantidade = 0,
				Valor = 0
			};

			var iContrato = await uow.ContratoItemRepository.GetByIdAsync(idItemContrato);
			if (iContrato == null)
				return result;
			result.Quantidade = (decimal)iContrato.Quantidade;
			result.Valor = (decimal)iContrato.ValorTotal;

			//Busca todas as medições do item
			var iMedicaoList = await uow.MedicaoItemRepository.GetAsync((i) => i.ItemContrato.Id == idItemContrato);

			foreach (var iMedicao in iMedicaoList) {
				result.Quantidade -= iMedicao.Quantidade;
				result.Valor -= iMedicao.Valor;
			}
			return result;

		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		public async Task<IActionResult> CreateMedicao([FromBody] CreateMedicaoRequestDto request) {

			if (request.Itens.Count == 0) {
				ModelState.AddModelError("Medicao", "Não foi informado nenhum item para a medição!");
				return ValidationProblem(ModelState);
			}

			var contrato = await uow.ContratoRepository.GetByIdAsync(request.IdContrato);
			if (contrato == null) {
				ModelState.AddModelError("Medicao", "Contrato.id = " + request.IdContrato + " não encontrado!");
				return ValidationProblem(ModelState);
			}

			//Cria cabeçalho da medição
			var medicao = new Medicao {
				Contrato = contrato,
				Data = request.Data,
				ValorTotal = 0
			};
					
			//Cria itens da medição {
			foreach (var iRequest in request.Itens) {

				//Busca o item do contrato
				var iContrato = await uow.ContratoItemRepository.GetByIdAsync(iRequest.IdContratoItem);
				if (iContrato == null) {
					ModelState.AddModelError("MedicaoItem", "Item do contrato não encontrado! (ContratoItem.id=" + iRequest.IdContratoItem + ")");
					return ValidationProblem(ModelState);
				}

				//Valida a quantidade sendo medida
				if (iRequest.Quantidade <= 0) {
					ModelState.AddModelError("MedicaoItem", 
						"Não é possível medir uma quantidade zerada ou negativa! (ContratoItem.CodEscopo=" + iContrato.CodEscopo + 
						"; MedicaoItem.Quantidade=" + iRequest.Quantidade + ")");
					return ValidationProblem(ModelState);
				}

				//Valida o valor sendo medido
				if (iRequest.Valor <= 0) {
					ModelState.AddModelError("MedicaoItem",
						"Não é possível medir um valor zerado ou negativo! (ContratoItem.CodEscopo=" + iContrato.CodEscopo +
						"; MedicaoItem.Valor=" + iRequest.Valor + ")");
					return ValidationProblem(ModelState);
				}

				var saldoItem = await GetSaldoMedicaoItem(iContrato.Id);
				//Valida o saldo de quantidade
				if (iRequest.Quantidade > saldoItem.Quantidade) {
					ModelState.AddModelError("MedicaoItem",
						"Não é possível medir uma quantidade acima da disponível no contrato! (ContratoItem.CodEscopo=" + iContrato.CodEscopo +
						"; MedicaoItem.Quantidade=" + iRequest.Quantidade + "; Saldo: "+saldoItem.Quantidade+")");
					return ValidationProblem(ModelState);
				}
				//Valida o saldo de valor
				if (iRequest.Valor > saldoItem.Valor) {
					ModelState.AddModelError("MedicaoItem",
						"Não é possível medir um valor acima da disponível no contrato! (ContratoItem.CodEscopo=" + iContrato.CodEscopo +
						"; MedicaoItem.Valor=" + iRequest.Valor + "; Saldo: " + saldoItem.Valor + ")");
					return ValidationProblem(ModelState);
				}

				//Cria a medição do item
				var medicaoItem = new MedicaoItem {
					ItemContrato = iContrato,
					Medicao = medicao,
					Quantidade = iRequest.Quantidade,
					Valor = iRequest.Valor
				};

				await uow.MedicaoItemRepository.InsertAsync(medicaoItem);
				//Totaliza a medição
				medicao.ValorTotal += medicaoItem.Valor;
			}

			await uow.MedicaoRepository.InsertAsync(medicao);
			await uow.SaveAsync();

			return Ok();
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("contrato/{idContrato:int}")]
		public async Task<IActionResult> GetAllByIdContrato([FromRoute] int idContrato) {
			List<MedicaoCabecalhoDto> result = new List<MedicaoCabecalhoDto>();
			var medicoes = await uow.MedicaoRepository.GetAsync((m) => m.Contrato.Id == idContrato);
			foreach (var cMedicao in medicoes) {
				result.Add(new MedicaoCabecalhoDto {
					Id = cMedicao.Id,
					Data = cMedicao.Data,
					IdContrato = idContrato,
					ValorTotal = cMedicao.ValorTotal
				});
			}
			return Ok(result);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idMedicao:int}")]
		public async Task<IActionResult> GetMedicao([FromRoute] int idMedicao) {
			var cMedicao = (await uow.MedicaoRepository.GetAsync((m) => m.Id == idMedicao, null, "Contrato")).FirstOrDefault();
			if (cMedicao == null)
				return NotFound();

			return Ok(new MedicaoCabecalhoDto {
				Id=cMedicao.Id,
				Data = cMedicao.Data,
				IdContrato = cMedicao.Contrato.Id,
				ValorTotal = cMedicao.ValorTotal
			});
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idMedicao:int}/itens")]
		public async Task<IActionResult> GetMedicaoItens([FromRoute] int idMedicao) {
			var cMedicao = (await uow.MedicaoRepository.GetAsync((m) => m.Id == idMedicao, null, "Contrato")).FirstOrDefault();
			if (cMedicao == null)
				return NotFound();
			var itens = await uow.MedicaoItemRepository.GetAsync((i) => i.Medicao.Id == idMedicao, null, "ItemContrato");

			if(itens.Count() == 0) {
				ModelState.AddModelError("MedicaoItem", "Não foram encontrados itens para a medição informada! (Medicao.Id="+idMedicao+")");
				return ValidationProblem(ModelState);
			}



			MedicaoDto result = new MedicaoDto();
			result.Cabecalho = new MedicaoCabecalhoDto {
				Id = idMedicao,
				Data = cMedicao.Data,
				IdContrato = cMedicao.Contrato.Id,
				ValorTotal = cMedicao.ValorTotal
			};
			result.Itens = new List<MedicaoItemDto>();

			foreach (var iMedicao in itens) {
				result.Itens.Add(new MedicaoItemDto {
					Id = iMedicao.Id,
					IdContratoItem = iMedicao.ItemContrato.Id,
					IdMedicao = cMedicao.Id,
					Quantidade = iMedicao.Quantidade,
					Valor = iMedicao.Valor
				});
			}
			return Ok(result);
		}

	}
}
