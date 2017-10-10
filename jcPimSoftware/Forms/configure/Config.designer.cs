namespace jcPimSoftware
{
    partial class Config
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
            this.tclConfig = new System.Windows.Forms.TabControl();
            this.tpePim = new System.Windows.Forms.TabPage();
            this.tclConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // tclConfig
            // 
            this.tclConfig.Controls.Add(this.tpePim);
            this.tclConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tclConfig.Location = new System.Drawing.Point(0, 0);
            this.tclConfig.Name = "tclConfig";
            this.tclConfig.SelectedIndex = 0;
            this.tclConfig.Size = new System.Drawing.Size(773, 460);
            this.tclConfig.TabIndex = 6;
            // 
            // tpePim
            // 
            this.tpePim.BackColor = System.Drawing.SystemColors.Control;
            this.tpePim.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tpePim.Location = new System.Drawing.Point(4, 25);
            this.tpePim.Name = "tpePim";
            this.tpePim.Padding = new System.Windows.Forms.Padding(3);
            this.tpePim.Size = new System.Drawing.Size(765, 431);
            this.tpePim.TabIndex = 0;
            this.tpePim.Text = "Pim Tx/Rx Cal";
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 460);
            this.ControlBox = false;
            this.Controls.Add(this.tclConfig);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Config";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Config";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Config_Load);
            this.tclConfig.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tclConfig;
        private System.Windows.Forms.TabPage tpePim;

    }
}