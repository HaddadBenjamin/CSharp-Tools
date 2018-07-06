using System;

namespace Ben.Tools.Development
{
    public sealed class TimerInSeconds : ATimer
    {
        public TimerInSeconds(double alarm, bool isRinging = false) : base(alarm, isRinging)
        {
        }

        public override double ElapsedTime => Convert.ToDouble(_stopwatch.ElapsedTicks) / 10000000;
    }
}