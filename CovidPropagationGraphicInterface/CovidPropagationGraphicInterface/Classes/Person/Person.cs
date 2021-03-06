using System;
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
        private PointF _destination;
        private Size _size;
        private Brush _color;
        private float _movementX;
        private float _movementY;
        private State _state;
        Car car;// Variable temporaire pour régler un bug

        internal Trajectory Trajectory { get => _trajectory; }

        public Person(Planning planning, State state)
        {
            _planning = planning;
            _state = state;
            _color = GlobalVariables.healthy_Person_Brush;
            _size = GlobalVariables.person_Size;
            _trajectory = new Trajectory();
            car = new Car();
            TeleportToLocation();
        }

        public void ChangeActivity()
        {
            _destination = _planning.Location;
            switch (_planning.GetActivity())
            {
                case Car car:
                    this.car = car;
                    PointF carDestination = _planning.NextLocation;
                    car.TeleportToLocation(_location);
                    car.SetDestination(carDestination);
                    TeleportToLocation(car.Inside); // Remplacer par un déplacement
                    _trajectory.Enabled = true;
                    _trajectory.SetTrajectory(_location, carDestination);
                    break;
                case Bus bus:
                    TeleportToLocation(bus.Inside);// Remplacer par un déplacement
                    _trajectory.Enabled = false;
                    break;
                default:
                    car.IsDisplayed = false;
                    CalculateMovementSpeed();
                    _trajectory.Enabled = false;
                    break;
            }
        }

        public void GoToLocation()
        {
            switch (_planning.GetActivity())
            {
                case Car car:
                    car.GoToLocation();
                    _location = car.Inside;
                    break;
                case Bus bus:
                    TeleportToLocation(bus.Inside);
                    break;
                default:
                    _location.X += _movementX;
                    _location.Y += _movementY;
                    break;
            }
        }

        private void CalculateMovementSpeed()
        {
            float distanceX, distanceY;

            distanceX = (float)Math.Sqrt(Math.Pow(_destination.X - _location.X, 2));
            distanceY = (float)Math.Sqrt(Math.Pow(_destination.Y - _location.Y, 2));

            distanceX = _destination.X < _location.X ? distanceX * -1 : distanceX;
            distanceY = _destination.Y < _location.Y ? distanceY * -1 : distanceY;

            _movementX = distanceX / GlobalVariables.ANIMATION_PER_PERIOD;
            _movementY = distanceY / GlobalVariables.ANIMATION_PER_PERIOD;
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
            switch (_state.CurrentState)
            {
                default:
                case PersonState.Healthy:
                    _color = GlobalVariables.healthy_Person_Brush;
                    break;
                case PersonState.Infected:
                    _color = GlobalVariables.Infected_Person_Brush;
                    break;
                case PersonState.Asymptomatic:
                    _color = GlobalVariables.Asymptomatic_Person_Brush;
                    break;
            }

            e.Graphics.FillPie(_color, new Rectangle(Point.Round(_location), _size), 0f, 360f);
        }
    }
}
