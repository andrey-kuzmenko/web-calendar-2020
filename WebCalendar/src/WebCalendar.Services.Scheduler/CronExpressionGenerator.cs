using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebCalendar.DAL.Models;

namespace WebCalendar.Services.Scheduler
{
    public static class CronExpressionGenerator
    {
        private const char SEPARATOR = ',';
        private const string UNDEFINED = "?";

        public static string GetCronExpression(this IRepeatableActivity schedule)
        {
            string seconds = GetSeconds(schedule);
            string minutes = GetMinutes(schedule);
            string hours = GetHours(schedule);
            string daysOfMounth = GetDaysOfMounth(schedule);
            string mounthes = GetMounthes(schedule);
            string daysOfWeek = GetDaysOfWeek(schedule);
            string years = GetYears(schedule);

            return $"{seconds} {minutes} {hours} {daysOfMounth} {mounthes} {daysOfWeek} {years}";
        }

        private static string Separate(IEnumerable<int> items)
        {
            IEnumerable<string> stringsOfDays = items.Select(d => d.ToString());

            return String.Join(SEPARATOR, stringsOfDays);
        }

        private static string GetSeconds(IRepeatableActivity schedule)
        {
            return schedule.StartTime.Second.ToString();
        }

        private static string GetMinutes(IRepeatableActivity schedule)
        {
            return schedule.StartTime.Minute.ToString();
        }

        private static string GetHours(IRepeatableActivity schedule)
        {
            return schedule.StartTime.Hour.ToString();
        }

        private static string GetDaysOfMounth(IRepeatableActivity schedule)
        {
            IEnumerable<int> daysOfMounth = schedule.DaysOfMounth;

            if (daysOfMounth == null || daysOfMounth.Count() == 0)
            {
                return UNDEFINED;
            }

            return Separate(daysOfMounth);
        }

        private static string GetMounthes(IRepeatableActivity schedule)
        {
            return Separate(schedule.Monthes);
        }

        private static string GetDaysOfWeek(IRepeatableActivity schedule)
        {
            IEnumerable<int> daysOfWeek = schedule.DaysOfWeek;

            if (daysOfWeek == null || daysOfWeek.Count() == 0)
            {
                return UNDEFINED;
            }

            return Separate(daysOfWeek);
        }

        private static string GetYears(IRepeatableActivity schedule)
        {
            return Separate(schedule.Years);
        }
    }
}
