namespace System
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public static class Maybe
    {
        public static Maybe<TValue> None<TValue>()
        {
            return new Maybe<TValue>();
        }

        public static Maybe<TValue> Some<TValue>(TValue value)
        {
            return new Maybe<TValue>(value);
        }

        public static Maybe<TValueResult> Select<TValue, TValueResult>(this Maybe<TValue> source, Func<TValue, TValueResult> selector)
        {
            return source.IsSome ? new Maybe<TValueResult>(selector(source.Value)) : new Maybe<TValueResult>();
        }

        public static TValueResult Select<TValue, TValueResult>(this Maybe<TValue> maybe, Func<TValue, TValueResult> selector, TValueResult defaultValue)
        {
            return maybe.IsSome ? selector(maybe.Value) : defaultValue;
        }

        public static TValueResult Select<TValue, TValueResult>(this Maybe<TValue> maybe, Func<TValue, TValueResult> selector, Func<TValueResult> defaultValue)
        {
            return maybe.IsSome ? selector(maybe.Value) : defaultValue();
        }

        public static TValue ValueOrDefault<TValue>(this Maybe<TValue> maybe, TValue defaultValue = default(TValue))
        {
            if (maybe.IsSome)
            {
                return maybe.Value;
            }

            return defaultValue;
        }

        public static TValue ValueOrDefault<TValue>(this Maybe<TValue> maybe, Func<TValue> defaultValue)
        {
            if (maybe.IsSome)
            {
                return maybe.Value;
            }

            return defaultValue();
        }

        public static TValueResult Match<TValue, TValueResult>(this Maybe<TValue> maybe, Func<TValue, TValueResult> withSome, Func<TValueResult> withNone)
        {
            if (maybe.IsSome)
            {
                return withSome(maybe.Value);
            }

            return withNone();
        }

        public static Maybe<TValue> FromNullable<TValue>(TValue? obj)
           where TValue : struct
        {
            if (obj == null)
            {
                return Maybe.None<TValue>();
            }

            return Maybe.Some(obj.Value);
        }

        public static Maybe<TValue> FromNullable<TValue>(TValue obj)
            where TValue : class
        {
            if (obj == null)
            {
                return Maybe.None<TValue>();
            }

            return Maybe.Some(obj);
        }

        public static TValue? ToNullable<TValue>(this Maybe<TValue> maybe)
            where TValue : struct
        {
            if (maybe.IsSome)
            {
                return maybe.Value;
            }

            return null;
        }

        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<TValue>> maybe)
        {
            return maybe.FlattenInternal();
        }

        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<TValue>>> maybe)
        {
            return maybe.FlattenInternal().Flatten();
        }

        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<TValue>>>> maybe)
        {
            return maybe.FlattenInternal().Flatten();
        }

        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<Maybe<TValue>>>>> maybe)
        {
            return maybe.FlattenInternal().Flatten();
        }

        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<Maybe<Maybe<TValue>>>>>> maybe)
        {
            return maybe.FlattenInternal().Flatten();
        }

        public static Maybe<TValue> Flatten<TValue>(this Maybe<Maybe<Maybe<Maybe<Maybe<Maybe<Maybe<TValue>>>>>>> maybe)
        {
            return maybe.FlattenInternal().Flatten();
        }

        private static Maybe<TValue> FlattenInternal<TValue>(this Maybe<Maybe<TValue>> maybe)
        {
            if (maybe.IsSome && maybe.Value.IsSome)
            {
                return Some(maybe.Value.Value);
            }

            return None<TValue>();
        }
    }

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public sealed class Maybe<TValue>
    {
        private readonly TValue value;

        private readonly bool isSome;

        public Maybe()
        {
            this.isSome = false;
        }

        public Maybe(TValue value)
        {
            this.value = value;
            this.isSome = true;
        }

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

        public bool IsSome
        {
            get
            {
                return this.isSome;
            }
        }

        public bool IsNone
        {
            get
            {
                return !this.isSome;
            }
        }

        public static bool operator ==(Maybe<TValue> left, Maybe<TValue> right)
        {
            return object.Equals(left, right);
        }

        public static bool operator !=(Maybe<TValue> left, Maybe<TValue> right)
        {
            return !object.Equals(left, right);
        }

        public override string ToString()
        {
            if (this.IsSome)
            {
                return string.Format("Some({0})", this.Value);
            }

            return "None";
        }

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

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<TValue>.Default.GetHashCode(this.value) * 397) ^ this.isSome.GetHashCode();
            }
        }

        private bool Equals(Maybe<TValue> other)
        {
            return this.isSome.Equals(other.isSome) && EqualityComparer<TValue>.Default.Equals(this.value, other.value);
        }
    }
}
