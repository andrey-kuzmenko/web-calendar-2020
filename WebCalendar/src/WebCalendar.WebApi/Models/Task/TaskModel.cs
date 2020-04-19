using System;
using WebCalendar.Services.Models.Calendar;

namespace WebCalendar.WebApi.Models.Task
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public bool IsDone { get; set; }

        public Guid CalendarId { get; set; }
        public CalendarServiceModel Calendar { get; set; }
    }
}