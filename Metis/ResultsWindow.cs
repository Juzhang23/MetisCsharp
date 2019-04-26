using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Metis
{
    public partial class ResultsWindow : Form
    {
        const int t5min = 288;

        DataTable dtP = new DataTable();
        DataTable dtP_graph = new DataTable();
        DataTable dtPramp = new DataTable();
        DataTable dtPramp_graph = new DataTable();
        DataTable dtI = new DataTable();
        DataTable dtPrices = new DataTable();
        DataTable dtPrices_graph = new DataTable();
        DataTable dtES_CHP = new DataTable(); // Charge Power
        DataTable dtES_CHR = new DataTable(); // Charge Ramp
        DataTable dtES_DISP = new DataTable(); // Discharge Power
        DataTable dtES_DISR = new DataTable(); // Discharge Ramp
        DataTable dtES_NISSE = new DataTable();
        DataTable dtES_Energy = new DataTable();
        int[] x = new int[24];
        double[] x288 = new double [t5min];
        double[] y = new double [24];
        double[] y288 = new double [t5min];

        string sTotalCost = "";
        int iCurrentParam = 0;

        //Parameter Identifier
        const int iGenPower = 1;
        const int iGenRamp = 2;
        const int iI = 3;
        const int iTotCost = 4;
        const int iChargePower = 5;
        const int iChargeRamp = 6;
        const int iDischargePower = 7;
        const int iDischargeRamp = 8;
        const int iNISSE = 9;
        const int iEnergy = 10;
        const int iPrices = 11;
        //
        int iModel = 0; // DTUC = 1, CTUC = 2, CTUCwES = 3, CTUCwNonMarketES = 4
        public ResultsWindow()
        {
            InitializeComponent();
            this.AllowTransparency = false;
            lbUnitSelect.Visible = false;
            cbGraphUnit.Visible = false;
            show_btns(false, false);
            hide_all_objects();
            twCategoriesTree.ExpandAll();
            for (int i = 0; i < 24; i++)
            {
                x[i] = i + 1;
            }
            for (int i = 0; i < 288; i++)
            {
                x288[i] = (i *1.0)/12;
            }
        }

        private void btnGraphView_Click(object sender, EventArgs e)
        {
            active_graph_button(true);
            active_table_button(false);
            ResultsTable.Visible = false;
            if (iModel == 1 && iCurrentParam == 11)
            {
                cbGraphUnit.Visible = false;
                lbUnitSelect.Visible = false;
                graph_discrete_prices_table(dtPrices, 24);
                chGraph.Visible = true;
            }
        }

        private void active_graph_button(Boolean isActive)
        {
            if (isActive)
            {
                btnGraphView.FlatAppearance.BorderColor = Color.Turquoise;
                btnGraphView.FlatAppearance.BorderSize = 1;
                lbUnitSelect.Visible = true;
                cbGraphUnit.Visible = true;
            }
            else
            {
                btnGraphView.FlatAppearance.BorderSize = 0;
                lbUnitSelect.Visible = false;
                cbGraphUnit.Visible = false;
            }
        }

        private void active_table_button(Boolean isActive)
        {
            if (isActive)
            {
                btnTableView.FlatAppearance.BorderSize = 1;
            }
            else
            {
                btnTableView.FlatAppearance.BorderSize = 0;
            }
        }

        private void btnTableView_Click(object sender, EventArgs e)
        {
            active_graph_button(false);
            active_table_button(true);
            ResultsTable.Visible = true;
            chGraph.Visible = false;
        }
        private void show_btns(Boolean GraphBtn, Boolean TableBtn)
        {
            btnGraphView.Visible = GraphBtn;
            btnTableView.Visible = TableBtn;
        }
        public void ProcessDTUCResults(DataTable P, DataTable I, DataTable LMP, string TotalCost, DataTable P_Ramping)
        {
            dtP = P;
            dtPramp = P_Ramping;
            dtI = I;
            dtPrices = LMP;
            iModel = 1;
            sTotalCost = TotalCost;
            update_treeview(false);
            this.Show();
        }
        public void ProcessCTUCResults(DataTable P, DataTable P_Ramping, DataTable Lambda, string TotalCost, DataTable I)
        {
            dtP = P;
            dtPrices = Lambda;
            dtPramp = P_Ramping;
            dtI = I;
            dtPrices = Lambda;
            sTotalCost = TotalCost;
            iModel = 2;
            update_treeview(false);
            this.Show();
        }
        public void ProcessCTUCwithES_MarketResults(DataTable P, DataTable I, DataTable LMP, string TotalCost, DataTable P_Ramping, DataTable ES_CH_P, 
            DataTable ES_CH_R, DataTable ES_DIS_P, DataTable ES_DIS_R, DataTable NISSE, DataTable Energy)
        {
            dtP = P;
            dtPramp = P_Ramping;
            dtI = I;
            dtPrices = LMP;
            iModel = 3;
            sTotalCost = TotalCost;

            dtES_CHP = ES_CH_P; // Charge Power
            dtES_CHR = ES_CH_R; // Charge Ramp
            dtES_DISP = ES_DIS_P; // Discharge Power
            dtES_DISR = ES_DIS_R; // Discharge Ramp
            dtES_NISSE = NISSE;
            dtES_Energy = Energy;
            this.Show();
        }
        public void ProcessCTUCwithES_NonMarketResults(DataTable P, DataTable I, DataTable LMP, string TotalCost, DataTable P_Ramping, DataTable ES_CH_P,
            DataTable ES_CH_R, DataTable ES_DIS_P, DataTable ES_DIS_R, DataTable NISSE, DataTable Energy)
        {
            dtP = P;
            dtPramp = P_Ramping;
            dtI = I;
            dtPrices = LMP;
            iModel = 3;
            sTotalCost = TotalCost;

            dtES_CHP = ES_CH_P; // Charge Power
            dtES_CHR = ES_CH_R; // Charge Ramp
            dtES_DISP = ES_DIS_P; // Discharge Power
            dtES_DISR = ES_DIS_R; // Discharge Ramp
            dtES_NISSE = NISSE;
            dtES_Energy = Energy;
            this.Show();
        }

        private void ResultsWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainWindow.ActiveForm.Show();
        }

        private void twCategoriesTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string s = twCategoriesTree.SelectedNode.Text;
            if (s == "Power Generation (MW)")
            {
                iCurrentParam = 1;
                Console.WriteLine("In P GEN");
                selected_gen_p();
            }
            else if (s == "Generation Ramping")
            {
                iCurrentParam = 2;
                selected_gen_ramp();
            }
            else if (s == "I")
            {
                iCurrentParam = 3;
                selected_I();
            }
            else if (s == "Total Cost")
            {
                iCurrentParam = 4;
                selected_total_cost();
            }
            else if (s == "Charge Power")
            {
                iCurrentParam = 5;
                selected_ES_charge_P();
            }
            else if (s == "Charge Ramping")
            {
                iCurrentParam = 6;
                selected_ES_charge_ramp();
            }
            else if (s == "Discharge Power")
            {
                iCurrentParam = 7;
                selected_ES_disharge_P();
            }
            else if (s == "Discharge Ramping")
            {
                iCurrentParam = 8;
                selected_ES_discharge_ramp();
            }
            else if (s == "NISSE")
            {
                iCurrentParam = 9;
                selected_NISSE();
            }
            else if (s == "Energy")
            {
                iCurrentParam = 10;
                selected_Energy();
            }
            else if (s == "Electricity Prices ($/MW)")
            {
                iCurrentParam = 11;
                selected_Prices();
            }
        }
        private void selected_gen_p()
        {
            hide_all_objects();
            show_btns(true, true);
            if (iModel == 1)
            {
                Console.WriteLine("P");
                ResultsTable.Visible = true;
                ResultsTable.DataSource = dtP;
                fill_cb_graph(dtP, false);
            }
            else
            {
                only_graph_display();
                fill_cb_graph(dtP, true);
            }
        }
        private void selected_gen_ramp()
        {  
            hide_all_objects();
            cbGraphUnit.Items.Clear();
            show_btns(true, true);
            if (iModel == 1)
            {
                Console.WriteLine("P RAMP");
                ResultsTable.Visible = true;
                ResultsTable.DataSource = dtPramp;
                fill_cb_graph(dtPramp, false);
            }
            else
            {
                only_graph_display();
                fill_cb_graph(dtPramp, true);
            }
        }
        private void selected_I()
        {
            hide_all_objects();
            show_btns(true, true);
            ResultsTable.DataSource = dtI;
            fill_cb_graph(dtI, false);
            Console.WriteLine("I");
            active_graph_button(false);
            active_table_button(true);
            ResultsTable.Visible = true;
        }
        private void selected_Prices()
        {
            hide_all_objects();
            
            if (iModel == 1)
            {
                hide_all_objects();
                show_btns(true, true);
                Console.WriteLine("LMP");
                ResultsTable.Visible = true;
                ResultsTable.DataSource = dtPrices;
                cbGraphUnit.Visible = false;
                graph_1d_table(dtPrices, "Time (h)", "$ / MW");
            }
            else 
            {
                ResultsTable.Visible = false;
                lbValue1.Visible = false;
                show_btns(false, false);
                active_graph_button(false);
                chGraph.Visible = true;
                graph_1d_table(dtPrices, "Time (h)", "$ / MW");
            }
        }
        private void selected_total_cost()
        {
            hide_all_objects();
            show_btns(false, false);
            lbValue1.Visible = true;
            lbValue1.Text = "$ " + sTotalCost;
        }
        private void selected_ES_charge_P()
        {
            hide_all_objects();
            show_btns(true, true);
            only_graph_display();
            fill_cb_graph(dtES_CHP, true);
        }
        private void selected_ES_disharge_P()
        {
            hide_all_objects();
            show_btns(true, true);
            only_graph_display();
            fill_cb_graph(dtES_DISP, true);
        }
        private void selected_ES_charge_ramp()
        {
            hide_all_objects();
            show_btns(true, true);
            only_graph_display();
            fill_cb_graph(dtES_CHR, true);
        }
        private void selected_ES_discharge_ramp()
        {
            hide_all_objects();
            show_btns(true, true);
            only_graph_display();
            fill_cb_graph(dtES_DISR, true);
        }
        private void selected_NISSE()
        {
            hide_all_objects();
            show_btns(true, true);
            only_graph_display();
            fill_cb_graph(dtES_NISSE, true);
        }
        private void selected_Energy()
        {
            hide_all_objects();
            show_btns(true, true);
            only_graph_display();
            fill_cb_graph(dtES_Energy, true);
        }

        private void hide_all_objects()
        {
            cbGraphUnit.ResetText();
            ResultsTable.Visible = false;
            lbValue1.Visible = false;
            chGraph.Visible = false;
        }



        private void cbGraphUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string expression = "";
            hide_all_objects();
            if ((iModel > 1) && (iCurrentParam != iI))
            {
                only_graph_display();
            }
            chGraph.Visible = true;
            int selected_item = Convert.ToInt16(cbGraphUnit.SelectedItem.ToString()); ;
            chGraph.ChartAreas[0].AxisX.Minimum = 0;
            chGraph.ChartAreas[0].AxisX.Maximum = 25;
            chGraph.ChartAreas[0].AxisX.Interval = 1;
            switch (iCurrentParam)
            {
                case iGenPower:                  
                    if (iModel == 1)
                    {
                        extract_y_values(dtP, selected_item - 1,24);
                        chGraph.Series[0].ChartType = SeriesChartType.StepLine;
                        chGraph.Series[0].Points.DataBindXY(x, y);
                    }
                    else
                    {
                        selected_item -= 1;
                        expression = "Unit = " + selected_item.ToString();
                        chGraph.DataSource = dtP.Select(expression);                      
                        chGraph.Series[0].ChartType = SeriesChartType.Spline;
                        chGraph.Series[0].XValueMember = "Time";
                        chGraph.Series[0].YValueMembers = "Value";
                        chGraph.DataBind();
                    }                 
                    break;

                case iGenRamp:
                    if (iModel == 1)
                    {
                        extract_y_values(dtPramp, selected_item - 1, 24);
                        chGraph.Series[0].ChartType = SeriesChartType.Spline;
                        chGraph.Series[0].Points.DataBindXY(x, y);
                    }
                    else
                    {
                        expression = "Unit = " + (selected_item - 1).ToString();
                        chGraph.DataSource = dtPramp.Select(expression);
                        chGraph.Series[0].ChartType = SeriesChartType.Spline;
                        chGraph.Series[0].XValueMember = "Time";
                        chGraph.Series[0].YValueMembers = "Value";
                        chGraph.DataBind();
                    }
                    
                    break;
                case iI:
                    extract_y_values(dtI, selected_item - 1, 24);
                    chGraph.Series[0].ChartType = SeriesChartType.StepLine; 
                    chGraph.Series[0].Points.DataBindXY(x, y);
                    break;
                case iChargePower:
                    selected_item -= 1;
                    expression = "Unit = " + selected_item.ToString();
                    chGraph.DataSource = dtES_CHP.Select(expression);

                    chGraph.Series[0].ChartType = SeriesChartType.Spline;
                    chGraph.Series[0].XValueMember = "Time";
                    chGraph.Series[0].YValueMembers = "Value";
                    chGraph.DataBind();
                    break;
                case iChargeRamp:
                    selected_item -= 1;
                    expression = "Unit = " + selected_item.ToString();
                    chGraph.DataSource = dtES_CHR.Select(expression);

                    chGraph.Series[0].ChartType = SeriesChartType.Spline;
                    chGraph.Series[0].XValueMember = "Time";
                    chGraph.Series[0].YValueMembers = "Value";
                    chGraph.DataBind();
                    break;
                case iDischargePower:
                    selected_item -= 1;
                    expression = "Unit = " + selected_item.ToString();
                    chGraph.DataSource = dtES_DISP.Select(expression);

                    chGraph.Series[0].ChartType = SeriesChartType.Spline;
                    chGraph.Series[0].XValueMember = "Time";
                    chGraph.Series[0].YValueMembers = "Value";
                    chGraph.DataBind();
                    break;
                case iDischargeRamp:
                    selected_item -= 1;
                    expression = "Unit = " + selected_item.ToString();
                    chGraph.DataSource = dtES_DISR.Select(expression);

                    chGraph.Series[0].ChartType = SeriesChartType.Spline;
                    chGraph.Series[0].XValueMember = "Time";
                    chGraph.Series[0].YValueMembers = "Value";
                    chGraph.DataBind();
                    break;
                case iNISSE:
                    selected_item -= 1;
                    expression = "Unit = " + selected_item.ToString();
                    chGraph.DataSource = dtES_NISSE.Select(expression);

                    chGraph.Series[0].ChartType = SeriesChartType.Spline;
                    chGraph.Series[0].XValueMember = "Time";
                    chGraph.Series[0].YValueMembers = "Value";
                    chGraph.DataBind();
                    break;
                case iEnergy:
                    selected_item -= 1;
                    expression = "Unit = " + selected_item.ToString();
                    chGraph.DataSource = dtES_Energy.Select(expression);

                    chGraph.Series[0].ChartType = SeriesChartType.Spline;
                    chGraph.Series[0].XValueMember = "Time";
                    chGraph.Series[0].YValueMembers = "Value";
                    chGraph.DataBind();
                    break;
            }
        }

        private void clear_cb_items()
        {
            cbGraphUnit.Items.Clear();
        }
        private void extract_y_values(DataTable dt, int iRow, int iNumTimePeriods)
        {
            for (int i = 0; i < iNumTimePeriods; i++)
            {
                y[i] = Convert.ToDouble(dt.Rows[iRow][i + 1]);
            }
        }

        private static void PrintTable(DataTable dt)
        {
            DataTableReader dtReader = dt.CreateDataReader();
            while (dtReader.Read())
            {
                for (int i = 0; i < dtReader.FieldCount; i++)
                {
                    Console.Write("{0} = {1} ",
                        dtReader.GetName(i).Trim(),
                        dtReader.GetValue(i).ToString().Trim());
                }
                Console.WriteLine();
            }
            dtReader.Close();
        }

        private void fill_cb_graph(DataTable dt, Boolean isZeroBased)
        {
            clear_cb_items();
            int NumUnits = (int)dt.Compute("Max(Unit)", "");
            if (isZeroBased)
            {
                NumUnits++;
            }
            Console.WriteLine(NumUnits);
            for (int i = 0; i < NumUnits; i++)
            {
                cbGraphUnit.Items.Add((i+1).ToString());
            }            
        }
        private void only_graph_display()
        {
            ResultsTable.Visible = false;
            lbValue1.Visible = false;
            show_btns(true, false);
            active_graph_button(true);
        }
        private void graph_1d_table(DataTable dt, string sXlabel, string sYlabel)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                y288[i] = Convert.ToDouble(dt.Rows[i][0]);
            }
            chGraph.ChartAreas[0].AxisX.Title = sXlabel;
            chGraph.ChartAreas[0].AxisY.Title = sYlabel;
            chGraph.Series[0].ChartType = SeriesChartType.Spline;
            chGraph.Series[0].Points.DataBindXY(x288, y288);
        }

        private void graph_discrete_prices_table(DataTable dt, int length)
        {
            for (int i = 0; i < length; i++)
            {
                y[i] = Convert.ToDouble(dt.Rows[0][i]);
            }
            chGraph.Series[0].ChartType = SeriesChartType.StepLine;
            chGraph.Series[0].Points.DataBindXY(x, y);
        }

        private void update_treeview(Boolean isES)
        {
            if (isES)
            {
                
            }
            else
            {
                twCategoriesTree.Nodes[0].Nodes[3].Remove();
            }
        }
    }
}
