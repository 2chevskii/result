using System;
using Dvchevskii.Result.Exceptions;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
        public T Expect(string message) =>
            IsOk ? UnwrapUnchecked() : throw new UnexpectedResultException(message);

        public T Unwrap() => Expect("Result was not in Ok state");

        public T UnwrapOrDefault() => UnwrapOrElse(_ => default(T));

        public T UnwrapOr(T defaultValue) => IsOk ? UnwrapUnchecked() : defaultValue;

        public T UnwrapOrElse(Func<E, T> defaultValueFactory) =>
            IsOk ? UnwrapUnchecked() : defaultValueFactory(UnwrapErrUnchecked());

        /// <summary>
        /// Directly casts held value into the Ok variant's type and returns it.
        /// This is meant to be used in the situation that you know that the Result is in Ok state.
        /// If that is not the case, method might work or might not,
        /// depending on the relationship between <see cref="T"/> and <see cref="E"/> types.
        /// That is considered Undefined Behaviour
        /// </summary>
        ///
        /// <exception cref="Exception">
        /// Conversion into <see cref="T"/> has failed for an arbitrary reason. This is UB
        /// </exception>
        ///
        /// <returns>
        /// Unwrapped held value
        /// </returns>
        public abstract T UnwrapUnchecked();
        /// <summary>
        /// Directly casts held value into the Err variant's type and returns it.
        /// This is meant to be used in the situation that you know that the Result is in Err state.
        /// If that is not the case, method might work or might not,
        /// depending on the relationship between <see cref="T"/> and <see cref="E"/> types.
        /// That is considered Undefined Behaviour
        /// </summary>
        ///
        /// <exception cref="Exception">
        /// Conversion into <see cref="E"/> has failed for an arbitrary reason. This is UB
        /// </exception>
        ///
        /// <returns>
        /// Unwrapped held value
        /// </returns>
        public abstract E UnwrapErrUnchecked();

        public E ExpectErr(string message) =>
            IsErr ? UnwrapErrUnchecked() : throw new UnexpectedResultException(message);

        public E UnwrapErr() => ExpectErr("Result was not in Err state");
    }
}
