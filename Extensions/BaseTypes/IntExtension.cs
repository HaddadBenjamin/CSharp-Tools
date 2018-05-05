using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Helpers.BaseTypes;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class IntExtension
    {
        public static bool NearlyEqual(this int left, int right, int epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this int number, int minimum, int maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this int number, int minimum, int maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this int number, int minimum, int maximum, int epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEqual(minimum, epsilon) ||
            number.NearlyEqual(maximum, epsilon);

        /// <summary>
        /// SOME like Sql, return true if the values contains one element that verify the comparerOperator comparaison with the number.
        /// </summary>
        public static bool SomeAnyVerifyTheComparerCondition(this int number, IEnumerable<int> values, EComparerOperator comparerOperator, int epsilon = default(int))
        {
            var comparerFunction = IntHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.Any(value => comparerFunction(number, value));
        }

        /// <summary>
        /// return true if all the values verify the comparerOperator comparaison with number.
        /// </summary>
        public static bool SomeAllVerifyTheComparerCondition(this int number, IEnumerable<int> values, EComparerOperator comparerOperator, int espilon = default(int))
        {
            var comparerFunction = IntHelper.OperatorComparerFunction(comparerOperator, espilon);

            return values.All(value => comparerFunction(number, value));
        }

        /// <summary>
        /// IN like Sql, return true if one element of the values is equal to number.
        /// </summary>
        public static bool In(this int number, IEnumerable<int> values) => values.Contains(number);
    }
}
