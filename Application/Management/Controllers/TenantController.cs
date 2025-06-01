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
	[Authorize(Roles = UsuarioRole.APP_ADMIN)]
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

		[HttpPost]		
		public async Task<IActionResult> CreateTenant([FromBody] CreateTenantDto request) {
			
			//remove todos os caracteres que não sejam númericos
			request.Cpf_cnpj = string.Concat(request.Cpf_cnpj.Where(char.IsDigit));

			var tenant = request.MapToEntity();
			tenant.DataBaseName = GenerateDatabaseName(tenant);

			//verifica se o CPF/CPNJ já esta cadastrado
			var existingTenant = (await uow.TenantRepository.GetAsync(t => t.Cpf_cnpj == request.Cpf_cnpj)).FirstOrDefault();
			if (existingTenant != null) {
				ModelState.AddModelError("", "CPF/CNPJ já cadastrado!");
				return ValidationProblem(ModelState);
			}

			await uow.TenantRepository.InsertAsync(tenant);
			await uow.SaveAsync();

			var response = TenantDto.MapFromEntity(tenant);

			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll() {
			var tenants = await uow.TenantRepository.GetAsync();
			var response = new List<TenantDto>();

			foreach (var tenant in tenants)
				response.Add(TenantDto.MapFromEntity(tenant));

			return Ok(response);
		}
	}
}
