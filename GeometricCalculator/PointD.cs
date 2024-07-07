namespace GeometricCalculator;

public class PointD
{
    public double X { get; }
    public double Y { get; }

    public PointD(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double GetDistanceTo(PointD other)
    {
        var xDistance = other.X - X;
        var yDistance = other.Y - Y;
        return Math.Sqrt(xDistance * xDistance + yDistance * yDistance);
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}