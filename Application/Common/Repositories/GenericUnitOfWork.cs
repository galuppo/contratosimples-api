using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace contratosimples_api.Application.Common.Repositories
{
	public class GenericUnitOfWork : IDisposable
	{
		private bool disposedValue = false;
		protected readonly IConfiguration config;
		protected DbContext? dbContext;
		protected IDbContextTransaction? transaction;

		public GenericUnitOfWork(IConfiguration config) : base() {
			this.config = config;
		}

		protected virtual void Dispose(bool disposing) {
			if (!disposedValue) {
				if (disposing) {
					if (dbContext != null)
						dbContext.Dispose();
				}
				disposedValue = true;
			}
		}

		public async Task<int> SaveAsync() {
			if (dbContext != null)
				return await dbContext.SaveChangesAsync();
			return -1;
		}

		public void Dispose() {
			this.transaction?.Dispose();
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		public async Task<int> BeginTransactionAsync() {
			if (dbContext != null) {
				if (this.transaction == null) {
					this.transaction = await this.dbContext.Database.BeginTransactionAsync();
					return 1;
				}
			}
			return -1;
		}

		public async Task EndTransactionAsync() {
			if ((dbContext != null) && (this.transaction != null)) {
				await this.dbContext.SaveChangesAsync();
				await this.transaction.CommitAsync();
			}
		}

		public async Task RollBackTransactionAsync() {
			if ((dbContext != null) && (this.transaction != null)) {
				await this.transaction.RollbackAsync();
			}
		}
	}
}
