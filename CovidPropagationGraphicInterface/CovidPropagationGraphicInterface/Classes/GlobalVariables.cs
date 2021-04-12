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
        public const int TIMER_INTERVAL = 1000;
        public const int ANIMATION_TIMER_INTERVAL =  15;
        public const int ANIMATION_PER_PERIOD = (int)(ANIMATION_FPS / (1000f / TIMER_INTERVAL));

        public const int BUS_MAX_PERSON = 18;
        public const int CAR_MAX_PERSON = 4;

        public const int MAX_PERSONS_TO_DISPLAY_TRAJECTORY = 30;

        public static Size person_Size = new Size(6, 6);

        private static int busSizeX = (person_Size.Width + person_Size.Width / 2) * 2;
        private static int busSizeY = (person_Size.Height + person_Size.Height / 2) * 9;
        public static Size bus_Size = new Size(busSizeX, busSizeY); // 25 pour mettre 2 personnes côte à côte. 100 en hauteur pour en mettre 9

        private static int carSizeX = (person_Size.Width + person_Size.Width / 2) * 2;
        private static int carSizeY = (person_Size.Height + person_Size.Height / 2) * 3;
        public static Size car_Size = new Size(carSizeX, carSizeY);

        public static Brush healthy_Person_Brush = Brushes.Blue;
        public static Brush Infected_Person_Brush = Brushes.Red;
        public static Brush Asymptomatic_Person_Brush = Brushes.Green;

        public static Pen bus_Pen = Pens.LightBlue;
        public static Pen car_Pen = Pens.White;

    }
}
