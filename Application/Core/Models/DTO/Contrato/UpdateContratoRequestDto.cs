using contratosimples_api.Application.Core.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Contrato
{
	public class UpdateContratoRequestDto
	{
		public int CentroDeCustoId { get; set; }
		public int ClienteId { get; set; }
		public int FornecedorId { get; set; }
		public DateOnly DtInicio { get; set; }
		public DateOnly DtFim { get; set; }
		public string Obs { get; set; }
		public List<ContratoItemDto> ItensToUpdate { get; set; }
		public List<CreateContratoItemRequestDto> ItensToAdd { get; set; }
		public List<int> ItensToDelete { get; set; }

	}
}
