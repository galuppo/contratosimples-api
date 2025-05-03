using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class Contrato
	{
		[Key]
		public int Id { get; set; }
		public CentroDeCusto CentroDeCusto { get; set; }
		public Cliente Cliente { get; set; }
		public Fornecedor Fornecedor { get; set; }
		public DateOnly DtInicio { get; set; }
		public DateOnly DtFim { get; set; }
		public string Obs { get; set; }
		public List<ContratoItem> Itens { get; set; }
	}
}
