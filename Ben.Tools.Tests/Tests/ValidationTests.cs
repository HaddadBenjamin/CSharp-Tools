using System;
using BenTools.Helpers;
using NUnit.Framework;

/// <summary>
/// Todo :
/// - Localized String.
/// - remove end contract ?
/// </summary>
namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class ValidationTests
    {
        [Test] public void Null() => 
            Assert.Throws<ArgumentNullException>(() => ValidationHelper.NotNull<object>(null, "object"));

        [Test] public void NullOrEmpty() => 
            Assert.Throws<ArgumentException>(() => ValidationHelper.NotNullOrEmpty(string.Empty, "empty string"));

        [Test] public void FillString() => 
            ValidationHelper.NotNullOrEmpty("filled", "filled string");

        [Test] public void InRange() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => ValidationHelper.InRange(4, 1, 3, "value"));

        [Test]
        public void LessOrEqual() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => ValidationHelper.LessThanOrEqualTo(4, 5, "value"));

        [Test] public void GreaterOrEqual() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => ValidationHelper.GreaterThanOrEqualTo(4, 3, "value"));

        [Test] public void True() =>
            Assert.Throws<ArgumentException>(() => ValidationHelper.IsTrue(false, "condition"));
    }
}
