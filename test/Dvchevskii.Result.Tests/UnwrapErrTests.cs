using Dvchevskii.Result.Exceptions;
using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class UnwrapErrTests
{
    [TestMethod]
    public void Test_ExpectErr()
    {
        Result.Err<int>(new Exception()).Invoking(r => r.ExpectErr("__test__")).Should().NotThrow();
        Result
            .Ok(42)
            .Invoking(r => r.ExpectErr("__test__"))
            .Should()
            .Throw<UnexpectedResultException>()
            .And.Message.Should()
            .Be("__test__");
    }

    [TestMethod]
    public void Test_UnwrapErr()
    {
        Result
            .Err<int>(new Exception())
            .Invoking(r => r.UnwrapErr())
            .Should()
            .NotThrow()
            .And.NotBeNull();
        Result
            .Ok(42)
            .Invoking(r => r.UnwrapErr())
            .Should()
            .Throw<UnexpectedResultException>()
            .And.Message.Should()
            .Be("Result was not in Err state");
    }

    [TestMethod]
    public void Test_UnwrapErrUnchecked()
    {
        /*TODO*/
    }
}