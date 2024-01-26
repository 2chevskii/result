using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E> : Result
    {
        public Type GetUnderlyingOkType() => typeof(T);

        public Type GetUnderlyingErrType() => typeof(E);

        public bool IsOkAnd(Predicate<T> predicate) => IsOk() && predicate(UnwrapUnchecked());

        public bool IsErrAnd(Predicate<E> predicate) => IsErr() && predicate(UnwrapErrUnchecked());
    }
}
