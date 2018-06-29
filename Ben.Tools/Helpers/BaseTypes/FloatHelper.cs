using System;
using System.Collections.Generic;
using BenTools.Extensions.BaseTypes;

namespace BenTools.Helpers.BaseTypes
{
    public static class FloatHelper
    {
        #region Maison
        #region Generate
        public static IEnumerable<float> GenerateNumbers(float startNumber = 0, float endNumber = 10, float addNumber = 1, Func<float, float, bool> comparator = null)
        {
            comparator = comparator ?? ((start, end) => start <= end);

            var numbers = new List<float>();

            for (; comparator(startNumber, endNumber); startNumber += addNumber)
                numbers.Add(startNumber);

            return numbers;
        }
        #endregion

        public static Func<float, float, bool> OperatorComparerFunction(EComparerOperator comparerOperator, float epsilon = Single.Epsilon)
        {
            Func<float, float, bool> comparer;

            switch (comparerOperator)
            {
                case EComparerOperator.Less: comparer = ((left, right) => left < right); break;
                case EComparerOperator.LessOrEqual:
                case EComparerOperator.LessOrNearlyEqual:
                    comparer = ((left, right) => left < right || left.NearlyEquals(right, epsilon)); break;
                case EComparerOperator.Equal:
                case EComparerOperator.NearlyEqual:
                    comparer = ((left, right) => left.NearlyEquals(right, epsilon)); break;
                case EComparerOperator.MoreOrEqual:
                case EComparerOperator.MoreOrNearlyEqual:
                    comparer = ((left, right) => left > right || left.NearlyEquals(right, epsilon)); break;
                default: comparer = ((left, right) => left > right); break;
            }

            return comparer;
        }
        #endregion
    }
}