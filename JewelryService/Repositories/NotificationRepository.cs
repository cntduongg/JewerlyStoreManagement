using JewelryService.Models;
using JewelryService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JewelryService.Repositories.Implementations
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(DbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Notification>> GetUnreadByUserAsync(int userId)
        {
            return await _dbSet
                .Where(n => n.Userid == userId && (n.Isread == false || n.Isread == null))
                .OrderByDescending(n => n.Createdat)
                .ToListAsync();
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            var notification = await _dbSet.FindAsync(notificationId);
            if (notification != null)
            {
                notification.Isread = true;
                notification.Readat = DateTime.UtcNow;
                _dbSet.Update(notification);
            }
        }

        public async Task<IEnumerable<Notification>> GetByTypeAsync(string type)
        {
            return await _dbSet
                .Where(n => n.Notificationtype == type)
                .OrderByDescending(n => n.Createdat)
                .ToListAsync();
        }
    }
}
