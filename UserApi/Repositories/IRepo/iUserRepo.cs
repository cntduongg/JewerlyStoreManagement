using UserApi.Models.Entities;

namespace UserApi.Repositories.IRepo
{
    public interface iUserRepo
    {
        Task<bool> ExistsAsync(string account);
        Task<User?> GetByAccountAsync(string account);
        Task AddAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<int> GetTotalCountAsync(string? role = null);
        Task<IEnumerable<User>> GetPagedUsersAsync(int skip, int take);
        Task SaveAsync();
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<User>> GetPagedUsersByRoleAsync(string role, int skip, int take);
    }
}
