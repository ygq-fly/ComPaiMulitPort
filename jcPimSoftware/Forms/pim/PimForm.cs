using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using jcXY2dPlotEx;
using System.Reflection;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using SpectrumLib;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using com_io_ctl;

namespace jcPimSoftware
{
    public partial class PimForm : Form, ISweep
    {
        //双频机当前端口 1:rev1  2:fwd1 3:rev2 4:fwd2
        public static  int port1_rev_fwd = 1;
        public static int pimOrder = 3;
        public float rxOffset = 0;
        int x_stop = 30;
        DateTime PimStat;
        int PimTimeCount = 0;
        //bool isWork_Zero = false;
        bool isReadCsv = false;

        Dictionary<int, Control> control_port = new Dictionary<int, Control>();
        //List<int> CurrentSelectPort = new List<int>() { 1, 2, 3, 4 };
        List<int> CurrentSelectPort=new List<int>();
        /// <summary>
        /// 是否切换了阶数
        /// </summary>
        bool drawplitm = true;

        /// <summary>
        /// 当前开关
        /// </summary>
        public  int current_port = 1;
        public int saveport = 1;

        //双反射记录数据类
        CurrentPortData cpd;
        /// <summary>
        /// 功放1等待句柄
        /// </summary>
        private ManualResetEvent power1_Handle=new ManualResetEvent(false);

        /// <summary>
        /// 功放2等待句柄
        /// </summary>
        private ManualResetEvent power2_Handle=new ManualResetEvent(false);

        //========

        /// <summary>
        /// 频谱分析接口
        /// </summary>
        public ISpectrum ISpectrumObj;
        sock so;
        //bool usesever=true ;
        string ipaddress;
        object obj;
        public static bool _istcp = false;
        //============
        #region 变量定义
        //offset
        float[] offset_range = new float[8] { -101, -104, -107, -109, -111, -113, -115, -117 };
        float[] offset_init = new float[7] { 0, 0, 0, 0, 0, 0, 0 };
        float offset_Val = 0;
        float offset_P = 0;
        bool offset_DoneRange = false;
        bool offset_DoneNormal = false;
        bool offset_DoneAvg = false;
        bool offset_DoneAvgType = false;//false:one-list;true:two-list
        List<float> offset_listAvg1 = new List<float>();
        List<float> offset_listAvg2 = new List<float>();
        string offset_FileName = "D:\\JcWindowServer.ini";
        bool offset_RemoteCtr = false;

        /// <summary>
        /// 远程服务
        /// </summary>
        SocketAdmin sa;
        //SocketAdmin sa = new SocketAdmin();
        Dictionary<string, TcpClient> dic = new Dictionary<string, TcpClient>();
        bool isOpenRemoteCtlServer = false;

        /// <summary>
        /// sql服务
        /// </summary>
        SqlInfo si = new SqlInfo();
        ProductInfo CurrentPi = new ProductInfo();
        bool isSqlConn = false;
        bool isOpenSqlConnServer = false;

        /// <summary>
        /// PIM定义
        /// </summary>
        private Pim_Sweep SweepObj;
        PimServices ps = new PimServices();
        List<CsvReport_Pim_Entry> listCrp = new List<CsvReport_Pim_Entry>();
        List<CsvReport_Pim_Entry> readListCrp = new List<CsvReport_Pim_Entry>();
        CsvReport_PIVH_Header header;
        /// <summary>
        /// 扫描类定义
        /// </summary>
        FreqSweepParam fsp = null;
        TimeSweepParam tsp = null;
        SweepParams p;
        SweepResult sr;
        /// <summary>
        /// 获取功放状态变量
        /// </summary>
        PowerStatus ps1;
        PowerStatus ps2;
        /// <summary>
        /// 错误信息变量
        /// </summary>
        private RFErrors rfErrors_1;
        private RFErrors rfErrors_2;
        private Settings_Pim settings;
        float limitValue = 0F;
        int sweepTimes = 0;
        /// <summary>
        /// pltPim.SetYStartStop
        /// </summary>
        float start = 0f;
        float stop = 0f;
        /// <summary> 用于单位转换
        /// 用于单位转换
        /// </summary>
        DataTable defaultDatalbe = new DataTable();
        DataTable dtWdbm = new DataTable();
        DataTable dtWdbc = new DataTable();
        DataTable dtDbmM = new DataTable();
        DataTable dtDbmC = new DataTable();
        /// <summary>
        /// 用于判断是dBc，还是dBm
        /// </summary>
        bool isFlagDbc = false;
        /// <summary>
        /// 用于判断是W，还是dBm
        /// </summary>
        bool isFlagW = false;
        /// <summary>
        /// 用于判断图片切换是扫频还是点频
        /// </summary>
        bool ft = false;
        /// <summary>
        /// 是否启用回放功能
        /// </summary>
        bool isRead = false;
        /// <summary>
        /// 用于判断是点频，还是扫频
        /// </summary>
        bool flag = false;
        /// <summary>
        /// 定义收到消息后的变量
        /// </summary>
        int sum = 0;
        int g = 0;
        bool sweepEnd = false;
        PointF[] pf = new PointF[1];
        PointF[] pf0 = new PointF[1];
        PointF[] pf1 = new PointF[2];
        PointF[] pf2 = new PointF[2];
        PointF[] pf3 = new PointF[2];
        PointF[] pf4 = new PointF[2];
        PointF[] pf5 = new PointF[1];
        PointF[] pf6 = new PointF[1];
        /// <summary>
        /// 图片资源文件夹名称
        /// </summary>
        private readonly string PicfolderName = "pim";
        /// <summary>
        /// 标识分析循环已经启动
        /// </summary>
        private bool Sweeping = false;
        /// <summary>
        /// mark
        /// </summary>
        int[] s = new int[5] { -1, -1, -1, -1, -1 };

        private const int wRvalue = -200;
        private int ERRORNUM = 0;
        /// <summary>扫描序列1的点数
        /// 
        /// </summary>
        private int NumofItem1 = 0;

        /// <summary>
        /// 设置两条带内线的开始边距
        /// </summary>
        private float margin = 0;
        //根据不同的阶数，来设置Rx的范围
        private float RxStart = 0;
        private float RxEnd = 0;
        //图标放大的坐标
        float x1 = 0;
        float x2 = 0;
        string ApiMaxVlaue = "";
        #endregion

        #region 当前已扫描类型 true扫频 false扫时
        /// <summary>
        /// 当前已扫描类型 true扫频 false扫时
        /// </summary>
        private bool scanType = true;
        #endregion

        #region WndProc消息
        //========
        IntPtr itpr;
        int click_Num = 0;
        int click_Num2 = 0;
        int click_Num3 = 0;
        object lock_obj = new object();
        class mesage_da
        {
            public   List<string> ds=new List<string>();
        }
        mesage_da ms = new mesage_da();
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MessageID.PIM_SUCCED: //成功收到一个点
                    {
                        PimSucced(m.WParam.ToInt32(), m.LParam.ToInt32());
                        break;
                    }
                case MessageID.RF_ERROR:  //失败
                    {                      
                        SweepObj.Clone(ref ps1, ref ps2, ref sr, ref rfErrors_1, ref rfErrors_2);
                        if (ERRORNUM > 0 && (rfErrors_1.RF_VswrError || rfErrors_1.RF_VswrError
                            || rfErrors_2.RF_VswrError || rfErrors_2.RF_RftErr))
                        {
                            //====用于远程连接的时候使用
                            string error_info = "1." + rfErrors_1.ToString() + "\n\r2." + rfErrors_2.ToString();
                            SweepObj.errmessage.Add(error_info);
                            //=================
                            MessageBox.Show(this, error_info);
                            PimBreakSweep(2000);
                            ChangeBtnPic(pbxStart, "start_in.gif");
                            ChangeBtnPic(pbxPeak, "peak_in.gif");
                            Sweeping = false;
                            break;
                        }
                        else
                        {
                            if (rfErrors_1.RF_CurrError || rfErrors_1.RF_TempError || rfErrors_1.RF_TimeOut ||
                                rfErrors_2.RF_CurrError || rfErrors_2.RF_TempError || rfErrors_2.RF_TimeOut)
                            {
                                string error_info = "1." + rfErrors_1.ToString() + "\n\r2." + rfErrors_2.ToString();
                                SweepObj.errmessage.Add(error_info);
                                //=================
                                MessageBox.Show(this, error_info);
                                PimBreakSweep(2000);
                                ChangeBtnPic(pbxStart, "start_in.gif");
                                ChangeBtnPic(pbxPeak, "peak_in.gif");
                                Sweeping = false;
                            }
                            ERRORNUM = 1; break;

                        }
                    }
                case MessageID.RF_VSWR_WARNINIG:
                    {
                        SweepObj.Clone(ref ps1, ref ps2, ref sr, ref rfErrors_1, ref rfErrors_2);

                        string error_info = "1." + rfErrors_1.ToString() + "\n\r2." + rfErrors_2.ToString();
                        SweepObj.errmessage.Add(error_info);
                        MessageBox.Show(this, error_info);

                        PimBreakSweep(2000);
                        ChangeBtnPic(pbxStart, "start_in.gif");
                        ChangeBtnPic(pbxPeak, "peak_in.gif");
                        Sweeping = false;
                        break;
                    }
                case MessageID.SPECTRUM_ERROR://频谱失败
                    {
                        PimBreakSweep(2000);
                        Sweeping = false;
                        ChangeBtnPic(pbxStart, "start_in.gif");
                        ChangeBtnPic(pbxPeak, "peak_in.gif");
                        pltPim.SetXAutoScroll(false);       
                        //
                        SweepObj.Spectrum_Succed();
                        //
                        string error_info = "Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!";
                        SweepObj.errmessage.Add(error_info);
                        MessageBox.Show(this, error_info);
                        break;
                    }
                case MessageID.PIM_SWEEP_DONE://扫描次数完成消息
                    {
                        if (!sweepEnd)
                        {
                            sweepTimes++;

                            if (settings.SweepType == SweepType.Freq_Sweep)
                            {
                                if (sweepTimes == settings.FreqSweepTimes)
                                {
                                    sweepTimes = 0;
                                    ChangeBtnPic(pbxStart, "start_in.gif");
                                    ChangeBtnPic(pbxPeak, "peak_in.gif");
                                    Sweeping = false;
                                    ScrollEnable(false, false, false);
                                }
                                else
                                {
                                    if (sweepTimes > 0)
                                    {
                                        L = 0;
                                        Q = 0;
                                        pltPim.Clear();
                                        pltPower.Clear();
                                        lblN1.Text = "";
                                        lblN2.Text = "";
                                        lblN3.Text = "";
                                        lblN4.Text = "";
                                        pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                                    }
                                }
                            }
                            else
                            {
                                if (sweepTimes == settings.TimeSweepTimes)
                                {
                                    sweepTimes = 0;
                                    ChangeBtnPic(pbxStart, "start_in.gif");
                                    ChangeBtnPic(pbxPeak, "peak_in.gif");
                                    Sweeping = false;
                                    ScrollEnable(false, true, true);
                                }
                                else
                                {
                                    if (sweepTimes > 0)
                                    {
                                        pltPim.SetXStartStop(0, x_stop);
                                        pltPim.Clear();
                                        pltPower.Clear();
                                        lblN1.Text = "";
                                        lblN2.Text = "";
                                        lblN3.Text = "";
                                        lblN4.Text = "";
                                        pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                                    }
                                }
                            }
                        }

                        break;
                    }
                case MessageID.SPECTRUEME_SUCCED://频谱成功
                    {
                        SweepObj.Spectrum_Succed();
                        break;
                    }
                case MessageID.RF_SUCCED_ALL:
                    {
                        if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr1)
                            SweepObj.Power1_Succed();
                        else if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr2)
                            SweepObj.Power2_Succed();
                        break;
                    }
                case MessageID.PIM_SWEEP_CLOSE:
                    {
                        sweepTimes = 0;
                        ChangeBtnPic(pbxStart, "start_in.gif");
                        ChangeBtnPic(pbxPeak, "peak_in.gif");
                        offset_RemoteCtr = false;
                        Sweeping = false;
                        sweepEnd = false;
                        server_sweepEnd = true;
                        if (isSqlConn && isOpenSqlConnServer)
                        {
                            CurrentPi.pim = maxdbm.ToString();
                            CurrentPi.result = lblPf.Text;
                            CurrentPi.fpim = pimTxt.Text;
                            CurrentPi.power = settings.Tx.ToString();
                            CurrentPi.mode = settings.SweepType.ToString();
                            CurrentPi.order = settings.PimOrder.ToString();
                            CurrentPi.limit = settings.Limit_Pim.ToString();
                            CurrentPi.device = "jc" + App_Configure.Cnfgs.SN;
                            CurrentPi.tester = si.PimOperator;
                            //SqlServer ss = new SqlServer(CurrentPi, si, new delegate_SavePDF(this.SavePdf));
                            //ss.ShowDialog();
                        }

                          if (App_Configure.Cnfgs.Ms_switch_port_count > 0)
                        {
                            //MessageBox.Show("1");
                            DataTable dt = new DataTable();
                            dt = dtDbmC.Copy();
                            DataTable dtm = new DataTable();
                            dtm = dtDbmM.Copy();
                            DataTable wdt = new DataTable();
                            wdt = dtWdbc.Copy();
                            DataTable wdtm = new DataTable();
                            wdtm = dtWdbm.Copy();
                            CsvReport_Pim_Entry[] temp = listCrp.ToArray();
                            bool sweep_ = settings.SweepType == SweepType.Freq_Sweep ? true : false;
                            //MessageBox.Show("存："+temp.Length.ToString());
                            try
                            {
                                float plt_max = max;
                                float plt_min = min;
                              
                                int n=0;
                                if (sweep_)
                                    n = fsp.Items1.Length;
                                else
                                    n = tsp.N;

                              //Image image = SaveImage(this);
                              //Image pimImage = SaveImage(pltPim);
                                //if (settings.PimSchema == ImSchema.REV)
                                //    cpd.GetPortData(saveport - 1, dt, dtm, wdt, wdtm, start, stop, RxStart, RxEnd, temp, sweep_, n, plt_max, plt_min, SaveImage(this), SaveImage(pltPim), lblPf.Text, NumofItem1);
                                //else
                                //cpd.GetPortData(saveport - 1, dt, dtm, wdt, wdtm, start, stop, RxStart, RxEnd, temp, sweep_, n, plt_max, plt_min, SaveImage(this), SaveImage(pltPim), lblPf.Text, NumofItem1);
                                if (settings.PimSchema == ImSchema.REV)
                                    cpd.GetPortData(saveport - 1, dt, dtm, wdt, wdtm, start, stop, RxStart, RxEnd, temp, sweep_, n, plt_max, plt_min, lblPf.Text, NumofItem1);
                                else
                                    cpd.GetPortData(saveport - 1, dt, dtm, wdt, wdtm, start, stop, RxStart, RxEnd, temp, sweep_, n, plt_max, plt_min, lblPf.Text, NumofItem1);
                                //SaveJpg_somePort(saveport - 1);
                                //SaveJpg_somePort2(saveport - 1);
                                //image.Dispose();
                                //pimImage.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                          }
                        break;
                    }
                case 0x0014: return;
                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
       
        #endregion

        #region 构造函数

        public PimForm()
        {
            //======
            itpr = this.Handle;
            //====
            //构造隔离度模块配置对象
            this.settings = new Settings_Pim("");

            //从默认隔离度模块配置对象复制设置项的值
            App_Settings.pim.Clone(this.settings);
            New_Pim_Sweep();
            InitializeComponent();

            //启用总是使用小格分隔线绘制网格，TRUE启用，FALSE关闭
            pltPim.SetAlwaysMinorLine(true);
            pltPower.SetAlwaysMinorLine(true);

            //启用网格拖动功能，TRUE启用，FALSE关闭
            pltPim.SetSampling(true);
            pltPim.Resume();
            pltPower.Resume();

            this.TopLevel = false;
            this.ShowInTaskbar = false;
            this.Dock = DockStyle.Fill;

            Type typeDgv = dgvPim.GetType();
            Type typePower = pbxPower.GetType();
            PropertyInfo piDgv = typeDgv.GetProperty("DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic);

            PropertyInfo piPower = typePower.GetProperty("DoubleBuffered",
               BindingFlags.Instance | BindingFlags.NonPublic);

            piDgv.SetValue(dgvPim, true, null);
            piPower.SetValue(pbxPower, true, null);
        }
        #endregion

        #region 窗体事件
        private void PimForm_Load(object sender, EventArgs e)
        {
            
            Setting();       //PIM初始化
            DgvIni();    
            //dgv表格初始化，默认显示7行
           
            if (App_Configure.Cnfgs.Ms_switch_port_count>0)
            {
                PortControlIni();
                ChangePortOffset(0);
                plIM.Location = new Point(826, 301);
                ChangeBtnPic(pbxFwd, "rev2_in.jpg");

                cpd = new CurrentPortData(App_Configure.Cnfgs.Ms_switch_port_count);
                if (App_Configure.Cnfgs.Ms_switch_port_count == 1)
                {

                    pbxFwd.Visible = false;
                }
                else
                {
                    pbxAllStart.Visible = true;
                }
                ////label1.Text = "  PORT1";
            }
            else
            {
                plIM.Location = new Point(826, 301);
            }

            for (int i = 0; i < App_Configure.Cnfgs.Ms_switch_port_count; i++)
            {
                CurrentSelectPort.Add(i + 1);
            }

            if (App_Configure.Cnfgs.Mode <=1)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = false;
            }
            x_stop = settings.TimeSweepPoints;
        }

        /////////////////ygq
        /// <summary>
        /// 开关控件初始化
        /// </summary>
        void PortControlIni()
        {
            int count = 0;
            control_port.Add(count++, pbxPort1);
            control_port.Add(count++, pbxPort2);
            control_port.Add(count++, pbxPort3);
            control_port.Add(count++, pbxPort4);
            control_port.Add(count++, pictureBox5);
            control_port.Add(count++, pictureBox4);
            //panel1.Visible = true;
            pbxFwd.Visible = false;
            plIM.Visible = false;
            plIM.Location = new Point(plIM.Location.X, 301);
            //ChangeBtnPic(pbxPort1, "port1.jpg");
          
          
            Setport_methodN(current_port);
            pblFt.Visible = false;
            if (settings.SweepType == SweepType.Time_Sweep)
                ChangeBtnPic(pictureBox6, "timetofreq.gif");
            else
                ChangeBtnPic(pictureBox6, "freqtotime.gif");
            for (int i = 0; i < App_Configure.Cnfgs.Ms_switch_port_count; i++)
            {
                control_port[i].Enabled = true;
                control_port[i].Visible = true;
                //currenport[i] = 1;
            }
            //settings.List_port = li;
            ChangeBtnPic(pbxPort1, "port1.jpg");

        }

        private void sa_ReachedEventHandler2(object sender, ReachedEventArgs e)
        {
            string _addr = sender as string;   
            string value="";
            if (e.msg.key.ToUpper() == "SET")
            {
                string name = GetName_String( e.msg.json, ref value);
                string sendmessge = handle_set(name, value);
                sa.OnSend(sendmessge, sender as string);
            }
            else if (e.msg.key.ToLower() == "exec")
            {
                handle_exec(e.msg.json, sender as string);
            }
            else
            {
                sa.OnSend("SET ERROR\r\n", sender as string);
            }
      

        }

        internal void Prepare()
        {

            // 建立频谱分析对象
            if (App_Configure.Cnfgs.Spectrum== SpectrumType.SPECAT2)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }

            else if (App_Configure.Cnfgs.Spectrum == SpectrumType.IRDSH)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }
            else if (App_Configure.Cnfgs.Spectrum == SpectrumType.Deli)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.Deli(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }
            else if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.FanShuang(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }

        }


        void SetPIMCommand(string value)
        {
            if (value.Contains("?"))
            {
                string str = value.Substring(0, value.Length - 2);
                switch (str.ToUpper())
                {
                    case "FREQ":
                        PointF[] PaintPointFs = (PointF[])ISpectrumObj.GetSpectrumData();
                        float x = PaintPointFs[0].X;
                        break;
                    case "RBW":
                        int rbw = App_Settings.spc.Rbw;
                        break;
                    case "ATT":
                        int att = App_Settings.spc.Att;
                        break;
                    case "CH":
                        int chanel = App_Configure.Cnfgs.Channel;
                        break;
                    case "DATA":
                        float pim_value = GetSpeRev();
                        break;
                    case "MODE":
                        SweepType stype = settings.SweepType;
                        break;


                }
            }
            else
            {
                string str = value.Remove(value.Length - 1, 1);
                string[] str_arr = str.Split(' ');
                switch (str_arr[0].ToUpper())
                { 
                    case "FREQ":
                        double f = Convert.ToDouble(str_arr[1].Remove(str_arr[1].Length - 3, 3));
                         SpectrumLib.Models.ScanModel ScanModel;
                         ScanModel = new SpectrumLib.Models.ScanModel();
                        ScanModel.StartFreq = f - App_Settings.pim.Scanband;
                        ScanModel.EndFreq = f + App_Settings.pim.Scanband;
                        ISpectrumObj.Setting(ScanModel);
                        break;

                
                }
            }
        
        }

        private float GetSpeRev()
        {
            int maxIndex = 0;
            float max = float.MinValue;
            PointF[] PaintPointFs = (PointF[])ISpectrumObj.GetSpectrumData();
            for (int i = 0; i < PaintPointFs.Length; i++)
            {
                if (PaintPointFs[i].Y > max)
                {
                    max = PaintPointFs[i].Y;
                    maxIndex = i;
                }
            }

            max = OffsetSpec(PaintPointFs[maxIndex].X, PaintPointFs[maxIndex].Y);
            return max;
        }

        private float OffsetSpec(float freq, float p)
        {
            float revP = p;

                if (settings.PimSchema==ImSchema.REV)
                {
                        revP = Rx_Tables.Offset(freq, FuncModule.PIM, true) + p - App_Settings.spc.RxRef;
                }
                else
                {
                        revP = Rx_Tables.Offset(freq, FuncModule.PIM, false) + p - App_Settings.spc.RxRef;
                }

            return (float)Math.Round(revP, 2);
        }

        private void sa_ReachedEventHandler_MS(object sender, ReachedEventArgs e)
        {
            string _addr = sender as string;
            string value = "";
            switch (e.msg.key.ToUpper())
            { 
                case "JC":
                    value = e.msg.json;
                   string[] str_list = value.Split(':');
                   value = str_list[1];
                    string SettingsCategory=str_list[0].ToUpper();//
                    if (SettingsCategory == "PIM")
                    {
                        SetPIMCommand(value);
                    }
                    else if (SettingsCategory == "SIG")
                    {
                    
                    }
                    else if (SettingsCategory == "ADMIN")
                    {

                    }
                    else
                    { 
                    
                    }
                    break;
                default:
                    if (e.msg.key.ToUpper() == "*RST")
                    {
                        Prepare();
                        ISpectrumObj.ResetStatus();
                    }
                    else if (e.msg.key.ToUpper() == "*CLS")
                    { 

                    }
                        break;
            
            }
            if (e.msg.key.ToUpper() == "SET")
            {
                string name = GetName_String(e.msg.json, ref value);
                string sendmessge = handle_set(name, value);
                sa.OnSend(sendmessge, sender as string);
            }
            else if (e.msg.key.ToLower() == "exec")
            {
                handle_exec(e.msg.json, sender as string);
            }
            else
            {
                sa.OnSend("SET ERROR\r\n", sender as string);
            }


        }
        void handle_exec(string pramname,string add)
        {
            switch (pramname.ToLower())
            {
                case "stop":
                    this.Invoke(new ThreadStart(delegate() {
                        PIMstop(); 
                    }));
                    sa.OnSend("1,succsee\r\n",add);
                    break;
                case "start":
                       PIMstart(add); ;
                    break;
            }
        }

        string handle_set(string pramname,string pramvalue)
        {

            string receivemess = "";
             if (Sweeping)
            {
                return "PIM BUSY\r\n";
            }
            switch (pramname.ToLower())
            {
                case "*idn":
                    receivemess = App_Configure.Cnfgs.Mac_Desc + "," + App_Configure.Cnfgs.SN + "\r\n";
                    break;
                case "port":
                    if (App_Configure.Cnfgs.SwitchMode == 0)
                        receivemess = ChangePort(pramvalue);
                    else
                    {
                        string[] val1 = App_Configure.Cnfgs.SwitchO.Split(',');
                        string[] val2 = App_Configure.Cnfgs.SwitchN.Split(','); 
                        if (val1.Length == val2.Length && val1.Length >= 1)
                        {
                            int id = Array.IndexOf(val1, pramvalue);
                            if (id >= 0)
                            {
                                receivemess = ChangePort(val2[id]);
                            }
                        }
                    }
                    break;
                case "getjpg":
                    this.Invoke(new ThreadStart(delegate() { receivemess =SetJPG(pramvalue); }));
                    break;
                case "sweep":
                    if (pramvalue == "0")
                       this.Invoke(new ThreadStart(delegate(){ receivemess = setSweep(SweepType.Freq_Sweep);}));
                    else if (pramvalue == "1")
                        this.Invoke(new ThreadStart(delegate() { receivemess = setSweep(SweepType.Time_Sweep); }));
                    else
                        receivemess = "0,ERROR FOR INPUT MODEL\r\n";
                    break;
                case "type":

                    if (App_Configure.Cnfgs.Ms_switch_port_count < 2)
                    {
                        if (pramvalue == "0")
                            this.Invoke(new ThreadStart(delegate() { receivemess = setType(true); }));
                        else if (pramvalue == "1")
                        {
                            //多端口频段没有fwd模式
                            if (App_Configure.Cnfgs.Ms_switch_port_count > 0)
                            {
                                receivemess = "0,ERROR FOR FWD OR REV\r\n";
                                break;
                            }
                            this.Invoke(new ThreadStart(delegate() { receivemess = setType(false); }));
                        }
                        else
                            receivemess = "0,ERROR FOR FWD OR REV\r\n";
                    }
                    else
                    {
                        try
                        {
                            int num = int.Parse(pramvalue);
                            if (num >= 2||num<=-1)
                            {
                                receivemess = "0,change port fail\r\n"; break;
                            }
                            
                            receivemess = ChangePort(num.ToString());
                        }
                        catch {
                            receivemess = "0,change port fail\r\n";
                        }
                        
                    }
                    break;
                case "unit":
                    if (pramvalue == "0")
                         this.Invoke(new ThreadStart(delegate(){ receivemess = setUnit(isFlagDbc);}));
                    else if (pramvalue == "1")
                         this.Invoke(new ThreadStart(delegate(){ receivemess = setUnit2(isFlagDbc);}));
                    else
                        receivemess = "0,ERROR FOR FWD OR UNIT\r\n";
                    break;
                case"limit":
                    this.Invoke(new ThreadStart(delegate() { receivemess = setLimit(pramvalue); }));
                    break;
                case "order":
                    if (pramvalue == "3" || pramvalue == "5" || pramvalue == "7" || pramvalue == "9")
                          this.Invoke(new ThreadStart(delegate(){receivemess = setOrder(int.Parse(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR ORDER\r\n";
                    break;
                case "times":
                     this.Invoke(new ThreadStart(delegate(){ receivemess = setTimes(Convert.ToSingle(pramvalue));}));
                    break;
                case "power":
                    if (checkouValue(Convert.ToSingle(pramvalue), App_Settings.sgn_1.Max_Power, App_Settings.sgn_1.Min_Power))
                        this.Invoke(new ThreadStart(delegate(){  receivemess = setPower(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR TX OUT OF RANGE\r\n";
                    break;
                case "step1":
                    if (checkouValue(Convert.ToSingle(pramvalue), settings.F1e - settings.F1s, (float)(0.1)))
                         this.Invoke(new ThreadStart(delegate(){ receivemess = setStep1(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR STEP\r\n";
                    break;
                case "step2":
                    if (checkouValue(Convert.ToSingle(pramvalue), settings.F2e - settings.F2s, (float)(0.1)))
                          this.Invoke(new ThreadStart(delegate(){receivemess = setStep2(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR STEP\r\n";
                    break;
                case "f1s":
                    if (checkouValue(Convert.ToSingle(pramvalue), App_Settings.sgn_1.Max_Freq, App_Settings.sgn_1.Min_Freq))
                          this.Invoke(new ThreadStart(delegate(){receivemess = setF1s(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR FREQ\r\n";
                    break;
                case "f1e":
                    if (checkouValue(Convert.ToSingle(pramvalue), App_Settings.sgn_1.Max_Freq, App_Settings.sgn_1.Min_Freq))
                          this.Invoke(new ThreadStart(delegate(){receivemess = setF1e(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR FREQ\r\n";
                    break;
                case "f2s":
                    if (checkouValue(Convert.ToSingle(pramvalue), App_Settings.sgn_2.Max_Freq, App_Settings.sgn_2.Min_Freq))
                         this.Invoke(new ThreadStart(delegate(){ receivemess = setF2s(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR FREQ\r\n";
                    break;
                case "f2e":
                    if (checkouValue(Convert.ToSingle(pramvalue), App_Settings.sgn_2.Max_Freq, App_Settings.sgn_2.Min_Freq))
                          this.Invoke(new ThreadStart(delegate(){receivemess = setF2e(Convert.ToSingle(pramvalue));}));
                    else
                        receivemess = "0,ERROR FOR FREQ\r\n";
                    break;
                case "save":
                     this.Invoke(new ThreadStart(delegate(){ receivemess = Save();}));
                    break;
                case "getmax":
                    receivemess = GetMax();
                    break;
                case "getport":
                    receivemess = GetPort();
                    break;
                default:
                    receivemess = "0,SET:ERROR\r\n";
                    break;
            }
            return receivemess ;
        }

        int  server_sweepNow = 0;
        bool server_sweepEnd = false;
        void SendCallback(IAsyncResult ar)
        {
            string _addr = ar.AsyncState as string;
            int n1 = 0;
            int n2 = 0;
            if (settings.SweepType == SweepType.Freq_Sweep)
            {
                n1 = fsp.Items1.Length;
                n2 = fsp.Items2.Length;
            }
            CsvReport_Pim_Entry[] temp;
            string crpe = "";
            while (true)
            {
                if (sa.m_client_list.ContainsKey(_addr))
                {
                    lock (listCrp)
                    {
                        temp = listCrp.ToArray();
                    }

                    if (server_sweepNow < temp.Length)
                    {
                        //1,43,930,43,960,900,-165
                        crpe = temp[server_sweepNow].No + "," +
                                   temp[server_sweepNow].P1.ToString() + "," +
                                   temp[server_sweepNow].F1.ToString() + "," +
                                   temp[server_sweepNow].P2.ToString() + "," +
                                   temp[server_sweepNow].F2.ToString() + "," +
                                   temp[server_sweepNow].Im_F.ToString() + "," +
                                   temp[server_sweepNow].Im_V.ToString() + "," +
                                   n1.ToString() + "," +
                                   n2.ToString();
                        break;
                    }
                    if (server_sweepEnd == true && Sweeping == false)
                        break;
                    Thread.Sleep(100);
                }
                else
                {
                    this.Invoke(new ThreadStart(delegate()
                    {
                        PIMstop();
                    }));
                    return; 
                }
            }

            if (server_sweepEnd == true && server_sweepNow > temp.Length - 1)
            {
                if (SweepObj.errmessage.Count > 0)
                {
                    for (int i = 0; i < SweepObj.errmessage.Count; i++)
                    {
                        sa.OnSend("ERR " + SweepObj.errmessage[i] + "\r\n", _addr);
                    }
                }
                if (!sa.OnSend("PIM END\r\n", _addr))
                {
                    this.Invoke(new ThreadStart(delegate()
                    {
                        PIMstop();
                    }));
                    return;
                }
            }
            else
            {
                if (!sa.OnSend("@" + crpe + "\r\n", _addr, new AsyncCallback(SendCallback)))
                {
                    this.Invoke(new ThreadStart(delegate()
                    {
                        PIMstop();
                    }));
                    return;
                }
                server_sweepNow++;
            }
        }

        private void PimForm_Shown(object sender, EventArgs e)
        {
            isOpenSqlConnServer = IniFile.GetString("sqlinfo", "open", "0", Application.StartupPath + "\\SqlInfo.ini") == "1" ? true : false;
            isOpenRemoteCtlServer = IniFile.GetString("connServer", "open", "0", Application.StartupPath + "\\SqlInfo.ini") == "1" ? true : false;

            if (isOpenSqlConnServer)
            {
                SqlLogin sl = new SqlLogin(si);
                if (sl.ShowDialog() == DialogResult.OK)
                {
                    isSqlConn = true;
                    return;
                }
                else
                    isSqlConn = false;
            }

            //开启服务
            if (isOpenRemoteCtlServer)
            {
                offset_init = new float[7] { 0, 0, 0, 0, 0, 0, 0 };
                offset_range = new float[8] { -101, -104, -107, -109, -111, -113, -115, -117 };
                offset_DoneRange = false;
                offset_DoneAvg = false;
                offset_P = 0;
                offset_Val = 0;
                try
                {
                    if ((_istcp = IniFile.GetString("connServer", "isclient", "0", Application.StartupPath + "\\SqlInfo.ini") == "1" ? true : false))
                    {
                        sa = new SocketAdmin();
                        string ip = IniFile.GetString("connServer", "addr", "127.0.0.1", Application.StartupPath + "\\SqlInfo.ini");
                        //string ip = "";
                        //IPHostEntry IpEntry = Dns.GetHostEntry(Dns.GetHostName());
                        //for (int i = 0; i < IpEntry.AddressList.Length; i++)
                        //{
                        //    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                        //    {
                        //        ip= IpEntry.AddressList[i].ToString();
                        //        break;
                        //    }
                        //}
                        int port = Convert.ToInt32(IniFile.GetString("connServer", "port", "6307", Application.StartupPath + "\\SqlInfo.ini"));
                        sa.ReachedEventHandler += sa_ReachedEventHandler2;
                        sa.OnServer(ip, port);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        //nozuonodie
        float DataDealWith(float val, float n)
        {
            if (!offset_RemoteCtr)
                return val;

            //Log.WriteLog("S: " + val.ToString(), Log.EFunctionType.PIM);
            if (offset_DoneRange)
            {
                if (val <= offset_range[0] && val > offset_range[1])
                    val -= offset_init[0];
                else if (val <= offset_range[1] && val > offset_range[2])
                    val -= offset_init[1];
                else if (val <= offset_range[2] && val > offset_range[3])
                    val -= offset_init[2];
                else if (val <= offset_range[3] && val > offset_range[4])
                    val -= offset_init[3];
                else if (val <= offset_range[4] && val > offset_range[5])
                    val -= offset_init[4];
                else if (val <= offset_range[5] && val > offset_range[6])
                    val -= offset_init[5];
                else if (val <= offset_range[6] && val > offset_range[7])
                    val -= offset_init[6];
            }

            if (offset_DoneAvg)
            {
                float all = 0;
                float count = 0;

                if (offset_DoneAvgType)
                {
                    if (n == 0)
                    {
                        offset_listAvg1.Add(val);
                        foreach (float e in offset_listAvg1)
                        {
                            all += e;
                        }
                        count = offset_listAvg1.Count;
                    }
                    else
                    {
                        offset_listAvg2.Add(val);
                        foreach (float e in offset_listAvg2)
                        {
                            all += e;
                        }
                        count = offset_listAvg2.Count;
                    }
                }
                else
                {
                    offset_listAvg1.Add(val);
                    foreach (float e in offset_listAvg1)
                    {
                        all += e;
                    }
                    count = offset_listAvg1.Count;
                }

                val = all / count;
            }

            return val;
        }

        #endregion

        #region 按钮事件

        #region Start
        private void pbxStart_MouseClick(object sender, MouseEventArgs e)
        {
          
            //NativeMessage.PostMessage(this.Handle, MessageID.PIM_SWEEP_CLOSE, 0, 0);
            StartClick();
            //SqlServer ss = new SqlServer(CurrentPi, si, null);
            //ss.ShowDialog();
        }

        private void pbxStart_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
                ChangeBtnPic(pbxStart, "start.gif");
        }

        private void pbxStart_MouseLeave(object sender, EventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                ChangeBtnPic(pbxStart, "start_in.gif");
                ChangeBtnPic(pbxPeak, "peak_in.gif");
            }
        }
        #endregion

        #region Rev
        private void pbxRev_MouseClick(object sender, MouseEventArgs e)
        {//rev1  第一个端口
            if (!Sweeping && !isRead)
            {

                if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
                {
                    if (App_Configure.Cnfgs.Mode >= 2)
                    {
                        if (settings.PimSchema != ImSchema.REV || port1_rev_fwd != 1)
                        {
                            settings.PimSchema = ImSchema.REV;
                            GPIO.Rev(1);

                            ChangeBtnPic(pbxFwd, "fwd1_in.jpg");
                            ChangeBtnPic(pbxRev, "rev1.jpg");

                            ChangeBtnPic(pictureBox2, "fwd2_in.jpg");
                            ChangeBtnPic(pictureBox1, "rev2_in.jpg");
                            port1_rev_fwd = 1;
                            SetPltPimX((int)settings.PimOrder);
                        }
                    }
                    else
                    {
                        if (settings.PimSchema != ImSchema.REV)
                        {
                            settings.PimSchema = ImSchema.REV;
                            GPIO.Rev();
                            ChangeBtnPic(pbxFwd, "fwd_in.gif");
                            ChangeBtnPic(pbxRev, "rev.gif");
                        }
                    }
                }
                else
                {
                    if (settings.PimSchema != ImSchema.REV)
                    {
                        settings.PimSchema = ImSchema.REV;
                        GPIO.Rev();
                        ChangeBtnPic(pbxFwd, "rev2_in.jpg");
                        ChangeBtnPic(pbxRev, "rev.gif");
                        Program.offset.GetTX(0);
                        Program.offset.GetRX(0);
                        UpdatePortData(0);
                    }
                }


            }
        }

        private void pbxRev_MouseMove(object sender, MouseEventArgs e)
        {
            if (App_Configure.Cnfgs.Mode >= 2)
            {
                ChangeBtnPic(pbxRev, "rev1.jpg");
            }
            else
            {
                ChangeBtnPic(pbxRev, "rev.gif");
            }
        }

        private void pbxRev_MouseLeave(object sender, EventArgs e)
        {
            if (App_Configure.Cnfgs.Mode >=2)
            {
                if (settings.PimSchema != ImSchema.REV || port1_rev_fwd != 1)
                    ChangeBtnPic(pbxRev, "rev1_in.jpg");
            }
            else
            {
                if (settings.PimSchema != ImSchema.REV )
                    ChangeBtnPic(pbxRev, "rev_in.gif");
            }
        }
        #endregion

        #region Peak
        private void pbxPeak_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxPeak, "peak.gif");
            pltPim.Peak();
        }

        private void pbxPeak_MouseMove(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
                ChangeBtnPic(pbxPeak, "peak.gif");
        }

        private void pbxPeak_MouseLeave(object sender, EventArgs e)
        {
            if (!Sweeping && !isRead)
                ChangeBtnPic(pbxPeak, "peak_in.gif");

        }
        #endregion

        #region Fwd
        private void pbxFwd_MouseClick(object sender, MouseEventArgs e)
        {//fwd1 第2个端口
            if (!Sweeping && !isRead)
            {

                if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
                {
                    if (App_Configure.Cnfgs.Mode >= 2)
                    {
                        if (settings.PimSchema != ImSchema.FWD || port1_rev_fwd != 2)
                        {
                            settings.PimSchema = ImSchema.FWD;
                            GPIO.Fwd(1);
                            ChangeBtnPic(pbxRev, "rev1_in.jpg");
                            ChangeBtnPic(pbxFwd, "fwd1.jpg");

                            ChangeBtnPic(pictureBox1, "rev2_in.jpg");
                            ChangeBtnPic(pictureBox2, "fwd2_in.jpg");
                            port1_rev_fwd = 2;
                            SetPltPimX((int)settings.PimOrder);
                        }
                    }
                    else
                    {
                        if (settings.PimSchema != ImSchema.FWD)
                        {
                            settings.PimSchema = ImSchema.FWD;
                            GPIO.Fwd();
                            ChangeBtnPic(pbxRev, "rev_in.gif");
                            ChangeBtnPic(pbxFwd, "fwd.gif");
                        }
                    }
                }
                else
                {
                    if (settings.PimSchema != ImSchema.FWD)
                    {
                        settings.PimSchema = ImSchema.FWD;
                        GPIO.Fwd();
                        ChangeBtnPic(pbxRev, "rev_in.gif");
                        ChangeBtnPic(pbxFwd, "rev2.jpg");
                        Program.offset.GetTX(1);
                        Program.offset.GetRX(1);
                        UpdatePortData(1);
                    }
                }
            }
        }

        private void pbxFwd_MouseMove(object sender, MouseEventArgs e)
        {
            if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
            {
                if (App_Configure.Cnfgs.Mode >= 2)
                {

                    ChangeBtnPic(pbxFwd, "fwd1.jpg");
                }
                else
                {
                    ChangeBtnPic(pbxFwd, "fwd.gif");
                }
            }
            else
            {
                ChangeBtnPic(pbxFwd, "rev2.jpg");        
            }
        }

        private void pbxFwd_MouseLeave(object sender, EventArgs e)
        {
            if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
            {
                if (App_Configure.Cnfgs.Mode >= 2)
                {
                    if (settings.PimSchema != ImSchema.FWD || port1_rev_fwd != 2)
                        ChangeBtnPic(pbxFwd, "fwd1_in.jpg");
                }
                else
                {
                    if (settings.PimSchema != ImSchema.FWD)
                        ChangeBtnPic(pbxFwd, "fwd_in.gif");
                }
            }
            else
            {
                if (settings.PimSchema != ImSchema.FWD)
                    ChangeBtnPic(pbxFwd, "rev2_in.jpg");
            }
        }
        #endregion

        #region Datasave
        private void pbxDatasave_MouseClick(object sender, MouseEventArgs e)
        {
            return;
            if (!Sweeping && !isRead)
            {
                if (listCrp.Count == 0)
                {
                    MessageBox.Show(this, "No data can be saved currently!");
                    return;
                }
               
                if (p != null)
                {
                    PimSaveForm psf = new PimSaveForm();
                    DialogResult dr = psf.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        App_Configure.Cnfgs.Modno = psf.textBox2.Text;
                        App_Configure.Cnfgs.Serno = psf.textBox3.Text;
                        App_Configure.Cnfgs.Opeor = psf.textBox1.Text;
                        string path = App_Configure.Cnfgs.Path_Rpt_Pim + "\\";
                        string result = "";
                        for (int i = 0; i < CurrentSelectPort.Count; i++)
                        {


                            if (psf._pdfFlag)
                            {
                                if (SavePdf(path + "pdf\\" + psf.txtPDF.Text + "_" + CurrentSelectPort[i].ToString() + ".pdf", CurrentSelectPort[i] - 1))
                                    result += "Saving the PD\\  file successfully!\r\n";
                                else
                                    result += "Saving the PDF file failed!\r\n";
                            }
                            if (psf._csvFlag)
                            {
                                if (!Directory.Exists(path + "csv\\" + psf.txtCsv.Text))
                                {
                                    Directory.CreateDirectory(path + "csv\\" + psf.txtCsv.Text);
                                }
                                if (SaveCsv(path + "csv\\" + psf.txtCsv.Text +"\\"+ psf.txtCsv.Text + "_" + CurrentSelectPort[i].ToString() + ".csv", CurrentSelectPort[i] - 1))
                                    result += "Saving the CSV file successfully!\r\n";
                                else
                                    result += "Saving the CSV file failed!\r\n";

                                //联滔电子txt报告  2016.8.12
                                //if (SaveT(psf._txtPath + "\\" + psf.txtCsv.Text))
                                //    result += "Saving the TXT file successfully!\r\n";
                                //else
                                //    result += "Saving the TXT file failed!\r\n";
                                ////}
                            }
                            if (psf._jpgFlag)
                            {

                                if (SaveJpg(path + "jpg\\" + psf.txtJpg.Text + "_" + CurrentSelectPort[i].ToString() + ".jpg",CurrentSelectPort[i]-1))
                                    result += "Saving the JPG file successfully!\r\n";
                                else
                                    result += "Saving the JPG file failed!\r\n";
                            }
                        }
                        if (psf._pdfFlag || psf._csvFlag || psf._jpgFlag)
                            MessageBox.Show(this, result);
                    }
                }
             

            }
        }

        private void pbxDatasave_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxDatasave, "datasave.gif");
        }

        private void pbxDatasave_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pbxDatasave, "datasave_in.gif");
        }
        #endregion

        #region Dataread
        List<int> allCsvPort = new List<int>();
        List<string> list_filName = new List<string>();
        private void pbxDataread_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                ReadFiles frmRead = new ReadFiles();
                frmRead.FillFiles(App_Configure.Cnfgs.Path_Rpt_Pim + "\\csv");
                //frmRead.FillFiles( "c:\\csv");
                DialogResult dr = frmRead.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    isReadCsv = true;
                    string strFilePath = App_Configure.Cnfgs.Path_Rpt_Pim + "\\csv\\" + frmRead.FileName;
                    //string strFilePath = "c:\\csv\\" + frmRead.FileName;
                    allCsvPort.Clear();
                    //DirectoryInfo folder = new DirectoryInfo(strFilePath);
                    list_filName.Clear();
                    DirectoryInfo info = new DirectoryInfo(strFilePath);
                    FileSystemInfo[] fs = info.GetFiles();


                    for (int i = 0; i < fs.Length; i++)
                    {
                        list_filName.Add(fs[i].FullName);
                    }
                    
                    for (int i = 0; i < control_port.Count; i++)
                    {
                        ChangeBtnPic((PictureBox)control_port[i], "port_black"+(i+1).ToString()+".jpg");
                    }

                    bool isH = false;
                    for (int i = 0; i < list_filName.Count; i++)
                    {
                        int num = int.Parse(list_filName[i].Substring(list_filName[i].Length - 5, 1));
                        if (current_port != num)
                            ChangeBtnPic((PictureBox)control_port[num-1], "port" + num.ToString() + "_in.jpg");
                        else
                        {
                            ChangeBtnPic((PictureBox)control_port[num-1], "port" + num.ToString() + ".jpg");
                            isH = true;
                        }
                        allCsvPort.Add(num);
                    }
                    if (isH)
                    {
                        //switch (current_port)
                        //{
                            ReadAllCsv(current_port);
                            //case 1: pbxPort1_MouseClick(null, null); break;
                            //case 2: pbxPort2_MouseClick(null, null); break;
                            //case 3: pbxPort3_MouseClick(null, null); break;
                            //case 4: pbxPort4_MouseClick(null, null); break;
                            //case 5: pictureBox5_MouseClick(null, null); break;
                            //case 6: pictureBox4_MouseClick(null, null); break;
                            //default:

                            //    break;
                        //}
                    }
                    else
                    {
                       
                        current_port = 0;
                    }
                    //ReadCsv(strFilePath);
                
                }
            }
        }

        void ReadAllCsv(int num)
        {
            for (int i = 0; i < allCsvPort.Count ; i++)
            {
                if (num == allCsvPort[i])
                {
                    ReadCsv(list_filName[i]);
                    break;
                }
            }
        }

        private void pbxDataread_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pbxDataread, "dataread_in.gif");
        }

        private void pbxDataread_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxDataread, "dataread.gif");
        }

        #endregion

        #region Setting

        //脚本
        List<PimTestScript> m_listScript;
        int m_settingIndex = 0;
        private void GetJcScript()
        {
            //初始化脚本
            m_listScript = new List<PimTestScript>();
            Settings_Pim sp_fullband = new Settings_Pim(this.settings.fileName);
            this.settings.Clone(sp_fullband);
            sp_fullband.projectName = "FULL-BAND";
            m_listScript.Add(new PimTestScript(sp_fullband));
            //读取脚本
            string strPath = Application.StartupPath + "\\settings\\JcScript.ini";
            if (!File.Exists(strPath))
            {
                FileStream fs = File.Create(strPath);
                fs.Close();
                fs.Dispose();
                IniFile.SetString("Script", "count", "0", strPath);
            }
            int count = int.Parse(IniFile.GetString("Script", "count", "0", strPath));
            for (int i = 1; i <= count; i++)
            {//解析参数
                string strValueList = IniFile.GetString("Script", "N" + i.ToString(), "", strPath);
                if (strValueList.Trim() != "")
                {
                    //1）扫描方式：点频/扫频
                    //2）互调测量阶数：3、5、7、9
                    //3）频率范围：扫频为F1，F2起止频率，点频为F1，F2单点；
                    //4）扫描步进：扫频为多少M，扫时为多少点；
                    //5）功率电平：默认43dBm
                    Settings_Pim sp = new Settings_Pim(this.settings.fileName);
                    this.settings.Clone(sp);
                    string[] strValue = strValueList.Split(',');

                    int a = 0;
                    sp.projectName = strValue[a++];
                    sp.SweepType = strValue[a++] == "0" ? SweepType.Freq_Sweep : SweepType.Time_Sweep;
                    sp.PimSchema = strValue[a++] == "0" ? ImSchema.FWD : ImSchema.REV;
                    sp.PimOrder = (ImOrder)int.Parse(strValue[a++]);
                    sp.F1s = float.Parse(strValue[a++]);
                    sp.F1e = float.Parse(strValue[a++]);
                    sp.F2s = float.Parse(strValue[a++]);
                    sp.F2e = float.Parse(strValue[a++]);
                    sp.Setp1 = sp.Setp2 = float.Parse(strValue[a++]);
                    sp.TimeSweepPoints = int.Parse(strValue[a++]);
                    sp.Tx = float.Parse(strValue[a++]);

                    m_listScript.Add(new PimTestScript(sp));
                }
            }
        }
        private void lblTestProject_Click(object sender, EventArgs e)
        {
            if (m_listScript == null)
                GetJcScript();
            PimTestProject frmPimTestProject = new PimTestProject(m_listScript, m_settingIndex);
            if (frmPimTestProject.ShowDialog() == DialogResult.OK)
            {
                m_settingIndex = frmPimTestProject.m_selected;
                this.settings = null;
                this.settings = m_listScript[m_settingIndex].settings;
                this.lblTestProject.Text = this.settings.projectName;
                //先执行SaveSettings
                SaveSettings((int)settings.PimOrder, (int)settings.SweepType, true, true);
                lblN1.Text = "=" + settings.Tx.ToString() + "dBm";
                lblN2.Text = "=" + settings.Tx.ToString() + "dBm";
                float v = ChangePimTxt(settings.F1s, settings.F2e, (int)settings.PimOrder);
                pimTxt.Text = v.ToString("0.0");
                //后执行RunSettings
                RunSettings();
            }
        }
        private void pbxSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                PimSettingForm frmPimSetting = new PimSettingForm(this.settings,CurrentSelectPort);
                DialogResult dr = frmPimSetting.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    x_stop = settings.TimeSweepPoints;
                    CurrentSelectPort = frmPimSetting.selectPort;
                    RunSettings();
                }
                frmPimSetting.Dispose();
            }
                            
        }
        private void RunSettings()
        {
            if (settings.SweepType == SweepType.Freq_Sweep)
            {
                if (scanType)
                    lblMaxPim.Visible = true;
                else
                    lblMaxPim.Visible = true;
                ScrollEnable(false, false, false);
                ChangeBtnPic(pblFt, "other", "frequency.png");
                pnlFpim.Visible = false;
            }
            else
            {
                if (scanType)
                    lblMaxPim.Visible = true;
                else
                    lblMaxPim.Visible = true;

                ScrollEnable(true, false, false);
                ChangeBtnPic(pblFt, "other", "time.png");
                pnlFpim.Visible = true;

                this.pltPim.SetXStartStop(0, x_stop);
                ScrollEnable(false, true, true);
                pnlFpim.Visible = true;
            }
            float limit_pim;
            if (settings.PimUnit == ImUint.dBc)
            {
                ChangeDbc();
                limit_pim = settings.Limit_Pim - (settings.Tx + settings.Tx) / 2;
                txtBox.Text = limit_pim.ToString();
            }
            else
            {
                ChangeDbmm();
                limit_pim = settings.Limit_Pim;
                txtBox.Text = limit_pim.ToString();
            }
            pltPim.SetLimitEnalbe(true, limit_pim, Color.FromArgb(160, 245, 255));
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
            ChangeOrder((int)settings.PimOrder);
            if (settings.PimSchema == ImSchema.REV)
            {
                GPIO.Rev();
                if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
                {
                    ChangeBtnPic(pbxFwd, "fwd_in.gif");
                    ChangeBtnPic(pbxRev, "rev.gif");
                }
                else
                {
                    ChangeBtnPic(pbxFwd, "rev2_in.jpg");
                    ChangeBtnPic(pbxRev, "rev.gif");
                }
            }
            else if (settings.PimSchema == ImSchema.FWD)
            {
                GPIO.Fwd();
                if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
                {
                    ChangeBtnPic(pbxRev, "rev_in.gif");
                    ChangeBtnPic(pbxFwd, "fwd.gif");
                }
                else
                {
                    ChangeBtnPic(pbxRev, "rev_in.gif");
                    ChangeBtnPic(pbxFwd, "rev2.jpg");
                }
            }
        }

        private void pbxSetting_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxSetting, "setting.gif");
        }

        private void pbxSetting_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pbxSetting, "setting_in.gif");
        }
        #endregion

        #region PimSettings
        private void pnlPimSettings_Leave(object sender, EventArgs e)
        {
            dgvPim.CurrentCell = null;
        }
        #endregion

        #endregion

        #region 启动方法
        /// <summary>
        /// 启动方法
        /// </summary>
        private void StartClick()
        {
            if (!Sweeping && !isRead)
            {
               
                sweepEnd = false;
                Sweeping = true;
                PbxStart();
            }
            else
            {
                if (isRead)
                {
                    sweepEnd = false;
                }
                else
                {
                    sweepEnd = true;
                    PimBreakSweep(1000);
                }

                Timer.Stop();
                isRead = false;
                ScrollEnable(false, true, true);
            }

        }
        #endregion

        #region MARK委托
        public string PimMarkText(MarkInfo[] mi)
        {
            ChangeBtnPic(pbxPeak, "peak_in.gif");
            string markDbc = "";
            string markDbm = "";
            string markValue = "";
            float v = 0f;
            int num = 0;
            for (int i = 0; i < mi.Length; i++)
            {

                if (mi[i].iChannel < 0)
                    continue;
                if (mi[i].fPoint.X > float.MinValue && mi[i].fPoint.Y > float.MinValue)
                {
                    if (settings.SweepType == SweepType.Freq_Sweep)
                    {
                        num++;
                        if (mi[i].fPoint.X != 0.0)
                        {
                            markDbc += "(" + mi[i].fPoint.X.ToString() + "MHz," + mi[i].fPoint.Y.ToString() + "dBc)";
                            markDbm += "(" + mi[i].fPoint.X.ToString() + "MHz," + mi[i].fPoint.Y.ToString() + "dBm)";
                        }
                        if (mi[i].fPoint.X != v && mi[i].fPoint.X != 0.0 && v != 0)
                        {
                            markDbc = "(" + mi[i].fPoint.X.ToString() + "MHz," + mi[i].fPoint.Y.ToString() + "dBc)";
                            markDbm = "(" + mi[i].fPoint.X.ToString() + "MHz," + mi[i].fPoint.Y.ToString() + "dBm)";
                        }
                        v = mi[i].fPoint.X;
                        if (mi[i].fPoint.X == 0.0 && num == 2)
                        {
                            markDbc = "(" + mi[i - 1].fPoint.X.ToString() + "MHz," + mi[i - 1].fPoint.Y.ToString() + "dBc)";
                            markDbm = "(" + mi[i - 1].fPoint.X.ToString() + "MHz," + mi[i - 1].fPoint.Y.ToString() + "dBm)";
                        }
                    }
                    else if (mi[i].fPoint.Y != 0.0)
                    {
                        markDbc = "(" + mi[i].fPoint.Y.ToString() + "dBc)";
                        markDbm = "(" + mi[i].fPoint.Y.ToString() + "dBm)";
                    }

                }
                if (isFlagDbc)
                    markValue = "Mark:" + markDbc;
                else
                    markValue = "Mark:" + markDbm;
            }
            return markValue;
        }

        public string PimPeakText(MarkInfo[] mi)
        {
            string str1 = "";
            string strValue = "";

            for (int i = 0; i < mi.Length; i++)
            {
                if (isFlagDbc)
                {
                    str1 = mi[i].fPoint.Y.ToString() + "dBc)";
                }
                else
                {
                    str1 = mi[i].fPoint.Y.ToString() + "dBm)";
                }
                if (settings.SweepType == SweepType.Freq_Sweep)
                {
                    strValue = "M" + mi[i].iOrder.ToString() + ": " +
                       "(" + mi[i].fPoint.X.ToString() + "MHz," + str1 +
                       mi[i].iChannel.ToString();
                }
                else
                {
                    strValue = "M" + mi[i].iOrder.ToString() + ": " +
                       "(" + str1 + mi[i].iChannel.ToString();
                }
            }
            return strValue;
        }
        #endregion

        #region NEW互调分析对象
        /// <summary>
        /// 互调分析对象
        /// </summary>
        private void New_Pim_Sweep()
        {
            if (SweepObj == null)
            {
                SweepObj = new Pim_Sweep(this.Handle);  ///我加了个句柄，为了发送消息
                SweepObj.Band = this.settings.Scanband;
            }
        }
        #endregion

        #region 开始分析循环
        /// <summary>
        /// 开始分析循环
        /// </summary>
        /// <param name="sweepParams"></param>
        private void StartSweep(SweepParams sweepParams)
        {
            SweepObj.InitSweep();
            SweepObj.Prepare(sweepParams);
            SweepObj.StartSweep();
        }
        #endregion

        #region 中断分析循环
        /// <summary>
        /// 中断分析循环，发生在用户切换到其他功能模块
        /// 或用户强行停止分析
        /// </summary>
        internal void PimBreakSweep(int timeOut)
        {
            if (Sweeping)
            {
                SweepObj.StopSweep(timeOut);
            }
        }
        #endregion
        internal bool PimBreakSweep()
        {
            bool res = false;
            if (Sweeping)
            {
                res = SweepObj.GetIsSweep();
            }
            return res;
        }
        #region PIM初始化设置
        /// <summary>
        /// PIM初始化设置
        /// </summary>
        private void Setting()
        {
            //REV,FWD切换
            ChangeRevFwd();
            ///根据扫描模式切换显示图片,设置PIM X轴的坐标
            if (settings.SweepType == SweepType.Freq_Sweep)
            {
                ft = false;
                ScrollEnable(false, false, false);
                ChangeBtnPic(pblFt, "other", "frequency.png");
                pnlFpim.Visible = false;
            }
            else
            {
                ft = true;
                ScrollEnable(true, false, false);
                pltPim.SetXStartStop(0, x_stop);
                ChangeBtnPic(pblFt, "other", "time.png");
                pnlFpim.Visible = true;
            }
            ChangeBtnPic(pbxL, "zoomin.bmp");
            if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
            {
                if (App_Configure.Cnfgs.Mode >= 2)
                {
                    if (settings.PimSchema == ImSchema.REV)
                    {
                        ChangeBtnPic(pbxRev, "rev1.jpg");
                        ChangeBtnPic(pbxFwd, "fwd1_in.jpg");
                    }
                    else if (settings.PimSchema == ImSchema.FWD)
                    {
                        ChangeBtnPic(pbxFwd, "fwd1.jpg");
                        ChangeBtnPic(pbxRev, "rev1_in.jpg");
                    }
                }
                else
                {
                    if (settings.PimSchema == ImSchema.REV)
                        ChangeBtnPic(pbxRev, "rev.gif");
                    else if (settings.PimSchema == ImSchema.FWD)
                        ChangeBtnPic(pbxFwd, "fwd.gif");
                }
            }
            else
            {
                if (settings.PimSchema == ImSchema.REV)
                    ChangeBtnPic(pbxRev, "rev.gif");
                else if (settings.PimSchema == ImSchema.FWD)
                    ChangeBtnPic(pbxFwd, "rev2.jpg");
            }
            ///Mark
            pltPim.SetMarkColor(0, Color.Orange);
            pltPim.SetMarkColor(1, Color.Orange);

            pltPim.SetMarkText(PimMarkText);
            pltPim.SetPeakText(PimPeakText);

            lblN1.Tag = settings.Tx;
            lblN2.Tag = settings.Tx;

            if (settings.PimUnit == ImUint.dBc)
            {
                isFlagDbc = true;
                ChangeBtnPic(pnlIm, "dbc", ((int)settings.PimOrder).ToString() + ".png");
                limitSelect.Text = "Pass/Fail limit(dBc)";
                float limit_pim = settings.Limit_Pim - settings.Tx;
                txtBox.Text = limit_pim.ToString();
                pltPim.SetLimitEnalbe(true, limit_pim, Color.FromArgb(160, 245, 255));
                start = wRvalue;
                stop = -100;
                pltPim.SetYStartStop(start, stop);
                pltPim.SetChannelVisible(0, false);
                pltPim.SetChannelVisible(1, false);
                pltPim.SetChannelVisible(2, true);
                pltPim.SetChannelVisible(3, false);
                if (settings.SweepType == SweepType.Freq_Sweep)
                    pltPim.SetChannelVisible(3, true);
                pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                dgvPim.Columns[6].HeaderText = "Im" + ((int)settings.PimOrder).ToString() + "_V(dBc)";
                s[0] = -1;
                s[1] = -1;
                s[2] = 2;
                s[3] = 3;
                s[4] = -1;
                pltPim.SetMarkVisible(0, false);
                pltPim.SetMarkVisible(1, true);
                pltPim.SetMarkSequence(1, s);
            }
            else
            {
                isFlagDbc = false;
                ChangeBtnPic(pnlIm, "dbm", ((int)settings.PimOrder).ToString() + ".png");
                limitSelect.Text = "Pass/Fail limit(dBm)";
                float limit_pim = settings.Limit_Pim;
                txtBox.Text = limit_pim.ToString();
                pltPim.SetLimitEnalbe(true, limit_pim, Color.FromArgb(160, 245, 255));
                start = wRvalue;
                stop = -70;
                pltPim.SetYStartStop(start, stop);
                pltPim.SetChannelVisible(0, true);
                pltPim.SetChannelVisible(1, false);
                pltPim.SetChannelVisible(2, false);
                pltPim.SetChannelVisible(3, false);
                if (settings.SweepType == SweepType.Freq_Sweep)
                    pltPim.SetChannelVisible(1, true);
                pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                pltPim.SetChannelColor(0, Color.Yellow);
                dgvPim.Columns[6].HeaderText = "Im" + ((int)settings.PimOrder).ToString() + "_V(dBm)";
                s[0] = 0;
                s[1] = 1;
                s[2] = -1;
                s[3] = -1;
                s[4] = -1;
                pltPim.SetMarkVisible(1, false);
                pltPim.SetMarkVisible(0, true);
                pltPim.SetMarkSequence(0, s);
            }
            //SetPltPimX((int)settings.PimOrder);
            ChangeOrder((int)settings.PimOrder);
            pltPim.SetSmoothing(true); //消除锯齿
            
            pltPim.AutoScroll = App_Configure.Cnfgs.IsScrool;
            pltPim.SetSampling(false);
            pltPim.SetChannelIcon(0, CurveIconStyle.cisSolidDiamond, true);
            pltPim.SetChannelIcon(1, CurveIconStyle.cisSolidDiamond, true);
            pltPim.SetChannelIcon(2, CurveIconStyle.cisSolidDiamond, true);
            pltPim.SetChannelIcon(3, CurveIconStyle.cisSolidDiamond, true);
            pltPim.SetChannelColor(1, Color.Red);
            pltPim.SetChannelColor(2, Color.Yellow);
            pltPim.SetChannelColor(3, Color.Red);

            pltPower.SetChannelIcon(0, CurveIconStyle.cisSolidDiamond, true);
            pltPower.SetChannelVisible(1, true);
            pltPower.SetChannelColor(1, Color.Red);
            pltPower.SetChannelIcon(1, CurveIconStyle.cisSolidDiamond, true);
            pltPower.SetChannelIcon(2, CurveIconStyle.cisSolidDiamond, true);
            pltPower.SetChannelIcon(3, CurveIconStyle.cisSolidDiamond, true);
            pltPower.SetChannelColor(2, Color.Yellow);
            pltPower.SetChannelColor(3, Color.Red);

            ///pltPower X Y 设置
            //pltPower.SetXStartStop(settings.F1s, settings.F2e);
            pltPower.SetXStartStop(App_Settings.spfc.cbn.TxS, App_Settings.spfc.cbn.TxE);
            pltPower.SetYStartStop(0, 50);

            AddColumns(dtDbmC);
            AddColumns(dtDbmM);
            AddColumns(dtWdbc);
            AddColumns(dtWdbm);
            AddColumns(defaultDatalbe);
        }
        #endregion

        #region 表格相应设置

        #region DataTable添加表头
        /// <summary>
        /// DataTable添加标头
        /// </summary>
        /// <param name="dt"></param>
        public void AddColumns(DataTable dt)
        {
            dt.Columns.Add("NO.");
            dt.Columns.Add("P1(dBm)");
            dt.Columns.Add("F1(MHz)");
            dt.Columns.Add("P2(dBm)");
            dt.Columns.Add("F2(MHz)");
            dt.Columns.Add("Im_F(MHz)");
            dt.Columns.Add("Im_V(dBm)");
        }
        #endregion

        #region Table New Row
        /// <summary>
        /// Table New Row
        /// </summary>
        private void TableNewRow(DataTable dt, int no, float p1, float f1, float p2, float f2, float imf, float imv)
        {
            DataRow row = dt.NewRow();
            row["NO."] = no;
            row["P1(dBm)"] = p1.ToString("0.0"); ;
            row["F1(MHz)"] = f1.ToString("0.0"); ;
            row["P2(dBm)"] = p2.ToString("0.0"); ;
            row["F2(MHz)"] = f2.ToString("0.0"); ;
            row["Im_F(MHz)"] = imf.ToString("0.0"); ;
            row["Im_V(dBm)"] = imv.ToString("0.0"); ;
            dt.Rows.Add(row);
        }
        #endregion

        #region dgv表格初始化，默认显示7行
        /// <summary>
        /// dgv表格初始化，默认显示7行
        /// </summary>
        private void DgvIni()
        {
            dgvPim.AllowUserToAddRows = false;
            for (int i = 0; i < 7; i++)
            {
                DataRow row = defaultDatalbe.NewRow();
                row["NO."] = null;
                row["P1(dBm)"] = null;
                row["F1(MHz)"] = null;
                row["P2(dBm)"] = null;
                row["F2(MHz)"] = null;
                row["Im_F(MHz)"] = null;
                row["Im_V(dBm)"] = null;
                defaultDatalbe.Rows.Add(row);
            }
            dgvPim.Columns[5].HeaderText = "Im" + ((int)settings.PimOrder).ToString() + "_F(MHz)";
            if (settings.PimUnit == ImUint.dBm)
            {
                dgvPim.Columns[6].HeaderText = "Im" + ((int)settings.PimOrder).ToString().ToString() + "_V(dBm)";
            }
            else
            {
                dgvPim.Columns[6].HeaderText = "Im" + ((int)settings.PimOrder).ToString().ToString() + "_V(dBc)";
            }

            dgvPim.DataSource = defaultDatalbe;
        }
        #endregion

        #endregion

        #region PbxStart按钮启动方法
        /// <summary>
        /// PbxStart按钮按钮启动方法
        /// </summary>
        private void PbxStart()
        {
           
            pimModeEs();
            lblMaxPim.Visible = true;
            SweepParams sws = GetSweepParams();
            pbxStart.Image = ImagesManage.GetImage("pim", "start.gif");
            ChangeBtnPic(pbxPeak, "peak.gif");
            pltPim.Peak();
            ClearS();
            sum = 0;
            ERRORNUM = 0;
            App_Settings.spc.RxRef = Convert.ToSingle(App_Settings.spc.List_rxRef[current_port - 1]);
            //App_Settings.spc.TxRef = Convert.ToSingle(App_Settings.spc.List_txRef[current_port - 1]);
            App_Settings.spc.OutTxRef = Convert.ToSingle(App_Settings.spc.List_txRef[current_port - 1]);
            if (App_Configure.Cnfgs.SwtichOrGpio)
                ChangePort(current_port - 1);
            else
            {
                if (current_port == 1 )
                    GPIO.Rev();
                else
                    GPIO.Fwd();
            }
            for (int i = 0; i < allCsvPort.Count; i++)
            {
                if (current_port == allCsvPort[i])
                 ChangeBtnPic((PictureBox)control_port[allCsvPort[i] - 1], "port" + allCsvPort[i].ToString() + ".jpg");
            }
            isReadCsv = false;
            x_changestop = x_stop;
            x_stop = settings.TimeSweepPoints;
            PimStat = DateTime.Now;
            PimTimeCount = 0;
            g = 0;
            L = 0;
            Q = 0;
            if (isFlagDbc)
            {
                lbl1.Visible = false;
                lbl2.Visible = true;
            }
            else
            {
                lbl2.Visible = false;
                lbl1.Visible = true;
            }
            if (isFlagW)
            {
                if (isFlagDbc)
                    dgvPim.DataSource = dtWdbc;
                else
                    dgvPim.DataSource = dtWdbm;
            }
            else
            {
                if (isFlagDbc)
                    dgvPim.DataSource = dtDbmC;
                else
                    dgvPim.DataSource = dtDbmM;
            }
            StartSweep(sws);         
            zoomFlag = false;
            ChangeBtnPic(pbxL, "zoomout.bmp");
        }
        #endregion

        #region 需要清空操作
        /// <summary>
        /// 需要清空操作
        /// </summary>
        private void ClearS()
        {
            server_sweepEnd = false;
            listCrp.Clear();
            SweepObj.errmessage.Clear();
            offset_listAvg1.Clear();
            offset_listAvg2.Clear();
            dtDbmC.Rows.Clear();
            dtDbmM.Rows.Clear();
            dtWdbc.Rows.Clear();
            dtWdbm.Rows.Clear();
            pltPim.Clear();
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
            pltPower.Clear();
            lbl1.Text = "";
            lbl2.Text = "";
            lblMaxPim.Text = "";
            lblN1.Text = "";
            lblN2.Text = "";
            lblN3.Text = "";
            lblN4.Text = "";
            DgvIni();
            timerLimit.Start();
            max = float.MinValue;
            min = float.MaxValue;
            maxdbm = 0;
            ctsBar.Visible = false;
        }
        #endregion

        #region 赋值扫描数据
        /// <summary>
        /// 赋值扫描数据
        /// </summary>
        /// <returns></returns>
        private SweepParams GetSweepParams()
        {
            p = new SweepParams();
            if (flag == true)
                p.C = settings.TimeSweepTimes;
            else
                p.C = settings.FreqSweepTimes;
            p.SweepType = settings.SweepType;
            p.DevInfo = new DeviceInfo();
            p.DevInfo.RF_Addr1 = App_Configure.Cnfgs.ComAddr1;
            p.DevInfo.RF_Addr2 = App_Configure.Cnfgs.ComAddr2;
            p.DevInfo.Spectrum = App_Configure.Cnfgs.Spectrum;
            p.RFInvolved = RFInvolved.Rf_1_2;
            p.PimOrder = settings.PimOrder;

            //给频谱仪设定参数
            p.SpeParam = new SpectrumLib.Models.ScanModel();
            p.SpeParam.Att = settings.Att_Spc;
            p.SpeParam.Rbw = settings.Rbw_Spc;
            p.SpeParam.TimeDelay = settings.SpecDelay;
            //if (p.DevInfo.Spectrum != 0)
            //    p.SpeParam.Rbw = 1;
            p.SpeParam.Vbw = settings.Vbw_Spc;
            p.SpeParam.Continued = false;//扫描一次（NEC）
            p.SpeParam.EnableTimer = false;//扫描一次（Bird,Deli）
            p.SpeParam.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;
            //针对Deli频谱设置（pim模式）
            p.SpeParam.DeliSpe = SpectrumLib.Defines.CommonDef.SpectrumType.Deli_PIM;//pim模式（Deli）
            p.SpeParam.Deli_isSpectrum = true;//频谱仪模式（Deli）

            p.WndHandle = this.Handle;
            p.RFPriority = RFPriority.LvlTwo;

            if (p.SweepType == SweepType.Time_Sweep)
                p.TmeParam = tsp;
            else
                p.FrqParam = fsp;
            return p;
        }
        #endregion

        #region 根据设置参数判断扫描是点屏或扫屏
        /// <summary>
        /// 根据设置参数判断扫描是点屏或扫屏
        /// </summary>
        private void pimModeEs()
        {
            int i = 0;
            int order = (int)settings.PimOrder;
            switch (order)
            {
                case 3: i = 0; break;
                case 5: i = 1; break;
                case 7: i = 2; break;
                case 9: i = 3; break;
                case 11: i = 4; break;
                case 13: i = 5; break;
                case 15: i = 6; break;
                case 17: i = 7; break;
                case 19: i = 8; break;
            }
            if (settings.SweepType == SweepType.Freq_Sweep)
            {
                if (isFlagDbc)
                {
                    pltPim.SetChannelVisible(0, false);
                    pltPim.SetChannelVisible(1, false);
                    pltPim.SetChannelVisible(2, true);
                    pltPim.SetChannelVisible(3, true);
                }
                else
                {
                    pltPim.SetChannelVisible(0, true);
                    pltPim.SetChannelVisible(1, true);
                    pltPim.SetChannelVisible(2, false);
                    pltPim.SetChannelVisible(3, false);
                }
                FreDo(i, order);
            }
            else
            {
                TimeDo(order);
            }
        }

        #region 扫频操作
        /// <summary>
        /// 扫频操作
        /// </summary>
        private void FreDo(int i, int order)
        {
            flag = false;
            scanType = true;
            ScrollEnable(false, false, false);
            if (settings.EnableSquence)
            {
                string path = Application.StartupPath + "\\settings\\Sweep.ini";
                if (File.Exists(path))
                {
                    //使用指定的频率扫描模式
                    fsp = ps.GetFreqSweepParam(settings, path,
                                    GetMacId(App_Configure.Cnfgs.MacID),
                                    settings.PimSchema.ToString(),
                                    "sweep",
                                    order);
                }
                else
                {
                    //====用于远程连接的时候使用

                    SweepObj.errmessage.Add("The file does not exist!");
                    //=================
                    MessageBox.Show(this, "The file does not exist!");
                }
            }
            else
            {
                //nozuonodie
                Settings_Pim tempSettings = new Settings_Pim("");
                this.settings.Clone(tempSettings);
                if (offset_RemoteCtr)
                    tempSettings.Tx -= offset_P;
                fsp = ps.FreqSweep(tempSettings, order, i);
            }
        }
        #endregion

        #region 点频操作
        /// <summary>
        /// 点频操作
        /// </summary>
        private void TimeDo(int order)
        {
            flag = true;
            scanType = false;
            ScrollEnable(true, false, false);
            pltPim.SetXStartStop(0, x_stop);
            pltPim.SetYStartStop(start, stop);
            if (settings.EnableSquence)
            {
                string path = Application.StartupPath + "\\settings\\Sweep.ini";
                if (File.Exists(path))
                {
                    //使用指定的时间扫描模式
                    tsp = ps.GetTimeSweepParam(settings, path,
                                    GetMacId(App_Configure.Cnfgs.MacID),
                                    settings.PimSchema.ToString(),
                                    order,
                                    "fixed",
                                    settings.TimeSweepPoints + 1);
                }
                else
                {                  
                    SweepObj.errmessage.Add("The file does not exist!");
                    MessageBox.Show(this, "The file does not exist!");
                }
            }
            else
            {
                //nozuonodie
                Settings_Pim tempSettings = new Settings_Pim("");
                this.settings.Clone(tempSettings);
                if (offset_RemoteCtr)
                    tempSettings.Tx -= offset_P;
                tsp = ps.TimeSweep(tempSettings, order,
                          settings.TimeSweepPoints + 1);
            }
        }
        #endregion

        #endregion

        #region 通用按钮图片切换
        /// <summary>
        /// 通用按钮图片切换
        /// </summary>
        /// <param name="pb">PictureBox对象</param>
        /// <param name="picName">图片名称</param>
        private void ChangeBtnPic(PictureBox pb, string picName)
        {
            pb.Image = ImagesManage.GetImage(PicfolderName, picName);
        }

        private void ChangeBtnPic(Panel pl, string folderName, string picName)
        {
            pl.BackgroundImage = ImagesManage.GetImage(folderName, picName);
        }
        #endregion

        #region 收到消息，获取一个点的数据显示

        /// <summary>
        /// 收到消息，获取一个点的数据显示
        /// </summary>
        /// <param name="n"></param>
        /// <param name="num"></param>
        private void PimSucced(int n, int num)
        {
            if (flag)
                Time(num);
            else
                Freq(n, num);
        }

        #region 收到消息，扫频
        int L = 0;
        int Q = 0;
        //int r = 0;
        //int t = 0;
        //int D = 0;
        /// <summary>
        /// 收到消息，扫频
        /// </summary>
        /// <param name="n"></param>
        /// <param name="num"></param>
        private void 
            Freq(int n, int num)
        {
            SweepObj.Clone(ref ps1, ref ps2, ref sr, ref rfErrors_1, ref rfErrors_2);
           
            
            if (sr != null)
            {
                ps1.Status2.OutP -= App_Settings.spc.OutTxRef;
                ps2.Status2.OutP -= App_Settings.spc.OutTxRef;
                //nozuonodie
                if (offset_RemoteCtr && offset_DoneNormal)
                {
                   
                    ps1.Status2.OutP += offset_P;
                    ps2.Status2.OutP += offset_P;
                    sr.dBmValue -= offset_Val;
                }

                if (n == 0)
                {

                    NumofItem1 = num;
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        // ----------------------------------------
                        if (settings.PimSchema == ImSchema.REV)
                        {
                            sr.dBmValue = sr.dBmValue + Rx_Tables.Offset(fsp.Items1[num].Rx, FuncModule.PIM, true);
                        }
                        else
                        {
                            sr.dBmValue = sr.dBmValue + Rx_Tables.Offset(fsp.Items1[num].Rx, FuncModule.PIM, false);
                        }
                        //----------------------------------------
                        //ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                        //ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset2_disp);

                        if (App_Configure.Cnfgs.Mode >=2)
                        {
                            if (port1_rev_fwd == 1 || port1_rev_fwd == 2)
                            {
                                ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                                ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);
                            }
                            else
                            {
                                ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev2_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev2_offset1_disp);
                                ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev2_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev2_offset2_disp);
                            }
                        }
                        else
                        {
                            ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                            ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);
                        }
                    }
                    else
                    {
                        //----------------------------------------
                        if (settings.PimSchema == ImSchema.REV)
                        {
                            sr.dBmValue = (float)App_Factors.pim_rx1.ValueWithOffset(fsp.Items1[num].Rx, sr.dBmValue);
                        }
                        else if (settings.PimSchema == ImSchema.FWD)
                        {
                            sr.dBmValue = (float)App_Factors.pim_rx2.ValueWithOffset(fsp.Items1[num].Rx, sr.dBmValue);
                        }
                        //----------------------------------------
                        ps1.Status2.OutP = (float)App_Factors.pim_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                        ps2.Status2.OutP = (float)App_Factors.pim_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                    }
                    //Log.WriteLog(" read rx= " + fsp.Items1[num].Rx.ToString() + " pim=" + sr.dBmValue .ToString()+ "\r\n", Log.EFunctionType.PIM);
                    //功放功率没有达到时，做处理
                    if (num <= 2)
                    {
                        if (num == 0)
                        {
                            ps1.Status2.OutP = settings.Tx;
                            ps2.Status2.OutP = settings.Tx2;
                        }
                        else
                        {
                            if (Math.Abs(settings.Tx - ps1.Status2.OutP+App_Settings.spc.TxRef+App_Settings.spc.TxRef) <= 2)
                            {
                                ps1.Status2.OutP = settings.Tx;
                            }
                            if (Math.Abs(settings.Tx2 - ps2.Status2.OutP+App_Settings.spc.TxRef+App_Settings.spc.TxRef) <= 2)
                            {
                                ps2.Status2.OutP = settings.Tx2;
                            }
                        }
                    }
                    /////ygq
                    if (port1_rev_fwd == 1 || port1_rev_fwd == 2)
                    {
                        if (fsp.Items1[num].Rx < App_Settings.spfc.cbn.RxS || fsp.Items1[num].Rx > App_Settings.spfc.cbn.RxE)
                        {
                            pf[0].Y = wRvalue;
                            pf0[0].Y = wRvalue;
                            pf5[0].Y = wRvalue;
                            pf6[0].Y = wRvalue;
                            L = 1;

                        }
                        else
                        {
                            //nozuonodie
                            sr.dBmValue = DataDealWith(sr.dBmValue, n);

                            pf[0].Y = (float)Math.Round(sr.dBmValue, 1);
                            //pf0[0].Y = (float)Math.Round(sr.dBmValue - settings.Tx, 1);
                            pf0[0].Y = (float)Math.Round(sr.dBmValue - Dbm_avg(settings.Tx,settings.Tx2), 1);
                        }
                    }
                    else
                    {
                        if (fsp.Items1[num].Rx <1920 || fsp.Items1[num].Rx > 1980)
                        {
                            pf[0].Y = wRvalue;
                            pf0[0].Y = wRvalue;
                            pf5[0].Y = wRvalue;
                            pf6[0].Y = wRvalue;
                            L = 1;

                        }
                        else
                        {
                            //nozuonodie
                            sr.dBmValue = DataDealWith(sr.dBmValue, n);

                            pf[0].Y = (float)Math.Round(sr.dBmValue, 1);
                            //pf0[0].Y = (float)Math.Round(sr.dBmValue - settings.Tx, 1);
                            pf0[0].Y = (float)Math.Round(sr.dBmValue - Dbm_avg(settings.Tx, settings.Tx2), 1);
                        }
                    }
                    //ygq

                    pf[0].X = (float)Math.Round(fsp.Items1[num].Rx, 1);
                    pf0[0].X = (float)Math.Round(fsp.Items1[num].Rx, 1);
                    pf5[0].X = pf[0].X;
                    pf6[0].X = pf0[0].X;
                    //钱丹强模式==============
                    if (App_Configure.Cnfgs.Qiandanqiang_mode == 1)
                    {
                        //Random r = new Random(Guid.NewGuid().GetHashCode());
                        if (isFlagDbc)
                        {
                            if (pf0[0].Y > -153 && pf0[0].Y < -140)
                                pf0[0].Y -= 13;
                        }
                        else
                            if (pf[0].Y > -113 && pf[0].Y < -93)
                                pf[0].Y -= 13;
                    }
                    //===================
                    if (isFlagDbc)
                        SetPimY(pf0[0].Y);
                    else
                        SetPimY(pf[0].Y);
                    if (L == 1)
                    {
                        //D = r + 1;
                        pltPim.Add(pf5, 0, num);
                        pltPim.Add(pf6, 2, num);
                        L = 0;
                    }
                    else
                    {
                        pltPim.Add(pf, 0, num);
                        pltPim.Add(pf0, 2, num);
                    }
                    pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                }
                else
                {
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        //----------------------------------------
                        if (settings.PimSchema == ImSchema.REV)
                        {
                            sr.dBmValue = sr.dBmValue + Rx_Tables.Offset(fsp.Items2[num].Rx, FuncModule.PIM, true);
                        }
                        else
                        {
                            sr.dBmValue = sr.dBmValue + Rx_Tables.Offset(fsp.Items2[num].Rx, FuncModule.PIM, false);
                        }
                        //----------------------------------------
                        //ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                        //ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset2_disp);
                        if (App_Configure.Cnfgs.Mode >=2)
                        {
                            if (port1_rev_fwd == 1 || port1_rev_fwd == 2)
                            {
                                ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                                ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);
                            }
                            else
                            {
                                ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev2_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev2_offset1_disp);
                                ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev2_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev2_offset2_disp);
                            }
                        }
                        else
                        {
                            ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                            ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);
                        
                        }
                    }
                    else
                    {
                        //----------------------------------------
                        if (settings.PimSchema == ImSchema.REV)
                        {
                            sr.dBmValue = (float)App_Factors.pim_rx1.ValueWithOffset(fsp.Items2[num].Rx, sr.dBmValue);
                        }
                        else if (settings.PimSchema == ImSchema.FWD)
                        {
                            sr.dBmValue = (float)App_Factors.pim_rx2.ValueWithOffset(fsp.Items2[num].Rx, sr.dBmValue);
                        }
                        //----------------------------------------
                        ps1.Status2.OutP = (float)App_Factors.pim_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                        ps2.Status2.OutP = (float)App_Factors.pim_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                    }
                    if (num <= 1)
                    {
                        if (Math.Abs(settings.Tx - ps1.Status2.OutP+App_Settings.spc.TxRef+App_Settings.spc.TxRef) <= 2)
                        {
                            ps1.Status2.OutP = settings.Tx;
                        }
                        if (Math.Abs(settings.Tx2 - ps2.Status2.OutP+App_Settings.spc.TxRef+App_Settings.spc.TxRef) <= 2)
                        {
                            ps2.Status2.OutP = settings.Tx2;
                        }
                    }
                    ///ygq
                    ///
                    if (port1_rev_fwd == 1 || port1_rev_fwd == 2)
                    {
                        if (fsp.Items2[num].Rx < App_Settings.spfc.cbn.RxS || fsp.Items2[num].Rx > App_Settings.spfc.cbn.RxE)
                        {
                            pf[0].Y = wRvalue;
                            pf0[0].Y = wRvalue;
                            pf5[0].Y = wRvalue;
                            pf6[0].Y = wRvalue;
                            Q = 1;
                        }
                        else
                        {
                            //nozuonodie
                            sr.dBmValue = DataDealWith(sr.dBmValue, n);

                            pf[0].Y = (float)Math.Round(sr.dBmValue, 1);
                            //pf0[0].Y = (float)Math.Round(sr.dBmValue - settings.Tx2, 1);
                            pf0[0].Y = (float)Math.Round(sr.dBmValue - Dbm_avg(settings.Tx, settings.Tx2), 1);
                        }
                    }
                    else
                    {
                        if (fsp.Items2[num].Rx <1920 || fsp.Items2[num].Rx > 1980)
                        {
                            pf[0].Y = wRvalue;
                            pf0[0].Y = wRvalue;
                            pf5[0].Y = wRvalue;
                            pf6[0].Y = wRvalue;
                            Q = 1;
                        }
                        else
                        {
                            //nozuonodie
                            sr.dBmValue = DataDealWith(sr.dBmValue, n);

                            pf[0].Y = (float)Math.Round(sr.dBmValue, 1);
                            //pf0[0].Y = (float)Math.Round(sr.dBmValue - settings.Tx2, 1);
                            pf0[0].Y = (float)Math.Round(sr.dBmValue - Dbm_avg(settings.Tx, settings.Tx2), 1);
                        }
                    }
                    pf[0].X = (float)Math.Round(fsp.Items2[num].Rx, 1);
                    pf0[0].X = (float)Math.Round(fsp.Items2[num].Rx, 1);
                    pf5[0].X = pf[0].X;
                    pf6[0].X = pf0[0].X;
                    //钱丹强模式=====================
                    if (App_Configure.Cnfgs.Qiandanqiang_mode == 1)
                    {
                        //Random r = new Random(Guid.NewGuid().GetHashCode());
                       
                        if (isFlagDbc)
                        {
                            if (pf0[0].Y > -153 && pf0[0].Y < -140)
                                pf0[0].Y -= 13;
                        }
                        else
                            if (pf[0].Y > -113 && pf[0].Y < -93)
                                pf[0].Y -= 13;
                    }
                    if (isFlagDbc)
                        SetPimY(pf0[0].Y);
                    else
                        SetPimY(pf[0].Y);

                    if (Q == 1)
                    {
                        pltPim.Add(pf5, 1, g);
                        pltPim.Add(pf6, 3, g);
                        Q = 0;
                    }
                    else
                    {
                        pltPim.Add(pf, 1, g);
                        pltPim.Add(pf0, 3, g);
                    }

                    pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                    g++;
                }
                sum += 1;
                //美化功率显示补偿
                if (App_Configure.Cnfgs.OpenOffset == 1)
                {
                    if (Math.Abs(ps1.Status2.OutP - settings.Tx-App_Settings.spc.TxRef-App_Settings.spc.TxRef) > 0.1 && Math.Abs(ps1.Status2.OutP - settings.Tx-App_Settings.spc.TxRef-App_Settings.spc.TxRef) <= 0.9)
                    {
                        if ((ps1.Status2.OutP-App_Settings.spc.TxRef-App_Settings.spc.TxRef) >= settings.Tx)
                            ps1.Status2.OutP = settings.Tx + 0.1f;
                        else
                            ps1.Status2.OutP = settings.Tx - 0.1f;
                    }
                    else
                    {
                        if (n == 0)
                        {
                            if (num > 2)
                            {
                                ps1.Status2.OutP = ps1.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                            }
                        }
                        else
                        {
                            if (num > 1)
                            {
                                ps1.Status2.OutP = ps1.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                            }
                        }
                    }

                    if (Math.Abs(ps2.Status2.OutP - settings.Tx2-App_Settings.spc.TxRef-App_Settings.spc.TxRef) > 0.1 && Math.Abs(ps2.Status2.OutP - settings.Tx2 -App_Settings.spc.TxRef-App_Settings.spc.TxRef) <= 0.9)
                    {
                        if ((ps2.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef) >= settings.Tx2)
                            ps2.Status2.OutP = settings.Tx2 + 0.1f;
                        else
                            ps2.Status2.OutP = settings.Tx2 - 0.1f;
                    }
                    else
                    {
                        if (n == 0)
                        {
                            if (num > 2)
                            {
                                ps2.Status2.OutP = ps2.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                            }
                        }
                        else
                        {
                            if (num > 1)
                            {
                                ps2.Status2.OutP = ps2.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                            }
                        }
                    }

                }
                else
                {
                    ps1.Status2.OutP = ps1.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                    ps2.Status2.OutP = ps2.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                }


                pf1[0].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf1[0].Y = -2f;
                pf1[1].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf1[1].Y = (float)Math.Round(ps1.Status2.OutP, 1);
                //pf1[1].Y = (float)Math.Round(settings.Tx, 1);

                pf2[0].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf2[0].Y = -2f;
                pf2[1].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf2[1].Y = (float)Math.Round(ps2.Status2.OutP, 1);
                //pf2[1].Y = (float)Math.Round(settings.Tx, 1);


             
               

                pf3[0].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf3[0].Y = -2f;
                pf3[1].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf3[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)ps1.Status2.OutP / 10.0), 1);
                //pf3[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)settings.Tx / 10.0), 1);

                pf4[0].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf4[0].Y = -2f;
                pf4[1].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf4[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)ps2.Status2.OutP / 10.0), 1);
                //pf4[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)settings.Tx / 10.0), 1);

                pltPower.Add(pf1, 0, 0);
                pltPower.Add(pf2, 1, 0);
                pltPower.Add(pf3, 2, 0);
                pltPower.Add(pf4, 3, 0);
                lblN1.Tag = pf1[1].Y;
                lblN1.Text = "=" + pf1[1].Y.ToString() + "dBm";
                lblN2.Tag = pf2[1].Y;
                lblN2.Text = "=" + pf2[1].Y.ToString() + "dBm";
                lblN3.Text = "=" + pf3[1].Y.ToString() + "W";
                lblN4.Text = "=" + pf4[1].Y.ToString() + "W";
                //=================

                //=====================
                //DBM →DBM
                TableNewRow(dtDbmM, sum, pf1[1].Y, pf1[0].X, pf2[1].Y, pf2[0].X, pf[0].X, pf[0].Y);

                //DBM →DBC
                TableNewRow(dtDbmC, sum, pf1[1].Y, pf1[0].X, pf2[1].Y, pf2[0].X, pf[0].X, pf0[0].Y);

                //W →DBM   W →DBC
                TableNewRow(dtWdbc, sum, pf3[1].Y, pf1[0].X, pf4[1].Y, pf2[0].X, pf[0].X, pf0[0].Y);

                //W →DBM
                TableNewRow(dtWdbm, sum, pf3[1].Y, pf1[0].X, pf4[1].Y, pf2[0].X, pf[0].X, pf[0].Y);
                dgvPim.FirstDisplayedScrollingRowIndex = sum - 1;

                CreatScrollbar();

                //保存数据参数
                GetCsvEnty(sum, pf1[0].X, pf2[0].X, pf[0].X, pf[0].Y, pf1[1].Y, pf2[1].Y);
            }
        }
        #endregion

        #region 收到消息，点频
        /// <summary>
        /// 收到消息，点频
        /// </summary>
        /// <param name="num"></param>
        private void Time(int num)
        {
            SweepObj.Clone(ref ps1, ref ps2, ref sr, ref rfErrors_1, ref rfErrors_2);
            if (sr != null)
            {
                ps1.Status2.OutP -= App_Settings.spc.OutTxRef;
                ps2.Status2.OutP -= App_Settings.spc.OutTxRef;
                //nozuonodie
                if (offset_RemoteCtr && offset_DoneNormal)
                {
                  
                    ps1.Status2.OutP += offset_P;
                    ps2.Status2.OutP += offset_P;
                    sr.dBmValue -= offset_Val;
                }

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    //----------------------------------------
                    if (settings.PimSchema == ImSchema.REV)
                    {
                        sr.dBmValue = sr.dBmValue + Rx_Tables.Offset(tsp.Rx, FuncModule.PIM, true);                        
                    }
                    else
                    {
                        sr.dBmValue = sr.dBmValue + Rx_Tables.Offset(tsp.Rx, FuncModule.PIM, false);
                    }
                    //----------------------------------------

                    //ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                    //ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset2_disp);
                 //ygq
                    if (App_Configure.Cnfgs.Mode >=2)
                    {
                        if (port1_rev_fwd == 1 || port1_rev_fwd == 2)
                        {
                            ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                            ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);
                        }
                        else
                        {
                            ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev2_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev2_offset1_disp);
                            ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev2_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev2_offset2_disp);
                        }
                    }
                    else
                    {

                            ps1.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
                            ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);
                      
                    }
                    //ygq
                }
                else
                {
                    if (settings.PimSchema == ImSchema.REV)
                    {
                        sr.dBmValue = (float)App_Factors.pim_rx1.ValueWithOffset(tsp.Rx, sr.dBmValue);
                    }
                    else if (settings.PimSchema == ImSchema.FWD)
                    {
                        sr.dBmValue = (float)App_Factors.pim_rx2.ValueWithOffset(tsp.Rx, sr.dBmValue);

                    }
                    ps1.Status2.OutP = (float)App_Factors.pim_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                    ps2.Status2.OutP = (float)App_Factors.pim_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                }
                //功放功率没有达到时，做处理
                if (num == 0)
                {
                    ps1.Status2.OutP = settings.Tx ;
                    ps2.Status2.OutP = settings.Tx2 ;
                }

                //nozuonodie
                sr.dBmValue = DataDealWith(sr.dBmValue, 0);
                //美化功率显示补偿
                if (App_Configure.Cnfgs.OpenOffset == 1)
                {
                    //功率加上设置的tx补偿的正负1db
                    if (Math.Abs(ps1.Status2.OutP - settings.Tx - App_Settings.spc.TxRef - App_Settings.spc.TxRef) > 0.1 && Math.Abs(ps1.Status2.OutP - settings.Tx - App_Settings.spc.TxRef - App_Settings.spc.TxRef) <= 0.9)
                    {
                        if (ps1.Status2.OutP >= settings.Tx)
                            ps1.Status2.OutP = settings.Tx + +0.1f;
                        else
                            ps1.Status2.OutP = settings.Tx + -0.1f;
                    }
                    else
                    {
                        if(num!=0)
                        ps1.Status2.OutP = ps1.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                    }

                    if (Math.Abs(ps2.Status2.OutP - App_Settings.spc.TxRef - settings.Tx2 - App_Settings.spc.TxRef) > 0.1 && Math.Abs(ps2.Status2.OutP - App_Settings.spc.TxRef - settings.Tx2 - App_Settings.spc.TxRef) <= 0.9)
                    {
                        if (ps2.Status2.OutP >= settings.Tx2)
                            ps2.Status2.OutP = settings.Tx2 + +0.1f;
                        else
                            ps2.Status2.OutP = settings.Tx2 + -0.1f;
                    }
                    else
                    {
                        if (num != 0)
                            ps2.Status2.OutP = ps2.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                    }

                }
                else
                {
                    ps1.Status2.OutP = ps1.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                    ps2.Status2.OutP = ps2.Status2.OutP - App_Settings.spc.TxRef - App_Settings.spc.TxRef;
                }

                if (num == 0)
                {
                    PimStat = DateTime.Now;
                    pf[0].X = 0;
                    pf0[0].X = 0;
                }
                else
                {
                    DateTime PimEnd = DateTime.Now;
                    TimeSpan ts = PimEnd - PimStat;
                    //pf[0].X = num;
                    //pf0[0].X = num;
                    pf[0].X = ts.Seconds + ts.Milliseconds / 1000f;
                    pf0[0].X = ts.Seconds + ts.Milliseconds / 1000f;
                }
                PimTimeCount++;
                if (PimTimeCount == 50)
                {
                    PimTimeCount = 0;
                 
                }
                
                pf[0].Y = (float)Math.Round(sr.dBmValue, 1);
                //pf0[0].Y = (float)Math.Round(sr.dBmValue - settings.Tx, 1);
                pf0[0].Y = (float)Math.Round(sr.dBmValue - Dbm_avg( settings.Tx,settings.Tx2), 1);
                //钱丹强模式
                if (App_Configure.Cnfgs.Qiandanqiang_mode == 1)
                {
                    //Random r = new Random(Guid.NewGuid().GetHashCode());
                    if (isFlagDbc)
                    {
                        if (pf0[0].Y > -153 && pf0[0].Y < -140)
                            pf0[0].Y -= 13;
                    }
                    else
                        if (pf[0].Y > -113 && pf[0].Y < -93)
                            pf[0].Y -= 13;
                }
                if (isFlagDbc)
                {
                    lbl2.Text = pf0[0].Y.ToString("0.0") + " dBc";
                    lbl1.Text = pf[0].Y.ToString("0.0") + " dBm";
                    SetPimY(pf0[0].Y);
                    if(!App_Configure.Cnfgs.IsScrool)
                    SetPimX(pf0[0].X);
                }
                else
                {
                    lbl1.Text = pf[0].Y.ToString("0.0") + " dBm";
                    lbl2.Text = pf0[0].Y.ToString("0.0") + " dBc";
                    SetPimY(pf[0].Y);
                    if (!App_Configure.Cnfgs.IsScrool)
                    SetPimX(pf[0].X);
                }

                pltPim.Add(pf, 0, num);
                pltPim.Add(pf0, 2, num);
                sum += 1;
                pf1[0].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf1[0].Y = -2f;
                pf1[1].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf1[1].Y = (float)Math.Round(ps1.Status2.OutP, 1);

                pf2[0].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf2[0].Y = -2f;
                pf2[1].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf2[1].Y = (float)Math.Round(ps2.Status2.OutP, 1);

                pf3[0].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf3[0].Y = -2f;
                pf3[1].X = (float)Math.Round(ps1.Status2.Freq, 1);
                pf3[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)ps1.Status2.OutP / 10.0), 1);

                pf4[0].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf4[0].Y = -2f;
                pf4[1].X = (float)Math.Round(ps2.Status2.Freq, 1);
                pf4[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)ps2.Status2.OutP / 10.0), 1);
                lblN1.Tag = pf1[1].Y;
                lblN1.Text = "=" + pf1[1].Y.ToString() + "dBm";
                lblN2.Tag = pf2[1].Y;
                lblN2.Text = "=" + pf2[1].Y.ToString() + "dBm";
                lblN3.Text = "=" + pf3[1].Y.ToString() + "W";
                lblN4.Text = "=" + pf4[1].Y.ToString() + "W";
                pltPower.Add(pf1, 0, 0);
                pltPower.Add(pf2, 1, 0);
                pltPower.Add(pf3, 2, 0);
                pltPower.Add(pf4, 3, 0);

                TableNewRow(dtDbmM, sum, pf1[1].Y, pf1[0].X, pf2[1].Y, pf2[0].X, (float)Math.Round(tsp.Rx, 1), pf[0].Y);

                TableNewRow(dtDbmC, sum, pf1[1].Y, pf1[0].X, pf2[1].Y, pf2[0].X, (float)Math.Round(tsp.Rx, 1), pf0[0].Y);

                TableNewRow(dtWdbc, sum, pf3[1].Y, pf1[0].X, pf4[1].Y, pf2[0].X, (float)Math.Round(tsp.Rx, 1), pf0[0].Y);

                TableNewRow(dtWdbm, sum, pf3[1].Y, pf1[0].X, pf4[1].Y, pf2[0].X, (float)Math.Round(tsp.Rx, 1), pf[0].Y);
                dgvPim.FirstDisplayedScrollingRowIndex = num;
                CreatScrollbar();
                GetCsvEnty(sum, pf1[0].X, pf2[0].X, (float)Math.Round(tsp.Rx, 1), pf[0].Y, pf1[1].Y, pf2[1].Y);
            }
        }
        #endregion

        #endregion

        #region 自动适应pltPimY坐标
        /// <summary>
        /// 自动适应pltPimY坐标
        /// </summary>
        /// <param name="pf"></param>
        private void SetPimY(float pf)
        {
            if (pf > stop)
            {
                stop = pf + 20;
                pltPim.SetYStartStop(start, stop);
            }
            if (pf < start)
            {
                start = pf - 20;
                pltPim.SetYStartStop(start, stop);
            }
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
        }
        #endregion

        float x_changestop=0 ;
        private void SetPimX(float pf)
        {
            if (pf > x_changestop)
            {
                x_changestop += 5;
                pltPim.SetXStartStop(0, x_changestop);
            }
          
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
        }

        #region 保存文件格式分类

        #region SavePdf
        /// <summary>
        /// SavePdf
        /// </summary>
        /// <param name="pdfFileName">PDF文件名</param>
        private bool SavePdf(string pdfFileName,int num)
        {
            //MessageBox.Show("start savepdf!");
            try
            {
                if (!File.Exists(pdfFileName))
                {
                    PdfReport_Data data = new PdfReport_Data();
                    data.Desc = App_Configure.Cnfgs.Mac_Desc;
                    data.Modno = App_Configure.Cnfgs.Modno;
                    data.Serno = App_Configure.Cnfgs.Serno;
                    data.Opeor = App_Configure.Cnfgs.Opeor;
                    if (p.SweepType == SweepType.Freq_Sweep)
                    {
                        data.Points_Num = p.FrqParam.Items1.Length + p.FrqParam.Items2.Length;
                        //data.Tx_out = p.FrqParam.Items1[0].P1;
                        data.Tx_out = ps1.Status2.OutP;
                        data.F_start = p.FrqParam.Items1[0].Tx1;
                        data.F_stop = p.FrqParam.Items2[0].Tx2;
                        data.Footer = "Pim Frequency Sweep";
                    }
                    else
                    {
                        data.Points_Num = sum;
                        data.Tx_out = p.TmeParam.P1;
                        data.F_start = p.TmeParam.F1;
                        data.F_stop = p.TmeParam.F2;
                        data.Footer = "Pim Time Sweep";
                    }
                    //float maxValue = float.MinValue;
                    //float minValue = float.MaxValue;
                    //bool flag = Comparison(ref maxValue, ref minValue);
                    float maxValue = cpd.pe[num].max;
                    float minValue = cpd.pe[num].min;

                    limitValue = float.Parse(txtBox.Text);
                    data.Limit_value = limitValue;
                    data.Max_value = maxValue;
                    data.Min_value = minValue;
                    //if (data.Max_value > data.Limit_value || data.Max_value == -200 || data.Max_value == float.MinValue)
                    //    data.Passed = "FAIL";
                    //else
                    //    data.Passed = "PASS";
                    data.Passed = cpd.pe[num].result;

                    //Bitmap pimBmp = new Bitmap(pltPim.Width,
                    //                                      pltPim.Height,
                    //                                      PixelFormat.Format24bppRgb);
                    //Graphics g = Graphics.FromImage(pimBmp);
                    //g.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, pltPim.Width, pltPim.Height),
                    //                                        pltPim.BackColor,
                    //                                        pltPim.BackColor, 0f),
                    //                                        new Rectangle(0, 0, pltPim.Width, pltPim.Height));
                    //g.Dispose();
                    //pltPim.SaveImage(pimBmp);

                    //data.Image = pimBmp;

                    data.Image = cpd.pe[num].pimImage;

                    PdfReport_Pim pim = new PdfReport_Pim();
                    pim.Do_Print(pdfFileName, data, flag);
                    return true;
                }
                else
                {
                    MessageBox.Show(this, "The PDF file name has already existed!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("保存PDF文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }
        #endregion

        #region CsvReport_Pim_Entry
        /// <summary>
        /// 赋值CsvReport_Pim_Entry
        /// </summary>
        /// <param name="n"></param>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <param name="imf"></param>
        /// <param name="imv"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private void GetCsvEnty(int n, float f1, float f2, float imf, float imv, float p1, float p2)
        {
            CsvReport_Pim_Entry crpe = new CsvReport_Pim_Entry();
            crpe.No = n;
            crpe.F1 = f1;
            crpe.F2 = f2;
            crpe.Im_F = imf;
            crpe.Im_V = imv;
            crpe.P1 = p1;
            crpe.P2 = p2;
            lock (listCrp)
            {
                listCrp.Add(crpe);
            }
            //Log.WriteLog(imf.ToString(), Log.EFunctionType.PIM);
        }
        #endregion

        #region GetMacId
        /// <summary>
        /// 获取仪表型号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private string GetMacId(int id)
        {
            string mac = string.Empty;
            switch (id)
            {
                case 0: mac = "CDMA"; break;
                case 1: mac = "GSM"; break;
                case 2: mac = "DCS"; break;
                case 3: mac = "PCS"; break;
                case 4: mac = "WCDMA"; break;
            }
            return mac;
        }
        #endregion

        #region SaveCsv
        /// <summary>
        /// SaveCsv 0:CDMA, 1:GSM, 2:DCS, 3:PCS, 4:WCDMA
        /// </summary>
        /// <param name="csvFileName">CSV文件名</param>
        private bool SaveCsv(string csvFileName,int num)
        {
            try
            {
                if (!File.Exists(csvFileName))
                {
                    CsvReport_PIVH_Header cr = new CsvReport_PIVH_Header();
                    cr.Date_Time = DateTime.Now.ToString();
                    cr.Im_Order = settings.PimOrder;
                    cr.Im_Schema = settings.PimSchema;
                    cr.Limit_Value = settings.Limit_Pim;
                    cr.Mac_Desc = App_Configure.Cnfgs.Mac_Desc;
                    //cr.N = NumofItem1;
                    cr.N = cpd.pe[num].NumofItem1;
                    if (p.SweepType == SweepType.Freq_Sweep)
                    {
                        cr.Point_Num = p.FrqParam.Items1.Length + p.FrqParam.Items2.Length;
                        cr.Sweep_Start = p.FrqParam.Items1[0].Tx1;
                        cr.Sweep_Stop = p.FrqParam.Items2[0].Tx2;
                    }
                    else
                    {
                        cr.Point_Num = sum;
                        cr.Sweep_Start = p.TmeParam.F1;
                        cr.Sweep_Stop = p.TmeParam.F2;
                    }
                    cr.Swp_Type = p.SweepType;

                    //if (pltPim.Max_Y_Point(0).Y > pltPim.Max_Y_Point(1).Y)
                    //    cr.Y_Max_RL = pltPim.Max_Y_Point(0).Y;
                    //else
                    //    cr.Y_Max_RL = pltPim.Max_Y_Point(1).Y;
                    //if (pltPim.Min_Y_Point(0).Y < pltPim.Min_Y_Point(1).Y)
                    //    cr.Y_Min_RL = pltPim.Min_Y_Point(0).Y;
                    //else
                    //    cr.Y_Min_RL = pltPim.Min_Y_Point(1).Y;

                    //CsvReport_Pim_Entry[] cp = new CsvReport_Pim_Entry[listCrp.Count];
                    //for (int i = 0; i < listCrp.Count; i++)
                    //{
                    //    cp[i] = new CsvReport_Pim_Entry();
                    //    cp[i].No = listCrp[i].No;
                    //    cp[i].P1 = listCrp[i].P1;
                    //    cp[i].F1 = listCrp[i].F1;
                    //    cp[i].P2 = listCrp[i].P2;
                    //    cp[i].F2 = listCrp[i].F2;
                    //    cp[i].Im_F = listCrp[i].Im_F;
                    //    cp[i].Im_V = listCrp[i].Im_V;
                    //}

                    //cr.Y_Max_RL = cpd.pe[num].max;
                    //cr.Y_Min_RL = cpd.pe[num].min;

                    CsvReport_Pim_Entry[] cp = new CsvReport_Pim_Entry[cpd.pe[num].dtm.Rows.Count];
                    for (int i = 0; i < cpd.pe[num].dtm.Rows.Count; i++)
                    {
                        DataRow dr = cpd.pe[num].dtm.Rows[i];
                        cp[i] = new CsvReport_Pim_Entry();
                        cp[i].No = Convert.ToInt32(dr[0]);
                        cp[i].P1 = Convert.ToSingle(dr[1]);
                        cp[i].F1 = Convert.ToSingle(dr[2]);
                        cp[i].P2 = Convert.ToSingle(dr[3]);
                        cp[i].F2 = Convert.ToSingle(dr[4]);
                        cp[i].Im_F = Convert.ToSingle(dr[5]);
                        cp[i].Im_V = Convert.ToSingle(dr[6]);
                    }
                    CsvReport.Save_Csv_Pim(csvFileName, cp, cr);

                    return true;
                }
                else
                {
                    MessageBox.Show(this, "The CSV file name has already existed!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("保存CSV文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }


        private bool SaveT(string csvFileName)
        {
            try
            {
                if (!File.Exists(csvFileName))
                {
                    CsvReport_PIVH_Header cr = new CsvReport_PIVH_Header();
                    cr.Date_Time = DateTime.Now.ToString();
                    cr.Im_Order = settings.PimOrder;
                    cr.Im_Schema = settings.PimSchema;
                    cr.Limit_Value = settings.Limit_Pim;
                    cr.Mac_Desc = App_Configure.Cnfgs.Mac_Desc;
                    cr.N = NumofItem1;
                    if (p.SweepType == SweepType.Freq_Sweep)
                    {
                        cr.Point_Num = p.FrqParam.Items1.Length + p.FrqParam.Items2.Length;
                        cr.Sweep_Start = p.FrqParam.Items1[0].Tx1;
                        cr.Sweep_Stop = p.FrqParam.Items2[0].Tx2;
                    }
                    else
                    {
                        cr.Point_Num = sum;
                        cr.Sweep_Start = p.TmeParam.F1;
                        cr.Sweep_Stop = p.TmeParam.F2;
                    }
                    cr.Swp_Type = p.SweepType;

                    if (pltPim.Max_Y_Point(0).Y > pltPim.Max_Y_Point(1).Y)
                        cr.Y_Max_RL = pltPim.Max_Y_Point(0).Y;
                    else
                        cr.Y_Max_RL = pltPim.Max_Y_Point(1).Y;
                    if (pltPim.Min_Y_Point(0).Y < pltPim.Min_Y_Point(1).Y)
                        cr.Y_Min_RL = pltPim.Min_Y_Point(0).Y;
                    else
                        cr.Y_Min_RL = pltPim.Min_Y_Point(1).Y;

                    CsvReport_Pim_Entry[] cp = new CsvReport_Pim_Entry[listCrp.Count];
                    for (int i = 0; i < listCrp.Count; i++)
                    {
                        cp[i] = new CsvReport_Pim_Entry();
                        cp[i].No = listCrp[i].No;
                        cp[i].P1 = listCrp[i].P1;
                        cp[i].F1 = listCrp[i].F1;
                        cp[i].P2 = listCrp[i].P2;
                        cp[i].F2 = listCrp[i].F2;
                        cp[i].Im_F = listCrp[i].Im_F;
                        cp[i].Im_V = listCrp[i].Im_V;
                    }      
                    CsvReport.SaveTxt(csvFileName, cp, cr, true);

                    return true;
                }
                else
                {
                    MessageBox.Show(this, "The TXT file name has already existed!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("保存TXT文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }
        #endregion


        void ReadCurrentPort(string fileName, List<CsvReport_Pim_Entry> entries)
        {
            try
            {
                string str_num = fileName.Substring(fileName.Length - 5, 1);
                int num = Convert.ToInt32(str_num) - 1;

                for (int i = 0; i < entries.Count; i++)
                {
                    cpd.ClearPortDatatable(num);
                    TableNewRow(cpd.pe[num].dt, entries[i].No, entries[i].P1, entries[i].F1, entries[i].P2, entries[i].F2, entries[i].Im_F, entries[i].Im_V);
                }
                if (current_port != num + 1)
                {
                    CloseButton(current_port);
                    current_port = num + 1;
                    Setport_methodN(num + 1);
                    ChangeBtnPic((PictureBox)control_port[num], "port" + (current_port).ToString() + ".jpg");
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        #region ReadCsv

        /// <summary>
        /// 回放数据
        /// </summary>
        /// <param name="csvFileName"></param>
        private void ReadCsv(string csvFileName)
        {
            isFlagDbc = false;
            ClearS();
            if (readListCrp != null)
                readListCrp.Clear();
            k = 0;
            j = 0;
            header = new CsvReport_PIVH_Header();
            bool flag = CsvReport.Read_Csv_Pim(csvFileName, out readListCrp, out header);
            //if(App_Configure.Cnfgs.Ms_switch_port_count>0)
            //ReadCurrentPort(csvFileName, readListCrp);
            if (flag)
            {
                isRead = true;
                zoomFlag = false;
                ChangeBtnPic(pbxL, "zoomin.bmp");
                pltPower.SetXStartStop(header.Sweep_Start, header.Sweep_Stop);
                txtBox.Text = header.Limit_Value.ToString();
                pltPim.SetLimitEnalbe(true, header.Limit_Value, Color.FromArgb(160, 245, 255));
                start = wRvalue;
                stop = -70;
                pltPim.SetYStartStop(start, stop);
                pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                string order = header.Im_Order.ToString();
                dgvPim.Columns[1].HeaderText = "P1(dBm)";
                dgvPim.Columns[3].HeaderText = "P2(dBm)";
                dgvPim.Columns[5].HeaderText = order + "_F(MHz)";
                dgvPim.Columns[6].HeaderText = order + "_V(dBm)";
                lblIM.Text = "IM ORDER" + ((int)header.Im_Order).ToString();
                settings.PimOrder = header.Im_Order;
                ChangeBtnPic(pnlIm, "dbm", ((int)settings.PimOrder).ToString() + ".png");
                limitSelect.Text = "Pass/Fail limit(dBm)";
                settings.PimUnit = ImUint.dBm;
                settings.PimSchema = header.Im_Schema;

                pltPim.SetChannelVisible(2, false);
                pltPim.SetChannelVisible(3, false);
                pltPim.SetChannelVisible(0, true);
                pltPim.SetChannelVisible(1, true);

                pltPower.SetChannelVisible(2, false);
                pltPower.SetChannelVisible(3, false);
                pltPower.SetChannelVisible(0, true);
                pltPower.SetChannelVisible(1, true);
                pltPower.SetYStartStop(0, 50);
                pltPim.SetMarkText(PimMarkText);
                pltPim.SetPeakText(PimPeakText);
                lblN3.Visible = false;
                lblN4.Visible = false;
                lblN1.Visible = true;
                lblN2.Visible = true;
                dgvPim.DataSource = dtDbmM;
                ChangeBtnPic(pbxStart, "start.gif");
                if (header.Swp_Type == SweepType.Freq_Sweep)
                {
                    ScrollEnable(false, false, false);
                    //ChangeBtnPic(pblFt, "other", "frequency.png");
                    ChangeBtnPic(pictureBox6, "freqtotime.gif");
                    ft = false;
                    settings.SweepType = SweepType.Freq_Sweep;
                    pnlFpim.Visible = false;
                    s[0] = 0;
                    s[1] = 1;
                    s[2] = -1;
                    s[3] = -1;
                    s[4] = -1;
                }
                else
                {
                    s[0] = 0;
                    s[1] = -1;
                    s[2] = -1;
                    s[3] = -1;
                    s[4] = -1;
                    lbl1.Text = "";
                    lbl2.Visible = false;
                    lbl1.Visible = true;
                    //pblFt.Location = new Point(226, 274);
                    //ChangeBtnPic(pblFt, "other", "time.png");
                    ChangeBtnPic(pictureBox6, "timetofreq.gif");
                    ft = true;
                    pltPim.SetXStartStop(0, x_stop);
                    settings.SweepType = SweepType.Time_Sweep;
                    pnlFpim.Visible = true;
                    ScrollEnable(true, false, false);
                }
                ChangeOrder((int)header.Im_Order);
                pltPim.SetMarkVisible(1, false);
                pltPim.SetMarkVisible(0, true);
                pltPim.SetMarkSequence(0, s);
                Timer.Interval = 300;
                Timer.Start();
                ChangeBtnPic(pbxPeak, "peak.gif");
                pltPim.Peak();
            }
            else
                isRead = false;
        }

        #region Timer
        int k = 0; //记数器
        int j = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (k < readListCrp.Count)
            {
                if (k != 0 && k % header.Point_Num == 0 && k < readListCrp.Count - 1)
                {
                    pltPim.Clear();
                    pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                }
                pf[0].X = readListCrp[k].Im_F;
                pf0[0].X = readListCrp[k].Im_F;
                pf[0].Y = readListCrp[k].Im_V;
                pf0[0].Y = readListCrp[k].Im_V - (readListCrp[k].P1 + readListCrp[k].P2) / 2;

                if (header.Swp_Type == SweepType.Freq_Sweep)
                {
                    if (k > header.N)
                    {
                        pltPim.Add(pf, 1, j);
                        pltPim.Add(pf0, 3, j);
                        j++;
                    }
                    else
                    {
                        pltPim.Add(pf, 0, k);
                        pltPim.Add(pf0, 2, k);
                    }
                }
                else
                {
                    pf[0].X = k;
                    pf0[0].X = k;
                    lbl1.Text = pf[0].Y.ToString("0.0") + " dBm";
                    lbl2.Text = pf0[0].Y.ToString("0.0") + " dBc";
                    pltPim.Add(pf, 0, k);
                    pltPim.Add(pf0, 2, k);
                }
                pf1[0].X = readListCrp[k].F1;
                pf1[0].Y = -2;
                pf1[1].X = readListCrp[k].F1;
                pf1[1].Y = readListCrp[k].P1;

                pf2[0].X = readListCrp[k].F2;
                pf2[0].Y = -2;
                pf2[1].X = readListCrp[k].F2;
                pf2[1].Y = readListCrp[k].P2;

                pf3[0].X = readListCrp[k].F1;
                pf3[0].Y = -2;
                pf3[1].X = readListCrp[k].F1;
                pf3[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)readListCrp[k].P1 / 10.0), 1);

                pf4[0].X = readListCrp[k].F2;
                pf4[0].Y = -2;
                pf4[1].X = readListCrp[k].F2;
                pf4[1].Y = (float)Math.Round(0.001 * Math.Pow(10, (double)readListCrp[k].P2 / 10.0), 1);

                pltPower.Add(pf1, 0, 0);
                pltPower.Add(pf2, 1, 0);
                pltPower.Add(pf3, 2, 0);
                pltPower.Add(pf4, 3, 0);
                lblN1.Tag = pf1[1].Y;
                lblN1.Text = "=" + pf1[1].Y.ToString() + "dBm";
                lblN2.Tag = pf2[1].Y;
                lblN2.Text = "=" + pf2[1].Y.ToString() + "dBm";
                lblN3.Text = "=" + pf3[1].Y.ToString() + "W";
                lblN4.Text = "=" + pf4[1].Y.ToString() + "W";

                //DBM →DBM
                TableNewRow(dtDbmM, k + 1, pf1[1].Y, pf1[0].X, pf2[1].Y, pf2[0].X, readListCrp[k].Im_F, pf[0].Y);

                //DBM →DBC
                TableNewRow(dtDbmC, k + 1, pf1[1].Y, pf1[0].X, pf2[1].Y, pf2[0].X, readListCrp[k].Im_F, pf0[0].Y);

                //W →DBM   W →DBC
                TableNewRow(dtWdbc, k + 1, pf3[1].Y, pf1[0].X, pf4[1].Y, pf2[0].X, readListCrp[k].Im_F, pf0[0].Y);

                //W →DBM
                TableNewRow(dtWdbm, k + 1, pf3[1].Y, pf1[0].X, pf4[1].Y, pf2[0].X, readListCrp[k].Im_F, pf[0].Y);
                dgvPim.FirstDisplayedScrollingRowIndex = k;
                CreatScrollbar();
            }
            else
            {
                Timer.Stop();
                isRead = false;
                ChangeBtnPic(pbxStart, "start_in.gif");
                ChangeBtnPic(pbxPeak, "peak_in.gif");
                if (settings.SweepType == SweepType.Freq_Sweep)
                    ScrollEnable(false, false, false);
                else
                    ScrollEnable(false, true, true);
            }
            k++;
        }
        #endregion

        #endregion

        #region SaveJpg
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jpgFileName">JPG文件名</param>
        private bool SaveJpg(string jpgFileName)
        {
            try
            {
                if (!File.Exists(jpgFileName))
                {
                    string strTitle = "";
                    Image pimImage = JpgReport.GetWindow(this.Handle);
                    Graphics g = Graphics.FromImage(pimImage);
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Near;
                    strTitle = "Time: " + DateTime.Now.ToString();
                    g.DrawImage(pimImage, new Point(0, 0));
                    g.DrawString(strTitle, new Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.White),
                    new PointF(800, 10), drawFormat);
                    pimImage.Save(jpgFileName);
                    this.Refresh();
                    return true;
                }
                else
                {
                    MessageBox.Show(this, "The JPG file name has already existed!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("保存JPG文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }
        private bool SaveJpg_somePort(int num)
        {
            try
            {
                
                    string strTitle = "";
                    Image pimImage = JpgReport.GetWindow(this.Handle);
                    Graphics g = Graphics.FromImage(pimImage);
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.Alignment = StringAlignment.Near;
                    strTitle = "Time: " + DateTime.Now.ToString();
                    g.DrawImage(pimImage, new Point(0, 0));
                    g.DrawString(strTitle, new Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.White),
                    new PointF(800, 10), drawFormat);
                    cpd.pe[num].image = pimImage;
                    this.Refresh();
                    return true;
                
                
            }
            catch (Exception e)
            {
                Log.WriteLog("保存JPG文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }
        private bool SaveJpg_somePort2(int num)
        {
            try
            {

                string strTitle = "";
                Image pimImage = JpgReport.GetWindow(this.Handle);
                Graphics g = Graphics.FromImage(pimImage);
                StringFormat drawFormat = new StringFormat();
                drawFormat.Alignment = StringAlignment.Near;
                strTitle = "Time: " + DateTime.Now.ToString();
                g.DrawImage(pimImage, new Point(0, 0));
                g.DrawString(strTitle, new Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.White),
                new PointF(800, 10), drawFormat);
                cpd.pe[num].pimImage = pimImage;
                this.Refresh();
                return true;


            }
            catch (Exception e)
            {
                Log.WriteLog("保存JPG文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }
        private bool SaveJpg(string jpgFileName,int num)
        {
            try
            {
                if (!File.Exists(jpgFileName))
                {
                    cpd.pe[num].image.Save(jpgFileName);
                    this.Refresh();
                    return true;
                }
                else
                {
                    MessageBox.Show(this, "The JPG file name has already existed!");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("保存JPG文件异常：" + e.ToString(), Log.EFunctionType.PIM);
                return false;
            }
        }

        public System.Drawing.Image SaveImage(Control handel)
        {

            string strTitle = "";
            System.Drawing.Image pimImage = JpgReport.GetWindow(handel.Handle);//需要保存的图形控件
            Graphics g = Graphics.FromImage(pimImage);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Near;
            strTitle = "Time: " + DateTime.Now.ToString();
            g.DrawImage(pimImage, new Point(0, 0));//画图
            g.DrawString(strTitle, new System.Drawing.Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.White),
            new PointF(800, 10), drawFormat);
            g.Dispose();
            this.Refresh();
            return pimImage;
        }
        #endregion

        #endregion

        #region ISweep 成员 用于模块切换关闭功放

        public void BreakSweep(int timeOut)
        {
            SweepObj.StopSweep(timeOut);
            Timer.Stop();
            sweepTimes = 0;
            isRead = false;
            ChangeBtnPic(pbxStart, "start_in.gif");
            ChangeBtnPic(pbxPeak, "peak_in.gif");
        }

        #endregion

        #region Panel 事件

        #region  点中图片按钮切换点频和扫频操作
        private void pblFt_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                if (scanType)
                    lblMaxPim.Visible = true;
                else
                    lblMaxPim.Visible = true;
                if (!ft)
                {
                    ScrollEnable(true, false, false);
                    ChangeBtnPic(pblFt, "other", "time.png");
                    ft = true;
                    settings.SweepType = SweepType.Time_Sweep;
                    this.pltPim.SetXStartStop(0, x_stop);
                    if (isFlagDbc)
                    {
                        lbl1.Visible = false;
                        lbl2.Visible = true;
                    }
                    else
                    {
                        lbl2.Visible = false;
                        lbl1.Visible = true;
                    }
                    pnlFpim.Visible = true;
                }
                else
                {
                    ScrollEnable(false, false, false);
                    ChangeBtnPic(pblFt, "other", "frequency.png");
                    ft = false;
                    settings.SweepType = SweepType.Freq_Sweep;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                    pnlFpim.Visible = false;
                }
                ChangeOrder((int)settings.PimOrder);
            }

        }
        #endregion

        #region 点F1、F2设置功率频率操作
        private void lblF1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                //客户修改数据后保存，（2013-1-25新加）
                float value = ReturnFreqValue((int)settings.SweepType, (int)settings.PimOrder, true);

                F1Reset f1Reset = new F1Reset((int)settings.SweepType, value, settings.F1e, settings.Tx, settings.Setp1);
                DialogResult dr = f1Reset.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    settings.Tx = Convert.ToSingle(f1Reset.nudPower.Value);
                    //settings.F1 = Convert.ToSingle(f1Reset.nudFre.Value);
                    settings.F1s = Convert.ToSingle(f1Reset.nudFre.Value);
                    settings.F1e = Convert.ToSingle(f1Reset.nudFreE.Value);
                    settings.Setp1 = Convert.ToSingle(f1Reset.nudStep.Value);
                    //保存设置（2013-1-25新加）
                    SaveSettings((int)settings.PimOrder, (int)settings.SweepType, true, false);

                    lblN1.Text = "=" + f1Reset.nudPower.Value.ToString() + "dBm";
                    lblN2.Text = "=" + settings.Tx2.ToString() + "dBm";
                    float v = ChangePimTxt(settings.F1s, settings.F2e, (int)settings.PimOrder);
                    pimTxt.Text = v.ToString("0.0");
                }
            }

        }

        private void lblF2_Click(object sender, EventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                //客户修改数据后保存，（2013-1-25新加）
                float value = ReturnFreqValue((int)settings.SweepType, (int)settings.PimOrder, false);

                F2Reset f2Reset = new F2Reset((int)settings.SweepType, settings.F2s, value, settings.Tx2, settings.Setp2);
                DialogResult dr = f2Reset.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    settings.Tx2 = Convert.ToSingle(f2Reset.nudPower.Value);

                   
                    //settings.F2 = Convert.ToSingle(f2Reset.nudFre.Value);
                    settings.F2s = Convert.ToSingle(f2Reset.nudFreS.Value);
                    settings.F2e = Convert.ToSingle(f2Reset.nudFre.Value);
                    settings.Setp2 = Convert.ToSingle(f2Reset.nudStep.Value);
                    //保存设置（2013-1-25新加）
                    SaveSettings((int)settings.PimOrder, (int)settings.SweepType, false, true);

                    lblN1.Text = "=" + settings.Tx.ToString() + "dBm";
                    lblN2.Text = "=" + f2Reset.nudPower.Value.ToString() + "dBm";
                    float v = ChangePimTxt(settings.F1s, settings.F2e, (int)settings.PimOrder);
                    pimTxt.Text = v.ToString("0.0");
                }
            }
        }

        /// <summary>
        /// 改变点频pimTxt
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <param name="order"></param>
        private float ChangePimTxt(float f1, float f2, int order)
        {
            int n, m;
            float f;
            m = (order - 1) / 2;
            n = m + 1;
            
            if(App_Configure.Cnfgs.Mode==0)
                f = (float)Math.Round(n * f2 - m * f1, 1);
            else if (App_Configure.Cnfgs.Mode == 1)
                f = (float)Math.Round(n * f1 - m * f2, 1);
            else
                f = (float)Math.Round(n * f1 - m * f2, 1);
            return f;
        }
        #endregion

        #region 上下箭头点击操作

        private void pnlUp1_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pnlUp1, "other", "up1.png");
        }

        private void pnlUp1_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pnlUp1, "other", "up2.png");
        }

        private void pnlDown1_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pnlDown1, "other", "down1.png");
        }

        private void pnlDown1_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pnlDown1, "other", "down2.png");
        }

        private void pnlUp2_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pnlUp2, "other", "up2.png");
        }

        private void pnlUp2_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pnlUp2, "other", "up1.png");
        }

        private void pnlDown2_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pnlDown2, "other", "down1.png");
        }

        private void pnlDown2_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pnlDown2, "other", "down2.png");
        }

        private void pnlUp2_MouseClick(object sender, MouseEventArgs e)
        {
            float num = float.Parse(txtBox.Text) + 1;
            if (isFlagDbc)
            {
                settings.Limit_Pim = num + settings.Tx;
            }
            else
            {
                settings.Limit_Pim = num;
            }
            txtBox.Text = num.ToString();
            pltPim.SetLimitEnalbe(true, num, Color.FromArgb(160, 245, 255));
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
        }

        private void pnlDown2_MouseClick(object sender, MouseEventArgs e)
        {
            float num = float.Parse(txtBox.Text) - 1;
            if (isFlagDbc)
            {
                settings.Limit_Pim = num + settings.Tx;
            }
            else
            {
                settings.Limit_Pim = num;
            }
            txtBox.Text = num.ToString();
            pltPim.SetLimitEnalbe(true, num, Color.FromArgb(160, 245, 255));
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
        }

        private void pnlUp1_MouseClick(object sender, MouseEventArgs e)
        {
            start += 10;
            stop += 10;
            pltPim.SetYStartStop(start, stop);
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
        }

        private void pnlDown1_MouseClick(object sender, MouseEventArgs e)
        {
            start -= 10;
            stop -= 10;
            pltPim.SetYStartStop(start, stop);
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
        }

        #endregion

        #region dBc/dBm的切换

        #region dBc 切换设置


        double Dbm_avg(float p1, float p2)
        {
            double w1 = Math.Pow(10,(p1 / 10));
            double w2 = Math.Pow( 10,(p2 / 10));

            double dbm = 10 * Math.Log10((w1 + w2) / 2);
            return dbm;
        }
        private void ChangeDbc()
        {
            isFlagDbc = true;
            ChangeBtnPic(pnlIm, "dbc", ((int)settings.PimOrder).ToString() + ".png");
            limitSelect.Text = "Pass/Fail limit(dBc)";
            float limit_pim = 0;
            if (Convert.ToSingle(lblN1.Tag) != 0 && Convert.ToSingle(lblN2.Tag) != 0)
            {
                //limit_pim = (float)Math.Round(float.Parse(txtBox.Text) - settings.Tx, 1);
                limit_pim = (float)Math.Round(float.Parse(txtBox.Text) - Dbm_avg( settings.Tx,settings.Tx2), 1);
                txtBox.Text = limit_pim.ToString();
            }

            pltPim.SetLimitEnalbe(true, limit_pim, Color.FromArgb(160, 245, 255));
            start = wRvalue;
            stop = -100;
            pltPim.SetYStartStop(start, stop);
            settings.PimUnit = ImUint.dBc;
            pltPim.SetChannelVisible(0, false);
            pltPim.SetChannelVisible(1, false);
            pltPim.SetChannelVisible(2, true);
            pltPim.SetChannelVisible(3, false);
            if (settings.SweepType == SweepType.Freq_Sweep)
                pltPim.SetChannelVisible(3, true);
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
            pltPim.SetChannelColor(2, Color.Yellow);
            pltPim.SetChannelColor(3, Color.Red);
            dgvPim.Columns[6].HeaderText = "Im" + ((int)settings.PimOrder).ToString() + "_V(dBc)";

            if (dtWdbc.Rows.Count > 0 || dtDbmC.Rows.Count > 0)
            {
                if (isFlagW)
                {
                    dgvPim.DataSource = dtWdbc;
                }
                else
                {
                    dgvPim.DataSource = dtDbmC;
                }
            }
        }
        #endregion

        #region dBmm 切换设置
        private void ChangeDbmm()
        {
            isFlagDbc = false;
            ChangeBtnPic(pnlIm, "dbm", ((int)settings.PimOrder).ToString() + ".png");
            limitSelect.Text = "Pass/Fail limit(dBm)";
            float limit_pim = 0;
            if (Convert.ToSingle(lblN1.Tag) != 0 && Convert.ToSingle(lblN2.Tag) != 0)
            {
                //limit_pim = (float)Math.Round(float.Parse(txtBox.Text) + settings.Tx, 1);
                limit_pim = (float)Math.Round(float.Parse(txtBox.Text) + Dbm_avg( settings.Tx,settings.Tx2), 1);
                txtBox.Text = limit_pim.ToString();
            }

            pltPim.SetLimitEnalbe(true, limit_pim, Color.FromArgb(160, 245, 255));
            start = wRvalue;
            stop = -70;
            pltPim.SetYStartStop(start, stop);
            settings.PimUnit = ImUint.dBm;
            pltPim.SetChannelVisible(0, true);
            pltPim.SetChannelVisible(1, false);
            pltPim.SetChannelVisible(2, false);
            pltPim.SetChannelVisible(3, false);
            if (settings.SweepType == SweepType.Freq_Sweep)
                pltPim.SetChannelVisible(1, true);
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
            pltPim.SetChannelColor(0, Color.Yellow);
            pltPim.SetChannelColor(1, Color.Red);
            dgvPim.Columns[6].HeaderText = "Im" + ((int)settings.PimOrder).ToString() + "_V(dBm)";

            if (dtWdbm.Rows.Count > 0 || dtDbmM.Rows.Count > 0)
            {
                if (isFlagW)
                {
                    dgvPim.DataSource = dtWdbm;
                }
                else
                {
                    dgvPim.DataSource = dtDbmM;
                }
            }
        }
        #endregion

        private void pnlIm_MouseClick(object sender, MouseEventArgs e)
        {
            max = float.MinValue;
            min = float.MaxValue;
            if (!isFlagDbc)
            {
                if (settings.SweepType == SweepType.Freq_Sweep)
                {
                    s[0] = -1;
                    s[1] = -1;
                    s[2] = 2;
                    s[3] = 3;
                    s[4] = -1;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                }
                else
                {
                    s[0] = -1;
                    s[1] = -1;
                    s[2] = 2;
                    s[3] = -1;
                    s[4] = -1;
                    lbl1.Visible = false;
                    lbl2.Visible = true;
                }
                pltPim.SetMarkVisible(0, false);
                pltPim.SetMarkVisible(1, true);
                pltPim.SetMarkSequence(1, s);
                pltPim.Peak();
                ChangeDbc();
            }
            else
            {
                if (settings.SweepType == SweepType.Freq_Sweep)
                {
                    s[0] = 0;
                    s[1] = 1;
                    s[2] = -1;
                    s[3] = -1;
                    s[4] = -1;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                }
                else
                {
                    s[0] = 0;
                    s[1] = -1;
                    s[2] = -1;
                    s[3] = -1;
                    s[4] = -1;
                    lbl2.Visible = false;
                    lbl1.Visible = true;
                }
                pltPim.SetMarkVisible(1, false);
                pltPim.SetMarkVisible(0, true);
                pltPim.SetMarkSequence(0, s);
                pltPim.Peak();
                ChangeDbmm();
            }
        }
        #endregion

        #region dBm/W的切换

        #region dBm 切换设置
        private void ChangeDbm()
        {
            isFlagW = false;
            ChangeBtnPic(pnlLevel, "other", "dbm.png");
            pltPower.SetChannelVisible(2, false);
            pltPower.SetChannelVisible(3, false);
            pltPower.SetChannelVisible(0, true);
            pltPower.SetChannelVisible(1, true);
            pltPower.SetYStartStop(0, 50);
            lblN3.Visible = false;
            lblN4.Visible = false;
            lblN1.Visible = true;
            lblN2.Visible = true;
            dgvPim.Columns[1].HeaderText = "P1(dBm)";
            dgvPim.Columns[3].HeaderText = "P2(dBm)";
            if (dtWdbc.Rows.Count > 0 || dtWdbm.Rows.Count > 0)
            {
                if (isFlagDbc)
                {
                    dgvPim.DataSource = dtDbmC;
                }
                else
                {
                    dgvPim.DataSource = dtDbmM;
                }
            }
        }
        #endregion

        #region W 切换设置
        private void ChangeW()
        {
            isFlagW = true;
            ChangeBtnPic(pnlLevel, "other", "w.png");
            pltPower.SetChannelVisible(0, false);
            pltPower.SetChannelVisible(1, false);
            pltPower.SetChannelVisible(2, true);
            pltPower.SetChannelVisible(3, true);
            pltPower.SetYStartStop(0, 30);
            lblN1.Visible = false;
            lblN2.Visible = false;
            lblN3.Visible = true;
            lblN4.Visible = true;
            dgvPim.Columns[1].HeaderText = "P1(W)";
            dgvPim.Columns[3].HeaderText = "P2(W)";

            if (dtWdbc.Rows.Count > 0 || dtWdbm.Rows.Count > 0)
            {
                if (isFlagDbc)
                {
                    dgvPim.DataSource = dtWdbc;
                }
                else
                {
                    dgvPim.DataSource = dtWdbm;
                }
            }
        }
        #endregion

        private void pnlLevel_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isFlagW)
                ChangeW();
            else
                ChangeDbm();
        }
        #endregion

        #region TouchPad弹出事件
        private void txtBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string defaultValue = txtBox.Text.Trim();
            try
            {
                TouchPad testTouchPad = new TouchPad(ref txtBox, defaultValue);
                DialogResult dr = testTouchPad.ShowDialog();
                if (dr == DialogResult.OK)
                {

                    float num = float.Parse(txtBox.Text.Trim());
                    if (isFlagDbc)
                    {
                        settings.Limit_Pim = num + settings.Tx;
                    }
                    else
                    {
                        settings.Limit_Pim = num;
                    }
                    pltPim.SetLimitEnalbe(true, num, Color.FromArgb(160, 245, 255));
                    pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
                }
            }
            catch { txtBox.Text = defaultValue; }
        }
        #endregion

        #endregion

        #region 保存客户修改的数据，调用时并且返回保存的数据

        /// <summary>保存客户修改的配置
        /// 
        /// </summary>
        /// <param name="im">互调阶数</param>
        /// <param name="sweepType">扫描模式（扫频，扫时）</param>
        /// <param name="SaveF1">是否保存F1的设置</param>
        /// <param name="SaveF2">是否保存F2的设置</param>
        private void SaveSettings(int order, int sweepType, bool SaveF1, bool SaveF2)
        {
            IniFile.SetString("pim", "tx", settings.Tx.ToString(),  "D:\\settings\\Settings_Pim.ini");
           

            int im = ReturnImsIndex(order);
            if (sweepType == (int)SweepType.Freq_Sweep)//扫频
            {
                SaveIms(im, SaveF1, SaveF2);
            }
            else
            {
                if (SaveF1)
                {
                    App_Settings.spfc.ims[im].F1fixed = settings.F1s;
                    IniFile.SetString("Specifics", "ord" + order.ToString() + "_F1fixed", settings.F1s.ToString(), "D:\\settings\\Specifics.ini");
                }
                if (SaveF2)
                {
                    App_Settings.spfc.ims[im].F2fixed = settings.F2e;
                    IniFile.SetString("Specifics", "ord" + order.ToString() + "_F2fixed", settings.F2e.ToString(),  "D:\\settings\\Specifics.ini");
                }
            }
        }

        /// <summary>把数据重新存回到App_Settings.spfc.ims[im]
        /// 
        /// </summary>
        /// <param name="im">ims的索引</param>
        /// <param name="savef1">保存f1的数据</param>
        /// <param name="savef2">保存f2的数据</param>
        private void SaveIms(int im, bool savef1, bool savef2)
        {
            int order=3;
            switch( im)
            {
                case 0:
                    order = 3; break;
                case 1:
                    order = 5;break;
                case 2:
                    order = 7;break;
                case 3:
                    order = 9;break;
                default:
                    order = 3;break;
            }
            if (savef1)
            {
                App_Settings.spfc.ims[im].F1UpS = settings.F1s;
                App_Settings.spfc.ims[im].F1UpE = settings.F1e;
                App_Settings.spfc.ims[im].F1Step = settings.Setp1;

                IniFile.SetString("Specifics", "ord" + order.ToString() + "_F1UpS", settings.F1s.ToString(),  "D:\\settings\\Specifics.ini");
                IniFile.SetString("Specifics", "ord" + order.ToString() + "_F1UpE", settings.F1e.ToString(), "D:\\settings\\Specifics.ini");
                IniFile.SetString("Specifics", "ord" + order.ToString() + "_F1Step", settings.Setp1.ToString(),  "D:\\settings\\Specifics.ini");

            }
            if (savef2)
            {
                App_Settings.spfc.ims[im].F2DnS = settings.F2s;
                App_Settings.spfc.ims[im].F2DnE = settings.F2e;
                App_Settings.spfc.ims[im].F2Step = settings.Setp2;

                IniFile.SetString("Specifics", "ord" + order.ToString() + "_F2DnS", settings.F2s.ToString(),  "D:\\settings\\Specifics.ini");
                IniFile.SetString("Specifics", "ord" + order.ToString() + "_F2DnE", settings.F2e.ToString(),  "D:\\settings\\Specifics.ini");
                IniFile.SetString("Specifics", "ord" + order.ToString() + "_F2Step", settings.Setp2.ToString(),  "D:\\settings\\Specifics.ini");
            }
        }

        /// <summary>根据阶数获得IMS的index
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private int ReturnImsIndex(int order)
        {
            int im = 0;
            switch (order)
            {
                case 3:
                    im = 0;
                    break;
                case 5:
                    im = 1;
                    break;
                case 7:
                    im = 2;
                    break;
                case 9:
                    im = 3;
                    break;
            }
            return im;
        }

        /// <summary>根据扫描模式，阶数，来获取客户保存的数据
        /// 
        /// </summary>
        /// <param name="sweeptype">扫描模式</param>
        /// <param name="order">阶数</param>
        /// <param name="returnF1">是读取F1，还是读取F2，true==>F1</param>
        /// <returns></returns>
        private float ReturnFreqValue(int sweeptype, int order, bool returnF1)
        {
            int im = ReturnImsIndex(order);
            float value = 0;
            if (sweeptype == (int)SweepType.Freq_Sweep)//扫频
            {
                if (returnF1)//F1的起始频率
                {
                    value = settings.F1s;
                }
                else
                {
                    value = settings.F2e;
                }
            }
            else//扫时
            {
                if (returnF1)//F1的起始频率
                {
                    value = App_Settings.spfc.ims[im].F1fixed;
                }
                else
                {
                    value = App_Settings.spfc.ims[im].F2fixed;
                }
            }
            return value;
        }

        #endregion

        #region Im order 切换

        private void lblIM_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                int im = (int)settings.PimOrder + 2;
                pimOrder = im;
                ChangeOrder(im);
                pltPim.Clear();
                lbl1.Text = "";
                lbl2.Text = "";
            }
        }

        #region 切换ORDER更改相应设置
        /// <summary>
        /// 切换ORDER更改相应设置
        /// </summary>
        /// <param name="im"></param>
        private void ChangeOrder(int im)
        {
            if (im > 9)
                im = 3;
            switch (im)
            {
                case 3:
                    settings.PimOrder = ImOrder.Im3;
                    settings.F1s = App_Settings.spfc.ims[0].F1UpS;
                    settings.F1e = App_Settings.spfc.ims[0].F1UpE;
                    settings.F2s = App_Settings.spfc.ims[0].F2DnS;
                    settings.F2e = App_Settings.spfc.ims[0].F2DnE;
                    settings.Setp1 = App_Settings.spfc.ims[0].F1Step;
                    settings.Setp2 = App_Settings.spfc.ims[0].F2Step;
                    
                    RxStart = App_Settings.spfc.ims[0].ImS;
                    RxEnd = App_Settings.spfc.ims[0].ImE;
                    //ygq
                    if (App_Configure.Cnfgs.Mode == 2)
                    {
                        if (port1_rev_fwd == 3 || port1_rev_fwd == 4)
                        {
                            RxStart = 1920;
                            RxEnd = 1980;

                        }
                    }

                    
                    //ygq
                    break;
                case 5:
                    settings.PimOrder = ImOrder.Im5;
                    settings.F1s = App_Settings.spfc.ims[1].F1UpS;
                    settings.F1e = App_Settings.spfc.ims[1].F1UpE;
                    settings.F2s = App_Settings.spfc.ims[1].F2DnS;
                    settings.F2e = App_Settings.spfc.ims[1].F2DnE;
                    settings.Setp1 = App_Settings.spfc.ims[1].F1Step;
                    settings.Setp2 = App_Settings.spfc.ims[1].F2Step;
                    RxStart = App_Settings.spfc.ims[1].ImS;
                    RxEnd = App_Settings.spfc.ims[1].ImE;
                    break;
                case 7:
                    settings.PimOrder = ImOrder.Im7;
                    settings.F1s = App_Settings.spfc.ims[2].F1UpS;
                    settings.F1e = App_Settings.spfc.ims[2].F1UpE;
                    settings.F2s = App_Settings.spfc.ims[2].F2DnS;
                    settings.F2e = App_Settings.spfc.ims[2].F2DnE;
                    settings.Setp1 = App_Settings.spfc.ims[2].F1Step;
                    settings.Setp2 = App_Settings.spfc.ims[2].F2Step;
                    RxStart = App_Settings.spfc.ims[2].ImS;
                    RxEnd = App_Settings.spfc.ims[2].ImE;
                    break;
                case 9:
                    settings.PimOrder = ImOrder.Im9;
                    settings.F1s = App_Settings.spfc.ims[3].F1UpS;
                    settings.F1e = App_Settings.spfc.ims[3].F1UpE;
                    settings.F2s = App_Settings.spfc.ims[3].F2DnS;
                    settings.F2e = App_Settings.spfc.ims[3].F2DnE;
                    settings.Setp1 = App_Settings.spfc.ims[3].F1Step;
                    settings.Setp2 = App_Settings.spfc.ims[3].F2Step;
                    RxStart = App_Settings.spfc.ims[3].ImS;
                    RxEnd = App_Settings.spfc.ims[3].ImE;
                    break;
            }




            if (settings.SweepType == SweepType.Freq_Sweep)
            {
                pltPim.SetLineEnalbe(true, RxStart, RxEnd, Color.White);
            }
            else
            {
                pltPim.SetLineEnalbe(false, RxStart, RxEnd, Color.White);
            }
            margin = (float)((RxEnd - RxStart) * 0.1);
            SetPltPimX(im);
            x1 = 0;
            x2 = RxEnd;

            lblIM.Text = "ORDER" + im.ToString();
            ChangeBtnPic(pbxOrder, "order" + im.ToString() + ".jpg");
            dgvPim.Columns[5].HeaderText = "Im" + im.ToString() + "_F(MHz)";
            if (settings.PimUnit == ImUint.dBm)
            {
                ChangeBtnPic(pnlIm, "dbm", im.ToString() + ".png");
                dgvPim.Columns[6].HeaderText = "Im" + im.ToString() + "_V(dBm)";
            }
            else
            {
                ChangeBtnPic(pnlIm, "dbc", im.ToString() + ".png");
                dgvPim.Columns[6].HeaderText = "Im" + im.ToString() + "_V(dBc)";
            }
        }
        #endregion

        #endregion

        #region 根据阶数来设置PltPim的X轴的start,stop,根据阶数来获取互调值
        /// <summary>
        /// 根据阶数来设置PltPim的X轴的start,stop,根据阶数来获取互调值
        /// </summary>
        /// <param name="i"></param>
        private void SetPltPimX(int i)
        {
            if (port1_rev_fwd == 2 || port1_rev_fwd == 3)
            {
                RxStart = 1920;
                RxEnd = 1980;
            }
            bool f = false;
            if (settings.SweepType == SweepType.Time_Sweep)
                f = true;

            float fpimValue = 0;
            if (!f)
                pltPim.SetXStartStop(RxStart - margin, RxEnd + margin);
            switch (i)
            {
                case 3:
                    if (App_Configure.Cnfgs.Mode == 0)
                        fpimValue = 2 * settings.F2e - settings.F1s;
                    else if (App_Configure.Cnfgs.Mode == 1)
                        fpimValue = 2 * settings.F1s - settings.F2e;
                    else
                        fpimValue = 2 * settings.F1s - settings.F2e;
                    break;
                case 5:
                    if (App_Configure.Cnfgs.Mode == 0)
                        fpimValue = 3 * settings.F2e - 2 * settings.F1s;
                    else if (App_Configure.Cnfgs.Mode == 1)
                        fpimValue = 3 * settings.F1s - 2 * settings.F2e;
                    else
                        fpimValue = 3 * settings.F1s - 2 * settings.F2e;
                    break;
                case 7:
                    if (App_Configure.Cnfgs.Mode == 0)
                        fpimValue = 4 * settings.F2e - 3 * settings.F1s;
                    else if (App_Configure.Cnfgs.Mode == 1)
                        fpimValue = 4 * settings.F1s - 3 * settings.F2e;
                    else
                        fpimValue = 4 * settings.F1s - 3 * settings.F2e;
                    break;
                case 9:
                    if (App_Configure.Cnfgs.Mode == 0)
                        fpimValue = 5 * settings.F2e - 4 * settings.F1s;
                    else if (App_Configure.Cnfgs.Mode == 1)
                        fpimValue = 5 * settings.F1s - 4 * settings.F2e;
                    else
                        fpimValue = 5 * settings.F1s - 4 * settings.F2e;
                    break;
            }
            pimTxt.Text = fpimValue.ToString("0.0");
        }
        #endregion

        #region Limit比较,显示PASS、FAIL

        float max = float.MinValue;
        float min = float.MaxValue;
        float maxdbm = 0;

        private void timerLimit_Tick(object sender, EventArgs e)
        {
            limitValue = float.Parse(txtBox.Text);
            Comparison(ref max, ref min);
            ApiMaxVlaue = max.ToString();
            if (limitValue > max)
            {
                lblPf.ForeColor = Color.Lime;
                lblPf.Text = "PASS";
            }
            else
            {
                lblPf.ForeColor = Color.Red;
                lblPf.Text = "FAIL";
            }

            lblPf.Visible = true;
        }
        #endregion

        #region 在已经有的序列里面PEAK最大值和最小值
        /// <summary>
        /// 在已经有的序列里面PEAK最大值和最小值
        /// </summary>
        /// <param name="maxValue"></param>
        /// <param name="minValue"></param>
        /// <returns></returns>
        private bool Comparison(ref float maxValue, ref float minValue)
        {
            bool flag = false;
            string Unit = "dBm";
            float Total = 0;
            int num = 0;
            if (settings.PimUnit == ImUint.dBc)
            {
                flag = true;
                Unit = "dBc";

                for (int i = 0; i < dtDbmC.Rows.Count; i++)
                {
                    if (Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]) != wRvalue)
                    {
                        Total = Total + Convert.ToSingle(dtDbmC.Rows[i]["Im_V(dBm)"]);
                        num = num + 1;
                        if (Convert.ToSingle(dtDbmC.Rows[i]["Im_V(dBm)"]) > maxValue)
                        {
                            maxValue = Convert.ToSingle(dtDbmC.Rows[i]["Im_V(dBm)"]);
                            maxdbm = maxValue + settings.Tx;
                        }
                        if (Convert.ToSingle(dtDbmC.Rows[i]["Im_V(dBm)"]) < minValue)
                        {
                            minValue = Convert.ToSingle(dtDbmC.Rows[i]["Im_V(dBm)"]);
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < dtDbmM.Rows.Count; i++)
                {
                    if (Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]) != wRvalue)
                    {
                        Total = Total + Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]);
                        num = num + 1;
                        if (Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]) > maxValue)
                        {
                            maxValue = Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]);
                            maxdbm = maxValue;
                        }
                        if (Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]) < minValue)
                        {
                            minValue = Convert.ToSingle(dtDbmM.Rows[i]["Im_V(dBm)"]);
                        }
                    }
                }

            }
            if (dtDbmM.Rows.Count > 0 && dtDbmC.Rows.Count > 0)
            {
                this.lblMaxPim.Text = "Peak=" + maxValue.ToString() + Unit
                                       + "   Max="
                                       + maxValue.ToString() + Unit + "   Min="
                                       + minValue.ToString() + Unit + "   Avg="
                                       + (Total / num).ToString() + Unit;
            }
            return flag;
        }
        #endregion

        #region 滚动条相应设置

        #region CustomScrollbar
        private void ctsBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                int int_Hegiht = dgvPim.Size.Height / 7;
                int index = 7;
                index = ctsBar.Value / int_Hegiht;
                if (index + 7 + 1 == dgvPim.Rows.Count)
                {
                    index = index + 1;
                }
                dgvPim.FirstDisplayedScrollingRowIndex = index;  //设置第一行显示 
            }
            catch { }

        }
        #endregion

        #region dgvPim_Scroll
        private void dgvPim_Scroll(object sender, ScrollEventArgs e)
        {
            int intHeight = dgvPim.Size.Height / 7;
            ctsBar.Value = dgvPim.FirstDisplayedScrollingRowIndex * intHeight;
        }
        #endregion

        #region 自定义滚动条
        /// <summary>
        /// 自定义滚动条大小
        /// </summary>
        private void CreatScrollbar()
        {
            if (dgvPim.Rows.Count <= 7)
            {
                ctsBar.Visible = false;
            }
            else
            {
                ctsBar.Visible = true;
            }
            ShowScrollbarValue();//显示滚动条位置
        }

        /// <summary>
        /// 显示滚动条
        /// </summary>
        private void ShowScrollbarValue()
        {
            this.ctsBar.Minimum = 0;
            this.ctsBar.LargeChange = ctsBar.Maximum / ctsBar.Height + dgvPim.Size.Height;
            this.ctsBar.SmallChange = 15;
            int a = dgvPim.Size.Height / 7 * dgvPim.Rows.Count;
            this.ctsBar.Maximum = a;
            int intHeight = dgvPim.Size.Height / 7;
            ctsBar.Value = dgvPim.FirstDisplayedScrollingRowIndex * intHeight;
        }
        #endregion
        //bool zoomFlag = false;yuan
        bool zoomFlag = false;
        private void pbxL_MouseClick(object sender, MouseEventArgs e)
        {
            ZoomInOut();
        }
        private void ZoomInOut()
        {
            if (!Sweeping && !isRead && settings.SweepType != SweepType.Time_Sweep)
            {
                if (!zoomFlag)
                {
                    if (dtDbmC.Rows.Count > 0)
                    {
                        Large();
                        pltPim.SetXStartStop(x2, x1);
                        x1 = 0;
                        x2 = RxEnd;
                        zoomFlag = true;
                        ChangeBtnPic(pbxL, "zoomout.bmp");
                    }
                }
                else
                {
                    pltPim.SetXStartStop(RxStart - margin, RxEnd + margin);
                    zoomFlag = false;
                    ChangeBtnPic(pbxL, "zoomin.bmp");
                }
            }

        }
        #endregion

        #region 放大扫描图象操作
        private void Large()
        {
            for (int i = 0; i < dtDbmC.Rows.Count; i++)
            {
                if (Convert.ToSingle(dtDbmC.Rows[i]["Im_F(MHz)"]) > x1)
                {
                    x1 = Convert.ToSingle(dtDbmC.Rows[i]["Im_F(MHz)"]);
                }
                if (Convert.ToSingle(dtDbmC.Rows[i]["Im_F(MHz)"]) < x2)
                {
                    x2 = Convert.ToSingle(dtDbmC.Rows[i]["Im_F(MHz)"]);
                }
                if (x1 > RxEnd)
                    x1 = RxEnd;
                if (x2 < RxStart)
                    x2 = RxStart;
            }
        }
        #endregion

        #region 开启滚动
        /// <summary>
        /// 开启滚动
        /// </summary>
        /// <param name="f"></param>
        private void ScrollEnable(bool f1, bool f2, bool f3)
        {
            pltPim.SetXAutoScroll(f1);
            pltPim.SetScrollAvailable(f2);
            pltPim.SetSampling(true);
            pltPim.SetStop(f3);
        }

        #endregion

        #region 用于空格启动
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space)
            {
                StartClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion

        private void dgvPim_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            DataGridViewRow dgr = dgvPim.Rows[e.RowIndex];   //获得DataGridViewRow     
            if (dgr.Cells[6].Value.ToString().Equals("-200.0"))   //如果状态为开启,条件可以自定义     
            {
                dgr.DefaultCellStyle.ForeColor = Color.Red;   //设置行背景色为浅绿色     
            }
        }

        #region ================================= Add by NQ =================================


        public void ChangePic()
        {
            if (App_Configure.Cnfgs.Ms_switch_port_count <= 0)
            {
                if (App_Configure.Cnfgs.Channel == 0)
                {
                    ChangeBtnPic(pbxFwd, "fwd_in.gif");
                    ChangeBtnPic(pbxRev, "rev.gif");
                }
                else
                {
                    ChangeBtnPic(pbxRev, "rev_in.gif");
                    ChangeBtnPic(pbxFwd, "fwd.gif");
                }
            }
            else
            {
                if (App_Configure.Cnfgs.Channel == 0)
                {
                    ChangeBtnPic(pbxFwd, "rev2_in.jpg");
                    ChangeBtnPic(pbxRev, "rev.gif");
                }
                else
                {
                    ChangeBtnPic(pbxRev, "rev_in.gif");
                    ChangeBtnPic(pbxFwd, "rev2.jpg");
                }
            }
        }

        private void ChangeRevFwd()
        {
            if (App_Configure.Cnfgs.Channel == 0)
            {
                if (GPIO.Rev())
                {
                    settings.PimSchema = ImSchema.REV;
                }
            }
            else
            {
                if (GPIO.Fwd())
                {
                    settings.PimSchema = ImSchema.FWD;
                }
            }
        }

        #endregion



        /////////////////////////////////by yuguoquan/////////////////////////////////////////
        //API///

        public string ChangePort(string num)
        {
           string mes="";
            try
            {

                this.Invoke(new ThreadStart(delegate()
                {
                    int count = int.Parse(num) + 1;
                    if (count >= App_Configure.Cnfgs.Ms_switch_port_count || count <= 0)
                        mes = "0,change port fail\r\n";
                    //Setport_method(int.Parse(num));
                    if (current_port != count)
                    {

                        CloseButton(current_port);
                        current_port = count;
                        saveport = count;
                        ClickPortMethodN();

                        if (isReadCsv)
                            ReadAllCsv(current_port);
                    }
                    //
                    mes = "1,success\r\n";
                }));
            }
            catch
            {
                mes= "0,change port fail\r\n";
            }
            return mes;
        }

        public string SetJPG(string name)
        {
            if (SaveJpg(Application.StartupPath + "\\jpg\\" + name + ".jpg"))
                return  "1,success\r\n";
            else
                return  "0,Saving the JPG file failed!\r\n";
        }
        public void PIMstop()
        {
            if (isRead)
            {
                sweepEnd = false;
            }
            else
            {
                sweepEnd = true;
                PimBreakSweep(1000);
            }
            Timer.Stop();
            
            isRead = false;
            ScrollEnable(false, true, true);
            
        }
        public void PIMstart(string add)
        {
            if (Sweeping)
            {
                sa.OnSend("PIM BUSY\r\n", add);
                return;
            }
            this.Invoke(new ThreadStart(delegate()
                {
                    //StartClick();//开始扫描
                    if (!Sweeping && !isRead)
                    {
                        sweepEnd = false;
                        Sweeping = true;
                        PbxStart();
                    }
                }));
            server_sweepNow = 0;
            if(!sa.OnSend("PIM START\r\n", add, new AsyncCallback(SendCallback)))
                return;
        }
        public string setOrder(int order)
        {
            settings.PimOrder = (ImOrder)order;
            ChangeOrder((int)settings.PimOrder);
            string str = "1,success\r\n";
            return str;
        }
        private  string setSweep(SweepType isFreqSweep)
        {
            if (!Sweeping && !isRead)
            {
                if (scanType)
                    lblMaxPim.Visible = true;
                else
                    lblMaxPim.Visible = true;
                if (isFreqSweep == SweepType.Time_Sweep)
                {
                    ScrollEnable(true, false, false);
                    ChangeBtnPic(pblFt, "other", "time.png");
                    ft = true;
                    settings.SweepType = SweepType.Time_Sweep;
                    this.pltPim.SetXStartStop(0, x_stop);
                    if (isFlagDbc)
                    {
                        lbl1.Visible = false;
                        lbl2.Visible = true;
                    }
                    else
                    {
                        lbl2.Visible = false;
                        lbl1.Visible = true;
                    }
                    pnlFpim.Visible = true;
                }
                else
                {
                    ScrollEnable(false, false, false);
                    ChangeBtnPic(pblFt, "other", "frequency.png");
                    ft = false;
                    settings.SweepType = SweepType.Freq_Sweep;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                    pnlFpim.Visible = false;
                }
                ChangeOrder((int)settings.PimOrder);
            }
            string str = "1,success\r\n";
            return str;
        }

        public string setType(bool isFWD)
        {
            if (isFWD)
            {
                settings.PimSchema = ImSchema.FWD;
                GPIO.Fwd();
                ChangeBtnPic(pbxRev, "rev_in.gif");
                ChangeBtnPic(pbxFwd, "fwd.gif");
            }
            else
            {
                settings.PimSchema = ImSchema.REV;
                GPIO.Rev();
                ChangeBtnPic(pbxFwd, "fwd_in.gif");
                ChangeBtnPic(pbxRev, "rev.gif");
            }

            return "1,success\r\n";

        }

        public string setLimit(string value)
        {
          
            float num = float.Parse(value);
            txtBox.Text = num.ToString();
            if (isFlagDbc)
            {
                settings.Limit_Pim = num + settings.Tx;
            }
            else
            {
                settings.Limit_Pim = num;
            }
            pltPim.SetLimitEnalbe(true, num, Color.FromArgb(160, 245, 255));
            pltPim.MajorLineWidth = pltPim.MajorLineWidth;//只是用于重绘控件
           

            return "1,success\r\n";
        }
        public string setUnit(bool isdbc)
        {

            max = float.MinValue;
            min = float.MaxValue;
            if (!isdbc)
            {
                if (settings.SweepType == SweepType.Freq_Sweep)
                {
                    s[0] = -1;
                    s[1] = -1;
                    s[2] = 2;
                    s[3] = 3;
                    s[4] = -1;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                }
                else
                {
                    s[0] = -1;
                    s[1] = -1;
                    s[2] = 2;
                    s[3] = -1;
                    s[4] = -1;
                    lbl1.Visible = false;
                    lbl2.Visible = true;
                }
                pltPim.SetMarkVisible(0, false);
                pltPim.SetMarkVisible(1, true);
                pltPim.SetMarkSequence(1, s);
                pltPim.Peak();
                ChangeDbc();
            }
          
            return "1,success\r\n";
        }
        public string setUnit2(bool isdbc)
        {

            max = float.MinValue;
            min = float.MaxValue;
           if(isdbc)
            {
                if (settings.SweepType == SweepType.Freq_Sweep)
                {
                    s[0] = 0;
                    s[1] = 1;
                    s[2] = -1;
                    s[3] = -1;
                    s[4] = -1;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                }
                else
                {
                    s[0] = 0;
                    s[1] = -1;
                    s[2] = -1;
                    s[3] = -1;
                    s[4] = -1;
                    lbl2.Visible = false;
                    lbl1.Visible = true;
                }
                pltPim.SetMarkVisible(1, false);
                pltPim.SetMarkVisible(0, true);
                pltPim.SetMarkSequence(0, s);
                pltPim.Peak();
                ChangeDbmm();
            }
            return "1,success\r\n";
        }

        public string setPower(float pow)
        {
            string str = "";
            if (pow < App_Settings.sgn_1.Min_Power || pow > App_Settings.sgn_1.Max_Power)
            {
                str = "0,ERROR FOR TX OUT OF RANGE\r\n";
            }
            else
            {             
                settings.Tx = pow;
                settings.Tx2 = pow;
                str = "1,success\r\n";
            }
            return str;
        }

        public string setStep1(float step)
        {
            string str = "";

            settings.Setp1 = step;
            str = "1,success\r\n";
            return str;
        }
        public string setStep2(float step)
        {
            string str = "";

            settings.Setp2 = step;
            str = "1,success\r\n";
            return str;
        }

        public string setTimes(float time)
        {
            string str = "";
            settings.TimeSweepPoints = (int)(time * 60);
            str = "1,success\r\n";
            return str;
        }
        public string setF1s(float f1)
        {
            settings.F1s = f1;
            return "1,success\r\n";
        }
        public string setF1e(float f1)
        {
            settings.F1e = f1;
            return "1,success\r\n";
        }
        public string setF2s(float f2)
        {
            settings.F2s = f2;
            return "1,success\r\n";
        }
        public string setF2e(float f2)
        {
            settings.F2e = f2;
            return "1,success\r\n";
        }
        public string Save()
        {
            SaveSettings((int)settings.PimOrder, (int)settings.SweepType, true, true);
            float v = ChangePimTxt(settings.F1s, settings.F2e, (int)settings.PimOrder);
            pimTxt.Text = v.ToString("0.0");
            lblN1.Text = "=" + settings.Tx.ToString() + "dBm";
            lblN2.Text = "=" + settings.Tx.ToString() + "dBm";

            return "1,success\r\n";
        }

        public string GetMax()
        {
            if (ApiMaxVlaue == "")
                return "0,ERROR FOR MAXVALUE\r\n";
            else
                return ApiMaxVlaue;
        }

        public string GetPort()
        {
     
            string value = App_Configure.Cnfgs.Ms_switch_port_count.ToString() + ",0";
            return value;
        }
        //
        //回调

        public bool checkouValue(float value, float max, float min)
        {
            if (value > max || value < min)
                return false;
            else
                return true;
        }

        List<string> recMessage = new List<string>();

        string GetName_String(string recmes,ref  string value)
        {
           
                string[] strTemp = recmes.Split(' ');
                if (strTemp.Length >= 2)
                    value = strTemp[1];
                return strTemp[0];
                     
        }

        //=============ygq
        int selectport = 0;
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            //if (!Sweeping && !isRead)
            //{
            //    Port pt = new Port(selectport);
            //    DialogResult dr = pt.ShowDialog();
            //      if (dr == DialogResult.OK)
            //      {
            //          selectport = pt.selectindex;
            //          Changecic(pt.selectindex);             
            //          ChangePort(pt.selectindex);
            //          label1.Text = "PORT" + (pt.selectindex+1).ToString();
            //      }
            //}         
        }
        public void  ChangePortOffset(int  num)
        {
                Program.offset.GetTX(num);
                Program.offset.GetRX(num);
        }
        private void  ChangePort(int num)
        {
            bool result = MsSwithc.CheckPort(num);
            if (!result)
                MessageBox.Show("# " + selectport.ToString() + " failed!"); 
        }
        private void ChangePortN(int num)
        {
            Program.mSwitch.SW_Work_Switch_Write((byte)num, false);
        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                Port pt = new Port(selectport);
                DialogResult dr = pt.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    Setport_method(pt.selectindex);
                }
            }
        }
        private void Setport_method(int num)
        {

                selectport = num;
                if (App_Configure.Cnfgs.SwtichOrGpio)
                    ChangePort(num);
                //ChangePort(num);
                ChangePortOffset(num);

        }
        private void Setport_methodN(int num)
        {
            if (num == 0) return;
            selectport = num;
            ChangePortOffset(num-1);
            //label1.Text = "  PORT" + (num + 1).ToString();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {//rev2 第3个端口
            if (!Sweeping && !isRead)
            {
                if (settings.PimSchema != ImSchema.REV || port1_rev_fwd != 3)
                {

                    settings.PimSchema = ImSchema.REV;
                    GPIO.Rev(2);
                    ChangeBtnPic(pictureBox2, "fwd2_in.jpg");
                    ChangeBtnPic(pictureBox1, "rev2.jpg");

                    ChangeBtnPic(pbxRev, "rev1_in.jpg");
                    ChangeBtnPic(pbxFwd, "fwd1_in.jpg");
                    port1_rev_fwd = 3;
                    SetPltPimX((int)settings.PimOrder);
                }
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //if (settings.PimSchema != ImSchema.REV)
            //    ChangeBtnPic(pbxRev, "rev_in.gif");
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            //ChangeBtnPic(pbxRev, "rev.gif");
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {//rwd2  第4个端口
            if (!Sweeping && !isRead)
            {
                if (settings.PimSchema != ImSchema.FWD || port1_rev_fwd != 4)
                {

                    settings.PimSchema = ImSchema.FWD;
                    GPIO.Fwd(2);
                    ChangeBtnPic(pictureBox1, "rev2_in.jpg");
                    ChangeBtnPic(pictureBox2, "fwd2.jpg");

                    ChangeBtnPic(pbxRev, "rev1_in.jpg");
                    ChangeBtnPic(pbxFwd, "fwd1_in.jpg");
                    port1_rev_fwd = 4;
                    SetPltPimX((int)settings.PimOrder);
                }
            }
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            //if (settings.PimSchema != ImSchema.FWD)
            //    ChangeBtnPic(pbxFwd, "fwd_in.gif");
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            //ChangeBtnPic(pbxFwd, "fwd.gif");
        }

        private void pbxAllStart_MouseMove(object sender, MouseEventArgs e)
        {
           if (!Sweeping && !isRead)
                ChangeBtnPic(pbxAllStart, "allstart.jpg");
        }

        private void pbxAllStart_MouseLeave(object sender, EventArgs e)
        {
            if (!Sweeping && !isRead)
                ChangeBtnPic(pbxAllStart, "uallstart.jpg");
        }

        private void pbxAllStart_Click(object sender, EventArgs e)
        {
            pbxStart.Enabled = false;

            Thread th = new Thread(Allstart);
            th.IsBackground = true;
            th.Start();
        }

        void Allstart()
        {
            try
            {

                if (!Sweeping && !isRead)
                {
                    PimAll();

                }
                else
                {
                    if (isRead)
                    {
                        sweepEnd = false;
                    }
                    else
                    {
                        sweepEnd = true;
                        PimBreakSweep(1000);
                    }
                    Timer.Stop();

                    isRead = false;
                    ScrollEnable(false, true, true);


                }
                this.Invoke(new ThreadStart(delegate()
                {
                    ChangeBtnPic(pbxAllStart, "uallstart.jpg");
                    ChangeBtnPic((PictureBox)control_port[saveport - 1], "port" + saveport.ToString() + "_in.jpg");
                    //current_port = 0;
                    pbxStart.Enabled = true;
                }));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 根据num 来执行端口按钮方法
        /// </summary>
        /// <param name="num"></param>
        private void SwitchPort(int num)
        {
            switch (num)
            {
                case 1:
                    pbxPort1_MouseClick(null, null);
                    break;
                case 2:
                    pbxPort2_MouseClick(null, null);
                    break;
                case 3:
                    pbxPort3_MouseClick(null, null);
                    break;
                case 4:
                    pbxPort4_MouseClick(null, null);
                    break;
                case 5:
                    pictureBox5_MouseClick(null, null);
                    break;
                case 6:
                    pictureBox4_MouseClick(null, null);
                    break;
                default:
                   
                    break;
            }
        }
        void PimAll()
        {
            for (int i = 0; i < App_Configure.Cnfgs.Ms_switch_port_count; i++)
            {
                cpd.ClearPortDatatable(i);//清空前次测试数据
            }

            for (int i = 0; i < CurrentSelectPort.Count; i++)//所选端口
            {

                this.Invoke(new ThreadStart(delegate()
                {
                    SwitchPort(CurrentSelectPort[i]);
                    sweepEnd = false;
                    Sweeping = true;
                    PbxStart();
                }));
                while (true)//等待当前端口测试才继续下一个端口
                {
                    if (SweepObj.errmessage.Count > 0|| PimBreakSweep())//判断是否有错误信息，有则停止测试
                    {
                        return;
                    }
                    if (!Sweeping && !isRead && server_sweepEnd)//判断当前端口是否结束
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(200);//等待
                    }
                }
            }

        }

        /// <summary>
        /// 根据保存的扫描数据来更新相应端口的界面数据
        /// </summary>
        /// <param name="num"></param>
        void UpdatePortData(int num)
        {
            if (!isReadCsv)
            {
                if (isFlagDbc)
                {
                    dgvPim.DataSource = cpd.pe[num].dt;//绑定表格数据源
                }
                else
                {
                    dgvPim.DataSource = cpd.pe[num].dtm;
                }
                dtDbmC = cpd.pe[num].dt.Copy();//深拷贝datatable数据
                dtDbmM = cpd.pe[num].dtm.Copy();
                dtWdbm = cpd.pe[num].wdtm.Copy();
                dtWdbc = cpd.pe[num].wdt.Copy();
                pltPim.Clear();
                if (cpd.pe[num].dt.Rows.Count > 0)//判断是否有数据和判断是否切换过阶数
                {
                    PointfAdd(cpd.pe[num].point, cpd.pe[num].point2,
                            cpd.pe[num].point3, cpd.pe[num].point4, cpd.pe[num].temp.Length);
                    max = float.MinValue;//只有重新设置max和min，才能在切换端口的时候来更新peak
                    min = float.MaxValue;
                    pltPim.Peak();
                }
            }
        }
        /// <summary>
        /// 根据端口数据，来重绘坐标数据
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="p4"></param>
        private void PointfAdd(PointF[] p1, PointF[] p2, PointF[] p3, PointF[] p4,int leng)
        {


            if (settings.SweepType == SweepType.Freq_Sweep)
            {
                pltPim.Add(p1, 0, 0);
                pltPim.Add(p3, 2, 0);

                if (leng > fsp.Items1.Length)
                {
                    pltPim.Add(p4, 3, 0);
                    pltPim.Add(p2, 1, 0);
                }
            }
            else
            {
                pltPim.Add(p2, 0, 0);
                pltPim.Add(p4, 2, 0);
                lbl2.Text = p4[p4.Length - 1].Y.ToString("0.0") + " dBc";
                lbl1.Text = p2[p2.Length - 1].Y.ToString("0.0") + " dBm";
            }

        }

        private void pbxOrder_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                int im = (int)settings.PimOrder + 2;
                ChangeOrder(im);
                pltPim.Clear();
                lbl1.Text = "";
                lbl2.Text = "";
                drawplitm = false;

            }
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping && !isRead)
            {
                if (scanType)
                    lblMaxPim.Visible = true;
                else
                    lblMaxPim.Visible = true;
                if (!ft)
                {
                    ScrollEnable(true, false, false);
                    ChangeBtnPic(pictureBox6, "timetofreq.gif");
                    ft = true;
                    settings.SweepType = SweepType.Time_Sweep;
                    this.pltPim.SetXStartStop(0, 15);
                    if (isFlagDbc)
                    {
                        lbl1.Visible = false;
                        lbl2.Visible = true;
                    }
                    else
                    {
                        lbl2.Visible = false;
                        lbl1.Visible = true;
                    }
                    pnlFpim.Visible = true;

                }
                else
                {
                    ScrollEnable(false, false, false);
                    ChangeBtnPic(pictureBox6, "freqtotime.gif");
                    ft = false;
                    settings.SweepType = SweepType.Freq_Sweep;
                    lbl1.Visible = false;
                    lbl2.Visible = false;
                    pnlFpim.Visible = false;
                }
                ChangeOrder((int)settings.PimOrder);
            }
        }

        private void pbxPort1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPortH(1)&&isReadCsv) return;
            //ClearS();
        
            if (!Sweeping && !isRead)
            {
                    if (current_port != 1)
                    {
                      
                        CloseButton(current_port);
                        current_port = 1;
                        saveport = 1;
                        ClickPortMethodN();
                       
                        if (isReadCsv)
                            ReadAllCsv(current_port);
                        //ChangeBtnPic(pbxPort1, "port1.jpg");             
                    }
            }
        }

        bool IsPortH(int item)
        { 
          
            return allCsvPort.Contains(item);
        }
       
        

        /// <summary>
        /// 切换开关按钮图片
        /// </summary>
        /// <param name="num"></param>
        private void CloseButton(int num)
        {
            switch (num)
            {
                case 1: ChangeBtnPic(pbxPort1, "port1_in.jpg");
                    break;
                case 2: ChangeBtnPic(pbxPort2, "port2_in.jpg");
                    break;
                case 3: ChangeBtnPic(pbxPort3, "port3_in.jpg"); ;
                    break;
                case 4: ChangeBtnPic(pbxPort4, "port4_in.jpg");
                    break;
                case 5: ChangeBtnPic(pictureBox5, "port5_in.jpg");
                    break;
                case 6: ChangeBtnPic(pictureBox4, "port6_in.jpg");
                    break;

            }

        }

        void ClickPortMethod()
        {
            Setport_method(current_port);
            UpdatePortData(current_port);
            ChangeBtnPic((PictureBox)control_port[current_port], "port" + (current_port + 1).ToString() + ".jpg");
        }
        void ClickPortMethodN()
        {
            Setport_methodN(current_port);
            UpdatePortData(current_port-1);
            ChangeBtnPic((PictureBox)control_port[current_port-1], "port" + (current_port ).ToString() + ".jpg");
           
        }

        private void pbxPort2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPortH(2) && isReadCsv) return;
       
            if (!Sweeping && !isRead)
            {
               
                if (current_port != 2)
                {
                    //ClearS();
                   
                    CloseButton(current_port);
                    current_port = 2;
                    saveport = 2;
                    ClickPortMethodN();
                    if (isReadCsv)
                        ReadAllCsv(current_port);
                    //ChangeBtnPic(pbxPort1, "port1.jpg");             
                }
            }
        }

        private void pbxPort3_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPortH(3) && isReadCsv) return;
            if (!Sweeping && !isRead)
            {
              
                if (current_port != 3)
                {
                  
                    //ClearS();
                    CloseButton(current_port);
                    current_port = 3;
                    saveport = 3;
                    ClickPortMethodN();
                    if (isReadCsv)
                        ReadAllCsv(current_port);
                    //ChangeBtnPic(pbxPort1, "port1.jpg");             
                }
            }
        }

        private void pbxPort4_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPortH(4) && isReadCsv) return;
            if (!Sweeping && !isRead)
            {
              
                if (current_port != 4)
                {
                  
                    //ClearS();
                    CloseButton(current_port);
                    current_port = 4;
                    saveport = 4;
                    ClickPortMethodN();
                    if (isReadCsv)
                        ReadAllCsv(current_port);
                    //ChangeBtnPic(pbxPort1, "port1.jpg");             
                }
            }
        }

        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPortH(5) && isReadCsv) return;
            if (!Sweeping && !isRead)
            {
              
                if (current_port != 5)
                {
                 
                    //ClearS();
                    CloseButton(current_port);
                    current_port = 5;
                    saveport = 5;
                    ClickPortMethodN();
                    if (isReadCsv)
                        ReadAllCsv(current_port);
                    //ChangeBtnPic(pbxPort1, "port1.jpg");             
                }
            }
        }

        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsPortH(6) && isReadCsv) return;
            if (!Sweeping && !isRead)
            {
               
                if (current_port != 6)
                {
                 
                    //ClearS();
                    CloseButton(current_port);
                    current_port = 6;
                    saveport = 6;
                    ClickPortMethodN();
                    if (isReadCsv)
                        ReadAllCsv(current_port);
                    //ChangeBtnPic(pbxPort1, "port1.jpg");             
                }
            }
        }

        private void pbxPort1_MouseLeave(object sender, EventArgs e)
        {
            if (!IsPortH(1) && isReadCsv) return;
            if (current_port != 1)
                ChangeBtnPic(pbxPort1, "port1_in.jpg");
        }

        private void pbxPort1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPortH(1) && isReadCsv) return;
            if (current_port != 1)
                ChangeBtnPic(pbxPort1, "port1.jpg");
        }

        private void pbxPort2_MouseLeave(object sender, EventArgs e)
        {
            if (!IsPortH(2) && isReadCsv) return;
            if (current_port != 2)
                ChangeBtnPic(pbxPort2, "port2_in.jpg");
        }

        private void pbxPort2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPortH(2) && isReadCsv) return;
            if (current_port != 2)
                ChangeBtnPic(pbxPort2, "port2.jpg");
        }

        private void pbxPort3_MouseLeave(object sender, EventArgs e)
        {
            if (!IsPortH(3) && isReadCsv) return;
            if (current_port != 3)
                ChangeBtnPic(pbxPort3, "port3_in.jpg");
        }

        private void pbxPort3_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPortH(3) && isReadCsv) return;
            if (current_port != 3)
                ChangeBtnPic(pbxPort3, "port3.jpg");
        }

        private void pbxPort4_MouseLeave(object sender, EventArgs e)
        {
            if (!IsPortH(4) && isReadCsv) return;
            if (current_port != 4)
                ChangeBtnPic(pbxPort4, "port4_in.jpg");
        }

        private void pbxPort4_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPortH(4) && isReadCsv) return;
            if (current_port != 4)
                ChangeBtnPic(pbxPort4, "port4.jpg");
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            if (!IsPortH(5) && isReadCsv) return;
            if (current_port != 5)
                ChangeBtnPic(pictureBox5, "port5_in.jpg");
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPortH(5) && isReadCsv) return;
            if (current_port != 5)
                ChangeBtnPic(pictureBox5, "port5.jpg");
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            if (!IsPortH(6) && isReadCsv) return;
            if (current_port != 6)
                ChangeBtnPic(pictureBox4, "port6_in.jpg");
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            if (!IsPortH(6) && isReadCsv) return;
            if (current_port != 6)
                ChangeBtnPic(pictureBox4, "port6.jpg");
        }

        //==========ygq

        //其他
        /////////////////////////////////by yuguoquan/////////////////////////////////////////
    }

}