using contratosimples_api.Application.Data;
using contratosimples_api.Application.Models.Entities;
using contratosimples_api.Application.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Repositories.Implementation
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
			await dbContext.Clientes.AddAsync(cliente);
			await dbContext.SaveChangesAsync();
			return cliente;
		}

		public async Task<Cliente?> DeleteAsync(int idCliente)
		{
			var cli = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == idCliente);
			if (cli == null)
				return null;
			dbContext.Clientes.Remove(cli);
			await dbContext.SaveChangesAsync();
			return cli;

		}

		public async Task<IEnumerable<Cliente>> GetAllAsync()
		{
			return await dbContext.Clientes.ToListAsync();
		}

		public async Task<Cliente?> GetById(int id)
		{
			return await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);
		}

		public async Task<Cliente?> UpdateAsync(Cliente cliente)
		{
			var cli = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Id == cliente.Id);
			if (cli == null)
				return null;
			dbContext.Entry(cli).CurrentValues.SetValues(cliente);
			await dbContext.SaveChangesAsync();
			return cli;
		}
	}
}
