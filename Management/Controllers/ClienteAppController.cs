using contratosimples_api.Common.Models.Entities;
using contratosimples_api.Management.Models.DTO.AppCliente;
using contratosimples_api.Management.Models.Entities;
using contratosimples_api.Management.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClienteAppController : ControllerBase
	{
		private readonly AppManagementUnitOfWork uow;

		private string GenerateDatabaseName(AppCliente appCliente) {
			var cpfCnpj = appCliente.Cpf_cnpj;	
			cpfCnpj = string.Concat(cpfCnpj.Where(Char.IsDigit));
			return cpfCnpj;
		}

		public ClienteAppController(AppManagementUnitOfWork uow) {
			this.uow = uow;
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> TestUser() {
			return Ok(HttpContext.User);
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.APP_ADMIN)]
		public async Task<IActionResult> CreateClienteApp([FromBody] CreateAppClienteDto request) {
			var appCliente = request.MapToEntity();
			appCliente.DataBaseName = GenerateDatabaseName(appCliente);

			await uow.AppClienteRepository.InsertAsync(appCliente);
			await uow.SaveAsync();

			var response = AppClienteDto.MapFromEntity(appCliente);

			return Ok(response);
		}
	}
}
