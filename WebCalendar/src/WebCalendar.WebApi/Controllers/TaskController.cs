using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.WebApi.Models.Task;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateTask([FromBody] TaskCreationModel taskModel)
        {
            return Ok();
        }
    }
}