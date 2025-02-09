using Microsoft.AspNetCore.Mvc;
using contratosimples_api.Application.Models.DTO.Cliente;
using contratosimples_api.Application.Repositories.Interface;
using contratosimples_api.Application.Models.Entities;

namespace contratosimples_api.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteController : ControllerBase
	{
		private readonly IClienteRepository clienteRepository;

		public ClienteController(IClienteRepository clienteRepository)
		{
			this.clienteRepository = clienteRepository;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCliente([FromBody] CreateClienteRequestDto request)
		{
			var cliente = request.MapToEntity();

			await clienteRepository.CreateAsync(cliente);

			var response = ClienteDto.MapFromEntity(cliente);

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCliente()
		{
			var clientes = await clienteRepository.GetAllAsync();

			var response = new List<ClienteDto>();
			foreach (var c in clientes)
			{
				response.Add(ClienteDto.MapFromEntity(c));
			}
			return Ok(response);
		}

		[HttpGet]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> GetClienteById([FromRoute] int idCliente)
		{
			var c = await clienteRepository.GetById(idCliente);

			if (c == null)
				return NotFound();

			var response = ClienteDto.MapFromEntity(c);


			return Ok(response);
		}

		[HttpPut]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> UpdateClienteById([FromRoute] int idCliente, [FromBody] UpdateClienteRequestDto request)
		{
			var cli = request.MapToEntity();
			cli.Id = idCliente;

			cli = await clienteRepository.UpdateAsync(cli);
			if (cli == null)
				return NotFound();

			var reponse = ClienteDto.MapFromEntity(cli);
			return Ok(reponse);
		}

		[HttpDelete]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> DeleteCliente([FromRoute] int idCliente)
		{
			var cli = await clienteRepository.DeleteAsync(idCliente);
			if (cli == null) return NotFound();

			return Ok(cli.Id);
		}

	}
}

