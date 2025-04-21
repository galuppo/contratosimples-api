using contratosimples_api.Management.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace contratosimples_api.Common.Models.Entities
{
	public class Usuario : IdentityUser
	{
		public AppCliente[] Clientes { get; set; }
	}
}
