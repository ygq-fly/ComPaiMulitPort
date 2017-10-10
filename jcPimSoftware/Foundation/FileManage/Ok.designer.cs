namespace jcPimSoftware
{
    partial class Ok
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
            this.bt_Ok = new System.Windows.Forms.Button();
            this.la_Txt = new System.Windows.Forms.Label();
            this.pbxOk = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOk)).BeginInit();
            this.SuspendLayout();
            // 
            // bt_Ok
            // 
            this.bt_Ok.Location = new System.Drawing.Point(75, 62);
            this.bt_Ok.Name = "bt_Ok";
            this.bt_Ok.Size = new System.Drawing.Size(75, 30);
            this.bt_Ok.TabIndex = 0;
            this.bt_Ok.Text = "确定";
            this.bt_Ok.UseVisualStyleBackColor = true;
            this.bt_Ok.Click += new System.EventHandler(this.bt_Ok_Click);
            // 
            // la_Txt
            // 
            this.la_Txt.AutoSize = true;
            this.la_Txt.Location = new System.Drawing.Point(72, 21);
            this.la_Txt.Name = "la_Txt";
            this.la_Txt.Size = new System.Drawing.Size(88, 16);
            this.la_Txt.TabIndex = 1;
            this.la_Txt.Text = "复制完成！";
            // 
            // pbxOk
            // 
            this.pbxOk.Location = new System.Drawing.Point(12, 12);
            this.pbxOk.Name = "pbxOk";
            this.pbxOk.Size = new System.Drawing.Size(32, 32);
            this.pbxOk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxOk.TabIndex = 4;
            this.pbxOk.TabStop = false;
            // 
            // Ok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 102);
            this.ControlBox = false;
            this.Controls.Add(this.pbxOk);
            this.Controls.Add(this.la_Txt);
            this.Controls.Add(this.bt_Ok);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ok";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Ok_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button bt_Ok;
        private System.Windows.Forms.Label la_Txt;
        private System.Windows.Forms.PictureBox pbxOk;
    }
}