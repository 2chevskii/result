using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class ToStringTests
{
    [TestMethod]
    public void Test_Ok_ToString()
    {
        Result.Ok(42).ToString().Should().Be($"Result<{nameof(Int32)}={42}, {nameof(Exception)}>");
    }

    [TestMethod]
    public void Test_Err_ToString()
    {
        Result
            .Err<int>(new Exception())
            .ToString()
            .Should()
            .Be($"Result<{nameof(Int32)}, {nameof(Exception)}={new Exception().ToString()}>");
        Result
            .Err<int>(null)
            .ToString()
            .Should()
            .Be($"Result<{nameof(Int32)}, {nameof(Exception)}=null>");
    }
}
