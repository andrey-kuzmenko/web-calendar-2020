using System;

namespace WebCalendar.Services.Notification.Models
{
    public class PushSubscriptionNotificationServiceModel
    {
        public Guid Id { get; set; }
        
        public string DeviceToken { get; set; }
    }
}