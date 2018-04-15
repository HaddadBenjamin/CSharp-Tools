using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Collections
{
    public static class CollectionExtension
    {
        public static ICollection<TElementType> RemoveAll<TElementType>(
            this ICollection<TElementType> collection,
            Func<TElementType, bool> predicate)
        {
            collection.Where(predicate)
                      .ToList()
                      .ForEach(e => collection.Remove(e));

            return collection;
        }
        public static ICollection<TElementType> RemoveElements<TElementType>(
            this ICollection<TElementType> collection,
            IEnumerable<TElementType> collectionToDelete) =>
            collection.RemoveAll(collectionToDelete.Contains);

        public static IEnumerable<TElementType> AddElements<TElementType>(
            this IEnumerable<TElementType> collection,
            IEnumerable<TElementType> elementsToAdd)
        {
            if (collection is List<TElementType> list)
            {
                list.AddRange(elementsToAdd);
                return list;
            }

            return collection.Concat(elementsToAdd);
        }
    }
}