using contratosimples_api.Common.Repositories;
using contratosimples_api.Management.Data;
using contratosimples_api.Management.Models.Entities;

namespace contratosimples_api.Management.Repositories
{
	public class AppManagementUnitOfWork : GenericUnitOfWork
	{

		private GenericRepository<AppCliente>? _appClienteRepository;

		public GenericRepository<AppCliente> AppClienteRepository { 
			get{
				if (_appClienteRepository == null)
					this._appClienteRepository = new GenericRepository<AppCliente>(this.dbContext);
				return _appClienteRepository;
			}
		}

		public AppManagementUnitOfWork(IConfiguration config, AppManagementDbContext appDbContext) : base(config) {
			this.dbContext = appDbContext;
		}
	}
}
