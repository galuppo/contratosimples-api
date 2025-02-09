using contratosimples_api.Models.Domain;

namespace contratosimples_api.Repositories.Interface
{
	public interface ICentroDeCustoRepository
	{
		Task<CentroDeCusto> CreateAsync(CentroDeCusto centroDeCusto);
		Task<IEnumerable<CentroDeCusto>> GetAllAsync();
		Task<CentroDeCusto?> GetById(int cod);
		Task<CentroDeCusto?> UpdateAsync(CentroDeCusto centroDeCusto);
		Task<CentroDeCusto?> DeleteAsync(int cod);
	}
}
