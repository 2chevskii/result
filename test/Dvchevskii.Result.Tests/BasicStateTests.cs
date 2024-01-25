using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class BasicStateTests
{
    private IResult[] okStateSubjects;
    private IResult[] errStateSubjects;

    [TestInitialize]
    public void Initialize()
    {
        okStateSubjects =
        [
            Result.Ok(0),
            Result.Ok<int, IndexOutOfRangeException>(42),
            Result.Ok("__testString__"),
            Result.Ok(new Exception("__test__"))
        ];
        errStateSubjects =
        [
            Result.Err<int>(new Exception("__testString__")),
            Result.Err<int, int>(42),
            Result.Err<int, Exception>(new InvalidOperationException())
        ];
    }

    [TestMethod]
    public void Test_IsOk()
    {
        okStateSubjects.Should().OnlyContain(r => r.IsOk());
        errStateSubjects.Should().OnlyContain(r => r.IsOk() == false);
    }

    [TestMethod]
    public void Test_IsErr()
    {
        okStateSubjects.Should().OnlyContain(r => r.IsErr() == false);
        errStateSubjects.Should().OnlyContain(r => r.IsErr());
    }
}
