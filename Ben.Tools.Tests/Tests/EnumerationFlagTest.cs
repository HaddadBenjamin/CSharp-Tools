using System;
using System.Collections.Generic;
using System.Linq;
using Ben.Tools.Extensions.BaseTypes;
using Ben.Tools.Extensions.Sequences;
using Ben.Tools.Helpers.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ben.Tools.Tests
{
    [Flags]
    enum TestEnumeration
    {
        A = 1,
        B = 2,
        C = 4,
        D = 8,
        E = 16
    }

    [TestClass]
    public class EnumerationFlagTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var sevenEnum = TestEnumeration.A | TestEnumeration.B | TestEnumeration.C;
            var sevenEnumSplitted = EnumerationFlagsHelper.ToEnumerations(sevenEnum);
            var sevenEnums = Enumerable.Repeat(sevenEnum, 3);
            var sevenInts = new List<int>{ 1, 2, 4};
            var sevenTexts = new List<string> {"A", "B", "C"};


            Assert.AreEqual(7, EnumerationFlagsHelper.ToInteger(sevenEnum));
            Assert.AreEqual(7, EnumerationFlagsHelper.ToInteger(sevenEnums));
            Assert.AreEqual(7, EnumerationFlagsHelper.ToInt(sevenInts));
            Assert.AreEqual(7, EnumerationFlagsHelper.ToInteger<TestEnumeration>(sevenTexts));

            Assert.AreEqual(7, EnumerationFlagsHelper.ToInts(sevenEnum).Sum());
            Assert.AreEqual(7, EnumerationFlagsHelper.ToIntegers<TestEnumeration>(sevenTexts).Sum());
            Assert.AreEqual(7, EnumerationFlagsHelper.ToIntegers(sevenEnumSplitted).Sum());


            Assert.AreEqual(sevenEnum, EnumerationFlagsHelper.ToEnumeration(sevenEnumSplitted));
            Assert.AreEqual(sevenEnum, EnumerationFlagsHelper.ToEnumeration<TestEnumeration>(7));
            Assert.AreEqual(sevenEnum, EnumerationFlagsHelper.ToEnumeration<TestEnumeration>(sevenInts));
            Assert.AreEqual(sevenEnum, EnumerationFlagsHelper.ToEnumeration<TestEnumeration>(sevenTexts));

            Assert.AreEqual(true, EnumerationFlagsHelper.ToEnumerations(sevenEnum).ContainsAll(sevenEnumSplitted));
            Assert.AreEqual(true, EnumerationFlagsHelper.ToEnumerations<TestEnumeration>(7).ContainsAll(sevenEnumSplitted));
            Assert.AreEqual(true, EnumerationFlagsHelper.ToEnumerations<TestEnumeration>(sevenTexts).ContainsAll(sevenEnumSplitted));
            Assert.AreEqual(true, EnumerationFlagsHelper.ToEnumerations<TestEnumeration>(sevenInts).ContainsAll(sevenEnumSplitted));


            Assert.AreEqual(true, EnumerationFlagsHelper.ToStrings(sevenEnum).ContainsAll(sevenTexts));
            Assert.AreEqual(true, EnumerationFlagsHelper.ToStrings<TestEnumeration>(7).ContainsAll(sevenTexts));
            Assert.AreEqual(true, EnumerationFlagsHelper.ToStrings(sevenEnumSplitted).ContainsAll(sevenTexts));
            Assert.AreEqual(true, EnumerationFlagsHelper.ToStrings<TestEnumeration>(sevenInts).ContainsAll(sevenTexts));


            Assert.AreEqual(true, EnumerationFlagsHelper.DoesFlagIsEnable(7, 1));
            Assert.AreEqual(true, EnumerationFlagsHelper.DoesFlagIsEnable(sevenEnum, TestEnumeration.A));
            Assert.AreEqual(true, EnumerationFlagsHelper.DoesFlagIsEnable(sevenEnum, "A"));
            Assert.AreEqual(true, EnumerationFlagsHelper.DoesFlagsAreEnable(sevenEnum, sevenEnumSplitted));
            Assert.AreEqual(true, EnumerationFlagsHelper.DoesFlagsAreEnable(sevenEnum, sevenInts));
            Assert.AreEqual(true, EnumerationFlagsHelper.DoesFlagsAreEnable(sevenEnum, sevenTexts));


            Assert.AreEqual(7, EnumerationFlagsHelper.AddFlag(1, 6));
            Assert.AreEqual(7, (int)EnumerationFlagsHelper.AddFlag(TestEnumeration.A, TestEnumeration.B | TestEnumeration.C));
            Assert.AreEqual(3, (int)EnumerationFlagsHelper.AddFlag(TestEnumeration.A, "B"));

            Assert.AreEqual(7, EnumerationFlagsHelper.AddFlags(1, new []{ 2, 4 }));
            Assert.AreEqual(7, (int)EnumerationFlagsHelper.AddFlags(TestEnumeration.A, new[] { TestEnumeration.B, TestEnumeration.C}));
            Assert.AreEqual(7, (int)EnumerationFlagsHelper.AddFlags(TestEnumeration.A, new []{ "B", "C"}));


            Assert.AreEqual(1, EnumerationFlagsHelper.RemoveFlag(7, 6));
            Assert.AreEqual(1, (int)EnumerationFlagsHelper.RemoveFlag(sevenEnum, TestEnumeration.B | TestEnumeration.C));
            Assert.AreEqual(5, (int)EnumerationFlagsHelper.RemoveFlag(sevenEnum, "B"));

            Assert.AreEqual(1, EnumerationFlagsHelper.RemoveFlags(7, new[] { 2, 4 }));
            Assert.AreEqual(1, (int)EnumerationFlagsHelper.RemoveFlags(sevenEnum, new[] { TestEnumeration.B, TestEnumeration.C }));
            Assert.AreEqual(1, (int)EnumerationFlagsHelper.RemoveFlags(sevenEnum, new[] { "B", "C" }));


            Assert.AreEqual(7, EnumerationFlagsHelper.CombineFlag(1.ToEnumerable(), 6));
            Assert.AreEqual(7, (int)EnumerationFlagsHelper.CombineFlag(TestEnumeration.A.ToEnumerable(), TestEnumeration.B | TestEnumeration.C));
            Assert.AreEqual(3, (int)EnumerationFlagsHelper.CombineFlag(TestEnumeration.A.ToEnumerable(), "B"));

            Assert.AreEqual(7, EnumerationFlagsHelper.CombineFlags(1.ToEnumerable(), new[] { 2, 4 }));
            Assert.AreEqual(7, (int)EnumerationFlagsHelper.CombineFlags(TestEnumeration.A.ToEnumerable(), new[] { TestEnumeration.B, TestEnumeration.C }));
            Assert.AreEqual(7, (int)EnumerationFlagsHelper.CombineFlags(TestEnumeration.A.ToEnumerable(), new[] { "B", "C" }));

        }
    }
}