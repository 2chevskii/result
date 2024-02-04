using System;

namespace Dvchevskii.Result.Extensions
{
    public static class ResultConversionExtensions
    {
        public static Result<T, E> AsOk<T, E>(this T self) => Result.Ok<T, E>(self);

        public static Result<T, Exception> AsOk<T>(this T self) => Result.Ok(self);

        public static Result<T, E> AsErr<T, E>(this E self) => Result.Err<T, E>(self);

        public static Result<T, Exception> AsErr<T>(this Exception self) => Result.Err<T>(self);
    }
}
