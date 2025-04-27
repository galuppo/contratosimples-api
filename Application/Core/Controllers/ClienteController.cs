using contratosimples_api.Application.Core.Models.DTO.Cliente;
using contratosimples_api.Application.Core.Repositories;
using contratosimples_api.Application.Management.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Core.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ClienteController : ControllerBase
	{

		private readonly ContratoSimplesUnitOfWork uow;

		public ClienteController(ContratoSimplesUnitOfWork uow) {
			this.uow = uow;
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		public async Task<IActionResult> CreateCliente([FromBody] CreateClienteRequestDto request) {
			var cliente = request.MapToEntity();

			await uow.ClienteRepository.InsertAsync(cliente);
			await uow.SaveAsync();

			var response = ClienteDto.MapFromEntity(cliente);

			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		public async Task<IActionResult> GetAllCliente() {
			var clientes = await uow.ClienteRepository.GetAsync();

			var response = new List<ClienteDto>();
			foreach (var c in clientes) {
				response.Add(ClienteDto.MapFromEntity(c));
			}
			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> GetClienteById([FromRoute] int idCliente) {
			var c = await uow.ClienteRepository.GetByIdAsync(idCliente);

			if (c == null)
				return NotFound();

			var response = ClienteDto.MapFromEntity(c);


			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> UpdateClienteById([FromRoute] int idCliente, [FromBody] UpdateClienteRequestDto request) {
			var cli = request.MapToEntity();
			cli.Id = idCliente;

			cli = await uow.ClienteRepository.UpdateAsync(idCliente, cli);
			if (cli == null)
				return NotFound();
			await uow.SaveAsync();
			var reponse = ClienteDto.MapFromEntity(cli);
			return Ok(reponse);
		}

		[HttpDelete]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idCliente:int}")]
		public async Task<IActionResult> DeleteCliente([FromRoute] int idCliente) {
			var cli = await uow.ClienteRepository.DeleteAsync(idCliente);
			if (cli == null) return NotFound();

			await uow.SaveAsync();

			return Ok(cli.Id);
		}

	}
}

