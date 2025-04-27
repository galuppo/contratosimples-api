using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace contratosimples_api.Application.Common.Repositories
{
	public class GenericRepository<TEntity> where TEntity : class
	{
		protected DbContext dbContext;
		protected DbSet<TEntity> dbSet;

		public GenericRepository(DbContext dbContext) {
			this.dbContext = dbContext;
			dbSet = this.dbContext.Set<TEntity>();
		}

		public virtual async Task<IEnumerable<TEntity>> GetAsync(
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "") {
			IQueryable<TEntity> query = dbSet;

			if (filter != null)
				query = query.Where(filter);

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				query = query.Include(includeProperty);

			if (orderBy != null)
				return await orderBy(query).ToListAsync();
			else
				return await query.ToListAsync();
		}

		public virtual async Task<TEntity?> GetByIdAsync(object id) {
			return await dbSet.FindAsync(id);
		}

		public virtual async Task<TEntity> InsertAsync(TEntity entity) {
			await dbSet.AddAsync(entity);
			return entity;
		}
		public virtual async Task<TEntity?> DeleteAsync(object id) {
			TEntity? entity = await dbSet.FindAsync(id);
			if (entity == null)
				return null;
			return Delete(entity);

		}
		protected virtual TEntity Delete(TEntity entity) {
			if (dbContext.Entry(entity).State == EntityState.Detached)
				dbSet.Attach(entity);
			dbSet.Remove(entity);
			return entity;
		}
		public virtual async Task<TEntity?> UpdateAsync(object id, TEntity entity) {
			var ent = await dbSet.FindAsync(id);
			if (ent == null) return null;
			if (dbContext.Entry(ent).State == EntityState.Detached)
				dbSet.Attach(ent);
			dbContext.Entry(ent).CurrentValues.SetValues(entity);
			dbContext.Entry(ent).State = EntityState.Modified;
			return entity;
		}
	}
}
