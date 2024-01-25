using System;
using Dvchevskii.Result.Exceptions;

namespace Dvchevskii.Result
{
    internal abstract class Result<T, E> : IResult<T, E>
    {
        public virtual bool IsOk() => false;

        public virtual bool IsErr() => false;

        public bool IsOkAnd(Predicate<T> predicate) => IsOk() && predicate(UnwrapUnchecked());

        public bool IsErrAnd(Predicate<E> predicate) => IsErr() && predicate(UnwrapErrUnchecked());

        public IResult<U, E> Map<U>(Func<T, U> mapper) =>
            IsOk()
                ? Ok<U, E>.Create(mapper(UnwrapUnchecked()))
                : Err<U, E>.Create(UnwrapErrUnchecked());

        public U MapOr<U>(Func<T, U> mapper, U defaultValue) =>
            IsOk() ? mapper(UnwrapUnchecked()) : defaultValue;

        public U MapOrElse<U>(Func<T, U> mapper, Func<E, U> elseMapper) =>
            IsOk() ? mapper(UnwrapUnchecked()) : elseMapper(UnwrapErrUnchecked());

        public IResult<T, F> MapErr<F>(Func<E, F> errMapper) =>
            IsErr()
                ? Err<T, F>.Create(errMapper(UnwrapErrUnchecked()))
                : Ok<T, F>.Create(UnwrapUnchecked());

        public IResult<T, E> Inspect(Action<T> inspector)
        {
            if (IsOk())
            {
                inspector(UnwrapUnchecked());
            }

            return this;
        }

        public IResult<T, E> InspectErr(Action<E> inspector)
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

        public IResult<U, E> And<U>(IResult<U, E> result) =>
            MapOrElse(_ => result, Err<U, E>.Create);

        public IResult<U, E> AndThen<U>(Func<T, IResult<U, E>> factory) =>
            MapOrElse(factory, Err<U, E>.Create);

        public IResult<T, F> Or<F>(IResult<T, F> result) => MapOrElse(Ok<T, F>.Create, _ => result);

        public IResult<T, F> OrElse<F>(Func<E, IResult<T, F>> factory) =>
            MapOrElse(Ok<T, F>.Create, factory);
    }
}
