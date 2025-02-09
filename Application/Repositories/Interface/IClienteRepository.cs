using contratosimples_api.Application.Models.Entities;

namespace contratosimples_api.Application.Repositories.Interface
{
	public interface IClienteRepository
	{
		Task<Cliente> CreateAsync(Cliente cliente);
		Task<IEnumerable<Cliente>> GetAllAsync();
		Task<Cliente?> GetById(int id);
		Task<Cliente?> UpdateAsync(Cliente cliente);
		Task<Cliente?> DeleteAsync(int idCliente);
	}
}
