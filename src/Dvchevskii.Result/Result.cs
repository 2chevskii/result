using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result
    {
        public static Result<T, E> Ok<T, E>(T value) => new Ok<T, E>(value);

        public static Result<T, Exception> Ok<T>(T value) => new Ok<T, Exception>(value);

        public static Result<T, E> Err<T, E>(E error) => new Err<T, E>(error);

        public static Result<T, Exception> Err<T>(Exception error) => new Err<T, Exception>(error);
    }

    public abstract partial class Result
    {
        public abstract ResultState State();

        public virtual bool IsOk() => Is(ResultState.Ok);

        public virtual bool IsErr() => Is(ResultState.Err);

        public virtual bool Is(ResultState state) => State() == state;

        public virtual byte NumericState() => (byte)State();
    }

    public abstract partial class Result : IEquatable<ResultState>
    {
        public bool Equals(ResultState state) => Is(state);
    }

    public abstract partial class Result : IComparable<ResultState>
    {
        public int CompareTo(ResultState other) => NumericState().CompareTo((byte)other);
    }
}
