using System;
using System.Collections.Generic;
using CorePush.Google;
using WebCalendar.Common.Contracts;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;
using WebCalendar.Services.PushNotification.Contracts;
using WebCalendar.Services.PushNotification.Models;
using Task = System.Threading.Tasks.Task;

namespace WebCalendar.Services.PushNotification.Implementation
{
    public class PushNotificationService: IPushNotificationService
    {
        private readonly IUnitOfWork _uow;
        private readonly FirebaseNotification _firebaseNotification;

        public PushNotificationService(IUnitOfWork uow, IMapper mapper, FirebaseNotification firebaseNotification)
        {
            _uow = uow;
            _firebaseNotification = firebaseNotification;
        }
        
        public async Task SendNotificationAsync(NotificationServiceModel notification, Guid userId)
        {
            IEnumerable<PushSubscription> pushSubscriptions = await _uow.GetRepository<PushSubscription>()
                .GetAllAsync(p => p.UserId == userId);

            using var fcm = new FcmSender(_firebaseNotification.ServerKey, _firebaseNotification.SenderId);
            foreach (PushSubscription pushSubscription in pushSubscriptions)
            {
                await fcm.SendAsync(pushSubscription.DeviceToken, new
                {
                    notification = new
                    {
                        title = notification.Title,
                        body = notification.Message,
                        click_action = notification.Url
                    },
                });
            }
        }

        public async Task SubscribeOnPushNotificationAsync(Guid userId, string token)
        {
            var pushSubscription = new PushSubscription
            {
                DeviceToken = token,
                UserId = userId
            };

            await _uow.GetRepository<PushSubscription>().AddAsync(pushSubscription);

            await _uow.SaveChangesAsync();
        }

        public async Task UnsubscribeFromPushNotificationAsync(Guid userId, string token)
        {
            IEnumerable<PushSubscription> pushSubscriptions = await _uow.GetRepository<PushSubscription>().GetAllAsync(
                p => p.UserId == userId && p.DeviceToken == token);

            foreach (PushSubscription pushSubscription in pushSubscriptions)
            {
                _uow.GetRepository<PushSubscription>().Remove(new PushSubscription
                {
                    Id = pushSubscription.Id
                });
            }

            await _uow.SaveChangesAsync();
        }
    }
}