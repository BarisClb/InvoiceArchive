using InvoiceArchive.Domain.Entities.Sql;
using System.Linq.Expressions;

namespace InvoiceArchive.Application.Interfaces.Sql
{
    public interface IBaseSqlRepository<TEntity> where TEntity : BaseEntitySql
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<bool> DeleteAsync(Guid entityId);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TEntity> GetByIdAsync(Guid entityId, IEnumerable<string>? includes = default, CancellationToken cancellationToken = default);
        Task<TEntity> GetFirstWhereAsync(Expression<Func<TEntity, bool>> predicate, IEnumerable<string>? includes = default, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAsync(int? pageNumber = default, int? pageSize = default, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = default, Expression<Func<TEntity, bool>>? predicate = default, IEnumerable<string>? includes = default, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);
    }
}
