using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace BenTools.Extensions.BaseTypes
{
    public static class ObjectExtension
    {
        #region Copy & Clone
        public static ObjectType DeepCopy<ObjectType>(this ObjectType objectToCopy) => (ObjectType)DeepCopyAlgorithm(objectToCopy);

        private static object DeepCopyAlgorithm(object objectToCopy)
        {
            if (objectToCopy == null)
                return null;

            var objectType = objectToCopy.GetType();

            if (objectType.IsValueType || objectType == typeof(string))
                return objectToCopy;

            if (objectType.IsArray)
            {
                var elementType = Type.GetType(objectType.FullName.Replace("[]", string.Empty));
                var arrayToCopy = objectToCopy as Array;
                var arrayNewInstance = Array.CreateInstance(elementType, arrayToCopy.Length);

                for (int arrayIndex = 0; arrayIndex < arrayToCopy.Length; arrayIndex++)
                    arrayNewInstance.SetValue(DeepCopyAlgorithm(arrayToCopy.GetValue(arrayIndex)), arrayIndex);

                return Convert.ChangeType(arrayNewInstance, objectToCopy.GetType());
            }

            if (objectType.IsClass)
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
            throw new ArgumentException("Unknown type");
        }

        public static ObjectType Clone<ObjectType>(this ObjectType objectToCopy)
        {
            var binaryFormatter = new BinaryFormatter();
            var memoryStream = new MemoryStream();

            using (memoryStream)
            {
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);

                return (ObjectType)binaryFormatter.Deserialize(memoryStream);
            }
        }
        #endregion

        #region Conversion
        public static IEnumerable<ElementType> ToEnumerable<ElementType>(this ElementType element)
        {
            yield return element;
        }
        #endregion
    }
}
