using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Linq;
using Ben.Tools.Utilities.Date.DateInterval;
using Ben.Tools.Extensions.Sequences;

namespace Ben.Tools.Tests
{
    /// <summary>
    /// Attention pour que ces tests fonctionnent j'ai dû rajouter [assembly:InternalsVisibleTo("DynamicTest")]  
    /// Du fait GrouppedDateIntervals.MergeWithIntervals() retourne un type anonymes.
    /// Les types anonymes sont internal à l'assembly, c'est à dire que d'autres projets ne peuvent pas y avoir accès.
    /// </summary>
    [TestClass]
    public class DateIntervalTests
    {
        [TestMethod] 
        public void ContainsAny()
        {
            var enumerable = new[] {1, 2, 3};
            var elements = new[] {1, 2, 3};

            var n = enumerable.AddElements(elements);
            var x = enumerable.ToList().AddElements(elements);
            var array = CultureInfo.CurrentCulture.TextInfo.ToTitleCase("my name is pierre");
        }
        [TestMethod]
        public void GrouppedMergeDayOneForDayOneAndTwoForDayTwo()
        {
            var dateTime = new DateTime(2015, 1, 1, 0, 0, 0);
            var datas = new []
            {
                dateTime,
                dateTime.AddMonths(1),
                dateTime.AddDays(1),
                dateTime.AddDays(1).AddMinutes(55)
            };

            var startUtc = dateTime;
            var endUtc = dateTime.AddDays(1);
            var dateIntervals = new GrouppedDateIntervals(startUtc, endUtc, EGrouppedTimeInterval.Day);
            var groups = dateIntervals.MergeWithIntervals(datas, a => a).ToList();

            Assert.AreEqual(2, groups[0].Values.Count());
            Assert.AreEqual(2, groups[1].Values.Count());
        }

        [TestMethod]
        public void GrouppedMergeYearFourTwoFirstYearOneSecondYear()
        {
            var dateTime = new DateTime(2015, 1, 1, 0, 0, 0);
            var datas = new[]
            {
                dateTime,
                dateTime.AddMonths(1),
                dateTime.AddYears(1),
            };

            var startUtc = dateTime;
            var endUtc = dateTime.AddYears(3);
            var dateIntervals = new GrouppedDateIntervals(startUtc, endUtc, EGrouppedTimeInterval.Year);
            var groups = dateIntervals.MergeWithIntervals(datas, a => a).ToList();

            Assert.AreEqual(4, groups.Count);
            Assert.AreEqual(2, groups[0].Values.Count());
            Assert.AreEqual(1, groups[1].Values.Count());
        }

        [TestMethod]
        public void GrouppedMergeDayOfWeekOneForMondayAndTwoForFriday()
        {
            var dateTime = new DateTime(1, 1, 1, 0, 0, 0);
            var datas = new[]
            {
                dateTime,
                dateTime.AddDays(4),
                dateTime.AddDays(11),
            };

            var startUtc = dateTime;
            var endUtc = dateTime.AddYears(3);
            var dateIntervals = new GrouppedDateIntervals(startUtc, endUtc, EGrouppedTimeInterval.DayOfWeek);
            var groups = dateIntervals.MergeWithIntervals(datas, a => a).ToList();

            Assert.AreEqual(1, groups[0].Values.Count());
            Assert.AreEqual(2, groups[4].Values.Count());
        }
    }
}
