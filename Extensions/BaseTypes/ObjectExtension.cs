using System.Collections.Generic;
using System.Linq;
using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class ObjectExtension
    {
        public static bool DefaultOverrideEquals<ComparedType>(
            this object left,
            object right)
        {
            if (ReferenceEquals(null, right) ||
                left.GetType() != right.GetType())
                return false;

            if (ReferenceEquals(left, right))
                return true;

            return left.Equals((ComparedType) right);
        }

        public static bool IsNull(this object anObject) => anObject is null;

        public static IEnumerable<ElementType> ToEnumerable<ElementType>(this ElementType element)
        {
            yield return element;
        }

        public static List<ElementType> AsList<ElementType>(this ElementType element) => 
            element.ToEnumerable()
                   .ToList();

        public static ElementType[] AsArray<ElementType>(this ElementType element) => 
            element.ToEnumerable()
                   .ToArray();
        
        public static Nullable<ValueType> AsNullable<ValueType>(this ValueType type)
            where ValueType : struct => 
            type as ValueType?;

        public static ValueType AsValueType<ValueType>(this Nullable<ValueType> nullable, ValueType defaultValue = default(ValueType))
            where ValueType : struct =>
            nullable.HasValue ? nullable.Value : defaultValue;
    }
}
