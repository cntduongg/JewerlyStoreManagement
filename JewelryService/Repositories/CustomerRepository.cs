using JewelryService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(DbContext context) : base(context)
        {
        }

        // 🔹 Tìm khách hàng theo số điện thoại
        public async Task<Customer?> GetByPhoneAsync(string phone)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Phone == phone && !c.Isdeleted);
        }

        // 🔹 Tìm khách hàng theo tên (search keyword)
        public async Task<IEnumerable<Customer>> SearchByNameAsync(string keyword)
        {
            return await _dbSet
                .Where(c => c.Fullname.ToLower().Contains(keyword.ToLower()) && !c.Isdeleted)
                .OrderBy(c => c.Fullname)
                .ToListAsync();
        }

        // 🔹 Lấy danh sách khách hàng đang hoạt động
        public async Task<IEnumerable<Customer>> GetActiveCustomersAsync()
        {
            return await _dbSet
                .Where(c => c.Isactive && !c.Isdeleted)
                .OrderByDescending(c => c.Createdat)
                .ToListAsync();
        }

        // 🔹 Kiểm tra số điện thoại có bị trùng không
        public async Task<bool> IsPhoneTakenAsync(string phone)
        {
            return await _dbSet.AnyAsync(c => c.Phone == phone && !c.Isdeleted);
        }
    }
}
