namespace contratosimples_api.Application.Management.Models.DTO.AppCliente
{
	public class TenantDto
	{
		public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }

		public static TenantDto MapFromEntity(Entities.Tenant entity) {
			return new TenantDto {
				Id = entity.Id,
				Cpf_cnpj = entity.Cpf_cnpj,
				NomeFantasia = entity.NomeFantasia,
				RazaoSocial = entity.RazaoSocial
			};
		}
	}
}
