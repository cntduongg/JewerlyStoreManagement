using JewelryService.Models;

namespace JewelryService.Repositories.Interfaces
{
    public interface INotificationRepository : IRepositoryBase<Notification>
    {
        // Các hàm đặc thù cho Notification
        Task<IEnumerable<Notification>> GetUnreadByUserAsync(int userId);
        Task MarkAsReadAsync(int notificationId);
        Task<IEnumerable<Notification>> GetByTypeAsync(string type);
    }
}
