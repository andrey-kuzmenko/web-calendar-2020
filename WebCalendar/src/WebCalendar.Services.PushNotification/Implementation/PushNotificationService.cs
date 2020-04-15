using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
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
            IEnumerable<PushSubscription> repositoryPushSubscriptions = await _uow.GetRepository<PushSubscription>()
                .GetAllAsync(p => p.UserId == userId);

            IEnumerable<WebPush.PushSubscription> pushSubscriptions = _mapper
                .Map<IEnumerable<PushSubscription>, IEnumerable<WebPush.PushSubscription>>(repositoryPushSubscriptions);

            string serializedNotification = JsonConvert.SerializeObject(notificationService);

            foreach (WebPush.PushSubscription pushSubscription in pushSubscriptions)
            {
                await _webPushClient.SendNotificationAsync(pushSubscription, serializedNotification);
            }
        }

        public async Task<Guid> SubscribeOnPushNotificationAsync(Guid userId, PushSubscriptionServiceModel pushSubscriptionServiceModel)
        {
            PushSubscription pushSubscription = _mapper
                .Map<PushSubscriptionServiceModel, PushSubscription>(pushSubscriptionServiceModel);

            pushSubscription.UserId = userId;

            await _uow.GetRepository<PushSubscription>().AddAsync(pushSubscription);

            await _uow.SaveChangesAsync();

            return pushSubscription.Id;
        }

        public async Task UnsubscribeFromPushNotificationAsync(Guid pushId)
        {
            _uow.GetRepository<PushSubscription>().Remove(new PushSubscription
            {
                Id = pushId
            });

            await _uow.SaveChangesAsync();
        }
    }
}