using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface.Classes
{
    class Legend : Control
    {
        SolidBrush stringBrush;
        Font stringFont;
        List<KeyValuePair<string, Pen>> buildings;
        List<KeyValuePair<string, Pen>> vehicles;
        List<KeyValuePair<string, Brush>> persons;
        Size buildingsSize;
        Size personsSize;
        Size carSize;
        Size busSize;
        Point elementLocation;
        int _verticalSpace;
        int _verticalSpacePerson;
        int _horizontalSpace;

        Bitmap bmp = null;
        Graphics g = null;

        public Legend()
        {
            stringBrush = new SolidBrush(Color.Black);
            stringFont = new Font("Arial", 12);
            buildingsSize = GlobalVariables.default_building_size;
            personsSize = new Size(8, 8);
            carSize = GlobalVariables.car_Size;
            busSize = GlobalVariables.bus_Size;

            _verticalSpace = 10;
            _verticalSpacePerson = 20;
            _horizontalSpace = 15;

            buildings = new List<KeyValuePair<string, Pen>>();
            buildings.Add(new KeyValuePair<string, Pen>("Maison", GlobalVariables.home_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("École", GlobalVariables.school_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Hôpital", GlobalVariables.hospital_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Entreprise", GlobalVariables.company_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Supermarché", GlobalVariables.supermarket_Pen_Color));
            buildings.Add(new KeyValuePair<string, Pen>("Restaurant", GlobalVariables.restaurant_Pen_Color));

            vehicles = new List<KeyValuePair<string, Pen>>();
            vehicles.Add(new KeyValuePair<string, Pen>("Voiture", GlobalVariables.car_Pen));
            vehicles.Add(new KeyValuePair<string, Pen>("Bus", GlobalVariables.bus_Pen));

            persons = new List<KeyValuePair<string, Brush>>();
            persons.Add(new KeyValuePair<string, Brush>("Personne saine", GlobalVariables.healthy_Person_Brush));
            persons.Add(new KeyValuePair<string, Brush>("Personne asymptomatique", GlobalVariables.Asymptomatic_Person_Brush));
            persons.Add(new KeyValuePair<string, Brush>("Personne infectée", GlobalVariables.Infected_Person_Brush));

            elementLocation = new Point(10, 10);
            Paint += ToPaint;
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

        public void ToPaint(object sender, PaintEventArgs e)
        {
            buildings.ForEach(b => {
                e.Graphics.DrawRectangle(b.Value, new Rectangle(elementLocation, buildingsSize));
                e.Graphics.DrawString(b.Key, stringFont, stringBrush, new Point(elementLocation.X + buildingsSize.Width + _horizontalSpace, elementLocation.Y + buildingsSize.Height / 4));
                elementLocation = new Point(elementLocation.X, elementLocation.Y + buildingsSize.Height + _verticalSpace);
            });

            e.Graphics.DrawRectangle(vehicles[0].Value, new Rectangle(elementLocation, carSize));
            e.Graphics.DrawString(vehicles[0].Key, stringFont, stringBrush, new Point(elementLocation.X + carSize.Width + _horizontalSpace, elementLocation.Y));
            elementLocation = new Point(elementLocation.X, elementLocation.Y + carSize.Height + _verticalSpace);

            e.Graphics.DrawRectangle(vehicles[1].Value, new Rectangle(elementLocation, busSize));
            e.Graphics.DrawString(vehicles[1].Key, stringFont, stringBrush, new Point(elementLocation.X + busSize.Width + _horizontalSpace, elementLocation.Y + busSize.Height / 4));
            elementLocation = new Point(elementLocation.X, elementLocation.Y + busSize.Height + _verticalSpace);

            persons.ForEach(p => {
                e.Graphics.FillPie(p.Value, new Rectangle(elementLocation, personsSize), 0f, 360f);
                e.Graphics.DrawString(p.Key, stringFont, stringBrush, new Point(elementLocation.X + personsSize.Width + _horizontalSpace, elementLocation.Y));
                elementLocation = new Point(elementLocation.X, elementLocation.Y + personsSize.Height + _verticalSpacePerson);
            });
        }
    }
}
