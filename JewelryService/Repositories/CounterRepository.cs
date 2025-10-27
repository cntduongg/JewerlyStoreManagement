using JewelryService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public class CounterRepository : RepositoryBase<Counter>, ICounterRepository
    {
        public CounterRepository(DbContext context) : base(context)
        {
        }

        // 🔹 Lấy counter theo mã
        public async Task<Counter?> GetByCodeAsync(string code)
        {
            return await _dbSet.FirstOrDefaultAsync(c => c.Countercode == code && !c.Isdeleted);
        }

        // 🔹 Lấy tất cả quầy hoạt động
        public async Task<IEnumerable<Counter>> GetActiveCountersAsync()
        {
            return await _dbSet
                .Where(c => c.Isactive && !c.Isdeleted)
                .OrderBy(c => c.Countername)
                .ToListAsync();
        }

        // 🔹 Kiểm tra mã counter đã tồn tại
        public async Task<bool> IsCodeTakenAsync(string code)
        {
            return await _dbSet.AnyAsync(c => c.Countercode == code && !c.Isdeleted);
        }
    }
}
