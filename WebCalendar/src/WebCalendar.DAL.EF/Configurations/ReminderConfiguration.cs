﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCalendar.DAL.Models.Entities;

namespace WebCalendar.DAL.EF.Configurations
{
    public class ReminderConfiguration : RepeatableActivityConfiguration<Reminder>
    {
        public void Configure(EntityTypeBuilder<Reminder> builder)
        {
            ConvertFieds(builder);

            builder.HasOne(r => r.Calendar)
                .WithMany(c => c.Reminders)
                .HasForeignKey(r => r.CalendarId);
        }
    }
}