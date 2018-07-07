using System;
using System.Collections.Generic;

namespace BenTools.Utilities.DateAndTime.DateInterval
{
     
    public class GrouppedDateIntervals : ADateIntervals<DateTime>
    {
        private DateTime _startUtc;
        private DateTime _endUtc;
        private readonly EGrouppedTimeInterval _timeInterval;

        public GrouppedDateIntervals(
            DateTime startUtc,
            DateTime endUtc,
            EGrouppedTimeInterval grouppedTimeInterval)
        {
            _timeInterval = grouppedTimeInterval;

            var timeInterval = GrouppedTimeIntervalHelper.ToTimeInterval(grouppedTimeInterval);
            _startUtc = TimeIntervalHelper.ResetDateTime(startUtc, timeInterval);
            _endUtc = TimeIntervalHelper.ResetDateTime(endUtc, timeInterval);

            _intervals = GenerateIntervals();
        }

        protected override IEnumerable<DateTime> GenerateIntervals()
        {
            var intervals = new List<DateTime>();
            var addOneAmountOfTimeFunction = GrouppedTimeIntervalHelper.AddOneAmountOfTimeFunction(_timeInterval);
            var amountOfTime = GetAmountsOfTime();
            bool ComparaisonFunction(int minimum, int maximum) => _timeInterval == EGrouppedTimeInterval.Year ? minimum <= maximum : minimum < maximum;

            for (var startTime = _timeInterval == EGrouppedTimeInterval.Year ? DateTime.MinValue.AddYears(_startUtc.Year + 1) : DateTime.MinValue;
                ComparaisonFunction(amountOfTime.Minimum, amountOfTime.Maximum);
                startTime = addOneAmountOfTimeFunction(startTime), amountOfTime.Minimum++)
                intervals.Add(startTime);

            return intervals;
        }

        protected override bool IsBetweenInterval(DateTime interval, DateTime current)
        {
            var amountOfTimeFunction = GrouppedTimeIntervalHelper.GetAmountOfTimeFunction(_timeInterval);

            return amountOfTimeFunction(current) == amountOfTimeFunction(interval);
        }

        private (int Minimum, int Maximum) GetAmountsOfTime()
        {
            return (_timeInterval == EGrouppedTimeInterval.Year)
                ? (_startUtc.Year, _endUtc.Year)
                : (0, GrouppedTimeIntervalHelper.GetMaximumAmountOfTime(_timeInterval));
        }
    }
    
}