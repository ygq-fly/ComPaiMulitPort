namespace jcPimSoftware
{
    partial class OkCancel
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
            this.b_Ok = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.l_Txt = new System.Windows.Forms.Label();
            this.pbxOkCancel = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxOkCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // b_Ok
            // 
            this.b_Ok.Location = new System.Drawing.Point(69, 71);
            this.b_Ok.Name = "b_Ok";
            this.b_Ok.Size = new System.Drawing.Size(74, 30);
            this.b_Ok.TabIndex = 0;
            this.b_Ok.Text = "确定";
            this.b_Ok.UseVisualStyleBackColor = true;
            this.b_Ok.Click += new System.EventHandler(this.b_Ok_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Location = new System.Drawing.Point(190, 71);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(74, 30);
            this.b_Cancel.TabIndex = 1;
            this.b_Cancel.Text = "取消";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // l_Txt
            // 
            this.l_Txt.AutoSize = true;
            this.l_Txt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.l_Txt.Location = new System.Drawing.Point(48, 24);
            this.l_Txt.Name = "l_Txt";
            this.l_Txt.Size = new System.Drawing.Size(192, 16);
            this.l_Txt.TabIndex = 2;
            this.l_Txt.Text = "     确定要退出程序吗？";
            // 
            // pbxOkCancel
            // 
            this.pbxOkCancel.Location = new System.Drawing.Point(12, 21);
            this.pbxOkCancel.Name = "pbxOkCancel";
            this.pbxOkCancel.Size = new System.Drawing.Size(32, 32);
            this.pbxOkCancel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxOkCancel.TabIndex = 3;
            this.pbxOkCancel.TabStop = false;
            // 
            // OkCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 123);
            this.ControlBox = false;
            this.Controls.Add(this.pbxOkCancel);
            this.Controls.Add(this.l_Txt);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_Ok);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OkCancel";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "消息";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OkCancel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxOkCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button b_Ok;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.Label l_Txt;
        private System.Windows.Forms.PictureBox pbxOkCancel;
    }
}