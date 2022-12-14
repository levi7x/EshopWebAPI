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
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProducts()
        {
            var products = _mapper.Map<List<ProductDto>>(_productRepository.GetProducts());
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var product = _mapper.Map<ProductDto>(_productRepository.GetProduct(id));

            if (product == null)
            {
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

            var product = _productRepository.GetProducts().FirstOrDefault(p=>p.ProductName.ToLower() == createProduct.ProductName.ToLower());

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

        [HttpPost("addBrand/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBrandToProduct([FromBody] Brand brand, int productId)
        {
            if (brand == null)
            {
                return BadRequest();
            }

            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return BadRequest();
            }

            var result = _productRepository.AddBrandToProduct(brand, product);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(product);
        }

        //todo verify if i need seperate id
        [HttpPut("{productId}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            if (productDto == null || productId != productDto.Id)
            {
                return BadRequest();
            }

            var updatedProduct = _mapper.Map<Product>(productDto);

            var result = _productRepository.UpdateProduct(updatedProduct);

            return Ok(result);
        }

        //todo add authorize
        [HttpDelete]
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

        [HttpPost("createCategory/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddProductToCategory(int productId, [FromBody] CategoryDto categoryDto)
        {
            if (productId == 0 || categoryDto == null)
            {
                return BadRequest();
            }

            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return BadRequest();
            }
            var category = _mapper.Map<Category>(categoryDto);
            var result = _productRepository.AddProductToCategory(product, category);

            return Ok(result);
        }

        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult RemoveCategoryFromProduct(int productId, [FromBody] CategoryDto categoryDto)
        {
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDto);

            _productRepository.RemoveProductFromCategory(product, category);

            return NoContent();
        }
    }
}
