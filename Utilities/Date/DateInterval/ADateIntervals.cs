using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Utilities.Date.DateInterval
{
     
    public class MergedInterval<Element, IntervalType>
    {
        public readonly IntervalType Interval;
        public readonly IEnumerable<Element> Values;

        public MergedInterval(IntervalType interval, IEnumerable<Element> values)
        {
            Interval = interval;
            Values = values;
        }
    }

    public abstract class ADateIntervals<TInterval>
    {
        protected IEnumerable<TInterval> _intervals;

        /// <summary>
        /// Permet de corréler les intervales de temps avec une collection de données.
        /// </summary>
        public virtual IEnumerable<MergedInterval<TElement, TInterval>> MergeWithIntervals<TElement>(
            IEnumerable<TElement> enumerable,
            Func<TElement, DateTime> getDateUtcFunction) => 
            _intervals.Select(interval => new MergedInterval<TElement, TInterval>(
            interval: interval,
            values: enumerable.Where(element => IsBetweenInterval(interval, getDateUtcFunction(element)))));

        protected abstract IEnumerable<TInterval> GenerateIntervals();

        protected abstract bool IsBetweenInterval(TInterval interval, DateTime current);
    }
    
}