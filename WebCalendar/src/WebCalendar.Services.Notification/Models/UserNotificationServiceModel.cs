using System;
using System.Collections.Generic;

namespace WebCalendar.Services.Notification.Models
{
    public class UserNotificationServiceModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public ICollection<PushSubscriptionNotificationServiceModel> PushSubscriptions { get; set; }
    }
}