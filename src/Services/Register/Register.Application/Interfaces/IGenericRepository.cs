using Register.Application.Domain.Entities;
using System.Linq.Expressions;
using Core.Common;

namespace Register.Application.Interfaces
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Insert(TEntity entity);
        Task<TEntity?> GetByID(Guid id);
        Task<List<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
