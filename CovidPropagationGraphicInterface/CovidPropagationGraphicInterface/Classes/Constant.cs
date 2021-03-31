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
        public const int NUMBER_OF_PERIODS = MINUTES_PER_DAY / TIME_OF_PERIOD; // 1440 = 1 jour en minutes
        public const int NUMBER_OF_DAY = 7;

        public const int ANIMATION_TIMER_INTERVAL = 15; // ~60 image par seconde

        public const int BUS_MAX_PERSON = 18;
        public const int CAR_MAX_PERSON = 4;

        public static Size PERSON_SIZE = new Size(6, 6);
        private static int busSizeX = (PERSON_SIZE.Width + PERSON_SIZE.Width / 2) * 2;
        private static int busSizeY = (PERSON_SIZE.Width + PERSON_SIZE.Width / 2) * 9;
        public static Size BUS_SIZE = new Size(busSizeX, busSizeY); // 25 pour mettre 2 personnes côte à côte. 100 en hauteur pour en mettre 9
        private static int carSizeX = (PERSON_SIZE.Width + PERSON_SIZE.Width / 2) * 2;
        private static int carSizeY = (PERSON_SIZE.Width + PERSON_SIZE.Width / 2) * 3;
        public static Size CAR_SIZE = new Size(carSizeX, carSizeY);
    }
}
