using Microsoft.EntityFrameworkCore;
using System.Data;
using UserApi.Data;
using UserApi.Models.Entities;
using UserApi.Repositories.IRepo;

namespace UserApi.Repositories
{
    public class UserRepo : iUserRepo
    {
        private readonly AppDBContext _context;
        public UserRepo(AppDBContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            try
            {
                Console.WriteLine($"Adding user: {user.Username}");
                await _context.Users.AddAsync(user);
                await SaveAsync();
                Console.WriteLine($"User {user.Username} added successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await SaveAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(string account)
        {
            return await _context.Users.AnyAsync(u => u.Username == account);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByAccountAsync(string account)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == account);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id) ?? throw new Exception("User not found");
        }

        public async Task<IEnumerable<User>> GetPagedUsersAsync(int skip, int take)
        {
            return await _context.Users.Where(u => u.Role == "Employee").Skip(skip).Take(take).ToListAsync();
        }

        public  async Task<IEnumerable<User>> GetPagedUsersByRoleAsync(string role, int skip, int take)
        {
            return await _context.Users.Skip(skip).Take(take).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync(string? role = null)
        {
            return role == null ? await _context.Users.CountAsync() : await _context.Users.CountAsync(u => u.Role == role);
        }

        public async Task SaveAsync()
        {
            try
            {
                Console.WriteLine("Saving changes to database...");
                await _context.SaveChangesAsync();
                Console.WriteLine("Changes saved successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving changes: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
