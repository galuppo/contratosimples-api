using contratosimples_api.Application.Core.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Core.Data
{
	public class ContratoSimplesDbContext : DbContext
	{
		public DbSet<CentroDeCusto> CentroDeCusto { get; set; }
		public DbSet<Cliente> Cliente { get; set; }
		public DbSet<Fornecedor> Fornecedor { get; set; }
		public DbSet<ContatoFornecedor> ContatoFornecedor { get; set; }
		public DbSet<Contrato> Contrato { get; set; }
		public DbSet<ContratoItem> ContratoItem { get; set; }
		public DbSet<Aditivo> Aditivo { get; set; }
		public DbSet<AditivoItem> AditivoItem { get; set; }

		public ContratoSimplesDbContext(DbContextOptions<ContratoSimplesDbContext> options) : base(options) {
			AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		}

	}
}
