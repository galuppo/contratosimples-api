namespace contratosimples_api.Models.DTO.Cliente
{
	public class UpdateClienteRequestDto
	{
		public string RazaoSocial { get; set; }
		public string NomeFantasia { get; set; }
		public string Cpf_cnpj { get; set; }
		public string Endereco { get; set; }
		public string InscricaoEstadual { get; set; }
		public string InscricaoMunicipal { get; set; }
	}
}
