using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Extensions.Sequences
{
    public static class ListExtension
    {
        #region Element(s) insertion
        private static int GetElementIndexOrMaxIntValue<ElementType>(this List<ElementType> list, Func<ElementType, bool> predicate)
        {
            var elementRetrieved = list.FirstOrDefault(predicate);

            return elementRetrieved != null ? list.IndexOf(elementRetrieved) : int.MaxValue;
        }

        public static List<ElementType> AddElementAtBegin<ElementType>(this List<ElementType> list, ElementType element)
        {
            list.Insert(0, element);

            return list;
        }

        public static List<ElementType> AddElementsAtBegin<ElementType>(this List<ElementType> list, List<ElementType> listToAdd)
            => list.AddElementsAt(listToAdd, 0);

        public static List<ElementType> AddElementBefore<ElementType>(this List<ElementType> list, ElementType element, int index) =>
            list.AddElementAt(element, index - 1);

        public static List<ElementType> AddElementBefore<ElementType>(this List<ElementType> list, ElementType element, Func<ElementType, bool> predicate) =>
        list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(predicate) - 1);

        public static List<ElementType> AddElementsBefore<ElementType>(this List<ElementType> list, List<ElementType> listToAdd, int index) =>
            list.AddElementsAt(listToAdd, index - 1);

        public static List<ElementType> AddElementsBefore<ElementType>(this List<ElementType> list, List<ElementType> listToAdd, Func<ElementType, bool> predicate) =>
            list.AddElementsAt(listToAdd, list.GetElementIndexOrMaxIntValue(predicate) - 1);

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

        public static List<ElementType> AddElementsAt<ElementType>(this List<ElementType> list, List<ElementType> listToAdd, int index)
        {
            if (index <= 0)
            {
                listToAdd.AddRange(list);

                return listToAdd;
            }
            if (index > list.Count())
            {
                list.AddRange(listToAdd);

                return list;
            }

            var firstPartOfList = list.WhereIndexIsAtMost(index - 1).ToList();
            var secondPartOfList = list.WhereIndexIsAtLeast(index).ToList();

            firstPartOfList.AddRange(listToAdd);
            firstPartOfList.AddRange(secondPartOfList);

            return firstPartOfList;
        }

        public static List<ElementType> AddElementsAt<ElementType>(this List<ElementType> list, List<ElementType> listToAdd, Func<ElementType, bool> predicate) =>
            list.AddElementsAt(listToAdd, list.GetElementIndexOrMaxIntValue(predicate));

        public static List<ElementType> AddElementAfter<ElementType>(this List<ElementType> list, ElementType element, int index) =>
            list.AddElementAt(element, index + 1);

        public static List<ElementType> AddElementAfter<ElementType>(this List<ElementType> list, ElementType element, Func<ElementType, bool> predicate) =>
        list.AddElementAt(element, list.GetElementIndexOrMaxIntValue(predicate) + 1);

        public static List<ElementType> AddElementsAfter<ElementType>(this List<ElementType> list, List<ElementType> listToAdd, int index) =>
            list.AddElementsAt(listToAdd, index + 1);

        public static List<ElementType> AddElementsAfter<ElementType>(this List<ElementType> list, List<ElementType> listToAdd, Func<ElementType, bool> predicate) =>
            list.AddElementsAt(listToAdd, list.GetElementIndexOrMaxIntValue(predicate) + 1);

        public static List<ElementType> AddElementAtEnd<ElementType>(this List<ElementType> list, ElementType element)
        {
            list.Add(element);

            return list;
        }

        public static List<ElementType> AddElementsAtEnd<ElementType>(this List<ElementType> list, List<ElementType> listToAdd)
            => list.AddElementsAt(listToAdd, int.MaxValue);
        #endregion
    }
}