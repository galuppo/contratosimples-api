using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class Aditivo
	{
		[Key]
		public int Id { get; set; }
		public Contrato Contrato { get; set; }
		public int Prazo { get; set; }
		public decimal ValorTotal { get; set; }
		public List<AditivoItem> ItensAditivo { get; set; }
	}
}
