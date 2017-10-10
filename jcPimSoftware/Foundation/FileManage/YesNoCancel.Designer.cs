namespace jcPimSoftware
{
    partial class YesNoCancel
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
            this.btn_Yes = new System.Windows.Forms.Button();
            this.btn_No = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.labeInfo = new System.Windows.Forms.Label();
            this.pbxYesNo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxYesNo)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Yes
            // 
            this.btn_Yes.Location = new System.Drawing.Point(53, 74);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(70, 30);
            this.btn_Yes.TabIndex = 0;
            this.btn_Yes.Text = "Yes";
            this.btn_Yes.UseVisualStyleBackColor = true;
            this.btn_Yes.Click += new System.EventHandler(this.btn_Yes_Click);
            // 
            // btn_No
            // 
            this.btn_No.Location = new System.Drawing.Point(137, 74);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(69, 30);
            this.btn_No.TabIndex = 1;
            this.btn_No.Text = "No";
            this.btn_No.UseVisualStyleBackColor = true;
            this.btn_No.Click += new System.EventHandler(this.btn_No_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(220, 74);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(73, 30);
            this.btn_Cancel.TabIndex = 2;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // labeInfo
            // 
            this.labeInfo.AutoSize = true;
            this.labeInfo.Location = new System.Drawing.Point(73, 31);
            this.labeInfo.Name = "labeInfo";
            this.labeInfo.Size = new System.Drawing.Size(208, 16);
            this.labeInfo.TabIndex = 3;
            this.labeInfo.Text = "Do you want to select it?";
            // 
            // pbxYesNo
            // 
            this.pbxYesNo.Location = new System.Drawing.Point(17, 24);
            this.pbxYesNo.Name = "pbxYesNo";
            this.pbxYesNo.Size = new System.Drawing.Size(32, 32);
            this.pbxYesNo.TabIndex = 4;
            this.pbxYesNo.TabStop = false;
            // 
            // YesNoCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 126);
            this.ControlBox = false;
            this.Controls.Add(this.pbxYesNo);
            this.Controls.Add(this.labeInfo);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.btn_Yes);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "YesNoCancel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Warning";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.YesNoCancel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxYesNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label labeInfo;
        private System.Windows.Forms.PictureBox pbxYesNo;
    }
}