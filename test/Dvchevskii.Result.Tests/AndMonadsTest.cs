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
        Result.Err<int>(new Exception("__test__")).IsErrAnd(e => e.Message == "__test__");
    }

    [TestMethod]
    public void Test_And()
    {
        Result.Ok(0).And(Result.Ok(1)).Unwrap().Should().Be(1);
    }
}