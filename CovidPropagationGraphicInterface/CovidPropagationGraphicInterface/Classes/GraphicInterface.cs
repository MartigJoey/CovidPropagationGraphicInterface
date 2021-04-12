using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using CovidPropagationGraphicInterface.Classes;

namespace CovidPropagationGraphicInterface
{
    class GraphicInterface : Control
    {
        List<Person> persons;
        List<Building> buildings;
        List<Vehicle> vehicles;

        Clock clock;
        Legend legend;
        int _currentAnimationPerPeriod; // Nombre de mouvements effectué durant une periode

        Bitmap bmp = null;
        Graphics g = null;
        Timer animationTimer;
        public GraphicInterface()
        {
            DoubleBuffered = true;
            clock = new Clock(new Point(750, 10));
            legend = new Legend(new Point(750, 60));
            Paint += clock.Paint;
            Paint += legend.Paint;
            animationTimer = new Timer();
            animationTimer.Tick += new EventHandler(AnimationOnTick);
            animationTimer.Interval = Constant.ANIMATION_TIMER_INTERVAL;
            _currentAnimationPerPeriod = 0;
        }

        internal bool HasStarted { get => persons != null; }

        public void Generate(List<Person> persons, List<Building> buildings, List<Vehicle> vehicles)
        {
            this.persons = persons;
            this.buildings = buildings;
            this.vehicles = vehicles;
            this.persons.ForEach(x => { Paint += x.Paint; Paint += x.Trajectory.Paint; x.TeleportToLocation(); } );
            this.buildings.ForEach(x => Paint += x.Paint);
            this.vehicles.ForEach(x => Paint += x.Paint);
            PositioningBuildings();
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
            persons.ForEach(x => x.ChangeDestination());
            _currentAnimationPerPeriod = 0;
        }

        public void AnimationOnTick(object sender, EventArgs e)
        {
            if (HasStarted)
            {
                if (_currentAnimationPerPeriod < Constant.ANIMATION_PER_PERIOD)
                {
                    persons.ForEach(x => x.GoToLocation());
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

        private void PositioningBuildings()
        {
            List<Building> topRow = new List<Building>();       int trInitialX = 60, trInitialY = 10, trSpace = 10;
                                                                                          
            List<Building> leftColumn = new List<Building>();   int lcInitialX = 10, lcInitialY = 60, lcSpace = 10;
            List<Building> rightColumn = new List<Building>();  int rcInitialX = 00, rcInitialY = 60, rcSpace = 10;

            List<Building> bottomRow = new List<Building>();    int brInitialX = 60, brInitialY = 00, brSpace = 10;

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
            int trSumElementSize = topRow.Sum(b => b.Size.Width);
            int brSumElementSize = bottomRow.Sum(b => b.Size.Width);

            if (trSumElementSize > brSumElementSize)
                brSpace = CalculateSpace(topRow.Count, trSumElementSize, trSpace,
                                         bottomRow.Count, brSumElementSize, brSpace);
            else
                trSpace = CalculateSpace(bottomRow.Count, brSumElementSize, brSpace,
                                         topRow.Count, trSumElementSize, trSpace);

            // Columns space
            int lcSumElementSize = leftColumn.Sum(b => b.Size.Height);
            int rcSumElementSize = rightColumn.Sum(b => b.Size.Height);

            if (lcSumElementSize > rcSumElementSize)
                rcSpace = CalculateSpace(leftColumn.Count, lcSumElementSize, lcSpace,
                               rightColumn.Count, rcSumElementSize, rcSpace);
            else
                lcSpace = CalculateSpace(rightColumn.Count, rcSumElementSize, rcSpace,
                               leftColumn.Count, lcSumElementSize, lcSpace);


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
        }

        private int CalculateSpace(int count1, int elementSize1, int space1, 
                                   int count2, int elementSize2, int space2)
        {
            int space;

            int size1 = elementSize1 + space1 * count1 - space1;
            int size2 = elementSize2 + space2 * count2 - space2;

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
                    x = prev == null ? initialX : (int)prev.Location.X + prev.Size.Width + space;
                else
                    y = prev == null ? initialY : (int)prev.Location.Y + prev.Size.Height + space;

                b.Location = new Point(x, y);
                prev = b;
            });
            return prev;
        }

        // méthode de modification de la taille des batiments, véhicules et individus en fonction du nombre d'éléments.
    }
}
