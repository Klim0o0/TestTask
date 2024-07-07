using GeometricCalculator.Result;

namespace GeometricCalculator.Figures;

public class Figure : IFigure
{
    private readonly PointD[] _points;

    private Figure(params PointD[] points)
    {
        _points = points;
    }

    public PointD[] Points
    {
        get { return _points.Select(x => x).ToArray(); }
    }

    private double? _area;

    public double Area
    {
        get
        {
            if (_area == null)
            {
                double area = 0;
                for (var i = 0; i < _points.Length - 1; i++)
                {
                    area += _points[i].X * _points[i + 1].Y - _points[i].Y * _points[i + 1].X;
                }

                area += _points[^1].X * _points[0].Y - _points[^1].Y * _points[0].X;
                _area = Math.Abs(area) / 2;
            }


            return _area.Value;
        }
    }

    /// <summary>
    /// Create Figure without self crossing by points 
    /// </summary>
    /// <exception cref="ErrorCode.LessThenTwoVertices" />
    public static Result<Figure> Create(params PointD[] points)
    {
        if (points.Length <= 2)
            return Result<Figure>.Error(ErrorCode.LessThenTwoVertices, $"Points count: {points.Length}, is not figure");

        if (Utils.IsPointsOnLine(points))
            return Result<Figure>.Error(ErrorCode.PointsOnOneLine, "All points on one line");


        return Result<Figure>.Ok(new Figure(points));
    }
}