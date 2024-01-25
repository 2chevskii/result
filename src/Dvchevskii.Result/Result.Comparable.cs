using System;

namespace Dvchevskii.Result
{
    internal abstract partial class Result<T, E>
    {
        public int CompareTo(ResultState other) => NumericState().CompareTo((byte)other);

        public int CompareTo(IResult<T, E> other)
        {
            if (other == null)
            {
                return 1;
            }

            int stateComparison = CompareTo(other.State());

            if (stateComparison != 0)
            {
                return stateComparison;
            }

            return MapOrElse(
                selfValue =>
                    selfValue is IComparable<T> selfCmp
                        ? selfCmp.CompareTo(other.UnwrapUnchecked())
                        : stateComparison,
                selfError =>
                    selfError is IComparable<E> selfErrorCmp
                        ? selfErrorCmp.CompareTo(other.UnwrapErrUnchecked())
                        : stateComparison
            );
        }
    }
}
