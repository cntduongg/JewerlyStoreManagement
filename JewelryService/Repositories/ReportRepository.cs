using JewelryService.Models;
using JewelryService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Repositories.Implementations
{
    public class ReportRepository : RepositoryBase<Report>, IReportRepository
    {
        public ReportRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Report>> GetByDateRangeAsync(DateOnly start, DateOnly end)
        {
            return await _dbSet
                .Where(r => r.Startdate >= start && r.Enddate <= end)
                .Include(r => r.Category)
                .Include(r => r.Counter)
                .Include(r => r.Staff)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetByStaffAsync(int staffId)
        {
            return await _dbSet
                .Where(r => r.Staffid == staffId)
                .Include(r => r.Category)
                .Include(r => r.Counter)
                .ToListAsync();
        }

        public async Task<IEnumerable<Report>> GetByCategoryAsync(int categoryId)
        {
            return await _dbSet
                .Where(r => r.Categoryid == categoryId)
                .Include(r => r.Staff)
                .Include(r => r.Counter)
                .ToListAsync();
        }
    }
}
