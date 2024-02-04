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
}
