﻿namespace EshopWebAPI.Models.Dto
{
    public class ProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ProductImageUrl { get; set; }
    }
}
