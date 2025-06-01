using contratosimples_api.Application.Management.Models.Entities;
using contratosimples_api.Application.Management.Repositories;
using contratosimples_api.Application.Management.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Security.Claims;

namespace contratosimples_api.Application.Management.Middlewares
{
	public class TenantAuthorizationMiddleware
	{
		private readonly RequestDelegate _next;

		private bool IsUserAdmin(HttpContext httpContext) {
			ClaimsPrincipal user = httpContext.User;
			var roles = user.FindAll(ClaimTypes.Role);

			foreach (var role in roles) {
				if (role.Value == UsuarioRole.APP_ADMIN)
					return true;
			}

			return false;
		}

		private bool NeedTenantAuthorization(HttpContext httpContext) {
			var requestPath = httpContext.Request.Path.Value.ToLower();

			//apenas os seguintes controllers não precisam de autorização baseada no tenant
			if (requestPath.StartsWith("/api/Usuario".ToLower()))
				return false;
			if(requestPath.StartsWith("/api/Tenant".ToLower()))
				return false;
			if(requestPath == "/openapi/v1.json")
				return false;

			return true;

		}

		public TenantAuthorizationMiddleware(RequestDelegate next) {
			_next = next;

		}


		public async Task InvokeAsync(HttpContext httpContext, AppManagementUnitOfWork uow, TenantService tenantService) {

			if (NeedTenantAuthorization(httpContext)) {
				var requestUser = httpContext.User;
				var tenantHeader = httpContext.Request.Headers["Tenant"];

				//verifica se foi especificado um usuário no request
				if (requestUser == null) {
					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					await httpContext.Response.WriteAsync("Usuário não identificado");
					return;
				}

				//verifica se foi identificado o tenant/cliente do request
				if (tenantHeader.Count == 0) {
					httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					await httpContext.Response.WriteAsync("Cliente não identificado");
					return;
				}


				//Verifica se o tenant existe
				var tenantCpfCnpj = tenantHeader.First().ToString();
				Tenant? t = (await uow.TenantRepository.GetAsync(t => t.Cpf_cnpj == tenantCpfCnpj)).FirstOrDefault();
				if (t == null) {
					httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
					await httpContext.Response.WriteAsync("Cliente não encontrado");
					return;
				}

				//Caso o usuário não seja um admin do app, ele precisa estar relacionado ao tenant
				if (!IsUserAdmin(httpContext)) {
					var userEmail = requestUser.FindFirstValue(ClaimTypes.Email);
					var user = (await uow.UsuarioRepository.GetAsync(u => u.Email == userEmail, null, "Tenants")).First();
					var tenant = user.Tenants.Find(t => t.Cpf_cnpj == tenantCpfCnpj);
					//se não encontrou o tenant relacionado ao usuário, retorna erro
					if(tenant is null) {
						httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						await httpContext.Response.WriteAsync("Usuário não está relacionado com o cliente");
						return;
					}
				}
				//Se passou por todas as validações, define o tenant do request
				tenantService.TransactionTenant = t;
			}
			await _next(httpContext);
		}
	}

	// Extension method used to add the middleware to the HTTP request pipeline.
	public static class TenantAuthorizationMiddlewareExtensions
	{
		public static IApplicationBuilder UseTenantAuthorization(this IApplicationBuilder builder) {
			return builder.UseMiddleware<TenantAuthorizationMiddleware>();
		}
	}
}
