using System;
using System.Reflection;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E> : IEquatable<Result<T, E>>, IEquatable<Result>
    {
        private const int HASHCODE_MULT_CONST = 6689;

        public override bool Equals(object obj)
        {
            if (obj is Result<T, E> resultTe)
            {
                return Equals(resultTe);
            }

            if (obj is Result result)
            {
                return Equals(result);
            }

            if (IsOk)
            {
                var myValue = UnwrapUnchecked();

                if (obj is IEquatable<T> equatable)
                {
                    return equatable.Equals(myValue);
                }

                if (obj is T)
                {
                    return obj.Equals(myValue);
                }

                if (obj is null)
                {
                    return myValue is null;
                }
            }

            if (IsErr)
            {
                var myErr = UnwrapErrUnchecked();

                if (obj is IEquatable<E> equatable)
                {
                    return equatable.Equals(myErr);
                }

                if (obj is E)
                {
                    return obj.Equals(myErr);
                }

                if (obj is null)
                {
                    return myErr is null;
                }
            }

            return false;
        }

        public bool Equals(Result other)
        {
            if (other is Result<T,E> resultTe)
            {
                return Equals(resultTe);
            }

            if (IsOk)
            {
                var myValue = UnwrapUnchecked();

                if (other is null)
                {
                    return myValue is null;
                }
            }

            if (IsErr)
            {
                var myError = UnwrapErrUnchecked();

                if (other is null)
                {
                    return myError is null;
                }
            }

            return false;
        }

        public bool Equals(Result<T, E> other)
        {
            if (other is Ok<T, E> ok)
            {
                return Equals(ok);
            }

            if (other is Err<T, E> err)
            {
                return Equals(err);
            }

            if (other is null)
            {
                if (IsOk)
                {
                    T okValue = UnwrapUnchecked();
                    return okValue == null;
                }

                if (IsErr)
                {
                    E errValue = UnwrapErrUnchecked();
                    return errValue == null;
                }
            }

            return false;
        }

        protected abstract bool Equals(Ok<T, E> ok);
        protected abstract bool Equals(Err<T, E> err);

        public override int GetHashCode() =>
            MapOrElse(
                val => unchecked(val.GetHashCode() * HASHCODE_MULT_CONST),
                e => unchecked(e.GetHashCode() * HASHCODE_MULT_CONST)
            );
    }
}
