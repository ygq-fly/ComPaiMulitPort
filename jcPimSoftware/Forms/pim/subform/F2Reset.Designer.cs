namespace jcPimSoftware
{
    partial class F2Reset
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
            this.lblF2Fre = new System.Windows.Forms.Label();
            this.lblF2Power = new System.Windows.Forms.Label();
            this.lblFre = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.nudFre = new System.Windows.Forms.NumericUpDown();
            this.nudPower = new System.Windows.Forms.NumericUpDown();
            this.nudFreS = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudStep = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudFre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).BeginInit();
            this.SuspendLayout();
            // 
            // lblF2Fre
            // 
            this.lblF2Fre.AutoSize = true;
            this.lblF2Fre.Location = new System.Drawing.Point(12, 30);
            this.lblF2Fre.Name = "lblF2Fre";
            this.lblF2Fre.Size = new System.Drawing.Size(104, 16);
            this.lblF2Fre.TabIndex = 0;
            this.lblF2Fre.Text = "F2 Frequency";
            // 
            // lblF2Power
            // 
            this.lblF2Power.AutoSize = true;
            this.lblF2Power.Location = new System.Drawing.Point(12, 74);
            this.lblF2Power.Name = "lblF2Power";
            this.lblF2Power.Size = new System.Drawing.Size(72, 16);
            this.lblF2Power.TabIndex = 1;
            this.lblF2Power.Text = "F2 Power";
            // 
            // lblFre
            // 
            this.lblFre.AutoSize = true;
            this.lblFre.Location = new System.Drawing.Point(342, 30);
            this.lblFre.Name = "lblFre";
            this.lblFre.Size = new System.Drawing.Size(144, 16);
            this.lblFre.TabIndex = 4;
            this.lblFre.Text = "950.0MHz-960.0MHz";
            // 
            // lblPower
            // 
            this.lblPower.AutoSize = true;
            this.lblPower.Location = new System.Drawing.Point(245, 76);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(128, 16);
            this.lblPower.TabIndex = 5;
            this.lblPower.Text = "29.0dBm-47.0dBm";
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(261, 159);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.Location = new System.Drawing.Point(122, 159);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 30);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // nudFre
            // 
            this.nudFre.DecimalPlaces = 1;
            this.nudFre.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFre.Location = new System.Drawing.Point(248, 28);
            this.nudFre.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFre.Name = "nudFre";
            this.nudFre.Size = new System.Drawing.Size(88, 26);
            this.nudFre.TabIndex = 10;
            this.nudFre.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nud_MouseDoubleClick);
            // 
            // nudPower
            // 
            this.nudPower.DecimalPlaces = 1;
            this.nudPower.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudPower.Location = new System.Drawing.Point(122, 74);
            this.nudPower.Name = "nudPower";
            this.nudPower.Size = new System.Drawing.Size(88, 26);
            this.nudPower.TabIndex = 11;
            this.nudPower.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nud_MouseDoubleClick);
            // 
            // nudFreS
            // 
            this.nudFreS.DecimalPlaces = 1;
            this.nudFreS.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudFreS.Location = new System.Drawing.Point(122, 28);
            this.nudFreS.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudFreS.Name = "nudFreS";
            this.nudFreS.Size = new System.Drawing.Size(88, 26);
            this.nudFreS.TabIndex = 12;
            this.nudFreS.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nud_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(221, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "MHz";
            // 
            // nudStep
            // 
            this.nudStep.DecimalPlaces = 1;
            this.nudStep.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudStep.Location = new System.Drawing.Point(122, 114);
            this.nudStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudStep.Name = "nudStep";
            this.nudStep.Size = new System.Drawing.Size(88, 26);
            this.nudStep.TabIndex = 16;
            this.nudStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudStep.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.nud_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Setp";
            // 
            // F2Reset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 204);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudStep);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudFreS);
            this.Controls.Add(this.nudPower);
            this.Controls.Add(this.nudFre);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblPower);
            this.Controls.Add(this.lblFre);
            this.Controls.Add(this.lblF2Power);
            this.Controls.Add(this.lblF2Fre);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F2Reset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "F2 Reset";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.F2Reset_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudFre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudPower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudFreS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblF2Fre;
        private System.Windows.Forms.Label lblF2Power;
        private System.Windows.Forms.Label lblFre;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.NumericUpDown nudFre;
        public System.Windows.Forms.NumericUpDown nudPower;
        public System.Windows.Forms.NumericUpDown nudFreS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.NumericUpDown nudStep;
        private System.Windows.Forms.Label label2;
    }
}