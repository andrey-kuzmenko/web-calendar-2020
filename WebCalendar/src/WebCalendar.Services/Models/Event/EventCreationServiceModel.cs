using System;
using System.Collections.Generic;
using WebCalendar.DAL.Models;
using WebCalendar.Services.Models.User;

namespace WebCalendar.Services.Models.Event
{
    public class EventCreationServiceModel : IRepeatableActivity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan? NotifyBeforeInterval { get; set; }
        public int? RepetitionsCount { get; set; }

        public Guid CalendarId { get; set; }
        public ICollection<UserSummaryModel> SubscribedUsers { get; set; }
        public TimeSpan NotifyAt { get; set; }
        public ICollection<int> DaysOfWeek { get; set; }
        public ICollection<int> DaysOfMounth { get; set; }
        public ICollection<int> Monthes { get; set; }
        public ICollection<int> Years { get; set; }
    }
}