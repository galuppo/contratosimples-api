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

		public UsuarioController(UserManager<Usuario> userManager, TokenRepository tokenRepository) {
			this.userManager = userManager;
			this.tokenRepository = tokenRepository;
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("Register")]
		public async Task<IActionResult> Register([FromBody] UsuarioRegisterDto request) {
			//Create IdentityUser object
			var user = new Usuario {
				UserName = request.Email?.Trim(),
				Email = request.Email?.Trim()
			};

			//verifica se o email já esta cadastrado
			var existingUser = await userManager.FindByEmailAsync(user.Email);
			if (existingUser != null) {
				ModelState.AddModelError("", "Usuário com email já cadastrado!");
				return ValidationProblem(ModelState);
			}

			var identityResult = await userManager.CreateAsync(user, request.Password);
			if (identityResult.Succeeded)
				identityResult = await userManager.AddToRoleAsync(user, UsuarioRole.READER_ROLE);
			if (identityResult.Succeeded)
				identityResult = await userManager.AddToRoleAsync(user, UsuarioRole.WRITER_ROLE);
			if (identityResult.Succeeded)
				return Ok();

			if (identityResult.Errors.Any()) {
				foreach (var error in identityResult.Errors) {
					ModelState.AddModelError("", error.Description);
				}
			}

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
						Email = request.Email,
						Roles = roles.ToList(),
						Token = token
					};

					return Ok(response);
				}

			}

			ModelState.AddModelError("", "Email ou senha incorreto.");
			return ValidationProblem(ModelState);
		}
	}


}
