using System;
using System.Collections.Generic;
//using Ical.Net;
using Ical.Net.DataTypes;
//using Ical.Net.Serialization.iCalendar.Serializers;
using Ical.Net.Serialization;
using Microsoft.EntityFrameworkCore;
using WebCalendar.DAL;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.Services.Export
{
    public class Exporter
    {
        private readonly IUnitOfWork _uow;
        
        public async void ExportCalendar(Guid id)
        {
               Calendar calendar = await _uow.GetRepository<Calendar>()
                .GetFirstOrDefaultAsync(
                    predicate: c => c.Id == id, 
                    include: source => source.Include(c => c.Events)
                        .ThenInclude(e => e.Event));
            Ical.Net.Calendar icalenadar = new Ical.Net.Calendar();




        }
    }
}
