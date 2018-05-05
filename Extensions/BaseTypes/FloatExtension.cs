using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class FloatExtension
    {
        public static bool NearlyEquals(this float left, float right, float epsilon = Single.Epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this float number, float minimum, float maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this float number, float minimum, float maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this float number, float minimum, float maximum, float epsilon = Single.Epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEquals(minimum, epsilon) ||
            number.NearlyEquals(maximum, epsilon);

        /// <summary>
        /// SOME like Sql, return true if the values contains one element that verify the comparerOperator comparaison with the number.
        /// </summary>
        public static bool SomeAnyVerifyTheComparerCondition(this float number, IEnumerable<float> values, EComparerOperator comparerOperator, float epsilon = Single.Epsilon)
        {
            var comparerFunction = FloatHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.Any(value => comparerFunction(number, value));
        }

        /// <summary>
        /// return true if all the values verify the comparerOperator comparaison with number.
        /// </summary>
        public static bool SomeAllVerifyTheComparerCondition(this float number, IEnumerable<float> values, EComparerOperator comparerOperator, float epsilon = Single.Epsilon)
        {
            var comparerFunction = FloatHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.All(value => comparerFunction(number, value));
        }

        /// <summary>
        /// IN like Sql, return true if one element of the values is equal to number.
        /// </summary>
        public static bool In(this float number, IEnumerable<float> values) => values.Contains(number);
}
