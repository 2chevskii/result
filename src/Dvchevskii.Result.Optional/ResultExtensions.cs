using Dvchevskii.Optional;

namespace Dvchevskii.Result.Optional
{
    public static class ResultExtensions
    {
        public static Option<T> Ok<T, E>(this Result<T, E> self) =>
            self.MapOrElse(Option.Some, _ => Option.None<T>());

        public static Option<E> Err<T, E>(this Result<T, E> self) =>
            self.MapOrElse(_ => Option.None<E>(), Option.Some);

        /// <summary>
        /// Transposes a <see cref="IResult"/> on an <see cref="Option"/> to an <see cref="Option"/> of a <see cref="IResult"/>
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="E"></typeparam>
        /// <returns></returns>
        public static Option<Result<T, E>> Transpose<T, E>(this Result<Option<T>, E> self) =>
            self.Match(
                opt =>
                    opt.MapOrElse(v => Option.Some(Result.Ok<T, E>(v)), Option.None<Result<T, E>>),
                e => Option.Some(Result.Err<T, E>(e))
            );
    }
}
