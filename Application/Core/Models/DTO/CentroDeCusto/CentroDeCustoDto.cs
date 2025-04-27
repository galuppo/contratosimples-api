namespace contratosimples_api.Application.Core.Models.DTO.CentroDeCusto
{
	public class CentroDeCustoDto
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Obs { get; set; }

		public static CentroDeCustoDto MapFromEntity(Entities.CentroDeCusto cdc) {
			return new CentroDeCustoDto {
				Id = cdc.Id,
				Nome = cdc.Nome,
				Obs = cdc.Obs,
			};
		}
	}
}
