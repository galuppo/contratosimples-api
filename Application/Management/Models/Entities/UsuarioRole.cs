using Microsoft.AspNetCore.Identity;

namespace contratosimples_api.Application.Management.Models.Entities
{
	public class UsuarioRole : IdentityRole
	{
		public const string WRITER_ROLE = "Writer";
		public const string READER_ROLE = "Reader";
		public const string APP_ADMIN = "App Admin";
	}
}
