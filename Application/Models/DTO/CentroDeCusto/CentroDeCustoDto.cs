namespace contratosimples_api.Application.Models.DTO.CentroDeCusto
{
	public class CentroDeCustoDto
	{
		public int Cod { get; set; }
		public string Nome { get; set; }
		public string Obs { get; set; }

		public static CentroDeCustoDto MapFromEntity(Entities.CentroDeCusto cdc)
		{
			return new CentroDeCustoDto
			{
				Cod = cdc.Cod,
				Nome = cdc.Nome,
				Obs = cdc.Obs,
			};
		}
	}
}
