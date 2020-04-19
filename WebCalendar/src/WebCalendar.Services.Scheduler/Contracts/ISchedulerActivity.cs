using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Contracts
{
    public interface ISchedulerActivity
    {
        public Guid Id { get; set; }
        public DateTime StartTime { get; set; }
        public ICollection<SchedulerUser> Users { get; set;}
    }
}
