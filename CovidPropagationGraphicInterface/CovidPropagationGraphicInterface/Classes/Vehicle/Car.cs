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
    class Car : Vehicle
    {
        public Car(Point location, Size size, int maxPerson, Pen color) : base(location, size, maxPerson, color)
        {
            // Do nothing
        }
        public Car() : this(new Point(0, 0), Constant.CAR_SIZE, Constant.CAR_MAX_PERSON, Pens.White)
        {
            // Do nothing
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_color, new Rectangle(_location, _size));
        }
    }
}
