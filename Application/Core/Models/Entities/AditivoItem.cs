using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class AditivoItem
	{
		[Key]
		public int Id { get; set; }
		public Aditivo Aditivo { get; set; }
		public ContratoItem ContratoItem { get; set; }
		public int TipoAditivo { get; set; }
		public decimal Quantidade { get; set; }
		public decimal ValorTotal { get; set; }

	}
}
