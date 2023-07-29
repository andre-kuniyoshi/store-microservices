using AspNetRazor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetRazor.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
    }
}
