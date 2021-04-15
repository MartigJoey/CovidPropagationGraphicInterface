using CovidPropagationGraphicInterface.Classes;
using CovidPropagationGraphicInterface.Classes.Person;
using CovidPropagationGraphicInterface.Classes.Vehicle;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CovidPropagationGraphicInterface
{
    public partial class FrmMain : Form
    {
        List<Person> dummyPersons;
        List<Vehicle> dummyVehicle;
        List<Bus> dummyBus;

        BuildingGenerator buildingGenerator;
        BusLineGenerator busLineGenerator;
        State testChange; // test le changement d'état d'un individus
        List<LineSeries> dummyDataChart;

        FrmLegend frmLegend;
        public FrmMain()
        {
            InitializeComponent();
            GlobalVariables.Interface_Size = graphicInterface.Size;
            graphicInterface.Size = GlobalVariables.Interface_Size;

            timer.Tick += new EventHandler(graphicInterface.OnTick);
            timer.Interval = GlobalVariables.TIMER_INTERVAL;
            TimeManager.Init();
            buildingGenerator = new BuildingGenerator();
            dummyVehicle = GenerateDummyVehicle(1000);
            dummyPersons = GenerateDummyPersons(1000);
            busLineGenerator = new BusLineGenerator(buildingGenerator.CenterZoneTopLeft, buildingGenerator.CenterZoneBottomRight, 
                                                    buildingGenerator.PerimeterZoneTopLeft, buildingGenerator.PerimeterZoneBottomRight);

            dummyBus = busLineGenerator.Buses;
            graphicInterface.Generate(dummyPersons, buildingGenerator.Buildings, dummyVehicle, dummyBus);
            SetClockTime();

            // ⚠️
            dummyDataChart = new List<LineSeries>();
            dummyDataChart.Add(new LineSeries { Title = "Infecté(s)", Values = new ChartValues<double>() { 0, 1, 2, 4, 2, 1, 0 } });
            crtChart.SetData(dummyDataChart);

            List<PieSeries> dummyDataPie = new List<PieSeries>();
            dummyDataPie.Add(new PieSeries { Title = "Sain(s)", Values = new ChartValues<double>() { 30 } });
            dummyDataPie.Add(new PieSeries { Title = "Infecté(s)", Values = new ChartValues<double>() { 1 } });
            pieChart.SetData(dummyDataPie);
            // ⚠️
        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            graphicInterface.TimerStart();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            testChange.CurrentState = PersonState.Infected;
            graphicInterface.TimerStop();
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
                Person person;
                if (i == quantity - 10)
                    person = CreatePerson(rdm, i, true);
                else
                    person = CreatePerson(rdm, i, false);

                persons.Add(person);
            }

            return persons;
        }

        /// <summary>
        /// ⚠️ Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création d'individus par la simulation. ⚠️
        /// </summary>
        private Person CreatePerson(Random rdm, int personIndex, bool takesBus)
        {
            Classes.Person.Day[] day = new Classes.Person.Day[GlobalVariables.NUMBER_OF_DAY];

            int nbBuilding = buildingGenerator.Buildings.Count;
            Building firstactivity = buildingGenerator.Buildings[rdm.Next(0, nbBuilding)];
            Building secondactivity = buildingGenerator.Buildings[rdm.Next(0, nbBuilding)];
            Building thirdactivity = buildingGenerator.Buildings[rdm.Next(0, nbBuilding)];
            for (int i = 0; i < GlobalVariables.NUMBER_OF_DAY; i++)
            {
                Period[] period = new Period[GlobalVariables.NUMBER_OF_PERIODS];
                for (int j = 0; j < GlobalVariables.NUMBER_OF_PERIODS; j++)
                {
                    if (j < GlobalVariables.NUMBER_OF_PERIODS / 3)
                    {
                        period[j] = new Period(firstactivity);
                    }else if (j < (GlobalVariables.NUMBER_OF_PERIODS / 3) * 2)
                    {
                        period[j] = new Period(secondactivity);
                    }else if (j < GlobalVariables.NUMBER_OF_PERIODS)
                    {
                        period[j] = new Period(thirdactivity);
                    }

                    if (takesBus)
                    {

                    }
                    else
                    {
                        Vehicle travelCar = dummyVehicle[personIndex];
                        if (j == GlobalVariables.NUMBER_OF_PERIODS / 3)
                        {
                            period[j] = new Period(travelCar);
                        }
                        else if (j == (GlobalVariables.NUMBER_OF_PERIODS / 3) * 2)
                        {
                            period[j] = new Period(travelCar);
                        }
                        else if (j == GlobalVariables.NUMBER_OF_PERIODS - 1)
                        {
                            period[j] = new Period(travelCar);
                        }
                    }
                }
                day[i] = new Classes.Person.Day(period);
            }
            Planning planning = new Planning(day);
            testChange = new State();
            return new Person(planning, testChange);
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

        private void Timer_Tick(object sender, EventArgs e)
        {
            SetClockTime();
            dummyDataChart[0].Values.Add(2d);
        }

        private void SetClockTime()
        {
            lblClock.Text = $"Jour : {TimeManager.CurrentDayString} Heure : {TimeManager.CurrentHour}";
        }

        private void btnLegend_Click(object sender, EventArgs e)
        {
            frmLegend = new FrmLegend();
            frmLegend.Show();
        }
    }
}
