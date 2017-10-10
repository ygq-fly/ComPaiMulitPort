using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using jcXY2dPlotEx;


namespace jcPimSoftware
{
    public partial class HarForm : Form, ISweep
    {
       
        #region 二次谐波模块的实例变量
        /// <summary>
        /// 配置项对象，由构造函数建立
        /// </summary>
        private Settings_Har settings;

        /// <summary>
        /// 二次谐波分析对象，仅在构造函数被建立一次
        /// </summary>
        private Har_Sweep SweepObj;
        
        /// <summary>
        /// 扫描参数对象，每次启动扫描都被重新构建 
        /// </summary>
        private SweepParams sweep_params;

        /// <summary>
        /// 标识扫描循环已经启动
        /// </summary>
        private bool Sweeping = false;

        /// <summary>
        /// 标识正在进行的扫描，已经完成的点数
        /// </summary>
        private int PointsDone;

        /// <summary>
        /// 当前欲操作的功放指示字
        /// </summary>
        private RFInvolved rf_involved = RFInvolved.Rf_1;

        /// <summary>
        /// 指示启用扫频 TRUE，或者启用扫时 FALSE
        /// </summary>
        private SweepType sweep_or_time = SweepType.Time_Sweep;

        /// <summary>
        /// 跟踪Autoscale的使能状态，并依此改变Autoscale按钮的贴图
        /// </summary>
        private bool AutoscaleEnable;

        /// <summary>
        /// 跟踪MARK的可见性，并依此改变Mark按钮的贴图
        /// </summary>
        private bool MarkVisible;

        /// <summary>
        /// 存放扫描结果，在扫描过程中，不断被赋予新值
        /// </summary>
        PointF[] sweep_points = new PointF[1];
        
        /// <summary>
        /// CSV报表头部
        /// </summary>
        private CsvReport_PIVH_Header csv_header = new CsvReport_PIVH_Header();

        /// <summary>
        /// CSV报表数据项
        /// </summary>
        private CsvReport_IVH_Entry[] csv_entries;

        /// <summary>
        /// PDF报表数据
        /// </summary>
        private PdfReport_Data pdf_data = new PdfReport_Data();

        /// <summary>
        /// PDF报表对象
        /// </summary>
        private PdfReport_Har pdf_har = new PdfReport_Har();
        #endregion


        #region 设备状态对象与扫描结构对象，由sweepobj.CloneReference赋值
        private PowerStatus ps1;
        private PowerStatus ps2;
        private SweepResult sr;
        private RFErrors rfr_errors1;
        private RFErrors rfr_errors2;
        #endregion


        #region 窗体的构造与加载
        public HarForm()
        {
            //建立扫描对象
            SweepObj = new Har_Sweep();

            //构造二次谐波模块配置对象
            this.settings = new Settings_Har("");
            
            //从默认二次谐波模块配置对象复制设置项的值
            App_Settings.har.Clone(this.settings);

            if (this.settings.Min_Har > this.settings.Max_Har)
                this.settings.Min_Har = this.settings.Max_Har - 1;

            InitializeComponent();

            pltHar.Resume();

            pltHar.SetSampling(false);

            this.TopLevel = false;
            this.ShowInTaskbar = false;
            this.Dock = DockStyle.Fill;
        }

        private void HarForm_Load(object sender, EventArgs e)
        {
            pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq.gif");

            pltHar.SetLimitEnalbe(true, this.settings.Limit, Color.FromArgb(160, 245, 255));

            UpdateWihtNewSettings();
        }

        public string Sweep_MarkText(MarkInfo[] mi)
        {
            string label = "";

            for (int i = 0; i < mi.Length; i++)
            {
                if (mi[i].iChannel < 0)
                    continue;

                label = label +
                        "(" + mi[i].fPoint.X.ToString("0.00") + "MHz, " +
                         mi[i].fPoint.Y.ToString("0.00") + "dBm)";
            }

            return "M" + mi[0].iOrder.ToString() + ": " + label;
        }

        public string Fixed_MarkText(MarkInfo[] mi)
        {
            string label = "";

            for (int i = 0; i < mi.Length; i++)
            {
                if (mi[i].iChannel < 0)
                    continue;

                label = label + "(" + mi[i].fPoint.Y.ToString("0.00") + "dBm)";
            }

            return "M" + mi[0].iOrder.ToString() + ": " + label;
        }
        #endregion


        #region 准备扫描参数和扫描序列
        private void Prepare_Time_Sweep1(SweepParams p)
        {
            p.SweepType = SweepType.Time_Sweep;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_1, 1);

            p.TmeParam = new TimeSweepParam();

            if (App_Configure.Cnfgs.Cal_Use_Table)
            {
                p.TmeParam.P1 = this.settings.Tx + Tx_Tables.har_tx1.Offset(this.settings.F, this.settings.Tx, Tx_Tables.har_offset1);   
            } 
            else
                p.TmeParam.P1 = (float)App_Factors.har_tx1.ValueWithOffset(this.settings.F, this.settings.Tx);

            p.TmeParam.F1 = this.settings.F;
            p.TmeParam.Rx = this.settings.F;
            p.TmeParam.N = this.settings.Time_Points;

            csv_entries = new CsvReport_IVH_Entry[p.TmeParam.N];
        }

        private void Prepare_Time_Sweep2(SweepParams p)
        {
            p.SweepType = SweepType.Time_Sweep;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_2, 1);

            p.TmeParam = new TimeSweepParam();

            if (App_Configure.Cnfgs.Cal_Use_Table)
            {
                p.TmeParam.P2 = this.settings.Tx + Tx_Tables.har_tx2.Offset(this.settings.F, this.settings.Tx, Tx_Tables.har_offset2);
            }
            else
                p.TmeParam.P2 = (float)App_Factors.har_tx2.ValueWithOffset(this.settings.F, this.settings.Tx);

            p.TmeParam.F2 = this.settings.F;
            p.TmeParam.Rx = this.settings.F;
            p.TmeParam.N = this.settings.Time_Points;

            csv_entries = new CsvReport_IVH_Entry[p.TmeParam.N];
        }

        private void Prepare_Freq_Sweep1(SweepParams p)
        {
            p.SweepType = SweepType.Freq_Sweep ;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_1, 1);

            p.FrqParam = new FreqSweepParam();           
            p.FrqParam.Items1 = NewSweepItems1(this.settings.Freq_Step);

            csv_entries = new CsvReport_IVH_Entry[p.FrqParam.Items1.Length];
        }

        private void Prepare_Freq_Sweep2(SweepParams p)
        {
            p.SweepType = SweepType.Freq_Sweep;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_2, 1);

            p.FrqParam = new FreqSweepParam();            
            p.FrqParam.Items2 = NewSweepItems2(this.settings.Freq_Step);

            csv_entries = new CsvReport_IVH_Entry[p.FrqParam.Items2.Length];
        }

        /// <summary>
        /// 初始化扫描参数，仅填入公共部分
        /// </summary>
        /// <param name="p"></param>
        /// <param name="wndh"></param>
        /// <param name="rf"></param>
        /// <param name="count"></param>
        private void Init_Sweep_Params(SweepParams p, IntPtr wndh, RFInvolved rf, int count)
        {
            p.C = count;
            p.WndHandle = wndh;

            p.DevInfo = new DeviceInfo();
            p.DevInfo.RF_Addr1 = App_Configure.Cnfgs.ComAddr1;
            p.DevInfo.RF_Addr2 = App_Configure.Cnfgs.ComAddr2;
            p.DevInfo.Spectrum = App_Configure.Cnfgs.Spectrum;
            p.RFInvolved = rf;

            p.SpeParam = new SpectrumLib.Models.ScanModel();
            p.SpeParam.Att = settings.Att_Spc;
            p.SpeParam.Rbw = settings.Rbw_Spc;
            p.SpeParam.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;
            p.SpeParam.Continued = false;
            p.SpeParam.DeliSpe = SpectrumLib.Defines.CommonDef.SpectrumType.Deli_HARMONIC;
        }
        
        /// <summary>
        /// 根据频率步进，生成功放1扫描序列
        /// </summary>
        /// <returns></returns>
        private FreqSweepItem[] NewSweepItems1(float step)
        {
            FreqSweepItem item = null;

            List<FreqSweepItem> lst = new List<FreqSweepItem>();
            
            int i = 0;
            float f = 0.0f;
            do
            {
                f = App_Settings.sgn_1.Min_Freq + i * step;

                if (f >= App_Settings.sgn_1.Max_Freq)
                    f = App_Settings.sgn_1.Max_Freq;

                item = new FreqSweepItem();

                item.Tx1 = f;

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    item.P1 = this.settings.Tx + Tx_Tables.har_tx1.Offset(f, this.settings.Tx, Tx_Tables.har_offset1);
                }
                else
                    item.P1 = (float)App_Factors.har_tx1.ValueWithOffset(f, this.settings.Tx);

                item.Rx = f;

                lst.Add(item);

                i++;

            } while (f < App_Settings.sgn_1.Max_Freq);

            FreqSweepItem[] items = null;

            if (lst.Count > 0) {
               items = new FreqSweepItem[lst.Count];

               for (int j = 0; j < lst.Count; j++)
               {
                   items[j] = new FreqSweepItem();
                   lst[j].Clone(items[j]);
               }
            }

            return items;
        }

        /// <summary>
        /// 根据频率步进，生成功放2扫描序列
        /// </summary>
        /// <returns></returns>
        private FreqSweepItem[] NewSweepItems2(float step)
        {
            FreqSweepItem item = null;

            List<FreqSweepItem> lst = new List<FreqSweepItem>();

            int i = 0;
            float f = 0.0f;
            do
            {
                f = App_Settings.sgn_2.Min_Freq + i * step;

                if (f >= App_Settings.sgn_2.Max_Freq)
                    f = App_Settings.sgn_2.Max_Freq;

                item = new FreqSweepItem();

                item.Tx2 = f;

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    item.P2 = this.settings.Tx + Tx_Tables.har_tx2.Offset(f, this.settings.Tx, Tx_Tables.har_offset2);
                }
                else
                    item.P2 = (float)App_Factors.har_tx2.ValueWithOffset(f, this.settings.Tx);

                item.Rx = f;

                lst.Add(item);

                i++;

            } while (f < App_Settings.sgn_2.Max_Freq);

            FreqSweepItem[] items = null;

            if (lst.Count > 0)
            {
                items = new FreqSweepItem[lst.Count];

                for (int j = 0; j < lst.Count; j++)
                {
                    items[j] = new FreqSweepItem();
                    lst[j].Clone(items[j]);
                }
            }

            return items;
        }
        #endregion


        #region 保存报表文件
        /// <summary>
        /// 将当前测试数据保存为PDF格式的报表
        /// </summary>
        /// <param name="pdfFileName"></param>
        private void SaveHarPdf(string pdfFileName)
        {
            pdf_data.Desc  = App_Configure.Cnfgs.Mac_Desc;
            pdf_data.Modno = App_Configure.Cnfgs.Modno;
            pdf_data.Serno = App_Configure.Cnfgs.Serno;
            pdf_data.Opeor = App_Configure.Cnfgs.Opeor;

            pdf_data.Points_Num = PointsDone;

            pdf_data.Tx_out = this.settings.Tx;

            if (sweep_or_time == SweepType.Time_Sweep)
            {
                pdf_data.F_start = this.settings.F;
                pdf_data.F_stop = this.settings.F;

                pdf_data.Footer = "Harmonic Time Sweep";

            } else {
                if (rf_involved == RFInvolved.Rf_1)
                {
                    pdf_data.F_start = App_Settings.sgn_1.Min_Freq;
                    pdf_data.F_stop  = App_Settings.sgn_2.Max_Freq;

                } else {
                    pdf_data.F_start = App_Settings.sgn_2.Min_Freq;
                    pdf_data.F_stop =  App_Settings.sgn_2.Max_Freq;
                }

                pdf_data.Footer = "Harmonic Frequency Sweep";
            }

            pdf_data.Max_value = pltHar.Max_Y_Point(0).Y;

            pdf_data.Min_value = pltHar.Min_Y_Point(0).Y;

            pdf_data.Limit_value = this.settings.Limit;

            if (pdf_data.Min_value >= pdf_data.Limit_value)         
                pdf_data.Passed = "PASS";            
            else
                pdf_data.Passed = "FAIL";

            Bitmap bmp = new Bitmap(pltHar.Width,
                                    pltHar.Height,
                                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);

            g.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, pltHar.Width, pltHar.Height),
                                                    pltHar.BackColor,
                                                    pltHar.BackColor, 0f),
                                                    new Rectangle(0, 0, pltHar.Width, pltHar.Height));
            g.Dispose();

            pltHar.SaveImage(bmp);

            pdf_data.Image = bmp;

            pdf_har.Do_Print(pdfFileName, pdf_data);
        }


        /// <summary>
        /// 将当前测试数据保存为CSV格式的报表
        /// </summary>
        /// <param name="csvFileName"></param>
        private void SaveHarCsv(string csvFileName)
        {
            //准备CSV头部
            csv_header.Mac_Desc = App_Configure.Cnfgs.Mac_Desc;

            csv_header.Date_Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            csv_header.Swp_Type = sweep_or_time;           
            csv_header.Im_Schema = ImSchema.REV;
            csv_header.Im_Order = ImOrder.Im3;

            if (rf_involved == RFInvolved.Rf_1)
            {
                csv_header.Sweep_Start = App_Settings.sgn_1.Min_Freq;
                csv_header.Sweep_Stop = App_Settings.sgn_1.Max_Freq;

            }  else {
                csv_header.Sweep_Start = App_Settings.sgn_2.Min_Freq;
                csv_header.Sweep_Stop = App_Settings.sgn_2.Max_Freq;
            }
           
            csv_header.Point_Num = PointsDone;

            csv_header.Limit_Value = this.settings.Limit;

            csv_header.Y_Min_RL = this.settings.Min_Har;

            csv_header.Y_Max_RL = this.settings.Max_Har;

            //使用CSV头部和CSV数据项数组
            CsvReport.Save_Csv_IVH(csvFileName, csv_entries, csv_header);
        }

        /// <summary>
        /// 将当前测试数据保存为JPG格式的报表
        /// </summary>
        /// <param name="jpgFileName"></param>
        private void SaveHarJpg(string jpgFileName)
        {
            Image img = JpgReport.GetWindow(this.Handle);
            string strTitle = "";
            Graphics g = Graphics.FromImage(img);
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Near;
            strTitle = "Time: " + DateTime.Now.ToString();
            g.DrawImage(img, new Point(0, 0));
            g.DrawString(strTitle, new Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.White),
            new PointF(620, 70), drawFormat);
            img.Save(jpgFileName);

            this.Refresh();
        }
        #endregion


        #region CSV回放
        /// <summary>
        /// CSV报表头部，仅供回放使用
        /// </summary>
        private CsvReport_PIVH_Header csv_header_playback;

        /// <summary>
        /// CSV报表数据项，仅供回放使用
        /// </summary>
        private List<CsvReport_IVH_Entry> csv_entries_playback;
                
        /// <summary>
        /// CSV回放计数器
        /// </summary>
        private int csv_points_playback;

        /// <summary>
        /// CSV回放标志位
        /// </summary>
        private bool csv_playbacking;

        /// <summary>
        /// 将CSV格式的报表文件读入内存，进行回放操作
        /// </summary>
        /// <param name="csvFileName"></param>
        private void ReadHarCsv(string csvFileName)
        {
            if (!csv_playbacking)
            {
                csv_playbacking = true;

                csv_points_playback = 0;

                pltHar.Clear();
                
                CsvReport.Read_Csv_IVH(csvFileName, out csv_entries_playback, out csv_header_playback);

                if ((csv_entries_playback != null) && (csv_header_playback != null))
                {
                    if (csv_header_playback.Swp_Type == SweepType.Time_Sweep)
                    {
                        pltHar.SetMarkText(Fixed_MarkText);

                        pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq.gif");
                        pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif");
                        pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif");

                        pltHar.SetXStartStop(0, csv_header_playback.Point_Num);

                    } else {

                        pltHar.SetMarkText(Sweep_MarkText);

                        if ((csv_header_playback.Sweep_Start >= App_Settings.sgn_1.Min_Freq) &&
                            (csv_header_playback.Sweep_Start <= App_Settings.sgn_1.Max_Freq))
                        {
                            pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1.gif");
                            pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif");
                            pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");

                        } else {
                            pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2.gif");
                            pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif");
                            pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");
                        }

                        pltHar.SetXStartStop(csv_header_playback.Sweep_Start, csv_header_playback.Sweep_Stop);
                    }

                    pltHar.SetYStartStop(this.settings.Min_Har, this.settings.Max_Har);
                }

                //启动定时器
                timPlayback.Enabled = true;
            }
        }

        /// <summary>
        /// CSV回放定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void csvPlayback_Tick(object sender, EventArgs e)
        {
            if (csv_points_playback >= csv_entries_playback.Count)
            {
                //关闭定时器
                timPlayback.Enabled = false;

                csv_playbacking = false;
                
                return;
            }

            if (csv_header_playback.Swp_Type == SweepType.Time_Sweep)
            {
                sweep_points[0].X = csv_entries_playback[csv_points_playback].No;

                sweep_points[0].Y = csv_entries_playback[csv_points_playback].IVH_Value;

            }
            else
            {
                sweep_points[0].X = csv_entries_playback[csv_points_playback].F;

                sweep_points[0].Y = csv_entries_playback[csv_points_playback].IVH_Value;
            }

            pltHar.Add(sweep_points, 0, csv_points_playback);

            //更新回放计数
            csv_points_playback++;
        }
        #endregion


        #region 按钮事件
        private void pbxStart_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)            
                StartSweep();

            else
                BreakSweep(1000);
        }

        private void pbxCarrier1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                rf_involved = RFInvolved.Rf_1;
                sweep_or_time = SweepType.Freq_Sweep;
                pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif");
                pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");
                lblSweep.Text = "Carrier1 Frequncy Sweep";

                pltHar.SetXStartStop(App_Settings.sgn_1.Min_Freq * settings.Multiplier, App_Settings.sgn_1.Max_Freq * settings.Multiplier);
            }
        }

        private void pbxCarrier2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                rf_involved = RFInvolved.Rf_2;
                sweep_or_time = SweepType.Freq_Sweep;
                pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif");
                pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");
                lblSweep.Text = "Carrier2 Frequncy Sweep";

                pltHar.SetXStartStop(App_Settings.sgn_2.Min_Freq * settings.Multiplier, App_Settings.sgn_2.Max_Freq * settings.Multiplier);
            }
        }

        private void pbxFreq_MouseClick(object sender, MouseEventArgs e)
        {
            float f_value = 0.0f;

            if (!Sweeping)
            {
                sweep_or_time = SweepType.Time_Sweep;

                pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif");
                pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif");

                HarFreqForm fm = new HarFreqForm(this.settings.F);

                if (fm.ShowDialog() == DialogResult.OK)
                {
                    f_value = fm.Value;

                    if ((f_value >= App_Settings.sgn_1.Min_Freq) &&
                        (f_value <= App_Settings.sgn_1.Max_Freq))
                    {
                        settings.F = f_value;
                        lblF.Text = "F:" + f_value.ToString("0.0") + "MHz";
                        float f = f_value * 2;
                        lblSweep.Text = "Carrier1 Time Sweep (" + f.ToString("0.#") + "MHz)";

                    }
                    else if ((f_value >= App_Settings.sgn_2.Min_Freq) &&
                               (f_value <= App_Settings.sgn_2.Max_Freq))
                    {
                        settings.F = f_value;
                        lblF.Text = "F:" + f_value.ToString("0.0") + "MHz";
                        float f = f_value * 2;
                        lblSweep.Text = "Carrier2 Time Sweep (" + f.ToString("0.#") + "MHz)";

                    }
                    else
                        MessageBox.Show(this,"Frequency Must In [" +
                                        App_Settings.sgn_1.Min_Freq.ToString("0.0") + "~" +
                                        App_Settings.sgn_1.Max_Freq.ToString("0.0") + "]/[" +
                                        App_Settings.sgn_2.Min_Freq.ToString("0.0") + "~" +
                                        App_Settings.sgn_2.Max_Freq.ToString("0.0") + "]");
                }
                else
                {
                    float f = settings.F * 2;
                    lblSweep.Text = "Carrier2 Time Sweep (" + f.ToString("0.#") + "MHz)";
                }

                fm.Dispose();

                pltHar.SetXStartStop(0, this.settings.Time_Points);                
            }
        }

        private void pbxMark_MouseClick(object sender, MouseEventArgs e)
        {
            ShowMark(!MarkVisible);
        }

        private void pbxAutoscale_MouseClick(object sender, MouseEventArgs e)
        {
            EnableAutoscale(!AutoscaleEnable);
        }

        private void pbxSave_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                if (PointsDone > 0)
                {
                    HarSaveDataForm sdf = new HarSaveDataForm();

                    if (sdf.ShowDialog() == DialogResult.OK)
                    {
                        App_Configure.Cnfgs.Modno = sdf.textBox3.Text;
                        App_Configure.Cnfgs.Serno = sdf.textBox2.Text;
                        App_Configure.Cnfgs.Opeor = sdf.textBox1.Text;
                        try
                        {
                            if (sdf._csvFlag)
                                SaveHarCsv(sdf.Csv_FileName);

                            if (sdf._pdfFlag)
                                SaveHarPdf(sdf.Pdf_FileName);

                            if (sdf._jpgFlag)
                                SaveHarJpg(sdf.Jpg_FileName);

                        }
                        catch (Exception de)
                        {
                            //Log2File(de.StackTrace);
                        }

                        sdf.Dispose();
                    }
                }
            }
        }

        private void pbxRead_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                HarReadDataForm rdf = new HarReadDataForm();

                rdf.FillFiles(App_Configure.Cnfgs.Path_Rpt_Har + "\\csv");

                if (rdf.ShowDialog() == DialogResult.OK)
                    ReadHarCsv(App_Configure.Cnfgs.Path_Rpt_Har + "\\csv\\" + rdf.FileName);               

                rdf.Dispose();
            }
        }

        private void pbxSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                HarSettingForm frmHarSetting = new HarSettingForm(this.SweepObj, this.settings);

                if (frmHarSetting.ShowDialog() == DialogResult.OK)
                    UpdateWihtNewSettings();

                frmHarSetting.Dispose();
            }
        }

        /// <summary>
        /// 功能按钮，鼠标在控件上移动的事件，根据控件的Tag值，进行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuncBtn_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pbx;
            int tag;

            if (sender is PictureBox)
            {
                pbx = (sender as PictureBox);
                tag = int.Parse(pbx.Tag.ToString());

                switch (tag)
                {
                    case 1: { pbx.Image = ImagesManage.GetImage("harmonic", "start.gif"); break; }
                    case 2: { pbx.Image = ImagesManage.GetImage("harmonic", "carrier1.gif"); break; }
                    case 3: { pbx.Image = ImagesManage.GetImage("harmonic", "carrier2.gif"); break; }
                    case 4: { pbx.Image = ImagesManage.GetImage("harmonic", "freq.gif"); break; }
                    case 5: { pbx.Image = ImagesManage.GetImage("harmonic", "mark.gif"); break; }
                    case 6: { pbx.Image = ImagesManage.GetImage("harmonic", "autoscale.gif"); break; }
                    case 7: { pbx.Image = ImagesManage.GetImage("harmonic", "save.gif"); break; }
                    case 8: { pbx.Image = ImagesManage.GetImage("harmonic", "read.gif"); break; }
                    case 9: { pbx.Image = ImagesManage.GetImage("harmonic", "setting.gif"); break; }
                }
            }
        }

        /// <summary>
        /// 功能按钮，鼠标移出的事件，根据控件的Tag值，进行操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuncBtn_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pbx;
            int tag;

            if (sender is PictureBox)
            {
                pbx = (sender as PictureBox);
                tag = int.Parse(pbx.Tag.ToString());

                switch (tag)
                {
                    case 1:
                        {
                            if (!Sweeping)
                                pbx.Image = ImagesManage.GetImage("harmonic", "start_in.gif");

                            break;
                        }

                    case 2:
                        {
                            if ((sweep_or_time == SweepType.Freq_Sweep) && (rf_involved == RFInvolved.Rf_1))
                                pbx.Image = ImagesManage.GetImage("harmonic", "carrier1.gif"); 
                            else
                                pbx.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif"); 
                            
                            break;
                        }

                    case 3:
                        {
                            if ((sweep_or_time == SweepType.Freq_Sweep) && (rf_involved == RFInvolved.Rf_2))
                                pbx.Image = ImagesManage.GetImage("harmonic", "carrier2.gif");
                            else
                                pbx.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif"); 

                            break;
                        }

                    case 4:
                        {
                            if (sweep_or_time == SweepType.Time_Sweep)
                                pbx.Image = ImagesManage.GetImage("harmonic", "freq.gif");
                            else
                                pbx.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");
                            break;
                        }                    

                    case 5:
                        {
                            if (!MarkVisible)
                                pbx.Image = ImagesManage.GetImage("harmonic", "mark_in.gif");

                            break;
                        }

                    case 6:
                        {
                            if (!AutoscaleEnable)
                                pbx.Image = ImagesManage.GetImage("harmonic", "autoscale_in.gif");
                            
                            break;

                        }

                    case 7: { pbx.Image = ImagesManage.GetImage("harmonic", "save_in.gif"); break; }

                    case 8: { pbx.Image = ImagesManage.GetImage("harmonic", "read_in.gif"); break; }

                    case 9: { pbx.Image = ImagesManage.GetImage("harmonic", "setting_in.gif"); break; }
                }
            }
        }
        #endregion 


        #region 二次谐波模块的实例函数
        /// <summary>
        /// 使用新的设置值更新界面
        /// </summary>
        private void UpdateWihtNewSettings()
        {
            lblF.Text = "F:" + this.settings.F.ToString("0.0") + "MHz";
            lblTx.Text = "Tx:" + this.settings.Tx.ToString("0.0") + "dBm";

            pltHar.SetYStartStop(this.settings.Min_Har, this.settings.Max_Har);

            if (sweep_or_time == SweepType.Time_Sweep)
                pltHar.SetXStartStop(0, this.settings.Time_Points);

            if ((this.settings.F >= App_Settings.sgn_1.Min_Freq) &&
                (this.settings.F <= App_Settings.sgn_1.Max_Freq))
            {
                float f = settings.F * 2;
                lblSweep.Text = "Carrier2 Time Sweep (" + f.ToString("0.#") + "MHz)";
            }

            else
            {
                float f = settings.F * 2;
                lblSweep.Text = "Carrier2 Time Sweep (" + f.ToString("0.#") + "MHz)";
            }

            pltHar.SetLimitEnalbe(true, this.settings.Limit, Color.FromArgb(160, 245, 255));
        }

        /// <summary>
        /// 使用扫描参数对象，启动二次谐波分析
        /// </summary>
        /// <param name="sweepParams"></param>
        private void StartSweep()
        {
            bool power_too_large = false;

            if ((!Sweeping) && (!csv_playbacking))
            {
                Sweeping = true;
                MarkVisible = false;
                AutoscaleEnable = false;
                PointsDone = 0;
                csv_points_playback = 0;

                pbxStart.Image = ImagesManage.GetImage("harmonic", "start.gif");
                pbxMark.Image = ImagesManage.GetImage("harmonic", "mark_in.gif");
                pbxAutoscale.Image = ImagesManage.GetImage("harmonic", "autoscale_in.gif");

                pltHar.Clear();

                pltHar.SetYStartStop(this.settings.Min_Har, this.settings.Max_Har);

                sweep_params = new SweepParams();
                
                //准备扫描参数，并启动扫描
                if (sweep_or_time == SweepType.Freq_Sweep)
                {
                    pltHar.SetMarkText(Sweep_MarkText);

                    if (rf_involved == RFInvolved.Rf_1)
                    {
                        pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1.gif");
                        pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif");
                        pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");
                        pltHar.SetXStartStop(App_Settings.sgn_1.Min_Freq * settings.Multiplier, App_Settings.sgn_1.Max_Freq * settings.Multiplier);

                        if (this.settings.Tx > App_Settings.sgn_1.Max_Power || this.settings.Tx < App_Settings.sgn_1.Min_Power)
                            power_too_large = true;
                        Prepare_Freq_Sweep1(sweep_params);

                        //for (int i = 0; i < sweep_params.FrqParam.Items1.Length; i++)
                        //if (sweep_params.FrqParam.Items1[i].P1 > App_Settings.sgn_1.Max_Power)
                        //{
                        //    power_too_large = true;
                        //    break;
                        //}
                    }
                    else
                    {
                        pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2.gif");
                        pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif");
                        pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq_in.gif");
                        pltHar.SetXStartStop(App_Settings.sgn_2.Min_Freq * settings.Multiplier, App_Settings.sgn_2.Max_Freq * settings.Multiplier);

                        if (this.settings.Tx > App_Settings.sgn_2.Max_Power || this.settings.Tx < App_Settings.sgn_2.Min_Power)
                            power_too_large = true;
                        Prepare_Freq_Sweep2(sweep_params);

                        //for (int i = 0; i < sweep_params.FrqParam.Items2.Length; i++)
                        //if (sweep_params.FrqParam.Items2[i].P2 > App_Settings.sgn_2.Max_Power)
                        //{
                        //    power_too_large = true;
                        //    break;
                        //}
                    }

                } else {
                    pltHar.SetMarkText(Fixed_MarkText);

                    pbxFreq.Image = ImagesManage.GetImage("harmonic", "freq.gif");
                    pbxCarrier1.Image = ImagesManage.GetImage("harmonic", "carrier1_in.gif");
                    pbxCarrier2.Image = ImagesManage.GetImage("harmonic", "carrier2_in.gif");                   

                    pltHar.SetXStartStop(0, (this.settings.Time_Points));

                    if ((this.settings.F >= App_Settings.sgn_1.Min_Freq) &&
                        (this.settings.F <= App_Settings.sgn_1.Max_Freq))
                        rf_involved = RFInvolved.Rf_1;
                    else
                        rf_involved = RFInvolved.Rf_2;

                    if (rf_involved == RFInvolved.Rf_1)
                    {
                        if (this.settings.Tx > App_Settings.sgn_1.Max_Power || this.settings.Tx < App_Settings.sgn_1.Min_Power)
                            power_too_large = true;

                        Prepare_Time_Sweep1(sweep_params);

                        //if (sweep_params.TmeParam.P1 > App_Settings.sgn_1.Max_Power)
                        //    power_too_large = true;
                    }
                    else
                    {
                        if (this.settings.Tx > App_Settings.sgn_2.Max_Power || this.settings.Tx < App_Settings.sgn_2.Min_Power)
                            power_too_large = true;

                        Prepare_Time_Sweep2(sweep_params);

                        //if (sweep_params.TmeParam.P2 > App_Settings.sgn_2.Max_Power)                       
                        //    power_too_large = true;
                    }
                }

                if (power_too_large)
                {
                    Sweeping = false;

                    MessageBox.Show(this,"The carrier power setup is out of its range!");
                }
                else
                {
                    SweepObj.InitSweep();

                    SweepObj.Prepare(sweep_params, this.settings.Multiplier);

                    SweepObj.StartSweep();
                }
            }                
        }

        /// <summary>
        /// 中断分析循环，发生在用户切换到其他功能模块
        /// 或用户强行停止分析
        /// </summary>
        public void BreakSweep(int timeOut)
        {
            SweepObj.StopSweep(timeOut);
        }        
       
        private void EnableAutoscale(bool enable)
        {
            float dBmMin, dBmMax;

            if (enable)
            {
                if ((PointsDone > 0) || (csv_points_playback > 0))
                {
                    dBmMin = pltHar.Min_Y_Point(0).Y;

                    dBmMax = pltHar.Max_Y_Point(0).Y;

                    dBmMin = dBmMin - 3;

                    dBmMax = dBmMax + 3;

                    pltHar.SetYStartStop(dBmMin, dBmMax);

                    AutoscaleEnable = enable;
                }

            }
            else
            {
                pltHar.SetYStartStop(settings.Min_Har, settings.Max_Har);

                AutoscaleEnable = enable;
            }
        }
       
        private void ShowMark(bool visible)
        {
            pltHar.SetMarkVisible(0, visible);

            MarkVisible = visible;
        }

        /// <summary>
        /// 获取功放输出功率、频率，频谱分析的场强值，并应用补偿数据
        /// 处理数据，并更新界面
        /// </summary>
        private void SweepProcesing2()
        {
            float tx_out   = 0.0f;
            float rx_value = 0.0f;
                
            //获取功放状态、分析数据、异常信息
            SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);

            //处理扫频
            if (sweep_or_time == SweepType.Freq_Sweep)
            {
                //功放1扫频
                if (rf_involved == RFInvolved.Rf_1)
                {
                    sweep_points[0].X = ps1.Status2.Freq * settings.Multiplier;

                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps1.Status2.OutP + Tx_Tables.har_tx1_disp.Offset(ps1.Status2.Freq, ps1.Status2.OutP, Tx_Tables.har_offset1_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps1.Status2.Freq * this.settings.Multiplier, FuncModule.HAR);

                    } else {
                        tx_out = (float)App_Factors.har_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                        rx_value = (float)App_Factors.har_rx1.ValueWithOffset(ps1.Status2.Freq * this.settings.Multiplier, sr.dBmValue);
                    }

                    sweep_points[0].Y = (rx_value + this.settings.Rev);

                    lblF.Text  = "F:" + sweep_points[0].X.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                
                //功放2扫频
                } else {
                    sweep_points[0].X = ps2.Status2.Freq * settings.Multiplier;

                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps2.Status2.OutP + Tx_Tables.har_tx2_disp.Offset(ps2.Status2.Freq, ps2.Status2.OutP, Tx_Tables.har_offset2_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps2.Status2.Freq * this.settings.Multiplier, FuncModule.HAR);
                    
                    } else {
                        tx_out = (float)App_Factors.har_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                        rx_value = (float)App_Factors.har_rx2.ValueWithOffset(ps2.Status2.Freq * this.settings.Multiplier, sr.dBmValue);
                    }

                    sweep_points[0].Y = (rx_value + this.settings.Rev);

                    lblF.Text = "F:" + sweep_points[0].X.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                }

                lblRx.Text = "Rx:" + sr.dBmValue.ToString("0.0") + "dBm";
                lblNoise.Text = "Noise:" + sr.dBmNosie.ToString("0.0") + "dBm";
                pltHar.Add(sweep_points, 0, PointsDone);

            //处理扫时
            } else {
                sweep_points[0].X = PointsDone;

                //功放1扫时
                if (rf_involved == RFInvolved.Rf_1)
                {
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps1.Status2.OutP + Tx_Tables.har_tx1_disp.Offset(ps1.Status2.Freq, ps1.Status2.OutP, Tx_Tables.har_offset1_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps1.Status2.Freq * this.settings.Multiplier, FuncModule.HAR);
                    
                    } else {
                        tx_out = (float)App_Factors.har_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                        rx_value = (float)App_Factors.har_rx1.ValueWithOffset(ps1.Status2.Freq * this.settings.Multiplier, sr.dBmValue);
                    }

                    sweep_points[0].Y = (rx_value + this.settings.Rev);

                    lblF.Text = "F:" + ps1.Status2.Freq.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                
                //功放2扫时
                } else  {
                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps2.Status2.OutP + Tx_Tables.har_tx2_disp.Offset(ps2.Status2.Freq, ps2.Status2.OutP, Tx_Tables.har_offset2_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps2.Status2.Freq, FuncModule.HAR);
                   
                    }  else {
                        tx_out = (float)App_Factors.har_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                        rx_value = (float)App_Factors.har_rx2.ValueWithOffset(ps2.Status2.Freq, sr.dBmValue);
                    }

                    sweep_points[0].Y = (rx_value + this.settings.Rev);

                    lblF.Text = "F:" + ps2.Status2.Freq.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                }

                lblRx.Text = "Rx:" + rx_value.ToString("0.0") + "dBm";
                lblNoise.Text = "Noise:" + sr.dBmNosie.ToString("0.0") + "dBm";
                pltHar.Add(sweep_points, 0, PointsDone);
            }
        }

        /// <summary>
        /// 处理扫描结果，并绘制曲线
        /// </summary>
        private void SweepProcessing()
        {   
            SweepProcesing2();

            #region 准备CSV数据项，并添加到数据项数组中
            CsvReport_IVH_Entry entry = new CsvReport_IVH_Entry();

            entry.No = PointsDone;

            entry.P = this.settings.Tx;

            if (sweep_or_time == SweepType.Freq_Sweep)
                entry.F = sweep_points[0].X;
            else
                entry.F = this.settings.F;

            entry.IVH_Value = sweep_points[0].Y;

            entry.Noise = sr.dBmNosie;

            entry.Rl = 0.0f;

            //将数据项添加到数组
            csv_entries[PointsDone] = entry;
            #endregion


            #region 更新扫描计数器
            PointsDone++;
            #endregion
        }
        #endregion


        #region 窗体的消息处理函数
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //完成一趟操作
                case MessageID.ISO_SWEEP_DONE:
                    {
                        Sweeping = false;

                        pbxStart.Image = ImagesManage.GetImage("harmonic", "start_in.gif");

                        break;
                    }

                //完成单点扫描
                case MessageID.ISO_SUCCED:
                    {
                        SweepProcessing();

                        break;
                    }

                //功放操作错误
                case MessageID.RF_ERROR:
                    {
                        SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);

                        MessageBox.Show(this,rfr_errors1.ToString() + "\n\r" + rfr_errors2.ToString());

                        BreakSweep(1000);
                        Sweeping = false;
                        pbxStart.Image = ImagesManage.GetImage("harmonic", "start_in.gif");

                        break;
                    }

                //频谱分析错误
                case MessageID.SPECTRUM_ERROR:
                    {
                        MessageBox.Show(this,"Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");
                        BreakSweep(1000);
                        Sweeping = false;
                        pbxStart.Image = ImagesManage.GetImage("harmonic", "start_in.gif");
                        break;
                    }

                //频谱分析成功
                case MessageID.SPECTRUEME_SUCCED:
                    {
                        SweepObj.Spectrum_Succed();

                        break;
                    }

                //功放操作成功
                case MessageID.RF_SUCCED_ALL:
                    {
                        if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr1)
                            SweepObj.Power1_Succed();

                        else if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr2)
                            SweepObj.Power2_Succed();

                        break;
                    }

                default:
                    {
                        base.WndProc(ref m);
                        break;
                    }
            }
        }
        #endregion
    }
}