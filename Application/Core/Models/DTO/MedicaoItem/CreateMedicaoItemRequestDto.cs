namespace contratosimples_api.Application.Core.Models.DTO.MedicaoItem
{
	public class CreateMedicaoItemRequestDto
	{
		public int IdContratoItem { get; set; }
		public decimal Quantidade { get; set; }
		public decimal Valor { get; set; }

	}
}
