using System;
using System.Collections.Generic;
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
        List<Bus> buses;

        int _currentAnimationPerPeriod; // Nombre de mouvements effectué durant une periode

        Bitmap bmp = null;
        Graphics g = null;
        Timer animationTimer;

        internal bool HasStarted { get => persons != null; }

        public GraphicInterface()
        {
            DoubleBuffered = true;
            animationTimer = new Timer();
            animationTimer.Tick += new EventHandler(AnimationOnTick);
            animationTimer.Interval = GlobalVariables.ANIMATION_TIMER_INTERVAL;
            _currentAnimationPerPeriod = 0;
        }

        public void Generate(List<Person> persons, List<Building> buildings, List<Vehicle> vehicles, List<Bus> buses)
        {
            this.persons = persons;
            this.buildings = buildings;
            this.vehicles = vehicles;
            this.buses = buses;
            EnableOrDisableTrajectory();
            this.persons.ForEach(x => { Paint += x.Paint; Paint += x.Trajectory.Paint; x.TeleportToLocation(); } );
            this.buildings.ForEach(x => Paint += x.Paint);
            this.vehicles.ForEach(x => Paint += x.Paint);
            this.buses.ForEach(x => Paint += x.Paint);
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
            g.Clear(GlobalVariables.background_Color);
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
    }
}
