using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Extensions.BaseTypes
{
    public static class FloatExtension
    {
        #region Equality Comparer
        public static bool NearlyEquals(this float left, float right, float epsilon = Single.Epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this float number, float minimum, float maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this float number, float minimum, float maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this float number, float minimum, float maximum, float epsilon = Single.Epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEquals(minimum, epsilon) ||
            number.NearlyEquals(maximum, epsilon);
        #endregion

        public static float FindNearest(this float @float, IEnumerable<float> numbers) => numbers
            .OrderBy(number => Math.Abs((long)@float - number))
            .LastOrDefault();
    }
}