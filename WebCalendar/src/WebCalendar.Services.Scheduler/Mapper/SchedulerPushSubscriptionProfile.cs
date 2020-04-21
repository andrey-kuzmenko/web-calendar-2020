using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Notification.Models;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Mapper
{
    public class SchedulerPushSubscriptionProfile : AutoMapperProfile
    {
        public SchedulerPushSubscriptionProfile()
        {
            CreateMap<PushSubscription, SchedulerPushSubscription>();

            CreateMap<SchedulerPushSubscription, PushSubscriptionNotificationServiceModel>();
        }
    }
}