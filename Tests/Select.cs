namespace Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Select
    {
        [TestMethod]
        public void Some()
        {
            var some = Maybe.Some(42);
            var selected = some.Select(v => "Value is " + v);
            Assert.AreEqual(Maybe.Some("Value is 42"), selected);
        }

        [TestMethod]
        public void None()
        {
            var none = Maybe.None<int>();
            var selected = none.Select(v => "Value is " + v);
            Assert.AreEqual(Maybe.None<string>(), selected);
        }

        [TestMethod]
        public void SomeWithDefault()
        {
            var some = Maybe.Some(42);
            var selected = some.Select(v => "Value is " + v, () => string.Empty);
            Assert.AreEqual("Value is 42", selected);
        }

        [TestMethod]
        public void NoneWithDefault()
        {
            var none = Maybe.None<int>();
            var selected = none.Select(v => "Value is " + v, () => string.Empty);
            Assert.AreEqual(string.Empty, selected);
        }

        [TestMethod]
        public void SomeWithDefaultValue()
        {
            var some = Maybe.Some(42);
            var selected = some.Select(v => "Value is " + v, string.Empty);
            Assert.AreEqual("Value is 42", selected);
        }

        [TestMethod]
        public void NoneWithDefaultValue()
        {
            var none = Maybe.None<int>();
            var selected = none.Select(v => "Value is " + v, string.Empty);
            Assert.AreEqual(string.Empty, selected);
        }
    }
}
