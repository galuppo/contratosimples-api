using contratosimples_api.Application.Core.Models.DTO.CentroDeCusto;
using contratosimples_api.Application.Core.Models.DTO.Cliente;
using contratosimples_api.Application.Core.Models.DTO.Fornecedor;

namespace contratosimples_api.Application.Core.Models.DTO.Contrato
{
	public class ContratoDto
	{
		public int Id { get; set; }
		public CentroDeCustoDto CentroDeCusto { get; set; }
		public ClienteDto Cliente { get; set; }
		public FornecedorDto Fornecedor { get; set; }
		public DateOnly DtInicio { get; set; }
		public DateOnly DtFim { get; set; }
		public string Obs { get; set; }

		public static ContratoDto MapFromEntity(Entities.Contrato entity) {
			return new ContratoDto {
				Id = entity.Id,
				CentroDeCusto = CentroDeCustoDto.MapFromEntity(entity.CentroDeCusto),
				Cliente = ClienteDto.MapFromEntity(entity.Cliente),
				Fornecedor = FornecedorDto.MapFromEntity(entity.Fornecedor),
				DtInicio = entity.DtInicio,
				DtFim = entity.DtFim,
				Obs = entity.Obs
			};
		}
	}
}
