using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.BaseTypes;

namespace BenTools.Helpers.BaseTypes
{
    public static class IntHelper
    {
        private static Random Random = new Random();

        #region Generate
        /// <summary>
        /// Le ToList() à la fin est important car autrement une nouvelle génération d'éléments se fera à chaque fois que vous allez parcourir votre séquence.
        /// </summary>
        public static List<int> Generate(int count = 10) => Enumerable.Range(0, count).Select(_ => Random.Next(int.MinValue, int.MaxValue)).ToList();

        public static IEnumerable<int> GenerateNumbers(int startNumber = 0, int endNumber = 10, int addNumber = 1, Func<int, int, bool> comparator = null)
        {
            comparator = comparator ?? ((start, end) => start <= end);

            var numbers = new List<int>();

            for (; comparator(startNumber, endNumber); startNumber += addNumber)
                numbers.Add(startNumber);

            return numbers;
        }

        public static IEnumerable<int> GenerateClampedNumbers(int numberOfElements = 10, int minimum = 0, int maximum = 1) =>
            Enumerable.Repeat<Func<int>>(() => Random.Next(minimum, maximum), numberOfElements)
                      .Select(generateValueFunction => generateValueFunction());

        public static int GenerateClampedNumber(int minimum = 0, int maximum = 1) => GenerateClampedNumbers(1, minimum, maximum).First();

        public static int GenerateUniqueInteger(bool positive = true)
        {
            var uniqueInteger = Guid.NewGuid().GetHashCode();

            return positive ? Math.Abs(uniqueInteger) : uniqueInteger;
        }
        #endregion

        public static Func<int, int, bool> OperatorComparerFunction(EComparerOperator comparerOperator, int epsilon = default(int))
        {
            Func<int, int, bool> comparer;

            switch (comparerOperator)
            {
                case EComparerOperator.Less:              comparer = ((left, right) => left < right);  break;
                case EComparerOperator.LessOrEqual:       comparer = ((left, right) => left <= right); break;
                case EComparerOperator.LessOrNearlyEqual: comparer = ((left, right) => left < right || left.NearlyEqual(right, epsilon)); break;
                case EComparerOperator.Equal:             comparer = ((left, right) => left == right); break;
                case EComparerOperator.NearlyEqual:       comparer = ((left, right) => left.NearlyEqual(right, epsilon)); break;
                case EComparerOperator.MoreOrEqual:       comparer = ((left, right) => left >= right); break;
                case EComparerOperator.MoreOrNearlyEqual: comparer = ((left, right) => left > right || left.NearlyEqual(right, epsilon)); break;
                default:                                  comparer = ((left, right) => left > right);  break;
            }

            return comparer;
        }

        /// <summary>
        /// If you use Sql, use @@Entity or Scope_entity() to generate a new unique Id.
        /// - { 5, 4, 3, 2, 1, 0 7, 7, 7 } => 6
        /// - { 5, 4, 3, 2, 1, 0 6, 7 } => 8
        /// - { } => 0
        /// </summary>
        public static int GetUniqueKey(IEnumerable<int> values)
        {
            var ordonedValues = values
                .Distinct()
                .OrderBy(v => v)
                .ToArray();

            for (int valueIndex = 0, firstValue = 0;
                valueIndex < ordonedValues.Length;
                valueIndex++, firstValue++)
            {
                if (firstValue < ordonedValues[valueIndex])
                    return firstValue;
            }

            return ordonedValues.Any() ? ordonedValues.Length : 0;
        }
        
        /// <summary>
        /// - { 5, 4, 3, 2, 1, 0, } => 6
        /// - { } => 1
        /// </summary>
        public static int IncrementKey(IEnumerable<int> values) => values.Any() ? values.Max() + 1 : 1;
    }
}
