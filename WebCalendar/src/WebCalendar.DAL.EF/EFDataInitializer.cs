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
                },

                new User
                {
                    Id = new Guid(),
                    FirstName = "Mykhail",
                    LastName = "Yermolenko",
                    Email = "ermolenko1999@gmail.com",
                    IsSubscribedToEmailNotifications = true,
                },

                new User
                {
                    Id = new Guid(),
                    FirstName = "Dmitriy",
                    LastName = "Pavliuk",
                    Email = "coster730@gmail.com",
                    IsSubscribedToEmailNotifications = false,
                },
                
                new User
                {
                    Id = new Guid(),
                    FirstName = "Michael",
                    LastName = "Yermolenko",
                    Email = "mixaluch_ermolenko@mail.ru",
                    IsSubscribedToEmailNotifications = false,
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
                    Title = "Main",
                    Description = "Everyday calendar for everyday tasks",
                    UserId = userid[0].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Title = "Work",
                    Description = "Calendar for work events only",
                    UserId = userid[0].Id
                },

                new Calendar
                {
                    Id  = new Guid(),
                    Title = "Holiday",
                    Description = "Calendar with all holidays",
                    UserId = userid[3].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Title = "Personal",
                    Description = "Calendar with personal goals and objectives",
                    UserId = userid[1].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Title = "Family",
                    Description = "Calendar for the whole family",
                    UserId = userid[1].Id
                },

                new Calendar
                {
                    Id = new Guid(),
                    Title = "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|",
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
                    Title = "New Year",
                    Description = "Happy New Year",
                    StartTime = new DateTime(2021, 1, 1, 0, 0, 0),
                    EndTime = new DateTime(2021, 1, 1, 23, 59, 59),
                    RepetitionsCount = 1000,
                    CalendarId = calendarid[2].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "My Birthday",
                    Description = "Party na hate",
                    StartTime = new DateTime(2020, 8, 13, 0, 0, 0),
                    EndTime = new DateTime(2100, 12, 31, 23, 59, 59),
                    RepetitionsCount = 100,
                    CalendarId = calendarid[0].Id
                },
                
                new Event
                {
                    Id = new Guid(),
                    Title = "Project Completion",
                    Description = "We came for victory",
                    StartTime = new DateTime(2020, 4, 24, 12, 0 , 0),
                    EndTime = new DateTime(2020, 4, 24, 13, 30, 0),
                    CalendarId = calendarid[1].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Family Council",
                    Description = "",
                    StartTime = new DateTime(2020, 5, 12, 18, 0, 0),
                    EndTime = new DateTime(2020, 5, 12, 18, 30, 0),
                    CalendarId = calendarid[3].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Coding",
                    Description = "It's time to become programmer",
                    StartTime = new DateTime(2020, 4, 23, 10, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 17, 59, 59),
                    CalendarId = calendarid[4].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|",
                    Description = "Event for testing text display" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|",
                    StartTime = new DateTime(2020, 4, 23, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 24, 0, 0, 0),
                    CalendarId = calendarid[5].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event#1",
                    StartTime = new DateTime(2020, 4, 22, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 0, 0, 0),
                    CalendarId = calendarid[0].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event#1",
                    StartTime = new DateTime(2020, 4, 22, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 0, 0, 0),
                    CalendarId = calendarid[1].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event#1",
                    StartTime = new DateTime(2020, 4, 22, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 0, 0, 0),
                    CalendarId = calendarid[2].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event#1",
                    StartTime = new DateTime(2020, 4, 22, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 0, 0, 0),
                    CalendarId = calendarid[3].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event#1",
                    StartTime = new DateTime(2020, 4, 22, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 0, 0, 0),
                    CalendarId = calendarid[4].Id
                },

                new Event
                {
                    Id = new Guid(),
                    Title = "Event#1",
                    Description = "Event#1",
                    StartTime = new DateTime(2020, 4, 22, 22, 0, 0),
                    EndTime = new DateTime(2020, 4, 23, 0, 0, 0),
                    CalendarId = calendarid[5].Id
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
                    UserId = userid[0].Id,
                    EventId = eventid[4].Id
                },

                new UserEvent
                {
                    UserId = userid[2].Id,
                    EventId = eventid[4].Id
                },

                new UserEvent
                {
                    UserId = userid[3].Id,
                    EventId = eventid[4].Id
                },

                new UserEvent
                {
                    UserId = userid[0].Id,
                    EventId = eventid[0].Id
                },

                new UserEvent
                {
                    UserId = userid[1].Id,
                    EventId = eventid[0].Id
                },

                new UserEvent
                {
                    UserId = userid[2].Id,
                    EventId = eventid[0].Id
                },

                new UserEvent
                {
                    UserId = userid[3].Id,
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
                    Title = "Reminder#1",
                    StartTime = new DateTime(2020, 4, 22, 10, 0, 0),
                    CalendarId = calendarid[0].Id
                },

                new Reminder
                {
                    Id = new Guid(),
                    Title = "Reminder#1",
                    StartTime = new DateTime(2020, 4, 22, 10, 0, 0),
                    CalendarId = calendarid[1].Id
                },

                new Reminder
                {
                    Id = new Guid(),
                    Title = "Reminder#1",
                    StartTime = new DateTime(2020, 4, 22, 10, 0, 0),
                    CalendarId = calendarid[2].Id
                },

                new Reminder
                {
                    Id = new Guid(),
                    Title = "Reminder#1",
                    StartTime = new DateTime(2020, 4, 22, 10, 0, 0),
                    CalendarId = calendarid[3].Id
                },

                new Reminder
                {
                    Id = new Guid(),
                    Title = "Reminder#1",
                    StartTime = new DateTime(2020, 4, 22, 10, 0, 0),
                    CalendarId = calendarid[4].Id
                },

                new Reminder
                {
                    Id = new Guid(),
                    Title = "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|",
                    StartTime = new DateTime(2020, 4, 23, 10, 45, 27),
                    CalendarId = calendarid[5].Id
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
                    Title = "Complete the project",
                    Description = "its time to stop",
                    StartTime = new DateTime(2020, 4, 24, 11, 59, 59),
                    IsDone = false,
                    CalendarId = calendarid[1].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#1",
                    Description = "Task#1",
                    StartTime = new DateTime(2020, 4, 22, 11, 59, 57),
                    IsDone = false,
                    CalendarId = calendarid[1].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#1",
                    Description = "Task#1",
                    StartTime = new DateTime(2020, 4, 22, 11, 59, 59),
                    IsDone = false,
                    CalendarId = calendarid[2].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#1",
                    Description = "Task#1",
                    StartTime = new DateTime(2020, 4, 22, 12, 0, 05),
                    IsDone = false,
                    CalendarId = calendarid[3].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#1",
                    Description = "Task#1",
                    StartTime = new DateTime(2020, 4, 22, 12, 5, 0),
                    IsDone = false,
                    CalendarId = calendarid[4].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#1",
                    Description = "Task#1",
                    StartTime = new DateTime(2020, 4, 20, 12, 0, 0),
                    IsDone = true,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#2",
                    Description = "Task#2",
                    StartTime = new DateTime(2020, 4, 21, 12, 0, 0),
                    IsDone = true,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#3",
                    Description = "Task#3",
                    StartTime = new DateTime(2020, 4, 22, 12, 0, 0),
                    IsDone = true,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#4",
                    Description = "Task#4",
                    StartTime = new DateTime(2020, 4, 23, 12, 0, 0),
                    IsDone = true,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#5",
                    Description = "Task#5",
                    StartTime = new DateTime(2020, 4, 24, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#6",
                    Description = "Task#6",
                    StartTime = new DateTime(2020, 4, 25, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#7",
                    Description = "Task#7",
                    StartTime = new DateTime(2020, 4, 26, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#8",
                    Description = "Task#8",
                    StartTime = new DateTime(2020, 4, 27, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#9",
                    Description = "Task#9",
                    StartTime = new DateTime(2020, 4, 28, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#10",
                    Description = "Task#10",
                    StartTime = new DateTime(2020, 4, 29, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#11",
                    Description = "Task#11",
                    StartTime = new DateTime(2020, 4, 30, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#12",
                    Description = "Task#12",
                    StartTime = new DateTime(2020, 5, 1, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#13",
                    Description = "Task#13",
                    StartTime = new DateTime(2020, 5, 2, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Task#14",
                    Description = "Task#14",
                    StartTime = new DateTime(2020, 5, 3, 12, 0, 0),
                    IsDone = false,
                    CalendarId = calendarid[0].Id
                },

                new Task
                {
                    Id = new Guid(),
                    Title = "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|" +
                            "Test|Test|Test|Test|Test|Test|Test|Test|",
                    Description = "Task for testing text display" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|" +
                    "text|text|text|text|text|text|text|text|text|text|",
                    StartTime = new DateTime(2020, 4, 23, 18, 30, 0),
                    IsDone = false,
                    CalendarId = calendarid[5].Id
                }
            };

            _context.Tasks.AddRange(tasks);
            _context.SaveChanges();
        }
    }
}