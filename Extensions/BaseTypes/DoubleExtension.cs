using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Helpers.BaseTypes;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class DoubleExtension
    {
        public static bool NearlyEquals(this double left, double right, double epsilon = Double.Epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this double number, double minimum, double maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this double number, double minimum, double maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this double number, double minimum, double maximum, double epsilon = Double.Epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEquals(minimum, epsilon) ||
            number.NearlyEquals(maximum, epsilon);

        /// <summary>
        /// SOME like Sql, return true if the values contains one element that verify the comparerOperator comparaison with the number.
        /// </summary>
        public static bool Some(this double number, IEnumerable<double> values, EComparerOperator comparerOperator, double epsilon = Double.Epsilon)
        {
            var comparerFunction = DoubleHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.Any(value => comparerFunction(number, value));
        }

        /// <summary>
        /// return true if all the values verify the comparerOperator comparaison with number.
        /// </summary>
        public static bool SomeAll(this double number, IEnumerable<double> values, EComparerOperator comparerOperator, double epsilon = Double.Epsilon)
        {
            var comparerFunction = DoubleHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.All(value => comparerFunction(number, value));
        }

        /// <summary>
        /// IN like Sql, return true if one element of the values is equal to number.
        /// </summary>
        public static bool In(this double number, IEnumerable<double> values) => values.Contains(number);
    }
}
