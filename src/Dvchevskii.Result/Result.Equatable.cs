using System;

namespace Dvchevskii.Result
{
    internal abstract partial class Result<T, E>
    {
        /*public static bool operator ==(Result<T, E> lhs, Result<T, E> rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Result<T, E> lhs, Result<T, E> rhs)
        {
            return lhs.Equals(rhs);
        }

        public bool Equals(IResult other)
        {
            if (other is null)
            {
                return false;
            }

            return other.IsOk() == this.IsOk();
        }

        public bool Equals(IResult<T, E> other)
        {
            if (!Equals((IResult)other))
            {
                return false;
            }

            if (other == null)
            {
                return false;
            }

            if (IsOk())
            {
                return Equals(other.Unwrap());
            }

            return Equals(other.UnwrapErr());
        }

        public abstract bool Equals(T other);
        public abstract bool Equals(E other);*/
    }
}
