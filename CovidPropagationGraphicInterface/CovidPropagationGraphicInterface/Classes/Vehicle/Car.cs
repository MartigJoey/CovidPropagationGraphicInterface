using CovidPropagationGraphicInterface.Classes;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Car : Vehicle
    {
        private bool _isDisplayed;

        public bool IsDisplayed { get => _isDisplayed; set => _isDisplayed = value; }

        public Car(PointF location) : base(location, GlobalVariables.car_Size, GlobalVariables.CAR_MAX_PERSON, GlobalVariables.car_Pen)
        {
            IsDisplayed = false;
        }
        public Car() : this(new PointF(0, 0))
        {
            // Does nothing
        }

        public override void SetDestination(PointF destination)
        {
            IsDisplayed = true;
            _destination = destination;
            CalculateMovementSpeed();
        }

        public override void GoToLocation()
        {
            _location.X += _movementX;
            _location.Y += _movementY;

            if (Point.Round(_location).Equals(Point.Round(_destination)))
                IsDisplayed = false;
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            if (IsDisplayed)
                e.Graphics.DrawRectangle(_color, new Rectangle(Point.Round(_location), _size));
        }
    }
}
