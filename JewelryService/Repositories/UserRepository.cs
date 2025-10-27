using JewelryService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        // 🔹 Lấy user theo username (dùng cho đăng nhập)
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username && !u.Isdeleted);
        }

        // 🔹 Lấy danh sách user đang hoạt động
        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .Where(u => u.Isactive && !u.Isdeleted)
                .OrderBy(u => u.Fullname)
                .ToListAsync();
        }

        // 🔹 Kiểm tra email đã tồn tại
        public async Task<bool> IsEmailTakenAsync(string email)
        {
            return await _dbSet.AnyAsync(u => u.Email == email && !u.Isdeleted);
        }

        // 🔹 Kiểm tra số điện thoại đã tồn tại
        public async Task<bool> IsPhoneTakenAsync(string phone)
        {
            return await _dbSet.AnyAsync(u => u.Phone == phone && !u.Isdeleted);
        }
    }
}
