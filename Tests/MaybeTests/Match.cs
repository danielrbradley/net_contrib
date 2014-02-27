namespace Tests.MaybeTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Match
    {
        [TestMethod]
        public void Some()
        {
            var maybe = Maybe.Some(42);
            var result = maybe.Match(
                withSome: v => "yes",
                withNone: () => "no");
            Assert.AreEqual("yes", result);
        }

        [TestMethod]
        public void None()
        {
            var maybe = Maybe.None<int>();
            var result = maybe.Match(
                withSome: v => "yes",
                withNone: () => "no");
            Assert.AreEqual("no", result);
        }

        [TestMethod]
        public void SomeWithoutReturn()
        {
            var wasWithSomeExecuted = false;
            var maybe = Maybe.Some(42);
            maybe.Match(
                withSome: v => { wasWithSomeExecuted = true; },
                withNone: Assert.Fail);
            Assert.IsTrue(wasWithSomeExecuted);
        }

        [TestMethod]
        public void NoneWithoutReturn()
        {
            var wasWithNoneExecuted = false;
            var maybe = Maybe.None<int>();
            maybe.Match(
                withSome: v => Assert.Fail(),
                withNone: () => wasWithNoneExecuted = true);
            Assert.IsTrue(wasWithNoneExecuted);
        }
    }
}
