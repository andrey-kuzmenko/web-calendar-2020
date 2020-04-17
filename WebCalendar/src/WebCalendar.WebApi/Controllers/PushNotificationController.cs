using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;
using WebCalendar.Services.PushNotification.Contracts;
using WebCalendar.Services.PushNotification.Models;
using WebCalendar.WebApi.Models;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PushNotificationController: ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUserService _userService;

        public PushNotificationController(IPushNotificationService pushNotificationService, IUserService userService)
        {
            _pushNotificationService = pushNotificationService;
            _userService = userService;
        }

        [HttpPost("subscribe/{userId}")]
        public async Task<IActionResult> Subscribe(Guid userId, [FromBody] PushSubscriptionModel pushSubscriptionModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            await _pushNotificationService.SubscribeOnPushNotificationAsync(user.Id, pushSubscriptionModel.DeviceToken);

            return Ok();
        }

        [HttpPost("unsubscribe/{userId}")]
        public async Task<IActionResult> Unsubscribe(Guid userId, [FromBody] PushSubscriptionModel pushSubscriptionModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            await _pushNotificationService.UnsubscribeFromPushNotificationAsync(user.Id, pushSubscriptionModel.DeviceToken);

            return Ok();
        }

        [HttpPost("test/{userId}")]
        [AllowAnonymous]
        [Obsolete("for test")]
        public async Task<ActionResult> Push(Guid userId, [FromBody] NotificationServiceModel notificationModel)
        {
            await _pushNotificationService.SendNotificationAsync(notificationModel, userId);

            return Ok();
        }
    }
}