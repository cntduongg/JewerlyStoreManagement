using JewelryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public interface IGoldPriceRepository : IRepositoryBase<Goldprice>
    {
        Task<Goldprice?> GetLatestByTypeAsync(string goldType);
        Task<IEnumerable<Goldprice>> GetByDateAsync(DateOnly date);
        Task<IEnumerable<Goldprice>> GetRecentPricesAsync(int days = 7);
    }
}
