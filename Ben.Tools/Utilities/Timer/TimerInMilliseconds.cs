using System;

namespace Ben.Tools.Development
{
    public sealed class TimerInMilliseconds : ATimer
    {
        public TimerInMilliseconds(double alarm, bool isRinging = false) : base(alarm, isRinging)
        {
        }

        public override double ElapsedTime => Convert.ToDouble(_stopwatch.ElapsedTicks) / 10000;
    }
}