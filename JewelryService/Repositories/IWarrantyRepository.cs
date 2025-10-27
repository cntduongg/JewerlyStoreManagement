using JewelryService.Models;

namespace JewelryService.Repositories.Interfaces
{
    public interface IWarrantyRepository : IRepositoryBase<Warranty>
    {
        // 🧩 Có thể thêm hàm chuyên biệt nếu cần, ví dụ:
        Task<IEnumerable<Warranty>> GetActiveWarrantiesAsync();
        Task<IEnumerable<Warranty>> GetWarrantiesByCustomerIdAsync(int customerId);
    }
}
