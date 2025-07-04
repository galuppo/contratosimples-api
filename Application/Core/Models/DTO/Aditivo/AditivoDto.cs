using contratosimples_api.Application.Core.Models.DTO.AditivoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Aditivo
{
	public class AditivoDto
	{
		public AditivoCabecalhoDto Cabecalho { get; set; }
		public List<AditivoItemDto> Itens { get; set; }
	}
}
