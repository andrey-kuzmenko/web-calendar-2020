using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using WebCalendar.Services.Notification.Contracts;
using WebCalendar.Services.Notification.Models;
using WebCalendar.Services.Scheduler.Contracts;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Implementation
{
    [DisallowConcurrentExecution]
    public class NotificationJob : IJob
    {
        private readonly ILogger<NotificationJob> _logger;
        //private readonly INotificationService _notificationService;
        public static readonly string JobDataKey = "key1";
        public static readonly string JobActivityTypeKey = "key2";

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public NotificationJob(ILogger<NotificationJob> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            //_notificationService = notificationService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task Execute(IJobExecutionContext context)
        {
            JobDataMap jobDataMap = context.JobDetail.JobDataMap;

            string value = jobDataMap.GetString(JobDataKey);
            string activityType = jobDataMap.GetString(JobActivityTypeKey);

            //var schedulerActivity = JsonConvert.DeserializeObject<SchedulerActivity>(value);

            _logger.LogInformation("job doing");
            
            switch (activityType)
            {
                case ConstantsStorage.TASK:
                {
                    _logger.LogInformation("job done");
                    using IServiceScope scope = _serviceScopeFactory.CreateScope();
                    
                    var notificationService = scope.ServiceProvider.GetService<INotificationService>();
                        
                    //notificationService.SendTaskNotificationAsync(schedulerActivity.Id, NotificationType.Start);
                    break;
                }

                case ConstantsStorage.EVENT:
                {
                    break;
                }

                case ConstantsStorage.REMINDER:
                {
                    break;
                }
            }
            

            return Task.CompletedTask;
        }
    }
}