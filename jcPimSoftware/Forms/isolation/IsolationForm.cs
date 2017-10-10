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
    public partial class IsolationForm : Form, ISweep
    {
       
        #region �����ģ���ʵ������
        /// <summary>
        /// ����������ɹ��캯������
        /// </summary>
        private Settings_Iso settings;

        /// <summary>
        /// ����ȷ������󣬽��ڹ��캯��������һ��
        /// </summary>
        private Iso_Sweep SweepObj;
        
        /// <summary>
        /// ɨ���������ÿ������ɨ�趼�����¹��� 
        /// </summary>
        private SweepParams sweep_params;

        /// <summary>
        /// ��ʶɨ��ѭ���Ѿ�����
        /// </summary>
        private bool Sweeping = false;

        /// <summary>
        /// ��ʶ���ڽ��е�ɨ�裬�Ѿ���ɵĵ���
        /// </summary>
        private int PointsDone;

        /// <summary>
        /// ��ǰ�������Ĺ���ָʾ��
        /// </summary>
        private RFInvolved rf_involved = RFInvolved.Rf_1;

        /// <summary>
        /// ָʾ����ɨƵ TRUE����������ɨʱ FALSE
        /// </summary>
        private SweepType sweep_or_time = SweepType.Time_Sweep;

        /// <summary>
        /// ����Autoscale��ʹ��״̬�������˸ı�Autoscale��ť����ͼ
        /// </summary>
        private bool AutoscaleEnable;

        /// <summary>
        /// ����MARK�Ŀɼ��ԣ������˸ı�Mark��ť����ͼ
        /// </summary>
        private bool MarkVisible;

        /// <summary>
        /// ���ɨ��������ɨ������У����ϱ�������ֵ
        /// </summary>
        PointF[] sweep_points = new PointF[1];

        /// <summary>
        /// ��Ŵ��Զ�У׼�����б�������õ���
        /// </summary>
        private Auto_CAL_Item cal_item;
        
        /// <summary>
        /// CSV����ͷ��
        /// </summary>
        private CsvReport_PIVH_Header csv_header = new CsvReport_PIVH_Header();

        /// <summary>
        /// CSV����������
        /// </summary>
        private CsvReport_IVH_Entry[] csv_entries;

        /// <summary>
        /// PDF��������
        /// </summary>
        private PdfReport_Data pdf_data = new PdfReport_Data();

        /// <summary>
        /// PDF�������
        /// </summary>
        private PdfReport_Iso pdf_iso = new PdfReport_Iso();
        #endregion


        #region �豸״̬������ɨ��ṹ������sweepobj.CloneReference��ֵ
        private PowerStatus ps1;
        private PowerStatus ps2;
        private SweepResult sr;
        private RFErrors rfr_errors1;
        private RFErrors rfr_errors2;
        #endregion


        #region ����Ĺ��������
        public IsolationForm()
        {
            //����ɨ�����
            SweepObj = new Iso_Sweep();

            //��������ģ�����ö���
            this.settings = new Settings_Iso("");
            
            //��Ĭ�ϸ����ģ�����ö������������ֵ
            App_Settings.iso.Clone(this.settings);

            if (this.settings.Min_Iso >= this.settings.Max_Iso)
                this.settings.Min_Iso = this.settings.Max_Iso - 1;

            InitializeComponent();

            pltIso.Resume();

            pltIso.SetSampling(false);

            this.TopLevel = false;
            this.ShowInTaskbar = false;
            this.Dock = DockStyle.Fill;
        }

        private void IsolationForm_Load(object sender, EventArgs e)
        {
            sweep_params = new SweepParams();
            Prepare_Time_Sweep1(sweep_params);
            pbxFreq.Image = ImagesManage.GetImage("isolation", "freq.gif");

            pltIso.SetLimitEnalbe(true, this.settings.Limit, Color.FromArgb(160, 245, 255));

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
                         mi[i].fPoint.Y.ToString("0.00") + "dB)";
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

                label = label + "(" + mi[i].fPoint.Y.ToString("0.00") + "dB)";
            }

            return "M" + mi[0].iOrder.ToString() + ": " + label;
        }
        #endregion


        #region ׼��ɨ�������ɨ������
        private void Prepare_Time_Sweep1(SweepParams p)
        {
            p.SweepType = SweepType.Time_Sweep;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_1, 1);

            p.TmeParam = new TimeSweepParam();

            if (App_Configure.Cnfgs.Cal_Use_Table)
            {
                p.TmeParam.P1 = this.settings.Tx + Tx_Tables.iso_tx1.Offset(this.settings.F, this.settings.Tx, Tx_Tables.iso_offset1);
                //Log.WriteLog("Tx_Tables.iso_tx1.Offset=" + Tx_Tables.iso_offset1.ToString(), Log.EFunctionType.ISOLATION);
            } 
            else
                p.TmeParam.P1 = (float)App_Factors.iso_tx1.ValueWithOffset(this.settings.F, this.settings.Tx);

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
                p.TmeParam.P2 = this.settings.Tx + Tx_Tables.iso_tx2.Offset(this.settings.F, this.settings.Tx, Tx_Tables.iso_offset2);
            }
            else
                p.TmeParam.P2 = (float)App_Factors.iso_tx2.ValueWithOffset(this.settings.F, this.settings.Tx);

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
        /// ��ʼ��ɨ������������빫������
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
            p.SpeParam.DeliSpe = SpectrumLib.Defines.CommonDef.SpectrumType.Deli_ISOLATION;
        }
        
        /// <summary>
        /// ����Ƶ�ʲ��������ɹ���1ɨ������
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
                    item.P1 = this.settings.Tx + Tx_Tables.iso_tx1.Offset(f, this.settings.Tx, Tx_Tables.iso_offset1);
                }
                else
                    item.P1 = (float)App_Factors.iso_tx1.ValueWithOffset(f, this.settings.Tx);

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
        /// ����Ƶ�ʲ��������ɹ���2ɨ������
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
                    item.P2 = this.settings.Tx + Tx_Tables.iso_tx2.Offset(f, this.settings.Tx, Tx_Tables.iso_offset2);
                }
                else
                    item.P2 = (float)App_Factors.iso_tx2.ValueWithOffset(f, this.settings.Tx);

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


        #region ���汨���ļ�
        /// <summary>
        /// ����ǰ�������ݱ���ΪPDF��ʽ�ı���
        /// </summary>
        /// <param name="pdfFileName"></param>
        private void SaveIsoPdf(string pdfFileName)
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

                pdf_data.Footer = "Isolation Time Sweep";

            } else {
                if (rf_involved == RFInvolved.Rf_1)
                {
                    pdf_data.F_start = App_Settings.sgn_1.Min_Freq;
                    pdf_data.F_stop  = App_Settings.sgn_2.Max_Freq;

                } else {
                    pdf_data.F_start = App_Settings.sgn_2.Min_Freq;
                    pdf_data.F_stop =  App_Settings.sgn_2.Max_Freq;
                }

                pdf_data.Footer = "Isolation Frequency Sweep";
            }

            pdf_data.Max_value = pltIso.Max_Y_Point(0).Y;

            pdf_data.Min_value = pltIso.Min_Y_Point(0).Y;

            pdf_data.Limit_value = this.settings.Limit;

            if (pdf_data.Min_value >= pdf_data.Limit_value)         
                pdf_data.Passed = "PASS";            
            else
                pdf_data.Passed = "FAIL";

            Bitmap bmp = new Bitmap(pltIso.Width,
                                    pltIso.Height,
                                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);

            g.FillRectangle(new LinearGradientBrush(new Rectangle(0, 0, pltIso.Width, pltIso.Height),
                                                    pltIso.BackColor,
                                                    pltIso.BackColor, 0f),
                                                    new Rectangle(0, 0, pltIso.Width, pltIso.Height));
            g.Dispose();

            pltIso.SaveImage(bmp);

            pdf_data.Image = bmp;

            pdf_iso.Do_Print(pdfFileName, pdf_data);
        }


        /// <summary>
        /// ����ǰ�������ݱ���ΪCSV��ʽ�ı���
        /// </summary>
        /// <param name="csvFileName"></param>
        private void SaveIsoCsv(string csvFileName)
        {
            //׼��CSVͷ��
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

            csv_header.Y_Min_RL = this.settings.Min_Iso;

            csv_header.Y_Max_RL = this.settings.Max_Iso;

            //ʹ��CSVͷ����CSV����������
            CsvReport.Save_Csv_IVH(csvFileName, csv_entries, csv_header);
        }
        

        /// <summary>
        /// ����ǰ�������ݱ���ΪJPG��ʽ�ı���
        /// </summary>
        /// <param name="jpgFileName"></param>
        private void SaveIsoJpg(string jpgFileName)
        {
            string strTitle = "";
            Image img = JpgReport.GetWindow(this.Handle);
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


        #region CSV�ط�
        /// <summary>
        /// CSV����ͷ���������ط�ʹ��
        /// </summary>
        private CsvReport_PIVH_Header csv_header_playback;

        /// <summary>
        /// CSV��������������ط�ʹ��
        /// </summary>
        private List<CsvReport_IVH_Entry> csv_entries_playback;
                
        /// <summary>
        /// CSV�طż�����
        /// </summary>
        private int csv_points_playback;

        /// <summary>
        /// CSV�طű�־λ
        /// </summary>
        private bool csv_playbacking;

        /// <summary>
        /// ��CSV��ʽ�ı����ļ������ڴ棬���лطŲ���
        /// </summary>
        /// <param name="csvFileName"></param>
        private void ReadIsoCsv(string csvFileName)
        {
            if (!csv_playbacking)
            {
                csv_playbacking = true;

                csv_points_playback = 0;

                pltIso.Clear();

                CsvReport.Read_Csv_IVH(csvFileName, out csv_entries_playback, out csv_header_playback);

                if ((csv_entries_playback != null) && (csv_header_playback != null))
                {
                    if (csv_header_playback.Swp_Type == SweepType.Time_Sweep)
                    {
                        pltIso.SetMarkText(Fixed_MarkText);

                        pbxFreq.Image = ImagesManage.GetImage("isolation", "freq.gif");
                        pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif");
                        pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif");

                        pltIso.SetXStartStop(0, csv_header_playback.Point_Num);

                    } else {

                        pltIso.SetMarkText(Sweep_MarkText);

                        if ((csv_header_playback.Sweep_Start >= App_Settings.sgn_1.Min_Freq) &&
                            (csv_header_playback.Sweep_Start <= App_Settings.sgn_1.Max_Freq))
                        {
                            pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1.gif");
                            pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif");
                            pbxFreq.Image = ImagesManage.GetImage("isolation", "freq_in.gif");

                        } else {
                            pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2.gif");
                            pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif");
                            pbxFreq.Image = ImagesManage.GetImage("isolation", "freq_in.gif");
                        }

                        pltIso.SetXStartStop(csv_header_playback.Sweep_Start, csv_header_playback.Sweep_Stop);
                    }

                    pltIso.SetYStartStop(this.settings.Min_Iso, this.settings.Max_Iso);
                }

                //������ʱ��
                timPlayback.Enabled = true;
            }
        }

        /// <summary>
        /// CSV�طŶ�ʱ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void csvPlayback_Tick(object sender, EventArgs e)
        {
            if (csv_points_playback >= csv_entries_playback.Count)
            {
                //�رն�ʱ��
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

            pltIso.Add(sweep_points, 0, csv_points_playback);

            //���»طż���
            csv_points_playback++;
        }
        #endregion


        #region ��ť�¼�
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
                pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif");
                pbxFreq.Image = ImagesManage.GetImage("isolation", "freq_in.gif");
                lblSweep.Text = "Carrier1 Frequncy Sweep";

                pltIso.SetXStartStop(App_Settings.sgn_1.Min_Freq, App_Settings.sgn_1.Max_Freq);
            }
        }

        private void pbxCarrier2_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                rf_involved = RFInvolved.Rf_2;
                sweep_or_time = SweepType.Freq_Sweep;
                pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif");
                pbxFreq.Image = ImagesManage.GetImage("isolation", "freq_in.gif");
                lblSweep.Text = "Carrier2 Frequncy Sweep";

                pltIso.SetXStartStop(App_Settings.sgn_2.Min_Freq, App_Settings.sgn_2.Max_Freq);
            }
        }

        private void pbxFreq_MouseClick(object sender, MouseEventArgs e)
        {
            float f_value = 0.0f;

            if (!Sweeping)
            {
                sweep_or_time = SweepType.Time_Sweep;

                pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif");
                pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif");

                IsoFreqForm fm = new IsoFreqForm(this.settings.F);

                if (fm.ShowDialog() == DialogResult.OK)
                {
                    f_value = fm.Value;

                    if ((f_value >= App_Settings.sgn_1.Min_Freq) &&
                        (f_value <= App_Settings.sgn_1.Max_Freq))
                    {
                        settings.F = f_value;
                        lblF.Text = "F:" + f_value.ToString("0.0") + "MHz";
                        lblSweep.Text = "Carrier1 Time Sweep";

                    } else if ((f_value >= App_Settings.sgn_2.Min_Freq) &&
                               (f_value <= App_Settings.sgn_2.Max_Freq))
                    {
                        settings.F = f_value;
                        lblF.Text = "F:" + f_value.ToString("0.0") + "MHz";
                        lblSweep.Text = "Carrier2 Time Sweep";

                    } else
                        MessageBox.Show(this,"Frequency Must In [" +
                                        App_Settings.sgn_1.Min_Freq.ToString("0.0") + "~" +
                                        App_Settings.sgn_1.Max_Freq.ToString("0.0") + "]/[" +
                                        App_Settings.sgn_2.Min_Freq.ToString("0.0") + "~" +
                                        App_Settings.sgn_2.Max_Freq.ToString("0.0") + "]");
                }

                fm.Dispose();

                //pltIso.SetXStartStop(0, this.settings.Time_Points - 1);      
                pltIso.SetXStartStop(0, this.settings.Time_Points);  
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
                    IsoSaveDataForm sdf = new IsoSaveDataForm();

                    if (sdf.ShowDialog() == DialogResult.OK)
                    {
                        App_Configure.Cnfgs.Modno = sdf.textBox3.Text;
                        App_Configure.Cnfgs.Serno = sdf.textBox2.Text;
                        App_Configure.Cnfgs.Opeor = sdf.textBox1.Text;
                        try
                        {
                            if (sdf._csvFlag)
                                SaveIsoCsv(sdf.Csv_FileName);

                            if (sdf._pdfFlag)
                                SaveIsoPdf(sdf.Pdf_FileName);

                            if (sdf._jpgFlag)
                                SaveIsoJpg(sdf.Jpg_FileName);
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
                IsoReadDataForm rdf = new IsoReadDataForm();

                rdf.FillFiles(App_Configure.Cnfgs.Path_Rpt_Iso + "\\csv");

                if (rdf.ShowDialog() == DialogResult.OK)
                    ReadIsoCsv(App_Configure.Cnfgs.Path_Rpt_Iso + "\\csv\\" + rdf.FileName);               

                rdf.Dispose();
            }
        }

        private void pbxSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Sweeping)
            {
                IsoSettingForm frmIsoSetting = new IsoSettingForm(this.SweepObj, this.settings);

                if (frmIsoSetting.ShowDialog() == DialogResult.OK)
                    UpdateWihtNewSettings();

                frmIsoSetting.Dispose();

                if (IsoSettingForm.bEnableCAL_RF1)
                    lblCAL1.Text = "CAL-1:ON";
                else
                    lblCAL1.Text = "CAL-1:OFF";

                if (IsoSettingForm.bEnableCAL_RF2)
                    lblCAL2.Text = "CAL-2:ON";
                else
                    lblCAL2.Text = "CAL-2:OFF";
            }
        }

        /// <summary>
        /// ���ܰ�ť������ڿؼ����ƶ����¼������ݿؼ���Tagֵ�����в���
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
                    case 1: { pbx.Image = ImagesManage.GetImage("isolation", "start.gif"); break; }
                    case 2: { pbx.Image = ImagesManage.GetImage("isolation", "carrier1.gif"); break; }
                    case 3: { pbx.Image = ImagesManage.GetImage("isolation", "carrier2.gif"); break; }
                    case 4: { pbx.Image = ImagesManage.GetImage("isolation", "freq.gif"); break; }
                    case 5: { pbx.Image = ImagesManage.GetImage("isolation", "mark.gif"); break; }
                    case 6: { pbx.Image = ImagesManage.GetImage("isolation", "autoscale.gif"); break; }
                    case 7: { pbx.Image = ImagesManage.GetImage("isolation", "save.gif"); break; }
                    case 8: { pbx.Image = ImagesManage.GetImage("isolation", "read.gif"); break; }
                    case 9: { pbx.Image = ImagesManage.GetImage("isolation", "setting.gif"); break; }
                }
            }
        }

        /// <summary>
        /// ���ܰ�ť������Ƴ����¼������ݿؼ���Tagֵ�����в���
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
                                pbx.Image = ImagesManage.GetImage("isolation", "start_in.gif");

                            break;
                        }

                    case 2:
                        {
                            if ((sweep_or_time == SweepType.Freq_Sweep) && (rf_involved == RFInvolved.Rf_1))
                                pbx.Image = ImagesManage.GetImage("isolation", "carrier1.gif"); 
                            else
                                pbx.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif"); 
                            
                            break;
                        }

                    case 3:
                        {
                            if ((sweep_or_time == SweepType.Freq_Sweep) && (rf_involved == RFInvolved.Rf_2))
                                pbx.Image = ImagesManage.GetImage("isolation", "carrier2.gif");
                            else
                                pbx.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif"); 

                            break;
                        }

                    case 4:
                        {
                            if (sweep_or_time == SweepType.Time_Sweep)
                                pbx.Image = ImagesManage.GetImage("isolation", "freq.gif");
                            else
                                pbx.Image = ImagesManage.GetImage("isolation", "freq_in.gif");
                            break;
                        }                    

                    case 5:
                        {
                            if (!MarkVisible)
                                pbx.Image = ImagesManage.GetImage("isolation", "mark_in.gif");

                            break;
                        }

                    case 6:
                        {
                            if (!AutoscaleEnable)
                                pbx.Image = ImagesManage.GetImage("isolation", "autoscale_in.gif");
                            
                            break;

                        }

                    case 7: { pbx.Image = ImagesManage.GetImage("isolation", "save_in.gif"); break; }

                    case 8: { pbx.Image = ImagesManage.GetImage("isolation", "read_in.gif"); break; }

                    case 9: { pbx.Image = ImagesManage.GetImage("isolation", "setting_in.gif"); break; }
                }
            }
        }
        #endregion 


        #region �����ģ���ʵ������
        /// <summary>
        /// ʹ���µ�����ֵ���½���
        /// </summary>
        private void UpdateWihtNewSettings()
        {
            lblF.Text = "F:" + this.settings.F.ToString("0.0") + "MHz";
            lblTx.Text = "Tx:" + this.settings.Tx.ToString("0.0") + "dBm";

            pltIso.SetYStartStop(this.settings.Min_Iso, this.settings.Max_Iso);

            if (sweep_or_time == SweepType.Time_Sweep)
                //pltIso.SetXStartStop(0, this.settings.Time_Points - 1);
                pltIso.SetXStartStop(0, this.settings.Time_Points);

            if ((this.settings.F >= App_Settings.sgn_1.Min_Freq) &&
                (this.settings.F <= App_Settings.sgn_1.Max_Freq))           
                
                lblSweep.Text = "Carrier1 Time Sweep";
            
            else           
                lblSweep.Text = "Carrier2 Time Sweep";

            pltIso.SetLimitEnalbe(true, this.settings.Limit, Color.FromArgb(160, 245, 255));
        }

        /// <summary>
        /// ʹ��ɨ�����������������ȷ���
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

                pbxStart.Image = ImagesManage.GetImage("isolation", "start.gif");
                pbxMark.Image = ImagesManage.GetImage("isolation", "mark_in.gif");
                pbxAutoscale.Image = ImagesManage.GetImage("isolation", "autoscale_in.gif");

                pltIso.Clear();

                pltIso.SetYStartStop(this.settings.Min_Iso, this.settings.Max_Iso);

                sweep_params = new SweepParams();
                
                //׼��ɨ�������������ɨ��
                if (sweep_or_time == SweepType.Freq_Sweep)
                {
                    pltIso.SetMarkText(Sweep_MarkText);

                    if (rf_involved == RFInvolved.Rf_1)
                    {
                        pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1.gif");
                        pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif");
                        pbxFreq.Image = ImagesManage.GetImage("isolation", "freq_in.gif");

                        if (!IsoSettingForm.bEnableCAL_RF1)
                        {
                            MessageBox.Show(this, "Please calibrate carrier 1!");
                            pbxStart.Image = ImagesManage.GetImage("isolation", "start_in.gif");
                            Sweeping = false;
                            return;
                        }

                        pltIso.SetXStartStop(App_Settings.sgn_1.Min_Freq, App_Settings.sgn_1.Max_Freq);

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
                        if (!IsoSettingForm.bEnableCAL_RF2)
                        {
                            MessageBox.Show(this, "Please calibrate carrier 2!");
                            pbxStart.Image = ImagesManage.GetImage("isolation", "start_in.gif");
                            Sweeping = false;
                            return;
                        }

                        pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2.gif");
                        pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif");
                        pbxFreq.Image = ImagesManage.GetImage("isolation", "freq_in.gif");
                        pltIso.SetXStartStop(App_Settings.sgn_2.Min_Freq, App_Settings.sgn_2.Max_Freq);

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
                    pltIso.SetMarkText(Fixed_MarkText);

                    pbxFreq.Image = ImagesManage.GetImage("isolation", "freq.gif");
                    pbxCarrier1.Image = ImagesManage.GetImage("isolation", "carrier1_in.gif");
                    pbxCarrier2.Image = ImagesManage.GetImage("isolation", "carrier2_in.gif");                   

                    //pltIso.SetXStartStop(0, (this.settings.Time_Points - 1));
                    pltIso.SetXStartStop(0, (this.settings.Time_Points));

                    if ((this.settings.F >= App_Settings.sgn_1.Min_Freq) &&
                        (this.settings.F <= App_Settings.sgn_1.Max_Freq))
                    {
                        if (!IsoSettingForm.bEnableCAL_RF1)
                        {
                            MessageBox.Show(this, "Please calibrate carrier 1!");
                            pbxStart.Image = ImagesManage.GetImage("isolation", "start_in.gif");
                            Sweeping = false;
                            return;
                        }

                        rf_involved = RFInvolved.Rf_1;
                    }
                    else
                    {
                        if (!IsoSettingForm.bEnableCAL_RF2)
                        {
                            MessageBox.Show(this, "Please calibrate carrier 2!");
                            pbxStart.Image = ImagesManage.GetImage("isolation", "start_in.gif");
                            Sweeping = false;
                            return;
                        }

                        rf_involved = RFInvolved.Rf_2;
                    }

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

                    SweepObj.Prepare(sweep_params);

                    SweepObj.StartSweep();
                }
            }                
        }

        /// <summary>
        /// �жϷ���ѭ�����������û��л�����������ģ��
        /// ���û�ǿ��ֹͣ����
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
                    dBmMin = pltIso.Min_Y_Point(0).Y;

                    dBmMax = pltIso.Max_Y_Point(0).Y;

                    dBmMin = dBmMin - 3;

                    dBmMax = dBmMax + 3;

                    pltIso.SetYStartStop(dBmMin, dBmMax);

                    AutoscaleEnable = enable;
                }

            }
            else
            {
                pltIso.SetYStartStop(settings.Min_Iso, settings.Max_Iso);

                AutoscaleEnable = enable;
            }
        }
       
        private void ShowMark(bool visible)
        {
            pltIso.SetMarkVisible(0, visible);

            MarkVisible = visible;
        }

        /// <summary>
        /// ��ȡ����������ʡ�Ƶ�ʣ�Ƶ�׷����ĳ�ǿֵ����Ӧ�ò�������
        /// �������ݣ������½���
        /// </summary>
        private void SweepProcesing2()
        {
            float tx_out   = 0.0f;
            float rx_value = 0.0f;
            Auto_CAL_Items Cal_Carrier;
           
            //��ȡ����״̬���������ݡ��쳣��Ϣ
            SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);

            //����ɨƵ
            if (sweep_or_time == SweepType.Freq_Sweep)
            {
                //����1ɨƵ
                if (rf_involved == RFInvolved.Rf_1)
                {
                    sweep_points[0].X = ps1.Status2.Freq;

                    Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_1);
                    cal_item = Cal_Carrier.GetItem(ps1.Status2.Freq);

                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps1.Status2.OutP + Tx_Tables.iso_tx1_disp.Offset(ps1.Status2.Freq, ps1.Status2.OutP, Tx_Tables.har_offset1_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps1.Status2.Freq, FuncModule.ISO);

                    } else {
                        tx_out = (float)App_Factors.iso_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                        rx_value = (float)App_Factors.iso_rx1.ValueWithOffset(ps1.Status2.Freq, sr.dBmValue);
                    }

                    sweep_points[0].Y = (tx_out - rx_value) -
                                        (cal_item.Tx0 - cal_item.Rx0) + cal_item.RL0; // -this.settings.Offset;

                    lblF.Text  = "F:" + sweep_points[0].X.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                
                //����2ɨƵ
                } else {
                    sweep_points[0].X = ps2.Status2.Freq;

                    Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_2);
                    cal_item = Cal_Carrier.GetItem(ps2.Status2.Freq);

                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps2.Status2.OutP + Tx_Tables.iso_tx2_disp.Offset(ps2.Status2.Freq, ps2.Status2.OutP, Tx_Tables.iso_offset2_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps2.Status2.Freq, FuncModule.ISO);
                    
                    } else {
                        tx_out = (float)App_Factors.iso_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                        rx_value = (float)App_Factors.iso_rx2.ValueWithOffset(ps2.Status2.Freq, sr.dBmValue);
                    }

                    sweep_points[0].Y = (tx_out - rx_value) -
                                        (cal_item.Tx0 - cal_item.Rx0) + cal_item.RL0; //- this.settings.Offset;

                    lblF.Text = "F:" + sweep_points[0].X.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                }

                lblRx.Text = "Rx:" + sr.dBmValue.ToString("0.0") + "dBm";
                lblNoise.Text = "Noise:" + sr.dBmNosie.ToString("0.0") + "dBm";
                pltIso.Add(sweep_points, 0, PointsDone);

            //����ɨʱ
            } else {
                sweep_points[0].X = PointsDone;

                //����1ɨʱ
                if (rf_involved == RFInvolved.Rf_1)
                {
                    Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_1);
                    cal_item = Cal_Carrier.GetItem(ps1.Status2.Freq);

                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps1.Status2.OutP + Tx_Tables.iso_tx1_disp.Offset(ps1.Status2.Freq, ps1.Status2.OutP, Tx_Tables.iso_offset1_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps1.Status2.Freq, FuncModule.ISO);
                    
                    } else {
                        tx_out = (float)App_Factors.iso_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                        rx_value = (float)App_Factors.iso_rx1.ValueWithOffset(ps1.Status2.Freq, sr.dBmValue);
                    }

                    sweep_points[0].Y = (tx_out - rx_value) -
                                        (cal_item.Tx0 - cal_item.Rx0) + cal_item.RL0; //- this.settings.Offset;

                    lblF.Text = "F:" + ps1.Status2.Freq.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                
                //����2ɨʱ
                } else  {
                    Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_2);
                    cal_item = Cal_Carrier.GetItem(ps2.Status2.Freq);

                    if (App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        tx_out = ps2.Status2.OutP + Tx_Tables.iso_tx2_disp.Offset(ps2.Status2.Freq, ps2.Status2.OutP, Tx_Tables.iso_offset2_disp);
                        rx_value = sr.dBmValue + Rx_Tables.Offset(ps2.Status2.Freq, FuncModule.ISO);
                   
                    }  else {
                        tx_out = (float)App_Factors.iso_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                        rx_value = (float)App_Factors.iso_rx2.ValueWithOffset(ps2.Status2.Freq, sr.dBmValue);
                    }

                    sweep_points[0].Y = (tx_out - rx_value) -
                                        (cal_item.Tx0 - cal_item.Rx0) + cal_item.RL0; //- this.settings.Offset;

                    lblF.Text = "F:" + ps2.Status2.Freq.ToString("0.0") + "MHz";
                    lblTx.Text = "Tx:" + tx_out.ToString("0.0") + "dBm";
                }

                lblRx.Text = "Rx:" + rx_value.ToString("0.0") + "dBm";
                lblNoise.Text = "Noise:" + sr.dBmNosie.ToString("0.0") + "dBm";
                pltIso.Add(sweep_points, 0, PointsDone);
            }
        }

        /// <summary>
        /// ����ɨ����������������
        /// </summary>
        private void SweepProcessing()
        {   
            SweepProcesing2();

            #region ׼��CSV���������ӵ�������������
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

            //����������ӵ�����
            csv_entries[PointsDone] = entry;
            #endregion


            #region ����ɨ�������
            PointsDone++;
            #endregion
        }
        #endregion


        #region �������Ϣ������
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                //���һ�˲���
                case MessageID.ISO_SWEEP_DONE:
                    {
                        Sweeping = false;

                        pbxStart.Image = ImagesManage.GetImage("isolation", "start_in.gif");

                        break;
                    }

                //��ɵ���ɨ��
                case MessageID.ISO_SUCCED:
                    {
                        SweepProcessing();

                        break;
                    }

                //���Ų�������
                case MessageID.RF_ERROR:
                    {
                        SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);

                        MessageBox.Show(this,rfr_errors1.ToString() + "\n\r" + rfr_errors2.ToString());

                        BreakSweep(1000);

                        break;
                    }

                //Ƶ�׷�������
                case MessageID.SPECTRUM_ERROR:
                    {
                        MessageBox.Show(this,"Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");

                        BreakSweep(1000);

                        break;
                    }

                //Ƶ�׷����ɹ�
                case MessageID.SPECTRUEME_SUCCED:
                    {
                        SweepObj.Spectrum_Succed();

                        break;
                    }

                //���Ų����ɹ�
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


        #region Log2File���������ڵ���
        //private void Log2File(string s)
        //{
        //    StreamWriter sw = new StreamWriter("c:\\my_iso.log", true, Encoding.ASCII);

        //    sw.WriteLine(s);
        //    sw.Flush();
        //    sw.Close();
        //}
        #endregion
    }
}