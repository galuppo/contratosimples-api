namespace contratosimples_api.Application.Models.DTO.ContratoItem
{
	public class ContratoItemDto	{
		public int Id { get; set; }
		public int ContratoId { get; set; }
		public int? GrupoId { get; set; }
		public string CodEscopo { get; set; }
		public string Descricao { get; set; }
		public double Quantidade { get; set; }
		public string UnMedida { get; set; }
		public double ValorUnit { get; set; }
		public double ValorTotal { get; set; }
		public bool IsGrupo { get; set; }

		public static ContratoItemDto MapFromEntity(Entities.ContratoItem entity)
		{
			return new ContratoItemDto
			{
				Id = entity.Id,
				ContratoId = entity.ContratoId,
				GrupoId = entity.Grupo == null ? null : entity.Grupo.Id,
				CodEscopo = entity.CodEscopo,
				Descricao = entity.Descricao,
				Quantidade = entity.Quantidade,
				UnMedida = entity.UnMedida,
				ValorUnit = entity.ValorUnit,
				ValorTotal = entity.ValorTotal,
				IsGrupo = entity.IsGrupo
			};
		}
	}
}
