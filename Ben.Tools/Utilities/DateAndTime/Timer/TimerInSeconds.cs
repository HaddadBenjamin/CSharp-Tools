using System;

namespace BenTools.Utilities.DateAndTime.Timer
{
    public sealed class TimerInSeconds : ATimer
    {
        public TimerInSeconds(double alarm, bool isRinging = false) : base(alarm, isRinging)
        {
        }

        public override double ElapsedTime => Convert.ToDouble(_stopwatch.ElapsedTicks) / 10000000;
    }
}