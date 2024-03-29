﻿using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;

namespace EshopWebAPI.Data.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetProducts();
        public Product GetProduct(int id);
        public Product GetProduct(string productName);
        public bool CreateProduct(Product product);
        public bool UpdateProduct(Product product);
        public bool DeleteProduct(int id);
        //relational
        public bool AddBrandToProduct(Brand brand, Product product);
        public bool AddProductToCategory(Product product, Category category);
        public bool RemoveProductFromCategory(Product product, Category category);
        public bool IfProductHasCategory(Product product, Category category);

        //UNIT TEST WRAPPER
        Product GetProductByNameToLower(ProductDto createProduct);
    }
}
