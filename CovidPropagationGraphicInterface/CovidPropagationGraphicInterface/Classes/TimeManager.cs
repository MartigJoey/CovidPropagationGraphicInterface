using System;

namespace CovidPropagationGraphicInterface.Classes
{
    static class TimeManager
    {
        static private int currentDay;
        static private int currentPeriod;
        static private string[] daysOfWeek = new string[] { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche"};

        public static int CurrentDay { get => currentDay; }
        public static string CurrentDayString { get => daysOfWeek[currentDay]; }
        public static int CurrentPeriod { get => currentPeriod; }
        public static string CurrentHour { get => GetTime(); }

        public static void Init()
        {
            currentDay = 0;
            currentPeriod = 0;
        }

        public static void NextPeriod()
        {
            if (currentPeriod < Constant.NUMBER_OF_PERIODS-1)
            {
                currentPeriod++;
            }
            else
            {
                currentPeriod = 0;
                if (currentDay < Constant.NUMBER_OF_DAY-1)
                {
                    currentDay++;
                }
                else
                {
                    currentDay = 0;
                }
            }
        }
        public static int[] GetNextPeriod()
        {
            int tempCurrentPeriod = currentPeriod;
            int tempCurrentDay = currentDay;
            if (tempCurrentPeriod < Constant.NUMBER_OF_PERIODS - 1)
            {
                tempCurrentPeriod++;
            }
            else
            {
                tempCurrentPeriod = 0;
                if (tempCurrentDay < Constant.NUMBER_OF_DAY - 1)
                {
                    tempCurrentDay++;
                }
                else
                {
                    tempCurrentDay = 0;
                }
            }

            return new int[] { tempCurrentDay, tempCurrentPeriod };
        }

        private static string GetTime()
        {
            float time = currentPeriod * Constant.TIME_OF_PERIOD / 60f;
            int hours = (int)Math.Truncate(time);
            int minutes = (int)((time - hours) * 60);
            return $"{hours.ToString("00")}:{minutes.ToString("00")}";
        }
    }
}
