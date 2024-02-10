using System;

namespace Dvchevskii.Result.Extensions.Unit
{
    public abstract class Result : Dvchevskii.Result.Result
    {
        public static Result<Dvchevskii.Unit.Unit, Exception> Ok()
        {
            return Dvchevskii.Result.Result.Ok(Dvchevskii.Unit.Unit.Default);
        }

        public static Result<Dvchevskii.Unit.Unit, E> Ok<E>()
        {
            return Dvchevskii.Result.Result.Ok<Dvchevskii.Unit.Unit, E>(
                Dvchevskii.Unit.Unit.Default
            );
        }
    }
}
