using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEvents.Application.Dtos;

namespace ProEvents.Application.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExists(string username);
        Task<UserDetailDTO> getUserByUsenameAsync(string username);
        Task<SignInResult> CheckUserPasswordAsync(UserDetailDTO userDetailDTO, string password);
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDetailDTO> UpdateUser(UserDetailDTO userDetailDTO);
    }
}