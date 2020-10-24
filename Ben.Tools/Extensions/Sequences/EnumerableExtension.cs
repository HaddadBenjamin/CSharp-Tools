using System;
using System.Collections.Generic;
using System.Linq;

namespace BenTools.Extensions.Sequences
{
    public static class EnumerableExtension
    {
        private static Random Random = new Random();

        #region Filters
        public static IEnumerable<ElementType> WhereContains<ElementType>(this IEnumerable<ElementType> leftSequence, IEnumerable<ElementType> rightSequence) =>
            leftSequence.Where(left => rightSequence.Contains(left));

        public static IEnumerable<ElementType> WhereNotContains<ElementType>(this IEnumerable<ElementType> leftSequence, IEnumerable<ElementType> rightSequence) =>
            leftSequence.Where(left => !rightSequence.Contains(left));

        public static IEnumerable<ElementType1> WhereContains<ElementType1, ElementType2>(this IEnumerable<ElementType1> leftSequence, IEnumerable<ElementType2> rightSequence, Func<ElementType1, ElementType2, bool> predicate) =>
            leftSequence.Where(left => rightSequence.Any(right => predicate(left, right)));

        public static IEnumerable<ElementType1> WhereNotContains<ElementType1, ElementType2>(this IEnumerable<ElementType1> leftSequence, IEnumerable<ElementType2> rightSequence, Func<ElementType1, ElementType2, bool> predicate) =>
            leftSequence.Where(left => !rightSequence.Any(right => predicate(left, right)));

        public static IEnumerable<ElementType> DistinctByColumn<ElementType, PredicateKey>(this IEnumerable<ElementType> sequence, Func<ElementType, PredicateKey> predicate) => sequence
            .GroupBy(predicate)
            .Select(element => element.First());
        #endregion

        #region Utilities
        public static void Foreach<ElementType>(this IEnumerable<ElementType> sequence, Action<ElementType> action)
        {
            foreach (var element in sequence)
                action(element);
        }

        public static void Foreach<ElementType>(this IEnumerable<ElementType> sequence, Action<ElementType, int> action)
        {
            var index = 0;

            foreach (var element in sequence)
                action(element, index++);
        }

        public static int IndexOf<ElementType>(this IEnumerable<ElementType> sequence, ElementType elementToSearch, IEqualityComparer<ElementType> comparer = default(IEqualityComparer<ElementType>))
        {
            comparer = comparer ?? EqualityComparer<ElementType>.Default;

            var elementFound = sequence
                .Select((element, index) => new { element, index })
                    .FirstOrDefault(elementWithIndex => comparer.Equals(elementWithIndex.element, elementToSearch));

            return elementFound == null ? -1 : elementFound.index;
        }

        /// <summary>
        /// Mélange une sequence.
        /// </summary>
        public static IEnumerable<ElementType> Shuffle<ElementType>(this IEnumerable<ElementType> sequence) => sequence.OrderBy(element => Guid.NewGuid());
        #endregion

        #region Element(s) Insert & Remove & Merge
        public static IEnumerable<ElementType> MergeBy<ElementType, ElementKey>(this IEnumerable<ElementType> sequence, IEnumerable<ElementType> elementsToMerge, Func<ElementType, ElementKey> predicate) =>
            sequence.Union(elementsToMerge)
                    .Reverse()
                    .GroupBy(predicate)
                    .Select(element => element.First());
        #endregion

        #region Predicate
        public static bool ContainsAll<ElementType>(this IEnumerable<ElementType> sequence, IEnumerable<ElementType> elements) => sequence.All(elements.Contains);

        public static bool ContainsAny<ElementType>(this IEnumerable<ElementType> sequence, IEnumerable<ElementType> elements) => sequence.Any(elements.Contains);
        #endregion

        #region Element(s) Generation
        public static ElementType RandomElement<ElementType>(this IEnumerable<ElementType> sequence) => sequence.RandomElements(1).Single();

        public static IEnumerable<ElementType> RandomElements<ElementType>(this IEnumerable<ElementType> sequence, int numbersOfElementsToGenerate = 10)
        {
            var sequenceCount = sequence.Count();

            return Enumerable.Repeat(sequence, numbersOfElementsToGenerate)
                             .Select(element => sequence.ElementAt(Random.Next(sequenceCount)));
        }
        #endregion

        #region Selection and Group
        /// <summary>
        /// GroupByColumns(3) : 
        ///                             [1, 2, 3]
        /// [1, 2, 3, 4, 5, 6, 7]  =>   [4, 5, 6]
        ///                             [7,     ]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> GroupByColumns<ElementType>(this IEnumerable<ElementType> sequence, int numberOfColumns) => sequence
            .Select((value, columnIndex) => columnIndex <= sequence.Count() / numberOfColumns ?
                    sequence.Where((element, index) => index >= columnIndex * numberOfColumns &&
                                                       index < (columnIndex + 1) * numberOfColumns)
                    : null)
            .Take(numberOfColumns);

        /// <summary>
        /// GroupByLines(3) :
        ///                             [1, 4, 7]
        /// [1, 2, 3, 4, 5, 6, 7]  =>   [2, 5,  ]
        ///                             [3, 6,  ]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> GroupByLines<ElementType>(this IEnumerable<ElementType> sequence, int numberOfLines) => sequence
            .Select((value, lineIndex) => lineIndex < numberOfLines ?
                sequence.Where((element, index) => index % numberOfLines == lineIndex) :
                null)
            .Take(numberOfLines);

        /// <summary>
        /// [1, 2, 3]         [1, 4, 7]
        /// [4, 5, 6]   ==>   [2, 5, 8]
        /// [7, 8, 9]         [3, 6, 9]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> Transpose<ElementType>(this IEnumerable<IEnumerable<ElementType>> jaggedArray) => jaggedArray
            .SelectMany(row => row.Select((element, index) => new { value = element, index = index }))
            .GroupBy(element => element.index, element => element.value, (index, value) => value);
        #endregion
    }
}
