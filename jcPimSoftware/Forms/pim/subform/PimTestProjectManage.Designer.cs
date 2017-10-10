namespace jcPimSoftware
{
    partial class PimTestProject
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
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.listBoxProject = new System.Windows.Forms.ListBox();
            this.groupSettings = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblPimMode = new System.Windows.Forms.Label();
            this.cbxPimMode = new System.Windows.Forms.ComboBox();
            this.cbxPimSchema = new System.Windows.Forms.ComboBox();
            this.nudTx = new System.Windows.Forms.NumericUpDown();
            this.lblPimSchema = new System.Windows.Forms.Label();
            this.lblTx = new System.Windows.Forms.Label();
            this.lblPimOrder = new System.Windows.Forms.Label();
            this.cbxPimOrder = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblFre = new System.Windows.Forms.Label();
            this.nudFre = new System.Windows.Forms.NumericUpDown();
            this.nudFreE = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.lblF1Fre = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblFre2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudFre2 = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudFreE2 = new System.Windows.Forms.NumericUpDown();
            this.nudStep = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudTime = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.btnModOk = new System.Windows.Forms.Button();
            this.btnModCancel = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.groupSettings.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreE)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFre2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreE2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDel
            // 
            this.btnDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDel.Location = new System.Drawing.Point(395, 147);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(57, 39);
            this.btnDel.TabIndex = 1;
            this.btnDel.Text = "删除";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(395, 102);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(57, 39);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // listBoxProject
            // 
            this.listBoxProject.Font = new System.Drawing.Font("SimSun", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBoxProject.FormattingEnabled = true;
            this.listBoxProject.ItemHeight = 17;
            this.listBoxProject.Location = new System.Drawing.Point(12, 12);
            this.listBoxProject.Name = "listBoxProject";
            this.listBoxProject.Size = new System.Drawing.Size(377, 174);
            this.listBoxProject.TabIndex = 3;
            this.listBoxProject.SelectedIndexChanged += new System.EventHandler(this.listBoxProject_SelectedIndexChanged);
            // 
            // groupSettings
            // 
            this.groupSettings.Controls.Add(this.groupBox4);
            this.groupSettings.Controls.Add(this.groupBox3);
            this.groupSettings.Controls.Add(this.groupBox2);
            this.groupSettings.Controls.Add(this.nudStep);
            this.groupSettings.Controls.Add(this.label3);
            this.groupSettings.Controls.Add(this.label2);
            this.groupSettings.Controls.Add(this.nudTime);
            this.groupSettings.Location = new System.Drawing.Point(12, 190);
            this.groupSettings.Name = "groupSettings";
            this.groupSettings.Size = new System.Drawing.Size(440, 248);
            this.groupSettings.TabIndex = 4;
            this.groupSettings.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblPimMode);
            this.groupBox4.Controls.Add(this.cbxPimMode);
            this.groupBox4.Controls.Add(this.cbxPimSchema);
            this.groupBox4.Controls.Add(this.nudTx);
            this.groupBox4.Controls.Add(this.lblPimSchema);
            this.groupBox4.Controls.Add(this.lblTx);
            this.groupBox4.Controls.Add(this.lblPimOrder);
            this.groupBox4.Controls.Add(this.cbxPimOrder);
            this.groupBox4.Location = new System.Drawing.Point(6, 15);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(428, 89);
            this.groupBox4.TabIndex = 60;
            this.groupBox4.TabStop = false;
            // 
            // lblPimMode
            // 
            this.lblPimMode.AutoSize = true;
            this.lblPimMode.Font = new System.Drawing.Font("SimSun", 12F);
            this.lblPimMode.Location = new System.Drawing.Point(6, 17);
            this.lblPimMode.Name = "lblPimMode";
            this.lblPimMode.Size = new System.Drawing.Size(88, 16);
            this.lblPimMode.TabIndex = 33;
            this.lblPimMode.Text = "SweepType:";
            // 
            // cbxPimMode
            // 
            this.cbxPimMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimMode.Font = new System.Drawing.Font("SimSun", 12F);
            this.cbxPimMode.FormattingEnabled = true;
            this.cbxPimMode.Items.AddRange(new object[] {
            "Freq_Sweep",
            "Time_Sweep"});
            this.cbxPimMode.Location = new System.Drawing.Point(101, 13);
            this.cbxPimMode.Name = "cbxPimMode";
            this.cbxPimMode.Size = new System.Drawing.Size(107, 24);
            this.cbxPimMode.TabIndex = 32;
            this.cbxPimMode.SelectedIndexChanged += new System.EventHandler(this.cbxPimMode_SelectedIndexChanged);
            // 
            // cbxPimSchema
            // 
            this.cbxPimSchema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimSchema.Font = new System.Drawing.Font("SimSun", 12F);
            this.cbxPimSchema.FormattingEnabled = true;
            this.cbxPimSchema.Location = new System.Drawing.Point(340, 14);
            this.cbxPimSchema.Name = "cbxPimSchema";
            this.cbxPimSchema.Size = new System.Drawing.Size(79, 24);
            this.cbxPimSchema.TabIndex = 36;
            // 
            // nudTx
            // 
            this.nudTx.DecimalPlaces = 1;
            this.nudTx.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudTx.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTx.Location = new System.Drawing.Point(340, 53);
            this.nudTx.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTx.Name = "nudTx";
            this.nudTx.Size = new System.Drawing.Size(79, 26);
            this.nudTx.TabIndex = 55;
            this.nudTx.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPimSchema
            // 
            this.lblPimSchema.AutoSize = true;
            this.lblPimSchema.Font = new System.Drawing.Font("SimSun", 12F);
            this.lblPimSchema.Location = new System.Drawing.Point(240, 17);
            this.lblPimSchema.Name = "lblPimSchema";
            this.lblPimSchema.Size = new System.Drawing.Size(88, 16);
            this.lblPimSchema.TabIndex = 37;
            this.lblPimSchema.Text = "PimSchema:";
            // 
            // lblTx
            // 
            this.lblTx.AutoSize = true;
            this.lblTx.Font = new System.Drawing.Font("SimSun", 12F);
            this.lblTx.Location = new System.Drawing.Point(240, 58);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(80, 16);
            this.lblTx.TabIndex = 53;
            this.lblTx.Text = "Tx(dBm)：";
            // 
            // lblPimOrder
            // 
            this.lblPimOrder.AutoSize = true;
            this.lblPimOrder.Font = new System.Drawing.Font("SimSun", 12F);
            this.lblPimOrder.Location = new System.Drawing.Point(6, 55);
            this.lblPimOrder.Name = "lblPimOrder";
            this.lblPimOrder.Size = new System.Drawing.Size(88, 16);
            this.lblPimOrder.TabIndex = 34;
            this.lblPimOrder.Text = "PIM Order:";
            // 
            // cbxPimOrder
            // 
            this.cbxPimOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPimOrder.Font = new System.Drawing.Font("SimSun", 12F);
            this.cbxPimOrder.FormattingEnabled = true;
            this.cbxPimOrder.Items.AddRange(new object[] {
            "3",
            "5",
            "7",
            "9"});
            this.cbxPimOrder.Location = new System.Drawing.Point(101, 55);
            this.cbxPimOrder.Name = "cbxPimOrder";
            this.cbxPimOrder.Size = new System.Drawing.Size(107, 24);
            this.cbxPimOrder.TabIndex = 35;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblFre);
            this.groupBox3.Controls.Add(this.nudFre);
            this.groupBox3.Controls.Add(this.nudFreE);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.lblF1Fre);
            this.groupBox3.Location = new System.Drawing.Point(6, 106);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 103);
            this.groupBox3.TabIndex = 59;
            this.groupBox3.TabStop = false;
            // 
            // lblFre
            // 
            this.lblFre.AutoSize = true;
            this.lblFre.ForeColor = System.Drawing.Color.Black;
            this.lblFre.Location = new System.Drawing.Point(40, 79);
            this.lblFre.Name = "lblFre";
            this.lblFre.Size = new System.Drawing.Size(119, 12);
            this.lblFre.TabIndex = 58;
            this.lblFre.Text = "(930.0MHz-940.0MHz)";
            // 
            // nudFre
            // 
            this.nudFre.DecimalPlaces = 1;
            this.nudFre.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFre.Location = new System.Drawing.Point(104, 15);
            this.nudFre.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFre.Name = "nudFre";
            this.nudFre.Size = new System.Drawing.Size(88, 21);
            this.nudFre.TabIndex = 19;
            // 
            // nudFreE
            // 
            this.nudFreE.DecimalPlaces = 1;
            this.nudFreE.Enabled = false;
            this.nudFreE.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFreE.Location = new System.Drawing.Point(104, 47);
            this.nudFreE.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFreE.Name = "nudFreE";
            this.nudFreE.Size = new System.Drawing.Size(88, 21);
            this.nudFreE.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 12);
            this.label4.TabIndex = 57;
            this.label4.Text = "F1   End(MHz)";
            // 
            // lblF1Fre
            // 
            this.lblF1Fre.AutoSize = true;
            this.lblF1Fre.Location = new System.Drawing.Point(7, 20);
            this.lblF1Fre.Name = "lblF1Fre";
            this.lblF1Fre.Size = new System.Drawing.Size(83, 12);
            this.lblF1Fre.TabIndex = 15;
            this.lblF1Fre.Text = "F1 Start(MHz)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblFre2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.nudFre2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.nudFreE2);
            this.groupBox2.Location = new System.Drawing.Point(233, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(201, 103);
            this.groupBox2.TabIndex = 58;
            this.groupBox2.TabStop = false;
            // 
            // lblFre2
            // 
            this.lblFre2.AutoSize = true;
            this.lblFre2.ForeColor = System.Drawing.Color.Black;
            this.lblFre2.Location = new System.Drawing.Point(39, 79);
            this.lblFre2.Name = "lblFre2";
            this.lblFre2.Size = new System.Drawing.Size(119, 12);
            this.lblFre2.TabIndex = 59;
            this.lblFre2.Text = "(930.0MHz-940.0MHz)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 12);
            this.label6.TabIndex = 25;
            this.label6.Text = "F2 Start(MHz)";
            // 
            // nudFre2
            // 
            this.nudFre2.DecimalPlaces = 1;
            this.nudFre2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFre2.Location = new System.Drawing.Point(104, 15);
            this.nudFre2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFre2.Name = "nudFre2";
            this.nudFre2.Size = new System.Drawing.Size(88, 21);
            this.nudFre2.TabIndex = 27;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 56;
            this.label7.Text = "F2   End(MHz)";
            // 
            // nudFreE2
            // 
            this.nudFreE2.DecimalPlaces = 1;
            this.nudFreE2.Enabled = false;
            this.nudFreE2.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFreE2.Location = new System.Drawing.Point(104, 47);
            this.nudFreE2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFreE2.Name = "nudFreE2";
            this.nudFreE2.Size = new System.Drawing.Size(88, 21);
            this.nudFreE2.TabIndex = 28;
            // 
            // nudStep
            // 
            this.nudStep.DecimalPlaces = 1;
            this.nudStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudStep.Location = new System.Drawing.Point(110, 216);
            this.nudStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudStep.Name = "nudStep";
            this.nudStep.Size = new System.Drawing.Size(88, 21);
            this.nudStep.TabIndex = 24;
            this.nudStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(263, 221);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 30;
            this.label3.Text = "Time(Min)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 221);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "Setp(MHz)";
            // 
            // nudTime
            // 
            this.nudTime.DecimalPlaces = 1;
            this.nudTime.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTime.Location = new System.Drawing.Point(337, 216);
            this.nudTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTime.Name = "nudTime";
            this.nudTime.Size = new System.Drawing.Size(88, 21);
            this.nudTime.TabIndex = 31;
            this.nudTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(64, 455);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 62;
            this.label1.Text = "Name:";
            // 
            // tbxName
            // 
            this.tbxName.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbxName.Location = new System.Drawing.Point(119, 452);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(107, 26);
            this.tbxName.TabIndex = 61;
            // 
            // btnModOk
            // 
            this.btnModOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModOk.Location = new System.Drawing.Point(246, 453);
            this.btnModOk.Name = "btnModOk";
            this.btnModOk.Size = new System.Drawing.Size(57, 25);
            this.btnModOk.TabIndex = 6;
            this.btnModOk.Text = "修改";
            this.btnModOk.UseVisualStyleBackColor = true;
            this.btnModOk.Click += new System.EventHandler(this.btnModOk_Click);
            // 
            // btnModCancel
            // 
            this.btnModCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModCancel.Location = new System.Drawing.Point(309, 453);
            this.btnModCancel.Name = "btnModCancel";
            this.btnModCancel.Size = new System.Drawing.Size(57, 25);
            this.btnModCancel.TabIndex = 5;
            this.btnModCancel.Text = "取消";
            this.btnModCancel.UseVisualStyleBackColor = true;
            this.btnModCancel.Click += new System.EventHandler(this.btnModCancel_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelect.Location = new System.Drawing.Point(395, 12);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(57, 39);
            this.btnSelect.TabIndex = 8;
            this.btnSelect.Text = "选择";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // PimTestProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 490);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.groupSettings);
            this.Controls.Add(this.btnModOk);
            this.Controls.Add(this.listBoxProject);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnModCancel);
            this.Controls.Add(this.btnDel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PimTestProject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "方案管理";
            this.Load += new System.EventHandler(this.PimTestProjectManage_Load);
            this.groupSettings.ResumeLayout(false);
            this.groupSettings.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreE)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFre2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreE2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox listBoxProject;
        private System.Windows.Forms.GroupBox groupSettings;
        private System.Windows.Forms.Button btnSelect;
        public System.Windows.Forms.NumericUpDown nudTime;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown nudFreE2;
        public System.Windows.Forms.NumericUpDown nudFre2;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.NumericUpDown nudStep;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown nudFreE;
        public System.Windows.Forms.NumericUpDown nudFre;
        private System.Windows.Forms.Label lblF1Fre;
        private System.Windows.Forms.Label lblPimMode;
        private System.Windows.Forms.ComboBox cbxPimMode;
        private System.Windows.Forms.Label lblPimSchema;
        private System.Windows.Forms.ComboBox cbxPimSchema;
        private System.Windows.Forms.Label lblPimOrder;
        private System.Windows.Forms.ComboBox cbxPimOrder;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown nudTx;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.Button btnModOk;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnModCancel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.Label lblFre;
        private System.Windows.Forms.Label lblFre2;
    }
}