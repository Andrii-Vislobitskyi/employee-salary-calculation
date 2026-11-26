using System.Globalization;
using EMP.Common.Dto.APP_Dto;

namespace EMP.Common.Helpers
{
    public class DateRangeCalculator
    {
        public RangeDateReq GetYesterday(RangeDateReq dateReq, DateTime today)
        {
            dateReq.BeginDate = new DateTime(today.Year, today.Month, today.Day - 1, 8, 0, 0);
            dateReq.EndDate = new DateTime(today.Year, today.Month, today.Day - 1, 23, 0, 0);

            return dateReq;
        }

        public RangeDateReq GetCurrentWeek(RangeDateReq dateReq, DateTime today)
        {
            DateTime firstDayOfWeek = GetFirstDayOfWeekDate(today);
        
            dateReq.BeginDate = new DateTime(firstDayOfWeek.Year, firstDayOfWeek.Month, firstDayOfWeek.Day, 8, 0, 0);
            dateReq.EndDate = new DateTime(today.Year, today.Month, today.Day-1, 23, 0, 0);

            return dateReq;
        }


        public  DateTime GetFirstDayOfWeekDate(DateTime current)
        {
            // Get the culture-specific first day of the week (e.g., Sunday for en-US, Monday for en-GB)
            var culture = CultureInfo.CurrentCulture;
            DayOfWeek firstDayOfWeek = culture.DateTimeFormat.FirstDayOfWeek;

            // Calculate the difference in days between the given date's DayOfWeek and the culture's FirstDayOfWeek
            int diff = current.DayOfWeek - firstDayOfWeek;

            // If the difference is negative (e.g., today is Tuesday (2), first day is Sunday (0), diff is 2; 
            // if today is Sunday (0), first day is Monday (1), diff is -1), adjust to ensure a positive offset within the week
            if (diff < 0)
            {
                diff += 7;
            }
            // Subtract the difference from the current date to get the first day of the week
            return current.AddDays(-diff).Date;
        }


        public RangeDateReq GetLastWeek(RangeDateReq dateReq, DateTime today)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            DayOfWeek firstDayOfWeek = ci.DateTimeFormat.FirstDayOfWeek;

            int daysToStartOfCurrentWeek = (int)today.DayOfWeek - (int)firstDayOfWeek;
            if (daysToStartOfCurrentWeek < 0)
            {
                daysToStartOfCurrentWeek += 7;
            }


            DateTime startOfCurrentWeek = today.AddDays(-daysToStartOfCurrentWeek);

            DateTime startOfLastWeek = startOfCurrentWeek.AddDays(-7);
            // The end of the last week is one day before the start of the current week
            DateTime endOfLastWeek = startOfCurrentWeek.AddDays(-1);


            dateReq.BeginDate = new DateTime(startOfLastWeek.Year, startOfLastWeek.Month, startOfLastWeek.Day, 8, 0, 0);
            dateReq.EndDate = new DateTime(endOfLastWeek.Year, endOfLastWeek.Month, endOfLastWeek.Day, 23, 0, 0);         

            return dateReq;

        }


        public  RangeDateReq GetCurrentMonth(RangeDateReq dateReq, DateTime today)
        {
            var currentMonth = new DateTime(today.Year, today.Month, 1);

            dateReq.BeginDate = new DateTime(currentMonth.Year, currentMonth.Month, currentMonth.Day, 8, 0, 0);
            dateReq.EndDate = new DateTime(today.Year, today.Month, today.Day-1, 23, 0, 0);

            return dateReq;
        }

        public  RangeDateReq GetLastMonth(RangeDateReq dateReq, DateTime today)
        {
            var currentMonth = new DateTime(today.Year, today.Month, 1);
            var firstDayOfLastMonth = currentMonth.AddMonths(-1);
            var lastDayOLastMonth = currentMonth.AddDays(-1);

            dateReq.BeginDate = new DateTime(firstDayOfLastMonth.Year, firstDayOfLastMonth.Month, firstDayOfLastMonth.Day, 8, 0, 0);
            dateReq.EndDate = new DateTime(lastDayOLastMonth.Year, lastDayOLastMonth.Month, lastDayOLastMonth.Day, 23, 0, 0);

            return dateReq;
        }
    }
 
}
