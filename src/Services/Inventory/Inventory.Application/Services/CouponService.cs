using Core.NotifierErrors;
using Core.Services;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Domain.Interfaces.Services;

namespace Inventory.Application.Services
{
    public class CouponService : BaseService, ICouponService
    {
        private readonly ICouponRepository _couponRepo;

        public CouponService(ICouponRepository CouponRepo, INotifier notifier) : base(notifier)
        {
            _couponRepo = CouponRepo;
        }

        public async Task<Coupon?> GetCouponByCode(string code)
        {
            return await _couponRepo.GetCouponByCode(code);
        }

        public async Task<bool> AddCoupon(Coupon coupon)
        {
            var isCodeExist = _couponRepo.CodeAlreadyExist(coupon.Code);
            
            if (isCodeExist == null)
            {
                Notify("code", "This code already exist");
                return false;
            }

            return await _couponRepo.AddAsync(coupon);
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            return await _couponRepo.UpdateAsync(coupon);
        }

        public async Task<bool> RemoveCoupon(Guid couponId)
        {
            return await _couponRepo.DeleteLogicAsync(couponId);
        }

        public async Task<bool> UseOneCoupon(Guid couponId)
        {
            var coupon = await _couponRepo.GetById(couponId);

            if(coupon == null)
            {
                Notify("Coupon", "Coupon not found");
                return false;
            }
            coupon.RemoveOneCoupon();

            await _couponRepo.UpdateAsync(coupon, coupon.Id);

            return true;
        }
    }
}
