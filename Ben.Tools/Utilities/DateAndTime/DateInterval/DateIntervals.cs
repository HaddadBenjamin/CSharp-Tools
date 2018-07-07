using System;
using System.Collections.Generic;

namespace BenTools.Utilities.DateAndTime.DateInterval
{
    /// <summary>
    /// Permet de créer tous les intervales de temps entre 2 dates et de les utiliser pour grouper des données.
    /// </summary>
     
    public class DateIntervals : ADateIntervals<(DateTime intervalStartUtc, DateTime intervalEndUtc)>
    {
        private DateTime _startUtc;
        private DateTime _endUtc;
        private readonly ETimeInterval _timeInterval;

        public DateIntervals(
            DateTime startUtc,
            DateTime endUtc,
            ETimeInterval timeInterval)
        {
            if (startUtc > endUtc)
                throw new ArgumentException(nameof(endUtc));

            _timeInterval = timeInterval;
            _startUtc = TimeIntervalHelper.ResetDateTime(startUtc, timeInterval);
            _endUtc = TimeIntervalHelper.ResetDateTime(endUtc, timeInterval);
            _intervals = GenerateIntervals();
        }

        protected override IEnumerable<(DateTime intervalStartUtc, DateTime intervalEndUtc)> GenerateIntervals()
        {
            var intervals = new List<(DateTime, DateTime)>();
            var addOneAmountOfTimeFunction = TimeIntervalHelper.AddOneAmountOfTimeFunction(_timeInterval);

            for (DateTime currentDateTime = _startUtc;
                currentDateTime < _endUtc;
                currentDateTime = addOneAmountOfTimeFunction(currentDateTime))
                intervals.Add((currentDateTime, addOneAmountOfTimeFunction(currentDateTime)));

            return intervals;
        }
        protected override bool IsBetweenInterval((DateTime intervalStartUtc, DateTime intervalEndUtc) interval, DateTime current) => 
            current >= interval.intervalStartUtc && current < interval.intervalEndUtc;
    }
    
}