using System;
using System.Threading.Tasks;
using WebCalendar.Services.PushNotification.Models;

namespace WebCalendar.Services.PushNotification.Contracts
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(NotificationServiceModel notificationService, Guid userId);
        Task SubscribeOnPushNotificationAsync(Guid userId, PushSubscriptionServiceModel pushSubscriptionServiceModel);
        Task UnsubscribeFromPushNotificationAsync(Guid userId);
    }
}