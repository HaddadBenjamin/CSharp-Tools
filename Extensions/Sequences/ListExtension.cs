using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Sequences
{
    public static class ListExtension
    {
        #region Utilities
        public static bool FastContains<ElementType>(this List<ElementType> list, ElementType elementToSearch) =>
           list.BinarySearch(elementToSearch) >= 0;
        #endregion

        #region Element(s) insertion
        #region AddElement(s)AtBegin
        public static List<ElementType> AddElementAtBegin<ElementType>(this List<ElementType> list, ElementType element)
        {
            list.Insert(0, element);

            return list;
        }

        public static List<ElementType> AddElementsAtBegin<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd)
            => list.AddElementsAt(sequenceToAdd, 0);
        #endregion

        #region AddElement(s)Before
        public static List<ElementType> AddElementBefore<ElementType>(this List<ElementType> list, ElementType element, int index) =>
            list.AddElementAt(element, index - 1);

        public static List<ElementType> AddElementBefore<ElementType>(this List<ElementType> list, ElementType element, Func<ElementType, bool> predicate) =>
        list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(predicate) - 1);

        public static List<ElementType> AddElementBefore<ElementType>(this List<ElementType> list, ElementType element, ElementType elementToSearch) =>
            list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(elementToSearch) - 1);

        public static List<ElementType> AddElementsBefore<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, int index) =>
            list.AddElementsAt(sequenceToAdd, index - 1);

        public static List<ElementType> AddElementsBefore<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, Func<ElementType, bool> predicate) =>
            list.AddElementsAt(sequenceToAdd, list.GetElementIndexOrMaxIntValue(predicate) - 1);

        public static List<ElementType> AddElementsBefore<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, ElementType elementToSearch) =>
            list.AddElementsAt(sequenceToAdd, list.GetElementIndexOrMaxIntValue(elementToSearch) - 1);
        #endregion

        #region AddElement(s)At
        public static List<ElementType> AddElementAt<ElementType>(this List<ElementType> list, ElementType element, int index)
        {
            if (index <= 0)
                return list.AddElementAtBegin(element);
            if (index < list.Count())
                list.Insert(index, element);
            else
                return list.AddElementAtEnd(element);

            return list;
        }

        public static List<ElementType> AddElementAt<ElementType>(this List<ElementType> list, ElementType element, Func<ElementType, bool> predicate) =>
            list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(predicate));

        public static List<ElementType> AddElementAt<ElementType>(this List<ElementType> list, ElementType element, ElementType elementToSearch) =>
           list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(elementToSearch));

        public static List<ElementType> AddElementsAt<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, int index)
        {
            if (index <= 0)
            {
                if (sequenceToAdd is List<ElementType> sequenceToAddList)
                {
                    sequenceToAddList.AddRange(list);

                    return sequenceToAddList;
                }

                return sequenceToAdd.Concat(list).ToList();
            }
            if (index > list.Count())
            {
                list.AddRange(sequenceToAdd);

                return list;
            }

            var firstPartOfList = list.WhereIndexIsAtMost(index - 1).ToList();
            var secondPartOfList = list.WhereIndexIsAtLeast(index).ToList();

            firstPartOfList.AddRange(sequenceToAdd);
            firstPartOfList.AddRange(secondPartOfList);

            return firstPartOfList;
        }

        public static List<ElementType> AddElementsAt<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, Func<ElementType, bool> predicate) =>
            list.AddElementsAt(sequenceToAdd, list.GetElementIndexOrMaxIntValue(predicate));

        public static List<ElementType> AddElementsAt<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, ElementType elementToSearch) =>
            list.AddElementsAt(sequenceToAdd, list.GetElementIndexOrMaxIntValue(elementToSearch));
        #endregion

        #region AddElement(s)After
        public static List<ElementType> AddElementAfter<ElementType>(this List<ElementType> list, ElementType element, int index) =>
            list.AddElementAt(element, index);

        public static List<ElementType> AddElementAfter<ElementType>(this List<ElementType> list, ElementType element, Func<ElementType, bool> predicate) =>
        list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(predicate));

        public static List<ElementType> AddElementAfter<ElementType>(this List<ElementType> list, ElementType element, ElementType elementToSearch) =>
            list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(elementToSearch));

        public static List<ElementType> AddElementsAfter<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, int index) =>
            list.AddElementsAt(sequenceToAdd, index);

        public static List<ElementType> AddElementsAfter<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, Func<ElementType, bool> predicate) =>
            list.AddElementsAt(sequenceToAdd, list.GetElementIndexOrMaxIntValue(predicate));

        public static List<ElementType> AddElementsAfter<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd, ElementType elementToSearch) =>
            list.AddElementsAt(sequenceToAdd, list.GetElementIndexOrMaxIntValue(elementToSearch));
        #endregion

        #region AddElement(s)AtEnd
        public static List<ElementType> AddElementAtEnd<ElementType>(this List<ElementType> list, ElementType element)
        {
            list.Add(element);

            return list;
        }

        public static List<ElementType> AddElementsAtEnd<ElementType>(this List<ElementType> list, IEnumerable<ElementType> sequenceToAdd)
            => list.AddElementsAt(sequenceToAdd, int.MaxValue);
        #endregion
        #endregion

        #region Intern Behaviour(s)
        private static int GetElementIndexOrMaxIntValue<ElementType>(this List<ElementType> list, Func<ElementType, bool> predicate)
        {
            var elementRetrieved = list.FirstOrDefault(predicate);

            return elementRetrieved != null ? list.IndexOf(elementRetrieved) : int.MaxValue;
        }

        private static int GetElementIndexOrMaxIntValue<ElementType>(this List<ElementType> list, ElementType element)
        {
            var index = list.IndexOf(element);

            return index != -1 ? index : int.MaxValue;
        }
        #endregion
    }
}
