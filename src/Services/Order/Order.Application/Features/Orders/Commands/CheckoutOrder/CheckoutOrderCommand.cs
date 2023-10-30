using MediatR;

namespace Order.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommand : IRequest<Guid>
    {
        public Guid ClientId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<Product> Products { get; set; }
        public DeliveryAddress? DeliveryAddress { get; set; }
        public Payment? Payment { get; set; }
    }

    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class DeliveryAddress
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string City { get; set; }
        public string UF { get; set; }
    }

    public class Payment
    {
        public string? CardName { get; set; }
        public string? CardNumber { get; set; }
        public string? Expiration { get; set; }
        public string? CVV { get; set; }
        public int? PaymentMethod { get; set; }
    }
}
