using contratosimples_api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<CentroDeCusto> CentroDeCustos { get; set; }
		public DbSet<Cliente> Cliente { get; set; }

		public ApplicationDbContext(DbContextOptions options) : base(options)
		{
		}

	}
}
