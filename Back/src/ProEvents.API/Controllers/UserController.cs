using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEvents.API.Extentions;
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

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _userService.getUserByUsenameAsync(userName);

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
                if (userDTO is null || userDTO.UserName is null)
                {
                    return BadRequest("Invalid Request.");
                }

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
                if (userLoginDTO is null)
                {
                    return BadRequest("Invalid Request.");
                }

                var user = await _userService.getUserByUsenameAsync(userLoginDTO.UserName);

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
                        username = user.UserName,
                        firstname = user.FirstName,
                        lastname = user.LastName,
                        token = _tokenService.CreateToken(user).Result
                    }
                );
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Save User. Error: {e.Message}");
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(UserDetailDTO userDetailDTO)
        {
            try
            {
                if (userDetailDTO is null)
                {
                    return BadRequest("Invalid Request.");
                }

                if (userDetailDTO.UserName != User.GetUserName() || User.GetUserId() != userDetailDTO.Id)
                {
                    return Unauthorized("Invalid User.");
                }

                var user = await _userService.UpdateUser(userDetailDTO);

                if (user == null)
                {
                    return NoContent();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Update User. Error: {e.Message}");
            }
        }

        [HttpPut("reset-password")]
        public async Task<IActionResult> ResetPassword(UserDetailDTO userDetailDTO)
        {
            try
            {
                if (userDetailDTO is null || string.IsNullOrEmpty(userDetailDTO.Password))
                {
                    return BadRequest("Invalid Request.");
                }

                if (userDetailDTO.UserName != User.GetUserName() || User.GetUserId() != userDetailDTO.Id)
                {
                    return Unauthorized("Invalid User.");
                }

                if (!userDetailDTO.Password.Equals(userDetailDTO.PasswordConfirmed))
                {
                    return BadRequest("Password not match.");
                }

                var user = await _userService.ResetPassword(userDetailDTO);

                if (user == null)
                {
                    return NoContent();
                }
                return Ok(user);
            }
            catch (Exception e)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                $"Error in Update User. Error: {e.Message}");
            }
        }
    }
}