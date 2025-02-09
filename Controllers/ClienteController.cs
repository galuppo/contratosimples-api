using contratosimples_api.Models.Domain;
using contratosimples_api.Models.DTO.Cliente;
using contratosimples_api.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Controllers
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
			var cliente = new Cliente
			{
				Cpf_cnpj = request.Cpf_cnpj,
				Endereco = request.Endereco,
				InscricaoEstadual = request.InscricaoEstadual,
				InscricaoMunicipal = request.InscricaoMunicipal,
				NomeFantasia = request.NomeFantasia,
				RazaoSocial = request.RazaoSocial
			};

			await clienteRepository.CreateAsync(cliente);

			var response = new ClienteDto
			{
				Id = cliente.Id,
				Cpf_cnpj = cliente.Cpf_cnpj,
				Endereco = cliente.Endereco,
				InscricaoEstadual = cliente.InscricaoEstadual,
				InscricaoMunicipal = cliente.InscricaoMunicipal,
				NomeFantasia = cliente.NomeFantasia,
				RazaoSocial = cliente.RazaoSocial
			};

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCliente()
		{
			var clientes = await clienteRepository.GetAllAsync();

			var response = new List<ClienteDto>();
			foreach (var c in clientes)
			{
				response.Add(new ClienteDto
				{
					Id = c.Id,
					Cpf_cnpj = c.Cpf_cnpj,
					Endereco = c.Endereco,
					InscricaoEstadual = c.InscricaoEstadual,
					InscricaoMunicipal = c.InscricaoMunicipal,
					NomeFantasia = c.NomeFantasia,
					RazaoSocial = c.RazaoSocial
				});
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

			var response = new ClienteDto
			{
				Id = c.Id,
				Cpf_cnpj = c.Cpf_cnpj,
				Endereco = c.Endereco,
				InscricaoEstadual = c.InscricaoEstadual,
				InscricaoMunicipal = c.InscricaoMunicipal,
				NomeFantasia = c.NomeFantasia,
				RazaoSocial = c.RazaoSocial
			};
			return Ok(response);
		}

		[HttpPut]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> UpdateClienteById([FromRoute] int idCliente, [FromBody] UpdateClienteRequestDto request)
		{
			var cli = new Cliente
			{
				Id = idCliente,
				Cpf_cnpj = request.Cpf_cnpj,
				Endereco = request.Endereco,
				InscricaoEstadual = request.InscricaoEstadual,
				InscricaoMunicipal = request.InscricaoMunicipal,
				NomeFantasia = request.NomeFantasia,
				RazaoSocial = request.RazaoSocial
			};

			cli = await clienteRepository.UpdateAsync(cli);
			if (cli == null) 
				return NotFound();

			var reponse = new ClienteDto
			{
				Id = cli.Id,
				Cpf_cnpj = cli.Cpf_cnpj,
				Endereco = cli.Endereco,
				InscricaoEstadual = cli.InscricaoEstadual,
				InscricaoMunicipal = cli.InscricaoMunicipal,
				NomeFantasia = cli.NomeFantasia,
				RazaoSocial = cli.RazaoSocial
			};
			return Ok(reponse);
		}

		[HttpDelete]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> DeleteCliente([FromRoute]int idCliente)
		{
			var cli = await clienteRepository.DeleteAsync(idCliente);
			if (cli == null) return NotFound();

			return Ok(cli.Id);
		}

	}
}

