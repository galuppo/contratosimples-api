using contratosimples_api.Data;
using contratosimples_api.Models.Domain;
using contratosimples_api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Repositories.Implementation
{
	public class ClienteRepository : IClienteRepository
	{
		private readonly ApplicationDbContext dbContext;

		public ClienteRepository(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<Cliente> CreateAsync(Cliente cliente)
		{
			await dbContext.Cliente.AddAsync(cliente);
			await dbContext.SaveChangesAsync();
			return cliente;
		}

		public async Task<Cliente?> DeleteAsync(int idCliente)
		{
			var cli = await dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == idCliente);
			if (cli == null)
				return null;
			dbContext.Cliente.Remove(cli);
			await dbContext.SaveChangesAsync();
			return cli;

		}

		public async Task<IEnumerable<Cliente>> GetAllAsync()
		{
			return await dbContext.Cliente.ToListAsync();
		}

		public async Task<Cliente?> GetById(int id)
		{
			return await dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Cliente?> UpdateAsync(Cliente cliente)
		{
			var cli = await dbContext.Cliente.FirstOrDefaultAsync(c => c.Id == cliente.Id);
			if (cli == null)
				return null;
			dbContext.Entry(cli).CurrentValues.SetValues(cliente);
			await dbContext.SaveChangesAsync();
			return cli;
		}
	}
}
