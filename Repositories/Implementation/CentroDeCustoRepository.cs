using contratosimples_api.Data;
using contratosimples_api.Models.Domain;
using contratosimples_api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Repositories.Implementation
{
	public class CentroDeCustoRepository : ICentroDeCustoRepository
	{
		private readonly ApplicationDbContext dbContext;

		public CentroDeCustoRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<CentroDeCusto> CreateAsync(CentroDeCusto centroDeCusto)
		{
			await dbContext.CentroDeCustos.AddAsync(centroDeCusto);
			await dbContext.SaveChangesAsync();

			return centroDeCusto;
		}

		public async Task<IEnumerable<CentroDeCusto>> GetAllAsync()
		{
			return await dbContext.CentroDeCustos.ToListAsync();
		}
	}
}
