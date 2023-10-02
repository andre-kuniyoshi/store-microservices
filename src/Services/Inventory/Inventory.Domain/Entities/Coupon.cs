using Core.Common;

namespace Inventory.Domain.Entities
{
    public class Coupon : BaseControlEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal PercentageDiscount { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void RemoveOneCoupon()
        {
            Quantity--;
        }
    }
}
