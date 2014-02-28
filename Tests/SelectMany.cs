namespace Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SelectMany
    {
        [TestMethod]
        public void SomeSome()
        {
            var some = Maybe.Some(42);
            var selected = some.SelectMany(v => Maybe.Some("Value is " + v));
            Assert.AreEqual(Maybe.Some("Value is 42"), selected);
        }

        [TestMethod]
        public void NoneSome()
        {
            var none = Maybe.None<int>();
            var selected = none.SelectMany(v => Maybe.Some("Value is " + v));
            Assert.AreEqual(Maybe.None<string>(), selected);
        }

        [TestMethod]
        public void SomeNone()
        {
            var some = Maybe.Some(42);
            var selected = some.SelectMany(v => Maybe.None<string>());
            Assert.AreEqual(Maybe.None<string>(), selected);
        }

        [TestMethod]
        public void NoneNone()
        {
            var none = Maybe.None<int>();
            var selected = none.SelectMany(v => Maybe.None<string>());
            Assert.AreEqual(Maybe.None<string>(), selected);
        }
    }
}
