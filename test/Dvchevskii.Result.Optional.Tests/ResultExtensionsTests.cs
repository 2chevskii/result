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
    }
}
