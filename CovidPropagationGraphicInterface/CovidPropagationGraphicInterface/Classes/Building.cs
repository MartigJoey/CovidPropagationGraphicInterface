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
    class Building : Activity
    {
        private Random rdm = new Random();
        private Point _location;
        private Size _size;
        private Pen _color;

        public PointF Location { get => _location; }
        public PointF Inside { get => new Point(
                                        rdm.Next(_location.X + Constant.PERSON_SIZE.Width, _location.X + _size.Width - Constant.PERSON_SIZE.Width),
                                        rdm.Next(_location.Y + Constant.PERSON_SIZE.Height, _location.Y + _size.Height - Constant.PERSON_SIZE.Height)
                                    ); 
        }
        public Size Size { get => _size; }

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
