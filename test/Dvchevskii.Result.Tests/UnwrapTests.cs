using Dvchevskii.Result.Exceptions;
using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class UnwrapTests
{
    [TestMethod]
    public void Test_Expect()
    {
        Result.Ok(42).Expect("__test__").Should().Be(42);
        Result
            .Err<int>(new Exception("__test__"))
            .Invoking(r => r.Expect("__expectMsg__"))
            .Should()
            .Throw<UnexpectedResultException>()
            .And.Message.Should()
            .Be("__expectMsg__");
    }

    [TestMethod]
    public void Test_Unwrap()
    {
        Result.Ok(42).Unwrap().Should().Be(42);
        Result
            .Err<int>(new Exception("__test__"))
            .Invoking(r => r.Unwrap())
            .Should()
            .Throw<UnexpectedResultException>()
            .And.Message.Should()
            .Be("Result was not in Ok state");
    }

    [TestMethod]
    public void Test_UnwrapOrDefault()
    {
        Result.Ok(42).UnwrapOrDefault().Should().Be(42);
        Result.Err<int>(null).UnwrapOrDefault().Should().Be(default(int));
    }

    [TestMethod]
    public void Test_UnwrapOr()
    {
        Result.Ok(42).UnwrapOr(69).Should().Be(42);
        Result.Err<int>(null).UnwrapOr(69).Should().Be(69);
    }

    [TestMethod]
    public void Test_UnwrapOrElse()
    {
        Result.Ok(42).UnwrapOrElse(_ => 420).Should().Be(42);
        Result.Err<int>(null).UnwrapOrElse(_ => 420).Should().Be(420);
    }

    [TestMethod]
    public void Test_UnwrapUnchecked()
    {
        Result.Ok(42).UnwrapUnchecked().Should().Be(42);
        Result.Ok<string?>(null).UnwrapUnchecked().Should().Be(null);
        Result.Err<Exception>(null).UnwrapUnchecked().Should().BeNull();
        Result.Err<Exception>(new Exception()).UnwrapUnchecked().Should().NotBeNull();
        Result.Err<Exception>(new InvalidOperationException()).Should().NotBeNull();
    }
}