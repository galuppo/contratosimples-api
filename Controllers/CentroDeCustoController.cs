using contratosimples_api.Data;
using contratosimples_api.Models.Domain;
using contratosimples_api.Models.DTO.CentroDeCusto;
using contratosimples_api.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Controllers
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
		public async Task<IActionResult> CreateCentroDeCusto([FromBody]CreateCentroDeCustoRequestDto request)
		{
			var centroDeCusto = new CentroDeCusto { 
				Nome = request.Nome,
				Obs = request.Obs
			};

			await centroDeCustoRepository.CreateAsync(centroDeCusto);

			var response = new CentroDeCustoDto
			{
				Cod = centroDeCusto.Cod,
				Nome = centroDeCusto.Nome,
				Obs = centroDeCusto.Obs
			};

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCentroDeCusto()
		{
			var centroDeCustos = await centroDeCustoRepository.GetAllAsync();

			var response = new List<CentroDeCustoDto>();
			foreach (var cdc in centroDeCustos)
			{
				response.Add(new CentroDeCustoDto
				{
					Cod = cdc.Cod,
					Nome = cdc.Nome,
					Obs = cdc.Obs
				});
			}
			return Ok(response);

		}

		[HttpGet]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> GetCentroDeCustoById([FromRoute]int codCentroDeCusto)
		{
			var cdc = await centroDeCustoRepository.GetById(codCentroDeCusto);
			
			if (cdc is null)
				return NotFound();

			var response = new CentroDeCustoDto
			{
				Cod = cdc.Cod,
				Nome = cdc.Nome,
				Obs = cdc.Obs,
			};

			return Ok(response);
		}

		[HttpPut]
		[Route("{codCentroDeCusto:int}")]
		public async Task<IActionResult> UpdateCentroDeCustoById([FromRoute]int codCentroDeCusto, [FromBody]UpdateCentroDeCustoRequestDto request)
		{
			var cdc = new CentroDeCusto
			{
				Cod = codCentroDeCusto,
				Nome = request.Nome,
				Obs = request.Obs
			};

			cdc = await centroDeCustoRepository.UpdateAsync(cdc);
			if (cdc is null)
				return NotFound();

			var response = new CentroDeCustoDto
			{
				Cod = cdc.Cod,
				Nome = cdc.Nome,
				Obs = cdc.Obs
			};

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
