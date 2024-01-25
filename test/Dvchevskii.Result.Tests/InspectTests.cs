using FluentAssertions;
using FluentAssertions.Events;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class InspectTests
{
    [TestMethod]
    public void Test_Inspect()
    {
        bool isCalled = false;
        Result.Ok(42).Inspect(v => isCalled = true);
        isCalled.Should().BeTrue();
        isCalled = false;
        Result.Err<int>(null).Inspect(v => isCalled = true);
        isCalled.Should().BeFalse();
    }

    [TestMethod]
    public void Test_InspectErr()
    {
        bool isCalled = false;
        Result.Ok(42).InspectErr(e => isCalled = true);
        isCalled.Should().BeFalse();
        Result.Err<int>(null).InspectErr(v => isCalled = true);
        isCalled.Should().BeTrue();
    }
}
