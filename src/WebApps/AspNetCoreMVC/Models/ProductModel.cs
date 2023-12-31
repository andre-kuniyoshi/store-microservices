﻿namespace AspNetCoreMVC.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string ObjectId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int Quantity { get; set; }
    }
}
