using UserApi.Models.Dtos;
using UserApi.Repositories;

namespace UserApi.Service.IService
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(CreateUserDto dto);
        Task<(bool Success, string Message)> LoginAsync(LoginDto dto);
        Task LogoutAsync();
    }
}
