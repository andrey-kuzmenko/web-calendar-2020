using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Contracts;

namespace WebCalendar.Services.Scheduler.Models
{
    public class SchedulerTask : ISchedulerActivity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }

        public ICollection<SchedulerUser> Users { get; set; }
    }
}
