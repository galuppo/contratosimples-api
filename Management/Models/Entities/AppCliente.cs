using contratosimples_api.Common.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Management.Models.Entities
{
	public class AppCliente
	{
		[Key]
		public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string DataBaseName { get; set; }
		public Usuario[] Usuarios { get; set; }
	}
}
