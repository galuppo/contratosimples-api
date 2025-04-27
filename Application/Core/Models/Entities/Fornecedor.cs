using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class Fornecedor
	{
		[Key] public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoMunicipal { get; set; }
		public string InscricaoEstadual { get; set; }
		public string Avaliacao { get; set; }
		public List<ContatoFornecedor> Contatos { get; set; }
	}
}
