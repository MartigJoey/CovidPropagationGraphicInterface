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
    class Bus : Vehicle
    {
        public Bus(Point location, Size size, int maxPerson, Pen color) : base(location, size, maxPerson, color)
        {
            // Do nothing
        }
        public Bus() : this(new Point(0, 0), Constant.CAR_SIZE, Constant.BUS_MAX_PERSON, Pens.Orange)
        {
            // Do nothing
        }

        public override void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_color, new Rectangle(_location, _size));
        }
    }
}
