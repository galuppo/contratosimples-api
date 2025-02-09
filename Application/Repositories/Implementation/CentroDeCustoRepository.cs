using contratosimples_api.Application.Data;
using contratosimples_api.Application.Models.Entities;
using contratosimples_api.Application.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Repositories.Implementation
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

		public async Task<CentroDeCusto?> DeleteAsync(int cod)
		{
			var cdc = await dbContext.CentroDeCustos.FirstOrDefaultAsync(c => c.Cod == cod);
			if (cdc == null)
				return null;

			dbContext.CentroDeCustos.Remove(cdc);
			await dbContext.SaveChangesAsync();
			return cdc;
		}

		public async Task<IEnumerable<CentroDeCusto>> GetAllAsync()
		{
			return await dbContext.CentroDeCustos.ToListAsync();
		}

		public async Task<CentroDeCusto?> GetById(int cod)
		{
			return await dbContext.CentroDeCustos.FirstOrDefaultAsync(c => c.Cod == cod);
		}

		public async Task<CentroDeCusto?> UpdateAsync(CentroDeCusto centroDeCusto)
		{
			var cdc = await dbContext.CentroDeCustos.FirstOrDefaultAsync(c => c.Cod == centroDeCusto.Cod);

			if (cdc != null)
			{
				dbContext.Entry(cdc).CurrentValues.SetValues(centroDeCusto);
				await dbContext.SaveChangesAsync();
				return centroDeCusto;
			}

			return null;
		}
	}
}
