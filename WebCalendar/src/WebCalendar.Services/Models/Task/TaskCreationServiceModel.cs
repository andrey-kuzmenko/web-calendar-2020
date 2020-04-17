using System;
using WebCalendar.DAL.Models;

namespace WebCalendar.Services.Models.Task
{
    public class TaskCreationServiceModel : IActivity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }

        public Guid CalendarId { get; set; }
    }
}