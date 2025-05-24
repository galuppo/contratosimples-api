using contratosimples_api.Application.Management.Data;
using contratosimples_api.Application.Management.Models.DTO.AppCliente;
using contratosimples_api.Application.Management.Models.DTO.Usuario;
using contratosimples_api.Application.Management.Models.Entities;
using contratosimples_api.Application.Management.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Management.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsuarioController : ControllerBase
	{
		private readonly UserManager<Usuario> userManager;
		private readonly TokenRepository tokenRepository;
		private readonly AppManagementUnitOfWork uow;

		public UsuarioController(UserManager<Usuario> userManager, TokenRepository tokenRepository, AppManagementUnitOfWork uow) {
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
			this.uow = uow;
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto request) {
			//Create IdentityUser object
			var user = new Usuario {
				UserName = request.UserName.Trim(),
				Email = request.Email?.Trim()
			};

			//verifica se o email já esta cadastrado
			var existingUser = await userManager.FindByEmailAsync(user.Email);
			if (existingUser != null) {
				ModelState.AddModelError("", "Usuário com email já cadastrado!");
				return ValidationProblem(ModelState);
			}

			try {
				await uow.BeginTransactionAsync();

				var identityResult = await userManager.CreateAsync(user, request.Password);
				if (identityResult.Succeeded)
					identityResult = await userManager.AddToRoleAsync(user, UsuarioRole.READER_ROLE);
				if (identityResult.Succeeded)
					identityResult = await userManager.AddToRoleAsync(user, UsuarioRole.WRITER_ROLE);
				if (identityResult.Succeeded) {

					if (request.TenantCPF_CNPJ != null) {
						Usuario u = await uow.UsuarioRepository.GetByIdAsync(user.Id);
						Tenant t = (await uow.TenantRepository.GetAsync(t => t.Cpf_cnpj == request.TenantCPF_CNPJ, null, "Usuarios")).FirstOrDefault();
						if (t == null) {
							ModelState.AddModelError("", "Tenant CPF/CNPJ " + request.TenantCPF_CNPJ + " não encontrado!");
							return ValidationProblem(ModelState);
						}

						t.Usuarios.Add(u);
						await uow.SaveAsync();

					}
					await uow.EndTransactionAsync();
					return Ok();
				}

				if (identityResult.Errors.Any()) {
					foreach (var error in identityResult.Errors) {
						ModelState.AddModelError("", error.Description);
					}
				}

			} catch (Exception ex) {
				await uow.RollBackTransactionAsync();
				throw;
			}
			await uow.RollBackTransactionAsync();
			return ValidationProblem(ModelState);
		}
		
		[HttpPost]
		[Route("Login")]
		public async Task<IActionResult> Login([FromBody] LoginRequestDto request) {
			//check email
			var identityUser = await userManager.FindByEmailAsync(request.Email);

			//check password
			if (identityUser is not null) {
				var checkPassResult = await userManager.CheckPasswordAsync(identityUser, request.Password);

				if (checkPassResult) {
					var roles = await userManager.GetRolesAsync(identityUser);

					var token = tokenRepository.CreateJwtToken(identityUser, roles);



					var response = new LoginResponseDto() {
						UserName = identityUser.UserName,
						Email = request.Email,
						Roles = roles.ToList(),
						Token = token,
						Tenants = new List<TenantDto>()
					};

					IEnumerable<Tenant> tenants;

					//se o usuário for admin do app, retorna todos os tenants
					if (roles.Contains(UsuarioRole.APP_ADMIN)) {
						tenants = await uow.TenantRepository.GetAsync();
					} else {
						Usuario user = (await uow.UsuarioRepository.GetAsync(u => u.Id == identityUser.Id, null, "Tenants")).First();
						tenants = user.Tenants;
					}					

					foreach (var tenant in tenants) 
						response.Tenants.Add(TenantDto.MapFromEntity(tenant));
						

					return Ok(response);
				}
			}
			ModelState.AddModelError("", "Email ou senha incorreto.");
			return ValidationProblem(ModelState);
		}
		
		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("Relate")]
		public async Task<IActionResult> RelateTenant([FromBody] RelateUserTenantRequestDto request) {
			var user = (await uow.UsuarioRepository.GetAsync(u => u.Email == request.UserEmail, null, "Tenants")).FirstOrDefault();
			if (user == null)
				return BadRequest("Usuário não encontrado!");

			var tenant = (await uow.TenantRepository.GetAsync(t => t.Cpf_cnpj == request.TenantCPF_CNPJ)).FirstOrDefault();
			if (tenant == null)
				return BadRequest("Tenant não encontrado!");

			user.Tenants.Add(tenant);
			await uow.UsuarioRepository.UpdateAsync(user.Id, user);
			await uow.SaveAsync();

			return Ok();
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("Tenant/{tenantCpfCnpj}")]
		public async Task<IActionResult> GetAllUsuariosByTenant([FromRoute] string tenantCpfCnpj) {
			var tenant = (await uow.TenantRepository.GetAsync(t => t.Cpf_cnpj == tenantCpfCnpj, null, "Usuarios")).FirstOrDefault();
			
			if(tenant == null) 
				return BadRequest("Tenant CPF/CNPJ "+tenantCpfCnpj+" não encontrado!");

			var response = new List<UsuarioDto>();
			foreach (var user in tenant.Usuarios)		
				response.Add(UsuarioDto.MapFromEntity(user));

			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{email}")]
		public async Task<IActionResult> GetUserByEmail([FromRoute] string email) {
			var user = (await uow.UsuarioRepository.GetAsync(u => u.Email == email.ToLower(), null, "Tenants")).FirstOrDefault();

			if (user == null)
				return NotFound();

			var response = UsuarioDto.MapFromEntity(user);
			return Ok(response);
		}
		
	
	
	}
}
