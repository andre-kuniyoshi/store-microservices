using Register.Application.Domain.Entities;

namespace Register.Application.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<List<User>> GetAllUsersAddresses();
        Task<IEnumerable<User>> GetByName(string name);
        Task<User?> GetOneUsersAddresses(Guid id);
        Task<bool> DeleteAddress(Address address);
    }
}
