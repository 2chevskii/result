using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result
    {
        public static Result<T, E> Ok<T, E>(T value) => Dvchevskii.Result.Ok<T, E>.From(value);

        public static Result<T, Exception> Ok<T>(T value) =>
            Dvchevskii.Result.Ok<T, Exception>.From(value);

        public static Result<T, E> Err<T, E>(E error) => Dvchevskii.Result.Err<T, E>.Create(error);

        public static Result<T, Exception> Err<T>(Exception error) =>
            Dvchevskii.Result.Err<T, Exception>.Create(error);
    }
}
