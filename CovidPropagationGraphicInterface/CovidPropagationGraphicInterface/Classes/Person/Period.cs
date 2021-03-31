namespace CovidPropagationGraphicInterface.Classes.Person
{
    class Period
    {
        private Building _activity;

        public Building Activity { get => _activity; }

        public Period(Building activity)
        {
            _activity = activity;
        }
    }
}
