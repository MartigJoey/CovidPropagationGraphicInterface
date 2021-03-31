using System;
using System.Collections.Generic;
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
    }
}
