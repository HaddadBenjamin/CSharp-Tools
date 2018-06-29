using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace SomeTests
{
    [TestFixture]
    public class DoubleTests
    {
        [TestCase(5d, new[] { 1d, 2d, 3d, 6d }, 3d)]
        [TestCase(3d, new[] { 1d, 2d, 3d, 6d }, 2d)]
        [TestCase(6d, new[] { 1d, 2d, 3d, 6d }, 3d)]
        public void NearestButLowerd(double @double, IEnumerable<double> numbers, double result)
        {
            Assert.That(@double.FindNearestNumberButLower(numbers), Is.EqualTo(result));
        }

        [TestCase(3d, new[] { 1d, 2d, 6d }, 2d)]
        [TestCase(6d, new[] { 1d, 2d, 3d, 6d }, 6d)]
        public void NearestButLowerOrEquald(double @double, IEnumerable<double> numbers, double result)
        {
            Assert.That(@double.FindNearestNumberButLowerOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(2d, new[] { 1d, 2d, 3d, 6d }, 3d)]
        [TestCase(3d, new[] { 1d, 2d, 3d, 6d }, 6d)]
        public void NearestButGreaterd(double @double, IEnumerable<double> numbers, double result)
        {
            Assert.That(@double.FindNearestNumberButGreater(numbers), Is.EqualTo(result));
        }

        [TestCase(3d, new[] { 1d, 2d, 6d }, 6d)]
        [TestCase(6d, new[] { 1d, 2d, 3d, 6d }, 6d)]
        public void NearestButGreaterOrEquald(double @double, IEnumerable<double> numbers, double result)
        {
            Assert.That(@double.FindNearestNumberButGreaterOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(6d, new[] { 1d, 2d, 4d, 7d }, 7d)]
        [TestCase(5d, new[] { 1d, 2d, 3d, 6d }, 6d)]
        public void Nearestd(double @double, IEnumerable<double> numbers, double result)
        {
            Assert.That(@double.FindNearestNumberButGreaterOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(0d, 100d, 25d, new[] { 0d, 25d, 50d, 75d, 100d })]
        [TestCase(10d, 25d, 5d, new[] { 5d, 10d, 15d, 20d, 25d })]
        public void GenerateNumbersd(double start, double end, double add, IEnumerable<double> results)
        {
            Assert.That(DoubleHelper.GenerateNumbers(start, end, add).Except(results).Count(), Is.EqualTo(0));
        }
    }
}