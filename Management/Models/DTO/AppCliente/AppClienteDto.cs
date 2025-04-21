using contratosimples_api.Management.Models.Entities;

namespace contratosimples_api.Management.Models.DTO.AppCliente
{
	public class AppClienteDto
	{		
		public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string DataBaseName { get; set; }

		public static AppClienteDto MapFromEntity(Entities.AppCliente entity) {
			return new AppClienteDto {
				Id = entity.Id,
				Cpf_cnpj = entity.Cpf_cnpj,
				DataBaseName = entity.DataBaseName,
				NomeFantasia = entity.NomeFantasia,
				RazaoSocial = entity.RazaoSocial
			};
		}
	}
}
