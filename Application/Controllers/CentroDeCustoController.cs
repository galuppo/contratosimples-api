using contratosimples_api.Application.Models.DTO.CentroDeCusto;
using contratosimples_api.Application.Models.Entities;
using contratosimples_api.Application.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	public class CentroDeCustoController : ControllerBase
	{
		private readonly ICentroDeCustoRepository centroDeCustoRepository;

		public CentroDeCustoController(ICentroDeCustoRepository centroDeCustoRepository)
		{
			this.centroDeCustoRepository = centroDeCustoRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCentroDeCusto([FromBody] CreateCentroDeCustoRequestDto request)
		{
			var centroDeCusto = request.MapToEntity();

			await centroDeCustoRepository.CreateAsync(centroDeCusto);

			var response = CentroDeCustoDto.MapFromEntity(centroDeCusto);

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCentroDeCusto()
		{
			var centroDeCustos = await centroDeCustoRepository.GetAllAsync();

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
			var cdc = await centroDeCustoRepository.GetById(codCentroDeCusto);

			if (cdc is null)
				return NotFound();

			var response = CentroDeCustoDto.MapFromEntity(cdc);

			return Ok(response);
		}

		[HttpPut]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> UpdateCentroDeCustoById([FromRoute] int codCentroDeCusto, [FromBody] UpdateCentroDeCustoRequestDto request)
		{
			var cdc = request.MapToEntity();
			cdc.Cod = codCentroDeCusto;

			cdc = await centroDeCustoRepository.UpdateAsync(cdc);
			if (cdc is null)
				return NotFound();

			var response = CentroDeCustoDto.MapFromEntity(cdc);

			return Ok(response);

		}

		[HttpDelete]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> DeleteCentroDeCusto([FromRoute] int codCentroDeCusto)
		{
			var cdc = await centroDeCustoRepository.DeleteAsync(codCentroDeCusto);

			if (cdc is null) return NotFound();

			return Ok(cdc.Cod);
		}
	}
}
