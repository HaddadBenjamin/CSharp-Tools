using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers.BaseTypes
{
    public static class IntHelper
    {
        public static int GenerateUniqueInteger(bool striclyPositive = true)
        {
            var uniqueInteger = Guid.NewGuid().GetHashCode();

            return striclyPositive ? Math.Abs(uniqueInteger) : uniqueInteger;
        }
        
        /// <summary>
        /// If you use Sql, use @@Entity or Scope_entity() to generate a new unique Id.
        /// - { 5, 4, 3, 2, 1, 0 7, 7, 7 } => 6
        /// - { 5, 4, 3, 2, 1, 0 6, 7 } => 8
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
