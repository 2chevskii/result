using System;
using System.Reflection;

namespace Dvchevskii.Result
{
    internal class Ok<T, E> : Result<T, E>, IOk
    {
        protected T value;

        protected Ok(T value) => this.value = value;

        internal static IResult<T, E> Create(T value) => new Ok<T, E>(value);

        public override ResultState State() => ResultState.Ok;

        public override T UnwrapUnchecked() => value;

        public override E UnwrapErrUnchecked() => (E)(object)value;

        public override string ToString() =>
            $"Result<{typeof(T).Name}={UnwrapUnchecked()?.ToString() ?? "null"}, {typeof(E).Name}>";

    }
}
