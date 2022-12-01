using AutoMapper;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
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


    }
}
