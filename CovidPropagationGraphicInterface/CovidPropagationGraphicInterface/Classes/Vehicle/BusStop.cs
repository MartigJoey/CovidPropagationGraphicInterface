using System;
using System.Collections.Generic;
using System.Drawing;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusStop
    {
        private List<KeyValuePair<PointF, bool>> _points;
        private float _totalLength;
        public BusStop(List<KeyValuePair<PointF, bool>> point)
        {
            _points = point;
        }

        public void AddNextBusStopFirstElement(PointF point)
        {
            _points.Add(new KeyValuePair<PointF, bool>(point, false));
            _totalLength = GetTotalLength();
        }

        public KeyValuePair<PointF, bool> GetCurrentPoint(int current)
        {

            return _points[current];
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

        /// Utiliser un dictionnaire pour optimiser
        public float GetThisPointLengthInPercent(int current)
        {
            int next = GetNextPointIndex(current);
            float thisPointLength = (float)Math.Sqrt(Math.Pow(_points[next].Key.X - _points[current].Key.X, 2) + Math.Pow(_points[next].Key.Y - _points[current].Key.Y, 2));
            return 100 - thisPointLength / _totalLength * 100;
        }

        private float GetTotalLength()
        {
            float totalLength = 0;
            PointF prev = PointF.Empty;
            foreach (var point in _points)
            {
                if (prev == PointF.Empty)
                {
                    prev = point.Key;
                    continue;
                }
                totalLength += (float)Math.Sqrt(Math.Pow(point.Key.X - prev.X, 2) + Math.Pow(point.Key.Y - prev.Y, 2));

                prev = point.Key;
            }
            return totalLength;
        }
    }
}
