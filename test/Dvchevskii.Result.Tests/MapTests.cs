using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class MapTests
{
    [TestMethod]
    public void Test_Map()
    {
        Result.Ok(42).Map(v => v * 2).Should().Be(Result.Ok(42 * 2));
        Result.Err<int>(null).Map(v => v * 2).Should().Be(Result.Err<int>(null));
    }

    [TestMethod]
    public void Test_MapOr()
    {
        Result.Ok(42).MapOr(v => v * 2, 10).Should().Be(42 * 2);
        Result.Err<int>(new Exception("__test__")).MapOr(v => v * 2, 10).Should().Be(10);
    }

    [TestMethod]
    public void Test_MapOrElse()
    {
        Result.Ok(42).MapOrElse(v => 1, e => 0).Should().Be(1);
        Result
            .Err<int>(new Exception("__test__"))
            .MapOrElse(v => v.ToString(), e => e.Message)
            .Should()
            .Be("__test__");
    }

    [TestMethod]
    public void Test_MapErr()
    {
        Result.Ok(42).MapErr(e => "error mapped").Should().Be(Result.Ok<int, string>(42));
        Result
            .Err<int>(new Exception("__test__"))
            .MapErr(e => "error mapped")
            .Should()
            .Be(Result.Err<int, string>("error mapped"));
    }
}
