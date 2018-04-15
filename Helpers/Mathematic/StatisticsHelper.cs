using System;
using System.Collections.Generic;
using System.Linq;

namespace Ben.Tools.Helpers.Mathematic
{
    public class StatisticsAverageData
    {
         
        public StatisticsAverageData(
            double count,
            double average)
        {
            Count = count;
            Average = average;
        }

        public double Count { get; set; }

        public double Average { get; set; }
    }

    /// <summary>
    /// [Count  Average]
    /// [128    223]
    /// [112    236]        =>  La moyenne des valeurs (Average) sur ces 662 éléments est de : 206.589.
    /// [422    194]     
    /// </summary>
    public static class StatisticsHelper
    {
        public static double Average(IEnumerable<StatisticsAverageData> averageDatas)
        {
            var totalCount = averageDatas.Sum(averageData => averageData.Count);

            return averageDatas.Select(averageData => averageData.Average * (averageData.Count / totalCount)).Sum();
        }

        public static double Average<CustomType>(
            IEnumerable<CustomType> enumerable,
            Func<CustomType, double> getCountFunction,
            Func<CustomType, double> getAverageFunction)
        {
            var averageDatas = enumerable.Select(element => new StatisticsAverageData(
                average: getAverageFunction(element),
                count: getCountFunction(element)));

            return Average(averageDatas);
        }
    }
    
}