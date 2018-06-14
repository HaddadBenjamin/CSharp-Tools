using System.Collections.Generic;

namespace Ben.Tools.Helpers.BaseTypes
{
    public static class DynamicHelper
    {
        public static IEnumerable<TElement> ToEnumerable<TElement>(
            dynamic dynamicObject,
            bool isSubClass = false)
        {
            var collection = new List<TElement>();

            if (dynamicObject != null)
            {
                foreach (var dynamicElement in dynamicObject)
                    collection.Add((TElement)(isSubClass ? dynamicElement : dynamicElement?.Value));
            }

            return collection;
        }

        public static TDataType GetData<TDataType>(
            dynamic dynamicObject,
            bool getValue = true) => 
            (TDataType)((getValue ? dynamicObject?.Value : dynamicObject) ?? default(TDataType));
    }
}