using UserApi.Models.Dtos;

namespace UserApi.Service.IService
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetAllUsersAsync(int currentPage = 1, int size = 10);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task<(bool Success, string Message)> UpdateUserAsync(int id, UserDto dto);
        Task<(bool Success, string Message)> DeleteUserAsync(int id);
        Task<UserDto> GetUserByIdAsync(int id);
        Task<PagedResult<UserDto>> GetPagedUsersByRoleAsync(string role, int currentPage = 1, int size = 10);
    }

    // Định nghĩa PagedResult để sử dụng trong giao diện
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
