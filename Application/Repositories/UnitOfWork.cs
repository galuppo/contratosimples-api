using contratosimples_api.Application.Data;
using contratosimples_api.Application.Models.Entities;

namespace contratosimples_api.Application.Repositories
{
	public class UnitOfWork : IDisposable
	{
		private readonly ApplicationDbContext dbContext;
		private GenericRepository<CentroDeCusto> centroDeCustoRepository;
		private GenericRepository<Cliente> clienteRepository;
		private GenericRepository<ContatoFornecedor> contatoFornecedorRepository;
		private GenericRepository<Fornecedor> fornecedorRepository;
		private GenericRepository<Contrato> contratoRepository;
		private GenericRepository<ContratoItem> contratoItemRepository;
		private bool disposed = false;

		public UnitOfWork(ApplicationDbContext dbContext):base()
		{
			//TODO: implementar aqui a criação de dbContext
			this.dbContext = dbContext;
		}

		public GenericRepository<Fornecedor> FornecedorRepository {
			get
			{
				if(this.fornecedorRepository == null)
					this.fornecedorRepository = new GenericRepository<Fornecedor> (this.dbContext);
				return this.fornecedorRepository;
			}
		}
		public GenericRepository<ContatoFornecedor> ContatoFornecedorRepository { 
			get {
				if (this.contatoFornecedorRepository == null)
					this.contatoFornecedorRepository = new GenericRepository<ContatoFornecedor>(this.dbContext);
				return this.contatoFornecedorRepository;
			}
		}
		public GenericRepository<Cliente> ClienteRepository { 
			get {
				if (this.clienteRepository == null)
					this.clienteRepository = new GenericRepository<Cliente>(this.dbContext);
				return this.clienteRepository;
			}
		}
		public GenericRepository<CentroDeCusto> CentroDeCustoRepository { 
			get {
				if (this.centroDeCustoRepository == null)
					this.centroDeCustoRepository = new GenericRepository<CentroDeCusto>(this.dbContext);
				return this.centroDeCustoRepository;
			}
		}
		public GenericRepository<Contrato> ContratoRepository
		{
			get
			{
				if (this.contratoRepository == null)
					this.contratoRepository = new GenericRepository<Contrato>(this.dbContext);
				return this.contratoRepository;
			}
		}
		public GenericRepository<ContratoItem> ContratoItemRepository
		{
			get
			{
				if (this.contratoItemRepository == null)
					this.contratoItemRepository = new GenericRepository<ContratoItem>(this.dbContext);
				return this.contratoItemRepository;
			}
		}

		public async Task<int> SaveAsync()
		{
			return await this.dbContext.SaveChangesAsync();
		}

		protected virtual void Dispose(bool disposing)
		{
			if(!this.disposed && disposing)
			{
				//	this.dbContext.Dispose();
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
