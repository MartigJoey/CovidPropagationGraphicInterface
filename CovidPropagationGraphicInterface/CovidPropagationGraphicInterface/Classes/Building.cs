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
        private BuildingType _type;

        public PointF Location { get => _location; set => _location = Point.Round(value); }
        public PointF Inside { get => new Point(
                                        rdm.Next(_location.X + GlobalVariables.person_Size.Width, _location.X + _size.Width - GlobalVariables.person_Size.Width),
                                        rdm.Next(_location.Y + GlobalVariables.person_Size.Height, _location.Y + _size.Height - GlobalVariables.person_Size.Height)
                                    ); 
        }
        public Size Size { get => _size; }
        internal BuildingType Type { get => _type; set => _type = value; }

        public Building(Size size, BuildingType type)
        {
            _size = size;
            Type = type;

            switch (Type)
            {
                case BuildingType.Home:
                    _color = Pens.Blue;
                    break;
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
