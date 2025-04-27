using contratosimples_api.Application.Core.Models.DTO.ContatoFornecedor;

namespace contratosimples_api.Application.Core.Models.DTO.Fornecedor
{
	public class UpdateFornecedorRequestDto
	{
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoMunicipal { get; set; }
		public string InscricaoEstadual { get; set; }
		public string Avaliacao { get; set; }
		public List<ContatoFornecedorDto> ContatosToUpdate { get; set; }
		public List<CreateContatoFornecedorDto> ContatosToAdd { get; set; }
		public List<int> ContatosToDelete { get; set; }

		public Entities.Fornecedor MapToEntity() {
			var fornecedor = new Entities.Fornecedor {
				RazaoSocial = RazaoSocial,
				NomeFantasia = NomeFantasia,
				Cpf_cnpj = Cpf_cnpj,
				Endereco = Endereco,
				InscricaoEstadual = InscricaoEstadual,
				InscricaoMunicipal = InscricaoMunicipal,
				Avaliacao = Avaliacao
			};
			return fornecedor;
		}
	}
}
