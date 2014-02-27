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
        public void NoneHasValue()
        {
            var maybe = Maybe.None<int>();
            var result = maybe.Match(
                withSome: v => "yes",
                withNone: () => "no");
            Assert.AreEqual("no", result);
        }
    }
}
