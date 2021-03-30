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
        public FrmMain()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(graphicInterface.OnTick);
        }

        List<Person> dummyPersons;
        List<Building> dummyBuilding;
        private void Start_Click(object sender, EventArgs e)
        {
            if (graphicInterface.HasStarted)
            {
                dummyPersons = new List<Person>();
                dummyBuilding = new List<Building>();
                graphicInterface.Generate(dummyPersons, dummyBuilding);
            }
            timer.Enabled = true;
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
        }
    }
}
