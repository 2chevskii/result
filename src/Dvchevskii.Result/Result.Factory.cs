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
}
