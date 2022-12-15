using AutoMapper;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressController(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAddresses()
        {
            var addresses = _mapper.Map<List<AddressDto>>(_addressRepository.GetAddresses());
            return Ok(addresses);
        }


        [HttpGet("{addressId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetAddress(int addressId)
        {
            if (addressId == 0)
            {
                return BadRequest();
            }

            var address = _mapper.Map<AddressDto>(_addressRepository.GetAddress(addressId));

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAddressByUser(string userId)
        {
            var address = _mapper.Map<AddressDto>(_addressRepository.GetAddressByUser(userId));

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(address);
        }


        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateAddress([FromBody] AddressDto addressDto)
        {
            if (addressDto == null)
            {
                return BadRequest();
            }
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userAddress = _addressRepository.GetAddressByUser(currentUserId);

            var newAddress = _mapper.Map<Address>(addressDto);

            newAddress.Id = userAddress.Id;

            var result = _addressRepository.UpdateAddress(newAddress);

            if(!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(newAddress);
        }

        
    }
}
