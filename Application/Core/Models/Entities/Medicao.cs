using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class Medicao
	{
		[Key]
		public int Id { get; set; }
		public Contrato Contrato { get; set; }
		public DateOnly Data { get; set; }
		public decimal ValorTotal { get; set; }
	}
}
