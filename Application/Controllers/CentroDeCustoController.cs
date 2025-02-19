using contratosimples_api.Application.Models.DTO.CentroDeCusto;
using contratosimples_api.Application.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class CentroDeCustoController : ControllerBase
	{
		private readonly UnitOfWork uow;

		public CentroDeCustoController(UnitOfWork uow)
		{
			this.uow = uow;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCentroDeCusto([FromBody] CreateCentroDeCustoRequestDto request)
		{
			var centroDeCusto = request.MapToEntity();

			await uow.CentroDeCustoRepository.InsertAsync(centroDeCusto);
			await uow.SaveAsync();

			var response = CentroDeCustoDto.MapFromEntity(centroDeCusto);

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCentroDeCusto()
		{
			var centroDeCustos = await uow.CentroDeCustoRepository.GetAsync();

			var response = new List<CentroDeCustoDto>();
			foreach (var cdc in centroDeCustos)
			{
				response.Add(CentroDeCustoDto.MapFromEntity(cdc));
			}
			return Ok(response);

		}

		[HttpGet]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> GetCentroDeCustoById([FromRoute] int codCentroDeCusto)
		{
			var cdc = await uow.CentroDeCustoRepository.GetByIdAsync(codCentroDeCusto);

			if (cdc is null)
				return NotFound();

			var response = CentroDeCustoDto.MapFromEntity(cdc);

			return Ok(response);
		}

		[HttpPut]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> UpdateCentroDeCustoById([FromRoute] int codCentroDeCusto, [FromBody] UpdateCentroDeCustoRequestDto request)
		{
			var cdc = await uow.CentroDeCustoRepository.GetByIdAsync(codCentroDeCusto);
			if (cdc is null)
				return NotFound();

			cdc = request.MapToEntity();
			cdc.Cod = codCentroDeCusto;

			await uow.CentroDeCustoRepository.UpdateAsync(codCentroDeCusto, cdc);
			await uow.SaveAsync();

			var response = CentroDeCustoDto.MapFromEntity(cdc);

			return Ok(response);

		}

		[HttpDelete]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> DeleteCentroDeCusto([FromRoute] int codCentroDeCusto)
		{
			var cdc = await uow.CentroDeCustoRepository.DeleteAsync(codCentroDeCusto);

			if (cdc is null) return NotFound();

			await uow.SaveAsync();

			return Ok(cdc.Cod);
		}
	}
}
