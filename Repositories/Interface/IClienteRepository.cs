using contratosimples_api.Models.Domain;

namespace contratosimples_api.Repositories.Interface
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
