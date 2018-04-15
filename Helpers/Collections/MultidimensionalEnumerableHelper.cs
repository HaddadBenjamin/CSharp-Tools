using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers.Collections
{
    public static class MultiDimensionnalEnumerableHelper
    {
        // Jagged = multidimensional with each column of different length.
         
        // [a1, a2, a3]         [a1, b1, c1]
        // [b1, b2, b3]   ==>   [a2, b2, c2]
        // [c1, c2, c3]         [a3, b3, c3]
        public static IEnumerable<IEnumerable<TElementType>> Transpose<TElementType>(
            this IEnumerable<IEnumerable<TElementType>> jaggedEnumerable,
            bool forceElemToArray = false) => 
            jaggedEnumerable
            .SelectMany(x => x.Select((y, i) => new { val = y, idx = i }))
            .GroupBy(x => x.idx, x => x.val, (x, y) => forceElemToArray ? y.ToArray() : y);

        public static TElementType[][] Transpose<TElementType>(this TElementType[][] jaggedArray)
        {
            var minimumElement = jaggedArray.Select(x => x.Length).Min();

            return jaggedArray
                .SelectMany(x => x.Take(minimumElement).Select((y, i) => new { val = y, idx = i }))
                .GroupBy(x => x.idx, x => x.val, (x, y) => y.ToArray()).ToArray();
        }
        
    }
}