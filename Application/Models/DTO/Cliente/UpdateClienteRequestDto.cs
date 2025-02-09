namespace contratosimples_api.Application.Models.DTO.Cliente
{
	public class UpdateClienteRequestDto
	{
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoEstadual { get; set; }
		public string InscricaoMunicipal { get; set; }

		public Entities.Cliente MapToEntity()
		{
			return new Entities.Cliente
			{
				Cpf_cnpj = this.Cpf_cnpj,
				Endereco = this.Endereco,
				InscricaoEstadual = this.InscricaoEstadual,
				InscricaoMunicipal = this.InscricaoMunicipal,
				NomeFantasia = this.NomeFantasia,
				RazaoSocial = this.RazaoSocial
			};
		}
	}
}
