using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.RepositoryInterface
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotification(Notification notification);
        Task<IEnumerable<Notification>> GetAllNotifications();
        Task<Notification?> GetNotificationById(Guid id);
        Task<Notification?> UpdateNotification(Notification notification);
        Task<bool> DeleteNotication(Guid id);
        Task<bool> DeleteNotification(Guid id);
    }
}
