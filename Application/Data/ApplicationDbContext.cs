using contratosimples_api.Application.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<CentroDeCusto> CentroDeCustos { get; set; }
		public DbSet<Cliente> Clientes { get; set; }
		public DbSet<Fornecedor> Fornecedores { get; set; }
		public DbSet<ContatoFornecedor> ContatosFornecedores { get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

	}
}
