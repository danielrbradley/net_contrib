namespace Tests.MaybeTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class Flatten
    {
        [TestMethod]
        public void SomeSome()
        {
            var maybeMaybe = Maybe.Some(Maybe.Some(42));
            var maybe = maybeMaybe.Flatten();
            Assert.AreEqual(Maybe.Some(42), maybe);
        }

        [TestMethod]
        public void SomeNone()
        {
            var maybeMaybe = Maybe.Some(Maybe.None<int>());
            var maybe = maybeMaybe.Flatten();
            Assert.AreEqual(Maybe.None<int>(), maybe);
        }

        [TestMethod]
        public void NoneNone()
        {
            var maybeMaybe = Maybe.None<Maybe<int>>();
            var maybe = maybeMaybe.Flatten();
            Assert.AreEqual(Maybe.None<int>(), maybe);
        }

        [TestMethod]
        public void Nesting3Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(42)));
            var flattened = nested.Flatten();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting4Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42))));
            var flattened = nested.Flatten();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting5Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42)))));
            var flattened = nested.Flatten();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting6Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42))))));
            var flattened = nested.Flatten();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting7Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42)))))));
            var flattened = nested.Flatten();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }
    }
}
