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
    }
}
