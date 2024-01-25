using System;

namespace Dvchevskii.Result
{
    public interface IResult : IEquatable<ResultState>, IComparable<ResultState>
    {
        bool IsOk();
        bool IsErr();

        bool HasState(ResultState state);
        byte NumericState();
        ResultState State();

        Type GetUnderlyingOkType();
        Type GetUnderlyingErrType();
    }
}
