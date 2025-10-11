using UserApi.Models.Dtos;
using UserApi.Repositories.IRepo;
using UserApi.Service.IService;

namespace UserApi.Service
{
    public class UserService : IUserService
    {
        private readonly iUserRepo _userRepository;

        public UserService(iUserRepo userRepository)
        {
            _userRepository = userRepository;
        }
        //get list user page
        public async Task<PagedResult<UserDto>> GetAllUsersAsync(int currentPage = 1, int size = 10)
        {
            var skip = (currentPage - 1) * size;
            var users = await _userRepository.GetPagedUsersAsync(skip, size);
            var totalCount = await _userRepository.GetTotalCountAsync();
            var response = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();

            return new PagedResult<UserDto>
            {
                Items = response,
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PageSize = size
            };
        }
        //get all
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();
        }
        //changepass
        public async Task<(bool Success, string Message)> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return (false, "User not found");

            // Logic kiểm tra oldPassword (giả sử đã hash)
            if (user.PasswordHash != HashPassword(oldPassword)) return (false, "Old password is incorrect");

            user.PasswordHash = HashPassword(newPassword); // Hàm hash cần được triển khai
            await _userRepository.SaveAsync();
            return (true, "Password changed successfully");
        }
        //update user
        public async Task<(bool Success, string Message)> UpdateUserAsync(int id, UserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return (false, "User not found");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.UpdatedAt = DateTime.Now;
            await _userRepository.SaveAsync();
            return (true, "User updated successfully");
        }
        //delete user
        public async Task<(bool Success, string Message)> DeleteUserAsync(int id)
        {
            var result = await _userRepository.DeleteAsync(id);
            return result ? (true, "User deleted successfully") : (false, "User not found");
        }
        // get by id
        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public async Task<PagedResult<UserDto>> GetPagedUsersByRoleAsync(string role, int currentPage = 1, int size = 10)
        {
            var skip = (currentPage - 1) * size;
            var users = await _userRepository.GetPagedUsersByRoleAsync(role, skip, size);
            var totalCount = await _userRepository.GetTotalCountAsync(role);
            var response = users.Select(u => new UserDto
            {
                UserId = u.UserId,
                Username = u.Username,
                FullName = u.FullName,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();

            return new PagedResult<UserDto>
            {
                Items = response,
                TotalCount = totalCount,
                CurrentPage = currentPage,
                PageSize = size
            };
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        Task<IService.PagedResult<UserDto>> IUserService.GetAllUsersAsync(int currentPage, int size)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UserDto>> IUserService.GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

       

        Task<UserDto> IUserService.GetUserByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<IService.PagedResult<UserDto>> IUserService.GetPagedUsersByRoleAsync(string role, int currentPage, int size)
        {
            throw new NotImplementedException();
        }
    }

    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
