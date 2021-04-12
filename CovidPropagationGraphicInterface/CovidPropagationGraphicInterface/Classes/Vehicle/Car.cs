using CovidPropagationGraphicInterface.Classes;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Car : Vehicle
    {
        private bool _isDisplayed;
        public Car(PointF location, Size size, int maxPerson) : base(location, size, maxPerson, GlobalVariables.car_Pen)
        {
            _isDisplayed = false;
        }
        public Car() : this(new PointF(0, 0), GlobalVariables.car_Size, GlobalVariables.CAR_MAX_PERSON)
        {
            // Does nothing
        }

        public override void SetDestination(PointF destination)
        {
            _isDisplayed = true;
            _destination = destination;
            CalculateMovementSpeed();
        }

        public override void GoToLocation()
        {
            _location.X += _movementX;
            _location.Y += _movementY;

            if (Point.Round(_location).Equals(Point.Round(_destination)))
            {
                _isDisplayed = false;
            }
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            if (_isDisplayed)
                e.Graphics.DrawRectangle(_color, new Rectangle(Point.Round(_location), _size));
        }
    }
}
