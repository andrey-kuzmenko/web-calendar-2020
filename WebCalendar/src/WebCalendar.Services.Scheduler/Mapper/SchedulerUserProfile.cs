using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Notification.Models;
using WebCalendar.Services.Scheduler.Models;

namespace WebCalendar.Services.Scheduler.Mapper
{
    class SchedulerUserProfile : AutoMapperProfile
    {
        public SchedulerUserProfile()
        {
            CreateMap<User, SchedulerUser>();

            CreateMap<SchedulerUser, UserNotificationServiceModel>();
        }
    }
}
