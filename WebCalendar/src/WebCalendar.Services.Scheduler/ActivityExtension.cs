using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.Services.Scheduler
{
    public static class ActivityExtension
    {
        public static IEnumerable<User> GetUsers(this Event @event)
        {
            List<User> users = new List<User>();

            users.Add(@event.Calendar.User);
            users.AddRange(@event.Calendar.CalendarUsers.Select(cu => cu.User));
            users.AddRange(@event.UserEvents.Select(ue => ue.User));

            return users;
        }

        public static IEnumerable<User> GetUsers(this Reminder reminder)
        {
            List<User> users = new List<User>();

            users.Add(reminder.Calendar.User);
            users.AddRange(reminder.Calendar.CalendarUsers.Select(cu => cu.User));

            return users;
        }

        public static IEnumerable<User> GetUsers(this Task reminder)
        {
            List<User> users = new List<User>();

            users.Add(reminder.Calendar.User);
            users.AddRange(reminder.Calendar.CalendarUsers.Select(cu => cu.User));

            return users;
        }
    }
}
