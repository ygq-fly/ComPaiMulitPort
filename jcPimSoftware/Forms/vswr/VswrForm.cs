// ===============================================================================
// 文件名：VswrForm
// 创建人：倪骞
// 日  期：2011-4-29
//
// 描  述：驻波分析模块
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-4-29   	   创建该文件
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using jcXY2dPlotEx;
using System.IO;



namespace jcPimSoftware
{
    public partial class VswrForm : Form, ISweep
    {
        #region 变量定义

        /// <summary>
        /// 驻波配置对象
        /// </summary>
        private Settings_Vsw settings = new Settings_Vsw("");

        /// <summary>
        /// 扫描参数对象
        /// </summary>
        private SweepParams sp;

        /// <summary>
        /// 驻波分析对象
        /// </summary>
        private Vsw_Sweep SweepObj = new Vsw_Sweep();

        /// <summary>
        /// 图片资源文件夹名称
        /// </summary>
        private readonly string PicfolderName = "vswr";

        /// <summary>
        /// VSWR分析标识
        /// </summary>
        private bool bIsAnalysis = false;

        /// <summary>
        /// MARK标识
        /// </summary>
        private bool bMarkEnable = false;

        /// <summary>
        /// AUTO SCALE标识
        /// </summary>
        private bool bAutoScaleEnable = false;

        /// <summary>
        /// 功放1扫频标识
        /// </summary>
        private bool bCarrier1Enable = false;

        /// <summary>
        /// 功放2扫频标识
        /// </summary>
        private bool bCarrier2Enable = false;

        /// <summary>
        /// 功放扫时标识
        /// </summary>
        private bool bFreqEnable = false;

        /// <summary>
        /// 回放标识
        /// </summary>
        private bool bReadEnable = false;

        /// <summary>
        /// 校准标志位
        /// </summary>
        private bool bIsCAL = false;

        /// <summary>
        /// 当前活动的功放
        /// </summary>
        private RFInvolved CurrentRFInvolved = RFInvolved.Rf_1;

        /// <summary>
        /// 展现形式，0回波损耗 1驻波比
        /// </summary>
        private bool ShowMode = false;

        /// <summary>
        /// RL最大值
        /// </summary>
        private float RL_Max = float.MinValue;

        /// <summary>
        /// RL最小值
        /// </summary>
        private float RL_Min = float.MaxValue;

        /// <summary>
        /// 驻波最大值
        /// </summary>
        private float VSWR_Max = float.MinValue;

        /// <summary>
        /// 驻波最小值
        /// </summary>
        private float VSWR_Min = float.MaxValue;

        /// <summary>
        /// 保存扫描数据的容器
        /// </summary>
        private PointF[][] SaveDataObj;

        /// <summary>
        /// 驻波自动校准对象结构
        /// </summary>
        public struct CalibrationObj
        {
            /// <summary>
            /// 校准频点
            /// </summary>
            public float Freq;
            /// <summary>
            /// 标准件RL
            /// </summary>
            public float RL0;
            /// <summary>
            /// 输出功率
            /// </summary>
            public float Tx0;
            /// <summary>
            /// 接收功率
            /// </summary>
            public float Rx0;
        }
        /// <summary>
        /// 功放一驻波校准对象集合
        /// </summary>
        private List<CalibrationObj> listCurrentCAL_1 = new List<CalibrationObj>();
        /// <summary>
        /// 功放二驻波校准对象集合
        /// </summary>
        private List<CalibrationObj> listCurrentCAL_2 = new List<CalibrationObj>();

        /// <summary>
        /// 扫描历史数据
        /// </summary>
        private struct HistoryData
        {
            /// <summary>
            /// 即时功率
            /// </summary>
            public float p;
            /// <summary>
            /// 中心频率
            /// </summary>
            public float f;
            /// <summary>
            /// Rx功率值
            /// </summary>
            public float rx;
            /// <summary>
            /// RL
            /// </summary>
            public float rl;
            /// <summary>
            /// VSWR
            /// </summary>
            public float vswr;
            /// <summary>
            /// 平均底噪
            /// </summary>
            public float noise;
        }

        /// <summary>
        /// 存放历史数据的容器
        /// </summary>
        private List<HistoryData> HistoryDatalist = new List<HistoryData>();

        /// <summary>
        /// 回放数据配置
        /// </summary>
        private CsvReport_PIVH_Header head;

        /// <summary>
        /// 回放数据集合
        /// </summary>
        private  List<CsvReport_IVH_Entry> listEntry;

        #region 设备状态对象与扫描结构对象，由sweepobj.CloneReference赋值
        private PowerStatus ps1;
        private PowerStatus ps2;
        private SweepResult sr;
        private RFErrors rfr_errors1;
        private RFErrors rfr_errors2;
        #endregion

        #endregion


        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        public VswrForm()
        {
            InitializeComponent();

            pltVswr.Resume();

            this.TopLevel = false;
            this.ShowInTaskbar = false;
            this.Dock = DockStyle.Fill;
        }

        #endregion


        #region 窗体事件

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VswrForm_Load(object sender, EventArgs e)
        {
            //加载配置
            LoadConfig();

            //加载校准文件
            LoadCALconfig();

            //注册委托,事件
            pltVswr.SetMarkText(VswrMarkText);
            pltVswr.SetSave2File(SaveChannel_0);

            //重绘
            ReDraw();
        }

        #endregion

        #region 委托方法

        public string VswrMarkText(MarkInfo[] mi)
        {
            string label = "";

            for (int i = 0; i < mi.Length; i++)
            {
                if (mi[i].iChannel < 0)
                    continue;

                if (bFreqEnable)
                {
                    if (!ShowMode)
                    {
                        label = label + "(" + mi[i].fPoint.Y.ToString("0.00") + "dB)/ch" +
                                           mi[i].iChannel.ToString() + ", ";
                    }
                    else
                    {
                        label = label + "(" + mi[i].fPoint.Y.ToString("0.000") + "dB)/ch" +
                                          mi[i].iChannel.ToString() + ", ";
                    }
                }
                else
                {
                    if (!ShowMode)
                    {
                        label = label + "(" + mi[i].fPoint.X.ToString("0.00") + "MHz, " +
                                            mi[i].fPoint.Y.ToString("0.00") + "dB)/ch" +
                                            mi[i].iChannel.ToString() + ", ";
                    }
                    else
                    {
                        label = label + "(" + mi[i].fPoint.X.ToString("0.00") + "MHz, " +
                                           mi[i].fPoint.Y.ToString("0.000") + "dB)/ch" +
                                           mi[i].iChannel.ToString() + ", ";
                    }
                }
            }

            return "M" + mi[0].iOrder.ToString() + ": " + label;
        }

        private void SaveChannel_0(PointF[][] pf)
        {
            SaveDataObj = pf;
        }

        #endregion

        #endregion


        #region 按钮事件

        #region START

        private void pbxVswrStart_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                StartAnalysis();
            }
            else
            {
                StopAnalysis(1000);
            }
        }

        private void pbxVswrStart_MouseLeave(object sender, EventArgs e)
        {
            if (!bIsAnalysis)
            {
                ChangeBtnPic(pbxVswrStart, "start_in.gif");
            }
        }

        #endregion

        #region CARRIER1

        private void pbxCarrier1_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeAnalysisMode(pbxCarrier1);
        }

        private void pbxCarrier1_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxCarrier1, "carrier1.gif");
        }

        private void pbxCarrier1_MouseLeave(object sender, EventArgs e)
        {
            if (!bCarrier1Enable)
            {
                ChangeBtnPic(pbxCarrier1, "carrier1_in.gif");
            }
        }

        #endregion

        #region CARRIER2

        private void pbxCarrier2_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeAnalysisMode(pbxCarrier2);
        }

        private void pbxCarrier2_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxCarrier2, "carrier2.gif");
        }

        private void pbxCarrier2_MouseLeave(object sender, EventArgs e)
        {
            if (!bCarrier2Enable)
            {
                ChangeBtnPic(pbxCarrier2, "carrier2_in.gif");
            }
        }

        #endregion

        #region FREQ

        private void pbxVswrFreq_MouseClick(object sender, MouseEventArgs e)
        {
            ChangeAnalysisMode(pbxVswrFreq);

            if (!bIsAnalysis)
            {
                FormVswrFreq frmFreq = new FormVswrFreq(settings);
                if (frmFreq.ShowDialog() == DialogResult.OK)
                {
                    this.settings.F = frmFreq.VswrFreq;
                    ReDraw();
                }
            }

            if (this.settings.F > App_Settings.sgn_1.Max_Freq)
                CurrentRFInvolved = RFInvolved.Rf_2;
            else
                CurrentRFInvolved = RFInvolved.Rf_1;
        }

        #endregion

        #region MARK

        private void pbxVswrMark_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bMarkEnable)
            {
                pltVswr.SetMarkVisible(0, true);
                if (bFreqEnable)
                {
                    pltVswr.Mark(1);
                }
                else if (bCarrier1Enable)
                {
                    pltVswr.Mark(App_Settings.sgn_1.Min_Freq);
                }
                else
                {
                    pltVswr.Mark(App_Settings.sgn_2.Min_Freq);
                }
                ChangeBtnPic(pbxVswrMark, "mark_in.gif");
                bMarkEnable = true;
            }
            else
            {
                pltVswr.SetMarkVisible(0, false);
                ChangeBtnPic(pbxVswrMark, "mark.gif");
                bMarkEnable = false;
            }
        }

        private void pbxVswrMark_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxVswrMark, "mark.gif");
        }

        private void pbxVswrMark_MouseLeave(object sender, EventArgs e)
        {
            if (!bMarkEnable)
            {
                ChangeBtnPic(pbxVswrMark, "mark_in.gif");
            }
        }

        #endregion

        #region AUTO SCALE

        private void pbxVswrAutoscale_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bAutoScaleEnable)
            {
                if (!ShowMode)
                {
                    if (RL_Max == float.MinValue | RL_Min == float.MaxValue)
                    {
                        return;
                    }
                    pltVswr.SetYStartStop(RL_Min, RL_Max);
                }
                else
                {
                    if (VSWR_Max == float.MinValue | VSWR_Min == float.MaxValue)
                    {
                        return;
                    }
                    //if (VSWR_Max - VSWR_Min < 0.001)
                    //{
                    //    pltVswr.SetYStartStop(1, 1.001);
                    //}
                    //else
                    //{
                        pltVswr.SetYStartStop(VSWR_Min, VSWR_Max);
                    //}
                }
                ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
                bAutoScaleEnable = true;
            }
            else
            {
                if (!ShowMode)
                {
                    if (!lblReplay.Visible)
                        pltVswr.SetYStartStop(this.settings.Min_Rls, this.settings.Max_Rls);
                    else
                        pltVswr.SetYStartStop(head.Y_Min_RL, head.Y_Max_RL);
                }
                else
                {
                    if (!lblReplay.Visible)
                        pltVswr.SetYStartStop(this.settings.Min_Vsw, this.settings.Max_Vsw);
                    else
                        pltVswr.SetYStartStop(head.Y_Min_VSWR, head.Y_Max_VSWR);
                }
                ChangeBtnPic(pbxVswrAutoscale, "autoscale.gif");
                bAutoScaleEnable = false;
            }
        }

        private void pbxVswrAutoscale_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxVswrAutoscale, "autoscale.gif");
        }

        private void pbxVswrAutoscale_MouseLeave(object sender, EventArgs e)
        {
            if (!bAutoScaleEnable)
            {
                ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
            }
        }

        #endregion

        #region SAVE

        private void pbxVswrSave_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                bool bCsvOk = false;
                bool bJpgOk = false;
                bool bPdfOk = false;

                pltVswr.Save(0);
                if (SaveDataObj == null || sp == null)
                {
                    MessageBox.Show(this, "No date can be save currently!");
                    return;
                }
                bReadEnable = true;
                FormVswrSave frmSave = new FormVswrSave();
                if (frmSave.ShowDialog() == DialogResult.OK)
                {
                    if (frmSave.bEnableCsv)
                    {
                        //SAVE CSV
                        if (ValidateFileName(frmSave.CsvFileName.Substring(frmSave.CsvFileName.LastIndexOf("\\") + 1)))
                        {
                            if (!File.Exists(frmSave.CsvFileName))
                            {
                                CsvReport_IVH_Entry[] Vsw_EntryList = new CsvReport_IVH_Entry[HistoryDatalist.Count];
                                for (int i = 0; i < Vsw_EntryList.Length; i++)
                                {
                                    CsvReport_IVH_Entry Vsw_Entry = new CsvReport_IVH_Entry();
                                    Vsw_Entry.No = i;
                                    Vsw_Entry.P = HistoryDatalist[i].p;
                                    Vsw_Entry.F = HistoryDatalist[i].f;
                                    Vsw_Entry.IVH_Value = HistoryDatalist[i].rx;
                                    Vsw_Entry.Noise = HistoryDatalist[i].noise;
                                    Vsw_Entry.Rl = HistoryDatalist[i].rl;
                                    Vsw_EntryList[i] = Vsw_Entry;
                                }
                                CsvReport_PIVH_Header Vsw_Head = new CsvReport_PIVH_Header();
                                Vsw_Head.Mac_Desc = App_Configure.Cnfgs.Mac_Desc;
                                Vsw_Head.Swp_Type = sp.SweepType;
                                Vsw_Head.Im_Schema = ImSchema.FWD;
                                Vsw_Head.Point_Num = this.settings.Count;
                                Vsw_Head.Limit_Value = this.settings.Limit_Vsw;
                                Vsw_Head.Date_Time = DateTime.Now.ToString();
                                Vsw_Head.Y_Max_RL = this.settings.Max_Rls;
                                Vsw_Head.Y_Min_RL = this.settings.Min_Rls;
                                Vsw_Head.Y_Max_VSWR = this.settings.Max_Vsw;
                                Vsw_Head.Y_Min_VSWR = this.settings.Min_Vsw;
                                if (bFreqEnable)
                                {
                                    Vsw_Head.Sweep_Start = this.settings.F;
                                    Vsw_Head.Sweep_Stop = this.settings.F;
                                }
                                else if (bCarrier1Enable)
                                {
                                    Vsw_Head.Sweep_Start = App_Settings.sgn_1.Min_Freq;
                                    Vsw_Head.Sweep_Stop = App_Settings.sgn_1.Max_Freq;
                                }
                                else
                                {
                                    Vsw_Head.Sweep_Start = App_Settings.sgn_2.Min_Freq;
                                    Vsw_Head.Sweep_Stop = App_Settings.sgn_2.Max_Freq;
                                }
                                CsvReport.Save_Csv_IVH(frmSave.CsvFileName, Vsw_EntryList, Vsw_Head);
                                bCsvOk = true;
                            }
                            else
                            {
                                MessageBox.Show(this, "The CSV file name has already existed!");
                                return;
                            }
                        }
                    }
                    if (frmSave.bEnableJpg)
                    {
                        //SAVE JPG
                        if (ValidateFileName(frmSave.JpgFileName.Substring(frmSave.JpgFileName.LastIndexOf("\\") + 1)))
                        {
                            if (!File.Exists(frmSave.JpgFileName))
                            {
                                string strTitle = "";
                                Image SpectrumImage = JpgReport.GetWindow(this.Handle);
                                Graphics g = Graphics.FromImage(SpectrumImage);
                                StringFormat drawFormat = new StringFormat();
                                drawFormat.Alignment = StringAlignment.Near;
                                strTitle = "[JPG File Save Time] " + DateTime.Now.ToString();
                                g.DrawImage(SpectrumImage, new Point(0, 0));
                                g.DrawString(strTitle, new Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.FromArgb(160, 245, 255)),
                                                                new PointF(620, 70), drawFormat);
                                SpectrumImage.Save(frmSave.JpgFileName);
                                bJpgOk = true;
                            }
                            else
                            {
                                MessageBox.Show(this, "The JPG file name has already existed!");
                                return;
                            }
                        }
                    }
                    if (frmSave.bEnablePdf)
                    {
                        //SAVE PDF
                        if (ValidateFileName(frmSave.PdfFileName.Substring(frmSave.PdfFileName.LastIndexOf("\\") + 1)))
                        {
                            if (!File.Exists(frmSave.PdfFileName))
                            {
                                PdfReport_Data data = new PdfReport_Data();
                                data.Desc = App_Configure.Cnfgs.Desc; ;
                                data.Modno = App_Configure.Cnfgs.Modno; ;
                                data.Serno = App_Configure.Cnfgs.Serno;
                                data.Opeor = App_Configure.Cnfgs.Opeor;
                                data.Points_Num = this.settings.Count;
                                data.Tx_out = this.settings.Tx;
                                if (bFreqEnable)
                                {
                                    data.F_start = this.settings.F;
                                    data.F_stop = this.settings.F;
                                }
                                else if (bCarrier1Enable)
                                {
                                    data.F_start = App_Settings.sgn_1.Min_Freq;
                                    data.F_stop = App_Settings.sgn_1.Max_Freq;
                                }
                                else
                                {
                                    data.F_start = App_Settings.sgn_2.Min_Freq;
                                    data.F_stop = App_Settings.sgn_2.Max_Freq;
                                }
                                data.Max_value = VSWR_Max;
                                data.Min_value = VSWR_Min;

                                data.Limit_value = this.settings.Limit_Vsw;
                                if (data.Min_value > data.Limit_value)
                                {
                                    data.Passed = "FAIL";
                                }
                                else
                                {
                                    data.Passed = "PASS";
                                }
                                data.Footer = "VSWR Frequency Sweep";

                                Bitmap Vswrbmp = new Bitmap(pltVswr.Width,
                                                            pltVswr.Height,
                                                            System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(Vswrbmp);
                                g.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, pltVswr.Width, pltVswr.Height),
                                                                        pltVswr.BackColor,
                                                                        pltVswr.BackColor, 0f),
                                                                        new Rectangle(0, 0, pltVswr.Width, pltVswr.Height));
                                g.Dispose();
                                pltVswr.SaveImage(Vswrbmp);

                                data.Image = Vswrbmp;
                                PdfReport_Vsw vsw = new PdfReport_Vsw();
                                vsw.Do_Print(frmSave.PdfFileName, data);
                                bPdfOk = true;
                            }
                            else
                            {
                                MessageBox.Show(this, "The PDF file name has already existed!");
                                return;
                            }
                        }
                    }

                    ReDraw();

                    string strMsg = "";
                    if (frmSave.bEnableCsv)
                    {
                        if (bCsvOk)
                            strMsg += "CSV file save OK!\r\n";
                        else
                            strMsg += "CSV file save failed!\r\n";
                    }

                    if (frmSave.bEnableJpg)
                    {
                        if (bJpgOk)
                            strMsg += "JPG file save OK!\r\n";
                        else
                            strMsg += "JPG file save failed!\r\n";
                    }

                    if (frmSave.bEnablePdf)
                    {
                        if (bPdfOk)
                            strMsg += "pdf file save OK!";
                        else
                            strMsg += "pdf file save failed!";
                    }

                    MessageBox.Show(this, strMsg);
                }
            }
            else
            {
                MessageBox.Show(this, "VSWR is analysising!");
            }
        }

        private void pbxVswrSave_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxVswrSave, "save.gif");
        }

        private void pbxVswrSave_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pbxVswrSave, "save_in.gif");
        }

        #endregion

        #region READ

        private void pbxVswrRead_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                IsoReadDataForm frmRead = new IsoReadDataForm();
                frmRead.FillFiles(App_Configure.Cnfgs.Path_Rpt_Vsw + "\\csv");

                //FormVswrRead frmRead = new FormVswrRead();
                if (frmRead.ShowDialog() == DialogResult.OK)
                {
                    string strFilePath = App_Configure.Cnfgs.Path_Rpt_Vsw + "\\csv\\" + frmRead.FileName;
                    bAutoScaleEnable = false;
                    ChangeBtnPic(pbxVswrRead, "read.gif");
                    bReadEnable = true;

                    try
                    {
                        CsvReport.Read_Csv_IVH(strFilePath, out listEntry, out head);
                        ReplayDarw(head);
                        RL_Max = float.MinValue;
                        RL_Min = float.MaxValue;
                        VSWR_Max = float.MinValue;
                        VSWR_Min = float.MaxValue;
                        pltVswr.Clear();
                        while (listEntry.Count > 0)
                        {
                            DarwHistroyPoint(head, listEntry);
                            listEntry.RemoveAt(0);
                        }
                        ChangeBtnPic(pbxVswrRead, "read_in.gif");
                        ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
                        bReadEnable = false;
                        bAutoScaleEnable = false;
                    }
                    catch
                    {
                        MessageBox.Show(this, "Data Reading failed!");
                        ChangeBtnPic(pbxVswrRead, "read_in.gif");
                        bReadEnable = false;
                        bAutoScaleEnable = false;
                        return;
                    }
                }
                else
                {
                    ChangeBtnPic(pbxVswrRead, "read_in.gif");
                }
            }
            else
            {
                MessageBox.Show(this, "VSWR is analysising!");
            }
        }

        private void pbxVswrRead_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxVswrRead, "read.gif");
        }

        private void pbxVswrRead_MouseLeave(object sender, EventArgs e)
        {
            if (!bReadEnable)
            {
                ChangeBtnPic(pbxVswrRead, "read_in.gif");
            }
        }

        #endregion

        #region SETTING

        private void pbxVswrSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                VswrSettingForm frmVswrStting = new VswrSettingForm(this.settings);
                if (frmVswrStting.ShowDialog() == DialogResult.OK)
                {
                    ReDraw();
                }
                listCurrentCAL_1 = VswrSettingForm.listCurrentCAL_1;
                listCurrentCAL_2 = VswrSettingForm.listCurrentCAL_2;

                if (VswrSettingForm.bEnableCAL_RF1)
                    lblCAL1.Text = "CAL-1:ON";
                else
                    lblCAL1.Text = "CAL-1:OFF";

                if (VswrSettingForm.bEnableCAL_RF2)
                    lblCAL2.Text = "CAL-2:ON";
                else
                    lblCAL2.Text = "CAL-2:OFF";
            }
            else
            {
                MessageBox.Show(this, "VSWR is analysising!");
            }
        }

        private void pbxVswrSetting_MouseMove(object sender, MouseEventArgs e)
        {
            ChangeBtnPic(pbxVswrSetting, "setting.gif");
        }

        private void pbxVswrSetting_MouseLeave(object sender, EventArgs e)
        {
            ChangeBtnPic(pbxVswrSetting, "setting_in.gif");
        }

        #endregion

        #region RL/VSWR

        private void rl_vswr_MouseClick(object sender, MouseEventArgs e)
        {
            int[] s = new int[5] { -1, -1, -1, -1, -1 }; 
            ShowMode = !ShowMode;
            if (!ShowMode)
            {
                pltVswr.SetChannelVisible(0, true);
                pltVswr.SetChannelVisible(1, false);
                s[0] = 0;
                pltVswr.SetMarkSequence(0, s);
                ChangeBtnPic(rl_vswr, "returnloss.gif");
            }
            else
            {
                pltVswr.SetChannelVisible(0, false);
                pltVswr.SetChannelVisible(1, true);
                s[0] = 1;
                pltVswr.SetMarkSequence(0, s);
                ChangeBtnPic(rl_vswr, "vswr.gif");
            }

            bAutoScaleEnable = false;
            ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
            ReDraw();
        }

        #endregion

        #endregion


        #region 辅助方法

        #region 启动驻波比分析
        /// <summary>
        /// 启动驻波比分析
        /// </summary>
        private void StartAnalysis()
        {
            if (bIsAnalysis)
                return;

            //修改状态变量
            bIsAnalysis = true;
            lblReplay.Visible = false;
            ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
            bAutoScaleEnable = false;
            ChangeBtnPic(pbxVswrMark, "mark_in.gif");
            bMarkEnable = false;
            pltVswr.SetMarkVisible(0, false);
            ChangeBtnPic(pbxVswrStart, "start.gif");

            HistoryDatalist.Clear();
            sp = new SweepParams();
            sp.WndHandle = this.Handle;

            //扫描模式判断
            if (bFreqEnable)
            {
                sp.SweepType = SweepType.Time_Sweep;
                sp.TmeParam = new TimeSweepParam();
                if (this.settings.F >= App_Settings.sgn_1.Min_Freq && this.settings.F <= App_Settings.sgn_1.Max_Freq)
                {
                    sp.RFInvolved = RFInvolved.Rf_1;
                    sp.TmeParam.F1 = this.settings.F;

                    if (!CheckOutputRange(this.settings.Tx, RFInvolved.Rf_1))
                    {
                        MessageBox.Show(this, "The carrier power setup is out of its range!");
                        ChangeBtnPic(pbxVswrStart, "start_in.gif");
                        bIsAnalysis = false;
                        return;
                    }
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        sp.TmeParam.P1 = Tx_Tables.vsw_tx1.Offset(this.settings.F, this.settings.Tx, Tx_Tables.vsw_offset1) + this.settings.Tx;
                    }
                    else
                    {
                        sp.TmeParam.P1 = (float)App_Factors.vsw_tx1.ValueWithOffset(this.settings.F, this.settings.Tx);
                    }

                    //if (!VswrSettingForm.bEnableCAL_RF1)
                    //{
                    //    MessageBox.Show(this, "Please Calibration Carrier1 first!");
                    //    ChangeBtnPic(pbxVswrStart, "start_in.gif");
                    //    bIsAnalysis = false;
                    //    return;
                    //}
                }
                if (this.settings.F >= App_Settings.sgn_2.Min_Freq && this.settings.F <= App_Settings.sgn_2.Max_Freq)
                {
                    sp.RFInvolved = RFInvolved.Rf_2;
                    sp.TmeParam.F2 = this.settings.F;

                    if (!CheckOutputRange(this.settings.Tx, RFInvolved.Rf_2))
                    {
                        MessageBox.Show(this, "The carrier power setup is out of its range!");
                        ChangeBtnPic(pbxVswrStart, "start_in.gif");
                        bIsAnalysis = false;
                        return;
                    }
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        sp.TmeParam.P2 = Tx_Tables.vsw_tx2.Offset(this.settings.F, this.settings.Tx, Tx_Tables.vsw_offset2) + this.settings.Tx;
                    }
                    else
                    {
                        sp.TmeParam.P2 = (float)App_Factors.vsw_tx2.ValueWithOffset(this.settings.F, this.settings.Tx);
                    }

                    //if (!VswrSettingForm.bEnableCAL_RF2)
                    //{
                    //    MessageBox.Show(this, "Please Calibration Carrier2 first!");
                    //    ChangeBtnPic(pbxVswrStart, "start_in.gif");
                    //    bIsAnalysis = false;
                    //    return;
                    //}
                }
                sp.TmeParam.Rx = this.settings.F;
                sp.TmeParam.N = this.settings.Count;
            }
            else
            {
                sp.SweepType = SweepType.Freq_Sweep;
                sp.FrqParam = new FreqSweepParam();
                if (bCarrier1Enable)
                {
                    sp.RFInvolved = RFInvolved.Rf_1;

                    if (!CheckOutputRange(this.settings.Tx, RFInvolved.Rf_1))
                    {
                        MessageBox.Show(this, "The carrier power setup is out of its range!");
                        ChangeBtnPic(pbxVswrStart, "start_in.gif");
                        bIsAnalysis = false;
                        return;
                    }
                    sp.FrqParam.Items1 = GetItemList(RFInvolved.Rf_1);

                    //if (!VswrSettingForm.bEnableCAL_RF1)
                    //{
                    //    MessageBox.Show(this, "Please Calibration Carrier1 first!");
                    //    ChangeBtnPic(pbxVswrStart, "start_in.gif");
                    //    bIsAnalysis = false;
                    //    return;
                    //}

                    //if(!CheckOutputRange(sp.FrqParam.Items1,RFInvolved.Rf_1))
                    //{
                    //    MessageBox.Show(this, "The carrier power setup is out of its range!");
                    //    ChangeBtnPic(pbxVswrStart, "start_in.gif");
                    //    bIsAnalysis = false;
                    //    return;
                    //}
                }
                if (bCarrier2Enable)
                {
                    sp.RFInvolved = RFInvolved.Rf_2;

                    if (!CheckOutputRange(this.settings.Tx, RFInvolved.Rf_2))
                    {
                        MessageBox.Show(this, "The carrier power setup is out of its range!");
                        ChangeBtnPic(pbxVswrStart, "start_in.gif");
                        bIsAnalysis = false;
                        return;
                    }
                    sp.FrqParam.Items2 = GetItemList(RFInvolved.Rf_2);

                    //if (!VswrSettingForm.bEnableCAL_RF2)
                    //{
                    //    MessageBox.Show(this, "Please Calibration Carrier2 first!");
                    //    ChangeBtnPic(pbxVswrStart, "start_in.gif");
                    //    bIsAnalysis = false;
                    //    return;
                    //}

                    //if (!CheckOutputRange(sp.FrqParam.Items2, RFInvolved.Rf_2))
                    //{
                    //    MessageBox.Show(this, "The carrier power setup is out of its range!");
                    //    ChangeBtnPic(pbxVswrStart, "start_in.gif");
                    //    bIsAnalysis = false;
                    //    return;
                    //}
                }
            }

            //设备参数
            sp.DevInfo = new DeviceInfo();
            sp.DevInfo.RF_Addr1 = App_Configure.Cnfgs.ComAddr1;
            sp.DevInfo.RF_Addr2 = App_Configure.Cnfgs.ComAddr2;
            sp.DevInfo.Spectrum = App_Configure.Cnfgs.Spectrum;
            sp.C = 1;

            //频谱参数
            sp.SpeParam = new SpectrumLib.Models.ScanModel();
            sp.SpeParam.Att = this.settings.Att_Spc;
            sp.SpeParam.Rbw = this.settings.Rbw_Spc;
            sp.SpeParam.Vbw = this.settings.Vbw_Spc;
            sp.SpeParam.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;
            sp.SpeParam.Continued = false;
            sp.SpeParam.FullPoints = true;
            sp.SpeParam.DeliSpe = SpectrumLib.Defines.CommonDef.SpectrumType.Deli_VSWR;

            RL_Max = float.MinValue;
            RL_Min = float.MaxValue;
            VSWR_Max = float.MinValue;
            VSWR_Min = float.MaxValue;
            if (!ShowMode)
            {
                pltVswr.SetChannelVisible(0, true);
                pltVswr.SetChannelVisible(1, false);
            }
            else
            {
                pltVswr.SetChannelVisible(0, false);
                pltVswr.SetChannelVisible(1, true);
            }

            //启动驻波分析
            ReDraw();
            pltVswr.Clear();
            SweepObj.InitSweep();
            SweepObj.Prepare(sp);
            SweepObj.StartSweep();
        }

        #endregion

        #region 停止驻波比分析
        /// <summary>
        /// 停止驻波比分析
        /// </summary>
        private void StopAnalysis(int timeOut)
        {
            SweepObj.StopSweep(timeOut);
            ChangeBtnPic(pbxVswrStart, "start_in.gif");
            pltVswr.SetXAutoScroll(false);
        }

        #endregion

        #region 加载驻波配置文件
        /// <summary>
        /// 加载驻波配置文件
        /// </summary>
        private void LoadConfig()
        {
            //从默认驻波比模块配置对象复制设置项的值
            App_Settings.vsw.Clone(this.settings);

            if (this.settings.Min_Rls >= this.settings.Max_Rls)
                this.settings.Min_Rls = this.settings.Max_Rls - 1;
            if (this.settings.Min_Vsw >= this.settings.Max_Vsw)
                this.settings.Min_Vsw = this.settings.Max_Vsw - 1;

            //默认设置点频
            ChangeAnalysisMode(pbxVswrFreq);
        }

        #endregion

        #region 加载驻波校准文件
        /// <summary>
        /// 加载驻波校准文件
        /// </summary>
        private void LoadCALconfig()
        {
            CalibrationObj CALObjItem;
            List<RL0_TableItem> ListItem = RL0_Tables.Items(FuncModule.VSW, CurrentRFInvolved);

            for (int i = 0; i < ListItem.Count; i++)
            {
                CALObjItem.Freq = ListItem[i].F;
                CALObjItem.RL0 = 0;
                CALObjItem.Tx0 = 0;
                CALObjItem.Rx0 = 0;
                listCurrentCAL_1.Add(CALObjItem);
            }

            for (int i = 0; i < ListItem.Count; i++)
            {
                CALObjItem.Freq = ListItem[i].F;
                CALObjItem.RL0 = 0;
                CALObjItem.Tx0 = 0;
                CALObjItem.Rx0 = 0;
                listCurrentCAL_2.Add(CALObjItem);
            }
        }

        #endregion

        #region 根据配置参数重绘
        /// <summary>
        /// 根据配置参数重绘
        /// </summary>
        private void ReDraw()
        {
            if (!ShowMode)
            {
                double a = (settings.Limit_Vsw - 1) / (settings.Limit_Vsw + 1);
                pltVswr.SetLimitEnalbe(true, (float)(-20 * Math.Log10(a)), Color.FromArgb(160, 245, 255)); 

                if (bAutoScaleEnable)
                {
                    if (lblReplay.Visible == false)
                        pltVswr.SetYStartStop(RL_Min, RL_Max);
                    else
                    {
                        pltVswr.SetYStartStop(head.Y_Min_RL, head.Y_Max_RL);
                        ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
                    }
                }
                else
                {
                    if (lblReplay.Visible == false)
                        pltVswr.SetYStartStop(this.settings.Min_Rls, this.settings.Max_Rls);
                    else
                        pltVswr.SetYStartStop(head.Y_Min_RL, head.Y_Max_RL);
                }

                double avg = (this.settings.Max_Rls - this.settings.Min_Rls) / 10.0;
                lblVswrDiv.Text = avg.ToString() + "dB/Div";
            }
            else
            {
                pltVswr.SetLimitEnalbe(true, this.settings.Limit_Vsw, Color.FromArgb(160, 245, 255));

                if (bAutoScaleEnable)
                {
                    if (lblReplay.Visible == false)
                        pltVswr.SetYStartStop(VSWR_Min, VSWR_Max);
                    else
                    {
                        pltVswr.SetYStartStop(head.Y_Min_VSWR, head.Y_Max_VSWR);
                        ChangeBtnPic(pbxVswrAutoscale, "autoscale_in.gif");
                    }
                }
                else
                {
                    if (lblReplay.Visible == false)
                        pltVswr.SetYStartStop(this.settings.Min_Vsw, this.settings.Max_Vsw);
                    else
                        pltVswr.SetYStartStop(head.Y_Min_VSWR, head.Y_Max_VSWR);
                }

                double avg = (this.settings.Max_Vsw - this.settings.Min_Vsw) / 10.0;
                lblVswrDiv.Text = avg.ToString() + "dB/Div";
            }

            if (bFreqEnable)
            {
                if (lblReplay.Visible == false)
                {
                    pltVswr.SetXStartStop(0f, this.settings.Count);
                    if (this.settings.F > App_Settings.sgn_1.Max_Freq)
                        lblVswrFreq.Text = "Carrier2 Time Sweep  F=" + this.settings.F.ToString("0.#") + "MHz";
                    else
                        lblVswrFreq.Text = "Carrier1 Time Sweep  F=" + this.settings.F.ToString("0.#") + "MHz";
                }
                else
                    pltVswr.SetXStartStop(0f, head.Point_Num);
            }
            else if (bCarrier1Enable)
            {
                pltVswr.SetXStartStop(App_Settings.sgn_1.Min_Freq, App_Settings.sgn_1.Max_Freq);
                lblVswrFreq.Text = "Carrier1 Freqency Sweep";
            }
            else
            {
                pltVswr.SetXStartStop(App_Settings.sgn_2.Min_Freq, App_Settings.sgn_2.Max_Freq);
                lblVswrFreq.Text = "Carrier2 Freqency Sweep";
            }
        }

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

        #endregion

        #region 通用扫频1、2，点频切换
        /// <summary>
        /// 通用扫频1、2，点频切换
        /// </summary>
        /// <param name="pb">PictureBox对象</param>
        private void ChangeAnalysisMode(PictureBox pb)
        {
            if (!bIsAnalysis)
            {
                ChangeBtnPic(pbxCarrier1, "carrier1_in.gif");
                ChangeBtnPic(pbxCarrier2, "carrier2_in.gif");
                ChangeBtnPic(pbxVswrFreq, "freq_in.gif");
                bCarrier1Enable = false;
                bCarrier2Enable = false;
                bFreqEnable = false;

                if (pb.Equals(pbxCarrier1))
                {
                    CurrentRFInvolved = RFInvolved.Rf_1;
                    ChangeBtnPic(pbxCarrier1, "carrier1.gif");
                    bCarrier1Enable = true;
                }
                else if (pb.Equals(pbxCarrier2))
                {
                    CurrentRFInvolved = RFInvolved.Rf_2;
                    ChangeBtnPic(pbxCarrier2, "carrier2.gif");
                    bCarrier2Enable = true;
                }
                else
                {
                    if (this.settings.F >= App_Settings.sgn_1.Min_Freq && this.settings.F <= App_Settings.sgn_1.Max_Freq)
                    {
                        CurrentRFInvolved = RFInvolved.Rf_1;
                    }
                    if (this.settings.F >= App_Settings.sgn_2.Min_Freq && this.settings.F <= App_Settings.sgn_2.Max_Freq)
                    {
                        CurrentRFInvolved = RFInvolved.Rf_2;
                    }
                    ChangeBtnPic(pbxVswrFreq, "freq.gif");
                    bFreqEnable = true;
                }

                ReDraw();
            }
            else
            {
                MessageBox.Show(this, "VSWR is analysising!");
            }
        }

        #endregion

        #region 根据步进获取扫描序列
        /// <summary>
        /// 根据步进获取扫描序列
        /// </summary>
        /// <param name="RF_num">功放编号</param>
        /// <returns>扫描序列</returns>
        private FreqSweepItem[] GetItemList(RFInvolved RF_num)
        {
            FreqSweepItem[] revItem = null;
            int count = 0;
            if (RF_num == RFInvolved.Rf_1)
            {
                if (((App_Settings.sgn_1.Max_Freq - App_Settings.sgn_1.Min_Freq) % this.settings.Freq_Step) > 0)
                {
                    count = (int)((App_Settings.sgn_1.Max_Freq - App_Settings.sgn_1.Min_Freq) / this.settings.Freq_Step) + 2;
                }
                else
                {
                    count = (int)((App_Settings.sgn_1.Max_Freq - App_Settings.sgn_1.Min_Freq) / this.settings.Freq_Step) + 1;
                }

                revItem = new FreqSweepItem[count];
                for (int i = 0; i < count; i++)
                {
                    revItem[i] = new FreqSweepItem();
                    revItem[i].Tx1 = App_Settings.sgn_1.Min_Freq + i * this.settings.Freq_Step;                   
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        revItem[i].P1 = Tx_Tables.vsw_tx1.Offset(revItem[i].Tx1, this.settings.Tx, Tx_Tables.vsw_offset1) + this.settings.Tx;
                    }
                    else
                    {
                        revItem[i].P1 = (float)App_Factors.vsw_tx1.ValueWithOffset(revItem[i].Tx1, this.settings.Tx);
                    }
                    revItem[i].Rx = revItem[i].Tx1;
                    if (revItem[i].Tx1 > App_Settings.sgn_1.Max_Freq)
                    {
                        revItem[i].Tx1 = App_Settings.sgn_1.Max_Freq;
                    }
                }
            }
            if (RF_num == RFInvolved.Rf_2)
            {
                if (((App_Settings.sgn_2.Max_Freq - App_Settings.sgn_2.Min_Freq) % this.settings.Freq_Step) > 0)
                {
                    count = (int)((App_Settings.sgn_2.Max_Freq - App_Settings.sgn_2.Min_Freq) / this.settings.Freq_Step) + 2;
                }
                else
                {
                    count = (int)((App_Settings.sgn_2.Max_Freq - App_Settings.sgn_2.Min_Freq) / this.settings.Freq_Step) + 1;
                }

                revItem = new FreqSweepItem[count];
                for (int i = 0; i < count; i++)
                {
                    revItem[i] = new FreqSweepItem();
                    revItem[i].Tx2 = App_Settings.sgn_2.Min_Freq + i * this.settings.Freq_Step;
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        revItem[i].P2 = Tx_Tables.vsw_tx2.Offset(revItem[i].Tx2, this.settings.Tx, Tx_Tables.vsw_offset2) + this.settings.Tx;
                    }
                    else
                    {
                        revItem[i].P2 = (float)App_Factors.vsw_tx2.ValueWithOffset(revItem[i].Tx2, this.settings.Tx);
                    }
                    revItem[i].Rx = revItem[i].Tx2;
                    if (revItem[i].Tx2 > App_Settings.sgn_2.Max_Freq)
                    {
                        revItem[i].Tx2 = App_Settings.sgn_2.Max_Freq;
                    }
                }
            }

            return revItem;
        }

        #endregion

        #region 根据频点获取校准参数
        /// <summary>
        /// 根据频点获取校准参数
        /// </summary>
        /// <param name="freq">频点</param>
        /// <returns>校准对象</returns>
        private CalibrationObj CalItem(float freq)
        {
            CalibrationObj revCalItem = new CalibrationObj();
            if (CurrentRFInvolved == RFInvolved.Rf_1)
            {
                for (int i = 0; i < listCurrentCAL_1.Count; i++)
                {
                    if (freq <= listCurrentCAL_1[i].Freq)
                    {
                        revCalItem = listCurrentCAL_1[i];
                        break;
                    }
                }
            }
            else
            {
                for (int i = 1; i < listCurrentCAL_2.Count; i++)
                {
                    if (freq <= listCurrentCAL_2[i].Freq)
                    {
                        revCalItem = listCurrentCAL_2[i];
                        break;
                    }
                }
            }

            return revCalItem;
        }

        #endregion

        #region 获取扫描结果对象绘图
        /// <summary>
        /// 获取扫描结果对象绘图
        /// </summary>
        /// <param name="index">扫描频点索引</param>
        private void DarwingPoint(int index)
        {
            Vsw_Sweep.ResultObj Obj = SweepObj.GetVswrScanResult();
            PointF[] pf_rl = new PointF[1];
            PointF[] pf_vswr = new PointF[1];
            float Tx = Obj.Pstatus.Status2.OutP;
            float Freq = Obj.Pstatus.Status2.Freq;
            float Rx = Obj.Sstatus.dBmValue;
            float Noise = Obj.Sstatus.dBmNosie;
            float RL = 0;
            float VSWR = 0;
            float a = 0;

            CalibrationObj calItem = CalItem(Freq);
            RL = (Tx - Rx) - (calItem.Tx0 - calItem.Rx0) + calItem.RL0;
            a = (float)Math.Pow(10, -RL / 20);
            VSWR = (1 + a) / (1 - a);
            if (bFreqEnable)
            {
                if (Freq > App_Settings.sgn_1.Max_Freq)
                {
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        Tx += Tx_Tables.vsw_tx2_disp.Offset(Freq, Tx,Tx_Tables.vsw_offset1_disp);
                        Rx += Rx_Tables.Offset(Freq, FuncModule.VSW);
                    }
                    else
                    {
                        Tx = (float)App_Factors.vsw_tx2_disp.ValueWithOffset(Freq, Tx);
                        Rx = (float)App_Factors.vsw_rx2.ValueWithOffset(Freq, Rx);
                    }
                }
                else
                {
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        Tx += Tx_Tables.vsw_tx1_disp.Offset(Freq, Tx, Tx_Tables.vsw_offset2_disp);
                        Rx += Rx_Tables.Offset(Freq, FuncModule.VSW);
                    }
                    else
                    {
                        Tx = (float)App_Factors.vsw_tx1_disp.ValueWithOffset(Freq, Tx);
                        Rx = (float)App_Factors.vsw_rx1.ValueWithOffset(Freq, Rx);
                    }
                }

                pf_rl[0].X = index;
                pf_rl[0].Y = RL;
                pf_vswr[0].X = index;
                pf_vswr[0].Y = VSWR;
                pltVswr.Add(pf_rl, 0, index);
                pltVswr.Add(pf_vswr, 1, index);
            }
            else
            {
                if (bCarrier1Enable)
                {
                    Tx = (float)App_Factors.vsw_tx1_disp.ValueWithOffset(Freq, Tx);
                    Rx = (float)App_Factors.vsw_rx1.ValueWithOffset(Freq, Rx);
                }
                if (bCarrier2Enable)
                {
                    Tx = (float)App_Factors.vsw_tx2_disp.ValueWithOffset(Freq, Tx);
                    Rx = (float)App_Factors.vsw_rx2.ValueWithOffset(Freq, Rx);
                }

                pf_rl[0].X = Freq;
                pf_rl[0].Y = RL;
                pf_vswr[0].X = Freq;
                pf_vswr[0].Y = VSWR;
                pltVswr.Add(pf_rl, 0, index);
                pltVswr.Add(pf_vswr, 1, index);
            }

            //保存历史数据
            HistoryData hd = new HistoryData();
            hd.p = Tx;
            hd.f = Freq;
            hd.rl = RL;
            hd.vswr = VSWR;
            hd.noise = Noise;
            HistoryDatalist.Add(hd);

            //更新RL最大最小值
            bool bChange_rl = false;
            if (RL > RL_Max)
            {
                RL_Max = RL;
                bChange_rl = true;
            }
            if (RL < RL_Min)
            {
                RL_Min = RL;
                bChange_rl = true;
            }
            //更新VSWR最大最小值
            bool bChange_vswr = false;
            if (VSWR > VSWR_Max)
            {
                VSWR_Max = VSWR;
                bChange_vswr = true;
            }
            if (VSWR < VSWR_Min)
            {
                VSWR_Min = VSWR;
                bChange_vswr = true;
            }

            if (bAutoScaleEnable && bChange_rl && (!ShowMode))
            {
                pltVswr.SetYStartStop(RL_Min, RL_Max);
            }
            if (bAutoScaleEnable && bChange_vswr && ShowMode)
            {
                pltVswr.SetYStartStop(VSWR_Min, VSWR_Max);
                //pltVswr.SetYStartStop(1, 2);
            }

            lbVswrTx.Text = "Tx:" + Tx.ToString("0.#") + "dBm";
            lblRL.Text = "RL:" + RL.ToString("0.#") + "dB";
            lblRx.Text = "Rx:" + Rx.ToString("0.#") + "dBm";
            lblNoise.Text = "Noise:" + Noise.ToString("0.00") + "dBm";
        }

        #endregion

        #region 根据回放参数重绘
        /// <summary>
        /// 根据回放参数重绘
        /// </summary>
        /// <param name="head">参数对象</param>
        private void ReplayDarw(CsvReport_PIVH_Header head)
        {
            if (head.Swp_Type == SweepType.Time_Sweep)
            {
                pltVswr.SetXStartStop(0, head.Point_Num);
                if (head.Sweep_Start > App_Settings.sgn_1.Max_Freq)
                    lblVswrFreq.Text = "Carrier2 Time Sweep  F=" + head.Sweep_Start.ToString("0.#") + "MHz";
                else
                    lblVswrFreq.Text = "Carrier1 Time Sweep  F=" + head.Sweep_Start.ToString("0.#") + "MHz";
            }
            else
            {
                pltVswr.SetXStartStop(head.Sweep_Start, head.Sweep_Stop);
                if (head.Sweep_Start > App_Settings.sgn_1.Max_Freq)
                    lblVswrFreq.Text = "Carrier2 Freqency Sweep";
                else
                    lblVswrFreq.Text = "Carrier1 Freqency Sweep";
            }

            if (!ShowMode)
            {
                pltVswr.SetYStartStop(head.Y_Min_RL, head.Y_Max_RL);
                double avg = (head.Y_Max_RL - head.Y_Min_RL) / 10.0;
                lblVswrDiv.Text = avg.ToString() + "dB/Div";
            }
            else
            {
                pltVswr.SetYStartStop(head.Y_Min_VSWR, head.Y_Max_VSWR);
                double avg = (head.Y_Max_VSWR - head.Y_Min_VSWR) / 10.0;
                lblVswrDiv.Text = avg.ToString() + "dB/Div";
            }

            lblReplay.Visible = true;
            lblReplay.Text = "Replay: " + head.Date_Time.ToString();
        }

        #endregion

        #region 获取扫描历史数据绘图
        /// <summary>
        /// 获取扫描历史数据绘图
        /// </summary>
        private void DarwHistroyPoint(CsvReport_PIVH_Header head, List<CsvReport_IVH_Entry> listEntry)
        {
            PointF[] pf_rl = new PointF[1];
            PointF[] pf_vswr = new PointF[1];

            int index = listEntry[0].No;
            float Freq = listEntry[0].F;
            float Tx = listEntry[0].P;
            float Noise = listEntry[0].Noise;
            float Rx = listEntry[0].IVH_Value;
            float RL = listEntry[0].Rl;
            float a = (float)Math.Pow(10, -RL / 20);
            float VSWR = (1 + a) / (1 - a);

            if (head.Swp_Type == SweepType.Time_Sweep)
            {
                pf_rl[0].X = index;
                pf_rl[0].Y = RL;
                pf_vswr[0].X = index;
                pf_vswr[0].Y = VSWR;
                pltVswr.Add(pf_rl, 0, index);
                pltVswr.Add(pf_vswr, 1, index);
            }
            else
            {
                pf_rl[0].X = Freq;
                pf_rl[0].Y = RL;
                pf_vswr[0].X = Freq;
                pf_vswr[0].Y = VSWR;
                pltVswr.Add(pf_rl, 0, index);
                pltVswr.Add(pf_vswr, 1, index);
            }

            //更新RL最大最小值
            if (RL > RL_Max)
            {
                RL_Max = RL;
            }
            if (RL < RL_Min)
            {
                RL_Min = RL;
            }
            //更新VSWR最大最小值
            if (VSWR > VSWR_Max)
            {
                VSWR_Max = VSWR;
            }
            if (VSWR < VSWR_Min)
            {
                VSWR_Min = VSWR;
            }
        }

        #endregion

        #region 输出功率范围校验

        private bool CheckOutputRange(float p, RFInvolved Num)
        {
            bool rev = true;
            if (Num == RFInvolved.Rf_1)
            {
                if (p > App_Settings.sgn_1.Max_Power || p < App_Settings.sgn_1.Min_Power)
                    rev = false;
            }
            else
            {
                if (p > App_Settings.sgn_1.Max_Power || p < App_Settings.sgn_1.Min_Power)
                    rev = false;
 
            }
            return rev;
        }

        private bool CheckOutputRange(FreqSweepItem[] listP, RFInvolved Num)
        {
            bool rev = true;
            if (Num == RFInvolved.Rf_1)
            {
                for (int i = 0; i < listP.Length; i++)
                {
                    if (listP[i].P1 > App_Settings.sgn_1.Max_Power || listP[i].P1 < App_Settings.sgn_1.Min_Power)
                    {
                        rev = false;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < listP.Length; i++)
                {
                    if (listP[i].P2 > App_Settings.sgn_2.Max_Power || listP[i].P2 < App_Settings.sgn_2.Min_Power)
                    {
                        rev = false;
                        break;
                    }
                }
            }

            return rev;
        }

        #endregion

        #region 文件名合法性校验
        /// <summary>
        /// 文件名合法性校验
        /// </summary>
        /// <param name="txt">文件名</param>
        /// <returns>true合法，false不合法</returns>
        private bool ValidateFileName(string txt)
        {
            string[] str = new string[] { "\\", "/", ":", "*", "?", "<", ">", "|" };

            if (txt != "")
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (txt.IndexOf(str[i]) != -1)
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        #endregion

        #endregion


        #region 窗体消息循环
        /// <summary>
        /// 窗体消息循环
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //完成一趟操作
                case MessageID.VSW_SWEEP_DONE:
                    StopAnalysis(1000);
                    bIsAnalysis = false;
                    break;
                //完成单点扫描
                case MessageID.VSW_SUCCED:
                    DarwingPoint(m.LParam.ToInt32());
                    break;
                //功放操作错误
                case MessageID.RF_ERROR:
                    StopAnalysis(1000);
                    //MessageBox.Show(this, "RF_Error");
                    SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);
                    MessageBox.Show(this,rfr_errors1.ToString() + "\n\r" + rfr_errors2.ToString());
                    break;
                //频谱分析错误
                case MessageID.SPECTRUM_ERROR:
                    StopAnalysis(1000);
                    MessageBox.Show(this, "Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");
                    break;
                //频谱分析成功
                case MessageID.SPECTRUEME_SUCCED:
                    SweepObj.Spectrum_Succed();
                    break;
                //功放操作成功
                case MessageID.RF_SUCCED_ALL:
                    if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr1)
                        SweepObj.Power1_Succed();
                    else if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr2)
                        SweepObj.Power2_Succed();
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion


        #region ISweep 成员

        public void BreakSweep(int timeOut)
        {
            StopAnalysis(timeOut);
            bIsAnalysis = false;
        }

        #endregion
    }
}