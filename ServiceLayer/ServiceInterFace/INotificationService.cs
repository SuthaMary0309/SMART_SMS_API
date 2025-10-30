using RepositoryLayer.Entity;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceInterFace
{
    public interface INotificationService
    {
        Task<IEnumerable<Notification>> GetAllNotificationsAsync();
        Task<Notification?> GetNotificationByIdAsync(Guid id);
        Task<Notification> AddNotificationAsync(NotificationRequestDTO request);
        Task<Notification?> UpdateNotificationAsync(Guid id, NotificationRequestDTO request);
        Task<bool> DeleteNotificationAsync(Guid id);
    }
}
