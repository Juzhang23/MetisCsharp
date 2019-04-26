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
    public partial class ESParmeters : Form
    {
        MainWindow incomingForm;
        public ESParmeters(MainWindow OriginalForm)
        {
            incomingForm = OriginalForm;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            incomingForm.iESUnitCount++;
            incomingForm.ESRow[1] = tbxChP.Text;
            incomingForm.ESRow[2] = tbxDisP.Text;
            incomingForm.ESRow[3] = tbxMaxE.Text;
            incomingForm.ESRow[4] = tbxMinE.Text;
            incomingForm.ESRow[5] = tbxInitE.Text;
            incomingForm.ESRow[6] = tbxRampUp.Text;
            incomingForm.ESRow[7] = tbxRampDown.Text;
            incomingForm.ESRow[8] = tbxChEff.Text;
            incomingForm.ESRow[9] = tbxDisEff.Text;
            incomingForm.ESRow[10] = tbxChQtA.Text;
            incomingForm.ESRow[11] = tbxChPriceA.Text;
            incomingForm.ESRow[12] = tbxChQtB.Text;
            incomingForm.ESRow[13] = tbxChPriceB.Text;
            incomingForm.ESRow[14] = tbxDisQtA.Text;
            incomingForm.ESRow[15] = tbxDisPriceA.Text;
            incomingForm.ESRow[16] = tbxDisQtB.Text;
            incomingForm.ESRow[17] = tbxDisPriceB.Text;
            incomingForm.AddRow();
            incomingForm.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            incomingForm.Show();
            this.Close();
        }
    }
}
