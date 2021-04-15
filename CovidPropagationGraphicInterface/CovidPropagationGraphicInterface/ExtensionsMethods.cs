using System.Collections.Generic;
using System.Drawing;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;

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

        public static void SetData(this LiveCharts.WinForms.CartesianChart chart, List<LineSeries> data)
        {
            data.ForEach(d => chart.Series.Add(d));

            chart.AxisX.Add(new Axis
            {
                Title = "Jours de la semaine",
                Labels = new[] { "Lun", "Mar", "Mer", "Jeu", "Ven", "Sam", "Dim" }
            });

            chart.AxisY.Add(new Axis
            {
                Title = "Nombre de cas",
                LabelFormatter = value => value.ToString(),
                MinValue = 0
            });
        }

        public static void SetData(this LiveCharts.WinForms.PieChart chart, List<PieSeries> data)
        {
            data.ForEach(d => chart.Series.Add(d));

            chart.AxisX.Add(new Axis
            {
                Title = "Person",
                Labels = new[] { "pi", "e", "ch", "art" }
            });

            chart.AxisX.Add(new Axis
            {
                Title = "Virus",
                Labels = new[] { "l", "ee", "cc", "kk" }
            });
        }
    }
}
