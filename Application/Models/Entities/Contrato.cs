using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace contratosimples_api.Application.Models.Entities
{
	public class Contrato
	{
        [Key]
        public int Id { get; set; }
		public CentroDeCusto CentroDeCusto { get; set; }
        public Cliente Cliente { get; set; }
		public Fornecedor Fornecedor { get; set; }
		public DateTime DtInicio { get; set; }
		public DateTime DtFim { get; set; }
		public string Obs { get; set; }
		public List<ContratoItem> Itens { get; set; }
	}
}
