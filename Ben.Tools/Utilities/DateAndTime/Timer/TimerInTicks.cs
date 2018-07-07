using System;

namespace BenTools.Utilities.DateAndTime.Timer
{
    public sealed class TimerInTicks : ATimer
    {
        public TimerInTicks(double alarm, bool isRinging = false) : base(alarm, isRinging)
        {
        }

        public override double ElapsedTime => Convert.ToDouble(_stopwatch.ElapsedTicks);
    }
}