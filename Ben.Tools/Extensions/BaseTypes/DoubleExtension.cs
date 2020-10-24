using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Extensions.BaseTypes
{
    public static class DoubleExtension
    {
        public static double FindNearest(this double @double, IEnumerable<double> numbers) => numbers
            .OrderBy(number => Math.Abs(@double - number))
            .LastOrDefault();

        #region Equality Comparer
        public static bool NearlyEquals(this double left, double right, double epsilon = Double.Epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this double number, double minimum, double maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this double number, double minimum, double maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this double number, double minimum, double maximum, double epsilon = Double.Epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEquals(minimum, epsilon) ||
            number.NearlyEquals(maximum, epsilon);
        #endregion
    }
}
