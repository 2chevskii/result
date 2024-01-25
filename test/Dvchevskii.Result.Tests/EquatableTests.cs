using FluentAssertions;

namespace Dvchevskii.Result.Tests;

[TestClass]
public class EquatableTests
{
    private IResult<int, Exception> okResult;
    private IResult<int, Exception> errResult;

    [TestInitialize]
    public void Initialize()
    {
        okResult = Result.Ok(42);
        errResult = Result.Err<int>(new Exception("__test__"));
    }

    [TestMethod]
    public void Test_EqualsBasic()
    {
        // both ok and err should never be equal to null
        okResult.Equals(null).Should().BeFalse();
        errResult.Equals(null).Should().BeFalse();
    }

    [TestMethod]
    public void Test_EqualsResultState()
    {
        okResult.Equals(ResultState.Ok).Should().BeTrue();
        okResult.Equals(ResultState.Err).Should().BeFalse();
        errResult.Equals(ResultState.Err).Should().BeTrue();
        errResult.Equals(ResultState.Ok).Should().BeFalse();
    }

    [TestMethod]
    public void Test_EqualsSameState()
    {
        /*
         * Results with the same state should be equal if they contain
         * equal underlying value
         * And should differ if values are different
         */
        okResult.Equals(Result.Ok(42)).Should().BeTrue();
        okResult.Equals(Result.Ok(31)).Should().BeFalse();

        // next one should be false because of referential inequality between two Exception instances
        errResult.Equals(Result.Err<int>(new Exception("__test__"))).Should().BeFalse();
        // this one is true though, because error value int(20)==int(20)
        Result.Err<int, int>(20).Equals(Result.Err<int, int>(20)).Should().BeTrue();
    }

    [TestMethod]
    public void Test_EqualsDifferentState()
    {
        /*
         * Results of different states should always be different
         * Even if their underlying values are the same
         */

        okResult.Equals(errResult).Should().BeFalse();
        Result.Ok<int, int>(10).Equals(Result.Err<int, int>(10)).Should().BeFalse();
    }

    [TestMethod]
    public void Test_EqualsUnderlyingValue()
    {
        /*
         * Results SHOULD NOT be equal to their underlying values.
         * Only, if this value is wrapped into the result as seen above
         */

        okResult.Equals(42).Should().BeFalse();
        Result.Err<int, int>(20).Equals(20).Should().BeFalse();
    }
}
