namespace contratosimples_api.Application.Management.Models.DTO.Usuario
{
	public class LoginResponseDto
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public List<string> Roles { get; set; }
	}
}
