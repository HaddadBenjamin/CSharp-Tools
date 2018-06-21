using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.Date;
using BenTools.Helpers.Enumerations;

namespace BenTools.Helpers.Date
{
     
    public static class DayOfWeeksHelper
    {
        public static IEnumerable<DayOfWeek> SortedByIndex() => 
            EnumerationHelper.ToEnumerations<DayOfWeek>()
                             .OrderBy(dayOfWeek => dayOfWeek.ToIndex());
    }
    
}
