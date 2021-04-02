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
        protected Pen _color;
        protected int _maxPersons;

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

        public void TeleportToLocation()
        {
            PointF destination;
            //_location = destination;
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_color, new Rectangle(Point.Round(_location), _size));
        }
    }
}
