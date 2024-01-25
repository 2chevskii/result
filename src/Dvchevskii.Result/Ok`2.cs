using System;

namespace Dvchevskii.Result
{
    internal class Ok<T, E> : Result<T, E>, IOk
    {
        protected T value;

        protected Ok(T value) => this.value = value;

        internal static IResult<T, E> Create(T value) => new Ok<T, E>(value);

        public override bool IsOk() => true;

        public override T UnwrapUnchecked() => value;

        public override E UnwrapErrUnchecked() => (E)(object)value;

        /*public override bool Equals(T other)
        {
            if (value == null && other == null)
            {
                return true;
            }

            if (other is IEquatable<T> eq)
            {
                return eq.Equals(Unwrap());
            }

            return other.Equals(Unwrap());
        }

        public override bool Equals(E other) => false;*/
    }
}
