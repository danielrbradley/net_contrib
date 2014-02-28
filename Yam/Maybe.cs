namespace System
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// LINQ style extension methods for working with the Maybe monad.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Returns a new <see cref="System.Maybe&lt;TValue&gt;"/>.None that has the specified type argument.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type to assign to the type parameter of the returned generic <see cref="System.Maybe&lt;TValue&gt;"/>
        /// </typeparam>
        /// <returns>
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see>.None whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> None<TValue>()
        {
            return new Maybe<TValue>();
        }

        /// <summary>
        /// Returns a new <see cref="System.Maybe&lt;TValue&gt;"/>.Some that wraps the specified value.
        /// </summary>
        /// <param name="value">
        /// The value to wrap in the <see cref="System.Maybe&lt;TValue&gt;"/>
        /// </param>
        /// <typeparam name="TValue">
        /// The type to assign to the type parameter of the returned generic <see cref="System.Maybe&lt;TValue&gt;"/>
        /// </typeparam>
        /// <returns>
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see>.Some whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Some<TValue>(TValue value)
        {
            return new Maybe<TValue>(value);
        }

        /// <summary>
        /// Projects the maybe into a new form.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TValueResult">
        /// The type of the value returned by <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to apply the transform function to.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to the <see cref="System.Maybe&lt;TValue&gt;">Maybe</see>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Maybe&lt;TValue&gt;"/> of None if <paramref name="source"/> is None, 
        /// or Some whose value the result of invoking the transform function to the value of <paramref name="source"/>
        /// </returns>
        public static Maybe<TValueResult> Select<TValue, TValueResult>(
            this Maybe<TValue> source,
            Func<TValue, TValueResult> selector)
        {
            return source.IsSome ? new Maybe<TValueResult>(selector(source.Value)) : new Maybe<TValueResult>();
        }

        /// <summary>
        /// Projects the maybe into a new form.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TValueResult">
        /// The type of the value returned by <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to apply the transform function to.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to the <see cref="System.Maybe&lt;TValue&gt;">Maybe</see>.
        /// </param>
        /// <param name="defaultValue">
        /// A value to return if the <paramref name="source"/> is None.
        /// </param>
        /// <returns>
        /// The result of invoking the <paramref name="selector"/> function to the value of <paramref name="source"/> if it is Some,
        /// or the <paramref name="defaultValue"/> if <paramref name="source"/> is None.
        /// </returns>
        public static TValueResult Select<TValue, TValueResult>(
            this Maybe<TValue> source,
            Func<TValue, TValueResult> selector,
            TValueResult defaultValue)
        {
            return source.IsSome ? selector(source.Value) : defaultValue;
        }

        /// <summary>
        /// Projects the maybe into a new form.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TValueResult">
        /// The type of the value returned by <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to apply the transform function to.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to the <see cref="System.Maybe&lt;TValue&gt;">Maybe</see>.
        /// </param>
        /// <param name="defaultValue">
        /// A value to return if the <paramref name="source"/> is None.
        /// </param>
        /// <returns>
        /// The result of invoking the <paramref name="selector"/> function to the value of <paramref name="source"/> if it is Some,
        /// or the result of invoking the <paramref name="defaultValue"/> function if <paramref name="source"/> is None.
        /// </returns>
        public static TValueResult Select<TValue, TValueResult>(
            this Maybe<TValue> source,
            Func<TValue, TValueResult> selector,
            Func<TValueResult> defaultValue)
        {
            return source.Match(selector, defaultValue);
        }

        /// <summary>
        /// Projects the maybe into a new form.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TResult">
        /// The type of the value returned by <paramref name="selector"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to apply the transform function to.
        /// </param>
        /// <param name="selector">
        /// A transform function to apply to the <see cref="System.Maybe&lt;TValue&gt;">Maybe</see>.
        /// </param>
        /// <returns>
        /// The flattened result of invoking the <paramref name="selector"/> function to the value of <paramref name="source"/> if it is Some,
        /// or None if <paramref name="source"/> is None.
        /// </returns>
        public static Maybe<TResult> SelectMany<TValue, TResult>(
            this Maybe<TValue> source,
            Func<TValue, Maybe<TResult>> selector)
        {
            return source.Select(selector).Flatten();
        }

        /// <summary>
        /// Returns the value of the maybe, or a default value value if the maybe is None.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to get the value of.
        /// </param>
        /// <param name="defaultValue">
        /// The default value to return is the <paramref name="source"/> is None.
        /// </param>
        /// <returns>
        /// The value of the source if the maybe is Some, or the default value if the maybe is None.
        /// </returns>
        public static TValue ValueOrDefault<TValue>(this Maybe<TValue> source, TValue defaultValue = default(TValue))
        {
            if (source.IsSome)
            {
                return source.Value;
            }

            return defaultValue;
        }

        /// <summary>
        /// Returns the value of the maybe, or a default value value if the maybe is None.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to get the value of.
        /// </param>
        /// <param name="defaultValue">
        /// A function which gives the value to return if the <paramref name="source"/> is None.
        /// </param>
        /// <returns>
        /// The value of the <paramref name="source"/> if the maybe is Some,
        /// or the result of invoking the <paramref name="defaultValue"/> function if <paramref name="source"/> is None.
        /// </returns>
        public static TValue ValueOrDefault<TValue>(this Maybe<TValue> source, Func<TValue> defaultValue)
        {
            if (source.IsSome)
            {
                return source.Value;
            }

            return defaultValue();
        }

        /// <summary>
        /// Branches execution based on if the maybe has a value.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <typeparam name="TValueResult">
        /// The type of the value returned by either continuation.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to branch execution from.
        /// </param>
        /// <param name="withSome">
        /// A function to invoke if <paramref name="source"/> is Some.
        /// </param>
        /// <param name="withNone">
        /// A function to invoke if <paramref name="source"/> is None.
        /// </param>
        /// <returns>
        /// The result of the function that was invoked.
        /// </returns>
        public static TValueResult Match<TValue, TValueResult>(
            this Maybe<TValue> source,
            Func<TValue, TValueResult> withSome,
            Func<TValueResult> withNone)
        {
            if (source.IsSome)
            {
                return withSome(source.Value);
            }

            return withNone();
        }

        /// <summary>
        /// Branches execution based on if the maybe has a value.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">Maybe</see> to branch execution from.
        /// </param>
        /// <param name="withSome">
        /// A function to invoke if <paramref name="source"/> is Some.
        /// </param>
        /// <param name="withNone">
        /// A function to invoke if <paramref name="source"/> is None.
        /// </param>
        public static void Match<TValue>(
            this Maybe<TValue> source,
            Action<TValue> withSome,
            Action withNone)
        {
            if (source.IsSome)
            {
                withSome(source.Value);
            }
            else
            {
                withNone();
            }
        }

        /// <summary>
        /// Returns a new <see cref="System.Maybe&lt;TValue&gt;">maybe</see> constructed from the specified value.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="obj"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The nullable object to be converted into the <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Maybe&lt;TValue&gt;">maybe</see> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> ToMaybe<TValue>(this TValue? obj)
           where TValue : struct
        {
            if (obj == null)
            {
                return Maybe.None<TValue>();
            }

            return Maybe.Some(obj.Value);
        }

        /// <summary>
        /// Returns a new <see cref="System.Maybe&lt;TValue&gt;">maybe</see> constructed from the specified value.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="obj"/>.
        /// </typeparam>
        /// <param name="obj">
        /// The nullable object to be converted into the <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Maybe&lt;TValue&gt;">maybe</see> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> ToMaybe<TValue>(this TValue obj)
            where TValue : class
        {
            if (obj == null)
            {
                return Maybe.None<TValue>();
            }

            return Maybe.Some(obj);
        }

        /// <summary>
        /// Returns a new nullable struct.
        /// </summary>
        /// <typeparam name="TValue">
        /// The type of the value of <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A <see cref="System.Maybe&lt;TValue&gt;">maybe</see> to be converted into a nullable struct.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static TValue? ToNullable<TValue>(this Maybe<TValue> source)
            where TValue : struct
        {
            if (source.IsSome)
            {
                return source.Value;
            }

            return null;
        }

        /// <summary>
        /// Constructs a <see cref="System.Maybe&lt;TValue&gt;">maybe</see> 
        /// from a nested <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </summary>
        /// <typeparam name="TValue">
        /// The inner-most type of the value of the <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A nested <see cref="System.Maybe&lt;TValue&gt;"/> whose type argument is
        /// also a <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<TValue>> source)
        {
            if (source.IsSome && source.Value.IsSome)
            {
                return Some(source.Value.Value);
            }

            return None<TValue>();
        }

        /// <summary>
        /// Constructs a <see cref="System.Maybe&lt;TValue&gt;">maybe</see> 
        /// from a nested <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </summary>
        /// <typeparam name="TValue">
        /// The inner-most type of the value of the <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A nested <see cref="System.Maybe&lt;TValue&gt;"/> whose type argument is
        /// also a <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<TValue>>> source)
        {
            if (source.IsSome && source.Value.IsSome && source.Value.Value.IsSome)
            {
                return Some(source.Value.Value.Value);
            }

            return None<TValue>();
        }

        /// <summary>
        /// Constructs a <see cref="System.Maybe&lt;TValue&gt;">maybe</see> 
        /// from a nested <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </summary>
        /// <typeparam name="TValue">
        /// The inner-most type of the value of the <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A nested <see cref="System.Maybe&lt;TValue&gt;"/> whose type argument is
        /// also a <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<TValue>>>> source)
        {
            if (source.IsSome && source.Value.IsSome && source.Value.Value.IsSome && source.Value.Value.Value.IsSome)
            {
                return Some(source.Value.Value.Value.Value);
            }

            return None<TValue>();
        }

        /// <summary>
        /// Constructs a <see cref="System.Maybe&lt;TValue&gt;">maybe</see> 
        /// from a nested <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </summary>
        /// <typeparam name="TValue">
        /// The inner-most type of the value of the <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A nested <see cref="System.Maybe&lt;TValue&gt;"/> whose type argument is
        /// also a <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<Maybe<TValue>>>>> source)
        {
            if (source.IsSome && source.Value.IsSome && source.Value.Value.IsSome && source.Value.Value.Value.IsSome
                && source.Value.Value.Value.Value.IsSome)
            {
                return Some(source.Value.Value.Value.Value.Value);
            }

            return None<TValue>();
        }

        /// <summary>
        /// Constructs a <see cref="System.Maybe&lt;TValue&gt;">maybe</see> 
        /// from a nested <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </summary>
        /// <typeparam name="TValue">
        /// The inner-most type of the value of the <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A nested <see cref="System.Maybe&lt;TValue&gt;"/> whose type argument is
        /// also a <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<Maybe<Maybe<TValue>>>>>> source)
        {
            if (source.IsSome && source.Value.IsSome && source.Value.Value.IsSome && source.Value.Value.Value.IsSome
                && source.Value.Value.Value.Value.IsSome && source.Value.Value.Value.Value.Value.IsSome)
            {
                return Some(source.Value.Value.Value.Value.Value.Value);
            }

            return None<TValue>();
        }

        /// <summary>
        /// Constructs a <see cref="System.Maybe&lt;TValue&gt;">maybe</see> 
        /// from a nested <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </summary>
        /// <typeparam name="TValue">
        /// The inner-most type of the value of the <paramref name="source"/>.
        /// </typeparam>
        /// <param name="source">
        /// A nested <see cref="System.Maybe&lt;TValue&gt;"/> whose type argument is
        /// also a <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// A <see cref="System.Nullable&lt;TValue&gt;"/> whose type argument is TValue.
        /// </returns>
        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<Maybe<Maybe<Maybe<TValue>>>>>>> source)
        {
            if (source.IsSome && source.Value.IsSome && source.Value.Value.IsSome && source.Value.Value.Value.IsSome
                && source.Value.Value.Value.Value.IsSome && source.Value.Value.Value.Value.Value.IsSome
                && source.Value.Value.Value.Value.Value.Value.IsSome)
            {
                return Some(source.Value.Value.Value.Value.Value.Value.Value);
            }

            return None<TValue>();
        }
    }

    /// <summary>
    /// The Maybe monad used to represent nullable values in a safe manner.
    /// </summary>
    /// <typeparam name="TValue">
    /// The underlying type of the nullable value.
    /// </typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class Maybe<TValue>
    {
        private readonly TValue value;

        private readonly bool isSome;

        /// <summary>
        /// Initializes a new instance of the <see cref="System.Maybe&lt;TValue&gt;"/> class to represent None.
        /// </summary>
        public Maybe()
        {
            this.isSome = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="System.Maybe&lt;TValue&gt;"/> class to represent Some value.
        /// </summary>
        /// <param name="value">
        /// The value wrapped inside the maybe.
        /// </param>
        public Maybe(TValue value)
        {
            this.value = value;
            this.isSome = true;
        }

        /// <summary>
        /// Gets the value inside the <see cref="System.Maybe&lt;TValue&gt;">maybe</see> Some, or throws an exception if the <see cref="System.Maybe&lt;TValue&gt;">maybe</see> is None.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The maybe is None.
        /// </exception>
        public TValue Value
        {
            get
            {
                if (this.isSome)
                {
                    return this.value;
                }

                throw new InvalidOperationException("TValuehe HasValue property is false");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="System.Maybe&lt;TValue&gt;">maybe</see> object is Some.
        /// </summary>
        public bool IsSome
        {
            get
            {
                return this.isSome;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="System.Maybe&lt;TValue&gt;">maybe</see> object is None.
        /// </summary>
        public bool IsNone
        {
            get
            {
                return !this.isSome;
            }
        }

        /// <summary>
        /// Indicates whether two System.Type objects are equal.
        /// </summary>
        /// <param name="left">
        /// The first object to compare.
        /// </param>
        /// <param name="right">
        /// The second object to compare.
        /// </param>
        /// <returns>
        /// true if left is equal to right; otherwise, false.
        /// </returns>
        public static bool operator ==(Maybe<TValue> left, Maybe<TValue> right)
        {
            return object.Equals(left, right);
        }

        /// <summary>
        /// Indicates whether two System.Type objects are not equal.
        /// </summary>
        /// <param name="left">
        /// The first object to compare.
        /// </param>
        /// <param name="right">
        /// The second object to compare.
        /// </param>
        /// <returns>
        /// true if left is note equal to right; otherwise, false.
        /// </returns>
        public static bool operator !=(Maybe<TValue> left, Maybe<TValue> right)
        {
            return !object.Equals(left, right);
        }

        /// <summary>
        /// Returns the text representation of the value of the current <see cref="System.Maybe&lt;TValue&gt;"/> object.
        /// </summary>
        /// <returns>
        /// A text representation of the value of the current <see cref="System.Maybe&lt;TValue&gt;"/> object 
        /// wrapped in ("Some{0}") if the <see cref="System.Maybe&lt;TValue&gt;">maybe</see> is Some, 
        /// or the string ("None") if the <see cref="System.Maybe&lt;TValue&gt;">maybe</see> is None.
        /// </returns>
        public override string ToString()
        {
            if (this.IsSome)
            {
                return string.Format("Some({0})", this.Value);
            }

            return "None";
        }

        /// <summary>
        /// Determines if the underlying value of the current <see cref="System.Maybe&lt;TValue&gt;"/> is 
        /// the same as the underlying value of the specified <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </summary>
        /// <param name="other">
        /// The object whose underlying value is to be compared with the underlying value of the current <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// true if the underlying value of <paramref name="other"/> is the same as the underlying value of the current <see cref="System.Maybe&lt;TValue&gt;"/>; otherwise, false.
        /// </returns>
        public bool Equals(Maybe<TValue> other)
        {
            return this.isSome.Equals(other.isSome) && EqualityComparer<TValue>.Default.Equals(this.value, other.value);
        }

        /// <summary>
        /// Determines if the underlying value of the current <see cref="System.Maybe&lt;TValue&gt;"/> is 
        /// the same as the underlying value of the specified <see cref="System.Object"/>.
        /// </summary>
        /// <param name="obj">
        /// The object whose underlying value is to be compared with the underlying value of the current <see cref="System.Maybe&lt;TValue&gt;"/>.
        /// </param>
        /// <returns>
        /// true if the underlying value of <paramref name="obj"/> is the same as the 
        /// underlying value of the current <see cref="System.Maybe&lt;TValue&gt;"/>; otherwise, false.
        /// This method also returns false if the object specified by the <paramref name="obj"/> 
        /// parameter is not a <see cref="System.Maybe&lt;TValue&gt;">maybe</see>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj is Maybe<TValue> && this.Equals((Maybe<TValue>)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// The hash code for this instance.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TValue>.Default.GetHashCode(this.value) * 397) ^ this.isSome.GetHashCode();
            }
        }
    }
}
