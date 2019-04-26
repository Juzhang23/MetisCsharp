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
    public partial class uPathForm : Form
    {
        MainWindow incomingForm;
        string path = "";
        public uPathForm(MainWindow OriginalForm)
        {
            incomingForm = OriginalForm;
            InitializeComponent();
        }
        private void btnPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.FileName;
                textBox1.Text = path;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            itfAmpl amplData = new itfAmpl();
            incomingForm.DataFilePath = path;
            DataTable dtGen = amplData.GetInputGenData(path);
            incomingForm.SetGenTableDataSource(dtGen);
            DataTable dtES = amplData.GetInputESData(path);
            incomingForm.SetESTableDataSource(dtES);
            incomingForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            incomingForm.Show();
            this.Close();
        }
    }
}
