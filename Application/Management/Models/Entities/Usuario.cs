using Microsoft.AspNetCore.Identity;

namespace contratosimples_api.Application.Management.Models.Entities
{
	public class Usuario : IdentityUser
	{
		public List<Tenant> Tenants { get; set; }
	}
}
