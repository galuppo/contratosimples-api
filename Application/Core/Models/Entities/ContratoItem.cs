using System.Diagnostics.CodeAnalysis;

namespace contratosimples_api.Application.Core.Models.Entities
{
	public class ContratoItem
	{
		public int Id { get; set; }
		public int ContratoId { get; set; }
		public ContratoItem? Grupo { get; set; }
		public string CodEscopo { get; set; }
		public string Descricao { get; set; }
		public double Quantidade { get; set; }
		public string UnMedida { get; set; }
		public double ValorUnit { get; set; }
		public double ValorTotal { get; set; }
		public bool IsGrupo { get; set; }
	}
}
