namespace Tests.MaybeTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Nullable
    {
        [TestMethod]
        public void SomeFromStruct()
        {
            int? nullableStruct = 42;
            var maybe = Maybe.FromNullable(nullableStruct);
            Assert.AreEqual(Maybe.Some(42), maybe);
        }

        [TestMethod]
        public void NoneFromStruct()
        {
            int? nullableStruct = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var maybe = Maybe.FromNullable(nullableStruct);
            Assert.AreEqual(Maybe.None<int>(), maybe);
        }

        [TestMethod]
        public void SomeFromClass()
        {
            // ReSharper disable once ConvertToConstant.Local
            var nullable = "Hello World";
            var maybe = Maybe.FromNullable(nullable);
            Assert.AreEqual(Maybe.Some("Hello World"), maybe);
        }

        [TestMethod]
        public void NoneFromClass()
        {
            string nullable = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            var maybe = Maybe.FromNullable(nullable);
            Assert.AreEqual(Maybe.None<string>(), maybe);
        }

        [TestMethod]
        public void SomeToStruct()
        {
            var maybe = Maybe.Some(42);
            var nullable = maybe.ToNullable();
            Assert.AreEqual(42, nullable);
        }

        [TestMethod]
        public void NoneToStruct()
        {
            var maybe = Maybe.None<int>();
            var nullable = maybe.ToNullable();
            Assert.AreEqual(null, nullable);
        }
    }
}
