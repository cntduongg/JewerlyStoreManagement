using JewelryService.Models;
using JewelryService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Repositories.Implementations
{
    public class PromotionRepository : RepositoryBase<Promotion>, IPromotionRepository
    {
        public PromotionRepository(DbContext context) : base(context)
        {
        }

        // ✅ Lấy các chương trình khuyến mãi đang hoạt động
        public async Task<IEnumerable<Promotion>> GetActivePromotionsAsync()
        {
            var now = DateTime.Now;
            return await _dbSet
                .Where(p => p.Isactive == true && !p.Isdeleted && p.Startdate <= now && p.Enddate >= now)
                .ToListAsync();
        }

        // ✅ Lấy các promotion áp dụng cho 1 product cụ thể
        public async Task<IEnumerable<Promotion>> GetByProductAsync(int productId)
        {
            // 1. Filter out the non-deleted/null records first in SQL.
            // 2. Use .AsEnumerable() to switch from LINQ-to-Entities (SQL) 
            //    to LINQ-to-Objects (C# in-memory).
            var promotions = await _dbSet
                .Where(p => !p.Isdeleted && p.Applicableproducts != null)
                .ToListAsync(); // Or .AsEnumerable() if you prefer to use the existing Task structure

            // 3. Perform the complex string-splitting/checking in C# memory.
            var productIdString = productId.ToString();

            return promotions
                .Where(p => p.Applicableproducts.Split(',').Contains(productIdString));
        }

        // ✅ Lấy các promotion áp dụng cho 1 category cụ thể
        public async Task<IEnumerable<Promotion>> GetByCategoryAsync(int categoryId)
        {
            // 1. Execute a partial query in the database (SQL)
            //    This efficiently filters out deleted records and null category strings.
            var promotionsToFilter = await _dbSet
                .Where(p => !p.Isdeleted && p.Applicablecategories != null)
                .ToListAsync();

            // 2. Perform the complex string-splitting filter in memory (C#)
            var categoryIdString = categoryId.ToString();

            return promotionsToFilter
                .Where(p => p.Applicablecategories
                              .Split(',') // This now executes in C#
                              .Contains(categoryIdString));
        }
    }
}
