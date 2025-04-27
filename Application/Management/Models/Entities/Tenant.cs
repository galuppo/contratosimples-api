using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Management.Models.Entities
{
	public class Tenant
	{
		[Key]
		public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string DataBaseName { get; set; }
		public List<Usuario> Usuarios { get; set; }
	}
}
