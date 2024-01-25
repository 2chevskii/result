using System;

namespace Dvchevskii.Result
{
    internal sealed class Ok<T> : Ok<T, Exception>, IResult<T>
    {
        private Ok(T value)
            : base(value) { }

        internal static new IResult<T> Create(T value) => new Ok<T>(value);
    }
}
