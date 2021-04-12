using CovidPropagationGraphicInterface.Classes;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Bus : Vehicle
    {
        private List<PointF> _trajectory;
        private int _trajectoryIndex;
        private bool _shouldRotate;
        public Bus(PointF location, Size size, int maxPerson, Pen color, List<PointF> trajectory, bool _isHorizontal = true) : base(location, size, maxPerson, color)
        {
            _trajectory = trajectory;
            _trajectoryIndex = 0;
            _shouldRotate = false;
            if (!_isHorizontal)
                Rotate();
        }

        public Bus(List<PointF> trajectory, PointF location, bool _isHorizontal = true) : this(location, GlobalVariables.bus_Size, GlobalVariables.BUS_MAX_PERSON, GlobalVariables.bus_Pen, trajectory, _isHorizontal)
        {
            _trajectoryIndex = trajectory.FindIndex(x => x.Equals(location));
        }

        public Bus(List<PointF> trajectory, bool _isHorizontal = true) : this(trajectory[0], GlobalVariables.bus_Size, GlobalVariables.BUS_MAX_PERSON, GlobalVariables.bus_Pen, trajectory, _isHorizontal)
        {
            // Does nothing
        }

        public void SetLine(List<PointF> trajectory, PointF location)
        {
            _trajectory = trajectory;
            _location = location;
            _trajectoryIndex = _trajectory.FindIndex(x => x.Equals(_location));
        }

        public void NextDestination()
        {
            if (++_trajectoryIndex > _trajectory.CountFromZero())
                _trajectoryIndex = 0;

            _destination = _trajectory[_trajectoryIndex];
            _shouldRotate = true;
            CalculateMovementSpeed();
        }

        private void Rotate()
        {
            Size tempSize = _size;
            _size.Width = tempSize.Height;
            _size.Height = tempSize.Width;
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            if (_shouldRotate)
            {
                Rotate();
                _shouldRotate = false;
            }
            e.Graphics.DrawRectangle(_color, new Rectangle(Point.Round(_location), _size));
        }
    }
}
