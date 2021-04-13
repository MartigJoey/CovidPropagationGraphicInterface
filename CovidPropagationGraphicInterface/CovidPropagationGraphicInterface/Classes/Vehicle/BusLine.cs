using System.Collections.Generic;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusLine
    {
        private List<BusStop> _busStops;

        public List<BusStop> BusStops { get => new List<BusStop>(_busStops); }

        public BusLine(List<BusStop> busStops)
        {
            _busStops = busStops;
            for (int i = 0; i < _busStops.Count; i++)
            {
                BusStop current = _busStops[i];
                BusStop next;
                if(i == _busStops.CountFromZero())
                    next = _busStops[0];
                else
                    next = _busStops[i+1];
                current.AddNextBusStopFirstElement(next.GetCurrentPoint(0));
            }
        }

        public BusStop GetCurrentStop(int current)
        {
            return _busStops[current];
        }

        public int GetNextStopIndex(int current)
        {
            if (++current > _busStops.CountFromZero())
                current = 0;

            return current;
        }
    }
}
