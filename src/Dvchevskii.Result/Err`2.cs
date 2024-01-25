using System;
using System.Runtime.InteropServices;

namespace Dvchevskii.Result
{
    internal class Err<T, E> : Result<T, E>, IErr
    {
        protected E error;

        protected Err(E error) => this.error = error;

        internal static IResult<T, E> Create(E error) => new Err<T, E>(error);

        public override bool IsErr() => true;

        public override T UnwrapUnchecked() => (T)(object)error;

        public override E UnwrapErrUnchecked() => error;

        /*public override bool Equals(T other) => false;

        public override bool Equals(E other)
        {
            if (error == null && other == null)
            {
                return true;
            }

            if (other is IEquatable<E> eq)
            {
                return eq.Equals(UnwrapErr());
            }

            return other.Equals(UnwrapErr());
        }*/
    }
}
