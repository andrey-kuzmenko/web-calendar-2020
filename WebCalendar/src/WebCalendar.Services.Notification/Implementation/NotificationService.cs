using System;
using System.Threading.Tasks;
using WebCalendar.DAL.Models;
using WebCalendar.EmailSender.Contracts;
using WebCalendar.PushNotification.Contracts;
using WebCalendar.Services.Notification.Contracts;
using WebCalendar.Services.Notification.Models;

namespace WebCalendar.Services.Notification.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IPushNotificationSender _pushNotificationSender;
        private readonly IEmailSender _emailSender;

        public NotificationService(IPushNotificationSender pushNotificationSender, IEmailSender emailSender)
        {
            _pushNotificationSender = pushNotificationSender;
            _emailSender = emailSender;
        }

        public async Task SendNotificationAsync(INotificable notificableEnity, NotificationType type)
        {
            /*User user = await _uow.GetRepository<User>().GetFirstOrDefaultAsync(
                predicate: u => u.Id == userId,
                include: users => users.Include(u => u.PushSubscriptions));
            
            await _emailSender.SendEmailAsync()*/
            
            throw new NotImplementedException();
        }
    }
}