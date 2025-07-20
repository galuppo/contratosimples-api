using contratosimples_api.Application.Core.Models.DTO.AditivoItem;
using contratosimples_api.Application.Core.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Aditivo
{
	public class UpdateAditivoRequestDto
	{
		public int IdAditivo { get; set; }
		public int Prazo { get; set; }
		public decimal ValorTotal { get; set; }
		public List<CreateContratoItemRequestDto> ItensContratoToAdd { get; set; }
		public List<ContratoItemDto> ItensContratoToUpdate { get; set; }
		public List<int> ItensContratoToDelete { get; set; }
		public List<CreateAditivoItemRequestDto> ItensAditivoToAdd { get; set; }
		public List<AditivoItemDto> ItensAditivoToUpdate { get; set; }
		public List<int> ItensAditivoToDelete { get; set; }
	}
}
