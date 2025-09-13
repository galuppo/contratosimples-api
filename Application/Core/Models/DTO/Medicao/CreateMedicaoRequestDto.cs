using contratosimples_api.Application.Core.Models.DTO.MedicaoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Medicao
{
	public class CreateMedicaoRequestDto
	{
		public int IdContrato { get; set; }
		public DateOnly Data { get; set; }
		public List<CreateMedicaoItemRequestDto> Itens { get; set; }
	}
}
