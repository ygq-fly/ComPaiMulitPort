namespace jcPimSoftware
{
    partial class FormRF
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
            this.btnOK = new System.Windows.Forms.Button();
            this.chkEnable = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownFreq = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownTx = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTx)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(178, 123);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 31);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "O K";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkEnable
            // 
            this.chkEnable.AutoSize = true;
            this.chkEnable.Location = new System.Drawing.Point(63, 129);
            this.chkEnable.Margin = new System.Windows.Forms.Padding(4);
            this.chkEnable.Name = "chkEnable";
            this.chkEnable.Size = new System.Drawing.Size(83, 20);
            this.chkEnable.TabIndex = 2;
            this.chkEnable.Text = "Enabled";
            this.chkEnable.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Freq:";
            // 
            // numericUpDownFreq
            // 
            this.numericUpDownFreq.DecimalPlaces = 1;
            this.numericUpDownFreq.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownFreq.Location = new System.Drawing.Point(63, 26);
            this.numericUpDownFreq.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownFreq.Name = "numericUpDownFreq";
            this.numericUpDownFreq.Size = new System.Drawing.Size(114, 26);
            this.numericUpDownFreq.TabIndex = 4;
            this.numericUpDownFreq.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDownFreq_MouseDoubleClick);
            // 
            // numericUpDownTx
            // 
            this.numericUpDownTx.DecimalPlaces = 1;
            this.numericUpDownTx.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTx.Location = new System.Drawing.Point(63, 75);
            this.numericUpDownTx.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownTx.Name = "numericUpDownTx";
            this.numericUpDownTx.Size = new System.Drawing.Size(114, 26);
            this.numericUpDownTx.TabIndex = 6;
            this.numericUpDownTx.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDownTx_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tx:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "(MHz)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 77);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "(dBm)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numericUpDownFreq);
            this.groupBox1.Controls.Add(this.chkEnable);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnOK);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownTx);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 168);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channel:RF_1";
            // 
            // FormRF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 192);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RF Settings";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormRF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTx)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkEnable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownFreq;
        private System.Windows.Forms.NumericUpDown numericUpDownTx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}