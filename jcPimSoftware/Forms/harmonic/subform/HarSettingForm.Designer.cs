namespace jcPimSoftware
{
    partial class HarSettingForm
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
            this.nudMinHar = new System.Windows.Forms.NumericUpDown();
            this.nudMaxHar = new System.Windows.Forms.NumericUpDown();
            this.nudLimit = new System.Windows.Forms.NumericUpDown();
            this.nudFreqStep = new System.Windows.Forms.NumericUpDown();
            this.nudAtt = new System.Windows.Forms.NumericUpDown();
            this.nudTimePoints = new System.Windows.Forms.NumericUpDown();
            this.nudTx = new System.Windows.Forms.NumericUpDown();
            this.nudFrq = new System.Windows.Forms.NumericUpDown();
            this.lblMin = new System.Windows.Forms.Label();
            this.numericUpDownRev = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblFreq = new System.Windows.Forms.Label();
            this.lblAtt = new System.Windows.Forms.Label();
            this.lblTx = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnDefault = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.gbxSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinHar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimePoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRev)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxSettings
            // 
            this.gbxSettings.Controls.Add(this.nudMinHar);
            this.gbxSettings.Controls.Add(this.nudMaxHar);
            this.gbxSettings.Controls.Add(this.nudLimit);
            this.gbxSettings.Controls.Add(this.nudFreqStep);
            this.gbxSettings.Controls.Add(this.nudAtt);
            this.gbxSettings.Controls.Add(this.nudTimePoints);
            this.gbxSettings.Controls.Add(this.nudTx);
            this.gbxSettings.Controls.Add(this.nudFrq);
            this.gbxSettings.Controls.Add(this.lblMin);
            this.gbxSettings.Controls.Add(this.numericUpDownRev);
            this.gbxSettings.Controls.Add(this.label6);
            this.gbxSettings.Controls.Add(this.label2);
            this.gbxSettings.Controls.Add(this.label3);
            this.gbxSettings.Controls.Add(this.label1);
            this.gbxSettings.Controls.Add(this.lblFreq);
            this.gbxSettings.Controls.Add(this.lblAtt);
            this.gbxSettings.Controls.Add(this.lblTx);
            this.gbxSettings.Controls.Add(this.lblMax);
            this.gbxSettings.Location = new System.Drawing.Point(8, 6);
            this.gbxSettings.Name = "gbxSettings";
            this.gbxSettings.Size = new System.Drawing.Size(406, 203);
            this.gbxSettings.TabIndex = 50;
            this.gbxSettings.TabStop = false;
            this.gbxSettings.Text = "Settings";
            // 
            // nudMinHar
            // 
            this.nudMinHar.Location = new System.Drawing.Point(140, 131);
            this.nudMinHar.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMinHar.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMinHar.Name = "nudMinHar";
            this.nudMinHar.Size = new System.Drawing.Size(80, 26);
            this.nudMinHar.TabIndex = 88;
            this.nudMinHar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudMaxHar
            // 
            this.nudMaxHar.Location = new System.Drawing.Point(317, 131);
            this.nudMaxHar.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudMaxHar.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nudMaxHar.Name = "nudMaxHar";
            this.nudMaxHar.Size = new System.Drawing.Size(80, 26);
            this.nudMaxHar.TabIndex = 87;
            this.nudMaxHar.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudLimit
            // 
            this.nudLimit.DecimalPlaces = 1;
            this.nudLimit.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLimit.Location = new System.Drawing.Point(317, 93);
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
            this.nudLimit.TabIndex = 86;
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
            this.nudFreqStep.Location = new System.Drawing.Point(140, 93);
            this.nudFreqStep.Name = "nudFreqStep";
            this.nudFreqStep.Size = new System.Drawing.Size(80, 26);
            this.nudFreqStep.TabIndex = 85;
            this.nudFreqStep.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudAtt
            // 
            this.nudAtt.Location = new System.Drawing.Point(317, 56);
            this.nudAtt.Name = "nudAtt";
            this.nudAtt.Size = new System.Drawing.Size(80, 26);
            this.nudAtt.TabIndex = 84;
            this.nudAtt.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // nudTimePoints
            // 
            this.nudTimePoints.Location = new System.Drawing.Point(140, 56);
            this.nudTimePoints.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimePoints.Name = "nudTimePoints";
            this.nudTimePoints.Size = new System.Drawing.Size(80, 26);
            this.nudTimePoints.TabIndex = 83;
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
            this.nudTx.Location = new System.Drawing.Point(317, 19);
            this.nudTx.Name = "nudTx";
            this.nudTx.Size = new System.Drawing.Size(80, 26);
            this.nudTx.TabIndex = 82;
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
            this.nudFrq.Location = new System.Drawing.Point(140, 19);
            this.nudFrq.Name = "nudFrq";
            this.nudFrq.Size = new System.Drawing.Size(80, 26);
            this.nudFrq.TabIndex = 81;
            this.nudFrq.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMin.Location = new System.Drawing.Point(38, 133);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(88, 16);
            this.lblMin.TabIndex = 49;
            this.lblMin.Text = "Min Y(dBm)";
            // 
            // numericUpDownRev
            // 
            this.numericUpDownRev.DecimalPlaces = 2;
            this.numericUpDownRev.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDownRev.Location = new System.Drawing.Point(140, 167);
            this.numericUpDownRev.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRev.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownRev.Name = "numericUpDownRev";
            this.numericUpDownRev.Size = new System.Drawing.Size(80, 26);
            this.numericUpDownRev.TabIndex = 79;
            this.numericUpDownRev.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nudValue_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(6, 97);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 77;
            this.label6.Text = "Freq Step(MHz)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(30, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 68;
            this.label2.Text = "Time Points";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(263, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 67;
            this.label3.Text = "Limit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(94, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 65;
            this.label1.Text = "Rev";
            // 
            // lblFreq
            // 
            this.lblFreq.AutoSize = true;
            this.lblFreq.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFreq.Location = new System.Drawing.Point(70, 24);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(56, 16);
            this.lblFreq.TabIndex = 61;
            this.lblFreq.Text = "F(MHz)";
            // 
            // lblAtt
            // 
            this.lblAtt.AutoSize = true;
            this.lblAtt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAtt.Location = new System.Drawing.Point(247, 60);
            this.lblAtt.Name = "lblAtt";
            this.lblAtt.Size = new System.Drawing.Size(64, 16);
            this.lblAtt.TabIndex = 59;
            this.lblAtt.Text = "ATT(dB)";
            // 
            // lblTx
            // 
            this.lblTx.AutoSize = true;
            this.lblTx.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTx.Location = new System.Drawing.Point(247, 24);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(64, 16);
            this.lblTx.TabIndex = 53;
            this.lblTx.Text = "Tx(dBm)";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMax.Location = new System.Drawing.Point(223, 133);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(88, 16);
            this.lblMax.TabIndex = 48;
            this.lblMax.Text = "Max Y(dBm)";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveAs.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveAs.Location = new System.Drawing.Point(122, 220);
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
            this.btnOK.Location = new System.Drawing.Point(334, 221);
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
            this.btnCancel.Location = new System.Drawing.Point(334, 259);
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
            this.btnDefault.Location = new System.Drawing.Point(16, 220);
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
            this.btnLoad.Location = new System.Drawing.Point(228, 220);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(80, 30);
            this.btnLoad.TabIndex = 80;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // HarSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 294);
            this.ControlBox = false;
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbxSettings);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HarSettingForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Harmonic";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.IsoSettingForm_Load);
            this.gbxSettings.ResumeLayout(false);
            this.gbxSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinHar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxHar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreqStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAtt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimePoints)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudTx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFrq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRev)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxSettings;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label lblAtt;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnDefault;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.NumericUpDown numericUpDownRev;
        private System.Windows.Forms.NumericUpDown nudMinHar;
        private System.Windows.Forms.NumericUpDown nudMaxHar;
        private System.Windows.Forms.NumericUpDown nudLimit;
        private System.Windows.Forms.NumericUpDown nudFreqStep;
        private System.Windows.Forms.NumericUpDown nudAtt;
        private System.Windows.Forms.NumericUpDown nudTimePoints;
        private System.Windows.Forms.NumericUpDown nudTx;
        private System.Windows.Forms.NumericUpDown nudFrq;

    }
}