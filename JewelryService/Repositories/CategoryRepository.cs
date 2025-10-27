using JewelryService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext context) : base(context)
        {
        }

        // 🔹 Lấy danh mục theo mã
        public async Task<Category?> GetByCodeAsync(string code)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Categorycode == code);
        }

        // 🔹 Lấy tất cả danh mục đang hoạt động
        public async Task<IEnumerable<Category>> GetActiveCategoriesAsync()
        {
            return await _dbSet
                .Where(c => c.Isactive)
                .OrderBy(c => c.Displayorder)
                .ThenBy(c => c.Categoryname)
                .ToListAsync();
        }

        // 🔹 Kiểm tra mã danh mục đã tồn tại
        public async Task<bool> IsCodeTakenAsync(string code)
        {
            return await _dbSet.AnyAsync(c => c.Categorycode == code);
        }
    }
}
