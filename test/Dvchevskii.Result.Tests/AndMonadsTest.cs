using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class AndMonadsTest
{
    [TestMethod]
    public void Test_IsOkAnd()
    {
        Result.Ok(42).IsOkAnd(v => v == 42).Should().BeTrue();
    }

    [TestMethod]
    public void Test_IsErrAnd()
    {
        Result
            .Err<int>(new Exception("__test__"))
            .IsErrAnd(e => e.Message == "__test__")
            .Should()
            .BeTrue();
    }

    [TestMethod]
    public void Test_And()
    {
        Result.Ok(0).And(Result.Ok(1)).Should().Be(Result.Ok(1));
    }

    [TestMethod]
    public void Test_AndThen()
    {
        Result.Ok(0).AndThen(_ => Result.Ok("test")).Should().Be(Result.Ok("test"));
    }
}

[TestClass]
public class OrMonadsTest
{
    [TestMethod]
    public void Test_Or()
    {
        Result.Ok(0).Or(Result.Ok(2)).Should().Be(Result.Ok(0));
        Result.Err<int>(null).Or(Result.Ok(1)).Should().Be(Result.Ok(1));
    }

    [TestMethod]
    public void Test_OrElse()
    {
        Result.Ok(0).OrElse(_ => Result.Ok(2)).Should().Be(Result.Ok(0));
        Result
            .Err<int>(null)
            .OrElse(_ => Result.Err<int, string>("_test_"))
            .Should()
            .Be(Result.Err<int, string>("_test_"));
    }
}
