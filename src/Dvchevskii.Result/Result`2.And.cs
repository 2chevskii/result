using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
        public Result<U, E> And<U>(Result<U, E> result) => MapOrElse(_ => result, Err<U, E>);

        public Result<U, E> AndThen<U>(Func<T, Result<U, E>> factory) =>
            MapOrElse(factory, Err<U, E>);

        public Result<T, F> Or<F>(Result<T, F> result) => MapOrElse(Ok<T, F>, _ => result);

        public Result<T, F> OrElse<F>(Func<E, Result<T, F>> factory) =>
            MapOrElse(Ok<T, F>, factory);
    }
}
