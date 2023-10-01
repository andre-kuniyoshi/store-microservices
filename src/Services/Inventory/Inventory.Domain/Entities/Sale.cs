using Core.Common;

namespace Inventory.Domain.Entities
{
    public class Sale : BaseControlEntity
    {
        public Guid ProductId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
