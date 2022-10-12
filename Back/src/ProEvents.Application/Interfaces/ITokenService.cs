using System.Threading.Tasks;
using ProEvents.Application.Dtos;

namespace ProEvents.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(UserDetailDTO userDetailDTO);
    }
}