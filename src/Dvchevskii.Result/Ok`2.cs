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

        public override bool Equals(Result other)
        {
            throw new NotImplementedException();
        }
    }
}
