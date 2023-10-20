
using Core.Common;

namespace Order.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public PurchaseOrder Order { get; set; }
    }
}
