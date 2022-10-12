using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;

namespace ProEvents.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        //[AllowAnonymous] // Allow get without authentication
        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetUser(string username)
        {
            try
            {
                var user = await _userService.getUserByUsenameAsync(username);

                return Ok(user);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error in get user. Error: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            try
            {
                if (await _userService.UserExists(userDTO.UserName))
                {
                    return BadRequest("User already exist.");
                }

                var user = await _userService.CreateUserAsync(userDTO);

                if (user == null)
                {
                    return NoContent();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Save User. Error: {e.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var user = await _userService.getUserByUsenameAsync(userLoginDTO.Username);

                if (user == null)
                {
                    return Unauthorized("Invalid User.");
                }

                var result = await _userService.CheckUserPasswordAsync(user, userLoginDTO.Password);

                if (!result.Succeeded)
                {
                    return Unauthorized("Invalid User Credentials.");
                }

                return Ok(
                    new
                    {
                        username = user.Username,
                        firstname = user.FirstName,
                        lastname = user.lastName,
                        token = _tokenService.CreateToken(user)
                    }
                );
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Save User. Error: {e.Message}");
            }
        }
    }
}