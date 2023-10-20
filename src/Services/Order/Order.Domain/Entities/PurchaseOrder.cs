using Core.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Domain.Entities
{
    public class PurchaseOrder : BaseControlEntity
    {
        public Guid ClientId{ get; set; }
        public decimal TotalPrice { get; set; }
        public List<Product> Products { get; set; }
        public DeliveryAddress? DeliveryAddress { get; set; }
        [NotMapped]
        public Payment? Payment { get; set; }
        public string? Status { get; set; }
    }
}
