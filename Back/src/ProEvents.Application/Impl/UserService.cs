using System.Threading.Tasks;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProEvents.Application.Dtos;
using ProEvents.Application.Interfaces;
using ProEvents.Domain.Identity;
using ProEvents.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ProEvents.Application.Impl
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserRepository userRepository, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SignInResult> CheckUserPasswordAsync(UserDetailDTO userDetailDTO, string password)
        {
            try
            {
                var user = await _userManager.Users
                .SingleOrDefaultAsync(user => user.UserName.Equals(userDetailDTO.Username.ToLower()));

                return await _signInManager.CheckPasswordSignInAsync(user, password, false);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDTO)
        {
            try
            {
                var user = _mapper.Map<User>(userDTO);
                var result = await _userManager.CreateAsync(user, userDTO.Password);

                if (result.Succeeded)
                {
                    return _mapper.Map<UserDTO>(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }

        public async Task<UserDetailDTO> getUserByUsenameAsync(string username)
        {
            try
            {
                var user = await _userManager.Users
               .SingleOrDefaultAsync(user => user.UserName.Equals(username.ToLower()));

                if (user is not null)
                {
                    return _mapper.Map<UserDetailDTO>(user);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }

        public async Task<UserDetailDTO> UpdateUser(UserDetailDTO userDetailDTO)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(userDetailDTO.Username);
                if (user is not null)
                {
                    _mapper.Map(userDetailDTO, user);

                    _userRepository.Update(user);

                    if (await _userRepository.SaveChangesAsync())
                    {
                        user = await _userRepository.GetUserByUsernameAsync(user.UserName);
                        return _mapper.Map<UserDetailDTO>(user);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }

        public async Task<UserDetailDTO> ResetPassword(UserDetailDTO userDetailDTO)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(userDetailDTO.Username);
                if (user is not null)
                {
                    _mapper.Map(userDetailDTO, user);

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var result = await _userManager.ResetPasswordAsync(user, token, userDetailDTO.Password);

                    _userRepository.Update(user);

                    if (await _userRepository.SaveChangesAsync())
                    {
                        user = await _userRepository.GetUserByUsernameAsync(user.UserName);
                        return _mapper.Map<UserDetailDTO>(user);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }

        public async Task<bool> UserExists(string username)
        {
            try
            {
                return await _userManager.Users
               .AnyAsync(user => user.UserName.Equals(username.ToLower()));
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex);
            }
        }
    }
}