using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Implementation
{
    public class SchedulerDataLoader : ISchedulerDataLoader
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public SchedulerDataLoader(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SchedulerEvent>> GetSchedulerEvents()
        {
            IEnumerable<Event> events = await _uow.GetRepository<Event>().GetAllAsync(
                include: source => source
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.CalendarUsers)
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.User)
                .Include(e => e.UserEvents)
                       .ThenInclude(ue => ue.User));
            IEnumerable<SchedulerEvent> schedulerEvents = _mapper.Map<IEnumerable<Event>, IEnumerable<SchedulerEvent>>(events);

            return schedulerEvents;
        }

        public async Task<IEnumerable<SchedulerReminder>> GetSchedulerReminders()
        {
            IEnumerable<Reminder> reminders = await _uow.GetRepository<Reminder>().GetAllAsync(include: source => source
               .Include(e => e.Calendar)
                   .ThenInclude(c => c.CalendarUsers)
               .Include(e => e.Calendar)
                   .ThenInclude(c => c.User));
            IEnumerable<SchedulerReminder> schedulerReminders = _mapper.Map<IEnumerable<Reminder>, IEnumerable<SchedulerReminder>>(reminders);

            return schedulerReminders;
        }

        public async Task<IEnumerable<SchedulerTask>> GetSchedulerTasks()
        {
            IEnumerable<DAL.Models.Entities.Task> tasks = await _uow.GetRepository<DAL.Models.Entities.Task>().GetAllAsync(
                include: source => source
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.CalendarUsers)
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.User));
            IEnumerable<SchedulerTask> schedulerTasks = _mapper.Map<IEnumerable<DAL.Models.Entities.Task>, IEnumerable<SchedulerTask>>(tasks);

            return schedulerTasks;
        }
    }
}
