using contratosimples_api.Application.Common.Repositories;
using contratosimples_api.Application.Management.Data;
using contratosimples_api.Application.Management.Models.Entities;

namespace contratosimples_api.Application.Management.Repositories
{
	public class AppManagementUnitOfWork : GenericUnitOfWork
	{

		private GenericRepository<Tenant>? _tenantRepository;
		private GenericRepository<Usuario>? _usuarioRepository;

		public GenericRepository<Tenant> TenantRepository {
			get {
				if (_tenantRepository == null)
					_tenantRepository = new GenericRepository<Tenant>(dbContext);
				return _tenantRepository;
			}
		}

		public GenericRepository<Usuario> UsuarioRepository { 
			get { 
				if(_usuarioRepository == null)
					_usuarioRepository = new GenericRepository<Usuario>(dbContext);
				return _usuarioRepository;
			} 
		}

		public AppManagementUnitOfWork(IConfiguration config, AppManagementDbContext appDbContext) : base(config) {
			dbContext = appDbContext;
		}
	}
}
