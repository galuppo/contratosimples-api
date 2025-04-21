using contratosimples_api.Application.Models.DTO.Fornecedor;
using contratosimples_api.Application.Repositories;
using contratosimples_api.Common.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace contratosimples_api.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class FornecedorController : ControllerBase
	{

		private readonly ContratoSimplesUnitOfWork uow;

		public FornecedorController(ContratoSimplesUnitOfWork uow)
		{
			this.uow = uow;
		}

		[HttpPost]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		public async Task<IActionResult> CreateFornecedor([FromBody] CreateFornecedorRequestDto request)
		{
			var fornecedor = request.MapToEntity();

			await uow.FornecedorRepository.InsertAsync(fornecedor);
			await uow.SaveAsync();

			var response = FornecedorDto.MapFromEntity(fornecedor);

			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		public async Task<IActionResult> GetAllFornecedor()
		{
			var fornecedores = await uow.FornecedorRepository.GetAsync();

			var response = new List<FornecedorDto>();
			foreach (var f in fornecedores)
			{
				response.Add(FornecedorDto.MapFromEntity(f));
			}
			return Ok(response);
		}

		[HttpGet]
		[Authorize(Roles = UsuarioRole.READER_ROLE)]
		[Route("{idFornecedor:int}")]
		public async Task<IActionResult> GetFornecedorById([FromRoute] int idFornecedor)
		{
			var f = await uow.FornecedorRepository.GetByIdAsync(idFornecedor);
			if (f == null)
				return NotFound();
			var response = FornecedorDto.MapFromEntity(f);

			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idFornecedor:int}")]
		public async Task<IActionResult> UpdateFornecedorById([FromRoute] int idFornecedor, [FromBody] UpdateFornecedorRequestDto request)
		{
			var forn = request.MapToEntity();
			forn.Id = idFornecedor;

			forn = await uow.FornecedorRepository.UpdateAsync(idFornecedor, forn);
			if (forn == null)
				return NotFound();
			foreach (var c in request.ContatosToAdd)
			{
				var contato = c.MapToEntity();
				contato.FornecedorId = idFornecedor;
				await uow.ContatoFornecedorRepository.InsertAsync(contato);
			}

			foreach (var c in request.ContatosToUpdate)
			{
				var contato = await uow.ContatoFornecedorRepository.GetByIdAsync(c.Id);
				if (contato == null)
					continue;

				contato.Nome = c.Nome;
				contato.Telefone = c.Telefone;
				contato.Email = c.Email;
				contato.Cargo = c.Cargo;

				await uow.ContatoFornecedorRepository.UpdateAsync(contato.Id, contato);
			}

			foreach (var id in request.ContatosToDelete)
				await uow.ContatoFornecedorRepository.DeleteAsync(id);


			await uow.SaveAsync();
			var response = FornecedorDto.MapFromEntity(forn);
			return Ok(response);
		}

		[HttpDelete]
		[Authorize(Roles = UsuarioRole.WRITER_ROLE)]
		[Route("{idFornecedor:int}")]
		public async Task<IActionResult> DeleteFornecedorById([FromRoute] int idFornecedor)
		{
			var forn = await uow.FornecedorRepository.GetAsync(f => f.Id == idFornecedor, null, "Contatos");
			if (forn == null)
				return NotFound();
			await uow.FornecedorRepository.DeleteAsync(idFornecedor);
			await uow.SaveAsync();

			return Ok(forn);
		}
	}
}
