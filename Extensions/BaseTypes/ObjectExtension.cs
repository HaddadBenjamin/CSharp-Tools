using System.Collections.Generic;
using System.Linq;
using System;

namespace Ben.Tools.Extensions.BaseTypes
{
    public static class ObjectExtension
    {
        public static bool DefaulEquals<ComparedType>(
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
        
        public static ObjectType DeepCopy<ObjectType>(this ObjectType objectToCopy) =>
            (ObjectType)DeepCopyAlgorithm(objectToCopy);

        private static object DeepCopyAlgorithm(object objectToCopy)
        {
            if (objectToCopy == null)
                return null;

            var objectType = objectToCopy.GetType();

            if (objectType.IsValueType || objectType == typeof(string))
                return objectToCopy;

            else if (objectType.IsArray)
            {
                var elementType = Type.GetType(objectType.FullName.Replace("[]", string.Empty));
                var arrayToCopy = objectToCopy as Array;
                var arrayNewInstance = Array.CreateInstance(elementType, arrayToCopy.Length);

                for (int arrayIndex = 0; arrayIndex < arrayToCopy.Length; arrayIndex++)
                    arrayNewInstance.SetValue(DeepCopyAlgorithm(arrayToCopy.GetValue(arrayIndex)), arrayIndex);

                return Convert.ChangeType(arrayNewInstance, objectToCopy.GetType());
            }

            else if (objectType.IsClass)
            {
                var classType = Activator.CreateInstance(objectToCopy.GetType());
                var classFields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                foreach (var classField in classFields)
                {
                    var fieldValue = classField.GetValue(objectToCopy);

                    if (fieldValue == null)
                        continue;

                    classField.SetValue(classType, DeepCopyAlgorithm(fieldValue));
                }

                return classType;
            }
            else
                throw new ArgumentException("Unknown type");
        }

        public static bool IsNull(this object anObject) => anObject is null;
        
        /// <summary>
        /// ISNULL like Sql, return and set a value if an the object is null.
        /// </summary>
        public static ObjectType IsNullGetSpecifiedValueIfNull<ObjectType>(this ObjectType anyObject, ObjectType valueIfNull)
            where ObjectType : class =>
            anyObject ?? valueIfNull;

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
