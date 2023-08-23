using Register.Application.Interfaces;
using Register.Application.NotificationPattern;
using Register.Application.Domain.Entities;
using Register.Application.Domain.Entities.Validations;

namespace Register.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, INotifier notifier) : base(notifier)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersWithAddress()
        {
            return await _userRepository.GetAllUsersAddresses();
        }

        public async Task<User?> GetOneUserById(Guid id)
        {
            return await _userRepository.GetOneUsersAddresses(id);
        }

        public async Task<IEnumerable<User>> GetUserByName(string nameQuery)
        {
            return await _userRepository.GetByName(nameQuery);
        }

        public async Task AddUser(User user)
        {
            if (!ValidateEntity(new UserValidator(), user)) return;

            if (_userRepository.Get(p => p.Document == user.Document).Result.Any())
            {
                Notify("Já existe uma user com este documento infomado.");
                return;
            }

            await _userRepository.Insert(user);
        }

        public async Task UpdateUser(User pessoa)
        {

            if (!ValidateEntity(new UserValidator(), pessoa)) return;

            if (_userRepository.Get(f => f.Document == pessoa.Document && f.Id != pessoa.Id).Result.Any())
            {
                Notify("Já existe um pessoa com este documento infomado");
                return;
            }

            await _userRepository.Update(pessoa);
        }

        public async Task DeleteUser(Guid id)
        {
            await _userRepository.Delete(id);
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
        }
    }
}
