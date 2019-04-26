namespace Metis
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnSideBar = new System.Windows.Forms.Panel();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnES = new System.Windows.Forms.Button();
            this.lbCopyright2 = new System.Windows.Forms.Label();
            this.lbCopyright1 = new System.Windows.Forms.Label();
            this.btnGenUnits = new System.Windows.Forms.Button();
            this.pnLogo = new System.Windows.Forms.Panel();
            this.lbLogo = new System.Windows.Forms.Label();
            this.pnData = new System.Windows.Forms.Panel();
            this.tGenUnits = new System.Windows.Forms.DataGridView();
            this.colUnitNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinGen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxGen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinGenCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinUpTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MinDownTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RampUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RampDown = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartupRamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShutdownRamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenQuantityA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenPriceA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenQuantityB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenPriceB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenQuantityC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GenPriceC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tEnergyUnits = new System.Windows.Forms.DataGridView();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESMaxE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESMinE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InitE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESRampUp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESRampD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChEff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisEff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChQtA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChPriceA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChQtB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChPriceB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisQtA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisPriceA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisQtB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DisPriceB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tLoadData = new System.Windows.Forms.DataGridView();
            this.T1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbModel = new System.Windows.Forms.ComboBox();
            this.lbModelSelect = new System.Windows.Forms.Label();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.pnSideBar.SuspendLayout();
            this.pnLogo.SuspendLayout();
            this.pnData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tGenUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tEnergyUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tLoadData)).BeginInit();
            this.SuspendLayout();
            // 
            // pnSideBar
            // 
            this.pnSideBar.Controls.Add(this.btnLoadData);
            this.pnSideBar.Controls.Add(this.btnES);
            this.pnSideBar.Controls.Add(this.lbCopyright2);
            this.pnSideBar.Controls.Add(this.lbCopyright1);
            this.pnSideBar.Controls.Add(this.btnGenUnits);
            this.pnSideBar.Controls.Add(this.pnLogo);
            this.pnSideBar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnSideBar.Location = new System.Drawing.Point(0, 0);
            this.pnSideBar.Name = "pnSideBar";
            this.pnSideBar.Size = new System.Drawing.Size(200, 652);
            this.pnSideBar.TabIndex = 0;
            // 
            // btnLoadData
            // 
            this.btnLoadData.FlatAppearance.BorderColor = System.Drawing.Color.Turquoise;
            this.btnLoadData.FlatAppearance.BorderSize = 0;
            this.btnLoadData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadData.ForeColor = System.Drawing.Color.Turquoise;
            this.btnLoadData.Image = global::Metis.Properties.Resources.home48;
            this.btnLoadData.Location = new System.Drawing.Point(3, 440);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(197, 96);
            this.btnLoadData.TabIndex = 7;
            this.btnLoadData.Text = "Load";
            this.btnLoadData.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnES
            // 
            this.btnES.FlatAppearance.BorderColor = System.Drawing.Color.Turquoise;
            this.btnES.FlatAppearance.BorderSize = 0;
            this.btnES.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnES.ForeColor = System.Drawing.Color.Turquoise;
            this.btnES.Image = global::Metis.Properties.Resources.battery48;
            this.btnES.Location = new System.Drawing.Point(3, 316);
            this.btnES.Name = "btnES";
            this.btnES.Size = new System.Drawing.Size(197, 96);
            this.btnES.TabIndex = 6;
            this.btnES.Text = "Energy Storage";
            this.btnES.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnES.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnES.UseVisualStyleBackColor = true;
            this.btnES.Click += new System.EventHandler(this.btnES_Click);
            // 
            // lbCopyright2
            // 
            this.lbCopyright2.AutoSize = true;
            this.lbCopyright2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCopyright2.ForeColor = System.Drawing.Color.Turquoise;
            this.lbCopyright2.Location = new System.Drawing.Point(47, 627);
            this.lbCopyright2.Name = "lbCopyright2";
            this.lbCopyright2.Size = new System.Drawing.Size(99, 16);
            this.lbCopyright2.TabIndex = 5;
            this.lbCopyright2.Text = "All rights reserved";
            // 
            // lbCopyright1
            // 
            this.lbCopyright1.AutoSize = true;
            this.lbCopyright1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCopyright1.ForeColor = System.Drawing.Color.Turquoise;
            this.lbCopyright1.Location = new System.Drawing.Point(3, 611);
            this.lbCopyright1.Name = "lbCopyright1";
            this.lbCopyright1.Size = new System.Drawing.Size(202, 16);
            this.lbCopyright1.TabIndex = 4;
            this.lbCopyright1.Text = "Copyright © 2019 University of Utah. ";
            // 
            // btnGenUnits
            // 
            this.btnGenUnits.FlatAppearance.BorderColor = System.Drawing.Color.Turquoise;
            this.btnGenUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenUnits.ForeColor = System.Drawing.Color.Turquoise;
            this.btnGenUnits.Image = ((System.Drawing.Image)(resources.GetObject("btnGenUnits.Image")));
            this.btnGenUnits.Location = new System.Drawing.Point(3, 189);
            this.btnGenUnits.Name = "btnGenUnits";
            this.btnGenUnits.Size = new System.Drawing.Size(197, 96);
            this.btnGenUnits.TabIndex = 1;
            this.btnGenUnits.Text = "Generating Units";
            this.btnGenUnits.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGenUnits.UseVisualStyleBackColor = true;
            this.btnGenUnits.Click += new System.EventHandler(this.btnGenUnits_Click);
            // 
            // pnLogo
            // 
            this.pnLogo.BackColor = System.Drawing.Color.DarkCyan;
            this.pnLogo.Controls.Add(this.lbLogo);
            this.pnLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnLogo.ForeColor = System.Drawing.Color.White;
            this.pnLogo.Location = new System.Drawing.Point(0, 0);
            this.pnLogo.Name = "pnLogo";
            this.pnLogo.Size = new System.Drawing.Size(200, 100);
            this.pnLogo.TabIndex = 0;
            // 
            // lbLogo
            // 
            this.lbLogo.AutoSize = true;
            this.lbLogo.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLogo.Location = new System.Drawing.Point(22, 20);
            this.lbLogo.Name = "lbLogo";
            this.lbLogo.Size = new System.Drawing.Size(152, 58);
            this.lbLogo.TabIndex = 0;
            this.lbLogo.Text = "METIS";
            // 
            // pnData
            // 
            this.pnData.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnData.AutoSize = true;
            this.pnData.Controls.Add(this.tGenUnits);
            this.pnData.Controls.Add(this.tEnergyUnits);
            this.pnData.Controls.Add(this.tLoadData);
            this.pnData.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pnData.Location = new System.Drawing.Point(200, 106);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(936, 546);
            this.pnData.TabIndex = 1;
            // 
            // tGenUnits
            // 
            this.tGenUnits.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.tGenUnits.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.tGenUnits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Turquoise;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tGenUnits.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tGenUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tGenUnits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUnitNum,
            this.MinGen,
            this.MaxGen,
            this.MinGenCost,
            this.StartCost,
            this.MinUpTime,
            this.MinDownTime,
            this.RampUp,
            this.RampDown,
            this.StartupRamp,
            this.ShutdownRamp,
            this.GenQuantityA,
            this.GenPriceA,
            this.GenQuantityB,
            this.GenPriceB,
            this.GenQuantityC,
            this.GenPriceC});
            this.tGenUnits.Dock = System.Windows.Forms.DockStyle.Left;
            this.tGenUnits.Location = new System.Drawing.Point(0, 0);
            this.tGenUnits.Name = "tGenUnits";
            this.tGenUnits.Size = new System.Drawing.Size(930, 546);
            this.tGenUnits.TabIndex = 0;
            // 
            // colUnitNum
            // 
            this.colUnitNum.HeaderText = "Unit";
            this.colUnitNum.Name = "colUnitNum";
            this.colUnitNum.Width = 54;
            // 
            // MinGen
            // 
            this.MinGen.HeaderText = "Min Generation (MW)";
            this.MinGen.Name = "MinGen";
            this.MinGen.ReadOnly = true;
            this.MinGen.Width = 138;
            // 
            // MaxGen
            // 
            this.MaxGen.HeaderText = "Max Generation (MW)";
            this.MaxGen.Name = "MaxGen";
            this.MaxGen.ReadOnly = true;
            this.MaxGen.Width = 142;
            // 
            // MinGenCost
            // 
            this.MinGenCost.HeaderText = "Min Generation Cost ($)";
            this.MinGenCost.Name = "MinGenCost";
            this.MinGenCost.ReadOnly = true;
            this.MinGenCost.Width = 135;
            // 
            // StartCost
            // 
            this.StartCost.HeaderText = "Startup Cost ($)";
            this.StartCost.Name = "StartCost";
            this.StartCost.ReadOnly = true;
            this.StartCost.Width = 95;
            // 
            // MinUpTime
            // 
            this.MinUpTime.HeaderText = "Min Up Time (h)";
            this.MinUpTime.Name = "MinUpTime";
            this.MinUpTime.ReadOnly = true;
            this.MinUpTime.Width = 94;
            // 
            // MinDownTime
            // 
            this.MinDownTime.HeaderText = "Min Down Time (h)";
            this.MinDownTime.Name = "MinDownTime";
            this.MinDownTime.ReadOnly = true;
            this.MinDownTime.Width = 108;
            // 
            // RampUp
            // 
            this.RampUp.HeaderText = "Ramp Up (MW/min)";
            this.RampUp.Name = "RampUp";
            this.RampUp.ReadOnly = true;
            this.RampUp.Width = 130;
            // 
            // RampDown
            // 
            this.RampDown.HeaderText = "Ramp Down (MW/min)";
            this.RampDown.Name = "RampDown";
            this.RampDown.ReadOnly = true;
            this.RampDown.Width = 144;
            // 
            // StartupRamp
            // 
            this.StartupRamp.HeaderText = "Startup Ramp (MW/min)";
            this.StartupRamp.Name = "StartupRamp";
            this.StartupRamp.ReadOnly = true;
            this.StartupRamp.Width = 151;
            // 
            // ShutdownRamp
            // 
            this.ShutdownRamp.HeaderText = "Shutdown Ramp (MW/min)";
            this.ShutdownRamp.Name = "ShutdownRamp";
            this.ShutdownRamp.ReadOnly = true;
            this.ShutdownRamp.Width = 166;
            // 
            // GenQuantityA
            // 
            this.GenQuantityA.HeaderText = "Quantity A (MW)";
            this.GenQuantityA.Name = "GenQuantityA";
            this.GenQuantityA.ReadOnly = true;
            this.GenQuantityA.Width = 87;
            // 
            // GenPriceA
            // 
            this.GenPriceA.HeaderText = "Price A ($/MW)";
            this.GenPriceA.Name = "GenPriceA";
            this.GenPriceA.ReadOnly = true;
            this.GenPriceA.Width = 104;
            // 
            // GenQuantityB
            // 
            this.GenQuantityB.HeaderText = "Quantity B (MW)";
            this.GenQuantityB.Name = "GenQuantityB";
            this.GenQuantityB.ReadOnly = true;
            this.GenQuantityB.Width = 86;
            // 
            // GenPriceB
            // 
            this.GenPriceB.HeaderText = "Price B ($/MW)";
            this.GenPriceB.Name = "GenPriceB";
            this.GenPriceB.ReadOnly = true;
            this.GenPriceB.Width = 103;
            // 
            // GenQuantityC
            // 
            this.GenQuantityC.HeaderText = "Quantity C (MW)";
            this.GenQuantityC.Name = "GenQuantityC";
            this.GenQuantityC.ReadOnly = true;
            this.GenQuantityC.Width = 88;
            // 
            // GenPriceC
            // 
            this.GenPriceC.HeaderText = "Price C ($/MW)";
            this.GenPriceC.Name = "GenPriceC";
            this.GenPriceC.ReadOnly = true;
            this.GenPriceC.Width = 105;
            // 
            // tEnergyUnits
            // 
            this.tEnergyUnits.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.tEnergyUnits.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tEnergyUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tEnergyUnits.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colUnit,
            this.ChP,
            this.DisP,
            this.ESMaxE,
            this.ESMinE,
            this.InitE,
            this.ESRampUp,
            this.ESRampD,
            this.ChEff,
            this.DisEff,
            this.ChQtA,
            this.ChPriceA,
            this.ChQtB,
            this.ChPriceB,
            this.DisQtA,
            this.DisPriceA,
            this.DisQtB,
            this.DisPriceB});
            this.tEnergyUnits.Location = new System.Drawing.Point(3, 0);
            this.tEnergyUnits.Name = "tEnergyUnits";
            this.tEnergyUnits.Size = new System.Drawing.Size(930, 540);
            this.tEnergyUnits.TabIndex = 1;
            this.tEnergyUnits.Visible = false;
            // 
            // colUnit
            // 
            this.colUnit.HeaderText = "Unit";
            this.colUnit.Name = "colUnit";
            // 
            // ChP
            // 
            this.ChP.HeaderText = "Charging Power (MW)";
            this.ChP.Name = "ChP";
            this.ChP.ReadOnly = true;
            // 
            // DisP
            // 
            this.DisP.HeaderText = "Discharging Power (MW)";
            this.DisP.Name = "DisP";
            this.DisP.ReadOnly = true;
            // 
            // ESMaxE
            // 
            this.ESMaxE.HeaderText = "Max Energy (MWh)";
            this.ESMaxE.Name = "ESMaxE";
            this.ESMaxE.ReadOnly = true;
            // 
            // ESMinE
            // 
            this.ESMinE.HeaderText = "Min Energy (MWh)";
            this.ESMinE.Name = "ESMinE";
            this.ESMinE.ReadOnly = true;
            // 
            // InitE
            // 
            this.InitE.HeaderText = "Initial Energy";
            this.InitE.Name = "InitE";
            this.InitE.ReadOnly = true;
            // 
            // ESRampUp
            // 
            this.ESRampUp.HeaderText = "Ramp Up (MW/min)";
            this.ESRampUp.Name = "ESRampUp";
            this.ESRampUp.ReadOnly = true;
            // 
            // ESRampD
            // 
            this.ESRampD.HeaderText = "Ramp Down (MW/min)";
            this.ESRampD.Name = "ESRampD";
            this.ESRampD.ReadOnly = true;
            // 
            // ChEff
            // 
            this.ChEff.HeaderText = "Charging Efficiency";
            this.ChEff.Name = "ChEff";
            this.ChEff.ReadOnly = true;
            // 
            // DisEff
            // 
            this.DisEff.HeaderText = "Discharging Efficiency";
            this.DisEff.Name = "DisEff";
            this.DisEff.ReadOnly = true;
            // 
            // ChQtA
            // 
            this.ChQtA.HeaderText = "Charging - Quantity A (MW)";
            this.ChQtA.Name = "ChQtA";
            this.ChQtA.ReadOnly = true;
            // 
            // ChPriceA
            // 
            this.ChPriceA.HeaderText = "Charging - Price A ($/MW)";
            this.ChPriceA.Name = "ChPriceA";
            this.ChPriceA.ReadOnly = true;
            // 
            // ChQtB
            // 
            this.ChQtB.HeaderText = "Charging - Quantity B (MW)";
            this.ChQtB.Name = "ChQtB";
            this.ChQtB.ReadOnly = true;
            // 
            // ChPriceB
            // 
            this.ChPriceB.HeaderText = "Charging - Price B ($/MW)";
            this.ChPriceB.Name = "ChPriceB";
            this.ChPriceB.ReadOnly = true;
            // 
            // DisQtA
            // 
            this.DisQtA.HeaderText = "Discharging - Quantity A (MW)";
            this.DisQtA.Name = "DisQtA";
            this.DisQtA.ReadOnly = true;
            // 
            // DisPriceA
            // 
            this.DisPriceA.HeaderText = "Discharging - Price A ($/MW)";
            this.DisPriceA.Name = "DisPriceA";
            this.DisPriceA.ReadOnly = true;
            // 
            // DisQtB
            // 
            this.DisQtB.HeaderText = "Discharging - Quantity B";
            this.DisQtB.Name = "DisQtB";
            // 
            // DisPriceB
            // 
            this.DisPriceB.HeaderText = "Discharging - Price B";
            this.DisPriceB.Name = "DisPriceB";
            // 
            // tLoadData
            // 
            this.tLoadData.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.tLoadData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tLoadData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tLoadData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.T1});
            this.tLoadData.Location = new System.Drawing.Point(3, 0);
            this.tLoadData.Name = "tLoadData";
            this.tLoadData.Size = new System.Drawing.Size(930, 540);
            this.tLoadData.TabIndex = 2;
            this.tLoadData.Visible = false;
            // 
            // T1
            // 
            this.T1.HeaderText = "1";
            this.T1.Name = "T1";
            // 
            // cbModel
            // 
            this.cbModel.DropDownWidth = 600;
            this.cbModel.FormattingEnabled = true;
            this.cbModel.Items.AddRange(new object[] {
            "Discrete Time Unit Commitment",
            "Continuous Time Unit Commitment",
            "Continuous Time Unit Commitment with Market Based Energy Storage",
            "Continuous Time Unit Commitment with Non-market Based Energy Storage"});
            this.cbModel.Location = new System.Drawing.Point(379, 59);
            this.cbModel.Name = "cbModel";
            this.cbModel.Size = new System.Drawing.Size(314, 29);
            this.cbModel.TabIndex = 4;
            this.cbModel.SelectedIndexChanged += new System.EventHandler(this.cbModel_SelectedIndexChanged);
            // 
            // lbModelSelect
            // 
            this.lbModelSelect.AutoSize = true;
            this.lbModelSelect.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbModelSelect.Location = new System.Drawing.Point(248, 64);
            this.lbModelSelect.Name = "lbModelSelect";
            this.lbModelSelect.Size = new System.Drawing.Size(112, 19);
            this.lbModelSelect.TabIndex = 10;
            this.lbModelSelect.Text = "Select Model:";
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Image = global::Metis.Properties.Resources.remove24;
            this.btnRemove.Location = new System.Drawing.Point(843, 53);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(39, 38);
            this.btnRemove.TabIndex = 8;
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Image = global::Metis.Properties.Resources.add24;
            this.btnAdd.Location = new System.Drawing.Point(798, 50);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(39, 38);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.FlatAppearance.BorderSize = 0;
            this.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoad.Image = global::Metis.Properties.Resources.Load24;
            this.btnLoad.Location = new System.Drawing.Point(753, 50);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(39, 38);
            this.btnLoad.TabIndex = 6;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnRun
            // 
            this.btnRun.FlatAppearance.BorderSize = 0;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Image = global::Metis.Properties.Resources.run24;
            this.btnRun.Location = new System.Drawing.Point(708, 51);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(39, 38);
            this.btnRun.TabIndex = 5;
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(43)))), ((int)(((byte)(61)))));
            this.ClientSize = new System.Drawing.Size(1136, 652);
            this.Controls.Add(this.lbModelSelect);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.cbModel);
            this.Controls.Add(this.pnData);
            this.Controls.Add(this.pnSideBar);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Turquoise;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MainWindow";
            this.Text = "Main Window";
            this.pnSideBar.ResumeLayout(false);
            this.pnSideBar.PerformLayout();
            this.pnLogo.ResumeLayout(false);
            this.pnLogo.PerformLayout();
            this.pnData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tGenUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tEnergyUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tLoadData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnSideBar;
        private System.Windows.Forms.Label lbCopyright2;
        private System.Windows.Forms.Label lbCopyright1;
        private System.Windows.Forms.Button btnGenUnits;
        private System.Windows.Forms.Panel pnLogo;
        private System.Windows.Forms.Label lbLogo;
        private System.Windows.Forms.Panel pnData;
        private System.Windows.Forms.ComboBox cbModel;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnES;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lbModelSelect;
        private System.Windows.Forms.DataGridView tGenUnits;
        private System.Windows.Forms.DataGridView tEnergyUnits;
        private System.Windows.Forms.DataGridView tLoadData;
        private System.Windows.Forms.DataGridViewTextBoxColumn T1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnitNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinGen;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxGen;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinGenCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinUpTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn MinDownTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn RampUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn RampDown;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartupRamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShutdownRamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenQuantityA;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenPriceA;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenQuantityB;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenPriceB;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenQuantityC;
        private System.Windows.Forms.DataGridViewTextBoxColumn GenPriceC;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChP;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisP;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESMaxE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESMinE;
        private System.Windows.Forms.DataGridViewTextBoxColumn InitE;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESRampUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESRampD;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChEff;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisEff;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChQtA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChPriceA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChQtB;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChPriceB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisQtA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisPriceA;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisQtB;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisPriceB;
    }
}