using JewelryService.Models;

namespace JewelryService.Repositories.Interfaces
{
    public interface IPromotionRepository : IRepositoryBase<Promotion>
    {
        Task<IEnumerable<Promotion>> GetActivePromotionsAsync();
        Task<IEnumerable<Promotion>> GetByProductAsync(int productId);
        Task<IEnumerable<Promotion>> GetByCategoryAsync(int categoryId);
    }
}
