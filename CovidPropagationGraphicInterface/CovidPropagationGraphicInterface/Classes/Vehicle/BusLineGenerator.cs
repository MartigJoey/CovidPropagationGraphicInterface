using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CovidPropagationGraphicInterface.Classes.Vehicle
{
    class BusLineGenerator
    {
        private List<Bus> _buses;
        private List<Bus> _horizontalBuses;
        private List<Bus> _verticalBuses;
        private List<Bus> _perimeterBusesIn;
        private List<Bus> _perimeterBusesOut;

        internal List<Bus> Buses { get => _buses; }

        public BusLineGenerator(Point CenterZoneTopLeft, Point CenterZoneBottomRight, Point PerimeterZoneTopLeft, Point PerimeterZoneBottomRight)
        {
            _buses = new List<Bus>();
            _horizontalBuses = new List<Bus>();
            _verticalBuses = new List<Bus>();
            _perimeterBusesIn = new List<Bus>();
            _perimeterBusesOut = new List<Bus>();
            CreateBusLines(CenterZoneTopLeft, CenterZoneBottomRight, PerimeterZoneTopLeft, PerimeterZoneBottomRight);
        }

        public Bus GetBusToGoLeft()
        {
            return _horizontalBuses.OrderBy(b => b.Location.X).Last();
        }
        public Bus GetBusToGoRight()
        {
            return _horizontalBuses.OrderBy(b => b.Location.X).First();
        }
        public Bus GetBusToGoDown()
        {
            return _horizontalBuses.OrderBy(b => b.Location.Y).First();
        }
        public Bus GetBusToGoUp()
        {
            return _horizontalBuses.OrderBy(b => b.Location.Y).Last();
        }

        private void CreateBusLines(Point topLeft, Point bottomRight, Point perimtererTopLeft, Point perimtererBottomRight)
        {
            int centerY = (topLeft.Y + bottomRight.Y) / 2;
            PointF horizontalTopLeft = new PointF(topLeft.X, centerY);
            PointF horizontalBottomLeft = new PointF(bottomRight.X - GlobalVariables.bus_Size.Height, centerY);
            PointF horizontalBottomRight = new PointF(bottomRight.X - GlobalVariables.bus_Size.Height, centerY + GlobalVariables.bus_Size.Width);
            PointF horizontalTopRight = new PointF(topLeft.X, centerY + GlobalVariables.bus_Size.Width);

            _horizontalBuses = CreateBusLine(horizontalTopLeft, horizontalBottomLeft, horizontalBottomRight, horizontalTopRight, false);

            int centerX = (topLeft.X + bottomRight.X) / 2;
            PointF verticalTopLeft = new PointF(centerX, topLeft.Y);
            PointF verticalBottomLeft = new PointF(centerX, bottomRight.Y - GlobalVariables.bus_Size.Height);
            PointF verticalBottomRight = new PointF(centerX + GlobalVariables.bus_Size.Width, bottomRight.Y - GlobalVariables.bus_Size.Height);
            PointF verticalTopRight = new PointF(centerX + GlobalVariables.bus_Size.Width, topLeft.Y);

           _verticalBuses = CreateBusLine(verticalTopLeft, verticalBottomLeft, verticalBottomRight, verticalTopRight, true);

            //CreatePerimeterBusLine(perimtererTopLeft, perimtererBottomRight, centerX, centerY, true);
            _perimeterBusesIn = CreatePerimeterBusLine(perimtererTopLeft.Clone(), perimtererBottomRight.Clone(), centerX, centerY, true);
            Size busWidth = new Size(GlobalVariables.bus_Size.Width, GlobalVariables.bus_Size.Width);
            _perimeterBusesOut = CreatePerimeterBusLine(Point.Subtract(perimtererTopLeft, busWidth), Point.Add(perimtererBottomRight, busWidth), centerX, centerY, false);

            Buses.AddRange(_horizontalBuses);
            Buses.AddRange(_verticalBuses);
            Buses.AddRange(_perimeterBusesIn);
            Buses.AddRange(_perimeterBusesOut);
        }

        private List<Bus> CreateBusLine(PointF topLeft, PointF bottomLeft, PointF bottomRight, PointF topRight, bool isVertical)
        {
            List<BusStop> busStops = new List<BusStop>();

            List<BusStopPoint> stopsLeft = CreateStops(topRight, topLeft, isVertical);
            List<BusStopPoint> stopsRight = CreateStops(bottomLeft, bottomRight, isVertical);

            busStops.Add(new BusStop(stopsLeft));
            busStops.Add(new BusStop(stopsRight));

            BusLine busLine = new BusLine(busStops);
            List<Bus> centerBuses = new List<Bus>();

            centerBuses.Add(new Bus(busLine, 0));
            centerBuses.Add(new Bus(busLine, 1));
            return centerBuses;
        }

        private List<BusStopPoint> CreateStops(PointF firstStopPosition, PointF secondStopPosition, bool isVertical)
        {
            List<BusStopPoint> stops = new List<BusStopPoint>();
            BusStopPoint firstStop = new BusStopPoint(firstStopPosition, isVertical, 75);
            BusStopPoint secondStop = new BusStopPoint(secondStopPosition, isVertical, 25);
            stops.Add(firstStop);
            stops.Add(secondStop);
            return stops;
        }

        private List<Bus> CreatePerimeterBusLine(Point perimeterTopLeft, Point perimeterBottomRight, int centerX, int centerY, bool isPerimeterIn)
        {
            List<BusStop> busStops = new List<BusStop>();

            List<BusStopPoint> stopsTopRight = new List<BusStopPoint>();
            BusStopPoint stopTopCenter = new BusStopPoint(new PointF(centerX, perimeterTopLeft.Y - GlobalVariables.bus_Size.Width - 1), false, 20);
            BusStopPoint stopTopRight = new BusStopPoint(new PointF(perimeterBottomRight.X, perimeterTopLeft.Y - GlobalVariables.bus_Size.Width - 1), isPerimeterIn ? false : true, 80);


            List<BusStopPoint> stopsBottomRight = new List<BusStopPoint>();
            BusStopPoint stopMiddleRight = new BusStopPoint(new PointF(perimeterBottomRight.X, centerY), true, 20);
            BusStopPoint stopBottomRight = new BusStopPoint(new PointF(perimeterBottomRight.X, perimeterBottomRight.Y), isPerimeterIn ? true : false, 80);


            List<BusStopPoint> stopsBottomLeft = new List<BusStopPoint>();
            BusStopPoint stopBottomCenter = new BusStopPoint(new PointF(centerX, perimeterBottomRight.Y), false, 20);
            BusStopPoint stopBottomLeft = new BusStopPoint(new PointF(perimeterTopLeft.X - GlobalVariables.bus_Size.Width - 1, perimeterBottomRight.Y), isPerimeterIn ? false : true, 80);


            List<BusStopPoint> stopsTopLeft = new List<BusStopPoint>();
            BusStopPoint stopLeftCenter = new BusStopPoint(new PointF(perimeterTopLeft.X - GlobalVariables.bus_Size.Width - 1, centerY), true, 20);
            BusStopPoint stopTopLeft = new BusStopPoint(new PointF(perimeterTopLeft.X - GlobalVariables.bus_Size.Width - 1, perimeterTopLeft.Y - GlobalVariables.bus_Size.Width - 1), isPerimeterIn ? true : false, 80);


            stopsTopRight.Add(stopTopCenter);
            stopsTopRight.Add(stopTopRight);

            stopsBottomRight.Add(stopMiddleRight);
            stopsBottomRight.Add(stopBottomRight);

            stopsBottomLeft.Add(stopBottomCenter);
            stopsBottomLeft.Add(stopBottomLeft);

            stopsTopLeft.Add(stopLeftCenter);
            stopsTopLeft.Add(stopTopLeft);

            busStops.Add(new BusStop(stopsTopRight));
            busStops.Add(new BusStop(stopsBottomRight));
            busStops.Add(new BusStop(stopsBottomLeft));
            busStops.Add(new BusStop(stopsTopLeft));

            if (!isPerimeterIn)
            {
                stopsTopRight.Reverse();
                stopsBottomRight.Reverse();
                stopsBottomLeft.Reverse();
                stopsTopLeft.Reverse();
                busStops.Reverse();
            }

            BusLine busLine = new BusLine(busStops);
            List<Bus> perimeterBuses = new List<Bus>();
            perimeterBuses.Add(new Bus(busLine, 0));
            perimeterBuses.Add(new Bus(busLine, 1));
            perimeterBuses.Add(new Bus(busLine, 2));
            perimeterBuses.Add(new Bus(busLine, 3));
            return perimeterBuses;
        }
    }
}
