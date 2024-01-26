using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class PatternMatchingTests
{
    [TestMethod]
    public void Test_Ok()
    {
        (
            Result.Ok(42) switch
            {
                IOk => true,
                _ => false
            }
        ).Should().BeTrue();

        (
            Result.Err<int>(null) switch
            {
                IOk => true,
                _ => false
            }
        ).Should().BeFalse();
    }

    [TestMethod]
    public void Test_OkGeneric()
    {
        (
            Result.Ok(42) switch
            {
                IOk<int> ok => ok.Value == 42,
                _ => false
            }
        ).Should().BeTrue();

        (
            Result.Err<int>(null) switch
            {
                IOk<int> ok => ok.Value == 42,
                _ => false
            }
        ).Should().BeFalse();
    }

    [TestMethod]
    public void Test_Err()
    {
        (Result.Err<int>(new Exception()) switch
        {
            IErr => true,
            _ => false
        }).Should().BeTrue();

        (Result.Ok(42) switch
        {
            IErr => true,
            _ => false
        }).Should().BeFalse();
    }

    [TestMethod]
    public void Test_ErrGeneric()
    {
        (Result.Err<int>(new Exception("__test__")) switch
        {
            IErr<Exception> e => e.Error.Message == "__test__",
            _ => false
        }).Should().BeTrue();

        (Result.Ok(42) switch
        {
            IErr<Exception> e => e.Error.Message == "__test__",
            _ => false
        }).Should().BeFalse();
    }
}
