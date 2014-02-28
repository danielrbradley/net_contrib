Yet Another Maybe (YAM)
===========

Yet Another Maybe library for the Microsoft .Net framework.

Why use a Maybe
------------------

If you've not come across the Maybe monad, I'd recommend checking out the [Wikipedia article](http://en.wikipedia.org/wiki/Monad_(functional_programming)#The_Maybe_monad) as well as this article as to [why Maybe is better than null](http://nickknowlson.com/blog/2013/04/16/why-maybe-is-better-than-null/) which covers most of the questions about its usage. Essentially, it's a replacement for null, meaning no more NullReferenceException's!

Getting Started
---------------

Initialize an instance of a maybe Some with a value:

    Maybe.Some(aValue)

and a None without a value:

    Maybe.None<AType>()

Alternatively, you can convert any existing nullable value or object into a maybe such as:

    int? a = 42;
    a.ToMaybe() == Maybe.Some(42)

    int? b = null;
    b.ToMaybe() == Maybe.None<int>()

    string c = "Hello world!";
    c.ToMaybe() == Maybe.Some("Hello world!")

    string d = null;
    d.ToMaybe() == Maybe.None<string>()

You can see if a maybe has a value by checking the "IsSome" or "IsNone" properties:

    if (maybe.IsSome) { ... }
    if (maybe.IsNone) { ... }

Manipulating a Maybe
-----------------------

### Select

Much like using LINQ on enumerable collection, you can use LINQ on a maybe to transform any value that might be inside the maybe.

    Maybe.Some(42).Select(x => x + 1) == Maybe.Some(43)

    Maybe.Some(17).Select(x => x.ToString()) == Maybe.Some("17")

If the source maybe is none, then any result will also be none

    Maybe.None<int>().Select(x => x + 1) == Maybe.None<int>()

    Maybe.None<int>().Select(x => x.ToString) == Maybe.None<string>()

You can also write this using the fluent LINQ syntax

    var y = from x in Maybe.Some(42)
            select x + 1

### ValueOrDefault

A safe way of unboxing a maybe is to use ValueOrDefault, allowing you to specify a default value is the maybe is None.

Getting the value of Some value type:

    Maybe.Some(42).ValueOrDefault() == 42

Getting the default of None value type:
    
    Maybe.None<int>().ValueOrDefault() == 0

Specifying your own default when the maybe is None:

    Maybe.None<int>().ValueOrDefault(-1) == -1

If the creation of the default value is expensive, you can wrap that in a callback:

    Maybe.None<int>().ValueOrDefault(() => new Random().Next()) == a random number

### Match

You can think of this like a glorified if-then-else using lambdas. Both possible branches of the match must have the same return.

Returning a value (func's):

    Maybe.Some("Request").Match(
        withSome: v => v + " Reponse",
        withNone: () => "404 Not Found")

Non returning (actions):

    Maybe.Some("Message").Match(
        withSome: v => ProcessMessage(v),
        withNone: () => throw new Exception())

### Select Many

When the result of a select is another maybe, you end up with a `Maybe<Maybe<T>>`, this can simply be flattened down to `Maybe<T>`.

    Maybe.Some(42).SelectMany(x => x.ToString().ToMaybe()) == Maybe.Some("42")
    Maybe.Some(42).SelectMany(x => Maybe.None<string>()) == Maybe.None<string>()

Select many can also be applied without passing a selector if you are not changing the underlying value type. You can flatten up to 7 levels of maybe at once.

    Maybe.Some(Maybe.Some(42)).SelectMany() == Maybe.Some(42)
