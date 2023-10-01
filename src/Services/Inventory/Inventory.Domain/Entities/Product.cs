using Core.Common;

namespace Inventory.Domain.Entities
{
    public class Product : BaseControlEntity
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public int Quantity{ get; set; }
        public Sale Sale { get; set; }
    }
}
