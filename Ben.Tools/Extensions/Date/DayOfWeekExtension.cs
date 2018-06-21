using System;

namespace BenTools.Extensions.Date
{
    public static class DayOfWeekExtension
    {
        public static int ToIndex(this DayOfWeek dayOfWeek) => ((int)dayOfWeek + 6) % 7;
    }
}