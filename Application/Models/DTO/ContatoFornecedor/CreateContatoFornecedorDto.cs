namespace contratosimples_api.Application.Models.DTO.ContatoFornecedor
{
	public class CreateContatoFornecedorDto
	{
		public string Nome { get; set; }
		public string Cargo { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }

		public Entities.ContatoFornecedor MapToEntity()
		{
			return new Entities.ContatoFornecedor
			{
				Nome = this.Nome,
				Cargo = this.Cargo,
				Telefone = this.Telefone,
				Email = this.Email
			};
		}
	}
}
