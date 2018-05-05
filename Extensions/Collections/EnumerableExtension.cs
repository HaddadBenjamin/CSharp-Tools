using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Collections
{
    public static class EnumerableExtension
    {
        public static void Foreach<ElementType>(
            this IEnumerable<ElementType> collection,
            Action<ElementType> callback)
        {
            foreach (ElementType element in collection)
                callback?.Invoke(element);
        }

        public static void ForEachWithIndex<ElementType>(
            this IEnumerable<ElementType> collection,
            Action<ElementType, int> callback)
        {
            var index = 0;

            foreach (ElementType element in collection)
                callback?.Invoke(element, index++);
        }

        /// <summary>
        /// Mélange de façon aléatoire une collection.
        /// </summary>
        public static IEnumerable<ElementType> Shuffle<ElementType>(this IEnumerable<ElementType> collection) =>
            collection.OrderBy(element => Guid.NewGuid());

        public static bool ContainsAll<ElementType>(
            this IEnumerable<ElementType> collection,
            IEnumerable<ElementType> elements) =>
            collection.All(elements.Contains);

        public static bool ContainsAny<ElementType>(
            this IEnumerable<ElementType> collection,
            IEnumerable<ElementType> elements) =>
            collection.Any(elements.Contains);

        public static ElementType RandomElement<ElementType>(this IEnumerable<ElementType> collection) =>
            collection.Shuffle()
                      .FirstOrDefault();
                      
        public static IEnumerable<ElementType> RandomElements<ElementType>(
            this IEnumerable<ElementType> collection,
            int numbersOfElementsToGenerate = 10)
        {
            var random = new Random();
            var collectionCount = collection.Count();

            return Enumerable.Repeat(collection, numbersOfElementsToGenerate)
                             .Select(element => collection.ElementAt(random.Next(collectionCount)));
        }

        /// <summary>
        /// GroupByColumns(3) : 
        ///                             [1, 2, 3]
        /// [1, 2, 3, 4, 5, 6, 7]  =>   [4, 5, 6]
        ///                             [7,     ]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> GroupByColumns<ElementType>(
            this IEnumerable<ElementType> collection,
            int numberOfColumns) =>
            collection
            .Select((value, columnIndex) =>
                columnIndex <= collection.Count() / numberOfColumns ?
                    collection.Where((resourceGroup, index) => index >= columnIndex * numberOfColumns &&
                                                               index < (columnIndex + 1) * numberOfColumns)
                    : null)
            .Take(numberOfColumns);

        /// <summary>
        /// GroupByLines :
        ///                             [1, 4, 7]
        /// [1, 2, 3, 4, 5, 6, 7]  =>   [2, 5,  ]
        ///                             [3, 6,  ]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> GroupByLines<ElementType>(
            this IEnumerable<ElementType> collection,
            int numberOfLines) =>
            collection
            .Select((value, lineIndex) =>
                lineIndex < numberOfLines ?
                    collection.Where((resourceGroup, index) => index % numberOfLines == lineIndex) :
                    null)
            .Take(numberOfLines);

        /// <summary>
        /// [1, 2, 3]         [1, 4, 7]
        /// [4, 5, 6]   ==>   [2, 5, 8]
        /// [7, 8, 9]         [3, 6, 9]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> Transpose<ElementType>(
            this IEnumerable<IEnumerable<ElementType>> jaggedArray) =>
            jaggedArray
                .SelectMany(row => row.Select((element, index) => new { value = element, index = index }))
                .GroupBy(element => element.index, element => element.value, (index, value) => value);

        /// <summary>
        /// COLEASCE like Sql, return the firt element not null, if none are found return null.
        /// </summary>
        public static ElementType ColeasceGetFirstElementNotNull<ElementType>(this IEnumerable<ElementType> collection)
            where ElementType : class =>
            collection.FirstOrDefault(element => element != null);

        /// <summary>
        /// NULLIF like Sql, return null if all the elements are equals, if none around found return the first not null.
        /// </summary>
        public static ElementType NullIfAllEqual<ElementType>(this IEnumerable<ElementType> collection)
            where ElementType : class =>
            collection.All(element => element == collection.FirstOrDefault())
                ? null
                : collection.First(element => element != null);

        public static IEnumerable<ElementType> RemoveNullElements<ElementType>(this IEnumerable<ElementType> collection)
            where ElementType : class =>
            collection.Where(element => element != null);
    }
}
