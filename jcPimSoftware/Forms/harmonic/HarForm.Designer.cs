namespace jcPimSoftware
{
    partial class HarForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HarForm));
            this.pnlIso = new System.Windows.Forms.Panel();
            this.lblNoise = new System.Windows.Forms.Label();
            this.lblRx = new System.Windows.Forms.Label();
            this.lblIsoDiv = new System.Windows.Forms.Label();
            this.lblTx = new System.Windows.Forms.Label();
            this.lblF = new System.Windows.Forms.Label();
            this.pnlWork = new System.Windows.Forms.Panel();
            this.pnlVswr = new System.Windows.Forms.Panel();
            this.pbxAutoscale = new System.Windows.Forms.PictureBox();
            this.pbxRead = new System.Windows.Forms.PictureBox();
            this.pbxSetting = new System.Windows.Forms.PictureBox();
            this.pbxSave = new System.Windows.Forms.PictureBox();
            this.pbxMark = new System.Windows.Forms.PictureBox();
            this.pbxFreq = new System.Windows.Forms.PictureBox();
            this.pbxCarrier2 = new System.Windows.Forms.PictureBox();
            this.pbxCarrier1 = new System.Windows.Forms.PictureBox();
            this.pbxStart = new System.Windows.Forms.PictureBox();
            this.pbxVswrBar = new System.Windows.Forms.PictureBox();
            this.pnlIsoTime = new System.Windows.Forms.Panel();
            this.lblSweep = new System.Windows.Forms.Label();
            this.pnlPlot = new System.Windows.Forms.Panel();
            this.pltHar = new jcXY2dPlotEx.XY2dPlotEx();
            this.pbxBackGround = new System.Windows.Forms.PictureBox();
            this.timPlayback = new System.Windows.Forms.Timer(this.components);
            this.pnlIso.SuspendLayout();
            this.pnlWork.SuspendLayout();
            this.pnlVswr.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutoscale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRead)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMark)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCarrier2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCarrier1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVswrBar)).BeginInit();
            this.pnlIsoTime.SuspendLayout();
            this.pnlPlot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxBackGround)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlIso
            // 
            this.pnlIso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.pnlIso.Controls.Add(this.lblNoise);
            this.pnlIso.Controls.Add(this.lblRx);
            this.pnlIso.Controls.Add(this.lblIsoDiv);
            this.pnlIso.Controls.Add(this.lblTx);
            this.pnlIso.Controls.Add(this.lblF);
            this.pnlIso.ForeColor = System.Drawing.Color.Black;
            this.pnlIso.Location = new System.Drawing.Point(20, 15);
            this.pnlIso.Margin = new System.Windows.Forms.Padding(4);
            this.pnlIso.Name = "pnlIso";
            this.pnlIso.Size = new System.Drawing.Size(985, 30);
            this.pnlIso.TabIndex = 0;
            // 
            // lblNoise
            // 
            this.lblNoise.AutoSize = true;
            this.lblNoise.BackColor = System.Drawing.Color.Transparent;
            this.lblNoise.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblNoise.ForeColor = System.Drawing.Color.Red;
            this.lblNoise.Location = new System.Drawing.Point(608, 6);
            this.lblNoise.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblNoise.Name = "lblNoise";
            this.lblNoise.Size = new System.Drawing.Size(129, 19);
            this.lblNoise.TabIndex = 24;
            this.lblNoise.Text = "Noise:-- dBm";
            // 
            // lblRx
            // 
            this.lblRx.AutoSize = true;
            this.lblRx.BackColor = System.Drawing.Color.Transparent;
            this.lblRx.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRx.ForeColor = System.Drawing.Color.Red;
            this.lblRx.Location = new System.Drawing.Point(446, 6);
            this.lblRx.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRx.Name = "lblRx";
            this.lblRx.Size = new System.Drawing.Size(99, 19);
            this.lblRx.TabIndex = 23;
            this.lblRx.Text = "Rx:-- dBm";
            // 
            // lblIsoDiv
            // 
            this.lblIsoDiv.AutoSize = true;
            this.lblIsoDiv.BackColor = System.Drawing.Color.Transparent;
            this.lblIsoDiv.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblIsoDiv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblIsoDiv.Location = new System.Drawing.Point(65, 6);
            this.lblIsoDiv.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIsoDiv.Name = "lblIsoDiv";
            this.lblIsoDiv.Size = new System.Drawing.Size(89, 19);
            this.lblIsoDiv.TabIndex = 21;
            this.lblIsoDiv.Text = "10dB/Div";
            // 
            // lblTx
            // 
            this.lblTx.AutoSize = true;
            this.lblTx.BackColor = System.Drawing.Color.Transparent;
            this.lblTx.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTx.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblTx.Location = new System.Drawing.Point(304, 6);
            this.lblTx.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTx.Name = "lblTx";
            this.lblTx.Size = new System.Drawing.Size(109, 19);
            this.lblTx.TabIndex = 19;
            this.lblTx.Text = "Tx:00.0dBm";
            // 
            // lblF
            // 
            this.lblF.AutoSize = true;
            this.lblF.BackColor = System.Drawing.Color.Transparent;
            this.lblF.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblF.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblF.Location = new System.Drawing.Point(162, 6);
            this.lblF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblF.Name = "lblF";
            this.lblF.Size = new System.Drawing.Size(109, 19);
            this.lblF.TabIndex = 18;
            this.lblF.Text = "F:000.0MHz";
            // 
            // pnlWork
            // 
            this.pnlWork.BackColor = System.Drawing.Color.White;
            this.pnlWork.Controls.Add(this.pnlVswr);
            this.pnlWork.Controls.Add(this.pnlIsoTime);
            this.pnlWork.Controls.Add(this.pnlPlot);
            this.pnlWork.Controls.Add(this.pnlIso);
            this.pnlWork.Controls.Add(this.pbxBackGround);
            this.pnlWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWork.Location = new System.Drawing.Point(0, 0);
            this.pnlWork.Name = "pnlWork";
            this.pnlWork.Size = new System.Drawing.Size(1024, 697);
            this.pnlWork.TabIndex = 3;
            // 
            // pnlVswr
            // 
            this.pnlVswr.BackColor = System.Drawing.SystemColors.ControlText;
            this.pnlVswr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlVswr.Controls.Add(this.pbxAutoscale);
            this.pnlVswr.Controls.Add(this.pbxRead);
            this.pnlVswr.Controls.Add(this.pbxSetting);
            this.pnlVswr.Controls.Add(this.pbxSave);
            this.pnlVswr.Controls.Add(this.pbxMark);
            this.pnlVswr.Controls.Add(this.pbxFreq);
            this.pnlVswr.Controls.Add(this.pbxCarrier2);
            this.pnlVswr.Controls.Add(this.pbxCarrier1);
            this.pnlVswr.Controls.Add(this.pbxStart);
            this.pnlVswr.Controls.Add(this.pbxVswrBar);
            this.pnlVswr.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlVswr.Location = new System.Drawing.Point(0, 628);
            this.pnlVswr.Name = "pnlVswr";
            this.pnlVswr.Size = new System.Drawing.Size(1024, 69);
            this.pnlVswr.TabIndex = 4;
            // 
            // pbxAutoscale
            // 
            this.pbxAutoscale.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxAutoscale.Image = ((System.Drawing.Image)(resources.GetObject("pbxAutoscale.Image")));
            this.pbxAutoscale.Location = new System.Drawing.Point(580, -2);
            this.pbxAutoscale.Name = "pbxAutoscale";
            this.pbxAutoscale.Size = new System.Drawing.Size(103, 69);
            this.pbxAutoscale.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxAutoscale.TabIndex = 64;
            this.pbxAutoscale.TabStop = false;
            this.pbxAutoscale.Tag = "6";
            this.pbxAutoscale.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxAutoscale.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxAutoscale.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxAutoscale_MouseClick);
            // 
            // pbxRead
            // 
            this.pbxRead.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxRead.Image = ((System.Drawing.Image)(resources.GetObject("pbxRead.Image")));
            this.pbxRead.Location = new System.Drawing.Point(775, -2);
            this.pbxRead.Name = "pbxRead";
            this.pbxRead.Size = new System.Drawing.Size(97, 69);
            this.pbxRead.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxRead.TabIndex = 63;
            this.pbxRead.TabStop = false;
            this.pbxRead.Tag = "8";
            this.pbxRead.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxRead.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxRead.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxRead_MouseClick);
            // 
            // pbxSetting
            // 
            this.pbxSetting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxSetting.Image = ((System.Drawing.Image)(resources.GetObject("pbxSetting.Image")));
            this.pbxSetting.Location = new System.Drawing.Point(872, -2);
            this.pbxSetting.Name = "pbxSetting";
            this.pbxSetting.Size = new System.Drawing.Size(129, 69);
            this.pbxSetting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxSetting.TabIndex = 62;
            this.pbxSetting.TabStop = false;
            this.pbxSetting.Tag = "9";
            this.pbxSetting.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxSetting.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxSetting.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxSetting_MouseClick);
            // 
            // pbxSave
            // 
            this.pbxSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxSave.Image = ((System.Drawing.Image)(resources.GetObject("pbxSave.Image")));
            this.pbxSave.Location = new System.Drawing.Point(683, -2);
            this.pbxSave.Name = "pbxSave";
            this.pbxSave.Size = new System.Drawing.Size(92, 69);
            this.pbxSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxSave.TabIndex = 61;
            this.pbxSave.TabStop = false;
            this.pbxSave.Tag = "7";
            this.pbxSave.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxSave.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxSave_MouseClick);
            // 
            // pbxMark
            // 
            this.pbxMark.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxMark.Image = ((System.Drawing.Image)(resources.GetObject("pbxMark.Image")));
            this.pbxMark.Location = new System.Drawing.Point(487, -2);
            this.pbxMark.Name = "pbxMark";
            this.pbxMark.Size = new System.Drawing.Size(93, 69);
            this.pbxMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxMark.TabIndex = 60;
            this.pbxMark.TabStop = false;
            this.pbxMark.Tag = "5";
            this.pbxMark.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxMark.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxMark.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxMark_MouseClick);
            // 
            // pbxFreq
            // 
            this.pbxFreq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxFreq.Image = ((System.Drawing.Image)(resources.GetObject("pbxFreq.Image")));
            this.pbxFreq.Location = new System.Drawing.Point(397, -2);
            this.pbxFreq.Name = "pbxFreq";
            this.pbxFreq.Size = new System.Drawing.Size(90, 69);
            this.pbxFreq.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxFreq.TabIndex = 59;
            this.pbxFreq.TabStop = false;
            this.pbxFreq.Tag = "4";
            this.pbxFreq.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxFreq.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxFreq.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxFreq_MouseClick);
            // 
            // pbxCarrier2
            // 
            this.pbxCarrier2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxCarrier2.Image = ((System.Drawing.Image)(resources.GetObject("pbxCarrier2.Image")));
            this.pbxCarrier2.Location = new System.Drawing.Point(268, -2);
            this.pbxCarrier2.Name = "pbxCarrier2";
            this.pbxCarrier2.Size = new System.Drawing.Size(129, 69);
            this.pbxCarrier2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxCarrier2.TabIndex = 58;
            this.pbxCarrier2.TabStop = false;
            this.pbxCarrier2.Tag = "3";
            this.pbxCarrier2.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxCarrier2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxCarrier2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxCarrier2_MouseClick);
            // 
            // pbxCarrier1
            // 
            this.pbxCarrier1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxCarrier1.Image = ((System.Drawing.Image)(resources.GetObject("pbxCarrier1.Image")));
            this.pbxCarrier1.Location = new System.Drawing.Point(145, -2);
            this.pbxCarrier1.Name = "pbxCarrier1";
            this.pbxCarrier1.Size = new System.Drawing.Size(123, 69);
            this.pbxCarrier1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxCarrier1.TabIndex = 57;
            this.pbxCarrier1.TabStop = false;
            this.pbxCarrier1.Tag = "2";
            this.pbxCarrier1.MouseLeave += new System.EventHandler(this.FuncBtn_MouseLeave);
            this.pbxCarrier1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FuncBtn_MouseMove);
            this.pbxCarrier1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxCarrier1_MouseClick);
            // 
            // pbxStart
            // 
            this.pbxStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbxStart.Image = ((System.Drawing.Image)(resources.GetObject("pbxStart.Image")));
            this.pbxStart.Location = new System.Drawing.Point(28, -2);
            this.pbxStart.Name = "pbxStart";
            this.pbxStart.Size = new System.Drawing.Size(117, 69);
            this.pbxStart.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxStart.TabIndex = 56;
            this.pbxStart.TabStop = false;
            this.pbxStart.Tag = "1";
            this.pbxStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbxStart_MouseClick);
            // 
            // pbxVswrBar
            // 
            this.pbxVswrBar.Image = ((System.Drawing.Image)(resources.GetObject("pbxVswrBar.Image")));
            this.pbxVswrBar.Location = new System.Drawing.Point(0, -2);
            this.pbxVswrBar.Name = "pbxVswrBar";
            this.pbxVswrBar.Size = new System.Drawing.Size(1024, 69);
            this.pbxVswrBar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbxVswrBar.TabIndex = 3;
            this.pbxVswrBar.TabStop = false;
            // 
            // pnlIsoTime
            // 
            this.pnlIsoTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.pnlIsoTime.Controls.Add(this.lblSweep);
            this.pnlIsoTime.ForeColor = System.Drawing.Color.Red;
            this.pnlIsoTime.Location = new System.Drawing.Point(20, 574);
            this.pnlIsoTime.Name = "pnlIsoTime";
            this.pnlIsoTime.Size = new System.Drawing.Size(985, 35);
            this.pnlIsoTime.TabIndex = 3;
            // 
            // lblSweep
            // 
            this.lblSweep.BackColor = System.Drawing.Color.Transparent;
            this.lblSweep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSweep.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSweep.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.lblSweep.Location = new System.Drawing.Point(0, 0);
            this.lblSweep.Margin = new System.Windows.Forms.Padding(0);
            this.lblSweep.Name = "lblSweep";
            this.lblSweep.Size = new System.Drawing.Size(985, 35);
            this.lblSweep.TabIndex = 15;
            this.lblSweep.Text = "Time Sweep";
            this.lblSweep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlPlot
            // 
            this.pnlPlot.BackColor = System.Drawing.Color.White;
            this.pnlPlot.Controls.Add(this.pltHar);
            this.pnlPlot.Location = new System.Drawing.Point(20, 47);
            this.pnlPlot.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPlot.Name = "pnlPlot";
            this.pnlPlot.Size = new System.Drawing.Size(985, 530);
            this.pnlPlot.TabIndex = 2;
            // 
            // pltHar
            // 
            this.pltHar.AixsLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(255)))), ((int)(((byte)(190)))));
            this.pltHar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.pltHar.BorderLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(146)))), ((int)(((byte)(52)))));
            this.pltHar.GridBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.pltHar.GridLineBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(39)))), ((int)(((byte)(22)))));
            this.pltHar.InnerMargin = 4;
            this.pltHar.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(115)))), ((int)(((byte)(146)))), ((int)(((byte)(52)))));
            this.pltHar.Location = new System.Drawing.Point(0, 0);
            this.pltHar.MajorLineWidth = 2;
            this.pltHar.Margin = new System.Windows.Forms.Padding(0);
            this.pltHar.MinorLineWidth = 0;
            this.pltHar.Name = "pltHar";
            this.pltHar.ShowTitle = false;
            this.pltHar.Size = new System.Drawing.Size(985, 531);
            this.pltHar.TabIndex = 1;
            this.pltHar.Title = "Plot XY 2d Demo";
            this.pltHar.TitleFont = new System.Drawing.Font("宋体", 9F);
            this.pltHar.TitleLabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(32)))), ((int)(((byte)(136)))));
            this.pltHar.XAxisTitle = "Frequency(MHz)";
            this.pltHar.XLabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(255)))), ((int)(((byte)(190)))));
            this.pltHar.XLabelFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pltHar.XMajorCount = 5;
            this.pltHar.XMajorLength = 10;
            this.pltHar.XMinorCount = 2;
            this.pltHar.XMinorLength = 5;
            this.pltHar.XShowLabel = true;
            this.pltHar.XShowMinor = true;
            this.pltHar.XShowTitle = false;
            this.pltHar.XTitleFont = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pltHar.YAxisTitle = "Phase Value(dBc)";
            this.pltHar.YLabelColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.pltHar.YLabelFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pltHar.YMajorCount = 5;
            this.pltHar.YMajorLength = 10;
            this.pltHar.YMinorCount = 2;
            this.pltHar.YMinorLength = 5;
            this.pltHar.YShowLabel = true;
            this.pltHar.YShowMinor = true;
            this.pltHar.YShowTitle = false;
            this.pltHar.YTitleFont = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pbxBackGround
            // 
            this.pbxBackGround.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxBackGround.Image = ((System.Drawing.Image)(resources.GetObject("pbxBackGround.Image")));
            this.pbxBackGround.Location = new System.Drawing.Point(0, 0);
            this.pbxBackGround.Name = "pbxBackGround";
            this.pbxBackGround.Size = new System.Drawing.Size(1024, 697);
            this.pbxBackGround.TabIndex = 0;
            this.pbxBackGround.TabStop = false;
            // 
            // timPlayback
            // 
            this.timPlayback.Interval = 250;
            this.timPlayback.Tick += new System.EventHandler(this.csvPlayback_Tick);
            // 
            // HarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(1024, 697);
            this.Controls.Add(this.pnlWork);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1024, 768);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1024, 697);
            this.Name = "HarForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SpectrumForm";
            this.Load += new System.EventHandler(this.HarForm_Load);
            this.pnlIso.ResumeLayout(false);
            this.pnlIso.PerformLayout();
            this.pnlWork.ResumeLayout(false);
            this.pnlVswr.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutoscale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxRead)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMark)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCarrier2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCarrier1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxVswrBar)).EndInit();
            this.pnlIsoTime.ResumeLayout(false);
            this.pnlPlot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxBackGround)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlIso;
        private System.Windows.Forms.Panel pnlWork;
        private System.Windows.Forms.PictureBox pbxBackGround;
        private System.Windows.Forms.Panel pnlPlot;
        private System.Windows.Forms.Label lblTx;
        private System.Windows.Forms.Label lblF;
        private System.Windows.Forms.Label lblIsoDiv;
        private System.Windows.Forms.Panel pnlIsoTime;
        private System.Windows.Forms.Label lblSweep;
        private System.Windows.Forms.Label lblRx;
        public jcXY2dPlotEx.XY2dPlotEx pltHar;
        private System.Windows.Forms.Panel pnlVswr;
        private System.Windows.Forms.PictureBox pbxAutoscale;
        private System.Windows.Forms.PictureBox pbxRead;
        private System.Windows.Forms.PictureBox pbxSetting;
        private System.Windows.Forms.PictureBox pbxSave;
        private System.Windows.Forms.PictureBox pbxMark;
        private System.Windows.Forms.PictureBox pbxFreq;
        private System.Windows.Forms.PictureBox pbxCarrier2;
        private System.Windows.Forms.PictureBox pbxStart;
        private System.Windows.Forms.PictureBox pbxVswrBar;
        private System.Windows.Forms.Timer timPlayback;
        private System.Windows.Forms.Label lblNoise;
        private System.Windows.Forms.PictureBox pbxCarrier1;

    }
}