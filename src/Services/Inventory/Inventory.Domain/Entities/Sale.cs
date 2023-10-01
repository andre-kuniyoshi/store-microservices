using Core.Common;

namespace Inventory.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Guid ProductId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public decimal Price { get; set; }
    }
}
