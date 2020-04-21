using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Mapper
{
    public class SchedulerPushSubscriptionProfile : AutoMapperProfile
    {
        public SchedulerPushSubscriptionProfile()
        {
            CreateMap<PushSubscription, SchedulerPushSubscription>();
        }
    }
}