using System;
using System.Runtime.InteropServices;

namespace Dvchevskii.Result
{
    public static class Result
    {
        public static IResult<T, E> Ok<T, E>(T value) => Dvchevskii.Result.Ok<T, E>.Create(value);

        public static IResult<T> Ok<T>(T value) => Dvchevskii.Result.Ok<T>.Create(value);

        public static IResult<T, E> Err<T, E>(E error) => Dvchevskii.Result.Err<T, E>.Create(error);

        public static IResult<T> Err<T>(Exception error) => Dvchevskii.Result.Err<T>.Create(error);
    }
}
