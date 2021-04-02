namespace CovidPropagationGraphicInterface.Classes.Person
{
    class Day
    {
        private Period[] _periods = new Period[Constant.NUMBER_OF_PERIODS];

        public Period[] Periods { get => _periods; }

        public Day(Period[] periods)
        {
            _periods = periods;
        }

        public Activity GetActivity(int period)
        {
            return Periods[period].Activity;
        }
    }
}
