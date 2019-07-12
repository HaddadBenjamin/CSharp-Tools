using System.Linq;
using BenTools.Extensions.Sequences;
using NUnit.Framework;
using Should;

namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class EnumerableTests
    {
        private class N
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [Test]
        public void MergeByTest()
        {
            var a = new [] 
            {
                new N() { Id = 1, Name = "a" },
                new N() { Id = 2, Name = "b" },
            };

            var b = new[]
            {
                new N() { Id = 2, Name = "new value" },
                new N() { Id = 3, Name = "c" },
            };

            var c = a.MergeBy(b, (element) => element.Id);

            c.Count().ShouldBeSameAs(3);

            var secondElement = c.Skip(1).First();

            secondElement.Name.ShouldBeSameAs("new value");
            secondElement.Id.ShouldBeSameAs(2);
        }

    }
}