using BenTools.Extensions.BaseTypes;
using BenTools.Helpers.BaseTypes;
using NUnit.Framework;

namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class NumberTests
    {
        [Test]
        public void TestNumbers()
        {
            Assert.AreEqual(true, IntExtension.IsBetween(36, 25, 50));
            Assert.AreEqual(true, IntExtension.IsBetweenOrEqual(36, 25, 36));
            Assert.AreEqual(true, IntExtension.IsBetweenOrNearlyEqual(36, 25, 35, 1));

            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.Less)(1, 2));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.LessOrEqual)(1, 1));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.LessOrNearlyEqual, 2)(0, 2));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.Equal)(1, 1));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.NearlyEqual, 2)(0, 2));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.More)(2, 1));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.MoreOrEqual)(1, 1));
            Assert.AreEqual(true, IntHelper.OperatorComparerFunction(EComparerOperator.MoreOrNearlyEqual, 5)(5, 0));

            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 5,6}, EComparerOperator.Less));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 2,3,4,5}, EComparerOperator.LessOrEqual));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 2,3,4}, EComparerOperator.LessOrNearlyEqual, 1));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 2,3,4,5}, EComparerOperator.Equal));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 2,3,4}, EComparerOperator.NearlyEqual, 1));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 2,3,4,5 }, EComparerOperator.MoreOrEqual));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(1, new [] { 2,3,4 }, EComparerOperator.MoreOrNearlyEqual, 1));
            Assert.AreEqual(true, IntExtension.DoesAnyVerifyTheComparerCondition(5, new [] { 5,6, 4 }, EComparerOperator.More));

            Assert.AreEqual(true, IntExtension.DoesAllVerifyTheComparerCondition(5, new[] { 7, 6 }, EComparerOperator.Less));
            Assert.AreEqual(true, IntExtension.DoesAllVerifyTheComparerCondition(5, new[] { 7, 6, 5 }, EComparerOperator.LessOrEqual));
            Assert.AreEqual(true, IntExtension.DoesAllVerifyTheComparerCondition(5, new[] { 5, 5 }, EComparerOperator.Equal));
            Assert.AreEqual(true, IntExtension.DoesAllVerifyTheComparerCondition(5, new[] { 5, 3, 4 }, EComparerOperator.MoreOrEqual));
            Assert.AreEqual(true, IntExtension.DoesAllVerifyTheComparerCondition(5, new[] { 4, 3 }, EComparerOperator.More));

            Assert.AreEqual(true, IntExtension.In(5, new[] {1, 2, 3, 4, 5}));
            Assert.AreEqual(false, IntExtension.In(5, new[] {1, 2, 3, 4}));

            //////////////////////////////
            Assert.AreEqual(true, FloatExtension.IsBetween(36, 25, 50));
            Assert.AreEqual(true, FloatExtension.IsBetweenOrEqual(36, 25, 36));
            Assert.AreEqual(true, FloatExtension.IsBetweenOrNearlyEqual(36, 25, 35, 1));

            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.Less)(1, 2));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.LessOrEqual)(1, 1));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.LessOrNearlyEqual, 2)(0, 2));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.Equal)(1, 1));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.NearlyEqual, 2)(0, 2));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.More)(2, 1));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.MoreOrEqual)(1, 1));
            Assert.AreEqual(true, FloatHelper.OperatorComparerFunction(EComparerOperator.MoreOrNearlyEqual, 5)(5, 0));
             
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 5f, 6f }, EComparerOperator.Less));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2f, 3, 4, 5 }, EComparerOperator.LessOrEqual));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2f, 3, 4 }, EComparerOperator.LessOrNearlyEqual, 1));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2f, 3, 4, 5 }, EComparerOperator.Equal));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2f, 3, 4 }, EComparerOperator.NearlyEqual, 1));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2f, 3, 4, 5 }, EComparerOperator.MoreOrEqual));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(1, new[] { 2f, 3, 4 }, EComparerOperator.MoreOrNearlyEqual, 1));
            Assert.AreEqual(true, FloatExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 5f, 6, 4 }, EComparerOperator.More));

            Assert.AreEqual(true, FloatExtension.DoesAllVerifyTheComparerCondition(5, new[] { 7f, 6 }, EComparerOperator.Less));
            Assert.AreEqual(true, FloatExtension.DoesAllVerifyTheComparerCondition(5, new[] { 7f, 6, 5 }, EComparerOperator.LessOrEqual));
            Assert.AreEqual(true, FloatExtension.DoesAllVerifyTheComparerCondition(5, new[] { 5f, 5 }, EComparerOperator.Equal));
            Assert.AreEqual(true, FloatExtension.DoesAllVerifyTheComparerCondition(5, new[] { 5f, 3, 4 }, EComparerOperator.MoreOrEqual));
            Assert.AreEqual(true, FloatExtension.DoesAllVerifyTheComparerCondition(5, new[] { 4f, 3 }, EComparerOperator.More));

            Assert.AreEqual(true, FloatExtension.In(5, new[] { 1f, 2, 3, 4, 5 }));
            Assert.AreEqual(false, FloatExtension.In(5, new[] { 1f, 2, 3, 4 }));

            //////////////////////////////
            Assert.AreEqual(true, DoubleExtension.IsBetween(36, 25, 50));
            Assert.AreEqual(true, DoubleExtension.IsBetweenOrEqual(36, 25, 36));
            Assert.AreEqual(true, DoubleExtension.IsBetweenOrNearlyEqual(36, 25, 35, 1));

            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.Less)(1, 2));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.LessOrEqual)(1, 1));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.LessOrNearlyEqual, 2)(0, 2));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.Equal)(1, 1));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.NearlyEqual, 2)(0, 2));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.More)(2, 1));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.MoreOrEqual)(1, 1));
            Assert.AreEqual(true, DoubleHelper.OperatorComparerFunction(EComparerOperator.MoreOrNearlyEqual, 5)(5, 0));

            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 5d, 6 }, EComparerOperator.Less));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2d, 3, 4, 5 }, EComparerOperator.LessOrEqual));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2d, 3, 4 }, EComparerOperator.LessOrNearlyEqual, 1));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2d, 3, 4, 5 }, EComparerOperator.Equal));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2d, 3, 4 }, EComparerOperator.NearlyEqual, 1));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 2d, 3, 4, 5 }, EComparerOperator.MoreOrEqual));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(1, new[] { 2d, 3, 4 }, EComparerOperator.MoreOrNearlyEqual, 1));
            Assert.AreEqual(true, DoubleExtension.DoesAnyVerifyTheComparerCondition(5, new[] { 5d, 6, 4 }, EComparerOperator.More));

            Assert.AreEqual(true, DoubleExtension.DoesAllVerifyTheComparerCondition(5, new[] { 7d, 6 }, EComparerOperator.Less));
            Assert.AreEqual(true, DoubleExtension.DoesAllVerifyTheComparerCondition(5, new[] { 7d, 6, 5 }, EComparerOperator.LessOrEqual));
            Assert.AreEqual(true, DoubleExtension.DoesAllVerifyTheComparerCondition(5, new[] { 5d, 5 }, EComparerOperator.Equal));
            Assert.AreEqual(true, DoubleExtension.DoesAllVerifyTheComparerCondition(5, new[] { 5d, 3, 4 }, EComparerOperator.MoreOrEqual));
            Assert.AreEqual(true, DoubleExtension.DoesAllVerifyTheComparerCondition(5, new[] { 4d, 3 }, EComparerOperator.More));

            Assert.AreEqual(true, DoubleExtension.In(5, new[] { 1d, 2, 3, 4, 5 }));
            Assert.AreEqual(false, DoubleExtension.In(5, new[] { 1d, 2, 3, 4 }));

            Assert.AreEqual(true, StringExtension.In("test", new []{"toto", "tata", "test"}));
        }
    }
}