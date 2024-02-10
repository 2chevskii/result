using FluentAssertions;

namespace Dvchevskii.Result.Extensions.Async.Tests;

[TestClass]
public class TaskExtensionsTests
{
    [TestMethod]
    public async Task Test_ThenMap()
    {
        (await Task.Run(() => Result.Ok<int, Exception>(42)).ThenMap(v => v * 2, _ => 0))
            .Should()
            .Be(42 * 2);

        (
            await Task.Run(() => Result.Err<int, Exception>(new Exception("__test__")))
                .ThenMap(_ => new Exception(), e => e)
        )
            .Message.Should()
            .Be("__test__");
    }
}
