using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Models.Entities
{
	public class ContatoFornecedor 
	{
		[Key] public int Id { get; set; }
		public string Nome { get; set; }
		public string Cargo { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }
		public int FornecedorId { get; set; }

	}
}
