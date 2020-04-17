﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF.Configurations
{
    public class EventConfiguration : RepeatableActivityConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            ConvertFieds(builder);

            builder.HasMany(e => e.UserEvents)
                .WithOne(ue => ue.Event)
                .HasForeignKey(ue => ue.EventId);

            builder.HasOne(e => e.Calendar)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CalendarId);
        }
    }
}