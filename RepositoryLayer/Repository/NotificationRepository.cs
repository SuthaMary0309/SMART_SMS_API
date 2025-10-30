using Microsoft.EntityFrameworkCore;
using RepositoryLayer.AppDbContext;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class NotificationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NotificationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notification> AddNotification(Notification notification)
        {
            var result = await _dbContext.Notifications.AddAsync(notification);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Notification>> GetAllNotifications()
        {
            return await _dbContext.Notifications.ToListAsync();
        }

        public async Task<Notification?> GetNotificationById(Guid id)
        {
            return await _dbContext.Notifications.FindAsync(id);
        }

        public async Task<Notification?> UpdateNotification(Notification notification)
        {
            var existing = await _dbContext.Notifications.FindAsync(notification.NotificationId);
            if (existing == null) return null;

            existing.Tittle = notification.Tittle;
            existing.Type = notification.Type;
            existing.Message = notification.Message;
            existing.DateSent = notification.DateSent;
            existing.UserID = notification.UserID;
            existing.DateReceived = notification.DateReceived;
            existing.Isread = notification.Isread;


            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteNotification(Guid id)
        {
            var existing = await _dbContext.Notifications.FindAsync(id);
            if (existing == null) return false;

            _dbContext.Notifications.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
    

