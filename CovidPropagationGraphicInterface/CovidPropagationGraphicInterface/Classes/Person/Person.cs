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
        private PointF _destination;
        private Size _size;
        private Brush _color;
        private float _movementX;
        private float _movementY;

        internal Trajectory Trajectory { get => _trajectory; }

        public Person(Planning planning, Vehicle vehicle)
        {
            this._planning = planning;
            _color = Brushes.Blue;
            _size = Constant.PERSON_SIZE;
            _trajectory = new Trajectory();
            TeleportToLocation();
        }

        public void ChangeDestination()
        {
            _destination = _planning.Location;
            switch (_planning.GetActivity())
            {
                case Car car:
                    //PointF carDestination = _planning.NextLocation;
                    //car.GoToLocation(_location);
                    //car.GoToLocation(carDestination);
                    // TeleportToLocation();// Remplacer par un déplacement rapide
                    CalculateMovementSpeed();
                    break;
                case Bus bus:
                    break;
                default:
                    CalculateMovementSpeed();
                    break;
            }
            //GoToLocation(destination); // Utiliser le véhicule pour les déplacements hors batiments
            //TeleportToLocation();
        }
        public void GoToLocation()
        {
            /*if (!_destination.Equals(_oldDestination))
            {
                _departure = _location;
                _oldDestination = _destination;
                // Calcule vitesse
                _trajectory.SetTrajectory(_departure, _destination);
            }*/
            _location.X += _movementX;
            _location.Y += _movementY;

            // _location.X += vitesse.X;
            // _location.Y += vitesse.Y;

            /*
             * Quand il change de destination, calculer sa vitesse et afficher sa trajectoire UNE FOIS. X
             * Une fois la vitesse calculée, commencer le déplacement.
             * Incrémenter ou décrémenter sa position en x et en y en fonction de sa vitesse.
             */
        }

        private void CalculateMovementSpeed()
        {
            float distanceX, distanceY;

            distanceX = (float)Math.Sqrt(Math.Pow(_destination.X - _location.X, 2));
            distanceY = (float)Math.Sqrt(Math.Pow(_destination.Y - _location.Y, 2));

            distanceX = _destination.X < _location.X ? distanceX * -1 : distanceX;
            distanceY = _destination.Y < _location.Y ? distanceY * -1 : distanceY;

            _movementX = distanceX / Constant.ANIMATION_FPS;
            _movementY = distanceY / Constant.ANIMATION_FPS;
        }

        public void TeleportToLocation(PointF destination)
        {
            _location = destination;
        }

        /// <summary>
        /// Utilisé à l'initialisation.
        /// </summary>
        public void TeleportToLocation()
        {
            _location = _planning.Location;
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillPie(_color, new Rectangle(Point.Round(_location), _size), 0f, 360f);
        }
    }
}
