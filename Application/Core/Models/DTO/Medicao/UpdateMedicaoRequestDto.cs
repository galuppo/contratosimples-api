using contratosimples_api.Application.Core.Models.DTO.MedicaoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Medicao
{
	public class UpdateMedicaoRequestDto
	{
		public int IdMedicao { get; set; }
		public DateOnly Data { get; set; }
		public List<CreateMedicaoItemRequestDto> ItensToAdd { get; set; }
		public List<UpdateMedicaoItemRequestDto> ItensToUpdate { get; set; }
		public List<int> ItensToDelete { get; set; }
	}
}
