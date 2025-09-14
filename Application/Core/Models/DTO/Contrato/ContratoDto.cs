using contratosimples_api.Application.Core.Models.DTO.Aditivo;
using contratosimples_api.Application.Core.Models.DTO.ContratoItem;
using contratosimples_api.Application.Core.Models.DTO.Medicao;

namespace contratosimples_api.Application.Core.Models.DTO.Contrato
{
	public class ContratoDto
	{
		public ContratoCabecalhoDto Cabecalho { get; set; }
		public List<ContratoItemDto> Itens { get; set; }
		public List<AditivoDto> Aditivos { get; set; }
		public List<MedicaoDto> Medicoes { get; set; }
	}
}
