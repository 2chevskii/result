namespace Dvchevskii.Result
{
    public abstract partial class Result
    {
        public abstract ResultState State();

        public bool IsOk() => Is(ResultState.Ok);

        public bool IsErr() => Is(ResultState.Err);

        public bool Is(ResultState state) => State() == state;

        public byte NumericState() => (byte)State();
    }

    public class ConvertableResult<T>
    {
        public ResultState State { get; }
        public T Value { get; }

        public ConvertableResult(ResultState state, T value)
        {
            State = state;
            Value = value;
        }
    }
}
