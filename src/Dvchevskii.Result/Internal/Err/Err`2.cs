namespace Dvchevskii.Result
{
    internal class Err<T, E> : Result<T, E>, IErr, IErr<E>
    {
        private E error;

        public E Error => error;

        public Err(E error) => this.error = error;

        public override ResultState State() => ResultState.Err;

        public override T UnwrapUnchecked() => (T)(object)error;

        public override E UnwrapErrUnchecked() => error;

        public override string ToString() =>
            $"Result<{typeof(T).Name}, {typeof(E).Name}={UnwrapErrUnchecked()?.ToString() ?? "null"}>";
    }
}
