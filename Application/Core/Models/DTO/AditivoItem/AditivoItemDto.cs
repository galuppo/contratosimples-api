using contratosimples_api.Application.Core.Models.DTO.ContratoItem;

namespace contratosimples_api.Application.Core.Models.DTO.AditivoItem
{
	public class AditivoItemDto
	{
		public int Id { get; set; }
		public int IdAditivo { get; set; }
		public ContratoItemDto ContratoItem { get; set; }
		public int TipoAditivo { get; set; }
		public decimal Quantidade { get; set; }
		public decimal ValorTotal { get; set; }

		public static AditivoItemDto MapFromEntity(Entities.AditivoItem entity) {
			return new AditivoItemDto {
				Id = entity.Id,
				IdAditivo = entity.Aditivo.Id,
				ContratoItem = ContratoItemDto.MapFromEntity(entity.ContratoItem),
				TipoAditivo = entity.TipoAditivo,
				Quantidade = entity.Quantidade,
				ValorTotal = entity.ValorTotal
			};
		}
	}
}
