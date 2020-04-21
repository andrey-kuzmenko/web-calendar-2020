using System.Collections.Generic;
using System.Threading.Tasks;
using WebCalendar.PushNotification.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.PushNotification.Contracts
{
    public interface IPushNotificationSender
    {
        Task<bool> SendPushNotificationAsync(IEnumerable<string> deviceTokens,
            Models.PushNotification pushNotification);
    }
}