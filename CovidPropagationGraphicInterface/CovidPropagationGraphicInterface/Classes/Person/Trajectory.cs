using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Trajectory
    {
        private PointF _pointA;
        private PointF _pointB;
        private Pen _pen;
        private bool _enabled;
        public bool Enabled { get => _enabled; set => _enabled = value; }

        public Trajectory()
        {
            _pen = new Pen(Color.Black, 1);
            Enabled = true;
        }

        public void SetTrajectory(PointF pointA, PointF pointB)
        {
            this._pointA = pointA;
            this._pointB = pointB;
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            if (Enabled)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.DrawLine(_pen, _pointA, _pointB);
            }
        }
    }
}
