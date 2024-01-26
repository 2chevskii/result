using System;
using System.Reflection;

namespace Dvchevskii.Result
{
    internal class Ok<T, E> : Result<T, E>, IOk, IOk<T>
    {
        private T value;

        public T Value => value;

        public Ok(T value) => this.value = value;

        public override ResultState State() => ResultState.Ok;

        public override T UnwrapUnchecked() => value;

        public override E UnwrapErrUnchecked() => (E)(object)value;

        public override string ToString() =>
            $"Result<{typeof(T).Name}={UnwrapUnchecked()?.ToString() ?? "null"}, {typeof(E).Name}>";
    }
}
