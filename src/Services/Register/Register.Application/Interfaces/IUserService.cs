using Register.Application.Domain.Entities;

namespace Register.Application.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<IEnumerable<User>> GetAllUsersWithAddress();
        Task<IEnumerable<User>> GetUserByName(string nameQuery);
        Task<User?> GetOneUserById(Guid id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(Guid id);
    }
}
