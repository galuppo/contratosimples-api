namespace contratosimples_api.Application.Core.Models.DTO.CentroDeCusto
{
	public class CreateCentroDeCustoRequestDto
	{
		public string Nome { get; set; }
		public string Obs { get; set; }

		public Entities.CentroDeCusto MapToEntity() {
			return new Entities.CentroDeCusto {
				Nome = Nome,
				Obs = Obs
			};
		}
	}
}
