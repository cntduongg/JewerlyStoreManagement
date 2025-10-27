using JewelryService.Models;

namespace JewelryService.Repositories.Interfaces
{
    public interface IReportRepository : IRepositoryBase<Report>
    {
        Task<IEnumerable<Report>> GetByDateRangeAsync(DateOnly start, DateOnly end);
        Task<IEnumerable<Report>> GetByStaffAsync(int staffId);
        Task<IEnumerable<Report>> GetByCategoryAsync(int categoryId);
    }
}
