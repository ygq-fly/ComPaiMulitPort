namespace jcPimSoftware
{
    partial class FormOffset
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
            this.plotOffset = new jcXY2dPlotEx.XY2dPlotEx();
            this.btnEnable = new System.Windows.Forms.Button();
            this.btnDisable = new System.Windows.Forms.Button();
            this.groupBoxOffset = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.lblRBW = new System.Windows.Forms.Label();
            this.lblBind = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkTest = new System.Windows.Forms.CheckBox();
            this.groupBoxOffset.SuspendLayout();
            this.SuspendLayout();
            // 
            // plotOffset
            // 
            this.plotOffset.AixsLineColor = System.Drawing.Color.Black;
            this.plotOffset.BackColor = System.Drawing.SystemColors.Control;
            this.plotOffset.BorderLineColor = System.Drawing.Color.Blue;
            this.plotOffset.GridBackColor = System.Drawing.SystemColors.Control;
            this.plotOffset.GridLineBackColor = System.Drawing.Color.SkyBlue;
            this.plotOffset.InnerMargin = 10;
            this.plotOffset.LineColor = System.Drawing.Color.Green;
            this.plotOffset.Location = new System.Drawing.Point(10, 84);
            this.plotOffset.MajorLineWidth = 2;
            this.plotOffset.MinorLineWidth = 1;
            this.plotOffset.Name = "plotOffset";
            this.plotOffset.ShowTitle = false;
            this.plotOffset.Size = new System.Drawing.Size(468, 246);
            this.plotOffset.TabIndex = 0;
            this.plotOffset.Title = "Plot XY 2d Demo";
            this.plotOffset.TitleFont = new System.Drawing.Font("宋体", 9F);
            this.plotOffset.TitleLabelColor = System.Drawing.Color.Red;
            this.plotOffset.XAxisTitle = "X Aixs";
            this.plotOffset.XLabelColor = System.Drawing.Color.Red;
            this.plotOffset.XLabelFont = new System.Drawing.Font("宋体", 9F);
            this.plotOffset.XMajorCount = 5;
            this.plotOffset.XMajorLength = 10;
            this.plotOffset.XMinorCount = 2;
            this.plotOffset.XMinorLength = 5;
            this.plotOffset.XShowLabel = false;
            this.plotOffset.XShowMinor = true;
            this.plotOffset.XShowTitle = false;
            this.plotOffset.XTitleFont = new System.Drawing.Font("宋体", 12F);
            this.plotOffset.YAxisTitle = "Y Aixs";
            this.plotOffset.YLabelColor = System.Drawing.Color.Red;
            this.plotOffset.YLabelFont = new System.Drawing.Font("宋体", 9F);
            this.plotOffset.YMajorCount = 5;
            this.plotOffset.YMajorLength = 10;
            this.plotOffset.YMinorCount = 2;
            this.plotOffset.YMinorLength = 5;
            this.plotOffset.YShowLabel = true;
            this.plotOffset.YShowMinor = true;
            this.plotOffset.YShowTitle = false;
            this.plotOffset.YTitleFont = new System.Drawing.Font("宋体", 12F);
            // 
            // btnEnable
            // 
            this.btnEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnable.Location = new System.Drawing.Point(306, 355);
            this.btnEnable.Name = "btnEnable";
            this.btnEnable.Size = new System.Drawing.Size(75, 30);
            this.btnEnable.TabIndex = 1;
            this.btnEnable.Text = "Enable";
            this.btnEnable.UseVisualStyleBackColor = true;
            this.btnEnable.Click += new System.EventHandler(this.btnEnable_Click);
            // 
            // btnDisable
            // 
            this.btnDisable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDisable.Location = new System.Drawing.Point(399, 355);
            this.btnDisable.Name = "btnDisable";
            this.btnDisable.Size = new System.Drawing.Size(75, 30);
            this.btnDisable.TabIndex = 2;
            this.btnDisable.Text = "Disable";
            this.btnDisable.UseVisualStyleBackColor = true;
            this.btnDisable.Click += new System.EventHandler(this.btnDisable_Click);
            // 
            // groupBoxOffset
            // 
            this.groupBoxOffset.Controls.Add(this.btnOpen);
            this.groupBoxOffset.Controls.Add(this.txtFilePath);
            this.groupBoxOffset.Location = new System.Drawing.Point(12, 12);
            this.groupBoxOffset.Name = "groupBoxOffset";
            this.groupBoxOffset.Size = new System.Drawing.Size(468, 54);
            this.groupBoxOffset.TabIndex = 3;
            this.groupBoxOffset.TabStop = false;
            this.groupBoxOffset.Text = "Offset Data File";
            // 
            // btnOpen
            // 
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Location = new System.Drawing.Point(386, 16);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 30);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Browse";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.txtFilePath.Location = new System.Drawing.Point(6, 18);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(363, 26);
            this.txtFilePath.TabIndex = 0;
            // 
            // lblRBW
            // 
            this.lblRBW.AutoSize = true;
            this.lblRBW.ForeColor = System.Drawing.Color.Blue;
            this.lblRBW.Location = new System.Drawing.Point(79, 69);
            this.lblRBW.Name = "lblRBW";
            this.lblRBW.Size = new System.Drawing.Size(56, 16);
            this.lblRBW.TabIndex = 4;
            this.lblRBW.Text = "lblRBW";
            // 
            // lblBind
            // 
            this.lblBind.AutoSize = true;
            this.lblBind.ForeColor = System.Drawing.Color.Red;
            this.lblBind.Location = new System.Drawing.Point(340, 69);
            this.lblBind.Name = "lblBind";
            this.lblBind.Size = new System.Drawing.Size(64, 16);
            this.lblBind.TabIndex = 5;
            this.lblBind.Text = "lblBind";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(79, 333);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(417, 333);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "3000MHz";
            // 
            // chkTest
            // 
            this.chkTest.AutoSize = true;
            this.chkTest.Location = new System.Drawing.Point(36, 361);
            this.chkTest.Name = "chkTest";
            this.chkTest.Size = new System.Drawing.Size(91, 20);
            this.chkTest.TabIndex = 8;
            this.chkTest.Text = "TestMode";
            this.chkTest.UseVisualStyleBackColor = true;
            this.chkTest.CheckedChanged += new System.EventHandler(this.chkTest_CheckedChanged);
            // 
            // FormOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 405);
            this.Controls.Add(this.chkTest);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBind);
            this.Controls.Add(this.lblRBW);
            this.Controls.Add(this.groupBoxOffset);
            this.Controls.Add(this.btnDisable);
            this.Controls.Add(this.btnEnable);
            this.Controls.Add(this.plotOffset);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormOffset";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Offset Option";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormOffset_Load);
            this.groupBoxOffset.ResumeLayout(false);
            this.groupBoxOffset.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private jcXY2dPlotEx.XY2dPlotEx plotOffset;
        private System.Windows.Forms.Button btnEnable;
        private System.Windows.Forms.Button btnDisable;
        private System.Windows.Forms.GroupBox groupBoxOffset;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label lblRBW;
        private System.Windows.Forms.Label lblBind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkTest;
    }
}