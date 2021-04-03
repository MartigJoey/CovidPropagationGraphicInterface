using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidPropagationGraphicInterface.Classes.Person
{
    /// <summary>
    /// ⚠️ Classe commune à la simulation et à l'interface graphique. ⚠️
    /// Elle peut être vouée à changer plus tard (Travail de diplôme). 
    /// Elle est crée de façon à ce que l'intialisation ne rentre pas en conflit avec 
    /// le contenu qui de doit pas changer pour le bon fonctionnement de la partie graphique
    /// </summary>
    class Planning
    {
        private Day[] _days = new Day[Constant.NUMBER_OF_DAY];

        public Day[] Days { get => _days; }
        public PointF Location { get => GetActivity(TimeManager.CurrentDay, TimeManager.CurrentPeriod).Inside; }
        public PointF NextLocation { get => GetNextActivity().Inside; }

        public Planning(Day[] days)
        {
            _days = days;
        }

        public Activity GetActivity(int dayOfWeek, int periodOfDay)
        {
            return _days[dayOfWeek].GetActivity(periodOfDay);
        }

        public Activity GetActivity()
        {
            return _days[TimeManager.CurrentDay].GetActivity(TimeManager.CurrentPeriod);
        }

        public Activity GetNextActivity()
        {
            int[] nextPeiod = TimeManager.GetNextPeriod();
            return _days[nextPeiod[0]].GetActivity(nextPeiod[1]);
        }
    }
}
