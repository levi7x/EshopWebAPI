using AutoMapper;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebAPI.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository,IBrandRepository brandRepository,ICategoryRepository categoryRepository,  IMapper mapper, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProducts()
        {
            _logger.LogInformation(DateTime.UtcNow + " Getting all products");
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());
            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
            _logger.LogInformation(DateTime.UtcNow + $"Getting product with id: {productId}");
            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(productId));

            if (product == null)
            {
                _logger.LogWarning(DateTime.UtcNow + $"Product with id: {productId} was not found");
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProduct([FromBody] ProductDto createProduct)
        {
            if (createProduct == null)
            {
                return BadRequest(createProduct);
            }

            //var product = _productRepository.GetProducts().FirstOrDefault(p=>p.ProductName.ToLower() == createProduct.ProductName.ToLower());
            var product = _productRepository.GetProductByNameToLower(createProduct);

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exists !");
                return BadRequest(createProduct);
            }

            var productMap = _mapper.Map<Product>(createProduct);

            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(createProduct);
        }


        //todo verify if i need seperate id
        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest();
            }

            var updatedProduct = _mapper.Map<Product>(productDto);

            var result = _productRepository.UpdateProduct(updatedProduct);

            return Ok(result);
        }

        //todo add authorize
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteProduct(int productId)
        {
            if (productId == 0)
            {
                return BadRequest();
            }

            _productRepository.DeleteProduct(productId);

            return NoContent();
        }


        //relational


        [HttpPost("AddCategory/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult AddProductToCategory(int productId, int categoryId)
        {
            var product = _productRepository.GetProduct(productId);
            var category = _categoryRepository.GetCategory(categoryId);

            if (product == null || category == null)
            {
                return BadRequest();
            }

            if (_productRepository.IfProductHasCategory(product, category))
            {
                return Conflict();
            }

            var result = _productRepository.AddProductToCategory(product, category);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(result);
        }

        [HttpDelete("RemoveCategory/{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult RemoveCategoryFromProduct(int productId, int categoryId)
        {

            var product = _productRepository.GetProduct(productId);
            var category = _categoryRepository.GetCategory(categoryId);

            if (product == null || category == null)
            {
                return BadRequest();
            }

            var result = _productRepository.RemoveProductFromCategory(product, category);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }


        [HttpPost("AddBrand/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBrandToProduct(int brandId, int productId)
        {
            var brand = _brandRepository.GetBrand(brandId);

            if (brand == null)
            {
                return NotFound();
            }

            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var result = _productRepository.AddBrandToProduct(brand, product);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(brand);
        }
    }
}
