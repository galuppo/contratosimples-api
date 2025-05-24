using contratosimples_api.Application.Management.Models.DTO.AppCliente;

namespace contratosimples_api.Application.Management.Models.DTO.Usuario
{
	public class UsuarioDto
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public List<TenantDto> Tenants { get; set; }

		public static UsuarioDto MapFromEntity(Entities.Usuario entity) {
			return new UsuarioDto {
				Email = entity.Email,
				UserName = entity.UserName,
				Tenants = entity.Tenants == null ? null : entity.Tenants.Select( t => TenantDto.MapFromEntity(t)).ToList(),
			};
		}
	}
}
