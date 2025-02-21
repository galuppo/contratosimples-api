using contratosimples_api.Application.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Models.DTO.Contrato
{
	public class CreateContratoRequestDto
	{
		public int CentroDeCustoId { get; set; }
		public int ClienteId { get; set; }
		public int FornecedorId { get; set; }
		public DateTime DtInicio { get; set; }
		public DateTime DtFim { get; set; }
		public string Obs { get; set; }
		public List<CreateContratoItemRequestDto> Itens { get; set; }

		public Entities.Contrato MapToEntity()
		{
			var contrato = new Entities.Contrato
			{
				DtInicio = DtInicio,
				DtFim = DtFim,
				Obs = Obs,
				Itens = new List<Entities.ContratoItem>()
			};
			return contrato;
		}
	}
}
