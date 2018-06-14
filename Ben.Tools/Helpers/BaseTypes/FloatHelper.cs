using Ben.Tools.Extensions.BaseTypes;
using System;

namespace Ben.Tools.Helpers.BaseTypes
{
    public static class FloatHelper
    {
        #region Maison
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