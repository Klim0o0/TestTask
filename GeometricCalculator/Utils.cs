namespace GeometricCalculator;

internal static class Utils
{
    internal static bool IsPointsOnLine(PointD[] points)
    {
        var point1 = points[0];
        var point2 = points[1];

        var a = point2.X - point1.X;
        var b = point2.Y - point1.Y;
        var x1 = point1.X;
        var y1 = point1.Y;
        foreach (var point in points.Skip(2))
        {
            if (Math.Abs((point.X - x1)*b - (point.Y - y1) *a) > Constants.Tolerance)
                return false;
        }

        return true;
    }
}