using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Extensions.Date;
using Ben.Tools.Helpers.Enumerations;

namespace Ben.Tools.Helpers.Date
{
     
    public static class DayOfWeeksHelper
    {
        public static IEnumerable<DayOfWeek> SortedByIndex() => 
            EnumerationHelper.ToEnumerations<DayOfWeek>()
                             .OrderBy(dayOfWeek => dayOfWeek.ToIndex());
    }
    
}
