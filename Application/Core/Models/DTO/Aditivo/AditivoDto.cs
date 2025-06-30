using contratosimples_api.Application.Core.Models.DTO.Contrato;
using contratosimples_api.Application.Core.Models.Entities;

namespace contratosimples_api.Application.Core.Models.DTO.Aditivo
{
	public class AditivoDto
	{
		public int Id { get; set; }
		public ContratoDto Contrato { get; set; }
		public int Prazo { get; set; }
		public decimal ValorTotal { get; set; }

		public static AditivoDto MapFromEntity(Entities.Aditivo entity) {
			return new AditivoDto {
				Id = entity.Id,
				Contrato = ContratoDto.MapFromEntity(entity.Contrato),
				Prazo = entity.Prazo,
				ValorTotal = entity.ValorTotal
			};
		}
	}
}
