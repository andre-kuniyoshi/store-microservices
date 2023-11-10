
namespace Basket.Domain.Entities
{
    public class BasketCheckout
    {
        public Guid ClientId { get; set; }
        public decimal TotalPrice { get; set; }     
        public List<ShoppingCartItem> Products { get; set; }

        // DeliveryAddress
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Street { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public string City { get; set; }
        public string UF { get; set; }

        // Payment
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
        public int PaymentMethod { get; set; }
    }
}
