using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class ResultTests
{
    private IResult<int, Exception> okResult,
        errResult;

    [TestInitialize]
    public void Initialize()
    {
        okResult = Result.Ok(42);
        errResult = Result.Err<int>(new Exception("Some ex"));
    }

    [TestMethod]
    public void Test_IsOk()
    {
        okResult.IsOk().Should().BeTrue();
        errResult.IsOk().Should().BeFalse();
    }

    [TestMethod]
    public void Test_IsErr()
    {
        okResult.IsErr().Should().BeFalse();
        errResult.IsErr().Should().BeTrue();
    }

    [TestMethod]
    public void Test_IsOkAnd()
    {
        Predicate<int> predicate = v => v == 42;

        okResult.IsOkAnd(predicate).Should().BeTrue();
        Result.Ok(30).IsOkAnd(predicate).Should().BeFalse();
        errResult.IsOkAnd(predicate).Should().BeFalse();
    }

    [TestMethod]
    public void Test_IsErrAnd()
    {
        okResult.IsErrAnd(e => e != null).Should().BeFalse();
        errResult.IsErrAnd(e => e != null).Should().BeFalse();
        errResult.IsErrAnd(e => e is InvalidOperationException).Should().BeFalse();
    }

    [TestMethod]
    public void Test_Map()
    {
        Result.Ok(42).Map(x => x + 8).Unwrap().Should().Be(50);
    }
}
