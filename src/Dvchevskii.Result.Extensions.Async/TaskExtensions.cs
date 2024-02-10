using System;
using System.Threading.Tasks;

namespace Dvchevskii.Result.Extensions.Async
{
    public static class TaskExtensions
    {
        public static Task<U> ThenMap<T, E, U>(
            this Task<Result<T, E>> self,
            Func<T, U> okMapper,
            Func<E, U> errMapper
        ) => self.ContinueWith(s => s.Result.MapOrElse(okMapper, errMapper));
    }
}
