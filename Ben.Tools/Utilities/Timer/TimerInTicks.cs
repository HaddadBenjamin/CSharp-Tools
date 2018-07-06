using System;

namespace Ben.Tools.Development
{
    public sealed class TimerInTicks : ATimer
    {
        public TimerInTicks(double alarm, bool isRinging = false) : base(alarm, isRinging)
        {
        }

        public override double ElapsedTime => Convert.ToDouble(_stopwatch.ElapsedTicks);
    }
}