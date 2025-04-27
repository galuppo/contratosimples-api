using Microsoft.EntityFrameworkCore;

namespace contratosimples_api.Application.Common.Repositories
{
	public class GenericUnitOfWork : IDisposable
	{
		private bool disposedValue = false;
		protected readonly IConfiguration config;
		protected DbContext? dbContext;

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
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}
