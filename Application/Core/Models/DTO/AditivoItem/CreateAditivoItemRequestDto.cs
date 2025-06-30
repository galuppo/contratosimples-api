namespace contratosimples_api.Application.Core.Models.DTO.AditivoItem
{
	public class CreateAditivoItemRequestDto
	{
		public int IdItemContrato { get; set; }
		public int TipoAditivo { get; set; }
		public decimal Quantidade { get; set; }
		public decimal ValorTotal { get; set; }

		public Entities.AditivoItem MapToEntity() {
			var item = new Entities.AditivoItem {
				TipoAditivo = TipoAditivo,
				Quantidade = Quantidade,	
				ValorTotal = ValorTotal
			};
			return item;
		}
	}
}
