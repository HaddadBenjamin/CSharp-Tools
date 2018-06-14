using Ben.Tools.Extensions.BaseTypes;
using Ben.Tools.Extensions.Sequences;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Ben.Tools.Tests
{
    [TestClass]
    public class ShitTests
    {
        class Pet
        {
            public int ID;
            public string Name;
        }

        class Human
        {
            public int ID;
            public string Name;
            public int Pet_id;
        }

        class Shit
        {
            private int a = 2;
        }

        [TestMethod]
        public void ShitTest()
        {
            var collectionA = new[] {1, 2, 3};
            var collectionB = new[] {"a", "b"};
            var zipResult = collectionA.Zip(collectionB, (a, b) => new { a = a, b = b});

            Shit[] shits = new Shit[] { null, null, new Shit() };
            Shit[] nulls = new Shit[] { null, null };
            Shit shit = null;

            Assert.AreEqual(1, shits.RemoveNullElements().Count());

            Assert.AreEqual(null, nulls.GetFirstElementNotNull());
            Assert.AreEqual(true, shits.GetFirstElementNotNull() != null);

            Assert.AreEqual(true, shits.NullIfAllEqual() != null);
            Assert.AreEqual(true, nulls.NullIfAllEqual() == null);

            Assert.AreEqual(true, shit.IsNullGetSpecifiedValueIfNull(new Shit()) != null);
        }
    }
}