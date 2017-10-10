namespace jcPimSoftware
{
    partial class Error
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
            this.error_Btn = new System.Windows.Forms.Button();
            this.lab = new System.Windows.Forms.Label();
            this.pbxError = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxError)).BeginInit();
            this.SuspendLayout();
            // 
            // error_Btn
            // 
            this.error_Btn.Location = new System.Drawing.Point(96, 62);
            this.error_Btn.Name = "error_Btn";
            this.error_Btn.Size = new System.Drawing.Size(85, 30);
            this.error_Btn.TabIndex = 0;
            this.error_Btn.Text = "Ok";
            this.error_Btn.UseVisualStyleBackColor = true;
            this.error_Btn.Click += new System.EventHandler(this.error_Btn_Click);
            // 
            // lab
            // 
            this.lab.AutoSize = true;
            this.lab.Location = new System.Drawing.Point(53, 24);
            this.lab.Name = "lab";
            this.lab.Size = new System.Drawing.Size(128, 16);
            this.lab.TabIndex = 1;
            this.lab.Text = "    Error！！！";
            // 
            // pbxError
            // 
            this.pbxError.Location = new System.Drawing.Point(18, 19);
            this.pbxError.Name = "pbxError";
            this.pbxError.Size = new System.Drawing.Size(32, 32);
            this.pbxError.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxError.TabIndex = 2;
            this.pbxError.TabStop = false;
            // 
            // Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 114);
            this.ControlBox = false;
            this.Controls.Add(this.pbxError);
            this.Controls.Add(this.lab);
            this.Controls.Add(this.error_Btn);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Error";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "错误";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Error_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button error_Btn;
        private System.Windows.Forms.Label lab;
        private System.Windows.Forms.PictureBox pbxError;
    }
}