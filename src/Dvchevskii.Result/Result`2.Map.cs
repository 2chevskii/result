using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
        public Result<U, E> Map<U>(Func<T, U> mapper) =>
            IsOk ? Ok<U, E>(mapper(UnwrapUnchecked())) : Err<U, E>(UnwrapErrUnchecked());

        public U MapOr<U>(Func<T, U> mapper, U defaultValue) =>
            IsOk ? mapper(UnwrapUnchecked()) : defaultValue;

        public U MapOrElse<U>(Func<T, U> mapper, Func<E, U> elseMapper) =>
            IsOk ? mapper(UnwrapUnchecked()) : elseMapper(UnwrapErrUnchecked());

        public Result<T, F> MapErr<F>(Func<E, F> errMapper) =>
            IsErr ? Err<T, F>(errMapper(UnwrapErrUnchecked())) : Ok<T, F>(UnwrapUnchecked());
    }
}
