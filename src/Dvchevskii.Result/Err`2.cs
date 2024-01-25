using System;
using System.Runtime.InteropServices;

namespace Dvchevskii.Result
{
    internal class Err<T, E> : Result<T, E>, IErr
    {
        protected E error;

        protected Err(E error) => this.error = error;

        internal static IResult<T, E> Create(E error) => new Err<T, E>(error);

        public override ResultState State() => ResultState.Err;

        public override T UnwrapUnchecked() => (T)(object)error;

        public override E UnwrapErrUnchecked() => error;

        public override string ToString() =>
            $"Result<{typeof(T).Name}, {typeof(E).Name}={UnwrapErrUnchecked()?.ToString() ?? "null"}>";
    }
}
