using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
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
    }
}
