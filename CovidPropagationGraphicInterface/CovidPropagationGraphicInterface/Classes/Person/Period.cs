namespace CovidPropagationGraphicInterface.Classes.Person
{
    class Period
    {
        private Activity _activity;

        public Activity Activity { get => _activity; }

        public Period(Activity activity)
        {
            _activity = activity;
        }
    }
}
