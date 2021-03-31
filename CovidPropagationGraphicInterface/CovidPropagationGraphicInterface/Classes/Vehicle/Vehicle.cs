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
    abstract class Vehicle
    {
        protected Random rdm = new Random();
        protected Point _location;
        protected Size _size;
        protected Pen _color;
        protected int _maxPersons;

        public Point Location { get => _location; }
        public Point Inside // A modifier pour intégrer un système de "siège" / places fixes limitées.
        {
            get => new Point(
                      rdm.Next(_location.X + Constant.PERSON_SIZE.Width, _location.X + _size.Width - Constant.PERSON_SIZE.Width),
                      rdm.Next(_location.Y + Constant.PERSON_SIZE.Height, _location.Y + _size.Height - Constant.PERSON_SIZE.Height)
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

        public abstract void Paint(object sender, PaintEventArgs e);
    }
}
