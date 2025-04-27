namespace contratosimples_api.Application.Core.Models.DTO.Cliente
{
	public class CreateClienteRequestDto
	{
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoEstadual { get; set; }
		public string InscricaoMunicipal { get; set; }

		public Entities.Cliente MapToEntity() {
			return new Entities.Cliente {
				Cpf_cnpj = Cpf_cnpj,
				Endereco = Endereco,
				InscricaoEstadual = InscricaoEstadual,
				InscricaoMunicipal = InscricaoMunicipal,
				NomeFantasia = NomeFantasia,
				RazaoSocial = RazaoSocial
			};
		}
	}
}
