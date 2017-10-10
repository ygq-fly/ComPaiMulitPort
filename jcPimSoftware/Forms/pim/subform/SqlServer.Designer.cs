namespace jcPimSoftware
{
    partial class SqlServer
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tB_Serial = new System.Windows.Forms.TextBox();
            this.tB_Type = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_Tester = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tB_JcID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Value = new System.Windows.Forms.Label();
            this.lbl_Peak = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_UpLoad = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(55, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 44);
            this.button1.TabIndex = 4;
            this.button1.Text = "上传";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(236, 346);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 44);
            this.button2.TabIndex = 5;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "产品序列号：";
            // 
            // tB_Serial
            // 
            this.tB_Serial.Location = new System.Drawing.Point(162, 23);
            this.tB_Serial.Name = "tB_Serial";
            this.tB_Serial.Size = new System.Drawing.Size(140, 21);
            this.tB_Serial.TabIndex = 1;
            this.tB_Serial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tB_Serial_KeyDown);
            // 
            // tB_Type
            // 
            this.tB_Type.Location = new System.Drawing.Point(162, 55);
            this.tB_Type.Name = "tB_Type";
            this.tB_Type.Size = new System.Drawing.Size(140, 21);
            this.tB_Type.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "产品类型：";
            // 
            // tB_Tester
            // 
            this.tB_Tester.Location = new System.Drawing.Point(162, 87);
            this.tB_Tester.Name = "tB_Tester";
            this.tB_Tester.ReadOnly = true;
            this.tB_Tester.Size = new System.Drawing.Size(140, 21);
            this.tB_Tester.TabIndex = 6;
            this.tB_Tester.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "操作员：";
            // 
            // tB_JcID
            // 
            this.tB_JcID.Location = new System.Drawing.Point(162, 119);
            this.tB_JcID.Name = "tB_JcID";
            this.tB_JcID.ReadOnly = true;
            this.tB_JcID.Size = new System.Drawing.Size(140, 21);
            this.tB_JcID.TabIndex = 7;
            this.tB_JcID.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(41, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "互调仪ID：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Value);
            this.groupBox1.Controls.Add(this.lbl_Peak);
            this.groupBox1.Location = new System.Drawing.Point(12, 174);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(337, 119);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数值:";
            // 
            // lbl_Value
            // 
            this.lbl_Value.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Value.Location = new System.Drawing.Point(6, 76);
            this.lbl_Value.Name = "lbl_Value";
            this.lbl_Value.Size = new System.Drawing.Size(325, 37);
            this.lbl_Value.TabIndex = 10;
            this.lbl_Value.Text = "@935MHz/2*43dBm/Freq/PIM3/900M\r\nLimit:-120dBm";
            this.lbl_Value.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Peak
            // 
            this.lbl_Peak.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Peak.Location = new System.Drawing.Point(6, 17);
            this.lbl_Peak.Name = "lbl_Peak";
            this.lbl_Peak.Size = new System.Drawing.Size(325, 52);
            this.lbl_Peak.TabIndex = 9;
            this.lbl_Peak.Text = "Peak: -165.3dBm/PASS";
            this.lbl_Peak.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tB_JcID);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tB_Tester);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tB_Type);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tB_Serial);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(337, 156);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设置:";
            // 
            // cb_UpLoad
            // 
            this.cb_UpLoad.AutoSize = true;
            this.cb_UpLoad.Location = new System.Drawing.Point(118, 312);
            this.cb_UpLoad.Name = "cb_UpLoad";
            this.cb_UpLoad.Size = new System.Drawing.Size(114, 16);
            this.cb_UpLoad.TabIndex = 3;
            this.cb_UpLoad.Text = "是否上传PDF报告";
            this.cb_UpLoad.UseVisualStyleBackColor = true;
            // 
            // SqlServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 402);
            this.Controls.Add(this.cb_UpLoad);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(377, 440);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(377, 440);
            this.Name = "SqlServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "上传";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.SqlServer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_Serial;
        private System.Windows.Forms.TextBox tB_Type;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tB_Tester;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tB_JcID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_Peak;
        private System.Windows.Forms.Label lbl_Value;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cb_UpLoad;
    }
}