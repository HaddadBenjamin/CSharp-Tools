using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SomeTests
{
    [TestFixture]
    public class FloatTests
    {
        [TestCase(5f, new[] { 1f, 2f, 3f, 6f }, 3f)]
        [TestCase(3f, new[] { 1f, 2f, 3f, 6f }, 2f)]
        [TestCase(6f, new[] { 1f, 2f, 3f, 6f }, 3f)]
        public void NearestButLowerf(float @float, IEnumerable<float> numbers, float result)
        {
            Assert.That(@float.FindNearestNumberButLower(numbers), Is.EqualTo(result));
        }

        [TestCase(3f, new[] { 1f, 2f, 6f }, 2f)]
        [TestCase(6f, new[] { 1f, 2f, 3f, 6f }, 6f)]
        public void NearestButLowerOrEqualf(float @float, IEnumerable<float> numbers, float result)
        {
            Assert.That(@float.FindNearestNumberButLowerOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(2f, new[] { 1f, 2f, 3f, 6f }, 3f)]
        [TestCase(3f, new[] { 1f, 2f, 3f, 6f }, 6f)]
        public void NearestButGreaterf(float @float, IEnumerable<float> numbers, float result)
        {
            Assert.That(@float.FindNearestNumberButGreater(numbers), Is.EqualTo(result));
        }

        [TestCase(3f, new[] { 1f, 2f, 6f }, 6f)]
        [TestCase(6f, new[] { 1f, 2f, 3f, 6f }, 6f)]
        public void NearestButGreaterOrEqualf(float integer, IEnumerable<float> numbers, float result)
        {
            Assert.That(integer.FindNearestNumberButGreaterOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(6f, new[] { 1f, 2f, 4f, 7f }, 7f)]
        [TestCase(5f, new[] { 1f, 2f, 3f, 6f }, 6f)]
        public void Nearestf(float @float, IEnumerable<float> numbers, float result)
        {
            Assert.That(@float.FindNearestNumberButGreaterOrEqual(numbers), Is.EqualTo(result));
        }

        [TestCase(0f, 100f, 25f, new[] { 0f, 25f, 50f, 75f, 100f })]
        [TestCase(10f, 25f, 5f, new[] { 5f, 10f, 15f, 20f, 25f })]
        public void GenerateNumbersf(float start, float end, float add, IEnumerable<float> results)
        {
            Assert.That(FloatHelper.GenerateNumbers(start, end, add).Except(results).Count(), Is.EqualTo(0));
        }
    }
}