using System;

namespace Dvchevskii.Result
{
    public interface IErr { }

    public interface IErr<out E>
    {
        E Error { get; }
    }
}
