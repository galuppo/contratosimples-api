using contratosimples_api.Application.Management.Models.DTO.AppCliente;
using contratosimples_api.Application.Management.Models.Entities;
using contratosimples_api.Application.Management.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TenantController : ControllerBase
	{
		private readonly AppManagementUnitOfWork uow;

		private string GenerateDatabaseName(Tenant appCliente) {
			var cpfCnpj = appCliente.Cpf_cnpj;
			cpfCnpj = string.Concat(cpfCnpj.Where(char.IsDigit));
			return cpfCnpj;
		}

		public TenantController(AppManagementUnitOfWork uow) {
			this.uow = uow;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> TestUser() {
			return Ok(HttpContext.User);
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.APP_ADMIN)]
		public async Task<IActionResult> CreateTenant([FromBody] CreateTenantDto request) {
			var tenant = request.MapToEntity();
			tenant.DataBaseName = GenerateDatabaseName(tenant);

			//verifica se o CPF/CPNJ já esta cadastrado
			var existingTenant = await uow.TenantRepository.GetAsync(t => t.Cpf_cnpj == request.Cpf_cnpj);
			if (existingTenant != null) {
				ModelState.AddModelError("", "CPF/CNPJ já cadastrado!");
				return ValidationProblem(ModelState);
			}

			await uow.TenantRepository.InsertAsync(tenant);
			await uow.SaveAsync();

			var response = TenantDto.MapFromEntity(tenant);

			return Ok(response);
		}
	}
}
