using System;

namespace BenTools.Utilities.DateAndTime.Timer
{
    public sealed class TimerInMilliseconds : ATimer
    {
        public TimerInMilliseconds(double alarm, bool isRinging = false) : base(alarm, isRinging)
        {
        }

        public override double ElapsedTime => Convert.ToDouble(_stopwatch.ElapsedTicks) / 10000;
    }
}