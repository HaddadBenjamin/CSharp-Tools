using System;
using Ben.Tools.Extensions.BaseTypes;

namespace Ben.Tools.Helpers.BaseTypes
{
    public static class DoubleHelper
    {
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
