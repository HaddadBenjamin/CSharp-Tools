using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class FloatExtension
    {
        public static bool NearlyEquals(
            this float floatNumber,
            float equalNumber,
            float epsilon = 0.01f) => Math.Abs(floatNumber - equalNumber) <= epsilon;
    }
}