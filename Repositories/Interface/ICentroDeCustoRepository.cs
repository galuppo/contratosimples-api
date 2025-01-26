using contratosimples_api.Models.Domain;

namespace contratosimples_api.Repositories.Interface
{
	public interface ICentroDeCustoRepository
	{
		Task<CentroDeCusto> CreateAsync(CentroDeCusto centroDeCusto);
	}
}
