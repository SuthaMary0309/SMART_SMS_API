using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class Notification
    {
        public Guid NotificationId { get; set; } = Guid.Empty;
        public string Tittle { get; set; } = string.Empty;
        public int Type { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime DateSent { get; set; }
        public DateTime DateReceived { get; set; }
        public bool Isread { get; set; }
        public Guid UserID { get; set; } = Guid.Empty;
    }
}
