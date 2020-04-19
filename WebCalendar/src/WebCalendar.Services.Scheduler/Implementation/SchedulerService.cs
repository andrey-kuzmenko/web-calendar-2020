using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.DAL.Repositories.Contracts;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Scheduler.Implementation
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IAsyncRepository<DAL.Models.Entities.Task> _taskRepository;
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IAsyncRepository<Reminder> _reminderRepository;
        private readonly IMapper _mapper;
        private readonly IScheduler _scheduler;
        
        public SchedulerService(IUnitOfWork uow, IMapper mapper, IQuartzHostedService quartzService)
        {
            _taskRepository = uow.GetRepository<DAL.Models.Entities.Task>();
            _eventRepository = uow.GetRepository<Event>();
            _reminderRepository = uow.GetRepository<Reminder>();

            _mapper = mapper;

            _scheduler = quartzService.Scheduler;
        }

        public async Task ScheduleTaskById(Guid id)
        {
            SchedulerTask schedulerTask = await GetTask(id);

            await _scheduler.ScheduleTask(schedulerTask);         
        }

        public async Task UnscheduleTaskById(Guid id)
        {
            SchedulerTask schedulerTask = await GetTask(id);

            await _scheduler.UnscheduleTask(schedulerTask);
        }

        public async Task RescheduleTaskById(Guid id)
        {
            SchedulerTask schedulerTask = await GetTask(id);

            await _scheduler.RescheduleTask(schedulerTask);
        }

        public async Task UnscheduleEventById(Guid id)
        {
            SchedulerEvent schedulerEvent = await GetEvent(id);

            await _scheduler.UnscheduleEvent(schedulerEvent);
        }

        public async Task RescheduleEventById(Guid id)
        {
            SchedulerEvent schedulerEvent = await GetEvent(id);

            await _scheduler.RescheduleEvent(schedulerEvent);
        }

        public async Task ScheduleEventById(Guid id)
        {
            SchedulerEvent schedulerEvent = await GetEvent(id);

            await _scheduler.ScheduleEvent(schedulerEvent);
        }

        public async Task ScheduleReminderById(Guid id)
        {
            SchedulerReminder schedulerReminder = await GetReminder(id);

            await _scheduler.ScheduleReminder(schedulerReminder);
        }

        public async Task UnscheduleReminderById(Guid id)
        {
            SchedulerReminder schedulerReminder = await GetReminder(id);

            await _scheduler.UnscheduleReminder(schedulerReminder);
        }

        public async Task RescheduleReminderById(Guid id)
        {
            SchedulerReminder schedulerReminder = await GetReminder(id);

            await _scheduler.RescheduleReminder(schedulerReminder);
        }

        private async Task<SchedulerTask> GetTask(Guid id)
        {
            var task = await _taskRepository.GetFirstOrDefaultAsync(
                predicate: t => t.Id == id,
                include: source => source
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.CalendarUsers)
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.User));

            SchedulerTask schedulerTask = _mapper.Map<DAL.Models.Entities.Task, SchedulerTask>(task);

            return schedulerTask;
        }

        private async Task<SchedulerEvent> GetEvent(Guid id)
        {
            var @event = await _eventRepository.GetFirstOrDefaultAsync(
                predicate: t => t.Id == id,
                include: source => source
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.CalendarUsers)
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.User)
                .Include(e => e.UserEvents)
                       .ThenInclude(ue => ue.User));

            SchedulerEvent schedulerEvent = _mapper.Map<Event, SchedulerEvent>(@event);

            return schedulerEvent;
        }

        private async Task<SchedulerReminder> GetReminder(Guid id)
        {
            var reminder = await _reminderRepository.GetFirstOrDefaultAsync(
                predicate: t => t.Id == id,
                include: source => source
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.CalendarUsers)
                .Include(e => e.Calendar)
                    .ThenInclude(c => c.User));

            SchedulerReminder schedulerReminder = _mapper.Map<Reminder, SchedulerReminder>(reminder);

            return schedulerReminder;
        }
    }
}
