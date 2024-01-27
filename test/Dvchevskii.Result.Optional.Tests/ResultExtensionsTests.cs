using Dvchevskii.Optional;
using FluentAssertions;

namespace Dvchevskii.Result.Optional.Tests;

[TestClass]
public class ResultExtensionsTests
{
    [TestMethod]
    public void Test_Ok()
    {
        Result.Ok(42).Ok().Should().Be(Option.Some(42));
        Result.Err<int>(new Exception()).Ok().Should().Be(Option.None<int>());
    }

    [TestMethod]
    public void Test_Err()
    {
        Result.Ok(42).Err().Should().Be(Option.None<Exception>());
        Exception ex = new Exception();
        Result.Err<int>(ex).Err().Should().Be(Option.Some(ex));
    }

    [TestMethod]
    public void Test_Transpose()
    {
        Result
            .Ok(Option.Some(42))
            .Transpose()
            .Should()
            .Be(Option.Some(Result.Ok<int, Exception>(42)));
    }
}
