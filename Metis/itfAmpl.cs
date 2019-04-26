using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ampl;
using ampl.Entities;
using System.Text.RegularExpressions;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.Diagnostics;

namespace Metis
{

    class itfAmpl
    {
        const int DTUC = 1;
        const int CTUC = 2;
        const int CTUC_ES = 3;
        const int CTUC_NON_MARKET_ES = 4;
        const int t5min = 288;
        const int iNumESUnits = 1;

        char[] BadChars = { 'G', '\'', 'I', 's', 'H', 'M', 'N', 'R' };
        public void Run(int iModel, string sModelPath, string sDataPath)
        {
            ResultsWindow resWindow = new ResultsWindow();
            // Create an AMPL instance
            using (AMPL a = new AMPL())
            {
                DataFrame Gens;
                DataFrame P;

                a.SetOption("solver", "gurobi");
                a.Read(sModelPath);
                a.ReadData(sDataPath);

                // Solve
                a.Solve();

                // Get objective entity by AMPL name
                string sTotalCost = a.GetObjective("Total_Cost").Value.ToString("0.00");
                int iNumGenUnits = Convert.ToInt16(a.GetSet("GENERATORS").Size.ToString());
                if (iModel == CTUC_ES | iModel == CTUC_NON_MARKET_ES)
                {
                    int iNumESUnits = a.GetSet("ENERGY_ST").Size;
                }
                switch (iModel)
                {
                    case DTUC:
                        Gens = a.GetParameter("GenData").GetValues();
                        P = a.GetVariable("P").GetValues();
                        DataTable dtP = generate_DT_2D_table(P);
                        DataTable dtPRamp = calculate_ramping(dtP, true);
                        DataTable dtI = generate_DT_2D_table(a.GetVariable("I").GetValues());
                        DataTable dtLMP = generate_1D_table(a.GetConstraint("Power_Balance").GetValues());

                        resWindow.ProcessDTUCResults(dtP, dtI, dtLMP, sTotalCost, dtPRamp);
                        break;

                    case CTUC:
                        DataTable CTUC_dtP = array2d_to_datatable(
                                                CT_P_Post_Mortem(Dataframe_to_3d_array(a.GetVariable("G_H").GetValues(), iNumGenUnits, 24, 4),
                                                iNumGenUnits)
                                                );
                        DataTable CTUC_dtLamda = array1d_to_datatable(
                                                        CT_Price_Post_Mortem(Dataframe_to_2d_array(a.GetConstraint("Load_Balance1").GetValues(), 24, 4, true),
                                                                            Dataframe_to_2d_array(a.GetConstraint("Load_Balance2").GetValues(), 24, 4, true),
                                                                            iNumGenUnits)
                                                        );
                        DataTable CTUC_dtPRamp = calculate_ramping1(
                                                    CT_P_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("G_H").GetValues(), iNumGenUnits, 24, 4),
                                                        iNumGenUnits)
                                                        );
                        DataTable CTUC_dtI = generate_DT_2D_table(a.GetVariable("I").GetValues());
                        resWindow.ProcessCTUCResults(CTUC_dtP, CTUC_dtPRamp, CTUC_dtLamda, sTotalCost, CTUC_dtI);
                        break;

                    case CTUC_ES:
                        double[,] arr_G_cont = CT_P_Post_Mortem(
                                                    Dataframe_to_3d_array(a.GetVariable("G_H").GetValues(), iNumGenUnits, 24, 4),
                                                    iNumGenUnits);

                        double[] arr_Lambda_cont = CT_Price_Post_Mortem(
                                                        Dataframe_to_2d_array(a.GetConstraint("Load_Balance1").GetValues(), 24, 4, true),
                                                        Dataframe_to_2d_array(a.GetConstraint("Load_Balance2").GetValues(), 24, 4, true),
                                                        iNumGenUnits);

                        double[,] arr_ES_G_cont = CT_ES_CH_DIS_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("D_H_S").GetValues(), iNumGenUnits, 24, 4),
                                                        iNumESUnits);
                        double[,] arr_ES_D_cont = CT_ES_CH_DIS_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("G_H_S").GetValues(), iNumGenUnits, 24, 4),
                                                        iNumESUnits);

                        double[,] arr_ES_E_cont = CT_ES_Energy_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("E_B_S").GetValues(), iNumGenUnits, 24, 5),
                                                        iNumESUnits);

                        double[,] arr_Gamma_E_cont = CT_ES_Gamma_Post_Mortem(
                                                            Dataframe_to_3d_array(a.GetConstraint("Integral1_INI").GetValues(), iNumGenUnits, 24, 5),
                                                            Dataframe_to_3d_array(a.GetConstraint("ES_minE").GetValues(), iNumGenUnits, 24, 5),
                                                            Dataframe_to_3d_array(a.GetConstraint("ES_maxE").GetValues(), iNumGenUnits, 24, 5),
                                                            iNumESUnits);

                        DataTable dtCTUC_ES_P = array2d_to_datatable(arr_G_cont);
                        DataTable dtCTUC_ES_Lamda = array1d_to_datatable(arr_Lambda_cont);
                        DataTable dtCTUC_ES_PRamp = calculate_ramping1(arr_G_cont);
                        DataTable dtCTUC_ES_I = generate_DT_2D_table(a.GetVariable("I").GetValues());

                        DataTable dtCTUC_ES_CHP = array2d_to_datatable(arr_ES_G_cont);
                        DataTable dtCTUC_ES_CHPR = calculate_ramping1(arr_ES_G_cont);
                        DataTable dtCTUC_ES_DISP = array2d_to_datatable(arr_ES_D_cont);
                        DataTable dtCTUC_ES_DISPR = calculate_ramping1(arr_ES_D_cont);

                        DataTable dtCTUC_ES_E = array2d_to_datatable(arr_ES_E_cont);
                        DataTable dtCTUC_ES_NISSE = array2d_to_datatable(arr_Gamma_E_cont);

                        resWindow.ProcessCTUCwithES_MarketResults(dtCTUC_ES_P,
                                                                    dtCTUC_ES_I,
                                                                    dtCTUC_ES_Lamda,
                                                                    sTotalCost,
                                                                    dtCTUC_ES_PRamp,
                                                                    dtCTUC_ES_CHP,
                                                                    dtCTUC_ES_CHPR,
                                                                    dtCTUC_ES_DISP,
                                                                    dtCTUC_ES_DISPR,
                                                                    dtCTUC_ES_NISSE,
                                                                    dtCTUC_ES_E);
                        break;

                    case CTUC_NON_MARKET_ES:
                        double[,] arr_G_cont_NM = CT_P_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("G_H").GetValues(), iNumGenUnits, 24, 4),
                                                        iNumGenUnits);

                        double[] arr_Lambda_cont_NM = CT_Price_Post_Mortem(
                                                        Dataframe_to_2d_array(a.GetConstraint("Load_Balance1").GetValues(), 24, 4, true),
                                                        Dataframe_to_2d_array(a.GetConstraint("Load_Balance2").GetValues(), 24, 4, true),
                                                        iNumGenUnits);

                        double[,] arr_ES_G_cont_NM = CT_ES_CH_DIS_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("D_H_S").GetValues(), iNumGenUnits, 24, 4),
                                                        iNumESUnits);
                        double[,] arr_ES_D_cont_NM = CT_ES_CH_DIS_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("G_H_S").GetValues(), iNumGenUnits, 24, 4),
                                                        iNumESUnits);

                        double[,] arr_ES_E_cont_NM = CT_ES_Energy_Post_Mortem(
                                                        Dataframe_to_3d_array(a.GetVariable("E_B_S").GetValues(), iNumGenUnits, 24, 5),
                                                        iNumESUnits);

                        double[,] arr_Gamma_E_cont_NM = CT_ES_Gamma_Post_Mortem(
                                                            Dataframe_to_3d_array(a.GetConstraint("Integral1_INI").GetValues(), iNumGenUnits, 24, 5),
                                                            Dataframe_to_3d_array(a.GetConstraint("ES_minE").GetValues(), iNumGenUnits, 24, 5),
                                                            Dataframe_to_3d_array(a.GetConstraint("ES_maxE").GetValues(), iNumGenUnits, 24, 5),
                                                            iNumESUnits);

                        DataTable dtCTUC_ES_NM_P = array2d_to_datatable(arr_G_cont_NM);
                        DataTable dtCTUC_ES_NM_Lamda = array1d_to_datatable(arr_Lambda_cont_NM);
                        DataTable dtCTUC_ES_NM_PRamp = calculate_ramping1(arr_G_cont_NM);
                        DataTable dtCTUC_ES_NM_I = generate_DT_2D_table(a.GetVariable("I").GetValues());

                        DataTable dtCTUC_ES_NM_CHP = array2d_to_datatable(arr_ES_G_cont_NM);
                        DataTable dtCTUC_ES_NM_CHPR = calculate_ramping1(arr_ES_G_cont_NM);
                        DataTable dtCTUC_ES_NM_DISP = array2d_to_datatable(arr_ES_D_cont_NM);
                        DataTable dtCTUC_ES_NM_DISPR = calculate_ramping1(arr_ES_D_cont_NM);

                        DataTable dtCTUC_ES_NM_E = array2d_to_datatable(arr_ES_E_cont_NM);
                        DataTable dtCTUC_ES_NM_NISSE = array2d_to_datatable(arr_Gamma_E_cont_NM);

                        resWindow.ProcessCTUCwithES_MarketResults(dtCTUC_ES_NM_P,
                                                                    dtCTUC_ES_NM_I,
                                                                    dtCTUC_ES_NM_Lamda,
                                                                    sTotalCost,
                                                                    dtCTUC_ES_NM_PRamp,
                                                                    dtCTUC_ES_NM_CHP,
                                                                    dtCTUC_ES_NM_CHPR,
                                                                    dtCTUC_ES_NM_DISP,
                                                                    dtCTUC_ES_NM_DISPR,
                                                                    dtCTUC_ES_NM_NISSE,
                                                                    dtCTUC_ES_NM_E);
                        break;

                    default:
                        P = a.GetVariable("P").GetValues();
                        break;
                }
            }


        }

        private DataTable generate_graph_data(DataFrame df, int iDimension, int iNumVarChar)
        {
            DataTable dt = new DataTable();
            string name = df.GetHeaders()[iDimension].ToString();
            string s_curr_index = "";
            if (iDimension == 2)
            {
                dt.Columns.Add("Unit", typeof(int));
                dt.Columns.Add("Time", typeof(int));
                dt.Columns.Add("Value", typeof(double));
                for (int i = 0; i < df.NumRows; i++)
                {
                    s_curr_index = df.GetColumn("index0").ToArray()[i].ToString().Trim('\'');
                    DataRow valueRow = dt.NewRow();
                    valueRow["Value"] = Convert.ToDouble(df.GetColumn(name).ToArray()[i].ToString());
                    valueRow["Unit"] = Convert.ToInt16(s_curr_index.Substring(iNumVarChar));
                    valueRow["Time"] = Convert.ToInt16(df.GetColumn("index1").ToArray()[i].ToString().Trim('\''));
                    dt.Rows.Add(valueRow);
                }
            }
            else if (iDimension == 1)
            {
                dt.Columns.Add("Time", typeof(int));
                dt.Columns.Add("Value", typeof(double));
                for (int i = 0; i < df.NumRows; i++)
                {
                    s_curr_index = df.GetColumn("index0").ToArray()[i].ToString().Trim('\'');
                    DataRow valueRow = dt.NewRow();
                    valueRow["Value"] = Convert.ToDouble(df.GetColumn(name).ToArray()[i].ToString());
                    valueRow["Time"] = Convert.ToInt16(df.GetColumn("index0").ToArray()[i].ToString().Trim('\''));
                    dt.Rows.Add(valueRow);
                }
            }
            return dt;
        }
        private DataTable generate_DT_2D_table(DataFrame df)
        {
            List<string> gen_index = new List<string>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Unit", typeof(int));

            int time = 24;
            int iNumUnits = Convert.ToInt16(df.NumRows.ToString()) / time;
            double[] timeColumns = new double[time];

            for (int j = 0; j < time; j++)
            {
                timeColumns[j] = j + 1;
                dt.Columns.Add("T" + (j + 1), typeof(double));
            }

            string s_prev_index = "";
            string s_curr_index = "";
            int count = 1;

            for (int i = 0; i < df.NumRows; i += time)
            {
                s_curr_index = df.GetRowByIndex(i)[0].ToString().Trim('\'');
                if (s_curr_index != s_prev_index)
                {
                    s_prev_index = s_curr_index;
                }
                //Console.WriteLine("Current Index = " + s_curr_index);
                DataRow valueRow = dt.NewRow();
                valueRow["Unit"] = Convert.ToInt16(s_curr_index.Trim(BadChars));

                for (int j = 0; j < time; j++)
                {
                    valueRow["T" + (j + 1)] = Convert.ToDouble(df.GetRow(s_curr_index, j + 1)[2].ToString());
                }
                dt.Rows.Add(valueRow);
                count++;
            }
            DataView dv = dt.DefaultView;
            dv.Sort = "Unit ASC";
            //PrintTable(dt);
            return dv.ToTable();
        }

        private static DataTable array2d_to_datatable(double[,] arr)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Unit", typeof(int));
            dt.Columns.Add("Time", typeof(double));
            dt.Columns.Add("Value", typeof(double));

            for (int g = 0; g < arr.GetLength(0); g++)
            {
                for (int s = 0; s < arr.GetLength(1); s++)
                {
                    DataRow valueRow = dt.NewRow();
                    valueRow["Value"] = arr[g, s];
                    valueRow["Unit"] = g;
                    valueRow["Time"] = (1.0 * s) / 12;
                    dt.Rows.Add(valueRow);
                }
            }
            return dt;
        }

        private static DataTable array1d_to_datatable(double[] arr)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value", typeof(double));

            for (int g = 0; g < arr.GetLength(0); g++)
            {
                DataRow valueRow = dt.NewRow();
                valueRow["Value"] = arr[g];
                dt.Rows.Add(valueRow);
            }
            return dt;
        }

        private static DataTable generate_1D_table(DataFrame df)
        {
            List<string> gen_index = new List<string>();
            DataTable dt = new DataTable();
            int time = 24;

            for (int j = 0; j < time; j++)
            {
                dt.Columns.Add("T" + (j + 1), typeof(double));
            }

            for (int i = 0; i < df.NumRows; i += time) //Since there are no Line constraints, this statement is not needed. It will be useful if there are actual LMPs 
            {
                DataRow valueRow = dt.NewRow();
                for (int j = 0; j < time; j++)
                {
                    valueRow["T" + (j + 1)] = Convert.ToDouble(df.GetRow(j + 1)[1].ToString());
                }
                dt.Rows.Add(valueRow);
            }
            //PrintTable(dt);
            return dt;
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
        private static DataTable calculate_ramping(DataTable dt, Boolean isDiscrete)
        {
            DataTable result = new DataTable();
            result = dt.Copy();
            double f_curr_index = 0;
            double f_prev_index = 0;

            double temp = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 1; j < dt.Rows[0].ItemArray.Count(); j++)
                {
                    if (j == 1)
                    {
                        result.Rows[i][j] = 0;
                    }
                    else
                    {
                        f_curr_index = Convert.ToDouble(dt.Rows[i][j]);
                        f_prev_index = Convert.ToDouble(dt.Rows[i][j - 1]);
                        if (isDiscrete)
                        {
                            temp = (f_curr_index - f_prev_index);
                        }
                        else
                        {
                            temp = (f_curr_index - f_prev_index);
                            temp = temp * 12;
                        }
                        result.Rows[i][j] = temp;
                    }
                }
            }
            return result;
        }

        private static DataTable calculate_ramping1(double[,] arr)
        {
            DataTable result = new DataTable();
            result.Columns.Add("Unit", typeof(int));
            result.Columns.Add("Time", typeof(double));
            result.Columns.Add("Value", typeof(double));
            double f_curr_index = 0;
            double f_prev_index = 0;

            double temp = 0;

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 1; j < arr.GetLength(1); j++)
                {
                    DataRow valueRow = result.NewRow();
                    valueRow["Unit"] = i;
                    valueRow["Time"] = (1.0 * j) / 12;

                    if (j == 1)
                    {
                        valueRow["Value"] = 0;// result.Rows[i][j] = 0;
                    }
                    else
                    {
                        f_curr_index = Convert.ToDouble(arr[i, j]);
                        f_prev_index = Convert.ToDouble(arr[i, j - 1]);
                        temp = (f_curr_index - f_prev_index);
                        temp = temp * 12;
                        valueRow["Value"] = temp;
                    }
                    result.Rows.Add(valueRow);
                }
            }
            return result;
        }

        private static DataTable generate_graph_ramping(DataTable dt)
        {
            DataTable result = new DataTable();

            result.Columns.Add("Unit", typeof(int));
            result.Columns.Add("Time", typeof(int));
            result.Columns.Add("Value", typeof(double));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Rows[0].ItemArray.Count(); j++)
                {
                    DataRow valueRow = result.NewRow();
                    valueRow["Value"] = dt.Rows[i][j];
                    valueRow["Unit"] = i;
                    valueRow["Time"] = j;
                    result.Rows.Add(valueRow);
                }
            }
            return result;
        }

        private double[,,] Dataframe_to_3d_array(DataFrame df, int iNumCol1, int iNumCol2, int iNumCol3)
        {

            double[,,] result = new double[iNumCol1, iNumCol2, iNumCol3];

            int firstIndex = 0;
            int secondIndex = 0;
            int thirdIndex = 0;
            double value = 0;

            for (int i = 0; i < df.NumRows; i++)
            {
                firstIndex = Convert.ToInt16(df.GetRowByIndex(i)[0].ToString().Trim(BadChars));
                secondIndex = Convert.ToInt16(df.GetRowByIndex(i)[1].ToString());
                //Console.WriteLine(df.ToArray()[i][2].ToString());
                thirdIndex = Convert.ToInt16(df.GetRowByIndex(i)[2].ToString().Trim(BadChars));
                value = Convert.ToDouble(df.GetRowByIndex(i)[3].ToString());
                result[firstIndex - 1, secondIndex - 1, thirdIndex - 1] = value;
            }
            return result;
        }
        private double[,] Dataframe_to_2d_array(DataFrame df, int iNumCol1, int iNumCol2, Boolean bTransposed)
        {

            double[,] result = new double[iNumCol1, iNumCol2];
            int firstIndex = 0;
            int secondIndex = 0;
            double value = 0;

            for (int i = 0; i < df.NumRows; i++)
            {
                if (bTransposed)
                {
                    firstIndex = Convert.ToInt16(df.GetRowByIndex(i)[0].ToString());
                    secondIndex = Convert.ToInt16(df.GetRowByIndex(i)[1].ToString().Trim(BadChars));
                }
                else
                {
                    firstIndex = Convert.ToInt16(df.GetRowByIndex(i)[1].ToString());
                    secondIndex = Convert.ToInt16(df.GetRowByIndex(i)[0].ToString().Trim(BadChars));
                }

                //Console.WriteLine(df.ToArray()[i][2].ToString());
                value = Convert.ToDouble(df.GetRowByIndex(i)[2].ToString());
                result[firstIndex - 1, secondIndex - 1] = value;
                // Console.WriteLine( "G" + firstIndex.ToString() + ", T" + secondIndex.ToString() + ", H" + thirdIndex.ToString() + " = " + value.ToString() );
            }
            return result;
        }

        public double[,] CT_P_Post_Mortem(double[,,] G_H, int iNumUnits)
        {
            int iTime = 24;
            int count = 0;
            double x = 0;
            double[,] G_cont = new double[iNumUnits, t5min];
            for (int g = 0; g < iNumUnits; g++)
            {
                for (int t = 0; t < iTime; t++)
                {
                    for (int s = 0; s < t5min; s++)
                    {
                        if (((s + 1) > (12 * t)) && ((s + 1) < (12 * (t + 1) + 1)))
                        {
                            count = (s + 1) - (12 * ((t + 1) - 1)) - 1;
                            x = (count * 1.0) / 12;
                            G_cont[g, s] = Math.Round((((2.00 * Math.Pow(x, 3)) - (3.00 * Math.Pow(x, 2)) + 1.00) * G_H[g, t, 0])
                                                + ((Math.Pow(x, 3) - (2.00 * Math.Pow(x, 2)) + x) * G_H[g, t, 1])
                                                + (((-2.00 * Math.Pow(x, 3)) + (3.00 * Math.Pow(x, 2))) * (G_H[g, t, 2]))
                                                + ((Math.Pow(x, 3) - (Math.Pow(x, 2))) * (G_H[g, t, 3]))
                                                , 3);
                        }

                    }
                }
            }
            return G_cont;
        }

        public double[] CT_Price_Post_Mortem(double[,] Load_Balance1_m, double[,] Load_Balance2_m, int iNumUnits)
        {
            int iTime = 24;
            int count = 0;
            double x = 0;

            Load_Balance1_m[0, 0] = Load_Balance1_m[0, 0] * 2;
            Load_Balance2_m[23, 1] = Load_Balance2_m[23, 1] * 2;

            double[] Lamda_cont = new double[t5min];
            for (int g = 0; g < iNumUnits; g++)
            {
                for (int t = 0; t < iTime; t++)
                {
                    for (int s = 0; s < t5min; s++)
                    {
                        if (((s + 1) > (12 * t)) && ((s + 1) < (12 * (t + 1) + 1)))
                        {
                            count = (s + 1) - (12 * ((t + 1) - 1)) - 1;
                            x = (count * 1.0) / 12;
                            if (t < 23)
                            {
                                Lamda_cont[s] = Math.Round((((2 * Math.Pow(x, 3)) - (3 * Math.Pow(x, 2)) + 1) * Load_Balance1_m[t, 0])
                                            + (((Math.Pow(x, 3)) - (2 * Math.Pow(x, 2)) + x) * Load_Balance1_m[t, 1])
                                            + (((-2 * Math.Pow(x, 3)) + (3 * Math.Pow(x, 2))) * Load_Balance1_m[t + 1, 0])
                                            + (((Math.Pow(x, 3)) - (Math.Pow(x, 2))) * Load_Balance1_m[t + 1, 1])
                                            , 3);
                            }
                            else
                            {
                                Lamda_cont[s] = Math.Round((((2 * Math.Pow(x, 3)) - (3 * Math.Pow(x, 2)) + 1) * Load_Balance1_m[t, 0])
                                                    + (((Math.Pow(x, 3)) - (2 * Math.Pow(x, 2)) + x) * Load_Balance1_m[t, 1])
                                                    + (((-2 * Math.Pow(x, 3)) + (3 * Math.Pow(x, 2))) * Load_Balance2_m[t, 2])
                                                    + (((Math.Pow(x, 3)) - (Math.Pow(x, 2))) * Load_Balance2_m[t, 3])
                                                    , 3);
                            }

                        }

                    }
                }
            }
            return Lamda_cont;
        }

  

        //Calculates Charge and Discharge
        public double[,] CT_ES_CH_DIS_Post_Mortem(double[,,] D_H_S, int iNumUnits)
        {
            int iTime = 24;
            double x = 0;
            double[,] ES_cont = new double[iNumUnits, t5min];
            for (int e = 0; e < iNumUnits; e++)
            {
                for (int t = 0; t < iTime; t++)
                {
                    for (int s = 0; s < t5min; s++)
                    {
                        if (((s + 1) > 12 * t) && ((s + 1) < (12 * (t + 1) + 1)))
                        {
                            x = (1.0 * ((s + 1) - (12 * ((t + 1) - 1)) - 1)) / 12;
                            ES_cont[e, s] = Math.Round((((2.00 * Math.Pow(x, 3)) - (3.00 * Math.Pow(x, 2)) + 1.00) * D_H_S[e, t, 0])
                                                + ((Math.Pow(x, 3) - (2.00 * Math.Pow(x, 2)) + x) * D_H_S[e, t, 1])
                                                + (((-2.00 * Math.Pow(x, 3)) + (3.00 * Math.Pow(x, 2))) * (D_H_S[e, t, 2]))
                                                + ((Math.Pow(x, 3) - (Math.Pow(x, 2))) * (D_H_S[e, t, 3]))
                                                , 3);
                        }
                    }
                }
            }
            return ES_cont;
        }

        public double[,] CT_ES_Energy_Post_Mortem(double[,,] E_B_S, int iNumUnits)
        {
            int iTime = 24;
            double x = 0;
            double[,] E_cont = new double[iNumUnits, t5min];
            for (int e = 0; e < iNumUnits; e++)
            {
                for (int t = 0; t < iTime; t++)
                {
                    for (int s = 0; s < t5min; s++)
                    {
                        if (((s + 1) > 12 * t) && ((s + 1) < (12 * (t + 1) + 1)))
                        {
                            x = (1.0 * ((s + 1) - (12 * ((t + 1) - 1)) - 1)) / 12;
                            E_cont[e, s] = Math.Round(
                                            (Math.Pow((1.0 - x), 4) * E_B_S[e, t, 0])
                                            + (4 * x * Math.Pow(1.0 - x, 3) * E_B_S[e, t, 1])
                                            + (6.0 * Math.Pow(x, 2) * Math.Pow(1.0 - x, 2) * E_B_S[e, t, 2])
                                            + (4.0 * Math.Pow(x, 3) * (1 - x) * E_B_S[e, t, 3])
                                            + (Math.Pow(x, 4) * E_B_S[e, t, 4])
                                            , 3);
                        }
                    }
                }
            }
            return E_cont;
        }

        public double[,] CT_ES_Gamma_Post_Mortem(double[,,] integral1_INI_m, double[,,] ES_minE_m, double[,,] ES_maxE_m, int iNumUnits)
        {
            int iTime = 24;
            double x = 0;
            double[,,] Gamma_E = new double[iNumUnits, iTime, 5];
            double[,] ES_Gamma_E_cont = new double[iNumUnits, t5min];
            for (int e = 0; e < iNumUnits; e++)
            {
                for (int t = 0; t < iTime; t++)
                {
                    if ((t + 1) == 1)
                    {
                        for (int bi = 0; bi < 5; bi++)
                        {
                            Gamma_E[e, t, bi] = integral1_INI_m[e, t, 4] + ES_minE_m[e, t, 4] - ES_maxE_m[e, t, 4];
                        }
                    }
                    else if (((t + 1) > 1) && (t < (iTime - 1)))
                    {
                        Gamma_E[e, t, 0] = Gamma_E[e, (t - 1), 4] + ES_minE_m[e, t, 0] + ES_maxE_m[e, t, 0];
                        Gamma_E[e, t, 1] = Gamma_E[e, t, 0] + ES_minE_m[e, t, 1] + ES_maxE_m[e, t, 1];
                        Gamma_E[e, t, 2] = Gamma_E[e, t, 1] + ES_minE_m[e, t, 2] + ES_maxE_m[e, t, 2];
                        Gamma_E[e, t, 3] = Gamma_E[e, t, 2] + ES_minE_m[e, t, 3] + ES_maxE_m[e, t, 3];
                        Gamma_E[e, t, 4] = Gamma_E[e, t, 3] + ES_minE_m[e, t, 4] + ES_maxE_m[e, t, 4];
                    }
                    else if (t == (iTime - 1))
                    {
                        Gamma_E[e, t, 0] = Gamma_E[e, (t - 1), 4] + ES_minE_m[e, t, 0] + ES_maxE_m[e, t, 0];
                        Gamma_E[e, t, 1] = Gamma_E[e, t, 0] + ES_minE_m[e, t, 1] + ES_maxE_m[e, t, 1];
                        Gamma_E[e, t, 2] = Gamma_E[e, t, 1] + ES_minE_m[e, t, 2] + ES_maxE_m[e, t, 2];
                        Gamma_E[e, t, 3] = Gamma_E[e, t, 2] + ES_minE_m[e, t, 3] + ES_maxE_m[e, t, 3];
                        Gamma_E[e, t, 4] = Gamma_E[e, t, 3];
                    }
                    for (int s = 0; s < t5min; s++)
                    {
                        if (((s + 1) > 12 * t) && ((s + 1) < (12 * (t + 1) + 1)))
                        {
                            x = (1.0 * ((s + 1) - (12 * ((t + 1) - 1)) - 1)) / 12;
                            ES_Gamma_E_cont[e, s] = Math.Round(-1.0 * (
                                                (Math.Pow((1.0 - x), 4) * Gamma_E[e, t, 0])
                                                + (4.0 * x * Math.Pow(1.0 - x, 3) * Gamma_E[e, t, 1])
                                                + (6.0 * Math.Pow(x, 2) * Math.Pow(1 - x, 2) * Gamma_E[e, t, 2])
                                                + (4.0 * Math.Pow(x, 3) * (1 - x) * Gamma_E[e, t, 3])
                                                + (Math.Pow(x, 4) * Gamma_E[e, t, 4])
                                                ), 3);

                        }
                    }
                }
            }
            return ES_Gamma_E_cont;

        }

        public DataTable GetInputGenData(string sPath)
        {
            DataTable result;
            using (AMPL data = new AMPL())
            {
                data.Read("D:/CMetis/Metis/models/ParamIndex.mod");
                data.ReadData(sPath);
                int iNumGenUnits = Convert.ToInt16(data.GetSet("GENERATORS").Size.ToString());
                int iNumParams = Convert.ToInt16(data.GetSet("GENPARAMS").Size.ToString());
                result = extract_gen_data(data.GetParameter("Copy_GenData").GetValues(), iNumGenUnits, iNumParams);
            }
            
            return result;
        }

        public DataTable GetInputESData(string sPath)
        {
            DataTable result;
            using (AMPL data = new AMPL())
            {
                data.Read("D:/CMetis/Metis/models/ParamIndex.mod");
                data.ReadData(sPath);
                try
                {
                    int iNumESUnits = Convert.ToInt16(data.GetSet("ENERGY_ST").Size.ToString());
                    int iNumParams = Convert.ToInt16(data.GetSet("ES_PARAMS").Size.ToString());
                    result = extract_ES_data(data.GetParameter("Copy_ES_Data").GetValues(), iNumESUnits, iNumParams);
                }
                catch
                {
                    result = null;
                }
                
            }
            return result;
        }
        private DataTable extract_gen_data(DataFrame df, int iNumUnits, int iNumParams)
        {
            DataTable dt = new DataTable();
            //Pmin Pmax MINCOST STCOST FUEL MUT MDT RU RD SUR SDR INI a b c;

            double[,] array = new double[iNumUnits, iNumParams];

            int firstIndex = 0;
            int secondIndex = 0;
            double value = 0;

            for (int i = 0; i < df.NumRows; i++)
            {
                firstIndex = Convert.ToInt16(df.GetRowByIndex(i)[1].ToString().Trim(BadChars));
                secondIndex = get_index_number(df.GetRowByIndex(i)[0].ToString().Trim('\''));
                value = Convert.ToDouble(df.GetRowByIndex(i)[2].ToString());
                array[firstIndex - 1, secondIndex] = value;
            }

            dt.Columns.Add("Unit", typeof(double));
            dt.Columns.Add("Min Generation (MW)", typeof(double));
            dt.Columns.Add("Max Generation (MW)", typeof(double));
            dt.Columns.Add("Min Generation Cost ($)", typeof(double));
            dt.Columns.Add("Startup Cost ($)", typeof(double));
            dt.Columns.Add("Min Up Time (h)", typeof(double));
            dt.Columns.Add("Min Down Time (h)", typeof(double));
            dt.Columns.Add("Ramp Up (MW/min)", typeof(double));
            dt.Columns.Add("Ramp Down (MW/min)", typeof(double));
            dt.Columns.Add("Startup Ramp (MW/min)", typeof(double));
            dt.Columns.Add("Shutdown Ramp (MW/min)", typeof(double));
            dt.Columns.Add("INI", typeof(double));
            dt.Columns.Add("a", typeof(double));
            dt.Columns.Add("b", typeof(double));
            dt.Columns.Add("c", typeof(double));

            for (int i = 0; i < array.GetLength(0); i++)
            {
                DataRow valueRow = dt.NewRow();
                
                valueRow["Unit"] = i + 1;
                valueRow["Min Generation (MW)"] = Convert.ToInt16(array[i, 1]);
                valueRow["Max Generation (MW)"] = Convert.ToInt16(array[i, 2]);
                valueRow["Min Generation Cost ($)"] = Convert.ToInt16(array[i, 3]);
                valueRow["Startup Cost ($)"] = Convert.ToInt16(array[i, 4]);
                valueRow["Min Up Time (h)"] = Convert.ToInt16(array[i, 5]);
                valueRow["Min Down Time (h)"] = Convert.ToInt16(array[i, 6]);
                valueRow["Ramp Up (MW/min)"] = Convert.ToInt16(array[i, 7]);
                valueRow["Ramp Down (MW/min)"] = Convert.ToInt16(array[i, 8]);
                valueRow["Startup Ramp (MW/min)"] = Convert.ToInt16(array[i, 9]);
                valueRow["Shutdown Ramp (MW/min)"] = Convert.ToInt16(array[i, 10]);
                valueRow["INI"] = Convert.ToInt16(array[i, 7]);
                valueRow["a"] = Convert.ToInt16(array[i, 8]);
                valueRow["b"] = Convert.ToInt16(array[i, 9]);
                valueRow["c"] = Convert.ToInt16(array[i, 10]);

                dt.Rows.Add(valueRow);
            }
            return dt;
        }
        private DataTable extract_ES_data(DataFrame df, int iNumUnits, int iNumParams)
        {
            DataTable dt = new DataTable();
            //Pmax_ch Pmax_dis RU_S RD_S Emax Emin E0 eta_ch eta_dis b_dis c_dis b_ch c_ch;

            double[,] array = new double[iNumUnits, iNumParams];

            int firstIndex = 0;
            int secondIndex = 0;
            double value = 0;

            for (int i = 0; i < df.NumRows; i++)
            {
                firstIndex = Convert.ToInt16(df.GetRowByIndex(i)[0].ToString());
                secondIndex = get_index_number(df.GetRowByIndex(i)[1].ToString().Trim('\''));
                value = Convert.ToDouble(df.GetRowByIndex(i)[2].ToString());
                array[firstIndex - 1, secondIndex] = value;
            }

            dt.Columns.Add("Unit", typeof(double));
            dt.Columns.Add("Charging Power (MW)", typeof(double));
            dt.Columns.Add("Discharging Power (MW)", typeof(double));
            dt.Columns.Add("Max Energy (MWh)", typeof(double));
            dt.Columns.Add("Min Energy (MWh)", typeof(double));
            dt.Columns.Add("Initial Energy", typeof(double));
            dt.Columns.Add("Ramp Up (MW/min)", typeof(double));
            dt.Columns.Add("Ramp Down (MW/min)", typeof(double));
            dt.Columns.Add("Charging Efficiency", typeof(double));
            dt.Columns.Add("Discharging Efficiency", typeof(double));


            for (int i = 0; i < array.GetLength(0); i++)
            {
                DataRow valueRow = dt.NewRow();

                valueRow["Unit"] = i + 1;
                valueRow["Charging Power (MW)"] = Convert.ToInt16(array[i, 0]);
                valueRow["Discharging Power (MW)"] = Convert.ToInt16(array[i, 1]);
                valueRow["Ramp Up (MW/min)"] = Convert.ToInt16(array[i, 2]);
                valueRow["Ramp Down (MW/min)"] = Convert.ToInt16(array[i, 3]);
                valueRow["Max Energy (MWh)"] = Convert.ToInt16(array[i, 4]);
                valueRow["Min Energy (MWh)"] = Convert.ToInt16(array[i, 5]);
                valueRow["Initial Energy"] = Convert.ToInt16(array[i, 6]);
                valueRow["Charging Efficiency"] = Convert.ToInt16(array[i, 7]);
                valueRow["Discharging Efficiency"] = Convert.ToInt16(array[i, 8]);

                dt.Rows.Add(valueRow);
            }
            return dt;
        }
        private static int get_index_number(string s)
        {
            int result = 1000;
            //Pmin Pmax MINCOST STCOST FUEL MUT MDT RU RD SUR SDR INI a b c;
            //Pmax_ch Pmax_dis RU_S RD_S Emax Emin E0 eta_ch eta_dis b_dis c_dis b_ch c_ch;
            if (s == "Pmin" || s == "Pmax_ch")
            {
                result = 0;
            }
            else if (s == "Pmax" || s == "Pmax_dis")
            {
                result = 1;
            }
            else if (s == "MINCOST" || s == "RU_S")
            {
                result = 2;
            }
            else if (s == "STCOST" || s == "RD_S")
            {
                result = 3;
            }
            else if (s == "FUEL" || s == "Emax")
            {
                result = 4;
            }
            else if (s == "MUT" || s == "Emin")
            {
                result = 5;
            }
            else if (s == "MDT" || s == "E0")
            {
                result = 6;
            }
            else if (s == "RU" || s == "eta_ch")
            {
                result = 7;
            }
            else if (s == "RD" || s == "eta_dis")
            {
                result = 8;
            }
            else if (s == "SUR" || s == "b_dis")
            {
                result = 9;
            }
            else if (s == "SDR" || s == "c_dis")
            {
                result = 10;
            }
            else if (s == "INI" || s == "b_ch")
            {
                result = 11;
            }
            else if (s == "a" || s == "c_ch")
            {
                result = 12;
            }
            else if (s == "b")
            {
                result = 13;
            }
            else if (s == "c")
            {
                result = 14;
            }
            else
            {
                result = 99;
            }
            return result;
        }
    }
}
