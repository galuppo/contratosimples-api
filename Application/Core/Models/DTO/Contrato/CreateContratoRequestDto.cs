using contratosimples_api.Application.Core.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Contrato
{
	public class CreateContratoRequestDto
	{
		public int CentroDeCustoId { get; set; }
		public int ClienteId { get; set; }
		public int FornecedorId { get; set; }
		public DateOnly DtInicio { get; set; }
		public DateOnly DtFim { get; set; }
		public string Obs { get; set; }
		public List<CreateContratoItemRequestDto> Itens { get; set; }

		public Entities.Contrato MapToEntity() {
			var contrato = new Entities.Contrato {
				DtInicio = DtInicio,
				DtFim = DtFim,
				Obs = Obs,
				Itens = new List<Entities.ContratoItem>()
			};
			return contrato;
		}
	}
}
