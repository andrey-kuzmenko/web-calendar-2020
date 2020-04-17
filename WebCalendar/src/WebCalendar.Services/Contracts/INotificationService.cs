using System.Threading.Tasks;
using WebCalendar.DAL.Models;
using WebCalendar.PushNotification.Models;

namespace WebCalendar.Services.Contracts
{
    public interface INotificationService
    {
        Task SendNotificationAsync(INotificable notificableEntity, Notification type);
    }
}