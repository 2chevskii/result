namespace Dvchevskii.Result
{
    internal class Err<T, E> : Result<T, E>, IErr
    {
        protected E error;

        protected Err(E error) => this.error = error;

        internal static IResult<T, E> Create(E error) => new Err<T, E>(error);

        public bool IsErr() => true;

        public override T UnwrapUnchecked() => (T)(object)error;

        public override E UnwrapErrUnchecked() => error;
    }
}