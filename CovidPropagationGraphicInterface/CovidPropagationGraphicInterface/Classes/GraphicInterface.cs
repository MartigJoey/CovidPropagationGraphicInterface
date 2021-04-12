using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CovidPropagationGraphicInterface.Classes;
using CovidPropagationGraphicInterface.Classes.Vehicle;

namespace CovidPropagationGraphicInterface
{
    class GraphicInterface : Control
    {
        List<Person> persons;
        List<Building> buildings;
        List<Vehicle> vehicles;
        List<Bus> buses;

        Clock clock;
        int _currentAnimationPerPeriod; // Nombre de mouvements effectué durant une periode

        Bitmap bmp = null;
        Graphics g = null;
        Timer animationTimer;
        public GraphicInterface()
        {
            DoubleBuffered = true;
            clock = new Clock(new Point(750, 10));
            Paint += clock.Paint;
            animationTimer = new Timer();
            animationTimer.Tick += new EventHandler(AnimationOnTick);
            animationTimer.Interval = GlobalVariables.ANIMATION_TIMER_INTERVAL;
            _currentAnimationPerPeriod = 0;

        }

        internal bool HasStarted { get => persons != null; }

        public void Generate(List<Person> persons, List<Building> buildings, List<Vehicle> vehicles, List<Bus> bus)
        {
            this.persons = persons;
            this.buildings = buildings;
            this.vehicles = vehicles;
            buses = new List<Bus>();
            EnableOrDisableTrajectory();
            this.persons.ForEach(x => { Paint += x.Paint; Paint += x.Trajectory.Paint; x.TeleportToLocation(); } );
            this.buildings.ForEach(x => Paint += x.Paint);
            this.vehicles.ForEach(x => Paint += x.Paint);
            PositioningCity();
        }

        private void EnableOrDisableTrajectory()
        {
            if (persons.Count > GlobalVariables.MAX_PERSONS_TO_DISPLAY_TRAJECTORY)
                Trajectory.globalEnabled = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (bmp == null)
                bmp = new Bitmap(Size.Width, Size.Height);

            if (g == null)
                g = Graphics.FromImage(bmp);

            PaintEventArgs p = new PaintEventArgs(g, e.ClipRectangle);
            g.Clear(Color.DarkSlateGray);
            base.OnPaint(p);
            e.Graphics.DrawImage(bmp, new Point(0, 0));
        }

        public void OnTick(object sender, EventArgs e)
        {
            TimeManager.NextPeriod();
            persons.ForEach(x => x.ChangeActivity());
            buses.ForEach(x => x.NextStop());
            _currentAnimationPerPeriod = 0;
        }

        public void AnimationOnTick(object sender, EventArgs e)
        {
            if (HasStarted)
            {
                if (_currentAnimationPerPeriod < GlobalVariables.ANIMATION_PER_PERIOD)
                {
                    persons.ForEach(x => x.GoToLocation());
                    buses.ForEach(x => x.GoToLocation());
                    _currentAnimationPerPeriod++;
                }

                Invalidate(true);
            }
        }

        public void TimerStart()
        {
            animationTimer.Enabled = true;
        }

        public void TimerStop()
        {
            animationTimer.Enabled = false;
        }

        private void PositioningCity()
        {
            List<Building> topRow = new List<Building>();       int trInitialX = 80, trInitialY = 30, trSpace = 10;
                                                                                          
            List<Building> leftColumn = new List<Building>();   int lcInitialX = 30, lcInitialY = 80, lcSpace = 10;
            List<Building> rightColumn = new List<Building>();  int rcInitialX = 10, rcInitialY = 80, rcSpace = 10;

            List<Building> bottomRow = new List<Building>();    int brInitialX = 80, brInitialY = 20, brSpace = 10;

            topRow = (from building in buildings
                      where building.Type == BuildingType.Home
                      select building).ToList();

            leftColumn = (from building in buildings
                          where building.Type == BuildingType.Restaurant || building.Type == BuildingType.Supermarket
                          select building).ToList();

            rightColumn = (from building in buildings
                           where building.Type == BuildingType.Hospital || building.Type == BuildingType.School
                           select building).ToList();

            bottomRow = (from building in buildings
                         where building.Type == BuildingType.Company
                         select building).ToList();

            // Rows space
            if (topRow.Count > bottomRow.Count)
            {
                brSpace = CalculateSpace(topRow.Count, topRow[0].Size.Width, trSpace,
                               bottomRow.Count, bottomRow[0].Size.Width, brSpace);
            }
            else
            {
                trSpace = CalculateSpace(bottomRow.Count, bottomRow[0].Size.Width, brSpace,
                               topRow.Count, topRow[0].Size.Width, trSpace);
            }

            // Columns space
            if (leftColumn.Count > rightColumn.Count)
            {
                rcSpace = CalculateSpace(leftColumn.Count, leftColumn[0].Size.Height, lcSpace,
                               rightColumn.Count, rightColumn[0].Size.Height, rcSpace);
            }
            else
            {
                lcSpace = CalculateSpace(rightColumn.Count, rightColumn[0].Size.Height, rcSpace,
                               leftColumn.Count, leftColumn[0].Size.Height, lcSpace);
            }

            Building trPrev = null;
            trPrev = PositioningBuilding(trPrev, topRow, trInitialX, trInitialY, trSpace, true);

            Building lcPrev = null;
            lcPrev = PositioningBuilding(lcPrev, leftColumn, lcInitialX, lcInitialY, lcSpace, false);

            Building rcPrev = null;
            rcInitialX = (int)trPrev.Location.X + trPrev.Size.Width;
            rcPrev = PositioningBuilding(rcPrev, rightColumn, rcInitialX, rcInitialY, rcSpace, false);

            Building brPrev = null;
            brInitialY = (int)lcPrev.Location.Y + lcPrev.Size.Height;
            brPrev = PositioningBuilding(brPrev, bottomRow, brInitialX, brInitialY, brSpace, true);

            Point CenterZoneTopLeft =  new Point(
                leftColumn[0].Size.Width + (int)leftColumn[0].Location.X, 
                topRow[0].Size.Height + (int)topRow[0].Location.Y
                );

            Point CenterZoneBottomRight =  new Point((int)rightColumn[0].Location.X, (int)bottomRow[0].Location.Y);


            Point PerimeterZoneTopLeft = new Point((int)leftColumn[0].Location.X, (int)topRow[0].Location.Y);

            Point PerimeterZoneBottomRight = new Point(
                (int)rightColumn[0].Location.X + rightColumn[0].Size.Width, 
                (int)bottomRow[0].Location.Y + bottomRow[0].Size.Height
                );

            CreateBusLines(CenterZoneTopLeft, CenterZoneBottomRight, PerimeterZoneTopLeft, PerimeterZoneBottomRight);
        }

        private int CalculateSpace(int count1, int elementSize1, int space1, 
                                   int count2, int elementSize2, int space2)
        {
            int space;

            int size1 = count1 * elementSize1 + space1 * count1 - space1;
            int size2 = count2 * elementSize2 + space2 * count2 - space2;

            int sizeDifference = size1 - size2;
            space = space2 + sizeDifference / (count2 - 1);

            return space;
        }

        private Building PositioningBuilding(Building prev, List<Building> buildings, int initialX, int initialY, int space, bool isRow)
        {
            buildings.ForEach(b => {
                int x = initialX;
                int y = initialY;

                if (isRow)
                    x = prev == null ? initialX : (int)prev.Location.X + b.Size.Width + space;
                else
                    y = prev == null ? initialY : (int)prev.Location.Y + b.Size.Height + space;

                b.Location = new Point(x, y);
                prev = b;
            });
            return prev;
        }

        private void CreateBusLines(Point topLeft, Point bottomRight, Point perimtererTopLeft, Point perimtererBottomRight)
        {
            CreateHorizontalBusLine(topLeft.X, bottomRight.X, (topLeft.Y + bottomRight.Y) / 2);
            //CreateVerticalBusLine(topLeft.Y, bottomRight.Y, (topLeft.X + bottomRight.X) / 2);
            //CreatePerimeterBusLine(perimtererTopLeft, perimtererBottomRight);
        }

        private void CreateHorizontalBusLine(int xLeft, int xRight, int y) 
        {
            List<BusStop> busStops = new List<BusStop>();

            /*KeyValuePair<PointF, bool> stop = new KeyValuePair<PointF, bool>(new PointF(xLeft, y), false);
            List<KeyValuePair<PointF, bool>> stops = new List<KeyValuePair<PointF, bool>>();
            stops.Add(stop);
            busStops.Add(new BusStop(stops));

            KeyValuePair<PointF, bool> stop1 = new KeyValuePair<PointF, bool>(new PointF(xRight - GlobalVariables.bus_Size.Width, y), false);
            List<KeyValuePair<PointF, bool>> stops1 = new List<KeyValuePair<PointF, bool>>();
            stops1.Add(stop1);
            busStops.Add(new BusStop(stops1));

            KeyValuePair<PointF, bool> stop2 = new KeyValuePair<PointF, bool>(new PointF(xRight - GlobalVariables.bus_Size.Width, y + GlobalVariables.bus_Size.Width), false);
            List<KeyValuePair<PointF, bool>> stops2 = new List<KeyValuePair<PointF, bool>>();
            stops2.Add(stop2);
            busStops.Add(new BusStop(stops2));

            KeyValuePair<PointF, bool> stop3 = new KeyValuePair<PointF, bool>(new PointF(xLeft, y + GlobalVariables.bus_Size.Width), false);
            List<KeyValuePair<PointF, bool>> stops3 = new List<KeyValuePair<PointF, bool>>();
            stops3.Add(stop3);
            busStops.Add(new BusStop(stops3));*/

            List<KeyValuePair<PointF, bool>> stops = new List<KeyValuePair<PointF, bool>>();
            KeyValuePair<PointF, bool> stop = new KeyValuePair<PointF, bool>(new PointF(xLeft, y), false);
            KeyValuePair<PointF, bool> stop1 = new KeyValuePair<PointF, bool>(new PointF(xRight - GlobalVariables.bus_Size.Width, y), false);
            stops.Add(stop);
            stops.Add(stop1);
            busStops.Add(new BusStop(stops));

            List<KeyValuePair<PointF, bool>> stops1 = new List<KeyValuePair<PointF, bool>>();
            KeyValuePair<PointF, bool> stop2 = new KeyValuePair<PointF, bool>(new PointF(xRight - GlobalVariables.bus_Size.Width, y + GlobalVariables.bus_Size.Width), false);
            KeyValuePair<PointF, bool> stop3 = new KeyValuePair<PointF, bool>(new PointF(xLeft, y + GlobalVariables.bus_Size.Width), false);
            stops1.Add(stop2);
            stops1.Add(stop3);
            busStops.Add(new BusStop(stops1));

            BusLine busLine = new BusLine(busStops);

            List<Bus> horizontalBuses = new List<Bus>();

            horizontalBuses.Add(new Bus(busLine, true));
            //horizontalBuses.Add(new Bus(busLine, busLine., true));
            horizontalBuses.ForEach(b => Paint += b.Paint);
            buses.AddRange(horizontalBuses);
        }

        /*
        private void CreateVerticalBusLine(int yTop, int yBottom, int x)
        {
            List<PointF> trajectory = new List<PointF>();
            List<Bus> verticalBuses = new List<Bus>();

            trajectory.Add(new PointF(x, yTop));
            trajectory.Add(new PointF(x, yBottom - GlobalVariables.bus_Size.Width));
            trajectory.Add(new PointF(x + GlobalVariables.bus_Size.Width, yBottom - GlobalVariables.bus_Size.Width));
            trajectory.Add(new PointF(x + GlobalVariables.bus_Size.Width, yTop));

            verticalBuses.Add(new Bus(trajectory, false));
            verticalBuses.Add(new Bus(trajectory, trajectory[2], false));
            verticalBuses.ForEach(b => Paint += b.Paint);
            buses.AddRange(verticalBuses);
        }

        private void CreatePerimeterBusLine(Point perimtererTopLeft, Point perimtererBottomRight)
        {
            List<PointF> trajectory = new List<PointF>();
            List<Bus> perimeterBuses = new List<Bus>();

            trajectory.Add(new PointF(perimtererTopLeft.X - GlobalVariables.bus_Size.Width, perimtererTopLeft.Y - GlobalVariables.bus_Size.Width));
            trajectory.Add(new PointF(perimtererBottomRight.X, perimtererTopLeft.Y - GlobalVariables.bus_Size.Width));
            trajectory.Add(new PointF(perimtererBottomRight.X, perimtererBottomRight.Y));
            trajectory.Add(new PointF(perimtererTopLeft.X - GlobalVariables.bus_Size.Width, perimtererBottomRight.Y));

            perimeterBuses.Add(new Bus(trajectory, true));
            perimeterBuses.Add(new Bus(trajectory, trajectory[1], false));
            perimeterBuses.Add(new Bus(trajectory, trajectory[2], true));
            perimeterBuses.Add(new Bus(trajectory, trajectory[3], false));
            perimeterBuses.ForEach(b => Paint += b.Paint);
            buses.AddRange(perimeterBuses);
        }

        // méthode de modification de la taille des batiments, véhicules et individus en fonction du nombre d'éléments.
    */
    }
}
