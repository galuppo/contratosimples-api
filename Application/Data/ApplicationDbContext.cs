using contratosimples_api.Application.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<CentroDeCusto> CentroDeCusto { get; set; }
		public DbSet<Cliente> Cliente { get; set; }
		public DbSet<Fornecedor> Fornecedor { get; set; }
		public DbSet<ContatoFornecedor> ContatoFornecedor { get; set; }
		public DbSet<Contrato> Contrato { get; set; }
		public DbSet<ContratoItem> ContratoItem { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

	}
}
