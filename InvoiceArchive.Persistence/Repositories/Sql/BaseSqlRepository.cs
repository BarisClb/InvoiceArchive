using InvoiceArchive.Application.Interfaces.Sql;
using InvoiceArchive.Domain.Entities.Sql;
using InvoiceArchive.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InvoiceArchive.Persistence.Repositories.Sql
{
    public class BaseSqlRepository<TEntity> : IBaseSqlRepository<TEntity> where TEntity : BaseEntitySql
    {
        internal readonly InvoiceArchiveDbContext _context;
        internal readonly DbSet<TEntity> _entity;

        public BaseSqlRepository(InvoiceArchiveDbContext context)
        {
            _context = context;
            _entity = _context.Set<TEntity>();
        }


        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var entityState = await _entity.AddAsync(entity);
            if (entityState.State != EntityState.Added)
                return null;
            var added = await _context.SaveChangesAsync();
            return added > 0 ? entityState.Entity : null;
        }

        public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _entity.AddRangeAsync(entities);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(Guid entityId)
        {
            var entity = await _entity.FindAsync(entityId);
            if (entity != null)
            {
                var entityState = _entity.Remove(entity);
                if (entityState.State != EntityState.Deleted)
                    return false;
                var deleted = await _context.SaveChangesAsync();
                return deleted > 0;
            }
            return false;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entity.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetByIdAsync(Guid entityId, IEnumerable<string>? includes = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entity.Where(entity => entity.Id == entityId);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entity.Where(predicate);

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> GetAsync(int? pageNumber = default, int? pageSize = default, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default, Expression<Func<TEntity, bool>>? predicate = default, IEnumerable<string>? includes = default, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _entity;

            if (predicate != null)
                query = query.Where(predicate);
            if (orderBy != null)
                query = orderBy(query);
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            if (pageSize != null)
                query = query.Skip(((pageNumber ?? 1) - 1) * (pageSize ?? 1)).Take(pageSize ?? 1);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
        {
            if (predicate == null)
                return await _entity.CountAsync(cancellationToken);
            else
                return await _entity.CountAsync(predicate, cancellationToken);
        }
    }
}
