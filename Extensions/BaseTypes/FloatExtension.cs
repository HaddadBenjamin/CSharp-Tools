using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class FloatExtension
    {
        public static bool NearlyEquals(
            this float left,
            float right,
            float epsilon = 0.01f) => Math.Abs(left - right) <= epsilon;
    }
}
