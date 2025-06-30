using contratosimples_api.Application.Common.Repositories;
using contratosimples_api.Application.Core.Data;
using contratosimples_api.Application.Core.Models.Entities;
using contratosimples_api.Application.Management.Services;
using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Core.Repositories
{
	public class ContratoSimplesUnitOfWork : GenericUnitOfWork
	{
		private GenericRepository<CentroDeCusto>? centroDeCustoRepository;
		private GenericRepository<Cliente>? clienteRepository;
		private GenericRepository<ContatoFornecedor>? contatoFornecedorRepository;
		private GenericRepository<Fornecedor>? fornecedorRepository;
		private GenericRepository<Contrato>? contratoRepository;
		private GenericRepository<ContratoItem>? contratoItemRepository;
		private GenericRepository<Aditivo>? aditivoRepository;
		private GenericRepository<AditivoItem>? aditivoItemRepository;

		public ContratoSimplesUnitOfWork(IConfiguration config, TenantService tenantService) : base(config) {
			var connectionString = config.GetConnectionString("ContratoSimplesConnectionString");

			if (connectionString == null)
				throw new Exception("Connection string not found!");

			connectionString = connectionString.Replace("%dbcliente%", tenantService.TransactionTenant.DataBaseName);

			var builder = new DbContextOptionsBuilder<ContratoSimplesDbContext>();
			builder.UseNpgsql(connectionString);

			dbContext = new ContratoSimplesDbContext(builder.Options);
			dbContext.Database.Migrate();
		}

		public GenericRepository<Fornecedor> FornecedorRepository {
			get {
				if (fornecedorRepository == null)
					fornecedorRepository = new GenericRepository<Fornecedor>(dbContext);
				return fornecedorRepository;
			}
		}
		public GenericRepository<ContatoFornecedor> ContatoFornecedorRepository {
			get {
				if (contatoFornecedorRepository == null)
					contatoFornecedorRepository = new GenericRepository<ContatoFornecedor>(dbContext);
				return contatoFornecedorRepository;
			}
		}
		public GenericRepository<Cliente> ClienteRepository {
			get {
				if (clienteRepository == null)
					clienteRepository = new GenericRepository<Cliente>(dbContext);
				return clienteRepository;
			}
		}
		public GenericRepository<CentroDeCusto> CentroDeCustoRepository {
			get {
				if (centroDeCustoRepository == null)
					centroDeCustoRepository = new GenericRepository<CentroDeCusto>(dbContext);
				return centroDeCustoRepository;
			}
		}
		public GenericRepository<Contrato> ContratoRepository {
			get {
				if (contratoRepository == null)
					contratoRepository = new GenericRepository<Contrato>(dbContext);
				return contratoRepository;
			}
		}
		public GenericRepository<ContratoItem> ContratoItemRepository {
			get {
				if (contratoItemRepository == null)
					contratoItemRepository = new GenericRepository<ContratoItem>(dbContext);
				return contratoItemRepository;
			}
		}
		public GenericRepository<Aditivo> AditivoRepository {
			get {
				if (aditivoRepository == null)
					aditivoRepository = new GenericRepository<Aditivo>(dbContext);
				return aditivoRepository;
			}
		}
		public GenericRepository<AditivoItem> AditivoItemRepository {
			get {
				if (aditivoItemRepository == null)
					aditivoItemRepository = new GenericRepository<AditivoItem>(dbContext);
				return aditivoItemRepository;
			}
		}
	}
}
