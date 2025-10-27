using JewelryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public interface ICounterRepository : IRepositoryBase<Counter>
    {
        Task<Counter?> GetByCodeAsync(string code);
        Task<IEnumerable<Counter>> GetActiveCountersAsync();
        Task<bool> IsCodeTakenAsync(string code);
    }
}
