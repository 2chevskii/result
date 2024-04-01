using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E> : IEquatable<Result<T, E>>
    {
        private const int HASHCODE_MULT_CONST = 6689;

        public override bool Equals(object obj)
        {
            if (obj is Result<T,E> resultTe)
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

                return obj == null && myValue == null;
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

                return obj == null && myErr == null;
            }

            return false;
        }

        public bool Equals(Result<T, E> other)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode() =>
            MapOrElse(
                val => unchecked(val.GetHashCode() * HASHCODE_MULT_CONST),
                e => unchecked(e.GetHashCode() * HASHCODE_MULT_CONST)
            );
    }
}
