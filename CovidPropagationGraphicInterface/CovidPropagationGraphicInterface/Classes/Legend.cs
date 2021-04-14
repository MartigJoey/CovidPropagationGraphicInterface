using System.Collections.Generic;
using System.Drawing;
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
            buildingsSize = GlobalVariables.default_building_size;
            personsSize = GlobalVariables.person_Size;
            vehiclesSize = GlobalVariables.car_Size;
            elementLocation = _location;

            buildings = new List<KeyValuePair<string, Pen>>();
            buildings.Add(new KeyValuePair<string, Pen>("Home", GlobalVariables.home_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("School", GlobalVariables.school_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Hospital", GlobalVariables.hospital_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Company", GlobalVariables.company_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Supermarket", GlobalVariables.supermarket_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Restaurant", GlobalVariables.restaurant_Pen_Color));

            vehicles = new List<KeyValuePair<string, Pen>>();
            vehicles.Add(new KeyValuePair<string, Pen>("Car", GlobalVariables.car_Pen));
            vehicles.Add(new KeyValuePair<string, Pen>("Bus", GlobalVariables.bus_Pen));

            persons = new List<KeyValuePair<string, Brush>>();
            persons.Add(new KeyValuePair<string, Brush>("Healthy person", GlobalVariables.healthy_Person_Brush));
            persons.Add(new KeyValuePair<string, Brush>("Asymptomatic Person", GlobalVariables.Asymptomatic_Person_Brush));
            persons.Add(new KeyValuePair<string, Brush>("Infected person", GlobalVariables.Infected_Person_Brush));
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
