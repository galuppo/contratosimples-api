using contratosimples_api.Application.Management.Models.DTO.AppCliente;

namespace contratosimples_api.Application.Management.Models.DTO.Usuario
{
	public class LoginResponseDto
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public List<string> Roles { get; set; }
		public List<TenantDto> Tenants { get; set; }
	}
}
