using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class TypeConversionTests
{
    private Result<int, float> ConvertOk()
    {
        var result = Result.Ok(42);
        return result.MorphOk<float>();
    }

    private Result<float, Exception> ConvertErr()
    {
        var result = Result.Err<int>(new Exception());
        return result.MorphErr<float>();
    }

    [TestMethod]
    public void Test_OkResultTypeConversion()
    {
        ConvertOk().State().Should().Be(ResultState.Ok);
        ConvertOk().Unwrap().Should().Be(42);
        ConvertOk().Should().BeAssignableTo<Result<int, float>>();
    }

    [TestMethod]
    public void Test_ErrResultTypeConversion()
    {
        ConvertErr().State().Should().Be(ResultState.Err);
        ConvertErr().UnwrapErr().Should().NotBeNull();
        ConvertErr().Should().BeAssignableTo<Result<float, Exception>>();
    }
}
