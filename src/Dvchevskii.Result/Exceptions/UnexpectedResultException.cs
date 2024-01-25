using System;

namespace Dvchevskii.Result.Exceptions
{
    public sealed class UnexpectedResultException : Exception
    {
        public UnexpectedResultException(string message)
            : base(message) { }
    }
}