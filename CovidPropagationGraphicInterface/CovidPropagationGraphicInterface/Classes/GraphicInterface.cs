using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CovidPropagationGraphicInterface
{
    class GraphicInterface : Control
    {
        List<Person> persons;
        List<Building> buildings;

        Bitmap bmp = null;
        Graphics g = null;
        public GraphicInterface()
        {
            DoubleBuffered = true;
        }

        internal bool HasStarted { get => persons != null; }

        public void Generate(List<Person> persons, List<Building> buildings)
        {
            this.persons = persons;
            this.buildings = buildings;
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
            if (HasStarted)
            {
                persons.ForEach(x => x.Action());
                buildings.ForEach(x => x.Action());
            }
        }
    }
}
