using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        public async Task SendTaskNotificationAsync(TaskNotificationServiceModel task, NotificationType type)
        {
            //IEnumerable<string> emails = usersForNotify.Select(u => u.Email);

            IEnumerable<string> deviceTokens = task.Users
                .SelectMany(u => u.PushSubscriptions)
                .Select(p => p.DeviceToken);

            switch (type)
            {
                case NotificationType.Start:
                {
                    var pushNotificationServiceModel = new PushNotificationServiceModel
                    {
                        Title = task.Title,
                        Message = "Time to start doing task",
                        Url = "" //url to page with details
                    };
                    
                    _logger.LogInformation("push send");
                    PushNotification.Models.Notification pushNotification = _mapper
                        .Map<PushNotificationServiceModel, 
                            PushNotification.Models.Notification>(pushNotificationServiceModel);

                    await _pushNotificationSender.SendPushNotificationAsync(deviceTokens, pushNotification);
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