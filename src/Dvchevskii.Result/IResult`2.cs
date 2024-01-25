using System;

namespace Dvchevskii.Result
{
    public interface IResult<T, E> : IResult
    {
        bool IsOkAnd(Predicate<T> predicate);
        bool IsErrAnd(Predicate<E> predicate);

        /*ok->Option*/
        /*err->Option*/

        IResult<U, E> Map<U>(Func<T, U> mapper);
        U MapOr<U>(Func<T, U> mapper, U defaultValue);
        U MapOrElse<U>(Func<T, U> mapper, Func<E, U> elseMapper);
        IResult<T, F> MapErr<F>(Func<E, F> errMapper);
        IResult<T, E> Inspect(Action<T> inspector);
        IResult<T, E> InspectErr(Action<E> inspector);

        T Expect(string message);
        T Unwrap();
        T UnwrapOrDefault();
        T UnwrapOr(T defaultValue);
        T UnwrapOrElse(Func<E, T> defaultValueFactory);
        T UnwrapUnchecked();

        E ExpectErr(string message);
        E UnwrapErr();
        E UnwrapErrUnchecked();

        IResult<U, E> And<U>(IResult<U, E> result);
        IResult<U, E> AndThen<U>(Func<T, IResult<U, E>> factory);

        IResult<T, F> Or<F>(IResult<T, F> result);
        IResult<T, F> OrElse<F>(Func<E, IResult<T, F>> factory);
    }
}
