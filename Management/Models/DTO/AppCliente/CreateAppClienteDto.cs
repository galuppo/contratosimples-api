namespace contratosimples_api.Management.Models.DTO.AppCliente
{
	public class CreateAppClienteDto
	{
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }

		public Entities.AppCliente MapToEntity() {
			return new Entities.AppCliente {
				Cpf_cnpj = Cpf_cnpj,
				NomeFantasia = NomeFantasia,
				RazaoSocial = RazaoSocial
			};
		}
	}
}
