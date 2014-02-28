namespace Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Core
    {
        [TestMethod]
        public void SomeHasValue()
        {
            var maybe = Maybe.Some(1);
            Assert.IsTrue(maybe.IsSome);
        }

        [TestMethod]
        public void NoneHasValue()
        {
            var maybe = Maybe.None<int>();
            Assert.IsFalse(maybe.IsSome);
        }

        [TestMethod]
        public void SomeValue()
        {
            var maybe = Maybe.Some(42);
            Assert.AreEqual(42, maybe.Value);
        }

        [TestMethod]
        public void NoneValue()
        {
            var maybe = Maybe.None<int>();
            try
            {
                var value = maybe.Value;
                Assert.Fail("Getting value of None should raise an invalid operation exception.");
            }
            catch (InvalidOperationException)
            {
            }
        }

        [TestMethod]
        public void SomeToString()
        {
            var maybe = Maybe.Some(42);
            var actual = maybe.ToString();
            Assert.AreEqual("Some(42)", actual);
        }

        [TestMethod]
        public void NoneToString()
        {
            var maybe = Maybe.None<int>();
            var actual = maybe.ToString();
            Assert.AreEqual("None", actual);
        }
    }
}
