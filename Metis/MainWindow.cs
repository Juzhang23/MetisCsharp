using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metis
{
    public partial class MainWindow : Form
    {
        DataTable dtGenUnits = new DataTable();
        int iBtnState = 1;
        public int iGenUnitCount = 0;
        public int iESUnitCount = 0;
        public string[] GenRow = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public string[] ESRow = new string[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        public string DataFilePath = "";

        int iModelSelected = 0;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGenUnits_Click(object sender, EventArgs e)
        {
            iBtnState = 1;
            btnGenUnits.FlatAppearance.BorderSize = 1;
            btnES.FlatAppearance.BorderSize = 0;
            btnLoadData.FlatAppearance.BorderSize = 0;
            tGenUnits.Visible = true;
            tEnergyUnits.Visible = false;
            tLoadData.Visible = false;
        }

        private void btnES_Click(object sender, EventArgs e)
        {
            iBtnState = 2;
            btnGenUnits.FlatAppearance.BorderSize = 0;
            btnES.FlatAppearance.BorderSize = 1;
            btnLoadData.FlatAppearance.BorderSize = 0;
            tGenUnits.Visible = false;
            tEnergyUnits.Visible = true;
            tLoadData.Visible = false;
        }

        private void btnLoadData_Click(object sender, EventArgs e)
        {
            iBtnState = 3;
            btnGenUnits.FlatAppearance.BorderSize = 0;
            btnES.FlatAppearance.BorderSize = 0;
            btnLoadData.FlatAppearance.BorderSize = 1;
            tGenUnits.Visible = false;
            tEnergyUnits.Visible = false;
            tLoadData.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            switch (iBtnState)
            {
                case 1:
                    GeneratorParameters gpam = new GeneratorParameters(this);
                    this.Hide();
                    gpam.Show();
                    break;
                case 2:
                    ESParmeters ESpam = new ESParmeters(this);
                    this.Hide();
                    ESpam.Show();
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
        internal void AddRow()
        {
            switch (iBtnState)
            {
                case 1:
                    GenRow[0] = iGenUnitCount.ToString();
                    tGenUnits.Rows.Add(GenRow);
                    break;
                case 2:
                    ESRow[0] = iESUnitCount.ToString();
                    tEnergyUnits.Rows.Add(ESRow);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
        internal void SetGenTableDataSource(DataTable dt)
        {
            tGenUnits.Columns.Clear();
            tGenUnits.Update();
            tGenUnits.DataSource = dt;
        }

        internal void SetESTableDataSource(DataTable dt)
        {
            tEnergyUnits.Columns.Clear();
            tEnergyUnits.Update();
            tEnergyUnits.DataSource = dt;
        }
        private void btnRun_Click(object sender, EventArgs e)
        {
            itfAmpl ampl = new itfAmpl();
            switch(iModelSelected)
            {
                case 1:
                    ampl.Run(iModelSelected, "D:/CMetis/Metis/models/DTUC_noNetwork.mod", DataFilePath);
                    break;
                case 2:
                    ampl.Run(iModelSelected, "D:/CMetis/Metis/models/CTUC.mod", DataFilePath);
                    break;
                case 3:
                    ampl.Run(iModelSelected, "D:/CMetis/Metis/models/CTUC_w_ES.mod", DataFilePath);
                    break;
                case 4:
                    ampl.Run(iModelSelected, "D:/CMetis/Metis/models/CTUC_w_ES_NoCSU.mod", DataFilePath);
                    break;
                default:
                    break;
            }
        }

        private void btnMinWindow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaxWindow_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = cbModel.Items[cbModel.SelectedIndex].ToString();

            switch (s)
            {
                case "Discrete Time Unit Commitment":
                    iModelSelected = 1;
                    break;
                case "Continuous Time Unit Commitment":
                    iModelSelected = 2;
                    break;
                case "Continuous Time Unit Commitment with Market Based Energy Storage":
                    iModelSelected = 3;
                    break;
                case "Continuous Time Unit Commitment with Non-market Based Energy Storage":
                    iModelSelected = 3;
                    break;
                default:
                    break;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            uPathForm path = new uPathForm(this);
            this.Hide();
            path.Show();
        }
    }
}
