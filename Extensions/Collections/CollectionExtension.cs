using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Collections
{
    public static class CollectionExtension
    {
        public static ICollection<ElementType> RemoveElements<ElementType>(
            this ICollection<ElementType> collection,
            Func<ElementType, bool> predicate)
        {
            collection.Where(predicate)
                      .ToList()
                      .ForEach(element => collection.Remove(element));

            return collection;
        }

        public static ICollection<ElementType> RemoveElements<ElementType>(
            this ICollection<ElementType> collection,
            IEnumerable<ElementType> collectionToRemove) =>
            collection.RemoveElements(collectionToRemove.Contains);

        public static IEnumerable<ElementType> AddElements<ElementType>(
            this IEnumerable<ElementType> collection,
            IEnumerable<ElementType> elementsToAdd)
        {
            if (collection is List<ElementType> list)
            {
                list.AddRange(elementsToAdd);
                return list;
            }

            return collection.Concat(elementsToAdd);
        }
    }
}
