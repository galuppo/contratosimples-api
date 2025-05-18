namespace contratosimples_api.Application.Management.Models.DTO.Usuario
{
	public class UsuarioDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }

		public static UsuarioDto MapFromEntity(Entities.Usuario entity) {
			return new UsuarioDto {
				Email = entity.Email,
				UserName = entity.UserName
			};
		}
	}
}
