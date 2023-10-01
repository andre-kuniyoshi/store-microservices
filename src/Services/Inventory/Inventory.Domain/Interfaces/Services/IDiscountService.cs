using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Services
{
    public interface IDiscountService
    {
        public Task<Coupon> GetDiscount(string productName);
        public Task<bool> AddDiscount(Coupon coupon);
        public Task<bool> UpdateDiscount(Coupon coupon);
        public Task<bool> RemoveDiscount(string productName);
    }
}
