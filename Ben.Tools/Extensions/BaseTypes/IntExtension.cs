using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Extensions.BaseTypes
{
    public static class IntExtension
    {
        public static int FindNearest(this int integer, IEnumerable<int> numbers) => numbers
            .OrderBy(number => Math.Abs((long) integer - number))
            .LastOrDefault();

        #region Equality Comparer
        public static bool NearlyEqual(this int left, int right, int epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this int number, int minimum, int maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this int number, int minimum, int maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this int number, int minimum, int maximum, int epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEqual(minimum, epsilon) ||
            number.NearlyEqual(maximum, epsilon);
        #endregion

    }
}
