namespace AspNetCoreMVC.Models
{
    public class BasketItemModel
    {
        public int Quantity { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public string ProductObjectId { get; set; }
        public string ProductName { get; set; }
    }
}
