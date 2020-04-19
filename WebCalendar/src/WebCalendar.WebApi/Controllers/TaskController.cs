using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Task;
using WebCalendar.Services.Models.User;
using WebCalendar.WebApi.Models.Task;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public TaskController(ITaskService taskService, IMapper mapper, IUserService userService)
        {
            _taskService = taskService;
            _mapper = mapper;
            _userService = userService;
        }
        
        [HttpPost("{userId}")]
        public async Task<ActionResult<TaskModel>> CreateTask(Guid userId, [FromBody] TaskCreationModel taskCreationModel)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }
            
            TaskCreationServiceModel taskCreationServiceModel = _mapper
                .Map<TaskCreationModel, TaskCreationServiceModel>(taskCreationModel);
            TaskServiceModel taskServiceModel = await _taskService.AddAsync(taskCreationServiceModel);

            TaskModel taskModel = _mapper.Map<TaskServiceModel, TaskModel>(taskServiceModel);

            return CreatedAtAction(nameof(GetTask), new
            {
                taskId = taskModel.Id,
                userId = user.Id
            }, taskModel);
        }

        [HttpGet("{taskId}/{userId}")]
        public async Task<ActionResult<TaskModel>> GetTask(Guid taskId, Guid userId)
        {
            UserServiceModel user = await _userService.GetByPrincipalAsync(User);

            if (user.Id != userId)
            {
                return Unauthorized();
            }

            TaskServiceModel taskServiceModel = await _taskService.GetByIdAsync(taskId);
            
            TaskModel taskModel = _mapper.Map<TaskServiceModel, TaskModel>(taskServiceModel);

            return Ok(taskModel);
        }
    }
}