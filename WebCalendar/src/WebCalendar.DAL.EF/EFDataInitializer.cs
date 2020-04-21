using System;
using System.Collections.Generic;
using System.Linq;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL.EF.Context;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF
{
    public class EFDataInitializer : IDataInitializer
    {
        private readonly ApplicationDbContext _context;

        public EFDataInitializer(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void Seed()
        {
            if (_context.Database.EnsureDeleted())
            {
                _context.Database.EnsureCreated();
                AddUser();
                AddCalendar();
                AddCalendarUser();
                AddEvent();
                AddUserEvent();
                AddTask();
                AddReminder();
            }
        }

        private void AddUser()
        {
            var users = new List<User>()
            {
                new User
                {
                    Id = new Guid(),
                    FirstName = "Sergey",
                    LastName = "Simakin",
                    Email = "sergey.simakin98@gmail.com",
                    IsSubscribedToEmailNotifications = true,
                    IsSubscribedToNativeNotifications = true
                },

                new User
                {
                    Id = new Guid(),
                    FirstName = "Mykhail",
                    LastName = "Yermolenko",
                    Email = "ermolenko1999@gmail.com",
                    IsSubscribedToEmailNotifications = true,
                    IsSubscribedToNativeNotifications = false
                },

                new User
                {
                    Id = new Guid(),
                    FirstName = "Dmitriy",
                    LastName = "Pavliuk",
                    Email = "coster730@gmail.com",
                    IsSubscribedToEmailNotifications = false,
                    IsSubscribedToNativeNotifications = true
                },
                
                new User
                {
                    Id = new Guid(),
                    FirstName = "Michael",
                    LastName = "Yermolenko",
                    Email = "mixaluch_ermolenko@mail.ru",
                    IsSubscribedToEmailNotifications = false,
                    IsSubscribedToNativeNotifications = false
                }
            };

            //_context.Users.AddRange(users);
            //_context.SaveChanges();
        }

        private void AddCalendar()
        {
            var userid = _context.Users.ToArray();

            var calendars = new List<Calendar>()
            {
                new Calendar
                {
                    Id = new Guid(),
                    Name = "Main",
                    Description = "Everyday calendar for everyday tasks",
                    UserId = userid[0].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Name = "Work",
                    Description = "Calendar for work events only",
                    UserId = userid[0].Id
                },

                new Calendar
                {
                    Id  = new Guid(),
                    Name = "Holiday",
                    Description = "Calendar with all holidays",
                    UserId = userid[3].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Name = "Personal",
                    Description = "Calendar with personal goals and objectives",
                    UserId = userid[1].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Name = "Family",
                    Description = "Calendar for the whole family",
                    UserId = userid[1].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Name = "Test",
                    Description = "Calendar for testing text display" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|",
                    UserId = userid[3].Id
                }
            };

            _context.Calendars.AddRange(calendars);
            _context.SaveChanges();
        }
        private void AddCalendarUser()
        {
            var calendarid = _context.Calendars.ToArray();
            var userid = _context.Users.ToArray();

            var calendaruser = new List<CalendarUser>()
            {
                new CalendarUser
                {
                    CalendarId = calendarid[0].Id,
                    UserId = userid[1].Id,
                },

                new CalendarUser
                {
                    CalendarId = calendarid[1].Id,
                    UserId = userid[1].Id,
                },

                new CalendarUser
                {
                    CalendarId = calendarid[1].Id,
                    UserId = userid[2].Id,
                },

                new CalendarUser
                {
                    CalendarId = calendarid[2].Id,
                    UserId = userid[0].Id,
                },

                new CalendarUser
                {
                    CalendarId = calendarid[2].Id,
                    UserId = userid[1].Id,
                },

                new CalendarUser
                {
                    CalendarId = calendarid[2].Id,
                    UserId = userid[2].Id,
                },

                new CalendarUser
                {
                    CalendarId = calendarid[4].Id,
                    UserId = userid[3].Id,
                }
            };

            _context.CalendarUsers.AddRange(calendaruser);
            _context.SaveChanges();
        }

        private void AddEvent()
        {
            var calendarid = _context.Calendars.ToArray();

            var events = new List<Event>()
            {
                new Event
                {
                    Id = new Guid(),
                    Name = "New Year",
                    Description = "Happy New Year",
                    StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    EndTime = new DateTime(2021, 1, 1, 23, 59, 59),
                    NotifyAt = new TimeSpan(),
                    NotifyBeforeInterval = new TimeSpan(),
                    RepetitionsCount = 1000,
                    CalendarId = calendarid[0].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Name = "My Birthday",
                    Description = "Party na hate",
                    StartTime = new DateTime(2020, 8, 13, 0, 0, 0),
                    EndTime = new DateTime(2100, 12, 31, 23, 59, 59),
                    NotifyAt = new TimeSpan(),
                    NotifyBeforeInterval = new TimeSpan(),
                    RepetitionsCount = 100,
                    CalendarId = calendarid[1].Id
                }
            };

            _context.Events.AddRange(events);
            _context.SaveChanges();
        }

        private void AddUserEvent()
        {
            var userid = _context.Users.ToArray();
            var eventid = _context.Events.ToArray();

            var userevent = new List<UserEvent>()
            {
                new UserEvent
                {
                    UserId = userid[1].Id,
                    EventId = eventid[0].Id
                }
            };

            _context.UserEvents.AddRange(userevent);
            _context.SaveChanges();
        }

        private void AddReminder()
        {
            var calendarid = _context.Calendars.ToArray();

            var reminders = new List<Reminder>()
            {
                new Reminder
                {
                    Id = new Guid(),
                    Name = "Courses",
                    Time = new DateTime(2020, 5, 9),
                    CalendarId = calendarid[1].Id
                }
            };

            _context.Reminders.AddRange(reminders);
            _context.SaveChanges();
        }
        private void AddTask()
        {
            var calendarid = _context.Calendars.ToArray();

            var tasks = new List<Task>()
            {
                new Task
                {
                    Id = new Guid(),
                    Name = "Become programmer",
                    Description = "#",
                    StartTime = new DateTime(2020, 5, 16),
                    IsDone = false,
                    CalendarId = calendarid[1].Id
                }
            };

            _context.Tasks.AddRange(tasks);
            _context.SaveChanges();
        }
    }
}