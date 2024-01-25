using System;

namespace Dvchevskii.Result
{
    internal sealed class Err<T> : Err<T, Exception>, IResult<T>
    {
        private Err(Exception error)
            : base(error) { }

        internal new static IResult<T> Create(Exception error) => new Err<T>(error);
    }
}
