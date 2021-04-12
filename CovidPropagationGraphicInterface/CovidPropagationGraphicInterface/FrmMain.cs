using CovidPropagationGraphicInterface.Classes;
using CovidPropagationGraphicInterface.Classes.Person;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    public partial class FrmMain : Form
    {
        List<Person> dummyPersons;
        List<Building> dummyBuilding;
        List<Vehicle> dummyVehicle;

        public FrmMain()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(graphicInterface.OnTick);
            timer.Interval = Constant.TIMER_INTERVAL;
            TimeManager.Init();
            dummyBuilding = GenerateDummyBuildings();
            dummyVehicle = GenerateDummyVehicle(5);
            dummyPersons = GenerateDummyPersons(5);
            graphicInterface.Generate(dummyPersons, dummyBuilding, dummyVehicle);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            graphicInterface.TimerStart();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            graphicInterface.TimerStop();
        }

        /// <summary>
        /// ⚠️ Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création de batiement par la simulation. ⚠️
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private List<Building> GenerateDummyBuildings()
        {
            List<Building> buildings = new List<Building>();
            buildings.Add(new Building(new Size(50, 50), BuildingType.Home));
            buildings.Add(new Building(new Size(30, 30), BuildingType.Home));
            buildings.Add(new Building(new Size(30, 30), BuildingType.Home));
            buildings.Add(new Building(new Size(30, 30), BuildingType.Home));
            buildings.Add(new Building(new Size(30, 30), BuildingType.Home));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Hospital));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Hospital));

            buildings.Add(new Building(new Size(20, 20), BuildingType.School));
            buildings.Add(new Building(new Size(20, 20), BuildingType.School));
            buildings.Add(new Building(new Size(20, 20), BuildingType.School));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Supermarket));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Supermarket));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Restaurant));

            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));
            buildings.Add(new Building(new Size(50, 50), BuildingType.Company));

            return buildings;
        }

        /// <summary>
        /// ⚠️ Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création d'individus par la simulation. ⚠️
        /// </summary>
        private List<Person> GenerateDummyPersons(int quantity)
        {
            Random rdm = new Random();
            List<Person> persons = new List<Person>();
            for (int i = 0; i < quantity; i++)
            {
                persons.Add(CreatePerson(rdm, i));
            }

            return persons;
        }

        /// <summary>
        /// ⚠️ Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création d'individus par la simulation. ⚠️
        /// </summary>
        private Person CreatePerson(Random rdm, int personIndex)
        {
            Classes.Person.Day[] day = new Classes.Person.Day[Constant.NUMBER_OF_DAY];

            int nbBuilding = dummyBuilding.Count - 1;
            Building firstactivity = dummyBuilding[rdm.Next(0, nbBuilding)];
            Building secondactivity = dummyBuilding[rdm.Next(0, nbBuilding)];
            Building thirdactivity = dummyBuilding[rdm.Next(0, nbBuilding)];
            for (int i = 0; i < Constant.NUMBER_OF_DAY; i++)
            {
                Period[] period = new Period[Constant.NUMBER_OF_PERIODS];
                for (int j = 0; j < Constant.NUMBER_OF_PERIODS; j++)
                {
                    if (j < Constant.NUMBER_OF_PERIODS / 3)
                    {
                        period[j] = new Period(firstactivity);
                    }else if (j < (Constant.NUMBER_OF_PERIODS / 3) * 2)
                    {
                        period[j] = new Period(secondactivity);
                    }else if (j < Constant.NUMBER_OF_PERIODS)
                    {
                        period[j] = new Period(thirdactivity);
                    }

                    Vehicle travelCar = dummyVehicle[personIndex];
                    if (j == Constant.NUMBER_OF_PERIODS / 3)
                    {
                        period[j] = new Period(travelCar);
                    }
                    else if (j == (Constant.NUMBER_OF_PERIODS / 3) * 2)
                    {
                        period[j] = new Period(travelCar);
                    }
                    else if (j == Constant.NUMBER_OF_PERIODS-1)
                    {
                        period[j] = new Period(travelCar);
                    }
                }
                day[i] = new Classes.Person.Day(period);
            }
            Planning planning = new Planning(day);
            return new Person(planning);
        }

        /// <summary>
        /// ⚠️ Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création d'individus par la simulation. ⚠️
        /// </summary>
        private List<Vehicle> GenerateDummyVehicle(int quantity)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            for (int i = 0; i < quantity; i++)
            {
                vehicles.Add(new Car());
            }
            return vehicles;
        }
    }
}
