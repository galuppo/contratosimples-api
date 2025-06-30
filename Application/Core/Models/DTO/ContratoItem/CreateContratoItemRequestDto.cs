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
		public static Dictionary<int, Entities.ContratoItem>[] MapRequestItens(List<CreateContratoItemRequestDto> request) {
			var hashGrupos = new Dictionary<int, Entities.ContratoItem>();
			var hashItens = new Dictionary<int, Entities.ContratoItem>();

			foreach (var item in request) {
				var contratoItem = item.MapToEntity();
				hashItens.Add(item.TemporaryId, contratoItem);

				if (item.IsGrupo)
					hashGrupos.Add(item.TemporaryId, contratoItem);
			}

			Dictionary<int, Entities.ContratoItem>[] response = { hashGrupos, hashItens };
			return response;

		}
	}
}
