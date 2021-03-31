using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CovidPropagationGraphicInterface.Classes.Person;
using CovidPropagationGraphicInterface.Classes;

namespace CovidPropagationGraphicInterface
{
    class Person
    {
        private Trajectory _trajectory;
        private Planning _planning;
        private PointF _location;
        private Size _size;
        private Brush _defaultColor = Brushes.Blue;
        private Brush _color;

        public Person(Planning planning)
        {
            this._planning = planning;
            _color = _defaultColor;
        }

        public void Action()
        {
            PointF destination = _planning.Location;
            GoToLocation(destination);
        }
        private void GoToLocation(PointF destination)
        {
            // Move to location
            // Set Trajectory Once
        }

        /// <summary>
        /// Utilisé à l'initialisation.
        /// </summary>
        public void TeleportToLocation()
        {
            PointF destination = _planning.Location;
            _location = destination;
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPie(_color, new Rectangle(Point.Round(_location), _size), 0, 360);
        }
    }
}
