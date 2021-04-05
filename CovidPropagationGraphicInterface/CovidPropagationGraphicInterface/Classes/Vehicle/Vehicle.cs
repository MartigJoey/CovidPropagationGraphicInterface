using CovidPropagationGraphicInterface.Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    abstract class Vehicle : Activity
    {
        protected Random rdm = new Random();
        protected PointF _location;
        protected Size _size;
        private PointF _destination;
        protected Pen _color;
        protected int _maxPersons;
        private float _movementX;
        private float _movementY;
        public PointF Location { get => _location; }
        public PointF Inside // A modifier pour intégrer un système de "siège" / places fixes limitées.
        {
            get => new Point(
                      rdm.Next((int)_location.X + Constant.PERSON_SIZE.Width, (int)_location.X + _size.Width - Constant.PERSON_SIZE.Width),
                      rdm.Next((int)_location.Y + Constant.PERSON_SIZE.Height, (int)_location.Y + _size.Height - Constant.PERSON_SIZE.Height)
                  );
        }
        public Size Size { get => _size; }

        public Vehicle(Point location, Size size, int maxPerson, Pen color)
        {
            _location = location;
            _size = size;
            _maxPersons = maxPerson;
            _color = color;
        }

        public void TeleportToLocation(PointF destination)
        {
            _location = destination;
        }

        public void SetDestination(PointF destination)
        {
            _destination = destination; 
            CalculateMovementSpeed();
        }

        public void GoToLocation()
        {
            _location.X += _movementX;
            _location.Y += _movementY;
        }

        private void CalculateMovementSpeed()
        {
            float distanceX, distanceY;

            distanceX = (float)Math.Sqrt(Math.Pow(_destination.X - _location.X, 2));
            distanceY = (float)Math.Sqrt(Math.Pow(_destination.Y - _location.Y, 2));

            distanceX = _destination.X < _location.X ? distanceX * -1 : distanceX;
            distanceY = _destination.Y < _location.Y ? distanceY * -1 : distanceY;

            _movementX = distanceX / Constant.ANIMATION_PER_PERIOD;
            _movementY = distanceY / Constant.ANIMATION_PER_PERIOD;
            //Console.WriteLine($"Movement {_movementX} {_movementY}");
            //Console.WriteLine($"Distance {distanceX} {distanceY}");
            //Console.WriteLine($"Destination {_destination}");
            //Console.WriteLine($"Location {_location}");
            //Console.WriteLine("_______________________");
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_color, new Rectangle(Point.Round(_location), _size));
        }
    }
}
