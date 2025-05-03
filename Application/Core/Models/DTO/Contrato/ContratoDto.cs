namespace contratosimples_api.Application.Core.Models.DTO.Contrato
{
	public class ContratoDto
	{
		public int Id { get; set; }
		public int CentroDeCustoId { get; set; }
		public int ClienteId { get; set; }
		public int FornecedorId { get; set; }
		public DateOnly DtInicio { get; set; }
		public DateOnly DtFim { get; set; }
		public string Obs { get; set; }

		public static ContratoDto MapFromEntity(Entities.Contrato entity) {
			return new ContratoDto {
				Id = entity.Id,
				CentroDeCustoId = entity.CentroDeCusto.Id,
				ClienteId = entity.Cliente.Id,
				FornecedorId = entity.Fornecedor.Id,
				DtInicio = entity.DtInicio,
				DtFim = entity.DtFim,
				Obs = entity.Obs
			};
		}
	}
}
