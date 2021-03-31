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
        Clock clock;

        Bitmap bmp = null;
        Graphics g = null;
        public GraphicInterface()
        {
            DoubleBuffered = true;
            clock = new Clock(new Point(10,10));
            Paint += clock.Paint;
        }

        internal bool HasStarted { get => persons != null; }

        public void Generate(List<Person> persons, List<Building> buildings)
        {
            Console.WriteLine("Generating...");
            this.persons = persons;
            this.buildings = buildings;
            //persons.ForEach(x => { Paint += x.Paint; x.TeleportToLocation(); } );
            buildings.ForEach(x => Paint += x.Paint);
            Console.WriteLine("Generated");
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
            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        public void OnTick(object sender, EventArgs e)
        {
            if (HasStarted)
            {
                TimeManager.NextPeriod();
                persons.ForEach(x => x.Action());
                Invalidate(true);
            }
        }
    }
}
