namespace Examples
{
    using System;

    public static class SimpleExample
    {
        public static void Run()
        {
            // ReSharper disable ExpressionIsAlwaysNull
            // ReSharper disable ConvertToConstant.Local
            // ReSharper disable SuggestUseVarKeywordEvident
            // ReSharper disable SpecifyACultureInStringConversionExplicitly

            ////// Creating a maybe //////

            Console.WriteLine(
                Maybe.Some("Hello, world!"));
            // Some(Hello, world!)

            Console.WriteLine(
                Maybe.None<string>());
            // None



            ////// LINQ Projections //////

            Console.WriteLine(
                from hello in Maybe.Some("Hello")
                select hello + ", world!");
            // Some(Hello, world!)

            Console.WriteLine(
                from hello in Maybe.Some("Hello")
                from world in Maybe.Some("world!")
                select hello + ", " + world);
            // Some(Hello, world!)

            Console.WriteLine(
                from hello in Maybe.Some("Hello")
                from world in Maybe.None<string>()
                select hello + ", " + world);
            // None



            ////// To Maybe //////

            // Nullable int with value
            int? aNullable = 42;
            Console.WriteLine(
                aNullable.ToMaybe()); // "Some(1)"

            // Nullable int without value
            int? bNullable = null;
            Console.WriteLine(
                bNullable.ToMaybe()); // "None"

            // String with value
            string cString = "Hello world!";
            Console.WriteLine(
                cString.ToMaybe()); // "Some(Hello world!)"

            // String without value
            string dString = null;
            Console.WriteLine(
                dString.ToMaybe()); // "None"



            ////// Properties //////

            Console.WriteLine(
                Maybe.Some(42).IsSome);
            // True

            Console.WriteLine(
                Maybe.Some(42).IsNone);
            // False

            Console.WriteLine(
                Maybe.None<int>().IsSome);
            // False

            Console.WriteLine(
                Maybe.None<int>().IsNone);
            // True

            Console.WriteLine(
                Maybe.Some(42).Value);
            // 42



            ////// Value Or Default //////

            // The value of Some(42)
            Console.WriteLine(
                Maybe.Some(42)
                    .ValueOrDefault()); // 42

            // The default of None<int> is 0.
            Console.WriteLine(
                Maybe.None<int>()
                    .ValueOrDefault()); // 0

            // Specifying a custom default value of -1
            Console.WriteLine(
                Maybe.None<int>()
                    .ValueOrDefault(-1)); // -1

            // Using a default value callback
            Console.WriteLine(
                Maybe.None<int>()
                    .ValueOrDefault(() => new Random().Next())); // A random number



            ////// Transforming using select //////

            // Transforming using select
            Console.WriteLine(
                Maybe.Some(42)
                    .Select(x => x + 1)); // Some(43)

            // Returning a different type in a select
            Console.WriteLine(
                Maybe.Some(17)
                    .Select(x => "It is " + x.ToString())); // Some(It is 17)

            // Selecting on none just returns none
            Console.WriteLine(
                Maybe.None<int>()
                    .Select(x => x + 1)); // None

            // Using LINQ syntax
            Console.WriteLine(
                from x in Maybe.Some(17)
                select "It is "+ 17); // Some(It is 17)



            ////// Flattening with Select Many //////

            // Flatten while transforming a maybe
            Console.WriteLine(
                Maybe.Some(42).SelectMany(x => x.ToString().ToMaybe())); // Some(42)
            Console.WriteLine(
                Maybe.Some(42).SelectMany(x => Maybe.None<string>())); // None

            // Flattening nested maybes
            Console.WriteLine(
                Maybe.Some(Maybe.Some(42)).SelectMany()); // Some(42)



            ////// Controlling flow with Match (like if-then-else) //////

            // Returning a value
            var response = Maybe.Some("Request")
                .Match(
                    withNone: () => "404 Not Found",
                    withSome: req => "Response to " + req);
            Console.WriteLine(response); // Reponse to Request

            // Non-returning
            Maybe.Some(42)
                .Match(
                    withNone: () => Console.WriteLine("Value is missing"),
                    withSome: value => Console.WriteLine("Value is " + value)); // Value is 42



            // ReSharper restore SpecifyACultureInStringConversionExplicitly
            // ReSharper restore SuggestUseVarKeywordEvident
            // ReSharper restore ConvertToConstant.Local
            // ReSharper restore ExpressionIsAlwaysNull
        }
    }
}
