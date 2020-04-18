using System;
using System.Collections.Generic;
using WebCalendar.DAL.Models;

namespace WebCalendar.Services.Models.Reminder
{
    public class ReminderEditionServiceModel : IRepeatableActivity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int? RepetitionsCount { get; set; }
        public ISet<int> DaysOfWeek { get; set; }
        public ISet<int> DaysOfMounth { get; set; }
        public ISet<int> Monthes { get; set; }
        public ISet<int> Years { get; set; }
    }
}