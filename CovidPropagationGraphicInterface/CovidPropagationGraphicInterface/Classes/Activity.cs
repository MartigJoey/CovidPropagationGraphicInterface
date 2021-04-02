using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPropagationGraphicInterface.Classes
{
    interface Activity
    {
        PointF Location { get; }
        PointF Inside { get; }
    }
}
