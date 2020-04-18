using System;
using System.Collections.Generic;
using System.Text;

namespace WebCalendar.DAL.Models
{
    public interface IRepeatableActivity : IActivity
    {
        int? RepetitionsCount { get; set; }

        ISet<int> DaysOfWeek { get; set; }
        ISet<int> DaysOfMounth { get; set; }
        ISet<int> Monthes { get; set; }
        ISet<int> Years { get; set; }
    }
}
