using CovidPropagationGraphicInterface.Classes;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    class Clock
    {
        private Point _location;
        SolidBrush drawBrush;
        Font drawFont;
        public Clock(Point location)
        {
            _location = location;
            drawBrush = new SolidBrush(Color.Black);
            drawFont = new Font("Arial", 15);
        }

        public void Paint(object sender, PaintEventArgs e)
        {
            string drawString = $"Jours : {TimeManager.CurrentDayString} {System.Environment.NewLine}" +
                                $"Heure : {TimeManager.CurrentHour}";
            
            e.Graphics.DrawString(drawString, drawFont, drawBrush, _location);
        }
    }
}
