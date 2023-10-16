using Register.Application.Interfaces;
using Register.Application.Domain.Entities;
using Register.Application.Domain.Entities.Validations;
using Core.NotifierErrors;
using Core.Services;

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
                Notify("User", "There is already a user with this document.");
                return;
            }

            await _userRepository.Insert(user);
        }

        public async Task UpdateUser(User newUser)
        {
            var user = await _userRepository.GetOneUsersAddresses(newUser.Id);
            if (user == null)
            {
                return;
            }

            // TODO: Refactor
            user.Name = newUser.Name;
            user.BirthDate = newUser.BirthDate;
            user.Phone = newUser.Phone;
            user.Ddd = newUser.Ddd;
            user.LastModifiedBy = "Admin";
            user.LastModifiedDate = DateTime.Now;

            user.Address.Street = newUser.Address.Street;
            user.Address.Number = newUser.Address.Number;
            user.Address.Complement= newUser.Address.Complement;
            user.Address.CEP = newUser.Address.CEP;
            user.Address.City = newUser.Address.City;
            user.Address.UF = newUser.Address.UF;
            user.Address.District = newUser.Address.District;
            user.Address.LastModifiedBy = "Admin";
            user.Address.LastModifiedDate = DateTime.Now;
            // TODO: Refactor
            //if (!ValidateEntity(new UserValidator(), pessoa)) return;

            //if (_userRepository.Get(f => f.Document == pessoa.Document && f.Id != pessoa.Id).Result.Any())
            //{
            //    Notify("User", "There is already a user with this document.");
            //    return;
            //}

            await _userRepository.Update(user);
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
