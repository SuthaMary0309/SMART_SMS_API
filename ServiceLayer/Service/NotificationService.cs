using RepositoryLayer.Entity;
using RepositoryLayer.RepoInterFace;
using RepositoryLayer.Repository;
using RepositoryLayer.RepositoryInterface;
using ServiceLayer.DTO.RequestDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Service
{
    public class NotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        // 🟣 Add new Notification
        public async Task<Notification> AddNotificationAsync(NotificationRequestDTO request)
        {
            var notification = new Notification
            {
                NotificationId = Guid.NewGuid(),
                Tittle = request.Tittle,
                Type = request.Type,
                Message = request.Message,
                DateSent = request.DateSent,
                UserID = request.UserID,
                DateReceived = request.DateReceived,
                Isread = request.Isread,
            };

            return await _notificationRepository.AddNotification(notification);
        }

        // 🟢 Get all Notification
        public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
        {
            return await _notificationRepository.GetAllNotifications();
        }

        // 🟡 Get Notification by ID
        public async Task<Notification?> GetNotificationByIdAsync(Guid id)
        {
            return await _notificationRepository.GetNotificationById(id);
        }

        // 🔵 Update Notification
        public async Task<Notification?> UpdateNotificationAsync(Guid id, NotificationRequestDTO request)
        {
            var existing = await _notificationRepository.GetNotificationById(id);
            if (existing == null) return null;

            existing.Tittle = request.Tittle;
            existing.Type = request.Type;
            existing.Message = request.Message;
            existing.DateSent = request.DateSent;
            existing.UserID = request.UserID;
            existing.DateReceived = request.DateReceived;
            existing.Isread = request.Isread;

            return await _notificationRepository.UpdateNotification(existing);
        }

        // 🔴 Delete Notification
        public async Task<bool> DeleteNotificationAsync(Guid id)
        {
            return await _notificationRepository.DeleteNotification(id);
        }
    }
}

    

