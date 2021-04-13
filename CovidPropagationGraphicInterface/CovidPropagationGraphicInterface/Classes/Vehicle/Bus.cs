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
        private bool _isVertical;

        public Bus(BusLine busLine, int currentStop = 1) : base(busLine.GetCurrentStop(currentStop).GetLastPoint().location, GlobalVariables.bus_Size, GlobalVariables.BUS_MAX_PERSON, GlobalVariables.bus_Pen)
        {
            _busLine = busLine;
            _busStopIndex = currentStop;
            _pointIndex = 0;
            _isVertical = false;
        }

        public Bus(BusLine busLine) : this(busLine, 0)
        {
            // Does nothing
        }

        public void NextStop()
        {
            BusStopPoint busStop;
            PointF oldDestination = _destination;
            _busStopIndex = _busLine.GetNextStopIndex(_busStopIndex);
            _pointIndex = 1;
            busStop = _busLine.GetCurrentStop(_busStopIndex).GetCurrentPoint(_pointIndex);
            _isVertical = busStop.isVertical;
            _destination = busStop.location;
            CalculateMovementSpeed(_busLine.GetCurrentStop(_busStopIndex).GetThisPointDurationInPercent(_pointIndex), oldDestination);
        }

        public void NextPoint()
        {
            PointF oldDestination = _destination;
            BusStop currentStop = _busLine.GetCurrentStop(_busStopIndex);
            BusStopPoint point = currentStop.GetCurrentPoint(_pointIndex);
            _pointIndex = currentStop.GetNextPointIndex(_pointIndex);
            _destination = point.location;
            _isVertical = point.isVertical;
            CalculateMovementSpeed(point.durationInPercent, oldDestination);
        }

        protected void CalculateMovementSpeed(float busStopPercent, PointF location)
        {
            float distanceX, distanceY, animationNb;
            _location = location;

            distanceX = (float)Math.Sqrt(Math.Pow(_destination.X - _location.X, 2));
            distanceY = (float)Math.Sqrt(Math.Pow(_destination.Y - _location.Y, 2));

            distanceX = _destination.X < _location.X ? distanceX * -1 : distanceX;
            distanceY = _destination.Y < _location.Y ? distanceY * -1 : distanceY;

            animationNb = (float)GlobalVariables.ANIMATION_PER_PERIOD / 100 * busStopPercent;

            _movementX = distanceX / (float)Math.Round(animationNb, 0);
            _movementY = distanceY / (float)Math.Round(animationNb, 0);
        }

        private void Rotate()
        {
            if (_isVertical)
            {
                _size.Width = GlobalVariables.bus_Size.Width;
                _size.Height = GlobalVariables.bus_Size.Height;
            }
            else
            {
                _size.Width = GlobalVariables.bus_Size.Height;
                _size.Height = GlobalVariables.bus_Size.Width;
            }
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
            Rotate();
            e.Graphics.DrawRectangle(_color, new Rectangle(Point.Round(_location), _size));
        }
    }
}
