using Dapper;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces.Repositories;
using Inventory.Infra.Data.Context;

namespace Inventory.Infra.Data.Repositories
{
    public class CouponRepository : GenericRepository<Coupon>, ICouponRepository
    {
        public CouponRepository(IDbContext context) : base(context, "Coupons")
        {
        }

        public async Task<Coupon?> GetCouponByCode(string code)
        {
            var sql = "SELECT * FROM Coupon WHERE Code = @Code AND Active = true ;";

            var coupon = await Context.Connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { Code = code });

            return coupon;
        }

        public async Task<bool> CodeAlreadyExist(string code)
        {
            var coupon = await GetCouponByCode(code);

            return coupon is not null;
        }
    }
}
