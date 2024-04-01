using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result
    {
        public abstract bool IsOk { get; }

        public abstract bool IsErr { get; }

        public abstract Type UnderlyingOkType { get; }
        public abstract Type UnderlyingErrType { get; }

        private protected Result() { }

        public abstract bool Equals(Result other);
    }
}
