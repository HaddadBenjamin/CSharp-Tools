using System.Collections.Generic;
using System.Linq;
using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class ObjectExtension
    {
        public static bool DefaultOverrideEquals<TTypeToCompare>(
            this object thisObject,
            object objectToCompare)
        {
            if (ReferenceEquals(null, objectToCompare))
                return false;
            if (ReferenceEquals(thisObject, objectToCompare))
                return true;
            if (thisObject.GetType() != objectToCompare.GetType())
                return false;

            return thisObject.Equals((TTypeToCompare) objectToCompare);
        }

        public static bool IsNull(this object anObject) => null == anObject;

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

        public static ValueType AsValueType<ValueType>(this Nullable<ValueType> nullable)
            where ValueType : struct =>
            nullable.HasValue ? nullable.Value : default(ValueType);
    }
}
