namespace contratosimples_api.Application.Models.DTO.ContatoFornecedor
{
	public class ContatoFornecedorDto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Cargo { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }

		public static ContatoFornecedorDto MapFromEntity(Entities.ContatoFornecedor contato)
		{
			return new ContatoFornecedorDto
			{
				Id = contato.Id,
				Nome = contato.Nome,
				Cargo = contato.Cargo,
				Email = contato.Email,
				Telefone = contato.Telefone
			};
		}
	}
}
