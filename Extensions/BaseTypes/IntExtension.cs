using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Helpers.BaseTypes;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class IntExtension
    {
        public static bool Any(this int integer) => integer > 0;
        
        public static bool IsBetween(this int number, int minimum, int maximum) => number >= minimum && number <= maximum;
        
        /// <summary>
        /// SOME like Sql, return true if the values contains one element that verify the comparerOperator comparaison with the number.
        /// </summary>
        public static bool Some(this int number, IEnumerable<int> values, EComparerOperator comparerOperator)
        {
            var comparerFunction = IntHelper.OperatorComparerFunction(comparerOperator);

            return values.Any(value => comparerFunction(number, value));
        }

        /// <summary>
        /// return true if all the values verify the comparerOperator comparaison with number.
        /// </summary>
        public static bool SomeAll(this int number, IEnumerable<int> values, EComparerOperator comparerOperator)
        {
            var comparerFunction = IntHelper.OperatorComparerFunction(comparerOperator);

            return values.All(value => comparerFunction(number, value));
        }

        public static bool IsBetween(this int number, int minimum, int maximum) => number >= minimum && number <= maximum;

        /// <summary>
        /// IN like Sql, return true if one element of the values is equal to number.
        /// </summary>
        public static bool IsIn(this int number, IEnumerable<int> values) => values.Any(value => value == number);
    }
}
