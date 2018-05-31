using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Sequences
{
    public static class CollectionExtension
    {
        #region Add & Remove Element(s)
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
        #endregion
    }
}
