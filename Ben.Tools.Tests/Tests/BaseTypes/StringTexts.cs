using System;
using System.Collections.Generic;
using System.Linq;
using BenTools.Extensions.BaseTypes;
using BenTools.Helpers.BaseTypes;
using NUnit.Framework;
using Shouldly;

namespace BenTools.Tests.Tests.BaseTypes
{
    [TestFixture]
    public class StringTexts
    {
        [TestCase("a a a  a", "a", new[] {0, 2, 4, 7})]
        public void FindAllTextOccurenceIndices(string text, string searchText, IEnumerable<int> results)
        {
            Assert.That(text.FindAllTextOccurenceIndices(searchText).Except(results).Count(), Is.EqualTo(0));
        }

        [Test]
        public void GuidsToString()
        {
            // Arrange
            var guids = Enumerable.Repeat(0, 10).Select(guid => Guid.NewGuid());
            // Act
            var @string = StringHelper.ToString(guids);

            // Assert
            @string.Length.ShouldBe(360);
        }

        [Test]
        public void StringToGuids()
        {
            // Arrange
            var guids = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
            var @string = StringHelper.ToString(guids);
            // Act
            var expectedGuids = @string.ToGuids().OrderBy(guid => guid);

            // Assert
            guids.ShouldBeSubsetOf(expectedGuids);
        }
    }
}