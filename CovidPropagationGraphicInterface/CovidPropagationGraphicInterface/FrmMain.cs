using CovidPropagationGraphicInterface.Classes;
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

        public FrmMain()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(graphicInterface.OnTick);
            TimeManager.Init();
            dummyPersons = new List<Person>();
            dummyBuilding = GenerateDummyBuildings(20);
            graphicInterface.Generate(dummyPersons, dummyBuilding);
        }

        private void Start_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }

        /// <summary>
        /// Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création de batiement par la simulation.
        /// </summary>
        /// <param name="quantity"></param>
        /// <returns></returns>
        private List<Building> GenerateDummyBuildings(int quantity)
        {
            Random rdm = new Random();
            List<Building> buildings = new List<Building>();
            int probaSchoolMin = 70, probaSchoolMax = 85;
            int probaSupermarketMin = 50, probaSupermarketMax = 70;
            int probaRestaurantMin = 30, probaRestaurantMax = 50;
            int probaCompanyMin = 0, probaCompanyMax = 30;

            int defaultSize = 50;
            int space = 5;
            int defaultLocationX = 0;
            int defaultLocationY = 70;
           
            for (int i = 0; i < quantity; i++)
            {
                int proba = rdm.Next(0, 100);
                BuildingType type;

                if (probaCompanyMin < proba && probaCompanyMax > proba)
                {
                    type = BuildingType.Company;
                }
                else if (probaRestaurantMin < proba && probaRestaurantMax > proba)
                {
                    type = BuildingType.Restaurant;
                }
                else if (probaSupermarketMin < proba && probaSupermarketMax > proba)
                {
                    type = BuildingType.Supermarket;
                }
                else if (probaSchoolMin < proba && probaSchoolMax > proba)
                {
                    type = BuildingType.School;
                }
                else
                {
                    type = BuildingType.Hospital;
                }

                buildings.Add(new Building(new Point((defaultLocationX + defaultSize + space) * i, defaultLocationY), new Size(defaultSize, defaultSize), type));
            }
            return buildings;
        }

        /// <summary>
        /// Ce code est utilisé uniquement dans le cadre du stage et sera supprimé 
        /// lors du travail de diplome pour permettre la création d'individus par la simulation.
        /// </summary>
        private List<Person> GenerateDummyPersons(int quantity)
        {
            
        }
    }
}
