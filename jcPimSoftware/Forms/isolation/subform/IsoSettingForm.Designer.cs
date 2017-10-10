namespace jcPimSoftware
{
    partial class IsoSettingForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxSettings = new System.Windows.Forms.GroupBox();
            this.nudMinIso = new System.Windows.Forms.NumericUpDown();
            this.nudMaxIso = new System.Windows.Forms.NumericUpDown();
            this.nudLimit = new System.Windows.Forms.NumericUpDown();
            this.nudFreqStep = new System.Windows.Forms.NumericUpDown();
            this.nudAtt = new System.Windows.Forms.NumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.nudTimePoints = new System.Windows.Forms.NumericUpDown();
            this.nudTx = new System.Windows.Forms.NumericUpDown();
            this.nudFrq = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblAtt = new System.Windows.Forms.Label();
            this.lblTx = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCarrier = new System.Windows.Forms.Label();
            this.cbxCarrier = new System.Windows.Forms.ComboBox();
            this.btnCal = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.gbxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinIso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIso)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimePoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrq)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxSettings
            // 
            this.gbxSettings.Controls.Add(this.nudMinIso);
            this.gbxSettings.Controls.Add(this.nudMaxIso);
            this.gbxSettings.Controls.Add(this.nudLimit);
            this.gbxSettings.Controls.Add(this.nudFreqStep);
            this.gbxSettings.Controls.Add(this.nudAtt);
            this.gbxSettings.Controls.Add(this.lblMin);
            this.gbxSettings.Controls.Add(this.nudTimePoints);
            this.gbxSettings.Controls.Add(this.nudTx);
            this.gbxSettings.Controls.Add(this.nudFrq);
            this.gbxSettings.Controls.Add(this.label6);
            this.gbxSettings.Controls.Add(this.label2);
            this.gbxSettings.Controls.Add(this.label3);
            this.gbxSettings.Controls.Add(this.lblFreq);
            this.gbxSettings.Controls.Add(this.lblAtt);
            this.gbxSettings.Controls.Add(this.lblTx);
            this.gbxSettings.Controls.Add(this.lblMax);
            this.gbxSettings.Location = new System.Drawing.Point(8, 6);
            this.gbxSettings.Name = "gbxSettings";
            this.gbxSettings.Size = new System.Drawing.Size(416, 163);
            this.gbxSettings.TabIndex = 50;
            this.gbxSettings.TabStop = false;
            this.gbxSettings.Text = "Settings";
            // 
            // nudMinIso
            // 
            this.nudMinIso.Location = new System.Drawing.Point(142, 131);
            this.nudMinIso.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinIso.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMinIso.Name = "nudMinIso";
            this.nudMinIso.Size = new System.Drawing.Size(80, 26);
            this.nudMinIso.TabIndex = 86;
            this.nudMinIso.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudMaxIso
            // 
            this.nudMaxIso.Location = new System.Drawing.Point(309, 131);
            this.nudMaxIso.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxIso.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMaxIso.Name = "nudMaxIso";
            this.nudMaxIso.Size = new System.Drawing.Size(80, 26);
            this.nudMaxIso.TabIndex = 85;
            this.nudMaxIso.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudLimit
            // 
            this.nudLimit.DecimalPlaces = 1;
            this.nudLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLimit.Location = new System.Drawing.Point(309, 92);
            this.nudLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLimit.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudLimit.Name = "nudLimit";
            this.nudLimit.Size = new System.Drawing.Size(80, 26);
            this.nudLimit.TabIndex = 84;
            this.nudLimit.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudFreqStep
            // 
            this.nudFreqStep.DecimalPlaces = 1;
            this.nudFreqStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFreqStep.Location = new System.Drawing.Point(142, 92);
            this.nudFreqStep.Name = "nudFreqStep";
            this.nudFreqStep.Size = new System.Drawing.Size(80, 26);
            this.nudFreqStep.TabIndex = 83;
            this.nudFreqStep.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudAtt
            // 
            this.nudAtt.Location = new System.Drawing.Point(309, 55);
            this.nudAtt.Name = "nudAtt";
            this.nudAtt.Size = new System.Drawing.Size(80, 26);
            this.nudAtt.TabIndex = 82;
            this.nudAtt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMin.Location = new System.Drawing.Point(80, 133);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(48, 16);
            this.lblMin.TabIndex = 49;
            this.lblMin.Text = "Min Y";
            // 
            // nudTimePoints
            // 
            this.nudTimePoints.Location = new System.Drawing.Point(142, 55);
            this.nudTimePoints.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimePoints.Name = "nudTimePoints";
            this.nudTimePoints.Size = new System.Drawing.Size(80, 26);
            this.nudTimePoints.TabIndex = 81;
            this.nudTimePoints.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimePoints.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudTx
            // 
            this.nudTx.DecimalPlaces = 1;
            this.nudTx.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTx.Location = new System.Drawing.Point(309, 19);
            this.nudTx.Name = "nudTx";
            this.nudTx.Size = new System.Drawing.Size(80, 26);
            this.nudTx.TabIndex = 80;
            this.nudTx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudFrq
            // 
            this.nudFrq.DecimalPlaces = 1;
            this.nudFrq.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFrq.Location = new System.Drawing.Point(142, 19);
            this.nudFrq.Name = "nudFrq";
            this.nudFrq.Size = new System.Drawing.Size(80, 26);
            this.nudFrq.TabIndex = 79;
            this.nudFrq.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(8, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 77;
            this.label6.Text = "Freq Step(MHz)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(32, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "Time Points";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(251, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 67;
            this.label3.Text = "Limit";
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFreq.Location = new System.Drawing.Point(72, 24);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(56, 16);
            this.lblFreq.TabIndex = 61;
            this.lblFreq.Text = "F(MHz)";
            // 
            // lblAtt
            // 
            this.lblAtt.AutoSize = true;
            this.lblAtt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAtt.Location = new System.Drawing.Point(235, 60);
            this.lblAtt.Name = "lblAtt";
            this.lblAtt.Size = new System.Drawing.Size(64, 16);
            this.lblAtt.TabIndex = 59;
            this.lblAtt.Text = "ATT(dB)";
            // 
            // lblTx
            // 
            this.lblTx.AutoSize = true;
            this.lblTx.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTx.Location = new System.Drawing.Point(235, 24);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(64, 16);
            this.lblTx.TabIndex = 53;
            this.lblTx.Text = "Tx(dBm)";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMax.Location = new System.Drawing.Point(251, 133);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(48, 16);
            this.lblMax.TabIndex = 48;
            this.lblMax.Text = "Max Y";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblCarrier);
            this.groupBox1.Controls.Add(this.cbxCarrier);
            this.groupBox1.Controls.Add(this.btnCal);
            this.groupBox1.Location = new System.Drawing.Point(8, 175);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 75);
            this.groupBox1.TabIndex = 51;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CAL";
            // 
            // lblCarrier
            // 
            this.lblCarrier.AutoSize = true;
            this.lblCarrier.Font = new System.Drawing.Font("宋体", 12F);
            this.lblCarrier.Location = new System.Drawing.Point(31, 34);
            this.lblCarrier.Name = "lblCarrier";
            this.lblCarrier.Size = new System.Drawing.Size(64, 16);
            this.lblCarrier.TabIndex = 85;
            this.lblCarrier.Text = "Carrier";
            // 
            // cbxCarrier
            // 
            this.cbxCarrier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCarrier.Font = new System.Drawing.Font("宋体", 12F);
            this.cbxCarrier.FormattingEnabled = true;
            this.cbxCarrier.Items.AddRange(new object[] {
            "Carrier1",
            "Carrier2"});
            this.cbxCarrier.Location = new System.Drawing.Point(103, 28);
            this.cbxCarrier.Name = "cbxCarrier";
            this.cbxCarrier.Size = new System.Drawing.Size(107, 24);
            this.cbxCarrier.TabIndex = 84;
            // 
            // btnCal
            // 
            this.btnCal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCal.Location = new System.Drawing.Point(258, 25);
            this.btnCal.Name = "btnCal";
            this.btnCal.Size = new System.Drawing.Size(131, 30);
            this.btnCal.TabIndex = 81;
            this.btnCal.Text = "Calibration";
            this.btnCal.UseVisualStyleBackColor = true;
            this.btnCal.Click += new System.EventHandler(this.btnCal_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveAs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveAs.Location = new System.Drawing.Point(111, 256);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(80, 30);
            this.btnSaveAs.TabIndex = 78;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(341, 256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 76;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(341, 293);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 77;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnDefault
            // 
            this.btnDefault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDefault.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDefault.Location = new System.Drawing.Point(5, 256);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(80, 30);
            this.btnDefault.TabIndex = 79;
            this.btnDefault.Text = "Defaults";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoad.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLoad.Location = new System.Drawing.Point(217, 256);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 30);
            this.btnLoad.TabIndex = 80;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // IsoSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 330);
            this.ControlBox = false;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxSettings);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IsoSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Isolation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IsoSettingForm_Load);
            this.gbxSettings.ResumeLayout(false);
            this.gbxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinIso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxIso)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimePoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrq)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxSettings;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label lblAtt;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCal;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblCarrier;
        private System.Windows.Forms.ComboBox cbxCarrier;
        private System.Windows.Forms.NumericUpDown nudFrq;
        private System.Windows.Forms.NumericUpDown nudTx;
        private System.Windows.Forms.NumericUpDown nudTimePoints;
        private System.Windows.Forms.NumericUpDown nudAtt;
        private System.Windows.Forms.NumericUpDown nudFreqStep;
        private System.Windows.Forms.NumericUpDown nudLimit;
        private System.Windows.Forms.NumericUpDown nudMaxIso;
        private System.Windows.Forms.NumericUpDown nudMinIso;

    }
}