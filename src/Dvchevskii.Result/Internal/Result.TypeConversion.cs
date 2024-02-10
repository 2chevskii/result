using System;
using Dvchevskii.Result.Exceptions;

namespace Dvchevskii.Result
{
    public abstract partial class Result<T, E>
    {
        public virtual Result<T, R> MorphOk<R>() =>
            MapOrElse(
                Ok<T, R>,
                _ =>
                    throw new UnexpectedResultException(
                        "Result state expected to be " + ResultState.Ok.ToString("G")
                    )
            );

        public virtual Result<R, E> MorphErr<R>() =>
            MapOrElse(
                _ =>
                    throw new UnexpectedResultException(
                        "Result state expected to be " + ResultState.Err.ToString("G")
                    ),
                Err<R, E>
            );

        public static implicit operator Result<T, E>(ConvertableResult<T> okResult)
        {
            if (okResult.State != ResultState.Ok)
            {
                throw new UnexpectedResultException(string.Empty);
            }

            return Result.Ok<T, E>(okResult.Value);
        }

        public static implicit operator Result<T, E>(ConvertableResult<E> errResult)
        {
            if (errResult.State != ResultState.Err)
            {
                throw new UnexpectedResultException(string.Empty);
            }

            return Result.Err<T, E>(errResult.Value);
        }
    }
}
