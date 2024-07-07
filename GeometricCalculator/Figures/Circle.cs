using GeometricCalculator.Result;

namespace GeometricCalculator.Figures;

public class Circle : IFigure
{
    public double Radius { get; }

    private Circle(double radius)
    {
        Radius = radius;
    }

    private double? _area;

    public double Area
    {
        get
        {
            if (_area == null)
            {
                _area = Math.PI * Radius * Radius;
            }

            return _area.Value;
        }
    }

    /// <summary>
    /// Create Circle by radius 
    /// </summary>
    /// <exception cref="ErrorCode.LessOrEqualZero" />
    public static Result<Circle> Create(double radius)
    {
        if (radius <= 0)
        {
            return Result<Circle>.Error(ErrorCode.LessOrEqualZero,
                $"Radius: {radius}.Radius can't be less or equal then 0");
        }

        return Result<Circle>.Ok(new Circle(radius));
    }
}