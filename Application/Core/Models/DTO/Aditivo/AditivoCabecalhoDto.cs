using contratosimples_api.Application.Core.Models.DTO.Contrato;
using contratosimples_api.Application.Core.Models.Entities;

namespace contratosimples_api.Application.Core.Models.DTO.Aditivo
{
	public class AditivoCabecalhoDto
	{
		public int Id { get; set; }
		public ContratoCabecalhoDto Contrato { get; set; }
		public int Prazo { get; set; }
		public decimal ValorTotal { get; set; }

		public static AditivoCabecalhoDto MapFromEntity(Entities.Aditivo entity) {
			return new AditivoCabecalhoDto {
				Id = entity.Id,
				Contrato = ContratoCabecalhoDto.MapFromEntity(entity.Contrato),
				Prazo = entity.Prazo,
				ValorTotal = entity.ValorTotal
			};
		}
	}
}
