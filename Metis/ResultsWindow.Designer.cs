namespace Metis
{
    partial class ResultsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Power Generation (MW)");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Generation Ramping");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("I");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Generation", new System.Windows.Forms.TreeNode[] {
            treeNode17,
            treeNode18,
            treeNode19});
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Electricity Prices ($/MW)");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Total Cost");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Charge Power");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("Charge Ramping");
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Charge", new System.Windows.Forms.TreeNode[] {
            treeNode23,
            treeNode24});
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Discharge Power");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Discharge Ramping");
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Discharge", new System.Windows.Forms.TreeNode[] {
            treeNode26,
            treeNode27});
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("NISSE");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("Energy");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Energy Storage", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode28,
            treeNode29,
            treeNode30});
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("Results", new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode31});
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnResultsTree = new System.Windows.Forms.Panel();
            this.twCategoriesTree = new System.Windows.Forms.TreeView();
            this.btnGraphView = new System.Windows.Forms.Button();
            this.btnTableView = new System.Windows.Forms.Button();
            this.lbUnitSelect = new System.Windows.Forms.Label();
            this.cbGraphUnit = new System.Windows.Forms.ComboBox();
            this.lbValue1 = new System.Windows.Forms.Label();
            this.pnResultsData = new System.Windows.Forms.Panel();
            this.ResultsTable = new System.Windows.Forms.DataGridView();
            this.chGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnResultsTree.SuspendLayout();
            this.pnResultsData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // pnResultsTree
            // 
            this.pnResultsTree.Controls.Add(this.twCategoriesTree);
            this.pnResultsTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnResultsTree.Location = new System.Drawing.Point(0, 0);
            this.pnResultsTree.Name = "pnResultsTree";
            this.pnResultsTree.Size = new System.Drawing.Size(272, 746);
            this.pnResultsTree.TabIndex = 0;
            // 
            // twCategoriesTree
            // 
            this.twCategoriesTree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.twCategoriesTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.twCategoriesTree.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.twCategoriesTree.ForeColor = System.Drawing.Color.Turquoise;
            this.twCategoriesTree.HideSelection = false;
            this.twCategoriesTree.Location = new System.Drawing.Point(12, 12);
            this.twCategoriesTree.Name = "twCategoriesTree";
            treeNode17.Name = "ndPGen";
            treeNode17.Text = "Power Generation (MW)";
            treeNode18.Name = "ndPRamp";
            treeNode18.Text = "Generation Ramping";
            treeNode19.Name = "ndI";
            treeNode19.Text = "I";
            treeNode20.Name = "tvGeneration";
            treeNode20.Text = "Generation";
            treeNode21.Name = "ndElectrictyPrices";
            treeNode21.Text = "Electricity Prices ($/MW)";
            treeNode22.Name = "ndTotalCost";
            treeNode22.Text = "Total Cost";
            treeNode23.Name = "ndCHP";
            treeNode23.Text = "Charge Power";
            treeNode24.Name = "ndCHR";
            treeNode24.Text = "Charge Ramping";
            treeNode25.Name = "ndCharge";
            treeNode25.Text = "Charge";
            treeNode26.Name = "ndDisP";
            treeNode26.Text = "Discharge Power";
            treeNode27.Name = "ndDisR";
            treeNode27.Text = "Discharge Ramping";
            treeNode28.Name = "ndDisch";
            treeNode28.Text = "Discharge";
            treeNode29.Name = "ndNisse";
            treeNode29.Text = "NISSE";
            treeNode30.Name = "ndEnergy";
            treeNode30.Text = "Energy";
            treeNode31.Name = "ndES";
            treeNode31.Text = "Energy Storage";
            treeNode32.Name = "tvResults";
            treeNode32.Text = "Results";
            this.twCategoriesTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode32});
            this.twCategoriesTree.ShowPlusMinus = false;
            this.twCategoriesTree.Size = new System.Drawing.Size(254, 652);
            this.twCategoriesTree.TabIndex = 0;
            this.twCategoriesTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.twCategoriesTree_AfterSelect);
            // 
            // btnGraphView
            // 
            this.btnGraphView.FlatAppearance.BorderSize = 0;
            this.btnGraphView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGraphView.Image = global::Metis.Properties.Resources.graph24;
            this.btnGraphView.Location = new System.Drawing.Point(361, 27);
            this.btnGraphView.Name = "btnGraphView";
            this.btnGraphView.Size = new System.Drawing.Size(39, 38);
            this.btnGraphView.TabIndex = 12;
            this.btnGraphView.UseVisualStyleBackColor = true;
            this.btnGraphView.Click += new System.EventHandler(this.btnGraphView_Click);
            // 
            // btnTableView
            // 
            this.btnTableView.FlatAppearance.BorderColor = System.Drawing.Color.Turquoise;
            this.btnTableView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTableView.Image = global::Metis.Properties.Resources.table24;
            this.btnTableView.Location = new System.Drawing.Point(304, 27);
            this.btnTableView.Name = "btnTableView";
            this.btnTableView.Size = new System.Drawing.Size(39, 38);
            this.btnTableView.TabIndex = 11;
            this.btnTableView.UseVisualStyleBackColor = true;
            this.btnTableView.Click += new System.EventHandler(this.btnTableView_Click);
            // 
            // lbUnitSelect
            // 
            this.lbUnitSelect.AutoSize = true;
            this.lbUnitSelect.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUnitSelect.ForeColor = System.Drawing.Color.Turquoise;
            this.lbUnitSelect.Location = new System.Drawing.Point(449, 35);
            this.lbUnitSelect.Name = "lbUnitSelect";
            this.lbUnitSelect.Size = new System.Drawing.Size(91, 19);
            this.lbUnitSelect.TabIndex = 14;
            this.lbUnitSelect.Text = "Select Unit:";
            // 
            // cbGraphUnit
            // 
            this.cbGraphUnit.BackColor = System.Drawing.SystemColors.Window;
            this.cbGraphUnit.DropDownWidth = 106;
            this.cbGraphUnit.FormattingEnabled = true;
            this.cbGraphUnit.Location = new System.Drawing.Point(567, 37);
            this.cbGraphUnit.Name = "cbGraphUnit";
            this.cbGraphUnit.Size = new System.Drawing.Size(107, 21);
            this.cbGraphUnit.TabIndex = 13;
            this.cbGraphUnit.SelectedIndexChanged += new System.EventHandler(this.cbGraphUnit_SelectedIndexChanged);
            // 
            // lbValue1
            // 
            this.lbValue1.AutoSize = true;
            this.lbValue1.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbValue1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbValue1.Location = new System.Drawing.Point(64, 69);
            this.lbValue1.Name = "lbValue1";
            this.lbValue1.Size = new System.Drawing.Size(170, 56);
            this.lbValue1.TabIndex = 0;
            this.lbValue1.Text = "label1";
            // 
            // pnResultsData
            // 
            this.pnResultsData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnResultsData.Controls.Add(this.ResultsTable);
            this.pnResultsData.Controls.Add(this.chGraph);
            this.pnResultsData.Controls.Add(this.lbValue1);
            this.pnResultsData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnResultsData.Location = new System.Drawing.Point(272, 99);
            this.pnResultsData.Name = "pnResultsData";
            this.pnResultsData.Size = new System.Drawing.Size(676, 647);
            this.pnResultsData.TabIndex = 1;
            // 
            // ResultsTable
            // 
            this.ResultsTable.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.ResultsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultsTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultsTable.Location = new System.Drawing.Point(0, 0);
            this.ResultsTable.Name = "ResultsTable";
            this.ResultsTable.Size = new System.Drawing.Size(676, 2);
            this.ResultsTable.TabIndex = 0;
            // 
            // chGraph
            // 
            this.chGraph.BackImageTransparentColor = System.Drawing.Color.White;
            this.chGraph.BackSecondaryColor = System.Drawing.Color.White;
            this.chGraph.BorderlineColor = System.Drawing.Color.Black;
            this.chGraph.BorderlineWidth = 5;
            this.chGraph.BorderSkin.PageColor = System.Drawing.SystemColors.WindowText;
            chartArea2.AxisX.InterlacedColor = System.Drawing.Color.Black;
            chartArea2.AxisX2.InterlacedColor = System.Drawing.Color.Black;
            chartArea2.AxisY.InterlacedColor = System.Drawing.Color.Black;
            chartArea2.AxisY.ScrollBar.LineColor = System.Drawing.Color.Black;
            chartArea2.AxisY2.InterlacedColor = System.Drawing.Color.Black;
            chartArea2.BackColor = System.Drawing.Color.White;
            chartArea2.BackImageTransparentColor = System.Drawing.Color.Black;
            chartArea2.BackSecondaryColor = System.Drawing.Color.Black;
            chartArea2.CursorX.SelectionColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea1";
            chartArea2.ShadowColor = System.Drawing.Color.Black;
            this.chGraph.ChartAreas.Add(chartArea2);
            this.chGraph.Dock = System.Windows.Forms.DockStyle.Bottom;
            legend2.BackColor = System.Drawing.Color.Black;
            legend2.BackImageTransparentColor = System.Drawing.Color.White;
            legend2.BorderColor = System.Drawing.Color.Black;
            legend2.Enabled = false;
            legend2.InterlacedRowsColor = System.Drawing.Color.Black;
            legend2.Name = "Legend1";
            legend2.ShadowColor = System.Drawing.Color.Black;
            this.chGraph.Legends.Add(legend2);
            this.chGraph.Location = new System.Drawing.Point(0, 2);
            this.chGraph.Name = "chGraph";
            this.chGraph.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            series2.BackImageTransparentColor = System.Drawing.Color.White;
            series2.BackSecondaryColor = System.Drawing.Color.Black;
            series2.BorderColor = System.Drawing.Color.Black;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.LabelBackColor = System.Drawing.Color.Black;
            series2.LabelBorderColor = System.Drawing.Color.Black;
            series2.LabelBorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series2.LabelBorderWidth = 3;
            series2.Legend = "Legend1";
            series2.MarkerBorderColor = System.Drawing.Color.Transparent;
            series2.MarkerBorderWidth = 2;
            series2.MarkerImageTransparentColor = System.Drawing.Color.Black;
            series2.Name = "Series1";
            this.chGraph.Series.Add(series2);
            this.chGraph.Size = new System.Drawing.Size(676, 645);
            this.chGraph.TabIndex = 1;
            this.chGraph.Text = "chart1";
            // 
            // ResultsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(948, 746);
            this.Controls.Add(this.lbUnitSelect);
            this.Controls.Add(this.cbGraphUnit);
            this.Controls.Add(this.btnGraphView);
            this.Controls.Add(this.btnTableView);
            this.Controls.Add(this.pnResultsData);
            this.Controls.Add(this.pnResultsTree);
            this.MinimumSize = new System.Drawing.Size(872, 728);
            this.Name = "ResultsWindow";
            this.Text = "Results";
            this.TransparencyKey = System.Drawing.Color.Transparent;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ResultsWindow_FormClosed);
            this.pnResultsTree.ResumeLayout(false);
            this.pnResultsData.ResumeLayout(false);
            this.pnResultsData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ResultsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnResultsTree;
        private System.Windows.Forms.TreeView twCategoriesTree;
        private System.Windows.Forms.Button btnTableView;
        private System.Windows.Forms.Button btnGraphView;
        private System.Windows.Forms.Label lbUnitSelect;
        private System.Windows.Forms.ComboBox cbGraphUnit;
        private System.Windows.Forms.Label lbValue1;
        private System.Windows.Forms.DataGridView ResultsTable;
        private System.Windows.Forms.Panel pnResultsData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chGraph;
    }
}