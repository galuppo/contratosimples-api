using contratosimples_api.Application.Core.Models.DTO.MedicaoItem;

namespace contratosimples_api.Application.Core.Models.DTO.Medicao
{
	public class MedicaoDto
	{
		public MedicaoCabecalhoDto Cabecalho { get; set; }
		public List<MedicaoItemDto> Itens { get; set; }
	}
}
