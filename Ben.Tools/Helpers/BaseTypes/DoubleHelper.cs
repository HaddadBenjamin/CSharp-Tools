using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.BaseTypes;

namespace BenTools.Helpers.BaseTypes
{
    public static class DoubleHelper
    {
        /// <summary>
        /// Le ToList() à la fin est important car autrement une nouvelle génération d'éléments se fera à chaque fois que vous allez parcourir votre séquence.
        /// </summary>
        private static readonly Random Random = new Random();

        #region Generate
        public static List<double> Generate(int count = 10) => Enumerable.Range(0, count).Select(_ => Random.NextDouble() * (float.MaxValue - float.MinValue) + float.MinValue).ToList();

        public static IEnumerable<double> GenerateNumbers(double startNumber = 0, double endNumber = 10, double addNumber = 1, Func<double, double, bool> comparator = null)
        {
            comparator = comparator ?? ((start, end) => start <= end);

            var numbers = new List<double>();

            for (; comparator(startNumber, endNumber); startNumber += addNumber)
                numbers.Add(startNumber);

            return numbers;
        }

        public static IEnumerable<double> GenerateClampedNumbers(int numberOfElements = 10, double minimum = 0, double maximum = 1) =>
            Enumerable.Repeat<Func<double>>(() => Random.NextDouble() * (maximum - minimum) + minimum, numberOfElements)
                      .Select(generateValueFunction => generateValueFunction());

        public static IEnumerable<double> GenerateNormalizedNumbers(int numberOfElements) =>
            Enumerable.Repeat<Func<double>>(Random.NextDouble, numberOfElements)
                      .Select(generateValueFunction => generateValueFunction());

        public static double GenerateClampedNumber(double minimum, double maximum) => GenerateClampedNumbers(1, minimum, maximum).First();
        #endregion

        public static Func<double, double, bool> OperatorComparerFunction(EComparerOperator comparerOperator, double epsilon = Double.Epsilon)
        {
            Func<double, double, bool> comparer;

            switch (comparerOperator)
            { 
                case EComparerOperator.Less:        comparer = ((left, right) => left < right);  break;
                case EComparerOperator.LessOrEqual:
                case EComparerOperator.LessOrNearlyEqual:
                                                    comparer = ((left, right) => left < right || left.NearlyEquals(right, epsilon)); break;
                case EComparerOperator.Equal:
                case EComparerOperator.NearlyEqual:
                                                    comparer = ((left, right) => left.NearlyEquals(right, epsilon)); break;
                case EComparerOperator.MoreOrEqual:
                case EComparerOperator.MoreOrNearlyEqual:
                                                    comparer = ((left, right) => left > right || left.NearlyEquals(right, epsilon)); break;
                default:                            comparer = ((left, right) => left > right);  break;
            }

            return comparer;
        }
    }
}
