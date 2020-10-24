using System.Collections.Generic;

namespace BenTools.Extensions.Sequences
{
    public static class ListExtension
    {
        public static bool FastContains<ElementType>(this List<ElementType> list, ElementType elementToSearch) => list.BinarySearch(elementToSearch) >= 0;
    }
}
