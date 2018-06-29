using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Helpers.BaseTypes;

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

        #region Find > Single Element
        private static float FindNearestNumberAlgorithm(this float @float, IEnumerable<float> numbers, float valueIfNotFound, Func<IEnumerable<float>, IEnumerable<float>> ordonnedFunction, Func<float, float, bool> comparator)
        {
            var ordonnedNumbers = ordonnedFunction(numbers);

            return ordonnedNumbers.Any(number => comparator(number, @float)) ?
                ordonnedNumbers.LastOrDefault(number => comparator(number, @float)) :
                valueIfNotFound;
        }

        public static float FindNearestNumber(this float @float, IEnumerable<float> numbers, float valueIfNotFound = 0) =>
            @float.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => numbers.OrderBy(number => Math.Abs(@float - number)), (number, @int) => true);

        public static float FindNearestNumberButLower(this float @float, IEnumerable<float> numbers, float valueIfNotFound = 0) =>
            @float.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderBy(number => number), (number, @int) => number < @float);

        public static float FindNearestNumberButLowerOrEqual(this float @float, IEnumerable<float> numbers, float valueIfNotFound = 0) =>
            @float.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderBy(number => number), (number, @int) => number <= @float);

        public static float FindNearestNumberButGreater(this float @float, IEnumerable<float> numbers, float valueIfNotFound = 0) =>
            @float.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderByDescending(number => number), (number, @int) => number > @float);

        public static float FindNearestNumberButGreaterOrEqual(this float @float, IEnumerable<float> numbers, float valueIfNotFound = 0) =>
            @float.FindNearestNumberAlgorithm(numbers, valueIfNotFound, (elements) => elements.OrderByDescending(number => number), (number, @int) => number >= @float);
        #endregion

        /// <summary>
        /// SOME like Sql, return true if the values contains one element that verify the comparerOperator comparaison with the number.
        /// </summary>
        public static bool DoesAnyVerifyTheComparerCondition(this float number, IEnumerable<float> values, EComparerOperator comparerOperator, float epsilon = Single.Epsilon)
        {
            var comparerFunction = FloatHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.Any(value => comparerFunction(number, value));
        }

        /// <summary>
        /// return true if all the values verify the comparerOperator comparaison with number.
        /// </summary>
        public static bool DoesAllVerifyTheComparerCondition(this float number, IEnumerable<float> values, EComparerOperator comparerOperator, float epsilon = Single.Epsilon)
        {
            var comparerFunction = FloatHelper.OperatorComparerFunction(comparerOperator, epsilon);

            return values.All(value => comparerFunction(number, value));
        }

        /// <summary>
        /// IN like Sql, return true if one element of the values is equal to number.
        /// </summary>
        public static bool In(this float number, IEnumerable<float> values) => values.Contains(number);
    }
}