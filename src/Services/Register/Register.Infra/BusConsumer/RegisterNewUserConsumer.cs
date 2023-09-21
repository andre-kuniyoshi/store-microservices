using EventBus.Messages.Events;
using MassTransit;
using Register.Application.Domain.Entities;
using Register.Application.Interfaces;

namespace Register.Infra.BusConsumer
{
    public class RegisterNewUserConsumer : IConsumer<RegisterNewUserEvent>
    {
        private readonly IUserRepository _userRepository;
        public RegisterNewUserConsumer(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Consume(ConsumeContext<RegisterNewUserEvent> context)
        {
            var newUser = new User
            {
                Id = context.Message.UserID,
                Name = context.Message.FullName,
                Email = context.Message.Email,
                BirthDate = context.Message.BirthDate,
                Document = context.Message.Document,
                Ddd = "11",
                Phone = context.Message.Phone,
                CreatedBy = "Identity.MVC",
                CreatedDate = DateTime.Now
            };

            await _userRepository.Insert(newUser);

        }
    }
}
