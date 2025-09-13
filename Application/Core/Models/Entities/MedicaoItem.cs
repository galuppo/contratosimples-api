using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class MedicaoItem
	{
		[Key]
		public int Id { get; set; }
		public Medicao Medicao { get; set; }
		public ContratoItem ItemContrato { get; set; }
		public decimal Quantidade { get; set; }
		public decimal Valor { get; set; }
	}
}
