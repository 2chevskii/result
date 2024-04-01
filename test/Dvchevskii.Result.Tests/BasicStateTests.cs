using FluentAssertions;
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace Dvchevskii.Result.Tests;

[TestClass]
public class BasicStateTests
{
    private Result[] _okStateSubjects;
    private Result[] _errStateSubjects;

    [TestInitialize]
    public void Initialize()
    {
        _okStateSubjects =
        [
            Result.Ok(0),
            Result.Ok<int, IndexOutOfRangeException>(42),
            Result.Ok("__testString__"),
            Result.Ok(new Exception("__test__"))
        ];
        _errStateSubjects =
        [
            Result.Err<int>(new Exception("__testString__")),
            Result.Err<int, int>(42),
            Result.Err<int, Exception>(new InvalidOperationException())
        ];
    }

    [TestMethod]
    public void Test_IsOk()
    {
        _okStateSubjects.Should().OnlyContain(r => r.IsOk);
        _errStateSubjects.Should().OnlyContain(r => r.IsOk);
    }

    [TestMethod]
    public void Test_IsErr()
    {
        _okStateSubjects.Should().OnlyContain(r => r.IsErr);
        _errStateSubjects.Should().OnlyContain(r => r.IsErr);
    }
}
