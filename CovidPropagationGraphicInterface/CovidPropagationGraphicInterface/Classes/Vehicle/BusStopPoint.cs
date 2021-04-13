using System.Drawing;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusStopPoint
    {
        internal PointF location;
        internal bool isVertical;
        internal int durationInPercent;

        public BusStopPoint(PointF location, bool isVertical, int durationInPercent)
        {
            this.location = location;
            this.isVertical = isVertical;
            this.durationInPercent = durationInPercent;
        }

        public BusStopPoint Clone()
        {
            return new BusStopPoint(location.Clone(), isVertical, durationInPercent);
        }
    }
}
