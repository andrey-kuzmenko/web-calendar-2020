using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using WebCalendar.Services.Models.Calendar;
using WebCalendar.Services.Models.Event;

namespace WebCalendar.Services.Models.User
{
    public class UserServiceModel : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsSubscribedToNativeNotifications { get; set; } = true;
        public bool IsSubscribedToEmailNotifications { get; set; }

        public ICollection<CalendarServiceModel> SharedCalendars { get; set; }
        public ICollection<CalendarServiceModel> Calendars { get; set; }
        public ICollection<EventServiceModel> SharedEvents { get; set; }
    }
}