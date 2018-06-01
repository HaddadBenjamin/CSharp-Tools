using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Extensions.BaseTypes;

namespace Ben.Tools.Extensions.Sequences
{
    public static class EnumerableExtension
    {
        #region Filters
        public static IEnumerable<ElementType> WhereWithIndex<ElementType>(this IEnumerable<ElementType> sequence, Func<ElementType, int, bool> predicate) =>
            sequence.Select((element, index) => new { element, index })
                    .Where((elementWithIndex) => predicate(elementWithIndex.element, elementWithIndex.index))
                    .Select((elementWithIndex) => elementWithIndex.element);

        public static IEnumerable<ElementType> WhereIndexIsBetween<ElementType>(this IEnumerable<ElementType> sequence, int minimumIndex, int maxmimumIndex) =>
            sequence.WhereWithIndex((element, index) => index >= minimumIndex && index <= maxmimumIndex);

        public static IEnumerable<ElementType> WhereIndexIsAtLeast<ElementType>(this IEnumerable<ElementType> sequence, int minimumIndex) =>
            sequence.WhereIndexIsBetween(minimumIndex, int.MaxValue);

        public static IEnumerable<ElementType> WhereIndexIsAtMost<ElementType>(this IEnumerable<ElementType> sequence, int maximumIndex) =>
            sequence.WhereIndexIsBetween(0, maximumIndex);

        public static IEnumerable<ElementType> DistinctByColumn<ElementType, PredicateKey>(
            this IEnumerable<ElementType> sequence,
            Func<ElementType, PredicateKey> predicate) =>
            sequence.GroupBy(predicate)
                    .Select(element => element.First());
        #endregion

        #region Count Method(s)
        public static bool CountAtLeast<ElementType>(this IEnumerable<ElementType> sequence, int numberOfElements) =>
            sequence.Count() >= numberOfElements;

        public static bool CountAtMost<ElementType>(this IEnumerable<ElementType> sequence, int numberOfElements) =>
            sequence.Count() <= numberOfElements;

        public static bool CountIsBetween<ElementType>(this IEnumerable<ElementType> sequence, int minimum, int maximum) =>
            sequence.Count() >= minimum && sequence.Count() <= maximum;

        public static bool CountCompare<ElementType>(this IEnumerable<ElementType> sequence, IEnumerable<ElementType> otherSequence) =>
            sequence.Count() == otherSequence.Count();
        #endregion

        #region Utilities
        public static void Consume<ElementType>(this IEnumerable<ElementType> sequence)
        {
            foreach (var element in sequence) ;
        }

        public static void Foreach<ElementType>(
            this IEnumerable<ElementType> sequence,
            Action<ElementType> action)
        {
            foreach (var element in sequence)
                action(element);
        }

        public static void Foreach<ElementType>(
            this IEnumerable<ElementType> sequence,
            Action<ElementType, int> action)
        {
            var index = 0;

            foreach (var element in sequence)
                action(element, index++);
        }
        
        public static int IndexOf<ElementType>(this IEnumerable<ElementType> sequence, ElementType elementToSearch, IEqualityComparer<ElementType> comparer = default(IEqualityComparer<ElementType>))
        {
            comparer = comparer ?? EqualityComparer<ElementType>.Default;

            var elementFound = sequence.Select((element, index) => new { element, index })
                                       .FirstOrDefault(elementWithIndex => comparer.Equals(elementWithIndex.element, elementToSearch));

            return elementFound == null ? -1 : elementFound.index;
        }

        public static int IndexOf<ElementType>(this IEnumerable<ElementType> sequence, Func<ElementType, bool> predicate, IEqualityComparer<ElementType> comparer = default(IEqualityComparer<ElementType>)) =>
            sequence.IndexOf(sequence.FirstOrDefault(predicate), comparer);
        
        public static IEnumerable<ElementType> CopySequence<ElementType>(this IEnumerable<ElementType> sequence)
            where ElementType : new() =>
            sequence.Select(element => element.CopyObject());
        #endregion

        #region Element(s) Insert & Remove
        public static IEnumerable<ElementType> AddElements<ElementType>(
            this IEnumerable<ElementType> sequence,
            IEnumerable<ElementType> elementsToAdd)
        {
            if (sequence is List<ElementType> list)
            {
                list.AddRange(elementsToAdd);
                return list;
            }

            return sequence.Concat(elementsToAdd);
        }

        public static IEnumerable<ElementType> RemoveNullElements<ElementType>(this IEnumerable<ElementType> sequence)
            where ElementType : class =>
            sequence.Where(element => element != null);

        public static IEnumerable<ElementType> RemoveElements<ElementType>(this IEnumerable<ElementType> sequence, Func<ElementType, bool> predicate) =>
            sequence.Except(sequence.Where(predicate));
        #endregion

        #region Predicate
        public static bool ContainsAll<ElementType>(
            this IEnumerable<ElementType> sequence,
            IEnumerable<ElementType> elements) =>
            sequence.All(elements.Contains);

        public static bool ContainsAny<ElementType>(
            this IEnumerable<ElementType> sequence,
            IEnumerable<ElementType> elements) =>
            sequence.Any(elements.Contains);
        #endregion

        #region Filters
        /// <summary>
        /// Mélange une sequence.
        /// </summary>
        public static IEnumerable<ElementType> Shuffle<ElementType>(this IEnumerable<ElementType> sequence) =>
            sequence.OrderBy(element => Guid.NewGuid());

        public static ElementType RandomElement<ElementType>(this IEnumerable<ElementType> sequence) =>
            sequence.Shuffle()
                    .FirstOrDefault();
        #endregion

        #region Element(s) Generation
        public static IEnumerable<ElementType> RandomElements<ElementType>(
            this IEnumerable<ElementType> sequence,
            int numbersOfElementsToGenerate = 10)
        {
            var random = new Random();
            var sequenceCount = sequence.Count();

            return Enumerable.Repeat(sequence, numbersOfElementsToGenerate)
                             .Select(element => sequence.ElementAt(random.Next(sequenceCount)));
        }
        #endregion

        #region Selection and Group
        /// <summary>
        /// GroupByColumns(3) : 
        ///                             [1, 2, 3]
        /// [1, 2, 3, 4, 5, 6, 7]  =>   [4, 5, 6]
        ///                             [7,     ]
        /// </summary>
        public static IEnumerable<IEnumerable<ElementType>> GroupByColumns<ElementType>(
            this IEnumerable<ElementType> sequence,
            int numberOfColumns) =>
            sequence
                .Select((value, columnIndex) =>
                    columnIndex <= sequence.Count() / numberOfColumns ?
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
        public static IEnumerable<IEnumerable<ElementType>> GroupByLines<ElementType>(
            this IEnumerable<ElementType> sequence,
            int numberOfLines) =>
            sequence
                .Select((value, lineIndex) =>
                    lineIndex < numberOfLines ?
                        sequence.Where((element, index) => index % numberOfLines == lineIndex) :
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
        #endregion

        #region Null Methods
        /// <summary>
        /// COLEASCE like Sql, return the firt element not null, if none are found return null.
        /// </summary>
        public static ElementType ColeasceGetFirstElementNotNull<ElementType>(this IEnumerable<ElementType> sequence)
            where ElementType : class =>
            sequence.FirstOrDefault(element => element != null);

        /// <summary>
        /// NULLIF like Sql, return null if all the elements are equals, if none around found return the first not null.
        /// </summary>
        public static ElementType NullIfAllEqual<ElementType>(this IEnumerable<ElementType> sequence)
            where ElementType : class =>
            sequence.All(element => element == sequence.FirstOrDefault())
                ? null
                : sequence.First(element => element != null);
        #endregion
    }
}
