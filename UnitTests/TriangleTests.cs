using FluentAssertions;
using GeometricCalculator;
using GeometricCalculator.Figures;
using GeometricCalculator.Result;
using NUnit.Framework;

namespace UnitTests;

[TestFixture]
public class TriangleTests
{
    [TestCase(2, 2, 2, 19.59)]
    public void ShouldSuccessWhenPositiveSides(double a, double b, double c, double expectedValue)
    {
        var triangle = Triangle.Create(a, b, c);
        triangle.IsSuccess.Should().BeTrue();
        triangle.Value!.Area.Should().BeInRange(expectedValue - 0.1, expectedValue + 0.1);
    }

    [TestCase(-2, 2, 2)]
    [TestCase(2, -2, 2)]
    [TestCase(2, 2, -2)]
    [TestCase(-2, -2, -2)]
    public void ShouldErrorWhenNegativeOrZeroSide(double a, double b, double c)
    {
        var triangle = Triangle.Create(a, b, c);
        triangle.IsSuccess.Should().BeFalse();
        triangle.ErrorCode.Should().Be(ErrorCode.LessOrEqualZero);
    }

    [TestCase(2, 2, 2, false)]
    [TestCase(2, 2, 2.82842712475, true)]
    public void ShouldSuccessRight(double a, double b, double c, bool expected)
    {
        var triangle = Triangle.Create(a, b, c);
        triangle.IsSuccess.Should().BeTrue();
        triangle.Value!.IsRight.Should().Be(expected);
    }


    public static object[] CreateRightAndNotTrianglesCases()
    {
        return new object[]
        {
            new object[] { new PointD(0, 0), new PointD(0, 5), new PointD(5, 0), true },
            new object[] { new PointD(0, 0), new PointD(0, 5), new PointD(5, 0.5), false }
        };
    }

    [TestCaseSource(nameof(CreateRightAndNotTrianglesCases))]
    public void ShouldSuccessRightCheckByPoints(PointD a, PointD b, PointD c, bool expected)
    {
        var triangle = Triangle.Create(a, b, c);
        triangle.IsSuccess.Should().BeTrue();
        triangle.Value!.IsRight.Should().Be(expected);
    }

    public static object[] CreateOneLinePoints()
    {
        return new object[]
        {
            new object[] { new PointD(0, 0), new PointD(0, 5), new PointD(0, 1) },
            new object[] { new PointD(1, 0), new PointD(2, 0), new PointD(3, 0) }
        };
    }
    
    
    [TestCaseSource(nameof(CreateOneLinePoints))]
    public void ShouldErrorWhenThreePointsOnLine(PointD a, PointD b, PointD c)
    {
        var triangle = Triangle.Create(new PointD(0,0), new PointD(0,1), new PointD(0,2));
        triangle.IsSuccess.Should().BeFalse();
        triangle.ErrorCode.Should().Be(ErrorCode.PointsOnOneLine);
    }
}