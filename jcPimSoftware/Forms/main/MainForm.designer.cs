namespace jcPimSoftware
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlBar = new System.Windows.Forms.Panel();
            this.pbxSpectrum = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pbxPim = new System.Windows.Forms.PictureBox();
            this.pbxQuit = new System.Windows.Forms.PictureBox();
            this.pbxCnfg = new System.Windows.Forms.PictureBox();
            this.pbxHelp = new System.Windows.Forms.PictureBox();
            this.pbxBar = new System.Windows.Forms.PictureBox();
            this.pbxHarmonic = new System.Windows.Forms.PictureBox();
            this.pbxIsolation = new System.Windows.Forms.PictureBox();
            this.pbxVswr = new System.Windows.Forms.PictureBox();
            this.pnlModule = new System.Windows.Forms.Panel();
            this.t = new System.Windows.Forms.Timer(this.components);
            this.pnlBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpectrum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQuit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCnfg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHelp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHarmonic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIsolation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVswr)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBar
            // 
            this.pnlBar.BackColor = System.Drawing.Color.Black;
            this.pnlBar.Controls.Add(this.pbxSpectrum);
            this.pnlBar.Controls.Add(this.label2);
            this.pnlBar.Controls.Add(this.label1);
            this.pnlBar.Controls.Add(this.lblTime);
            this.pnlBar.Controls.Add(this.pbxPim);
            this.pnlBar.Controls.Add(this.pbxQuit);
            this.pnlBar.Controls.Add(this.pbxCnfg);
            this.pnlBar.Controls.Add(this.pbxHelp);
            this.pnlBar.Controls.Add(this.pbxBar);
            this.pnlBar.Controls.Add(this.pbxHarmonic);
            this.pnlBar.Controls.Add(this.pbxIsolation);
            this.pnlBar.Controls.Add(this.pbxVswr);
            this.pnlBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBar.Location = new System.Drawing.Point(0, 0);
            this.pnlBar.Margin = new System.Windows.Forms.Padding(4);
            this.pnlBar.Name = "pnlBar";
            this.pnlBar.Size = new System.Drawing.Size(1024, 71);
            this.pnlBar.TabIndex = 0;
            // 
            // pbxSpectrum
            // 
            this.pbxSpectrum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxSpectrum.Image = ((System.Drawing.Image)(resources.GetObject("pbxSpectrum.Image")));
            this.pbxSpectrum.Location = new System.Drawing.Point(103, 5);
            this.pbxSpectrum.Name = "pbxSpectrum";
            this.pbxSpectrum.Size = new System.Drawing.Size(116, 69);
            this.pbxSpectrum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxSpectrum.TabIndex = 70;
            this.pbxSpectrum.TabStop = false;
            this.pbxSpectrum.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxSpectrum_MouseClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.LightGray;
            this.label2.Image = ((System.Drawing.Image)(resources.GetObject("label2.Image")));
            this.label2.Location = new System.Drawing.Point(680, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 20);
            this.label2.TabIndex = 81;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightGray;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(500, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 80;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTime.ForeColor = System.Drawing.Color.LightGray;
            this.lblTime.Image = ((System.Drawing.Image)(resources.GetObject("lblTime.Image")));
            this.lblTime.Location = new System.Drawing.Point(840, 29);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(0, 20);
            this.lblTime.TabIndex = 79;
            // 
            // pbxPim
            // 
            this.pbxPim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxPim.Image = ((System.Drawing.Image)(resources.GetObject("pbxPim.Image")));
            this.pbxPim.Location = new System.Drawing.Point(27, 5);
            this.pbxPim.Name = "pbxPim";
            this.pbxPim.Size = new System.Drawing.Size(80, 69);
            this.pbxPim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxPim.TabIndex = 69;
            this.pbxPim.TabStop = false;
            this.pbxPim.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxPim_MouseClick);
            // 
            // pbxQuit
            // 
            this.pbxQuit.BackColor = System.Drawing.Color.Transparent;
            this.pbxQuit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxQuit.Image = ((System.Drawing.Image)(resources.GetObject("pbxQuit.Image")));
            this.pbxQuit.Location = new System.Drawing.Point(409, 5);
            this.pbxQuit.Name = "pbxQuit";
            this.pbxQuit.Size = new System.Drawing.Size(84, 69);
            this.pbxQuit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxQuit.TabIndex = 75;
            this.pbxQuit.TabStop = false;
            this.pbxQuit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxQuit_MouseClick);
            // 
            // pbxCnfg
            // 
            this.pbxCnfg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxCnfg.Image = ((System.Drawing.Image)(resources.GetObject("pbxCnfg.Image")));
            this.pbxCnfg.Location = new System.Drawing.Point(219, 5);
            this.pbxCnfg.Name = "pbxCnfg";
            this.pbxCnfg.Size = new System.Drawing.Size(100, 69);
            this.pbxCnfg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxCnfg.TabIndex = 73;
            this.pbxCnfg.TabStop = false;
            this.pbxCnfg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxCnfg_MouseClick);
            // 
            // pbxHelp
            // 
            this.pbxHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxHelp.Image = ((System.Drawing.Image)(resources.GetObject("pbxHelp.Image")));
            this.pbxHelp.Location = new System.Drawing.Point(319, 5);
            this.pbxHelp.Name = "pbxHelp";
            this.pbxHelp.Size = new System.Drawing.Size(90, 69);
            this.pbxHelp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxHelp.TabIndex = 74;
            this.pbxHelp.TabStop = false;
            this.pbxHelp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxHelp_MouseClick);
            // 
            // pbxBar
            // 
            this.pbxBar.BackColor = System.Drawing.Color.Transparent;
            this.pbxBar.Image = ((System.Drawing.Image)(resources.GetObject("pbxBar.Image")));
            this.pbxBar.Location = new System.Drawing.Point(-1, 5);
            this.pbxBar.Name = "pbxBar";
            this.pbxBar.Size = new System.Drawing.Size(1024, 69);
            this.pbxBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbxBar.TabIndex = 68;
            this.pbxBar.TabStop = false;
            // 
            // pbxHarmonic
            // 
            this.pbxHarmonic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxHarmonic.Image = ((System.Drawing.Image)(resources.GetObject("pbxHarmonic.Image")));
            this.pbxHarmonic.Location = new System.Drawing.Point(448, 5);
            this.pbxHarmonic.Name = "pbxHarmonic";
            this.pbxHarmonic.Size = new System.Drawing.Size(122, 69);
            this.pbxHarmonic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxHarmonic.TabIndex = 0;
            this.pbxHarmonic.TabStop = false;
            this.pbxHarmonic.Visible = false;
            this.pbxHarmonic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxHarmonic_MouseClick);
            // 
            // pbxIsolation
            // 
            this.pbxIsolation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxIsolation.Image = ((System.Drawing.Image)(resources.GetObject("pbxIsolation.Image")));
            this.pbxIsolation.Location = new System.Drawing.Point(232, 5);
            this.pbxIsolation.Name = "pbxIsolation";
            this.pbxIsolation.Size = new System.Drawing.Size(124, 69);
            this.pbxIsolation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxIsolation.TabIndex = 71;
            this.pbxIsolation.TabStop = false;
            this.pbxIsolation.Visible = false;
            this.pbxIsolation.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxIsolation_MouseClick);
            // 
            // pbxVswr
            // 
            this.pbxVswr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxVswr.Image = ((System.Drawing.Image)(resources.GetObject("pbxVswr.Image")));
            this.pbxVswr.Location = new System.Drawing.Point(356, 5);
            this.pbxVswr.Name = "pbxVswr";
            this.pbxVswr.Size = new System.Drawing.Size(92, 69);
            this.pbxVswr.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxVswr.TabIndex = 72;
            this.pbxVswr.TabStop = false;
            this.pbxVswr.Visible = false;
            this.pbxVswr.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxVswr_MouseClick);
            // 
            // pnlModule
            // 
            this.pnlModule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlModule.Location = new System.Drawing.Point(0, 71);
            this.pnlModule.Name = "pnlModule";
            this.pnlModule.Size = new System.Drawing.Size(1024, 699);
            this.pnlModule.TabIndex = 1;
            // 
            // t
            // 
            this.t.Enabled = true;
            this.t.Interval = 1000;
            this.t.Tick += new System.EventHandler(this.t_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1024, 770);
            this.Controls.Add(this.pnlModule);
            this.Controls.Add(this.pnlBar);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1032, 802);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 736);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlBar.ResumeLayout(false);
            this.pnlBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSpectrum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxQuit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCnfg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHelp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxHarmonic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxIsolation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVswr)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBar;
        private System.Windows.Forms.Panel pnlModule;
        private System.Windows.Forms.PictureBox pbxBar;
        private System.Windows.Forms.PictureBox pbxPim;
        private System.Windows.Forms.PictureBox pbxQuit;
        private System.Windows.Forms.PictureBox pbxCnfg;
        private System.Windows.Forms.PictureBox pbxHelp;
        private System.Windows.Forms.Timer t;
        private System.Windows.Forms.PictureBox pbxHarmonic;
        private System.Windows.Forms.PictureBox pbxIsolation;
        private System.Windows.Forms.PictureBox pbxVswr;
        private System.Windows.Forms.PictureBox pbxSpectrum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
    }
}