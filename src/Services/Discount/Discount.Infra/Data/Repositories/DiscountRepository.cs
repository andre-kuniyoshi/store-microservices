using Dapper;
using Discount.Infra.Data.Context;
using Discount.Domain.Entities;
using Discount.Domain.Interfaces.Repositories;

namespace Discount.Infra.Data.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IDbContext _context;
        public DiscountRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var sql = "SELECT * FROM Coupon WHERE ProductName = @ProductName ;";

            using (var connection = _context.CrateConnection())
            {
                var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(sql, new { ProductName  = productName });

                if (coupon == null)
                {
                    return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
                }

                return coupon;
            }

        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var sql = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount) ; ";

            using (var connection = _context.CrateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { coupon.ProductName, coupon.Description, coupon.Amount });

                if(affectedRows == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var sql = "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id ; ";

            using (var connection = _context.CrateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { coupon.Id, coupon.ProductName, coupon.Description, coupon.Amount });

                if (affectedRows == 0)
                {
                    return false;
                }

                return true;
            }
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var sql = "DELETE FROM Coupon WHERE ProductName = @ProductName";

            using (var connection = _context.CrateConnection())
            {
                var affectedRows = await connection.ExecuteAsync(sql, new { ProductName = productName });

                if (affectedRows == 0)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
