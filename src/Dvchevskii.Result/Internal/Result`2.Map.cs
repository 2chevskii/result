using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
        public Result<U, E> Map<U>(Func<T, U> mapper) =>
            IsOk()
                ? new Ok<U, E>(mapper(UnwrapUnchecked()))
                : (Result<U, E>)new Err<U, E>(UnwrapErrUnchecked());

        public U MapOr<U>(Func<T, U> mapper, U defaultValue) =>
            IsOk() ? mapper(UnwrapUnchecked()) : defaultValue;

        public U MapOrElse<U>(Func<T, U> mapper, Func<E, U> elseMapper) =>
            IsOk() ? mapper(UnwrapUnchecked()) : elseMapper(UnwrapErrUnchecked());

        public Result<T, F> MapErr<F>(Func<E, F> errMapper) =>
            IsErr()
                ? new Err<T, F>(errMapper(UnwrapErrUnchecked()))
                : (Result<T, F>)new Ok<T, F>(UnwrapUnchecked());
    }
}
