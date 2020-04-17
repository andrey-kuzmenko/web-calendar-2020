using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCalendar.DAL.Models;

namespace WebCalendar.DAL.EF.Configurations
{
    public abstract class RepeatableActivityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IRepeatableActivity
    {
        protected readonly ValueConverter _valueConverter =
            new ValueConverter<ICollection<int>, string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries)
                    .Select(val => int.Parse(val)).ToList());

        protected void ConvertFieds(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.Years)
                .HasConversion(_valueConverter);

            builder.Property(e => e.Monthes)
                .HasConversion(_valueConverter);

            builder.Property(e => e.DaysOfMounth)
                .HasConversion(_valueConverter);

            builder.Property(e => e.DaysOfWeek)
                .HasConversion(_valueConverter);
        }
        public abstract void Configure(EntityTypeBuilder<T> builder);
      
    }
}
