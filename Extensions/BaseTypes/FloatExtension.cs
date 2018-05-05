using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class FloatExtension
    {
        public static bool NearlyEquals(this float left, float right, float epsilon = Single.Epsilon) => Math.Abs(left - right) <= epsilon;
        
        public static bool IsBetween(this float number, float minimum, float maximum) => number >= minimum && number <= maximum;
}
