using Azure.Core;
using EshopWebAPI.Data;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Services;
using EshopWebAPI.Services.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Security.Claims;
using System.Security.Cryptography;

namespace EshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;
        private readonly IUserRepository _userRepository;

        public AuthController(UserManager<User> userManager, JwtService jwtService, IUserRepository userRepository)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _userRepository = userRepository;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<User>> RegisterUser([FromBody] UserLoginDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userByEmail = await _userManager.FindByEmailAsync(user.Email);

            if(userByEmail != null)
            {
                ModelState.AddModelError("", "User with that email already exists !");
                return Conflict(user);
            }

            var Address = new Address()
            {
                Street = "",
                City = "",
                State = "",
                PostalCode = ""
            };

            var newUser = new User() { UserName = user.Email, Email = user.Email, Address = Address, CreatedDate = DateTime.UtcNow };

            var result = await _userManager.CreateAsync(newUser,user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }


            var roleResult = await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            user.Password = null;
            return Created("", user);
        }


        //login
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<string>> Login(AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Bad credentials");
            }
            
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return BadRequest("Bad credentials");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userRole = await _userManager.GetRolesAsync(user);
            if (userRole == null)
            {
                return BadRequest("User not in role");
            }

            //var jwt = _jwtService.CreateToken(user, userRole.First());


            var jwt = _jwtService.Generate(user, userRole.First());


            //Response.Cookies.Append("jwt", jwt, new CookieOptions
            //{
            //    HttpOnly = true,
            //    SameSite = SameSiteMode.None,
            //    Secure = true,
            //    Path = "/"
            //});

            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Problem while updating user");
            }

            return Ok(jwt);
        }

        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                var user = _userRepository.UserByToken(refreshToken);

                if (user == null)
                {
                    return Unauthorized("Invalid refresh token");
                }
                if (user.TokenExpires < DateTime.Now)
                {
                    return Unauthorized("Token has expired");
                }


                var userRole = await _userManager.GetRolesAsync(user);

                var token = _jwtService.Generate(user, userRole.First());
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken);

                user.RefreshToken = newRefreshToken.Token;
                user.TokenCreated = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;

                var result = await _userManager.UpdateAsync(user);

                return Ok(token);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            };

            Response.Cookies.Delete("refreshToken", cookieOptions);
            
            return Ok(new
            {
                message = "success"
            });
        }

        private RefreshToken GenerateRefreshToken(User user)
        {
            var refreshToken = new RefreshToken
            {
                Token = _jwtService.Generate(user),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true,
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);
        }
    }
}
