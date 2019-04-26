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
    public partial class GeneratorParameters : Form
    {
        MainWindow incomingForm;
        public GeneratorParameters(MainWindow OriginalForm)
        {
            incomingForm = OriginalForm;
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            incomingForm.iGenUnitCount++;
            incomingForm.GenRow[1] = tbxMinGen.Text;
            incomingForm.GenRow[2] = tbxMaxGen.Text;
            incomingForm.GenRow[3] = tbxMinGenCost.Text;
            incomingForm.GenRow[4] = tbxStartCost.Text;
            incomingForm.GenRow[5] = tbxMinUpTime.Text;
            incomingForm.GenRow[6] = tbxMinDownTime.Text;
            incomingForm.GenRow[7] = tbxRampUp.Text;
            incomingForm.GenRow[8] = tbxRampDown.Text;
            incomingForm.GenRow[9] = tbxStartRamp.Text;
            incomingForm.GenRow[10] = tbxShutRamp.Text;
            incomingForm.GenRow[11] = tbxQtA.Text;
            incomingForm.GenRow[12] = tbxPriceA.Text;
            incomingForm.GenRow[13] = tbxQtB.Text;
            incomingForm.GenRow[14] = tbxPriceB.Text;
            incomingForm.GenRow[15] = tbxQtC.Text;
            incomingForm.GenRow[16] = tbxPriceC.Text;
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
