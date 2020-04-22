﻿using Ical.Net.CalendarComponents;
using System;
using System.Collections.Generic;
using System.Text;
using WebCalendar.Common;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.Services.Export.Mapper
{
    class ExportTaskModelProfile : AutoMapperProfile
    {
        public ExportTaskModelProfile()
        {
            CreateMap<Task, Todo>()
                   .ForMember(e => e.Uid, o => o.MapFrom(e => e.Id))
                   .ForMember(e => e.Name, o => o.MapFrom(e => e.Title))
                   .ForMember(e => e.Description, o => o.MapFrom(e => e.Description))
                   .ForMember(e => e.Created, o => o.MapFrom(e => e.AddedDate))
                   .ForMember(e => e.LastModified, o => o.MapFrom(e => e.ModifiedDate))
                   .ForMember(e => e.Start, o => o.MapFrom(e => e.StartTime));
        }
    }
}
