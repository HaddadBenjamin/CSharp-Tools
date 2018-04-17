using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Collections
{
    public static class EnumerableExtension
    {
        public static void ForEachWithIndex<TDataType>(
            this IEnumerable<TDataType> collection,
            Action<TDataType, int> callback)
        {
            var index = 0;

            foreach (TDataType element in collection)
            {
                callback?.Invoke(element, index);

                ++index;
            }
        }

        /// <summary>
        /// Mélange de façon aléatoire une collection.
        /// </summary>
        public static IEnumerable<TElement> Shuffle<TElement>(this IEnumerable<TElement> collection) =>
            collection.OrderBy(element => Guid.NewGuid());
         
        public static bool ContainsAny<TElementType>(
            this IEnumerable<TElementType> collection,
            IEnumerable<TElementType> elements) =>
            collection.Any(elements.Contains);

        public static TElementType RandomElement<TElementType>(this IEnumerable<TElementType> collection) =>
            collection.Shuffle()
                      .FirstOrDefault();
        
        public static IEnumerable<TElementType> RandomElements<TElementType>(
            this IEnumerable<TElementType> collection,
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
        /// GroupByLines(3) : 
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

        public static void Foreach<TDataType>(
            this IEnumerable<TDataType> collection,
            Action<TDataType> callback)
        {
            foreach (TDataType element in collection)
                callback?.Invoke(element);
        }
        
        // [a1, a2, a3]         [a1, b1, c1]
        // [b1, b2, b3]   ==>   [a2, b2, c2]
        // [c1, c2, c3]         [a3, b3, c3]
        public static IEnumerable<IEnumerable<TElementType>> Transpose<TElementType>(
            this IEnumerable<IEnumerable<TElementType>> jaggedArray) => 
            jaggedArray
            .SelectMany(row => row.Select((element, index) => new { value = element, index = index }))
            .GroupBy(element => element.index, element => element.value, (index, value) => value);
    }
}
