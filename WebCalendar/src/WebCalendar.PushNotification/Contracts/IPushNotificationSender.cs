using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.PushNotification.Contracts
{
    public interface IPushNotificationSender
    {
        Task SendPushNotificationAsync(IEnumerable<string> deviceTokens,
            Models.PushNotification pushNotification);
    }
}