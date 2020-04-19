using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Contracts;

namespace WebCalendar.Services.Scheduler.Models
{
    public class SchedulerReminder : ISchedulerRepeatableActivity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CronExpression { get; set; }
        public int? RepetitionsCount { get; set; }

        public ICollection<SchedulerUser> Users { get; set; }
    }
}
