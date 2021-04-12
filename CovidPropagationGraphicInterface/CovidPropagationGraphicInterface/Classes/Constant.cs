using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPropagationGraphicInterface.Classes
{
    static class Constant
    {
        public const int TIME_OF_PERIOD = 30; // En minutes
        public const int MINUTES_PER_DAY = 1440;
        public const int NUMBER_OF_PERIODS = MINUTES_PER_DAY / TIME_OF_PERIOD;
        public const int NUMBER_OF_DAY = 7;

        public const int ANIMATION_FPS = 60;
        public const int TIMER_INTERVAL = 100;
        public const int ANIMATION_TIMER_INTERVAL =  15;
        public const int ANIMATION_PER_PERIOD = ANIMATION_FPS / (1000 / TIMER_INTERVAL);

        public const int BUS_MAX_PERSON = 18;
        public const int CAR_MAX_PERSON = 4;

        public readonly static Size PERSON_SIZE = new Size(6, 6);

        private readonly static int busSizeX = (PERSON_SIZE.Width + PERSON_SIZE.Width / 2) * 2;
        private readonly static int busSizeY = (PERSON_SIZE.Height + PERSON_SIZE.Height / 2) * 9;
        public readonly static Size BUS_SIZE = new Size(busSizeX, busSizeY); // 25 pour mettre 2 personnes côte à côte. 100 en hauteur pour en mettre 9

        private readonly static int carSizeX = (PERSON_SIZE.Width + PERSON_SIZE.Width / 2) * 2;
        private readonly static int carSizeY = (PERSON_SIZE.Height + PERSON_SIZE.Height / 2) * 3;
        public readonly static Size CAR_SIZE = new Size(carSizeX, carSizeY);

        // Buildings 
            // colors
        public readonly static Pen HOME_PEN_COLOR = Pens.Blue;
        public readonly static Pen SCHOOL_PEN_COLOR = Pens.Beige;
        public readonly static Pen HOSPITAL_PEN_COLOR = Pens.Red;
        public readonly static Pen COMPANY_PEN_COLOR = Pens.Yellow;
        public readonly static Pen SUPERMARKET_PEN_COLOR = Pens.Green;
        public readonly static Pen RESTAURANT_PEN_COLOR = Pens.LightGreen;

            // Size
        public readonly static Size DEFAULT_BUILDING_SIZE = new Size(50, 50);

        // Persons colors
        public readonly static Brush SANE_PERSON_BRUSH_COLOR = Brushes.Blue;
        public readonly static Brush IMMUNE_PERSON_BRUSH_COLOR = Brushes.Green;
        public readonly static Brush INFECTED_PERSON_BRUSH_COLOR = Brushes.Red;

        // Vehicles colors
        public readonly static Pen CAR_PEN_COLOR = Pens.White;
        public readonly static Pen BUS_PEN_COLOR = Pens.LightBlue;
    }
}
