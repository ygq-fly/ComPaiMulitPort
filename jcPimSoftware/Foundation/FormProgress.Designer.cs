namespace jcPimSoftware.Foundation
{
    partial class FormProgress
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
            this.components = new System.ComponentModel.Container();
            this.pgbBar = new System.Windows.Forms.ProgressBar();
            this.btnOK = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblAc = new System.Windows.Forms.Label();
            this.lblErrorInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgbBar
            // 
            this.pgbBar.Location = new System.Drawing.Point(0, 751);
            this.pgbBar.Name = "pgbBar";
            this.pgbBar.Size = new System.Drawing.Size(1024, 18);
            this.pgbBar.Step = 1;
            this.pgbBar.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(501, 711);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 24);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // timer
            // 
            this.timer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.ForeColor = System.Drawing.Color.Turquoise;
            this.lblInfo.Location = new System.Drawing.Point(24, 59);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 16);
            this.lblInfo.TabIndex = 6;
            // 
            // lblAc
            // 
            this.lblAc.AutoSize = true;
            this.lblAc.ForeColor = System.Drawing.Color.Red;
            this.lblAc.Location = new System.Drawing.Point(24, 20);
            this.lblAc.Name = "lblAc";
            this.lblAc.Size = new System.Drawing.Size(0, 16);
            this.lblAc.TabIndex = 7;
            // 
            // lblErrorInfo
            // 
            this.lblErrorInfo.AutoSize = true;
            this.lblErrorInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblErrorInfo.ForeColor = System.Drawing.Color.Red;
            this.lblErrorInfo.Location = new System.Drawing.Point(24, 672);
            this.lblErrorInfo.Name = "lblErrorInfo";
            this.lblErrorInfo.Size = new System.Drawing.Size(0, 14);
            this.lblErrorInfo.TabIndex = 8;
            // 
            // FormProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.lblErrorInfo);
            this.Controls.Add(this.lblAc);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pgbBar);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormProgress";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pgbBar;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblAc;
        public System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblErrorInfo;
    }
}