using contratosimples_api.Application.Models.DTO.CentroDeCusto;
using contratosimples_api.Application.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Controllers
{

	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CentroDeCustoController : ControllerBase
	{
		private readonly UnitOfWork uow;

		public CentroDeCustoController(UnitOfWork uow)
		{
			this.uow = uow;
		}

		[HttpPost]
		[Authorize(Roles = "Writer")]
		public async Task<IActionResult> CreateCentroDeCusto([FromBody] CreateCentroDeCustoRequestDto request)
		{
			var centroDeCusto = request.MapToEntity();

			await uow.CentroDeCustoRepository.InsertAsync(centroDeCusto);
			await uow.SaveAsync();

			var response = CentroDeCustoDto.MapFromEntity(centroDeCusto);

			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = "Reader")]
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
		[Authorize(Roles = "Reader")]
		[Route("{idCentroDeCusto:int}")]
		public async Task<IActionResult> GetCentroDeCustoById([FromRoute] int idCentroDeCusto)
		{
			var cdc = await uow.CentroDeCustoRepository.GetByIdAsync(idCentroDeCusto);

			if (cdc is null)
				return NotFound();

			var response = CentroDeCustoDto.MapFromEntity(cdc);

			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = "Writer")]
		[Route("{idCentroDeCusto:int}")]
		public async Task<IActionResult> UpdateCentroDeCustoById([FromRoute] int idCentroDeCusto, [FromBody] UpdateCentroDeCustoRequestDto request)
		{
			var cdc = await uow.CentroDeCustoRepository.GetByIdAsync(idCentroDeCusto);
			if (cdc is null)
				return NotFound();

			cdc = request.MapToEntity();
			cdc.Id = idCentroDeCusto;

			await uow.CentroDeCustoRepository.UpdateAsync(idCentroDeCusto, cdc);
			await uow.SaveAsync();

			var response = CentroDeCustoDto.MapFromEntity(cdc);

			return Ok(response);

		}

		[HttpDelete]
		[Authorize(Roles = "Writer")]
		[Route("{idCentroDeCusto:int}")]
		public async Task<IActionResult> DeleteCentroDeCusto([FromRoute] int idCentroDeCusto)
		{
			var cdc = await uow.CentroDeCustoRepository.DeleteAsync(idCentroDeCusto);

			if (cdc is null) return NotFound();

			await uow.SaveAsync();

			return Ok(cdc.Id);
		}
	}
}
