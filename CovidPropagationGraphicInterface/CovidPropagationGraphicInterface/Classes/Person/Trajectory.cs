using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Trajectory
    {
        PointF pointA;
        PointF pointB;
        Pen pen;
        public Trajectory()
        {
            pen = new Pen(Color.Black, 1);
        }

        public void SetTrajectory(PointF pointA, PointF pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            e.Graphics.DrawLine(pen, pointA, pointB);
        }
    }
}
