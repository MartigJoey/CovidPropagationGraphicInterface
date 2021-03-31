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
        private PointF _departure;
        private PointF _oldDestination;
        private Size _size;
        private Brush _color;
        private Vehicle movementMethod;

        internal Trajectory Trajectory { get => _trajectory; }

        public Person(Planning planning, Vehicle vehicle)
        {
            this._planning = planning;
            movementMethod = vehicle;
            _color = Brushes.Blue;
            _size = Constant.PERSON_SIZE;
            _trajectory = new Trajectory();
            TeleportToLocation();
        }

        public void Action()
        {
            PointF destination = _planning.Location;
            GoToLocation(destination); // Utiliser le véhicule pour les déplacements hors batiments
        }
        private void GoToLocation(PointF destination)
        {
            if (!destination.Equals(_oldDestination))
            {
                _departure = _location;
                _oldDestination = destination;
                // Calcule vitesse
                _trajectory.SetTrajectory(_departure, destination);
            }

            // _location.X += vitesse.X;
            // _location.Y += vitesse.Y;

            /*
             * Quand il change de destination, calculer sa vitesse et afficher sa trajectoire UNE FOIS. X
             * Une fois la vitesse calculée, commencer le déplacement.
             * Incrémenter ou décrémenter sa position en x et en y en fonction de sa vitesse.
             */
        }

        private void CalculateSpeed()
        {

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
            e.Graphics.FillPie(_color, new Rectangle(Point.Round(_location), _size), 0f, 360f);
        }
    }
}
