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
                Ok<int, Exception> => true,
                _ => false
            }
        ).Should().BeTrue();

        (
            Result.Err<int>(null) switch
            {
                Ok<int, Exception> => true,
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
                Ok<int, Exception> ok => ok == 42,
                _ => false
            }
        ).Should().BeTrue();

        (
            Result.Err<int>(null) switch
            {
                Ok<int, Exception> ok => ok == 42,
                _ => false
            }
        ).Should().BeFalse();
    }

    [TestMethod]
    public void Test_Err()
    {
        (
            Result.Err<int>(new Exception()) switch
            {
                Err<int, Exception> => true,
                _ => false
            }
        ).Should().BeTrue();

        (
            Result.Ok(42) switch
            {
                Err<int, Exception> => true,
                _ => false
            }
        ).Should().BeFalse();
    }

    [TestMethod]
    public void Test_ErrGeneric()
    {
        (
            Result.Err<int>(new Exception("__test__")) switch
            {
                Err<int, Exception> e => e.UnwrapErrUnchecked().Message == "__test__",
                _ => false
            }
        ).Should().BeTrue();

        (
            Result.Ok(42) switch
            {
                Err<int, Exception> e => e.UnwrapErrUnchecked().Message == "__test__",
                _ => false
            }
        ).Should().BeFalse();
    }
}
