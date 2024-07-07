using FluentAssertions;
using GeometricCalculator.Figures;
using GeometricCalculator.Result;
using NUnit.Framework;

namespace UnitTests;

[TestFixture]
public class CircleTests
{
    [TestCase(1, Math.PI)]
    [TestCase(2, Math.PI * 4)]
    public void ShouldSuccessWhenPositiveRadius(double radius, double expectedValue)
    {
        var circle = Circle.Create(radius);
        circle.IsSuccess.Should().BeTrue();
        circle.Value!.Area.Should().Be(expectedValue);
    }

    [TestCase(0, TestName = "Zero radius")]
    [TestCase(-1, TestName = "Negative radius")]
    public void ShouldErrorWhenNegativeOrZeroRadius(double radius)
    {
        var circle = Circle.Create(radius);
        circle.IsSuccess.Should().BeFalse();
        circle.ErrorCode.Should().Be(ErrorCode.LessOrEqualZero);
    }
}