using System.Collections.Generic;
using System.Drawing;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusStop
    {
        private List<KeyValuePair<PointF, bool>> _point;

        public BusStop(List<KeyValuePair<PointF, bool>> point)
        {
            _point = point;
        }

        public KeyValuePair<PointF, bool> GetCurrentPoint(int current)
        {
            return _point[current];
        }

        public int GetNextPointIndex(int current)
        {
            if (++current > _point.CountFromZero())
                current = 0;

            return current;
        }

        public int CountNbStop()
        {
            return _point.Count;
        }

    }
}
