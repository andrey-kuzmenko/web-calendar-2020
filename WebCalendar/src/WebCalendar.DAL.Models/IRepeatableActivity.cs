using System;
using System.Collections.Generic;
using System.Text;

namespace WebCalendar.DAL.Models
{
    public interface IRepeatableActivity : IActivity
    {
        public DateTime EndTime { get; set; }
        public TimeSpan NotifyAt { get; set; }
        public int? RepetitionsCount { get; set; }

        public ICollection<int> DaysOfWeek { get; set; }
        public ICollection<int> DaysOfMounth { get; set; }
        public ICollection<int> Monthes { get; set; }
        public ICollection<int> Years { get; set; }
    }
}
