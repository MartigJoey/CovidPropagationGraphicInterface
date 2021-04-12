using System.Collections.Generic;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusLine
    {
        private List<BusStop> _busStops;

        public List<BusStop> BusStops { get => new List<BusStop>(_busStops); }

        public BusLine(List<BusStop> busStops, int current = 0)
        {
            _busStops = busStops;
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
