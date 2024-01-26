using System;

namespace Dvchevskii.Result
{
    public abstract partial class Result : IEquatable<ResultState>
    {
        public bool Equals(ResultState state) => Is(state);
    }
}
