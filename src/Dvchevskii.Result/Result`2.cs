using System;
using Dvchevskii.Result.Exceptions;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E> : Result
    {
        public Type GetUnderlyingOkType() => typeof(T);

        public Type GetUnderlyingErrType() => typeof(E);

        public bool IsOkAnd(Predicate<T> predicate) => IsOk() && predicate(UnwrapUnchecked());

        public bool IsErrAnd(Predicate<E> predicate) => IsErr() && predicate(UnwrapErrUnchecked());

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

        public Result<T, E> Inspect(Action<T> inspector)
        {
            if (IsOk())
            {
                inspector(UnwrapUnchecked());
            }

            return this;
        }

        public Result<T, E> InspectErr(Action<E> inspector)
        {
            if (IsErr())
            {
                inspector(UnwrapErrUnchecked());
            }

            return this;
        }

        public T Expect(string message) =>
            IsOk() ? UnwrapUnchecked() : throw new UnexpectedResultException(message);

        public T Unwrap() => Expect("Result was not in Ok state");

        public T UnwrapOrDefault() => UnwrapOrElse(_ => default(T));

        public T UnwrapOr(T defaultValue) => IsOk() ? UnwrapUnchecked() : defaultValue;

        public T UnwrapOrElse(Func<E, T> defaultValueFactory) =>
            IsOk() ? UnwrapUnchecked() : defaultValueFactory(UnwrapErrUnchecked());

        public abstract T UnwrapUnchecked();
        public abstract E UnwrapErrUnchecked();

        public E ExpectErr(string message) =>
            IsErr() ? UnwrapErrUnchecked() : throw new UnexpectedResultException(message);

        public E UnwrapErr() => ExpectErr("Result was not in Err state");

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
