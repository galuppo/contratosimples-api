using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Models.Entities
{
	public class CentroDeCusto
	{
		[Key]
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Obs { get; set; }

	}
}
