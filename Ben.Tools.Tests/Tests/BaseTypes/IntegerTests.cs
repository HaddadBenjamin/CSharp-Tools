using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SomeTests
{
    [TestFixture]
    public class IntegerTests
    {
        [TestCase(5, new[] {1, 2, 3, 6}, 3)]
        [TestCase(3, new[] {1, 2, 3, 6}, 2)]
        [TestCase(6, new[] {1, 2, 3, 6}, 3)]
        public void NearestButLower(int integer, IEnumerable<int> numbers, int result)
        {
            Assert.That(integer.FindNearestNumberButLower(numbers), Is.EqualTo(result));
        }

        [TestCase(3, new[] { 1, 2, 6 }, 2)]
        [TestCase(6, new[] { 1, 2, 3, 6 }, 6)]
        public void NearestButLowerOrEqual(int integer, IEnumerable<int> numbers, int result)
        {
            Assert.That(integer.FindNearestNumberButLowerOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(2, new[] { 1, 2, 3, 6 }, 3)]
        [TestCase(3, new[] { 1, 2, 3, 6 }, 6)]
        public void NearestButGreater(int integer, IEnumerable<int> numbers, int result)
        {
            Assert.That(integer.FindNearestNumberButGreater(numbers), Is.EqualTo(result));
        }

        [TestCase(3, new[] { 1, 2, 6 }, 6)]
        [TestCase(6, new[] { 1, 2, 3, 6 }, 6)]
        public void NearestButGreaterOrEqual(int integer, IEnumerable<int> numbers, int result)
        {
            Assert.That(integer.FindNearestNumberButGreaterOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(6, new[] { 1, 2, 4, 7 }, 7)]
        [TestCase(5, new[] { 1, 2, 3, 6 }, 6)]
        public void Nearest(int integer, IEnumerable<int> numbers, int result)
        {
            Assert.That(integer.FindNearestNumberButGreaterOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(0, 100, 25, new[] {0, 25, 50, 75, 100})]
        [TestCase(10, 25, 5, new[] {5, 10, 15, 20, 25})]
        public void GenerateNumbers(int start, int end, int add, IEnumerable<int> results)
        {
            Assert.That(IntHelper.GenerateNumbers(start, end, add).Except(results).Count(), Is.EqualTo(0));
        }
    }
}