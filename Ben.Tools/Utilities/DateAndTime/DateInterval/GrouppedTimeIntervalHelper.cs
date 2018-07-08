using System;
using BenTools.Extensions.Date;
using BenTools.Helpers.Enumerations;

namespace BenTools.Utilities.DateAndTime.DateInterval
{
     
    public static class GrouppedTimeIntervalHelper
    {
        public static int GetMaximumAmountOfTime(EGrouppedTimeInterval grouppedInterval) =>  (grouppedInterval == EGrouppedTimeInterval.DayOfWeek) ? 7 : TimeIntervalHelper.GetMaximumAmountOfTime(ToTimeInterval(grouppedInterval));

        public static Func<DateTime, DateTime> AddOneAmountOfTimeFunction(EGrouppedTimeInterval grouppedInterval) => TimeIntervalHelper.AddOneAmountOfTimeFunction(ToTimeInterval(grouppedInterval));

        public static ETimeInterval ToTimeInterval(EGrouppedTimeInterval grouppedInterval) => (grouppedInterval == EGrouppedTimeInterval.DayOfWeek) ? ETimeInterval.Day : EnumerationHelper.ToEnumerationByText<EGrouppedTimeInterval, ETimeInterval>(grouppedInterval);

        public static Func<DateTime, int> GetAmountOfTimeFunction(EGrouppedTimeInterval grouppedInterval)
        {
            if (grouppedInterval == EGrouppedTimeInterval.DayOfWeek)
                return date => date.DayOfWeek.ToIndex();

            return TimeIntervalHelper.GetAmountOfTimeFunction(ToTimeInterval(grouppedInterval));
        }
    }
    
}