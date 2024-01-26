using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result : IComparable<ResultState>
    {
        public int CompareTo(ResultState other) => NumericState().CompareTo((byte)other);
    }
}
