using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SomeTests
{
    [TestFixture]
    public class StringTexts
    {
        [TestCase("a a a  a", "a", new[] {0, 2, 4, 7})]
        public void FindAllTextOccurenceIndices(string text, string searchText, IEnumerable<int> results)
        {
            Assert.That(text.FindAllTextOccurenceIndices(searchText).Except(results).Count(), Is.EqualTo(0));
        }
    }
}