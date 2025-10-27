using JewelryService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JewelryService.Data.Repositories
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        Task<Category?> GetByCodeAsync(string code);
        Task<IEnumerable<Category>> GetActiveCategoriesAsync();
        Task<bool> IsCodeTakenAsync(string code);
    }
}
