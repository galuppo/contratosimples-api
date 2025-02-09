namespace contratosimples_api.Application.Models.DTO.Cliente
{
	public class ClienteDto
	{
		public int Id { get; set; }
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoEstadual { get; set; }
		public string InscricaoMunicipal { get; set; }

		public static ClienteDto MapFromEntity(Entities.Cliente cliente)
		{
			return new ClienteDto {
				Id = cliente.Id,
				Cpf_cnpj = cliente.Cpf_cnpj,
				Endereco = cliente.Endereco,
				InscricaoEstadual = cliente.InscricaoEstadual,
				InscricaoMunicipal = cliente.InscricaoMunicipal,
				NomeFantasia = cliente.NomeFantasia,
				RazaoSocial = cliente.RazaoSocial
			};
		}
	}

}
