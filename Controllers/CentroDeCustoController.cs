using contratosimples_api.Data;
using contratosimples_api.Models.Domain;
using contratosimples_api.Models.DTO;
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
		public async Task<IActionResult> CreateCentroDeCusto(CreateCentroDeCustoRequestDto request)
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
	}
}
