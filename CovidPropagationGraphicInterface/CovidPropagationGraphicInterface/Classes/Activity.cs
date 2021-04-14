using System.Drawing;

namespace CovidPropagationGraphicInterface.Classes
{
    interface Activity
    {
        PointF Location { get; }
        PointF Inside { get; }
    }
}
