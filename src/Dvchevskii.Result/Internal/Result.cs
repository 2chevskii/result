namespace Dvchevskii.Result
{
    public abstract partial class Result
    {
        public abstract ResultState State();

        public virtual bool IsOk() => Is(ResultState.Ok);

        public virtual bool IsErr() => Is(ResultState.Err);

        public virtual bool Is(ResultState state) => State() == state;

        public virtual byte NumericState() => (byte)State();
    }
}
