using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.BaseTypes;

namespace BenTools.Helpers.BaseTypes
{
    public static class DoubleHelper
    {
        #region Generate
        public static IEnumerable<double> GenerateNumbers(double startNumber = 0, double endNumber = 10, double addNumber = 1, Func<double, double, bool> comparator = null)
        {
            comparator = comparator ?? ((start, end) => start <= end);

            var numbers = new List<double>();

            for (; comparator(startNumber, endNumber); startNumber += addNumber)
                numbers.Add(startNumber);

            return numbers;
        }

        public static IEnumerable<double> GenerateNormalizedValues(int numberOfElements) =>
            Enumerable.Repeat<Func<double>>(new Random().NextDouble, numberOfElements)
                      .Select(generateValueFunction => generateValueFunction());
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
