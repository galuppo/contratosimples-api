namespace contratosimples_api.Application.Core.Models.DTO.Medicao
{
	public class MedicaoCabecalhoDto
	{
		public int Id { get; set; }
		public int IdContrato { get; set; }
		public DateOnly Data { get; set; }
		public decimal ValorTotal { get; set; }
	}
}
