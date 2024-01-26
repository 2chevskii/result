using System;

namespace Dvchevskii.Result
{
    public static class ResultExtensions
    {
        public static U Match<T, E, U>(
            this Result<T, E> result,
            Func<T, U> mapOk,
            Func<E, U> mapErr
        ) => result.MapOrElse(mapOk, mapErr);
    }
}
