using AutoMapper;
using Castle.Core.Logging;
using EshopWebAPI.Controllers;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopWebAPI.Tests.Controller
{
    public class ProductControllerTests
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductControllerTests()
        {
            _productRepository = A.Fake<IProductRepository>();
            _brandRepository = A.Fake<IBrandRepository>();
            _categoryRepository = A.Fake<ICategoryRepository>();
            _mapper = A.Fake<IMapper>();
            _logger = A.Fake<ILogger<ProductController>>();
        }

        [Fact]
        public void ProductController_GetProducts_ReturnOK()
        {
            //Arrange
            var products = A.Fake<ICollection<ProductDto>>();
            var productList = A.Fake<List<ProductDto>>();
            A.CallTo(() => _mapper.Map<List<ProductDto>>(products)).Returns(productList);
            var controller = new ProductController(_productRepository, _brandRepository, _categoryRepository, _mapper, _logger);

            //Act
            var result = controller.GetProducts();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void ProductController_CreateProduct_ReturnOK()
        {
            //Arrange
            var createProduct = A.Fake<ProductDto>();
            var product = A.Fake<Product>();
            var productMap = A.Fake<Product>();
            var products = A.Fake<ICollection<ProductDto>>();
            var productList = A.Fake<List<ProductDto>>();
            A.CallTo(() => _productRepository.GetProductByNameToLower(createProduct)).Returns(product);
            A.CallTo(() => _mapper.Map<Product>(createProduct)).Returns(product);
            A.CallTo(() => _productRepository.CreateProduct(productMap)).Returns(true);
            var controller = new ProductController(_productRepository, _brandRepository, _categoryRepository, _mapper, _logger);

            //Act
            var result = controller.CreateProduct(createProduct);

            //Assert
            result.Should().NotBeNull();
        }
    }
}
