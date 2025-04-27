namespace contratosimples_api.Application.Management.Models.DTO.AppCliente
{
	public class CreateTenantDto
	{
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }

		public Entities.Tenant MapToEntity() {
			return new Entities.Tenant {
				Cpf_cnpj = Cpf_cnpj,
				NomeFantasia = NomeFantasia,
				RazaoSocial = RazaoSocial
			};
		}
	}
}
