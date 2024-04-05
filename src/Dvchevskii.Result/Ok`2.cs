using System;

namespace Dvchevskii.Result
{
    public class Ok<T,E> : Result<T,E>
    {
        private readonly T _value;

        public override bool IsOk => true;
        public override bool IsErr => false;

        private Ok(T value)
        {
            _value = value;
        }

        public static implicit operator T(Ok<T, E> ok)
        {
            return ok.UnwrapUnchecked();
        }

        public static Ok<T, E> From(T value) => new Ok<T, E>(value);

        public override T UnwrapUnchecked()
        {
            return _value;
        }

        public override E UnwrapErrUnchecked()
        {
            throw new InvalidOperationException();
        }

        protected override bool Equals(Ok<T, E> ok)
        {
            var otherValue = ok.UnwrapUnchecked();

            if (otherValue is IEquatable<T> equatable)
            {
                return equatable.Equals(UnwrapUnchecked());
            }

            if (otherValue is null)
            {
                return UnwrapUnchecked() is null;
            }

            return otherValue.Equals(UnwrapUnchecked());
        }

        protected override bool Equals(Err<T, E> err)
        {
            return err is null && UnwrapUnchecked() is null;
        }
    }
}
