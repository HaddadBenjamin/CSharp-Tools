using System;
using BenTools.Extensions.Date;

namespace BenTools.Utilities.Date.DateInterval
{
     
    public static class TimeIntervalHelper
    {
        public static DateTime ResetDateTime(DateTime date, ETimeInterval interval)
        {
            if (interval > ETimeInterval.Millisecond) date = date.ResetMilliseconds();
            if (interval > ETimeInterval.Second) date = date.ResetSeconds();
            if (interval > ETimeInterval.Minute) date = date.ResetMinutes();
            if (interval > ETimeInterval.Hour) date = date.ResetHours();
            if (interval > ETimeInterval.Day) date = date.ResetDays();
            if (interval > ETimeInterval.Month) date = date.ResetMonths();

            return date;
        }

        public static int GetMaximumAmountOfTime(ETimeInterval interval)
        {
            switch (interval)
            {
                case ETimeInterval.Millisecond: return 1000;
                case ETimeInterval.Second: return 60;
                case ETimeInterval.Minute: return 60;
                case ETimeInterval.Hour: return 24;
                case ETimeInterval.Day: return 31;
                case ETimeInterval.Month: return 12;

                default: throw new ArgumentException(nameof(interval));
            }
        }

        public static Func<DateTime, int> GetAmountOfTimeFunction(ETimeInterval interval)
        {
            Func<DateTime, int> amountOfTimeFunction;

            if (interval == ETimeInterval.Millisecond) amountOfTimeFunction = date => date.Millisecond;
            else if (interval == ETimeInterval.Second) amountOfTimeFunction = date => date.Second;
            else if (interval == ETimeInterval.Minute) amountOfTimeFunction = date => date.Minute;
            else if (interval == ETimeInterval.Hour) amountOfTimeFunction = date => date.Hour;
            else if (interval == ETimeInterval.Day) amountOfTimeFunction = date => date.Day;
            else if (interval == ETimeInterval.Month) amountOfTimeFunction = date => date.Month;
            else amountOfTimeFunction = date => date.Year;

            return amountOfTimeFunction;
        }

        public static Func<DateTime, DateTime> AddOneAmountOfTimeFunction(ETimeInterval interval)
        {
            Func<DateTime, DateTime> addAmountOfTimeFunction;

            if (interval == ETimeInterval.Millisecond) addAmountOfTimeFunction = date => date.AddMilliseconds(1);
            else if (interval == ETimeInterval.Second) addAmountOfTimeFunction = date => date.AddSeconds(1);
            else if (interval == ETimeInterval.Minute) addAmountOfTimeFunction = date => date.AddMinutes(1);
            else if (interval == ETimeInterval.Hour) addAmountOfTimeFunction = date => date.AddHours(1);
            else if (interval == ETimeInterval.Day) addAmountOfTimeFunction = date => date.AddDays(1);
            else if (interval == ETimeInterval.Month) addAmountOfTimeFunction = date => date.AddMonths(1);
            else addAmountOfTimeFunction = date => date.AddYears(1);

            return addAmountOfTimeFunction;
        }
    }
    
}