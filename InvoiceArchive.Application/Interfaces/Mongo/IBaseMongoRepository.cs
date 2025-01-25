using InvoiceArchive.Domain.Entities.Mongo;
using System.Linq.Expressions;

namespace InvoiceArchive.Application.Interfaces.Mongo
{
    public interface IBaseMongoRepository<TEntity> where TEntity : BaseEntityMongo
    {
        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task InsertAsync(TEntity entity);
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
