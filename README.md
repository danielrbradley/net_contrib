Yet Another Maybe (YAM)
===========

Yet Another Maybe library for the Microsoft .Net framework.

What is a Maybe?
------------------

A Maybe is a descriptive way of saying a result might, or might not return a value. 

When we want a method to only optionally return a value, we describe this as saying we *maybe* return a value, meaning that we will return *Some* value or *None*.

This library is just another attempt at creating a high quality, tested implementation of this pattern for C#.

If you've not come across the Maybe monad, I'd recommend checking out the [Wikipedia article](http://en.wikipedia.org/wiki/Monad_(functional_programming)#The_Maybe_monad) as well as this article as to [why Maybe is better than null](http://nickknowlson.com/blog/2013/04/16/why-maybe-is-better-than-null/) which covers most of the questions about its usage. Essentially, it's a more expressive option instead of using null, which leads to much fewer NullReferenceException's in day-to-day working!



Getting Started
---------------

To create a Maybe *with* a value, just pass the value to `Maybe.Some(...)`

**Example 1**

    var aValue = "Hello, world!"
    var maybe = Maybe.Some(aValue);
    Console.WriteLine(maybe)
    
**Output**

    Some(Hello, world!)

To create a Maybe *without* a value call `Maybe.None<...>()`, specifying the type you would have returned.

** Example 2**

    var maybe = Maybe.None<string>();
    Console.WriteLine(maybe);

**Output**
    None



LINQ
----

YAM integrates really nicely with [LINQ](http://msdn.microsoft.com/en-us/library/bb397676.aspx), allowing you to use the fluent syntax to your advantage.

*Side note: A maybe is equivalent to a collection which is limited to either 0 or 1 items. None represents an empty list, and Some represents a list with only 1 item.*

### Projection Operations

> Projection refers to the operation of transforming an object into a new form that often consists only of those 
> properties that will be subsequently used. By using projection, you can construct a new type that is built from 
> each object. You can project a property and perform a mathematical function on it. You can also project the 
> original object without changing it.
>
> -- <cite>[MSDN](http://msdn.microsoft.com/en-us/library/bb546168.aspx)</cite>

#### Select Syntax Example

    var maybeHello = Maybe.Some("Hello");
    
    var maybeHelloWorld =
        from hello in maybeHello
        select hello + ", world!";
    
    Console.WriteLine(maybeHelloWorld)

**Output**

    Some(Hello, world!)

#### Select Many Syntax Example

    var maybeHello = Maybe.Some("Hello");
    var maybeWorld = Maybe.Some("world!");
    
    var maybeHelloWorld =
        from hello in maybeHello
        from world in maybeWorld
        select hello + ", " + world;
    
    Console.WriteLine(maybeHelloWorld)

**Output**

    Some(Hello, world!)

#### Selecting when a value is None

If any of the inputs to a LINQ select statement are *None*, then the result will also be *None*.

**Example**

    var maybeHello = Maybe.Some("Hello");
    var maybeWorld = Maybe.None<string>();
    
    var maybeHelloWorld =
        from hello in maybeHello
        from world in maybeWorld
        select hello + ", " + world;
    
    Console.WriteLine(maybeHelloWorld)

**Output**

    None

Advanced Techniques
-----------------------

### Properties

You can see if a maybe has a value by checking the "IsSome" or "IsNone" properties:

    if (maybe.IsSome) { ... }
    if (maybe.IsNone) { ... }

You can get to the value directly via the 'Value' property:

    maybe.Value

**Important:** *If you try to access the value of None, an InvalidOperationException will be thrown.*


### ValueOrDefault

A safer way of unboxing a maybe is to use the `ValueOrDefault()` extension method, allowing you to specify a default value if the maybe is None.

Getting the value of Some value type:

    Maybe.Some(42).ValueOrDefault() == 42

Getting the default of None value type:
    
    Maybe.None<int>().ValueOrDefault() == 0

Specifying your own default when the maybe is None:

    Maybe.None<int>().ValueOrDefault(-1) == -1

If the creation of the default value is expensive, you can wrap that in a callback:

    Maybe.None<int>().ValueOrDefault(() => new Random().Next()) == a random number



### Conversion from using nulls

You can convert any existing reference type or nullable value into a maybe by using the .ToMaybe extension method.

Nullable value-types:

    int? a = 42;
    a.ToMaybe() == Maybe.Some(42)

    int? b = null;
    b.ToMaybe() == Maybe.None<int>()

Objects reference-types:

    string c = "Hello world!";
    c.ToMaybe() == Maybe.Some("Hello world!")

    string d = null;
    d.ToMaybe() == Maybe.None<string>()


### Match

You can think of this like a glorified if-then-else using lambdas. Both possible branches of the match must have the same return. 

Returning a value:

    Maybe.Some("Request").Match(
        withSome: v => v + " Reponse",
        withNone: () => "404 Not Found") == "Request Response"

Non returning:

    Maybe.Some(42).Match(
        withSome: v => Console.WriteLine("Value is " + v)
        withNone: () => Console.WriteLine("Value is missing")) // Writes "Value is 42"


### Select

Much like using LINQ on enumerable collection, you can use LINQ on a maybe to transform any value that might be inside the maybe.

    Maybe.Some(42).Select(x => x + 1) == Maybe.Some(43)

    Maybe.Some(17).Select(x => "It is " + x.ToString()) == Maybe.Some("It is 17")

If the source maybe is none, then any result will also be none

    Maybe.None<int>().Select(x => x + 1) == Maybe.None<int>()

    Maybe.None<int>().Select(x => x.ToString) == Maybe.None<string>()

You can also write this using the fluent LINQ syntax

    (from x in Maybe.Some(17)
     select "It is " + x) == Maybe.Some("It is 17")

### Select Many

When the result of a select is another maybe, you end up with a `Maybe<Maybe<T>>`, this can simply be flattened down to `Maybe<T>`.

    Maybe.Some(42)
        .SelectMany(x => x.ToString().ToMaybe()) == Maybe.Some("42")

    Maybe.Some(42)
        .SelectMany(x => Maybe.None<string>()) == Maybe.None<string>()

Select many can also be applied without passing a selector if you are not changing the underlying value type. You can flatten up to 7 levels of maybe at once.

    Maybe.Some(Maybe.Some(42)).SelectMany() == Maybe.Some(42)
