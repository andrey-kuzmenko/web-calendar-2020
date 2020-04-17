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
    public class NotificationController: ControllerBase
    {
        private readonly IPushNotificationService _pushNotificationService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public NotificationController(IPushNotificationService pushNotificationService, IUserService userService, IMapper mapper)
        {
            _pushNotificationService = pushNotificationService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("subscribe/{userId}")]
        public async Task<IActionResult> Subscribe(string userId, [FromBody] PushSubscriptionModel pushSubscription)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id.ToString() != userId)
            {
                return Unauthorized();
            }

            PushSubscriptionServiceModel pushSubscriptionServiceModel = _mapper
                .Map<PushSubscriptionModel, PushSubscriptionServiceModel>(pushSubscription);

            await _pushNotificationService.SubscribeOnPushNotificationAsync(user.Id, pushSubscriptionServiceModel);

            return Ok();
        }

        [HttpDelete("unsubscribe/{userId}")]
        public async Task<IActionResult> Unsubscribe(string userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id.ToString() != userId)
            {
                return Unauthorized();
            }

            await _pushNotificationService.UnsubscribeFromPushNotificationAsync(user.Id);

            return Ok();
        }

        [HttpPost("test/{userId}")]
        [AllowAnonymous]
        [Obsolete("for test")]
        public async Task<ActionResult> Push(string userId, [FromBody] NotificationModel notificationModel)
        {
            await _pushNotificationService.SendNotificationAsync(new NotificationServiceModel
                {
                    Message = notificationModel.Message,
                    Title = notificationModel.Title,
                    Url = notificationModel.Url
                },
                new Guid(userId));

            return Ok();
        }
    }
}