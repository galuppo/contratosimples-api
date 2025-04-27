namespace contratosimples_api.Application.Core.Models.DTO.ContratoItem
{
	public class CreateContratoItemRequestDto
	{
		public int TemporaryId { get; set; }
		public int? GrupoId { get; set; }
		public string CodEscopo { get; set; }
		public string Descricao { get; set; }
		public double Quantidade { get; set; }
		public string UnMedida { get; set; }
		public double ValorUnit { get; set; }
		public double ValorTotal { get; set; }
		public bool IsGrupo { get; set; }

		public Entities.ContratoItem MapToEntity() {
			return new Entities.ContratoItem {
				CodEscopo = CodEscopo,
				Descricao = Descricao,
				Quantidade = Quantidade,
				UnMedida = UnMedida,
				ValorUnit = ValorUnit,
				ValorTotal = ValorTotal,
				IsGrupo = IsGrupo
			};
		}
	}
}
