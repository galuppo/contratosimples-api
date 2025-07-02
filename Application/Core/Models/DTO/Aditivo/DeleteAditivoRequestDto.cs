using contratosimples_api.Application.Core.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Aditivo
{
	public class DeleteAditivoRequestDto
	{
		public int IdAditivo { get; set; }
		public List<ContratoItemDto> ItensToUpdate { get; set; }
	}
}
