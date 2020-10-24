using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace BenTools.Helpers.Sequences
{
    internal static class SortHelper<TypeToSort>
    {
        public static void FindTheFastestSortThenDisplayAllTheirTimes<OtherTypeToSort>(OtherTypeToSort[] datas) where OtherTypeToSort : new()
        {
            var ticks = long.MaxValue;
            var fastestSort = string.Empty;
            var sortHistory = string.Empty;
            var stopwatch = new Stopwatch();
            var tmpDatas = new TypeToSort[datas.Length];

            DoSortAndLogTime(MergeSort, "Merge");
            DoSortAndLogTime(QuickSort, "Quick");
            DoSortAndLogTime(HeapSort, "Heap");

            void DoSortAndLogTime(Action<TypeToSort[]> sort, string sortName)
            {
                Array.Copy(datas, tmpDatas, datas.Length);
                stopwatch.Start();
                sort(tmpDatas);
                stopwatch.Stop();
                if (ticks > stopwatch.ElapsedTicks)
                {
                    ticks = stopwatch.ElapsedTicks;
                    fastestSort = sortName;
                }

                var timeSpan = stopwatch.Elapsed;

                sortHistory += $"{sortName} sort time : {(timeSpan.Minutes * 60) + timeSpan.Seconds}.{timeSpan.ToString("fff")}s";
                stopwatch.Reset();
            }

            Console.WriteLine($"The fastest sort is {fastestSort} and cost {ticks} tickets.{Environment.NewLine}*****History*****{Environment.NewLine}{sortHistory}");
        }

        #region Merge
        public static void MergeSort(TypeToSort[] datas) => MergeSortRecursive(datas, 0, datas.Length - 1);

        private static void MergeSortRecursive(TypeToSort[] datas, int left, int right)
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                MergeSortRecursive(datas, left, mid);
                MergeSortRecursive(datas, mid + 1, right);
                MergeAlgorithm(datas, left, mid + 1, right);
            }
        }

        private static void MergeAlgorithm(TypeToSort[] datas, int left, int mid, int right)
        {
            var temp = new TypeToSort[datas.Length];
            int i, left_end, num_elements, tmp_pos;

            left_end = mid - 1;
            tmp_pos = left;
            num_elements = right - left + 1;

            while (left <= left_end && mid <= right)
                temp[tmp_pos++] = Comparer<TypeToSort>.Default.Compare(datas[left], datas[mid]) <= 0
                    ? datas[left++]
                    : datas[mid++];

            while (left <= left_end)
                temp[tmp_pos++] = datas[left++];

            while (mid <= right)
                temp[tmp_pos++] = datas[mid++];

            for (i = 0; i < num_elements; i++)
            {
                datas[right] = temp[right];
                right--;
            }
        }
        #endregion

        #region Heap
        public static void HeapSort(TypeToSort[] datas)
        {
            var heapSize = datas.Length;

            for (var p = (heapSize - 1) / 2; p >= 0; p--)
                HeapSortAlgorithm(datas, heapSize, p);

            for (var i = datas.Length - 1; i > 0; i--)
            {
                Swap(datas, i, 0);

                heapSize--;
                HeapSortAlgorithm(datas, heapSize, 0);
            }
        }

        private static void HeapSortAlgorithm(TypeToSort[] datas, int heapSize, int index)
        {
            var left = (index + 1) * 2 - 1;
            var right = (index + 1) * 2;
            var largest = left < heapSize && Comparer<TypeToSort>.Default.Compare(datas[left], datas[index]) > 0 ? left : index;

            if (right < heapSize && Comparer<TypeToSort>.Default.Compare(datas[right], datas[largest]) > 0)
                largest = right;

            if (largest != index)
            {
                Swap(datas, index, largest);

                HeapSortAlgorithm(datas, heapSize, largest);
            }
        }
        #endregion

        #region Quick
        public static void QuickSort(TypeToSort[] datas) => QuickSortRecursive(datas, 0, datas.Length - 1);

        private static void QuickSortRecursive(TypeToSort[] arr, int left, int right)
        {
            if (left < right)
            {
                var pivot = QuickSortAlgorithm(arr, left, right);

                if (pivot > 1)
                    QuickSortRecursive(arr, left, pivot - 1);

                if (pivot + 1 < right)
                    QuickSortRecursive(arr, pivot + 1, right);
            }
        }

        public static int QuickSortAlgorithm(TypeToSort[] datas, int left, int right)
        {
            var pivot = datas[left];

            while (true)
            {
                while (Comparer<TypeToSort>.Default.Compare(datas[left], pivot) < 0)
                    left++;

                while (Comparer<TypeToSort>.Default.Compare(datas[right], pivot) > 0)
                    right--;

                if (left < right)
                    Swap(datas, left, right);
                else
                    return right;
            }
        }
        #endregion

        private static void Swap<TypeToSwap>(TypeToSwap[] datas, int indexOfFirstData, int indexOfSecondData)
        {
            var temp = datas[indexOfFirstData];

            datas[indexOfFirstData] = datas[indexOfSecondData];
            datas[indexOfSecondData] = temp;
        }
    }
}