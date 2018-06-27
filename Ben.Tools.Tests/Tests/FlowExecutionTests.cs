using System;
using System.Collections.Generic;
using BenTools.Helpers;
using NUnit.Framework;

namespace BenTools.Tests.Tests
{
    [TestFixture]
    public class FlowExecutionTests
    {
        [Test]
        public void Ternaries_Tests()
        {
            Assert.AreEqual(5, FlowExecutionHelper.Ternaries(
                (false, 2),
                (false, 3),
                (false, 5)));

            Assert.AreEqual(1, FlowExecutionHelper.Ternaries(
                (true, 1),
                (false, 5)));

            Assert.AreEqual(10, FlowExecutionHelper.Ternaries(
                (false, 1),
                (true, 10)));
        }

        [Test]
        public void IfsTree_Tests()
        {
            var value = 0;

            FlowExecutionHelper.IfsTree(
                new IfTree
                (
                    new (bool, Action, IfTree)[]
                    {
                        (false, () => value = 1, default(IfTree)),
                        (false, () => value = 2, default(IfTree))
                    },  default((Action, IfTree))
                )
            );

            Assert.AreEqual(0, value);

            FlowExecutionHelper.IfsTree(
                new IfTree
                (
                    new (bool, Action, IfTree)[]
                    {
                        (true, () => value = 1, new IfTree
                        (
                            new (bool, Action, IfTree)[]
                            {
                                (true, () => value = 5, default(IfTree)),
                            },  default((Action, IfTree))
                        )),
                    },  default((Action, IfTree))
                )
            );

            Assert.AreEqual(5, value);

            FlowExecutionHelper.IfsTree(
                new IfTree
                (
                    new (bool, Action, IfTree)[] { },
                    (() => value = 2, new IfTree
                    (
                        new (bool, Action, IfTree)[]
                        {
                            (true, () => value = 1, new IfTree
                            (
                                new (bool, Action, IfTree)[]
                                {
                                    (true, () => value = 10, default(IfTree)),
                                },  default((Action, IfTree))
                            )),
                        },  default((Action, IfTree))
                    ))
                )
            );

            Assert.AreEqual(10, value);
        }

        [Test]
        public void ElseIf_Test()
        {
            var value = 0;

            FlowExecutionHelper.Ifs(new (bool, Action)[]
            {
                (false, () => value = 1),
                (true, () => value = 2)
            },  () => value = 3);

            Assert.AreEqual(2, value);
        }

        [Test]
        public void Else_Test()
        {
            var value = 0;

            FlowExecutionHelper.Ifs(new (bool, Action)[]
            {
                (false, () => value = 1),
                (false, () => value = 2)
            },  () => value = 3);

            Assert.AreEqual(3, value);
        }

        [Test]
        public void Do_False_Test()
        {
            var sequence = new List<int>();

            FlowExecutionHelper.Do((index) => sequence.Add(1), (index) => false);

            Assert.AreEqual(1, sequence.Count);
        }

        [Test]
        public void Do_True_Test()
        {
            var sequence = new List<int>();

            FlowExecutionHelper.Do((index) => sequence.Add(1), (index) => index < 20);

            Assert.AreEqual(20, sequence.Count);
        }

        [Test]
        public void For_Test()
        {
            var sequence = new List<int>();

            FlowExecutionHelper.For(10, (index) => sequence.Add(index));

            Assert.AreEqual(10, sequence.Count);
        }
    }
}
