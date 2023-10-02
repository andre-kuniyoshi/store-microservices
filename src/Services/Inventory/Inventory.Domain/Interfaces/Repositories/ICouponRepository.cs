using Inventory.Domain.Entities;

namespace Inventory.Domain.Interfaces.Repositories
{
    public interface ICouponRepository : IGenericRepository<Coupon>
    {
        public Task<Coupon?> GetCouponByCode(string code);
        public Task<bool> CodeAlreadyExist(string code);
    }
}
