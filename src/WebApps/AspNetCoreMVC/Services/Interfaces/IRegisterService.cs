using AspNetCoreMVC.Models;

namespace AspNetCoreMVC.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<UserModel> GetUserInfos();
        Task<UserModel> UpdateUserInfos(UserModel user);
    }
}
