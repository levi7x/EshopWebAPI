using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepositoy;
        private readonly ILogger _logger;

        public BrandController(IBrandRepository brandRepositoy, ILogger<BrandController> logger)
        {
            _brandRepositoy = brandRepositoy;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBrands()
        {
            _logger.LogInformation(DateTime.UtcNow + " Getting all Brands");
            var brands = _brandRepositoy.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBrandById(int brandId)
        {
            if (brandId == 0)
            {
                return BadRequest();
            }
            _logger.LogInformation(DateTime.UtcNow + $" Getting brand by id: {brandId}");
            var brand = _brandRepositoy.GetBrand(brandId);

            if (brand == null)
            {
                _logger.LogWarning(DateTime.UtcNow + $" Brand with the id: {brandId} was not found");
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBrandByName(string name)
        {
            if (name == null)
            {
                return BadRequest();
            }
            _logger.LogInformation(DateTime.UtcNow + $" Getting brand {name}");
            var brand = _brandRepositoy.GetBrand(name);

            if (brand == null)
            {
                _logger.LogWarning(DateTime.UtcNow + $" Brand {name} was not found");
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateBrand([FromBody] Brand brand)
        {
            if (brand == null)
            {
                return BadRequest();
            }

            var result = _brandRepositoy.CreateBrand(brand);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            return Ok(brand);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateBrand([FromBody] Brand brand)
        {
            if (brand == null)
            {
                return BadRequest();
            }

            var result = _brandRepositoy.UpdateBrand(brand);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(brand);
        }
    }
}
