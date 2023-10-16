using AspNetCoreMVC.Models;
using AspNetCoreMVC.Services.Interfaces;
using Core.Extensions;

namespace AspNetCoreMVC.Services
{
    public class RegisterService : IRegisterService
    {

        private readonly HttpClient _client;

        public RegisterService(HttpClient client, ILogger<RegisterService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<UserModel> GetUserInfos()
        {
            var response = await _client.GetAsync("/Register");
            return await response.ReadContentAs<UserModel>();
        }

        public async Task<UserModel> UpdateUserInfos(UserModel user)
        {
            var response = await _client.PutAsJson("/Register", user);
            return await response.ReadContentAs<UserModel>(); ;
        }
    }
}
