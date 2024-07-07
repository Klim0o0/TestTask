using FluentAssertions;
using GeometricCalculator;
using GeometricCalculator.Figures;
using GeometricCalculator.Result;
using NUnit.Framework;

namespace UnitTests;

[TestFixture]
public class FigureTests
{
    public static object[] CreateCases()
    {
        return new object[]
        {
            new object[] { new[] { new PointD(0, 0), new PointD(0, 5), new PointD(5, 0) }, 12.5 },
            new object[] { new[] { new PointD(0, 0), new PointD(0, 5), new PointD(5, 5), new PointD(5, -2) }, 30 },
            new object[]
            {
                new[] { new PointD(0, 0), new PointD(0, 5), new PointD(5, 5), new PointD(5, -2), new PointD(-5, -5) },
                47.5
            }
        };
    }

    [TestCaseSource(nameof(CreateCases))]
    public void ShouldSuccessCalculateValidFigure(PointD[] pointDs, double expectedValue)
    {
        var figure = Figure.Create(pointDs);
        figure.IsSuccess.Should().BeTrue();
        figure.ErrorCode.Should().BeNull();
        figure.Value!.Area.Should().Be(expectedValue);
    }

    [Test]
    public void ShouldErrorWhenLess3Points()
    {
        var figure = Figure.Create(new PointD(0, 0), new PointD(0, 5));
        figure.IsSuccess.Should().BeFalse();
        figure.ErrorCode.Should().Be(ErrorCode.LessThenTwoVertices);
    }

    public static object[] CreateOneLinePoints()
    {
        return new object[]
        {
            new object[] { new [] { new PointD(0, 0), new PointD(0, 5), new PointD(0, 1) } },
            new object[] { new [] { new PointD(1, 0), new PointD(2, 0), new PointD(3, 0), new PointD(5, 0) } }
        };
    }


    [TestCaseSource(nameof(CreateOneLinePoints))]
    public void ShouldErrorWhenThreePointsOnLine(PointD[] points)
    {
        var figure = Figure.Create(points);
        figure.IsSuccess.Should().BeFalse();
        figure.ErrorCode.Should().Be(ErrorCode.PointsOnOneLine);
    }
    
    [Test]
    public void ShouldSuccessWhenThreePointsOnLineButOtherNot()
    {
        var figure = Figure.Create(new PointD(1, 0), new PointD(2, 0), new PointD(3, 0), new PointD(5, 5) );
        var expectedFigure = Figure.Create(new PointD(1, 0), new PointD(3, 0), new PointD(5, 5) );
        figure.IsSuccess.Should().BeTrue();
        figure.Value!.Area.Should().Be(expectedFigure.Value!.Area);
    }
}