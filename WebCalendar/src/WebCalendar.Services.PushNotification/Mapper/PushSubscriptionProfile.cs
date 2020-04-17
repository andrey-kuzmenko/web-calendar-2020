using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.PushNotification.Models;

namespace WebCalendar.Services.PushNotification.Mapper
{
    public class PushSubscriptionProfile : AutoMapperProfile
    {
        public PushSubscriptionProfile()
        {
            CreateMap<PushSubscriptionServiceModel, PushSubscription>();
            CreateMap<PushSubscription, WebPush.PushSubscription>();
        }
    }
}