using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E> : IEquatable<Result<T, E>>
    {
        private const int HASHCODE_MULT_CONST = 6689;

        public override bool Equals(object obj)
        {
            if (obj is ResultState state)
            {
                return Is(state);
            }

            if (obj is Result<T, E> result)
            {
                return Equals(result);
            }

            return false;
        }

        public bool Equals(Result<T, E> other)
        {
            if (other == null)
            {
                return false;
            }

            if (IsOk())
            {
                if (other.IsErr())
                {
                    return false;
                }

                T selfValue = UnwrapUnchecked();

                if (selfValue == null)
                {
                    return other.UnwrapUnchecked() == null;
                }

                if (selfValue is IEquatable<T> selfEq)
                {
                    return selfEq.Equals(other.UnwrapUnchecked());
                }

                return selfValue.Equals(other.UnwrapUnchecked());
            }

            if (other.IsOk())
            {
                return false;
            }

            E selfErr = UnwrapErrUnchecked();

            if (selfErr == null)
            {
                return other.UnwrapErrUnchecked() == null;
            }

            if (selfErr is IEquatable<E> selfErrEq)
            {
                return selfErrEq.Equals(other.UnwrapErrUnchecked());
            }

            return selfErr.Equals(other.UnwrapErrUnchecked());
        }

        public override int GetHashCode() =>
            MapOrElse(
                val => unchecked(val.GetHashCode() * HASHCODE_MULT_CONST),
                e => unchecked(e.GetHashCode() * HASHCODE_MULT_CONST)
            );
    }
}
