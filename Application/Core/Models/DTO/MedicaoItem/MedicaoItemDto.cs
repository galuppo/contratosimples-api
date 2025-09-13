namespace contratosimples_api.Application.Core.Models.DTO.MedicaoItem
{
	public class MedicaoItemDto
	{
		public int Id { get; set; }
		public int IdMedicao { get; set; }
		public int IdContratoItem { get; set; }
		public decimal Quantidade { get; set; }
		public decimal Valor { get; set; }
	}
}
