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

        public BrandController(IBrandRepository brandRepositoy)
        {
            _brandRepositoy = brandRepositoy;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBrands()
        {
            var brands = _brandRepositoy.GetBrands();
            return Ok(brands);
        }

        [HttpGet("{brandId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBrandByName(int brandId)
        {
            if (brandId == 0)
            {
                return BadRequest();
            }

            var brand = _brandRepositoy.GetBrand(brandId);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBrandByName(string name)
        {
            var brand = _brandRepositoy.GetBrand(name);

            if (brand == null)
            {
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
