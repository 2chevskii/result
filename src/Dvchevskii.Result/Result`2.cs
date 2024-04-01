using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E> : Result
    {
        public override Type UnderlyingOkType => typeof(T);
        public override Type UnderlyingErrType => typeof(E);

        public bool IsOkAnd(Predicate<T> predicate) => IsOk && predicate(UnwrapUnchecked());

        public bool IsErrAnd(Predicate<E> predicate) => IsErr && predicate(UnwrapErrUnchecked());
    }
}
