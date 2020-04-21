using System;

namespace WebCalendar.Services.Scheduler.Models
{
    public class SchedulerPushSubscription
    {
        public Guid Id { get; set; }
        
        public string DeviceToken { get; set; }
    }
}