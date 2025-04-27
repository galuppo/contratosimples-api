namespace contratosimples_api.Application.Core.Models.DTO.Fornecedor
{
	public class FornecedorDto
	{
		public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoMunicipal { get; set; }
		public string InscricaoEstadual { get; set; }
		public string Avaliacao { get; set; }

		public static FornecedorDto MapFromEntity(Entities.Fornecedor fornecedor) {
			return new FornecedorDto {
				Id = fornecedor.Id,
				Avaliacao = fornecedor.Avaliacao,
				Cpf_cnpj = fornecedor.Cpf_cnpj,
				Endereco = fornecedor.Endereco,
				InscricaoEstadual = fornecedor.InscricaoEstadual,
				InscricaoMunicipal = fornecedor.InscricaoMunicipal,
				NomeFantasia = fornecedor.NomeFantasia,
				RazaoSocial = fornecedor.RazaoSocial
			};
		}
	}
}
