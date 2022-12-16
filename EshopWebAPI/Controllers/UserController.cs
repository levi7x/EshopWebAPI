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
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, IMapper mapper, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            _logger.LogInformation(DateTime.UtcNow + " Getting all users");
            var users = _mapper.Map<List<UserDto>>(_userRepository.GetUsers());
            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(string userId)
        {
            _logger.LogInformation(DateTime.UtcNow + $" Getting user with id: {userId}");
            var user = _mapper.Map<UserDto>(_userRepository.GetUser(userId));

            if (user == null)
            {
                _logger.LogWarning(DateTime.UtcNow + $"User with id: {userId} was not found");
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateUserInfo([FromBody] UserDto userDto)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userDto == null)
            {
                return BadRequest();
            }

            var user = _userRepository.GetUserAsNoTracking(currentUserId);

            user.PhoneNumber = userDto.PhoneNumber;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;

            var result = _userRepository.UpdateUser(user);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(userDto);
        }
    }
}
