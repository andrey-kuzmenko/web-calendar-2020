using System;
using System.Threading.Tasks;
using WebCalendar.Services.PushNotification.Models;

namespace WebCalendar.Services.PushNotification.Contracts
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(NotificationServiceModel notification, Guid userId);
        Task SubscribeOnPushNotificationAsync(Guid userId, string token);
        Task UnsubscribeFromPushNotificationAsync(Guid userId, string token);

    }
}