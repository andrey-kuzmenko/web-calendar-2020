using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.EmailSender.Contracts;
using WebCalendar.PushNotification.Contracts;
using WebCalendar.Services.Notification.Contracts;
using WebCalendar.Services.Notification.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.Notification.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IPushNotificationSender _pushNotificationSender;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<NotificationService> _logger;

        public NotificationService(IPushNotificationSender pushNotificationSender, 
            IEmailSender emailSender, 
            IUnitOfWork uow, 
            IMapper mapper, ILogger<NotificationService> logger)
        {
            _pushNotificationSender = pushNotificationSender;
            _emailSender = emailSender;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        /*public async Task SendNotificationAsync(INotificable notificableEnity, NotificationType type)
        {
            /*User user = await _uow.GetRepository<User>().GetFirstOrDefaultAsync(
                predicate: u => u.Id == userId,
                include: users => users.Include(u => u.PushSubscriptions));
            
            await _emailSender.SendEmailAsync()#1#
            
            throw new NotImplementedException();
        }*/
        public async Task SendTaskNotificationAsync(Guid taskId, NotificationType type)
        {
            DAL.Models.Entities.Task task = await _uow.GetRepository<DAL.Models.Entities.Task>().GetFirstOrDefaultAsync(
                predicate: t => t.Id == taskId);
            
            Calendar calendar = await _uow.GetRepository<Calendar>().GetFirstOrDefaultAsync(
                predicate: c => c.Id == task.CalendarId,
                include: calendars => calendars
                    .Include(c => c.User)
                        .ThenInclude(u => u.PushSubscriptions)
                    .Include(c=> c.CalendarUsers)
                        .ThenInclude(cu => cu.User));

            List<User> usersForNotify = calendar.CalendarUsers.Select(cu => cu.User).ToList();
            usersForNotify.Add(calendar.User);

            List<string> emails = usersForNotify.Select(u => u.Email).ToList();

            List<string> deviceTokens = usersForNotify
                .SelectMany(u => u.PushSubscriptions)
                .Select(p => p.DeviceToken)
                .ToList();

            switch (type)
            {
                case NotificationType.Start:
                {
                    var pushNotificationServiceModel = new PushNotificationServiceModel
                    {
                        Title = task.Title,
                        Message = "It's time to start the task",
                        Url = "" //url to page with details
                    };
                    
                    _logger.LogInformation("push send");
                    PushNotification.Models.PushNotification pushNotification = _mapper
                        .Map<PushNotificationServiceModel, 
                            PushNotification.Models.PushNotification>(pushNotificationServiceModel);

                    //await _pushNotificationSender.SendPushNotificationAsync(deviceTokens, pushNotification);
                    string jobId = BackgroundJob.Schedule<IPushNotificationSender>(n => 
                            n.SendPushNotificationAsync(deviceTokens, pushNotification), 
                        DateTime.SpecifyKind(task.StartTime, DateTimeKind.Utc)
                    );
                    break;
                }
            }
        }

        /*public async Task SendTaskNotificationAsync(IEnumerable<string> deviceToken,
            PushNotificationServiceModel pushNotification)
        {
            _pushNotificationSender.SendPushNotificationAsync(deviceToken, )
        }*/
    }
}