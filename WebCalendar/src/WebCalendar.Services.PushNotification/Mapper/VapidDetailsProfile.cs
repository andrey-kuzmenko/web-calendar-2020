using WebCalendar.Common;

namespace WebCalendar.Services.PushNotification.Mapper
{
    public class VapidDetailsProfile : AutoMapperProfile
    {
        public VapidDetailsProfile()
        {
            CreateMap<VapidDetails, WebPush.VapidDetails>();
        }
    }
}