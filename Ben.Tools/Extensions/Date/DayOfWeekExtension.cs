using System;

namespace Ben.Tools.Extensions.Date
{
    public static class DayOfWeekExtension
    {
        public static int ToIndex(this DayOfWeek dayOfWeek) => ((int)dayOfWeek + 6) % 7;
    }
}