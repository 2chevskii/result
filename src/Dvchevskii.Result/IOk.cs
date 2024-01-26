namespace Dvchevskii.Result
{
    public interface IOk { }

    public interface IOk<out T>
    {
        T Value { get; }
    }
}
