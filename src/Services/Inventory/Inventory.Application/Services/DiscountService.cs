using Discount.Domain.Entities;
using Discount.Domain.Interfaces.Repositories;
using Discount.Domain.Interfaces.Services;

namespace Discount.Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepo;

        public DiscountService(IDiscountRepository discountRepo)
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
