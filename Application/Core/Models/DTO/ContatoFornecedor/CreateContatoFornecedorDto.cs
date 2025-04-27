namespace contratosimples_api.Application.Core.Models.DTO.ContatoFornecedor
{
	public class CreateContatoFornecedorDto
	{
		public string Nome { get; set; }
		public string Cargo { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }

		public Entities.ContatoFornecedor MapToEntity() {
			return new Entities.ContatoFornecedor {
				Nome = Nome,
				Cargo = Cargo,
				Telefone = Telefone,
				Email = Email
			};
		}
	}
}
