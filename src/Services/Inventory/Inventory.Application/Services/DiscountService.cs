using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Domain.Interfaces.Services;

namespace Inventory.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ICouponRepository _discountRepo;

        public DiscountService(ICouponRepository discountRepo)
        {
            _discountRepo = discountRepo;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            return await _discountRepo.GetDiscount(productName);
        }

        public async Task<bool> AddDiscount(Coupon coupon)
        {
            return await _discountRepo.CreateDiscount(coupon);
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            return await _discountRepo.UpdateDiscount(coupon);
        }

        public async Task<bool> RemoveDiscount(string productName)
        {
            return await _discountRepo.DeleteDiscount(productName);
        }
    }
}
