using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface.Classes
{
    class Legend
    {
        private Point _location;
        SolidBrush stringBrush;
        Font stringFont;
        List<KeyValuePair<string, Pen>> buildings;
        List<KeyValuePair<string, Pen>> vehicles;
        List<KeyValuePair<string, Brush>> persons;
        Size buildingsSize;
        Size personsSize;
        Size vehiclesSize;
        Point elementLocation;
        int verticalSpace = 10;
        int horizontalSpace = 15;

        public Legend(Point location)
        {
            _location = location;
            stringBrush = new SolidBrush(Color.Black);
            stringFont = new Font("Arial", 14);
            buildingsSize = Constant.DEFAULT_BUILDING_SIZE;
            personsSize = Constant.PERSON_SIZE;
            vehiclesSize = Constant.CAR_SIZE;
            elementLocation = _location;

            buildings = new List<KeyValuePair<string, Pen>>();
            buildings.Add(new KeyValuePair<string, Pen>("Home", Constant.HOME_PEN_COLOR));
            buildings.Add(new KeyValuePair<string, Pen>("School", Constant.SCHOOL_PEN_COLOR));
            buildings.Add(new KeyValuePair<string, Pen>("Hospital", Constant.HOSPITAL_PEN_COLOR));
            buildings.Add(new KeyValuePair<string, Pen>("Company", Constant.COMPANY_PEN_COLOR));
            buildings.Add(new KeyValuePair<string, Pen>("Supermarket", Constant.SUPERMARKET_PEN_COLOR));
            buildings.Add(new KeyValuePair<string, Pen>("Restaurant", Constant.RESTAURANT_PEN_COLOR));

            vehicles = new List<KeyValuePair<string, Pen>>();
            vehicles.Add(new KeyValuePair<string, Pen>("Car", Constant.CAR_PEN_COLOR));
            vehicles.Add(new KeyValuePair<string, Pen>("Bus", Constant.BUS_PEN_COLOR));

            persons = new List<KeyValuePair<string, Brush>>();
            persons.Add(new KeyValuePair<string, Brush>("Person", Constant.SANE_PERSON_BRUSH_COLOR));
            persons.Add(new KeyValuePair<string, Brush>("Person", Constant.IMMUNE_PERSON_BRUSH_COLOR));
            persons.Add(new KeyValuePair<string, Brush>("Person", Constant.INFECTED_PERSON_BRUSH_COLOR));
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            elementLocation = _location;
            buildings.ForEach(b => {
                e.Graphics.DrawRectangle(b.Value, new Rectangle(elementLocation, buildingsSize));
                e.Graphics.DrawString(b.Key, stringFont, stringBrush, new Point(elementLocation.X + buildingsSize.Width + horizontalSpace, elementLocation.Y));
                elementLocation = new Point(elementLocation.X, elementLocation.Y + buildingsSize.Height + verticalSpace);
            });

            vehicles.ForEach(v => {
                e.Graphics.DrawRectangle(v.Value, new Rectangle(elementLocation, vehiclesSize));
                e.Graphics.DrawString(v.Key, stringFont, stringBrush, new Point(elementLocation.X + vehiclesSize.Width + horizontalSpace, elementLocation.Y));
                elementLocation = new Point(elementLocation.X, elementLocation.Y + vehiclesSize.Height + verticalSpace);
            });

            persons.ForEach(p => {
                e.Graphics.FillPie(p.Value, new Rectangle(elementLocation, personsSize), 0f, 360f);
                e.Graphics.DrawString(p.Key, stringFont, stringBrush, new Point(elementLocation.X + personsSize.Width + horizontalSpace, elementLocation.Y));
                elementLocation = new Point(elementLocation.X, elementLocation.Y + personsSize.Height + verticalSpace);
            });
        }
    }
}
