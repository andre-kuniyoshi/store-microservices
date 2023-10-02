using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Services
{
    public interface ICouponService
    {
        public Task<Coupon?> GetCouponByCode(string code);
        public Task<bool> UseOneCoupon(Guid couponId);
        public Task<bool> AddCoupon(Coupon coupon);
        public Task<bool> UpdateCoupon(Coupon coupon);
        public Task<bool> RemoveCoupon(Guid couponId);
    }
}
