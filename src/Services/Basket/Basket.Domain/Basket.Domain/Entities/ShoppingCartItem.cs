﻿namespace Basket.API.Entities
{
    public class ShoppingCartItem
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
    }
}
