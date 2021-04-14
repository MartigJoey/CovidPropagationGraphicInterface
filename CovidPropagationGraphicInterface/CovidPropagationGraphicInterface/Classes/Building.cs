using System;
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
        public Size Size { get => _size; set => _size = value; }
        internal BuildingType Type { get => _type; set => _type = value; }

        public Building(Size size, BuildingType type)
        {
            _size = size;
            Type = type;

            switch (Type)
            {
                default:
                case BuildingType.Home:
                    _color = GlobalVariables.home_Pen_Color;
                    break;
                case BuildingType.School:
                    _color = GlobalVariables.school_Pen_Color;
                    break;
                case BuildingType.Hospital:
                    _color = GlobalVariables.hospital_Pen_Color;
                    break;
                case BuildingType.Company:
                    _color = GlobalVariables.company_Pen_Color;
                    break;
                case BuildingType.Supermarket:
                    _color = GlobalVariables.supermarket_Pen_Color;
                    break;
                case BuildingType.Restaurant:
                    _color = GlobalVariables.restaurant_Pen_Color;
                    break;
            }
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(_color, new Rectangle(_location, _size));
        }
    }
}
