using GeometricCalculator.Result;

namespace GeometricCalculator.Figures;

public class Triangle : IFigure
{
    public double A { get; }
    public double B { get; }
    public double C { get; }

    private Triangle(double a, double b, double c)
    {
        A = a;
        B = b;
        C = c;
    }

    private double? _area;

    public double Area
    {
        get
        {
            if (_area == null)
            {
                var p = A + B + C;
                _area = Math.Sqrt(p * (p - A) * (p - B) * (p - C));
            }

            return _area.Value;
        }
    }

    private bool? _isRight;

    public bool IsRight
    {
        get
        {
            if (_isRight == null)
            {
                _isRight = Math.Abs(A * A - (B * B + C * C)) < Constants.Tolerance ||
                           Math.Abs(B * B - (A * A + C * C)) < Constants.Tolerance ||
                           Math.Abs(C * C - (B * B + A * A)) < Constants.Tolerance;
            }

            return _isRight.Value;
        }
    }

    /// <summary>
    /// Create Triangle by sides lenght 
    /// </summary>
    /// <exception cref="ErrorCode.LessOrEqualZero" />
    public static Result<Triangle> Create(double a, double b, double c)
    {
        if (a <= 0 || b <= 0 || c <= 0)
            return Result<Triangle>.Error(ErrorCode.LessOrEqualZero,
                $"Sides: {a}, {b}, {c}.Sides can't be less or equal then 0");
        return Result<Triangle>.Ok(new Triangle(a, b, c));
    }

    /// <summary>
    /// Create Triangle by points 
    /// </summary>
    /// <exception cref="ErrorCode.PointsOnOneLine" />
    public static Result<Triangle> Create(PointD a, PointD b, PointD c)
    {
        if (Utils.IsPointsOnLine(new[] { a, b, c }))
            return Result<Triangle>.Error(ErrorCode.PointsOnOneLine, "All points on one line");

        return Create(
            a.GetDistanceTo(b),
            b.GetDistanceTo(c),
            c.GetDistanceTo(a));
    }
}