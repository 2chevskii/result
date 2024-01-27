namespace Dvchevskii.Result
{
    public interface IErr<out E>
    {
        E Error { get; }
    }
}
