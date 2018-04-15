using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Extensions.Date;

namespace Ben.Tools.Helpers.Date
{
     
    public static class DayOfWeeksHelper
    {
        public static IEnumerable<DayOfWeek> GetDayOfWeeksSorted() => 
            ((DayOfWeek[])Enum.GetValues(typeof(DayOfWeek)))
            .OrderBy(dayOfWeek => dayOfWeek.ToIndex());

        public static IEnumerable<string> GetDayOfWeeksText() => 
            GetDayOfWeeksSorted()
            .Select(dayOfWeek => dayOfWeek.ToString());
    }
    
}