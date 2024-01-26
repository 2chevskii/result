using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
        public Result<U, E> And<U>(Result<U, E> result) =>
            MapOrElse(_ => result, e => new Err<U, E>(e));

        public Result<U, E> AndThen<U>(Func<T, Result<U, E>> factory) =>
            MapOrElse(factory, e => new Err<U, E>(e));

        public Result<T, F> Or<F>(Result<T, F> result) =>
            MapOrElse(v => new Ok<T, F>(v), _ => result);

        public Result<T, F> OrElse<F>(Func<E, Result<T, F>> factory) =>
            MapOrElse(v => new Ok<T, F>(v), factory);
    }
}
