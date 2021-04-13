using System.Collections.Generic;
using System.Drawing;

namespace CovidPropagationGraphicInterface
{
    public static class ExtensionsMethods
    {
        public static int CountFromZero<T>(this List<T> list)
        {
            return list.Count - 1;
        }

        public static Point Clone(this Point point)
        {
            return new Point(point.X, point.Y);
        }

        public static PointF Clone(this PointF pointF)
        {
            return new PointF(pointF.X, pointF.Y);
        }
    }
}
