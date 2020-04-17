using System;
using System.Threading.Tasks;
using WebCalendar.DAL.Models;
using WebCalendar.PushNotification.Models;
using WebCalendar.Services.Contracts;

namespace WebCalendar.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        public async Task SendNotificationAsync(INotificable notificableEnity, Notification type)
        {
            /*User user = await _uow.GetRepository<User>().GetFirstOrDefaultAsync(
                predicate: u => u.Id == userId,
                include: users => users.Include(u => u.PushSubscriptions));
            
            await _emailSender.SendEmailAsync()*/
            
            throw new NotImplementedException();
        }
    }
}