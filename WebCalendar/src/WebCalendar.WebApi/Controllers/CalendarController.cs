using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCalendar.Common.Contracts;
using WebCalendar.Services.Contracts;
using WebCalendar.Services.Models.Calendar;
using WebCalendar.WebApi.Models.Calendar;

namespace WebCalendar.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;
        private readonly IMapper _mapper;

        public CalendarController(ICalendarService calendarService, IMapper mapper)
        {
            _calendarService = calendarService;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<CalendarInfoModel>> GetUserCalendars(Guid userId)
        {
            IEnumerable<CalendarServiceModel> userCalendarsServiceModels = await _calendarService
                .GetAllUserCalendarsAsync(userId);

            IEnumerable<CalendarInfoModel> userCalendars = _mapper
                .Map<IEnumerable<CalendarServiceModel>, IEnumerable<CalendarInfoModel>>(userCalendarsServiceModels);

            return Ok(userCalendars);
        }
    }
}