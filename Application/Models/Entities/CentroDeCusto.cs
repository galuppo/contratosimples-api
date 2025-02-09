using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Application.Models.Entities
{
	public class CentroDeCusto
	{
		[Key]
		public int Cod { get; set; }
		public string Nome { get; set; }
		public string Obs { get; set; }

	}
}
