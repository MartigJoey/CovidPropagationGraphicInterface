using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace CovidPropagationGraphicInterface
{
    class Building
    {
        private Point _location;
        private Size _size;
        private Pen _color;

        public Point Location { get => _location; }
        public Size Size { get => _size;}

        public Building(Point location, Size size, BuildingType type)
        {
            this._location = location;
            this._size = size;

            switch (type)
            {
                case BuildingType.School:
                    _color = Pens.Beige;
                    break;
                case BuildingType.Hospital:
                    _color = Pens.Red;
                    break;
                case BuildingType.Company:
                    _color = Pens.Yellow;
                    break;
                case BuildingType.Supermarket:
                    _color = Pens.Green;
                    break;
                case BuildingType.Restaurant:
                    _color = Pens.Gold;
                    break;
                default:
                    break;
            }
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_color, new Rectangle(_location, _size));
        }
    }
}
