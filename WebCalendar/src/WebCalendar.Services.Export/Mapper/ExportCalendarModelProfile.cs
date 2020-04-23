using Ical.Net;
using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.Common;

namespace WebCalendar.Services.Export.Mapper
{
    class ExportCalendarModelProfile : AutoMapperProfile
    {
        public ExportCalendarModelProfile()
        {
            CreateMap<DAL.Models.Entities.Calendar, Calendar>()
                   .ForMember(e => e.Events, o => o.MapFrom(e => e.Events))
                   .ForMember(e => e.Todos, o => o.MapFrom(e => e.Tasks));
        }
    }
}
