using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class ComparableTests
{
    [TestMethod]
    public void Test_CompareTo()
    {
        Result.Ok(42).CompareTo(Result.Ok(42)).Should().Be(0);
        Result.Err<int>(new Exception()).CompareTo(Result.Ok(42)).Should().Be(-1);
    }
}
