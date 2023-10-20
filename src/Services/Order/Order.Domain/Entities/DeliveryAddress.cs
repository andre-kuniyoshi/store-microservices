using Core.Common;

namespace Order.Domain.Entities
{
    public class DeliveryAddress : BaseEntity
    {
        public Guid? OrderId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
        public PurchaseOrder Order { get; set; }
    }
}
