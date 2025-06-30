using contratosimples_api.Application.Core.Models.DTO.AditivoItem;
using contratosimples_api.Application.Core.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Aditivo
{
	public class CreateAditivoRequestDto
	{
		public int IdContrato { get; set; }
		public int Prazo { get; set; }
		public decimal ValorTotal { get; set; }
		public List<CreateContratoItemRequestDto> ItensContratoToAdd { get; set; }
		public List<ContratoItemDto> ItensContratoToUpdate { get; set; }
		public List<CreateAditivoItemRequestDto> ItensAditivoToAdd { get; set; }

		public Entities.Aditivo MapToEntity() {
			var aditivo = new Entities.Aditivo {
				Prazo = Prazo,
				ValorTotal = ValorTotal,
			};
			return aditivo;
		}
	}
}
