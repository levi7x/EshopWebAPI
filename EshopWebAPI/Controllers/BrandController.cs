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
        private readonly IBrandRepositoy _brandRepositoy;

        public BrandController(IBrandRepositoy brandRepositoy)
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

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetBrandByName(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var brand = _brandRepositoy.GetBrand(id);

            if (brand == null)
            {
                return NotFound();
            }

            return Ok(brand);
        }

        [HttpGet("{name:string}")]
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
    }
}
