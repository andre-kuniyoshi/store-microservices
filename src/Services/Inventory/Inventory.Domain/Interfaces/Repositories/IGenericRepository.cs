using Core.Common;

namespace Inventory.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseControlEntity
    {
        Task<bool> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity newEntity, Guid id);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteLogicAsync(Guid id);
        Task<bool> Exist(Guid id);
        Task<TEntity?> GetById(Guid id);
        Task<IEnumerable<TEntity>> GetAll();
    }
}
