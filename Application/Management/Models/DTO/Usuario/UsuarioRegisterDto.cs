namespace contratosimples_api.Application.Management.Models.DTO.Usuario
{
	public class UsuarioRegisterDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string TenantCPF_CNPJ { get; set; }
	}
}
