using BenTools.Utilities.DateAndTime.Timer;
using NUnit.Framework;

namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class TimerTest
    {
        [Test]
        public void Timer()
        {
            var Timer = new TimerInSeconds(2);

            while (!Timer.IsRingingReset())
            {
                var alarm = Timer.Alarm;
                var remainigTime = Timer.ReaminingTime;
                var elaspedTime = Timer.ElapsedTime;
                var percent = Timer.Percent;
                var ratio = Timer.Ratio;
            }
        }
    }
}