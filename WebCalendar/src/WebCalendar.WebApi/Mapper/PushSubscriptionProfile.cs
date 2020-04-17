using WebCalendar.Common;
using WebCalendar.WebApi.Models;

namespace WebCalendar.WebApi.Mapper
{
    public class PushSubscriptionProfile : AutoMapperProfile
    {
        public PushSubscriptionProfile()
        {
            CreateMap<PushSubscriptionModel, Services.PushNotification.Models.PushSubscriptionServiceModel>();
        }
    }
}