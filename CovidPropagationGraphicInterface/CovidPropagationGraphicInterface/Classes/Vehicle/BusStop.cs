using System.Collections.Generic;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusStop
    {
        private List<BusStopPoint> _points;
        public BusStop(List<BusStopPoint> point)
        {
            _points = point;
        }

        public void AddNextBusStopFirstElement(BusStopPoint point)
        {
            _points.Add(point.Clone());
        }

        public BusStopPoint GetCurrentPoint(int current)
        {
            return _points[current];
        }

        public BusStopPoint GetLastPoint()
        {
            return _points[_points.CountFromZero()];
        }

        public int GetNextPointIndex(int current)
        {
            if (++current > _points.CountFromZero())
                current = 0;

            return current;
        }

        public int CountNbStop()
        {
            return _points.Count;
        }

        public float GetThisPointDurationInPercent(int current)
        {
            return _points[current].durationInPercent;
        }
    }
}
