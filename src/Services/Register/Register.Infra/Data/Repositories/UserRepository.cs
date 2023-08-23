using Register.Application.Interfaces;
using Register.Application.Domain.Entities;
using Register.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Register.Infra.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(RegistersDbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAllUsersAddresses()
        {
            return await DbSet.Include(req => req.Address).ToListAsync();
        }
        public async Task<IEnumerable<User>> GetByName(string name)
        {
            return await DbSet.Include(req => req.Address).Where(p => p.Name.Contains(name)).ToListAsync();
        }

        public async Task<User?> GetOneUsersAddresses(Guid id)
        {
            return await DbSet.Include(req => req.Address).Where(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAddress(Address address)
        {
            Db.Remove(address);
            return await SaveChanges() > 0;
        }
    }
}
