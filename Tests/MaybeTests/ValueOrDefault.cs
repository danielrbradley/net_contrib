namespace Tests.MaybeTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValueOrDefault
    {
        [TestMethod]
        public void SomeValueType()
        {
            var some = Maybe.Some(42);
            var value = some.ValueOrDefault();
            Assert.AreEqual(42, value);
        }

        [TestMethod]
        public void NoneValueType()
        {
            var none = Maybe.None<int>();
            var value = none.ValueOrDefault();
            Assert.AreEqual(0, value);
        }

        [TestMethod]
        public void SomeReferenceType()
        {
            var some = Maybe.Some("Hello world");
            var value = some.ValueOrDefault();
            Assert.AreEqual("Hello world", value);
        }

        [TestMethod]
        public void NoneReferenceType()
        {
            var none = Maybe.None<string>();
            var value = none.ValueOrDefault();
            Assert.AreEqual(null, value);
        }

        [TestMethod]
        public void SomeValueTypeDefaultValue()
        {
            var some = Maybe.Some(42);
            var value = some.ValueOrDefault(13);
            Assert.AreEqual(42, value);
        }

        [TestMethod]
        public void NoneValueTypeDefaultValue()
        {
            var none = Maybe.None<int>();
            var value = none.ValueOrDefault(13);
            Assert.AreEqual(13, value);
        }

        [TestMethod]
        public void SomeReferenceTypeDefaultValue()
        {
            var some = Maybe.Some("Hello world");
            var value = some.ValueOrDefault(string.Empty);
            Assert.AreEqual("Hello world", value);
        }

        [TestMethod]
        public void NoneReferenceTypeDefaultValue()
        {
            var none = Maybe.None<string>();
            var value = none.ValueOrDefault(string.Empty);
            Assert.AreEqual(string.Empty, value);
        }

        [TestMethod]
        public void SomeValueTypeDefaultValueFunc()
        {
            var some = Maybe.Some(42);
            var value = some.ValueOrDefault(() => 13);
            Assert.AreEqual(42, value);
        }

        [TestMethod]
        public void NoneValueTypeDefaultValueFunc()
        {
            var none = Maybe.None<int>();
            var value = none.ValueOrDefault(() => 13);
            Assert.AreEqual(13, value);
        }

        [TestMethod]
        public void SomeReferenceTypeDefaultValueFunc()
        {
            var some = Maybe.Some("Hello world");
            var value = some.ValueOrDefault(() => string.Empty);
            Assert.AreEqual("Hello world", value);
        }

        [TestMethod]
        public void NoneReferenceTypeDefaultValueFunc()
        {
            var none = Maybe.None<string>();
            var value = none.ValueOrDefault(() => string.Empty);
            Assert.AreEqual(string.Empty, value);
        }
    }
}
