using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPropagationGraphicInterface.Classes
{
    static class GlobalVariables
    {
        public const int TIME_OF_PERIOD = 30; // En minutes
        public const int MINUTES_PER_DAY = 1440;
        public const int NUMBER_OF_PERIODS = MINUTES_PER_DAY / TIME_OF_PERIOD;
        public const int NUMBER_OF_DAY = 7;

        public const int ANIMATION_FPS = 60;
        public const int TIMER_INTERVAL = 1000; // 50 minimum
        public const int ANIMATION_TIMER_INTERVAL =  15;
        public const int ANIMATION_PER_PERIOD = (int)(ANIMATION_FPS / (1000f / TIMER_INTERVAL));

        public const int BUS_MAX_PERSON = 18;
        public const int CAR_MAX_PERSON = 4;

        public const int MAX_PERSONS_TO_DISPLAY_TRAJECTORY = 30;

        // Buildings 
            // Colors
        public readonly static Pen home_Pen_Color = Pens.Blue;
        public readonly static Pen school_Pen_Color = Pens.Beige;
        public readonly static Pen hospital_Pen_Color = Pens.Red;
        public readonly static Pen company_Pen_Color = Pens.Yellow;
        public readonly static Pen supermarket_Pen_Color = Pens.Green;
        public readonly static Pen restaurant_Pen_Color = Pens.LightGreen;

        // Size
        public readonly static Size DEFAULT_BUILDING_SIZE = new Size(50, 50);

        // Persons 
            // Colors
        public static Brush healthy_Person_Brush = Brushes.Blue;
        public static Brush Infected_Person_Brush = Brushes.Red;
        public static Brush Asymptomatic_Person_Brush = Brushes.Green;

            // Size
        public static Size person_Size = new Size(6, 6);

        // Vehicles 
            // Colors
        public static Pen bus_Pen = Pens.LightBlue;
        public static Pen car_Pen = Pens.White;

            // Size
        private static int busSizeX = (person_Size.Width + person_Size.Width / 2) * 2;
        private static int busSizeY = (person_Size.Height + person_Size.Height / 2) * 9;
        public static Size bus_Size = new Size(busSizeX, busSizeY); // 25 pour mettre 2 personnes côte à côte. 100 en hauteur pour en mettre 9

        private static int carSizeX = (person_Size.Width + person_Size.Width / 2) * 2;
        private static int carSizeY = (person_Size.Height + person_Size.Height / 2) * 3;
        public static Size car_Size = new Size(carSizeX, carSizeY);
    }
}
