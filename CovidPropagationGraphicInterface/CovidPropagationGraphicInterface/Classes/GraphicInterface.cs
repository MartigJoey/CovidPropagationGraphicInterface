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

        Bitmap bmp = null;
        Graphics g = null;
        Timer animationTimer;
        public GraphicInterface()
        {
            DoubleBuffered = true;
            clock = new Clock(new Point(10,10));
            Paint += clock.Paint;
            animationTimer = new Timer();
            animationTimer.Tick += new EventHandler(AnimationOnTick);
            animationTimer.Interval = Constant.ANIMATION_TIMER_INTERVAL;
        }

        internal bool HasStarted { get => persons != null; }

        public void Generate(List<Person> persons, List<Building> buildings, List<Vehicle> vehicles)
        {
            this.persons = persons;
            this.buildings = buildings;
            this.vehicles = vehicles;
            persons.ForEach(x => { Paint += x.Paint; Paint += x.Trajectory.Paint ; x.TeleportToLocation(); } );
            buildings.ForEach(x => Paint += x.Paint);
            vehicles.ForEach(x => Paint += x.Paint);
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
        }

        public void AnimationOnTick(object sender, EventArgs e)
        {
            if (HasStarted)
            {
                persons.ForEach(x => x.GoToLocation());
                //vehicles.ForEach(x => x.TeleportToLocation());
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

        // méthode de positionnement des batiments

        // méthode de modification de la taille des batiments, véhicules et individus en fonction du nombre d'éléments.
    }
}
