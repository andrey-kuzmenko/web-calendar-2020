using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebCalendar.Services.Scheduler.Implementation;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler
{
    public static class SchedulerExtension
    {
        public static async Task ScheduleEvent(this IScheduler scheduler, SchedulerEvent @event)
        {
            if (@event.NotifyBeforeInterval != null)
            {
                await ScheduleEventInAdvance(scheduler, @event);
            }

            JobKey jobKey = new JobKey(@event.Id.ToString(), ConstantsStorage.EVENT_GROUP);
            TriggerKey triggerKey = new TriggerKey(@event.Id.ToString(), ConstantsStorage.EVENT_GROUP);

            IJobDetail job = JobBuilder.Create<NotificationJob>()
                .WithIdentity(jobKey)
                .UsingJobData(NotificationJob.JobDataKey, JsonConvert.SerializeObject(@event))
                .UsingJobData(NotificationJob.JobActivityTypeKey, ConstantsStorage.EVENT)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithCronSchedule(@event.CronExpression)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async Task UnscheduleEvent(this IScheduler scheduler, SchedulerEvent @event)
        {
            JobKey jobKey = new JobKey(@event.Id.ToString(), ConstantsStorage.EVENT_GROUP);

            await UnscheduleEventInAdvance(scheduler, @event);
            await scheduler.DeleteJob(jobKey);
        }

        public static async Task RescheduleEvent(this IScheduler scheduler, SchedulerEvent @event)
        {
            await UnscheduleEvent(scheduler, @event);
            await ScheduleEvent(scheduler, @event);
        }

        public static async Task ScheduleReminder(this IScheduler scheduler, SchedulerReminder reminder)
        {
            JobKey jobKey = new JobKey(reminder.Id.ToString(), ConstantsStorage.REMINDER_GROUP);
            TriggerKey triggerKey = new TriggerKey(reminder.Id.ToString(), ConstantsStorage.REMINDER_GROUP);

            IJobDetail job = JobBuilder.Create<NotificationJob>()
                .WithIdentity(jobKey)
                .UsingJobData(NotificationJob.JobDataKey, JsonConvert.SerializeObject(reminder))
                .UsingJobData(NotificationJob.JobActivityTypeKey, ConstantsStorage.REMINDER)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithCronSchedule(reminder.CronExpression)
                .EndAt(reminder.EndTime)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async Task UnscheduleReminder(this IScheduler scheduler, SchedulerReminder reminder)
        {
            JobKey jobKey = new JobKey(reminder.Id.ToString(), ConstantsStorage.REMINDER_GROUP);

            await scheduler.DeleteJob(jobKey);
        }

        public static async Task RescheduleReminder(this IScheduler scheduler, SchedulerReminder reminder)
        {
            await UnscheduleReminder(scheduler, reminder);
            await ScheduleReminder(scheduler, reminder);
        }

        public static async Task ScheduleTask(this IScheduler scheduler, SchedulerTask task)
        {
            JobKey jobKey = new JobKey(task.Id.ToString(), ConstantsStorage.TASK_GROUP);
            TriggerKey triggerKey = new TriggerKey(task.Id.ToString(), ConstantsStorage.TASK_GROUP);

            IJobDetail job = JobBuilder.Create<NotificationJob>()
                .WithIdentity(jobKey)
                .UsingJobData(NotificationJob.JobDataKey, JsonConvert.SerializeObject(task))
                .UsingJobData(NotificationJob.JobActivityTypeKey, ConstantsStorage.TASK)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(task.StartTime)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        public static async Task UnscheduleTask(this IScheduler scheduler, SchedulerTask task)
        {
            JobKey jobKey = new JobKey(task.Id.ToString(), ConstantsStorage.TASK_GROUP);

            await scheduler.DeleteJob(jobKey);
        }

        public static async Task RescheduleTask(this IScheduler scheduler, SchedulerTask task)
        {
            await UnscheduleTask(scheduler, task);
            await ScheduleTask(scheduler, task);
        }

        private static async Task ScheduleEventInAdvance(this IScheduler scheduler, SchedulerEvent @event)
        {
            JobKey jobKey = new JobKey(@event.Id.ToString(), ConstantsStorage.ADVANCE_EVENT_GROUP);
            TriggerKey triggerKey = new TriggerKey(@event.Id.ToString(), ConstantsStorage.EVENT_GROUP);
            DateTime startTime = new DateTime(@event.StartTime.Ticks - @event.NotifyBeforeInterval.Value.Ticks);

            IJobDetail job = JobBuilder.Create<NotificationJob>()
                .WithIdentity(jobKey)
                .UsingJobData(NotificationJob.JobDataKey, JsonConvert.SerializeObject(@event))
                .UsingJobData(NotificationJob.JobActivityTypeKey, ConstantsStorage.ADVANCE_EVENT)
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .StartAt(startTime)
                .Build();

            await scheduler.ScheduleJob(job, trigger);
        }

        private static async Task UnscheduleEventInAdvance(this IScheduler scheduler, SchedulerEvent @event)
        {
            JobKey jobKey = new JobKey(@event.Id.ToString(), ConstantsStorage.ADVANCE_EVENT_GROUP);

            await scheduler.DeleteJob(jobKey);
        }
    }
}
