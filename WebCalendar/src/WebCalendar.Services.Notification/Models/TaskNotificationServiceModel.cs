using System;
using System.Collections.Generic;

namespace WebCalendar.Services.Notification.Models
{
    public class TaskNotificationServiceModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }

        public ICollection<UserNotificationServiceModel> Users { get; set; }
    }
}