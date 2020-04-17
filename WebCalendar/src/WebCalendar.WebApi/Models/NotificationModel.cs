using System;

namespace WebCalendar.WebApi.Models
{
    [Obsolete("for test")]
    public class NotificationModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
    }
}