namespace Tests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SelectMany
    {
        [TestMethod]
        public void SomeSomeSelector()
        {
            var some = Maybe.Some(42);
            var selected = some.SelectMany(v => Maybe.Some("Value is " + v));
            Assert.AreEqual(Maybe.Some("Value is 42"), selected);
        }

        [TestMethod]
        public void NoneSomeSelector()
        {
            var none = Maybe.None<int>();
            var selected = none.SelectMany(v => Maybe.Some("Value is " + v));
            Assert.AreEqual(Maybe.None<string>(), selected);
        }

        [TestMethod]
        public void SomeNoneSelector()
        {
            var some = Maybe.Some(42);
            var selected = some.SelectMany(v => Maybe.None<string>());
            Assert.AreEqual(Maybe.None<string>(), selected);
        }

        [TestMethod]
        public void NoneNoneSelector()
        {
            var none = Maybe.None<int>();
            var selected = none.SelectMany(v => Maybe.None<string>());
            Assert.AreEqual(Maybe.None<string>(), selected);
        }

        [TestMethod]
        public void SomeSome()
        {
            var maybeMaybe = Maybe.Some(Maybe.Some(42));
            var maybe = maybeMaybe.SelectMany();
            Assert.AreEqual(Maybe.Some(42), maybe);
        }

        [TestMethod]
        public void SomeNone()
        {
            var maybeMaybe = Maybe.Some(Maybe.None<int>());
            var maybe = maybeMaybe.SelectMany();
            Assert.AreEqual(Maybe.None<int>(), maybe);
        }

        [TestMethod]
        public void NoneNone()
        {
            var maybeMaybe = Maybe.None<Maybe<int>>();
            var maybe = maybeMaybe.SelectMany();
            Assert.AreEqual(Maybe.None<int>(), maybe);
        }

        [TestMethod]
        public void LinqSomeSome()
        {
            var result = from a in Maybe.Some(17)
                         from b in Maybe.Some(42)
                         select a + b;
            Assert.AreEqual(Maybe.Some(59), result);
        }

        [TestMethod]
        public void LinqSomeNone()
        {
            var result = from a in Maybe.Some(17)
                         from b in Maybe.None<int>()
                         select a + b;
            Assert.AreEqual(Maybe.None<int>(), result);
        }

        [TestMethod]
        public void LinqNoneSome()
        {
            var result = from a in Maybe.None<int>()
                         from b in Maybe.Some(42)
                         select a + b;
            Assert.AreEqual(Maybe.None<int>(), result);
        }

        [TestMethod]
        public void LinqNoneNone()
        {
            var result = from a in Maybe.None<int>()
                         from b in Maybe.None<int>()
                         select a + b;
            Assert.AreEqual(Maybe.None<int>(), result);
        }

        [TestMethod]
        public void LinqMixedTypes()
        {
            var result = from a in Maybe.Some("Hello world")
                         from b in Maybe.Some(42)
                         select string.Join(" ", a, b);
            Assert.AreEqual(Maybe.Some("Hello world 42"), result);
        }

        [TestMethod]
        public void Linq3Levels()
        {
            var result = from a in Maybe.Some(17)
                         from b in Maybe.Some(42)
                         from c in Maybe.Some(53)
                         select a + b + c;
            Assert.AreEqual(Maybe.Some(112), result);
        }

        [TestMethod]
        public void Nesting3Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(42)));
            var flattened = nested.SelectMany();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting4Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42))));
            var flattened = nested.SelectMany();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting5Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42)))));
            var flattened = nested.SelectMany();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting6Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42))))));
            var flattened = nested.SelectMany();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }

        [TestMethod]
        public void Nesting7Levels()
        {
            var nested = Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(Maybe.Some(42)))))));
            var flattened = nested.SelectMany();
            Assert.AreEqual(Maybe.Some(42), flattened);
        }
    }
}
