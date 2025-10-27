using JewelryService.Models;
using JewelryService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Repositories.Implementations
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }

        // ✅ Lấy danh sách sản phẩm đang hoạt động
        public async Task<IEnumerable<Product>> GetActiveProductsAsync()
        {
            return await _dbSet
                .Where(p => p.Isactive == true && !p.Isdeleted)
                .OrderByDescending(p => p.Createdat)
                .ToListAsync();
        }

        // ✅ Lấy sản phẩm theo danh mục
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(p => p.Categoryid == categoryId && !p.Isdeleted)
                .Include(p => p.Category)
                .ToListAsync();
        }

        // ✅ Tìm kiếm sản phẩm theo từ khóa (tên, code, hoặc tag)
        public async Task<IEnumerable<Product>> SearchAsync(string keyword)
        {
            keyword = keyword.ToLower();
            return await _dbSet
                .Where(p => !p.Isdeleted && (
                    p.Productname.ToLower().Contains(keyword) ||
                    p.Productcode.ToLower().Contains(keyword) ||
                    (p.Tags != null && p.Tags.ToLower().Contains(keyword))
                ))
                .Include(p => p.Category)
                .ToListAsync();
        }

        // ✅ Lấy sản phẩm sắp hết hàng
        public async Task<IEnumerable<Product>> GetLowStockProductsAsync()
        {
            return await _dbSet
                .Where(p => p.Minstocklevel.HasValue && p.Stockquantity <= p.Minstocklevel && !p.Isdeleted)
                .OrderBy(p => p.Stockquantity)
                .ToListAsync();
        }
    }
}
