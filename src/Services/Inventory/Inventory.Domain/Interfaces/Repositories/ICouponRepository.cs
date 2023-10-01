using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Repositories
{
    public interface ICouponRepository
    {
        public Task<Coupon> GetDiscount(string productName);
        public Task<bool> CreateDiscount(Coupon coupon);
        public Task<bool>UpdateDiscount(Coupon coupon);
        public Task<bool> DeleteDiscount(string productName);
    }
}
