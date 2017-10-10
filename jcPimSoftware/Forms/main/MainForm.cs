using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AnalyzerApparatus.Gui.Isolation;


namespace jcPimSoftware
{
    public partial class MainForm : Form
    {

        private SpectrumLib.ISpectrum ISpectrumObj = null;
        private SpectrumLib.Models.ScanModel model = null;

        #region 跟踪并控制活动模块窗体的可见与不可见
        private readonly int ModuleCount = 5;

        private bool[] ActiveModules;
        private void ResetActiveModules()
        {
            for(int i = 0; i < ActiveModules.Length; i++)
                ActiveModules[i] = false;           
        }

        private void SetActiveModules(int index)
        {
            ResetActiveModules();
            ActiveModules[index] = true;
        }
       

        /// <summary>
        /// 初始化，将全部功能按钮的贴图设置为不活动的
        /// </summary>
        private void LoadInactiveImages()
        {
            pbxPim.Image = ImagesManage.GetImage("main", "pim_in.gif");
            pbxSpectrum.Image = ImagesManage.GetImage("main", "spectrum_in.gif");
            pbxIsolation.Image = ImagesManage.GetImage("main", "isolation_in.gif");
            pbxVswr.Image = ImagesManage.GetImage("main", "vswr_in.gif");
            pbxHarmonic.Image = ImagesManage.GetImage("main", "harmonic_in.gif");
            pbxCnfg.Image = ImagesManage.GetImage("main", "config_in.gif");
            pbxHelp.Image = ImagesManage.GetImage("main", "lock_in.gif");
        }

        /// <summary>
        /// 隐藏不活的模块的窗体
        /// </summary>
        private void HideInActiveModules()
        {
            if (frmPim != null)
            {
                if (!ActiveModules[0])
                    frmPim.Hide();
            }

            if (frmSpectrum != null)
            {
                if (!ActiveModules[1])
                    frmSpectrum.Hide();
            }

            if (frmIsolation != null)
            {
                if (!ActiveModules[2])
                    frmIsolation.Hide();
            }

            if (frmVswr != null)
            {
                if (!ActiveModules[3])
                    frmVswr.Hide();
            }

            if (frmHar != null)
            {
                if (!ActiveModules[4])
                    frmHar.Hide();
            }
           
        }
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化负则跟踪活动模块的定长数组
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            ActiveModules = new bool[ModuleCount];

            ResetActiveModules();

            PimActive();
        }
        #endregion

        #region 建立活动模块的窗体
        PimForm frmPim = null;
        private void PimActive()
        {
            if (frmPim == null)
            {
                frmPim = new PimForm();
                this.Controls.Add(frmPim);
                frmPim.Parent = this.pnlModule;
            }

            SetActiveModules(0);
            frmPim.pltPim.SetMarkText(frmPim.PimMarkText);
            frmPim.pltPim.SetPeakText(frmPim.PimPeakText);
            this.SuspendLayout();

            LoadInactiveImages();
            pbxPim.Image = ImagesManage.GetImage("main", "pim.gif");

            frmPim.Show();

            HideInActiveModules();

            this.ResumeLayout(true);
        }

        SpectrumForm frmSpectrum = null;
        private void SpectrumActive()
        {
            if (frmSpectrum == null)
            {
                frmSpectrum = new SpectrumForm();
                this.Controls.Add(frmSpectrum);
                frmSpectrum.Parent = this.pnlModule;
            }

            SetActiveModules(1);
            frmSpectrum.plot.SetMarkText(frmSpectrum.MakeupMarkText);
            frmSpectrum.plot.SetPeakText(frmSpectrum.MakeupPeakText);

            this.SuspendLayout();

            LoadInactiveImages();
            pbxSpectrum.Image = ImagesManage.GetImage("main", "spectrum.gif");

            frmSpectrum.Show();

            HideInActiveModules();

            this.ResumeLayout(true);
        }

        IsolationForm frmIsolation = null;

        private void IsolationActive()
        {
            if (frmIsolation == null)
            {
                frmIsolation = new IsolationForm();

                this.Controls.Add(frmIsolation);

                frmIsolation.Parent = this.pnlModule;
            }

            SetActiveModules(2);
            frmIsolation.pltIso.SetMarkText(frmIsolation.Sweep_MarkText);
            frmIsolation.pltIso.SetMarkText(frmIsolation.Fixed_MarkText);

            this.SuspendLayout();

            LoadInactiveImages();
            pbxIsolation.Image = ImagesManage.GetImage("main", "isolation.gif");

            frmIsolation.Show();

            HideInActiveModules();

            this.ResumeLayout(true);
        }

        VswrForm frmVswr = null;
        private void VswrActive()
        {
            if (frmVswr == null)
            {
                frmVswr = new VswrForm();

                this.Controls.Add(frmVswr);

                frmVswr.Parent = this.pnlModule;
            }

            SetActiveModules(3);
            frmVswr.pltVswr.SetMarkText(frmVswr.VswrMarkText);
            this.SuspendLayout();

            LoadInactiveImages();
            pbxVswr.Image = ImagesManage.GetImage("main", "vswr.gif");

            frmVswr.Show();

            HideInActiveModules();

            this.ResumeLayout(true);
        }

        HarForm frmHar = null;
        private void HarActive()
        {
            if (frmHar == null)
            {
                frmHar = new HarForm();

                this.Controls.Add(frmHar);

                frmHar.Parent = this.pnlModule;
            }

            SetActiveModules(4);
            frmHar.pltHar.SetMarkText(frmHar.Sweep_MarkText);
            frmHar.pltHar.SetMarkText(frmHar.Fixed_MarkText);

            this.SuspendLayout();

            LoadInactiveImages();
            pbxHarmonic.Image = ImagesManage.GetImage("main", "harmonic.gif");

            frmHar.Show();

            HideInActiveModules();

            this.ResumeLayout(true);
        }


        Config frmConfig = null;
        private void CnfgActive()
        {
            pbxCnfg.Image = ImagesManage.GetImage("main", "config.gif");

            if (frmConfig == null)
            {
                frmConfig = new Config();
                frmConfig.ShowDialog();
                frmConfig.Dispose();
                frmConfig = null;
            }
        }
        #endregion

        #region 功能按钮事件

        private void pbxPim_MouseClick(object sender, MouseEventArgs e)
        {
            SwitchFun(EFunctionName.PIM);
        }

        private void pbxSpectrum_MouseClick(object sender, MouseEventArgs e)
        {
            SwitchFun(EFunctionName.SPECTRUM);
        }

        private void pbxIsolation_MouseClick(object sender, MouseEventArgs e)
        {
            SwitchFun(EFunctionName.ISOLATION);
        }

        private void pbxVswr_MouseClick(object sender, MouseEventArgs e)
        {
            SwitchFun(EFunctionName.VSWR);
        }

        private void pbxHarmonic_MouseClick(object sender, MouseEventArgs e)
        {
            SwitchFun(EFunctionName.HARMONIC);
        }

        private void pbxCnfg_MouseClick(object sender, MouseEventArgs e)
        {
            SwitchFun(EFunctionName.CONFIG);
            GlobalConfiguration gc = new GlobalConfiguration();
            gc.ShowDialog();
            if (frmPim != null)
            {
                frmPim.ChangePic();
            }
            if (frmSpectrum != null)
            {
                frmSpectrum.ChangeRevFwd();
            }
            if (App_Configure.Cnfgs.Spectrum == 2)
            {
                SetSpectrum();
            }  
        }

        private void pbxQuit_MouseClick(object sender, MouseEventArgs e)
        {
            YesNo y = new YesNo("Warning", "      Quit ?", "Yes", "No");
            y.lb_Txt.Location = new Point(35, 27);
            y.Size = new Size(204, 150);
            y.b_Yes.Location = new Point(12, 63);
            y.b_No.Location = new Point(105, 63);
            DialogResult dr = y.ShowDialog();
            if (dr == DialogResult.Yes)
            {
                SwitchFun(EFunctionName.CONFIG);
                //保存频谱配置
                App_Settings.spc.StoreSettings();
                //Program.mSwitch.Disconnect();
                StartForm sf = new StartForm("Closing......");
                sf.infoMsg = "Port is Closing...";
                sf.ShowDialog();

             
                this.Close();
            }
        }

        private void pbxHelp_MouseClick(object sender, MouseEventArgs e)
        {
            SystemLock syslockfrm = new SystemLock();
            syslockfrm.ShowDialog();
        }

        #endregion

        #region 功能模块切换处理

        #region 各个功能模块枚举

        /// <summary>
        /// 功能模块枚举
        /// </summary>
        enum EFunctionName
        {
            PIM = 0,
            SPECTRUM = 1,
            ISOLATION = 2,
            VSWR = 3,
            HARMONIC = 4,
            CONFIG = 5
        }

        #endregion 

        /// <summary>
        /// 功能模块
        /// </summary>
        private EFunctionName FunType = EFunctionName.PIM;

        /// <summary>
        /// 切换功能模块
        /// </summary>
        /// <param name="fun">目标功能模块</param>
        private void SwitchFun(EFunctionName fun)
        {
            EFunctionName currentFun = FunType;     //当前功能模块

            //停止当前功能模块的运行
            if (!currentFun.Equals(fun))
            {
                switch (currentFun)
                {
                    case EFunctionName.PIM:
                        frmPim.BreakSweep(1000);
                        break;
                    case EFunctionName.SPECTRUM:
                        frmSpectrum.BreakSweep(1000);
                        break;
                    case EFunctionName.ISOLATION:
                        frmIsolation.BreakSweep(1000);
                        break;
                    case EFunctionName.VSWR:
                        frmVswr.BreakSweep(1000);
                        break;
                    case EFunctionName.HARMONIC:
                        frmHar.BreakSweep(1000);
                        break;
                    case EFunctionName.CONFIG:
                        if (currentFun.Equals(EFunctionName.PIM))
                            frmPim.BreakSweep(1000);
                        if (currentFun.Equals(EFunctionName.SPECTRUM))
                            frmSpectrum.BreakSweep(1000);
                        if (currentFun.Equals(EFunctionName.ISOLATION))
                            frmIsolation.BreakSweep(1000);
                        if (currentFun.Equals(EFunctionName.VSWR))
                            frmVswr.BreakSweep(1000);
                        if (currentFun.Equals(EFunctionName.HARMONIC))
                            frmHar.BreakSweep(1000);
                        break;
                }

                //切换到目标功能模块
                switch (fun)
                {
                    case EFunctionName.PIM:
                        //GPIO.Rev();
                        //Deli频谱仪界面切换设置
                        if (App_Configure.Cnfgs.Spectrum == 2)
                        {
                            SetSpectrum();
                        }  
                        PimActive();
                        break;
                    case EFunctionName.SPECTRUM:
                        bool f = GPIO.Rev();
                        SpectrumActive();
                        if (f)
                        {
                            frmSpectrum.lblChannel.Text = "Rev";
                        }
                        break;
                    case EFunctionName.ISOLATION:
                        //GPIO.Fwd();
                        //IsolationActive();
                        //AttenuatorPic ap = new AttenuatorPic();
                        //ap.ShowDialog();
                        break;
                    case EFunctionName.VSWR:
                        //GPIO.Fwd();
                        //VswrActive();
                        break;
                    case EFunctionName.HARMONIC:
                        //GPIO.Fwd();
                        //HarActive();
                        break;
                    default:
                        break;
                }
                if (!fun.Equals(EFunctionName.CONFIG))
                    FunType = fun;
            }
        }

        #endregion

        private void t_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            label1.Text = "SN: " + App_Configure.Cnfgs.SN;
            label2.Text = "CAL: " + App_Configure.Cnfgs.CAL;
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                pbxSpectrum.Visible = false;
                pbxCnfg.Location = new Point(pbxCnfg.Location.X-pbxSpectrum.Size.Width, pbxCnfg.Location.Y);
                pbxHelp.Location = new Point(pbxHelp.Location.X - pbxCnfg.Size.Width, pbxHelp.Location.Y);
                pbxQuit.Location = new Point(pbxQuit.Location.X - pbxHelp.Size.Width, pbxQuit.Location.Y);
            }
        }

        /// <summary>设置频谱仪(Deli)
        /// 
        /// </summary>
        private void SetSpectrum()
        {
            if (ISpectrumObj == null)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.Deli(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                model = new SpectrumLib.Models.ScanModel();
            }
            GetValueofScanModel();
        }

        /// <summary>设置model值(Deli)
        /// 
        /// </summary>
        private void GetValueofScanModel()
        {
            object o;
            model.StartFreq = App_Settings.pim.F1;
            model.EndFreq = model.StartFreq + 2 * App_Settings.pim.Scanband;
            model.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;
            model.Att = App_Settings.pim.Att_Spc;
            model.Rbw = App_Settings.pim.Rbw_Spc;
            model.Vbw = App_Settings.pim.Vbw_Spc;
            model.Deli_averagecount = 6;
            model.Deli_detector = "AVERage";
            model.Deli_ref = -50;
            model.Deli_refoffset = 0;
            model.Deli_startspe = 1;//频谱仪是否第一次启动
            model.Deli_isSpectrum = true;//频谱模式

            o = model;
            if (!ISpectrumObj.Setting(o))
            {
                ISpectrumObj.ResetStatus();
                Log.WriteLog("Deli set failed!", Log.EFunctionType.SPECTRUM);
            }
        }
    }
}