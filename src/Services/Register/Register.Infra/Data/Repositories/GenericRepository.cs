using Register.Application.Interfaces;
using Register.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Core.Common;

namespace Register.Infra.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly RegistersDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected GenericRepository(RegistersDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity?> GetByID(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Insert(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task<bool> Delete(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            return await SaveChanges() > 0;
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
