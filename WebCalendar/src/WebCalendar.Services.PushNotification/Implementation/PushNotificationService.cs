using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.PushNotification.Contracts;
using WebCalendar.Services.PushNotification.Models;
using WebPush;
using PushSubscription = WebCalendar.DAL.Models.Entities.PushSubscription;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.PushNotification.Implementation
{
    public class PushNotificationService: IPushNotificationService
    {
        private readonly IUnitOfWork _uow;
        private readonly WebPushClient _webPushClient;
        private readonly IMapper _mapper;

        public PushNotificationService(WebPushClient webPushClient, IUnitOfWork uow, IMapper mapper, VapidDetails vapidDetails)
        {
            _webPushClient = webPushClient;
            _uow = uow;
            _mapper = mapper;
            
            WebPush.VapidDetails details = _mapper.Map<VapidDetails, WebPush.VapidDetails>(vapidDetails);
            _webPushClient.SetVapidDetails(details);
        }

        public async Task SendNotificationAsync(NotificationServiceModel notificationService, Guid userId)
        {
            PushSubscription userPushSubscription = await _uow.GetRepository<User>().GetFirstOrDefaultAsync(
                selector: u => u.PushSubscription,
                predicate: u => u.Id == userId);

            WebPush.PushSubscription pushSubscription = _mapper
                .Map<PushSubscription, WebPush.PushSubscription>(userPushSubscription);

            string serializedNotification = JsonConvert.SerializeObject(notificationService);

            await _webPushClient.SendNotificationAsync(pushSubscription, serializedNotification);
        }

        public async Task SubscribeOnPushNotificationAsync(Guid userId, PushSubscriptionServiceModel pushSubscriptionServiceModel)
        {
            User user = await _uow.GetRepository<User>().GetByIdAsync(userId);

            PushSubscription pushSubscription = _mapper.Map<PushSubscriptionServiceModel, PushSubscription>(pushSubscriptionServiceModel);

            user.PushSubscription = pushSubscription;

            _uow.GetRepository<User>().Update(user);
            await _uow.SaveChangesAsync();
        }

        public async Task UnsubscribeFromPushNotificationAsync(Guid userId)
        {
            User user = await _uow.GetRepository<User>().GetFirstOrDefaultAsync(
                predicate: u => u.Id == userId,
                include: query => 
                    query.Include(u => u.PushSubscription));
            
            _uow.GetRepository<PushSubscription>().Remove(new PushSubscription
            {
                Id = user.PushSubscription.Id
            });
            
            await _uow.SaveChangesAsync();
        }
    }
}