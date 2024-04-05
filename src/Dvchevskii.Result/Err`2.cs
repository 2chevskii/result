using System;

namespace Dvchevskii.Result
{
    public class Err<T, E> : Result<T, E>
    {
        private readonly E _error;

        public override bool IsOk => false;
        public override bool IsErr => true;

        private Err(E error)
        {
            _error = error;
        }

        public static implicit operator E(Err<T, E> err)
        {
            return err.UnwrapErrUnchecked();
        }

        public static Err<T, E> Create(E error) => new Err<T, E>(error);

        public override T UnwrapUnchecked()
        {
            throw new InvalidOperationException();
        }

        public override E UnwrapErrUnchecked()
        {
            return _error;
        }

        protected override bool Equals(Ok<T, E> ok)
        {
            return ok is null && UnwrapErrUnchecked() is null;
        }

        protected override bool Equals(Err<T, E> err)
        {
            var otherErr = err.UnwrapErrUnchecked();

            if (otherErr is IEquatable<E> equatable)
            {
                return equatable.Equals(UnwrapErrUnchecked());
            }

            if (otherErr is null)
            {
                return UnwrapErrUnchecked() is null;
            }

            return otherErr.Equals(UnwrapErrUnchecked());
        }
    }
}
