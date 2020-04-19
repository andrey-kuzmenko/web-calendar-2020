using System;
using System.Collections.Generic;
using System.Text;

namespace WebCalendar.Services.Scheduler.Contracts
{
    public interface ISchedulerRepeatableActivity : ISchedulerActivity
    {
        public DateTime EndTime { get; set; }
        public string CronExpression { get; set; }
        public int? RepetitionsCount { get; set; }
    }
}
