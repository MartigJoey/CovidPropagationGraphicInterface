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
            // Does nothing
        }
        public Bus() : this(new Point(0, 0), Constant.CAR_SIZE, Constant.BUS_MAX_PERSON, Pens.Orange)
        {
            // Does nothing
        }
    }
}
