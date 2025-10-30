using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ServiceInterFace;
using System;
using System.Threading.Tasks;

namespace SMART_SMS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // 🟣 Add new Notification (parameters-based)
        [HttpPost("add")]
        public async Task<IActionResult> AddNotification(string title, int type, string message, DateTime dateSent, DateTime dateReceived, bool isRead, Guid userID)
        {
            var notificationDto = new ServiceLayer.DTO.RequestDTO.NotificationRequestDTO
            {
                Tittle = title,
                Type = type,
                Message = message,
                DateSent = dateSent,
                DateReceived = dateReceived,
                Isread = isRead,
                UserID = userID
            };

            var notification = await _notificationService.AddNotificationAsync(notificationDto);
            return Ok(notification);
        }

        // 🟢 Get all Notifications
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllNotifications()
        {
            var notifications = await _notificationService.GetAllNotificationsAsync();
            return Ok(notifications);
        }

        // 🟡 Get Notification by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetNotificationById(Guid id)
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            if (notification == null)
                return NotFound(new { message = "Notification not found" });

            return Ok(notification);
        }

        // 🔵 Update Notification (parameters-based)
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateNotification(Guid id, string title, int type, string message, DateTime dateSent, DateTime dateReceived, bool isRead, Guid userID)
        {
            var notificationDto = new ServiceLayer.DTO.RequestDTO.NotificationRequestDTO
            {
                Tittle = title,
                Type = type,
                Message = message,
                DateSent = dateSent,
                DateReceived = dateReceived,
                Isread = isRead,
                UserID = userID
            };

            var updated = await _notificationService.UpdateNotificationAsync(id, notificationDto);
            if (updated == null)
                return NotFound(new { message = "Notification not found" });

            return Ok(updated);
        }

        // 🔴 Delete Notification
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteNotification(Guid id)
        {
            var deleted = await _notificationService.DeleteNotificationAsync(id);
            if (!deleted)
                return NotFound(new { message = "Notification not found or already deleted" });

            return Ok(new { message = "Notification deleted successfully" });
        }
    }
}
