namespace jcPimSoftware
{
    partial class GlobalConfiguration
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
            this.groupBoxGFSZ = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.numericUpDownTemp = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownVswr = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioBroad = new System.Windows.Forms.RadioButton();
            this.radioNarrow = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk_battary = new System.Windows.Forms.CheckBox();
            this.radio_gpio_new = new System.Windows.Forms.RadioButton();
            this.radio_gpio_old = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFileManage = new System.Windows.Forms.Button();
            this.btnParameters = new System.Windows.Forms.Button();
            this.btnCompensation = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBoxGFSZ.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVswr)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxGFSZ
            // 
            this.groupBoxGFSZ.Controls.Add(this.label18);
            this.groupBoxGFSZ.Controls.Add(this.numericUpDownTemp);
            this.groupBoxGFSZ.Controls.Add(this.numericUpDownVswr);
            this.groupBoxGFSZ.Controls.Add(this.label2);
            this.groupBoxGFSZ.Controls.Add(this.label1);
            this.groupBoxGFSZ.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBoxGFSZ.Location = new System.Drawing.Point(12, 12);
            this.groupBoxGFSZ.Name = "groupBoxGFSZ";
            this.groupBoxGFSZ.Size = new System.Drawing.Size(226, 118);
            this.groupBoxGFSZ.TabIndex = 0;
            this.groupBoxGFSZ.TabStop = false;
            this.groupBoxGFSZ.Text = "Amplifier Settings";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Tahoma", 12F);
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(192, 81);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 19);
            this.label18.TabIndex = 45;
            this.label18.Text = "°C";
            // 
            // numericUpDownTemp
            // 
            this.numericUpDownTemp.DecimalPlaces = 1;
            this.numericUpDownTemp.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownTemp.Location = new System.Drawing.Point(110, 79);
            this.numericUpDownTemp.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDownTemp.Minimum = new decimal(new int[] {
            200,
            0,
            0,
            -2147483648});
            this.numericUpDownTemp.Name = "numericUpDownTemp";
            this.numericUpDownTemp.Size = new System.Drawing.Size(76, 26);
            this.numericUpDownTemp.TabIndex = 4;
            this.numericUpDownTemp.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDownTemp_MouseDoubleClick);
            // 
            // numericUpDownVswr
            // 
            this.numericUpDownVswr.DecimalPlaces = 1;
            this.numericUpDownVswr.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numericUpDownVswr.Location = new System.Drawing.Point(110, 32);
            this.numericUpDownVswr.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownVswr.Name = "numericUpDownVswr";
            this.numericUpDownVswr.Size = new System.Drawing.Size(76, 26);
            this.numericUpDownVswr.TabIndex = 3;
            this.numericUpDownVswr.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.numericUpDownVswr_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(7, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Temp Limit:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(7, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "VSWR Limit:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioBroad);
            this.groupBox1.Controls.Add(this.radioNarrow);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox1.Location = new System.Drawing.Point(244, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(176, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "REV | FWD";
            // 
            // radioBroad
            // 
            this.radioBroad.AutoSize = true;
            this.radioBroad.Location = new System.Drawing.Point(15, 79);
            this.radioBroad.Name = "radioBroad";
            this.radioBroad.Size = new System.Drawing.Size(50, 20);
            this.radioBroad.TabIndex = 1;
            this.radioBroad.TabStop = true;
            this.radioBroad.Text = "FWD";
            this.radioBroad.UseVisualStyleBackColor = true;
            // 
            // radioNarrow
            // 
            this.radioNarrow.AutoSize = true;
            this.radioNarrow.Location = new System.Drawing.Point(15, 34);
            this.radioNarrow.Name = "radioNarrow";
            this.radioNarrow.Size = new System.Drawing.Size(50, 20);
            this.radioNarrow.TabIndex = 0;
            this.radioNarrow.TabStop = true;
            this.radioNarrow.Text = "REV";
            this.radioNarrow.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk_battary);
            this.groupBox2.Controls.Add(this.radio_gpio_new);
            this.groupBox2.Controls.Add(this.radio_gpio_old);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox2.Location = new System.Drawing.Point(12, 136);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(226, 124);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "GPIO Switching";
            // 
            // chk_battary
            // 
            this.chk_battary.AutoSize = true;
            this.chk_battary.Location = new System.Drawing.Point(10, 83);
            this.chk_battary.Name = "chk_battary";
            this.chk_battary.Size = new System.Drawing.Size(163, 20);
            this.chk_battary.TabIndex = 5;
            this.chk_battary.Text = "Service detect AC";
            this.chk_battary.UseVisualStyleBackColor = true;
            // 
            // radio_gpio_new
            // 
            this.radio_gpio_new.AutoSize = true;
            this.radio_gpio_new.Location = new System.Drawing.Point(110, 30);
            this.radio_gpio_new.Name = "radio_gpio_new";
            this.radio_gpio_new.Size = new System.Drawing.Size(90, 20);
            this.radio_gpio_new.TabIndex = 4;
            this.radio_gpio_new.TabStop = true;
            this.radio_gpio_new.Text = "new GPIO";
            this.radio_gpio_new.UseVisualStyleBackColor = true;
            // 
            // radio_gpio_old
            // 
            this.radio_gpio_old.AutoSize = true;
            this.radio_gpio_old.Font = new System.Drawing.Font("宋体", 12F);
            this.radio_gpio_old.Location = new System.Drawing.Point(10, 30);
            this.radio_gpio_old.Name = "radio_gpio_old";
            this.radio_gpio_old.Size = new System.Drawing.Size(90, 20);
            this.radio_gpio_old.TabIndex = 2;
            this.radio_gpio_old.TabStop = true;
            this.radio_gpio_old.Text = "old GPIO";
            this.radio_gpio_old.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnFileManage);
            this.groupBox3.Controls.Add(this.btnParameters);
            this.groupBox3.Controls.Add(this.btnCompensation);
            this.groupBox3.Controls.Add(this.btnTest);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 12F);
            this.groupBox3.Location = new System.Drawing.Point(244, 136);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 174);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Others";
            // 
            // btnFileManage
            // 
            this.btnFileManage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFileManage.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFileManage.Location = new System.Drawing.Point(26, 131);
            this.btnFileManage.Name = "btnFileManage";
            this.btnFileManage.Size = new System.Drawing.Size(115, 30);
            this.btnFileManage.TabIndex = 67;
            this.btnFileManage.Text = "File Manage";
            this.btnFileManage.UseVisualStyleBackColor = true;
            this.btnFileManage.Click += new System.EventHandler(this.btnFileManage_Click);
            // 
            // btnParameters
            // 
            this.btnParameters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnParameters.Enabled = false;
            this.btnParameters.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnParameters.Location = new System.Drawing.Point(26, 94);
            this.btnParameters.Name = "btnParameters";
            this.btnParameters.Size = new System.Drawing.Size(115, 30);
            this.btnParameters.TabIndex = 66;
            this.btnParameters.Text = "Parameters";
            this.btnParameters.UseVisualStyleBackColor = true;
            this.btnParameters.Click += new System.EventHandler(this.btnParameters_Click);
            // 
            // btnCompensation
            // 
            this.btnCompensation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompensation.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCompensation.Location = new System.Drawing.Point(26, 57);
            this.btnCompensation.Name = "btnCompensation";
            this.btnCompensation.Size = new System.Drawing.Size(115, 30);
            this.btnCompensation.TabIndex = 65;
            this.btnCompensation.Text = "Compensation";
            this.btnCompensation.UseVisualStyleBackColor = true;
            this.btnCompensation.Click += new System.EventHandler(this.btnCompensation_Click);
            // 
            // btnTest
            // 
            this.btnTest.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTest.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.Location = new System.Drawing.Point(26, 20);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(115, 30);
            this.btnTest.TabIndex = 64;
            this.btnTest.Text = "Test Mode";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(44, 280);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 30);
            this.btnSave.TabIndex = 64;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(153, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 30);
            this.btnCancel.TabIndex = 65;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // GlobalConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 331);
            this.ControlBox = false;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxGFSZ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalConfiguration";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Global Configuration";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.GlobalConfiguration_Load);
            this.groupBoxGFSZ.ResumeLayout(false);
            this.groupBoxGFSZ.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVswr)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxGFSZ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownTemp;
        private System.Windows.Forms.NumericUpDown numericUpDownVswr;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioBroad;
        private System.Windows.Forms.RadioButton radioNarrow;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radio_gpio_new;
        private System.Windows.Forms.RadioButton radio_gpio_old;
        private System.Windows.Forms.CheckBox chk_battary;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCompensation;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnParameters;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnFileManage;
    }
}