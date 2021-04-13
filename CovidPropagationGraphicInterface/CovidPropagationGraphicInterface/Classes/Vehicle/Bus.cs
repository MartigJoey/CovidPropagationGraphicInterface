using CovidPropagationGraphicInterface.Classes;
using CovidPropagationGraphicInterface.Classes.Vehicle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Bus : Vehicle
    {
        private BusLine _busLine;
        private int _busStopIndex;
        private int _pointIndex;
        private bool _shouldRotate;

        public Bus(PointF location, Size size, int maxPerson, Pen color, BusLine busLine, bool _isHorizontal = true) : base(location, size, maxPerson, color)
        {
            _busLine = busLine;
            _busStopIndex = 0;
            _pointIndex = 0;
            _shouldRotate = false;
            if (!_isHorizontal)
                Rotate();
        }

        public Bus(BusLine busLine, PointF location, bool _isHorizontal = true) : this(location, GlobalVariables.bus_Size, GlobalVariables.BUS_MAX_PERSON, GlobalVariables.bus_Pen, busLine, _isHorizontal)
        {
            // Does nothing
        }

        public Bus(BusLine busLine, bool _isHorizontal = true) : this(busLine.GetCurrentStop(0).GetCurrentPoint(0).Key, GlobalVariables.bus_Size, GlobalVariables.BUS_MAX_PERSON, GlobalVariables.bus_Pen, busLine, _isHorizontal)
        {
            // Does nothing
        }

        public void NextStop()
        {
            _busStopIndex = _busLine.GetNextStopIndex(_busStopIndex);
            _pointIndex = 1;
            _destination = _busLine.GetCurrentStop(_busStopIndex).GetCurrentPoint(_pointIndex).Key;
            CalculateMovementSpeed(_busLine.GetCurrentStop(_busStopIndex).GetThisPointLengthInPercent(_pointIndex));
        }

        public void NextPoint()
        {
            _pointIndex = _busLine.GetCurrentStop(_busStopIndex).GetNextPointIndex(_pointIndex);
            _destination = _busLine.GetCurrentStop(_busStopIndex).GetCurrentPoint(_pointIndex).Key;
            _shouldRotate = true;
            CalculateMovementSpeed(_busLine.GetCurrentStop(_busStopIndex).GetThisPointLengthInPercent(_pointIndex));
        }

        protected void CalculateMovementSpeed(float busStopPercent)
        {
            float distanceX, distanceY, animationNb;

            distanceX = (float)Math.Sqrt(Math.Pow(_destination.X - _location.X, 2));
            distanceY = (float)Math.Sqrt(Math.Pow(_destination.Y - _location.Y, 2));

            distanceX = _destination.X < _location.X ? distanceX * -1 : distanceX;
            distanceY = _destination.Y < _location.Y ? distanceY * -1 : distanceY;

            animationNb = (float)GlobalVariables.ANIMATION_PER_PERIOD / 100 * busStopPercent;

            //Console.WriteLine(animationNb);

            _movementX = distanceX / (float)Math.Round(animationNb, 0);
            _movementY = distanceY / (float)Math.Round(animationNb, 0);

            //Console.WriteLine(_location + " " + _destination);
            //Console.WriteLine(distanceX + "  movementX: " + distanceX / animationNb);
            //Console.WriteLine(distanceY + "  movementY: " + distanceY / animationNb);
            //Console.WriteLine("movementX: " + _movementX + "  DistanceX:" + distanceX);
            //Console.WriteLine("movementY: " + _movementY + "  DistanceY:"+ distanceY);
            //Console.WriteLine("___________________________ " + animationNb);
        }

        private void Rotate()
        {
            Size tempSize = _size;
            _size.Width = tempSize.Height;
            _size.Height = tempSize.Width;
        }

        public override void GoToLocation()
        {
            _location.X += _movementX;
            _location.Y += _movementY;

            if (Point.Round(_location).Equals(Point.Round(_destination)))
                NextPoint();
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
