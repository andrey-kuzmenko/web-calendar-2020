using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUserService _userService;

        public NotificationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPut("email/subscribe/{userId}")]
        public async Task<IActionResult> SubscribeOnEmailNotification(Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.SubscribeOnEmailNotificationAsync(user.Id);

            return Ok();
        }
        
        [HttpPut("email/unsubscribe/{userId}")]
        public async Task<IActionResult> UnsubscribeFromEmailNotification(Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.UnsubscribeFromEmailNotificationAsync(user.Id);

            return Ok();
        }
        
        [HttpPost("push/subscribe/{userId}")]
        public async Task<IActionResult> SubscribeOnPushNotification(Guid userId, 
            [FromBody] PushSubscriptionModel pushSubscriptionModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.SubscribeOnPushNotificationAsync(user.Id, pushSubscriptionModel.DeviceToken);

            return Ok();
        }

        [HttpPost("push/unsubscribe/{userId}")]
        public async Task<IActionResult> Unsubscribe(Guid userId, [FromBody] PushSubscriptionModel pushSubscriptionModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            await _userService.UnsubscribeFromPushNotificationAsync(user.Id, pushSubscriptionModel.DeviceToken);

            return Ok();
        }
    }
}