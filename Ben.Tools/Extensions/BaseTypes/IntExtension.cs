using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Helpers.BaseTypes;

namespace BenTools.Extensions.BaseTypes
{
    public static class IntExtension
    {
        #region Find > Single Element
        private static int FindNearestNumberAlgorithm(this int integer, IEnumerable<int> numbers, int valueIfNotFound, Func<IEnumerable<int>, IEnumerable<int>> ordonnedFunction, Func<int, int, bool> comparator)
        {
            var ordonnedNumbers = ordonnedFunction(numbers);

            return ordonnedNumbers.Any(number => comparator(number, integer)) ?
                ordonnedNumbers.LastOrDefault(number => comparator(number, integer)) :
                valueIfNotFound;
        }

        public static int FindNearestNumber(this int integer, IEnumerable<int> numbers, int valueIfNotFound = 0) =>
            integer.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => numbers.OrderBy(number => Math.Abs((long)integer - number)), (number, @int) => true);

        public static int FindNearestNumberButLower(this int integer, IEnumerable<int> numbers, int valueIfNotFound = 0) =>
            integer.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderBy(number => number), (number, @int) => number < integer);

        public static int FindNearestNumberButLowerOrEqual(this int integer, IEnumerable<int> numbers, int valueIfNotFound = 0) =>
            integer.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderBy(number => number), (number, @int) => number <= integer);

        public static int FindNearestNumberButGreater(this int integer, IEnumerable<int> numbers, int valueIfNotFound = 0) =>
            integer.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderByDescending(number => number), (number, @int) => number > integer);

        public static int FindNearestNumberButGreaterOrEqual(this int integer, IEnumerable<int> numbers, int valueIfNotFound = 0) =>
            integer.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderByDescending(number => number), (number, @int) => number >= integer);
        #endregion

        #region Equality Comparer
        public static bool NearlyEqual(this int left, int right, int epsilon) => Math.Abs(left - right) <= epsilon;

        public static bool IsBetween(this int number, int minimum, int maximum) => number > minimum && number < maximum;

        public static bool IsBetweenOrEqual(this int number, int minimum, int maximum) => number >= minimum && number <= maximum;

        public static bool IsBetweenOrNearlyEqual(this int number, int minimum, int maximum, int epsilon) =>
            number.IsBetween(minimum, maximum) ||
            number.NearlyEqual(minimum, epsilon) ||
            number.NearlyEqual(maximum, epsilon);
        #endregion

        #region Converter
        public static string ToBaseN(this int number, int baseN) => Convert.ToString(number, baseN);
        #endregion
            
        #region Mathematics
        // Détermine si le nombre est divisble que par 1 et lui même.
        public static bool IsPrime(int number) => 
            number > 1 && 
            Enumerable.Range(1, number)
                      .Where(numberToTest => number % numberToTest == 0)
                      .SequenceEqual(new[] { 1, numberToTest });
         #endregion

        /// <summary>
        /// SOME like Sql, return true if the values contains one element that verify the comparerOperator comparaison with the number.
        /// </summary>
        public static bool DoesAnyVerifyTheComparerCondition(this int number, IEnumerable<int> values, EComparerOperator comparerOperator, int epsilon = default(int))
        {
            var comparerFunction = IntHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.Any(value => comparerFunction(number, value));
        }

        /// <summary>
        /// return true if all the values verify the comparerOperator comparaison with number.
        /// </summary>
        public static bool DoesAllVerifyTheComparerCondition(this int number, IEnumerable<int> values, EComparerOperator comparerOperator, int espilon = default(int))
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
