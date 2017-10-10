// ===============================================================================
// �ļ�����SpectrumForm
// �����ˣ����
// ��  �ڣ�2011-4-29
//
// ��  ����Ƶ��ɨ��ģ��
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-4-29   	   �������ļ�
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SpectrumLib.Defines;
using SpectrumLib.Models;
using jcXY2dPlotEx;
using System.IO;


namespace jcPimSoftware
{
    public partial class SpectrumForm : Form, ISweep
    {
        #region  ��������

        /// <summary>
        /// Ƶ��������
        /// </summary>
        private SpectrumDef.ESpectrumType SpectrumType;

        /// <summary>
        /// Ƶ���ǽӿڶ���
        /// </summary>
        private SpectrumLib.ISpectrum ISpectrumObj;

        /// <summary>
        /// ��ݲ˵�������
        /// </summary>
        private SpectrumMenuDeal MenuConfig;

        /// <summary>
        /// Ƶ�׷����߳�
        /// </summary>
        private Thread thdAnalysis;

        /// <summary>
        /// Ƶ�׷�������
        /// </summary>
        private SpectrumLib.Models.ScanModel ScanModel;

        /// <summary>
        /// ���������仯��ʽ
        /// </summary>
        private enum EParamChangeMode
        {
            /// <summary>
            /// ��ʼ����Ƶ�ʱ仯
            /// </summary>
            Start_End = 0,
            /// <summary>
            /// ����Ƶ�ʴ���仯
            /// </summary>
            Center_Span = 1,
            /// <summary>
            /// ���������仯
            /// </summary>
            Others = 2
        }

        /// <summary>
        /// Ƶ�׷��������ṹ
        /// </summary>
        private struct ScanParamObj
        {
            /// <summary>
            /// ɨ����ʼƵ��(KHz)
            /// </summary>
            public int StartAnalysisFreq;
            /// <summary>
            /// ɨ�����Ƶ��(KHz)
            /// </summary>
            public int EndAnalysisFreq;
            /// <summary>
            /// ɨ������Ƶ��(KHz)
            /// </summary>
            public int CenterAnalysisFreq;
            /// <summary>
            /// ɨ�����
            /// </summary>
            public int AnalysisSpan;
            /// <summary>
            /// ATT˥��
            /// </summary>
            public int AnalysisAtt;
        }

        /// <summary>
        /// ��ǰƵ�׷�������
        /// </summary>
        private ScanParamObj CurrentScanParam;

        /// <summary>
        /// TRACE SELECT����״̬
        /// </summary>
        private struct TraceStatusObj
        {
            /// <summary>
            /// ����
            /// </summary>
            public int TraceIndex;
            /// <summary>
            /// REFREASH״̬ true���� falseδ����
            /// </summary>
            public bool bDown_REFREASH;
            /// <summary>
            /// MAXHOLD״̬ true���� falseδ����
            /// </summary>
            public bool bDown_MAXHOLD;
            /// <summary>
            /// MINHOLD״̬ true���� falseδ����
            /// </summary>
            public bool bDown_MINHOLD;
            /// <summary>
            /// HOLD״̬ true���� falseδ����
            /// </summary>
            public bool bDown_HOLD;
        }

        /// <summary>
        /// ��ŵ�ǰ����TRACE״̬�Ķ���
        /// </summary>
        private TraceStatusObj[] CurrentTraceStatus = new TraceStatusObj[5];

        /// <summary>
        /// Ƶ�׷����������󼯺�
        /// </summary>
        private jcPimSoftware.SpectrumDef.OffsetObj[] OffsetParamArray;

        /// <summary>
        /// ��ݲ˵�����
        /// </summary>
        private ContextMenuStrip ShortcutMenu;

        /// <summary>
        /// ��ǰ����Ŀ�ݲ˵�
        /// </summary>
        private jcPimSoftware.SpectrumDef.EShortcutMenu CurrentActivatedMenu;

        /// <summary>
        /// ATT��ݲ˵�����
        /// </summary>
        private int MenuAttSelectedIndex = 1;

        /// <summary>
        /// SPAN��ݲ˵�����
        /// </summary>
        private int MenuSpanSelectedIndex = 0;

        /// <summary>
        /// RBW��ݲ˵�����
        /// </summary>
        private int MenuRbwSelectedIndex = 0;

        /// <summary>
        /// VBW��ݲ˵�����
        /// </summary>
        private int MenuVbwSelectedIndex = 0;

        /// <summary>
        /// SCALE��ݲ˵�����
        /// </summary>
        private int MenuScaleSelectedIndex = 0;

        /// <summary>
        /// UNIT��ݲ˵�����
        /// </summary>
        private int MenuUnitSelectedIndex = 0;

        /// <summary>
        /// REF POSITION��ݲ˵�����
        /// </summary>
        private int MenuRefPositionIndex = 0;

        /// <summary>
        /// HOLD��ݲ˵�����
        /// </summary>
        private int MenuHoldSelectedIndex = -1;

        /// <summary>
        /// MARKSELECT��ݲ˵�����
        /// </summary>
        private int MenuMarkSelectSelectedIndex = -1;

        /// <summary>
        /// PEAKHOLD��ݲ˵�����
        /// </summary>
        private int MenuPeakHoldSelectedIndex = -1;

        /// <summary>
        /// TRACESELECT��ݲ˵�����
        /// </summary>
        private int MenuTraceSelectIndex = -1;

        /// <summary>
        /// �����嶨�ƵĻ�ͼ��Ϣ
        /// </summary>
        private readonly uint MsgReciveData = 0x0400 + 200;

        /// <summary>
        /// �����嶨�Ƶĸ澯��Ϣ
        /// </summary>
        private readonly uint MsgReciveWarning = 0x0400 + 202;

        /// <summary>
        /// ͼƬ��Դ�ļ�������
        /// </summary>
        private readonly string PicfolderName = "spectrum";

        /// <summary>
        /// ��ͼ�㼯
        /// </summary>
        private PointF[] PaintPointFs = null;

        /// <summary>
        /// ��ʶ�Ƿ����ڽ���Ƶ�׷��� true���� falseֹͣ
        /// </summary>
        private bool bIsAnalysis = false;

        /// <summary>
        /// OFFSET��־λ true���� false������
        /// </summary>
        private bool bEnableOffset = false;

        /// <summary>
        /// PEAK��ʶ
        /// </summary>
        private bool bEnablePeak = false;

        /// <summary>
        /// ����ɨ�����ݵ�����
        /// </summary>
        private PointF[][] SaveDataObj;

        /// <summary>
        /// ���ɨ�����ݽṹ�Ķ�ջ(���ڼ���N��ɨ��ƽ��ֵ)
        /// </summary>
        private List<PointF[]> ScanDataStacklist = new List<PointF[]>();

        private int Scancount = 5;

        #endregion

        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public SpectrumForm()
        {
            InitializeComponent();
            plot.Resume();
            plot.SetScrollAvailable(false);
            this.TopLevel = false;
            this.ShowInTaskbar = false;
            this.Dock = DockStyle.Fill;
        }

        #endregion

        #region �����¼�

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpectrumForm_Load(object sender, System.EventArgs e)
        {
            //���ص�ǰ����
            LoadConfig();

            //TRACE��ʼ��
            ResetTraces();

            //����MARK����ɫ
            plot.SetMarkColor(0, System.Drawing.Color.DarkOrange);
            plot.SetMarkColor(1, System.Drawing.Color.DarkOrange);
            plot.SetMarkColor(2, System.Drawing.Color.DarkOrange);
            plot.SetMarkColor(3, System.Drawing.Color.DarkOrange);
            plot.SetMarkColor(4, System.Drawing.Color.DarkOrange);

            //ע��ί��
            plot.SetMarkText(MakeupMarkText);
            plot.SetPeakText(MakeupPeakText);
            plot.SetSave2File(SaveChannel_0);

            //���Ų�����ʼ��
            exe_params = new SweepParams();
            exe_params.SweepType = SweepType.Time_Sweep;
            exe_params.WndHandle = this.Handle;
            exe_params.DevInfo = new DeviceInfo();
            exe_params.TmeParam = new TimeSweepParam();
            //������Ƶ���Ų�
            RFSignal.SetWndHandle(exe_params.WndHandle);

            power1_Handle = new ManualResetEvent(false);
            power2_Handle = new ManualResetEvent(false);

            RedrawWithNewParam();
        }

        #endregion

        #region ί�з���

        public string MakeupMarkText(MarkInfo[] mi)
        {
            string label = "";

            for (int i = 0; i < mi.Length; i++)
            {
                if (mi[i].iChannel < 0)
                    continue;

                label = label + "(" + mi[i].fPoint.X.ToString() + "MHz, " +
                        mi[i].fPoint.Y.ToString() + "dBm)/ch" +
                        mi[i].iChannel.ToString() + ", ";
            }

            return "M" + mi[0].iOrder.ToString() + ": " + label;
        }

        public string MakeupPeakText(MarkInfo[] mi)
        {
            //return "M" + mi[0].iOrder.ToString() + ": " +
            //       "(" + mi[0].fPoint.X.ToString() + "MHz, " + mi[0].fPoint.Y.ToString() + "dBm)/ch" +
            //       mi[0].iChannel.ToString();

            return "Peak: " +
                   "(" + mi[0].fPoint.X.ToString() + "MHz, " + mi[0].fPoint.Y.ToString() + "dBm)/ch" +
                   mi[0].iChannel.ToString();
        }

        private void SaveChannel_0(PointF[][] pf)
        {
            SaveDataObj = pf;
        }

        #endregion

        #endregion

        #region ��ť�¼�

        #region TOP

        private void pbxTop_MouseClick(object sender, MouseEventArgs e)
        {
            MenuSwitch(pnlFir);
        }

        private void pbxTop_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxTop, "top.gif");
        }

        private void pbxTop_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxTop, "top_in.gif");
        }

        #endregion

        #region LAST

        private void pbxSpecLast_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MenuSwitch(pnlFourth);
        }

        private void pbxSpecLast_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecLast, "last.gif");
        }

        private void pbxSpecLast_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecLast, "last_in.gif");
        }

        #endregion

        #region NEXT

        private void pbxFirNext_MouseClick(object sender, MouseEventArgs e)
        {
            MenuSwitch(pnlSnd);
        }

        private void pbxFirNext_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxFirNext, "next.gif");
        }

        private void pbxFirNext_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxFirNext, "next_in.gif");
        }


        private void pbxSendNext_MouseClick(object sender, MouseEventArgs e)
        {
            MenuSwitch(pnlThd);
        }

        private void pbxSendNext_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSendNext, "next.gif");
        }

        private void pbxSendNext_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSendNext, "next_in.gif");
        }


        private void pbxThrdNext_MouseClick(object sender, MouseEventArgs e)
        {
            MenuSwitch(pnlFourth);
        }

        private void pbxThrdNext_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxThrdNext, "next.gif");
        }

        private void pbxThrdNext_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxThrdNext, "next_in.gif");
        }

        #endregion

        #region BACK

        private void pbxFthBack_MouseClick(object sender, MouseEventArgs e)
        {
            MenuSwitch(pnlThd);
        }

        private void pbxFthBack_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxFthBack, "back.gif");
        }

        private void pbxFthBack_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxFthBack, "back_in.gif");
        }


        private void pbxSndBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MenuSwitch(pnlFir);
        }

        private void pbxSndBack_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSndBack, "back.gif");
        }

        private void pbxSndBack_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSndBack, "back_in.gif");
        }


        private void pbxthrdBack_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            MenuSwitch(pnlSnd);
        }

        private void pbxthrdBack_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxthrdBack, "back.gif");
        }

        private void pbxthrdBack_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxthrdBack, "back_in.gif");
        }

        #endregion

        #region ANALYSIS

        private void pbxAnalyze_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                StartAnalysis();
            }
            else
            {
                StopAnalysis();
            }
        }

        #endregion

        #region REF LEVEL

        private void pbxSpecRef_MouseClick(object sender, MouseEventArgs e)
        {
            DynamicMenu(pbxSpecRef, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.REF_LEVEL), MenuAttSelectedIndex, MenuConfig.CurrentMenuHeight.REF_LEVEL);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.REF_LEVEL;
        }

        private void pbxSpecRef_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecRef, "reflevel.gif");
        }

        private void pbxSpecRef_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecRef, "reflevel_in.gif");
        }

        #endregion

        #region START

        private void pbxSpecStart_MouseClick(object sender, MouseEventArgs e)
        {
            FormFreq frmFreq = new FormFreq();
            frmFreq.intType = 0;
            frmFreq.IntCurrentFreq = GetCurrentFreqFormPaint(frmFreq.intType);
            if (frmFreq.ShowDialog() == DialogResult.OK)
            {
                if (frmFreq.IntStartFreq < CurrentScanParam.EndAnalysisFreq)
                {
                    CurrentScanParam.StartAnalysisFreq = frmFreq.IntStartFreq;
                    UpadatScanParam(EParamChangeMode.Start_End);
                    if (bIsAnalysis)
                    {
                        ReStartAnalysis();
                    }
                    else
                    {
                        RedrawWithNewParam();
                    }
                }
            }
        }

        private void pbxSpecStart_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecStart, "start.gif");
        }

        private void pbxSpecStart_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecStart, "start_in.gif");
        }

        #endregion

        #region STOP

        private void pbxSpecStop_MouseClick(object sender, MouseEventArgs e)
        {
            FormFreq frmFreq = new FormFreq();
            frmFreq.intType = 1;
            frmFreq.IntCurrentFreq = GetCurrentFreqFormPaint(frmFreq.intType);
            if (frmFreq.ShowDialog() == DialogResult.OK)
            {
                if (frmFreq.IntEndFreq > CurrentScanParam.StartAnalysisFreq)
                {
                    CurrentScanParam.EndAnalysisFreq = frmFreq.IntEndFreq;
                    UpadatScanParam(EParamChangeMode.Start_End);
                    if (bIsAnalysis)
                    {
                        ReStartAnalysis();
                    }
                    else
                    {
                        RedrawWithNewParam();
                    }
                }
            }
        }

        private void pbxSpecStop_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecStop, "stop.gif");
        }

        private void pbxSpecStop_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecStop, "stop_in.gif");
        }

        #endregion

        #region CENTER

        private void pbxSpecCenter_MouseClick(object sender, MouseEventArgs e)
        {
            FormFreq frmFreq = new FormFreq();
            frmFreq.intType = 2;
            frmFreq.IntCurrentFreq = GetCurrentFreqFormPaint(frmFreq.intType);
            if (frmFreq.ShowDialog() == DialogResult.OK)
            {
                CurrentScanParam.CenterAnalysisFreq = frmFreq.IntCenterFreq;
                UpadatScanParam(EParamChangeMode.Center_Span);
                if (bIsAnalysis)
                {
                    ReStartAnalysis();
                }
                else
                {
                    RedrawWithNewParam();
                }
            }
        }

        void pbxSpecCenter_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecCenter, "center.gif");
        }

        void pbxSpecCenter_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecCenter, "center_in.gif");
        }

        #endregion

        #region SPAN

        private void pbxSpan_MouseClick(object sender, MouseEventArgs e)
        {
            DynamicMenu(pbxSpan, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.SPAN), MenuSpanSelectedIndex, MenuConfig.CurrentMenuHeight.SPAN);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.SPAN;
        }

        private void pbxSpan_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpan, "Span.gif");
        }

        private void pbxSpan_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpan, "Span_in.gif");
        }

        #endregion

        #region RBW

        private void pbxRbw_MouseClick(object sender, MouseEventArgs e)
        {
            DynamicMenu(pbxRbw, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.RBW), MenuRbwSelectedIndex, MenuConfig.CurrentMenuHeight.RBW);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.RBW;
        }

        private void pbxRbw_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxRbw, "rbw.gif");
        }

        private void pbxRbw_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxRbw, "rbw_in.gif");
        }

        #endregion

        #region HOLD MENU

        private void pbxHoldMenu_MouseClick(object sender, MouseEventArgs e)
        {
            DynamicMenu(pbxHoldMenu, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.HOLD_MENU), MenuHoldSelectedIndex, MenuConfig.CurrentMenuHeight.HOLD_MENU);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.HOLD_MENU;
        }

        void pbxHoldMenu_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxHoldMenu, "holdmenu.gif");
        }

        void pbxHoldMenu_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxHoldMenu, "holdmenu_in.gif");
        }

        #endregion

        #region SCALE

        private void pbxSpecScale_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DynamicMenu(pbxSpecScale, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.SCALE), MenuScaleSelectedIndex, MenuConfig.CurrentMenuHeight.SCALE);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.SCALE;
        }

        private void pbxSpecScale_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecScale, "scale.gif");
        }

        private void pbxSpecScale_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecScale, "scale_in.gif");
        }

        #endregion

        #region REF POSITION

        private void pbxRefposition_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DynamicMenu(pbxRefposition, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.REF_POSITION), MenuRefPositionIndex, MenuConfig.CurrentMenuHeight.REF_POSITION);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.REF_POSITION;
        }

        private void pbxRefposition_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxRefposition, "refposition.gif");
        }

        private void pbxRefposition_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxRefposition, "refposition_in.gif");
        }

        #endregion

        #region UNIT

        private void pbxUnit_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DynamicMenu(pbxUnit, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.UNIT), MenuUnitSelectedIndex, MenuConfig.CurrentMenuHeight.UNIT);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.UNIT;
        }

        private void pbxUnit_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxUnit, "unit.gif");
        }

        private void pbxUnit_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxUnit, "unit_in.gif");
        }

        #endregion

        #region MARK SELECT

        private void pbxMarkerselect_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DynamicMenu(pbxMarkerselect, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.MARK_SELECT), MenuMarkSelectSelectedIndex, MenuConfig.CurrentMenuHeight.MARK_SELECT);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.MARK_SELECT;
        }

        private void pbxMarkerselect_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxMarkerselect, "markerselect.gif");
        }

        private void pbxMarkerselect_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxMarkerselect, "markerselect_in.gif");
        }

        #endregion

        #region ENTRY

        private void pbxEntry_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            FormEntry frmEntry = new FormEntry();
            frmEntry.InputEntry = CurrentScanParam.CenterAnalysisFreq;
            frmEntry.MinFreq = CurrentScanParam.StartAnalysisFreq;
            frmEntry.MaxFreq = CurrentScanParam.EndAnalysisFreq;

            if (frmEntry.ShowDialog() == DialogResult.OK)
            {
                if (MenuMarkSelectSelectedIndex == -1)
                {
                    plot.SetMarkVisible(0, true);
                    MenuMarkSelectSelectedIndex = 1;
                }
                else
                {
                    plot.SetMarkVisible(MenuMarkSelectSelectedIndex - 1, true);
                }
                plot.Mark(frmEntry.OutputEntry / 1000.0f);
            }
        }

        private void pbxEntry_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxEntry, "entry.gif");
        }

        private void pbxEntry_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxEntry, "entry_in.gif");
        }

        #endregion

        #region PEAK

        private void pbxSpecPeak_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!bEnablePeak)
            {
                plot.Peak();
                MenuMarkSelectSelectedIndex = 1;
                ChangeBtnPic(pbxSpecPeak, "peak.gif");
                bEnablePeak = true;
            }
            else
            {
                plot.SetMarkVisible(0, false);
                ChangeBtnPic(pbxSpecPeak, "peak_in.gif");
                MenuMarkSelectSelectedIndex = -1;
                bEnablePeak = false;
            }
        }

        private void pbxSpecPeak_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecPeak, "peak.gif");
        }

        private void pbxSpecPeak_MouseLeave(object sender, System.EventArgs e)
        {
            if (!bEnablePeak)
                ChangeBtnPic(pbxSpecPeak, "peak_in.gif");
        }

        #endregion

        #region Rev

        private void pbxRev_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                FormRev frmRev = new FormRev();
                frmRev.Rev = App_Settings.spc.Rev;
                if (frmRev.ShowDialog() == DialogResult.OK)
                {
                    App_Settings.spc.Rev = frmRev.Rev;
                }
            }
        }

        private void pbxRev_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxRev, "rev.gif");
        }

        private void pbxRev_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxRev, "rev_in.gif");
        }

        #endregion

        #region VBW

        private void pbxVbw_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //DynamicMenu(pbxPeakhold, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.PEAK_HOLD), MenuPeakHoldSelectedIndex, MenuConfig.CurrentMenuHeight.PEAK_HOLD);
            //CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.PEAK_HOLD;

            if (App_Configure.Cnfgs.Spectrum == 1)
            {
                DynamicMenu(pbxVbw, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.VBW), MenuVbwSelectedIndex, MenuConfig.CurrentMenuHeight.VBW);
                CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.VBW;
            }
            else
            {
                MessageBox.Show(this, "This function is not available now!");
            }
        }

        private void pbxVbw_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxVbw, "vbw.gif");
        }

        private void pbxVbw_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxVbw, "vbw_in.gif");
        }

        #endregion

        #region TRACE SELECT

        private void pbxTraceselect_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DynamicMenu(pbxTraceselect, MenuConfig.GetMenuContent(SpectrumDef.EShortcutMenu.TRACE_SELECT), MenuTraceSelectIndex, MenuConfig.CurrentMenuHeight.TRACE_SELECT);
            CurrentActivatedMenu = jcPimSoftware.SpectrumDef.EShortcutMenu.TRACE_SELECT;
        }

        private void pbxTraceselect_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxTraceselect, "traceselect.gif");
        }

        private void pbxTraceselect_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxTraceselect, "traceselect_in.gif");
        }

        #endregion

        #region REFREASH
        /// <summary>
        /// REFREASH
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbxReflesh_MouseClick(object sender, MouseEventArgs e)
        {
            if (MenuTraceSelectIndex != -1)
            {
                if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_REFREASH)
                {
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_REFREASH = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MAXHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MINHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_HOLD = false;

                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    plot.SetChannelColor(0, System.Drawing.Color.Yellow);
                }
                else
                {
                    ChangeBtnPic(pbxReflesh, "reflesh.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    SetTraceFun(MenuTraceSelectIndex - 1, 0);
                }
            }

            ShowTrace();
        }

        #endregion

        #region MAXHOLD
        /// <summary>
        /// MAXHOLD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbxMaxhold_MouseClick(object sender, MouseEventArgs e)
        {
            if (MenuTraceSelectIndex != -1)
            {
                if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MAXHOLD)
                {
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_REFREASH = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MAXHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MINHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_HOLD = false;

                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    plot.Trace(ChannelTraceType.cttMax, false);
                }
                else
                {
                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    SetTraceFun(MenuTraceSelectIndex - 1, 1);
                }
            }

            ShowTrace();
        }

        #endregion

        #region MINHOLD
        /// <summary>
        /// MINHOLD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbxMinhold_MouseClick(object sender, MouseEventArgs e)
        {
            if (MenuTraceSelectIndex != -1)
            {
                if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MINHOLD)
                {
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_REFREASH = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MAXHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MINHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_HOLD = false;

                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    plot.Trace(ChannelTraceType.cttMin, false);
                }
                else
                {
                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    SetTraceFun(MenuTraceSelectIndex - 1, 2);
                }
            }

            ShowTrace();
        }

        #endregion

        #region HOLD
        /// <summary>
        /// HOLD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbxHold_MouseClick(object sender, MouseEventArgs e)
        {
            if (MenuTraceSelectIndex != -1)
            {
                if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_HOLD)
                {
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_REFREASH = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MAXHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MINHOLD = false;
                    CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_HOLD = false;

                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold_in.gif");

                    plot.Trace(ChannelTraceType.cttHld, false);
                }
                else
                {
                    ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
                    ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
                    ChangeBtnPic(pbxMinhold, "minhold_in.gif");
                    ChangeBtnPic(pbxHold, "hold.gif");

                    SetTraceFun(MenuTraceSelectIndex - 1, 3);
                }
            }

            ShowTrace();
        }

        #endregion

        #region <<

        private void pbxLeft_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int Span = CurrentScanParam.AnalysisSpan;
            CurrentScanParam.StartAnalysisFreq -= Span / 5;
            CurrentScanParam.EndAnalysisFreq -= Span / 5;

            UpadatScanParam(EParamChangeMode.Start_End);
            if (bIsAnalysis)
            {
                ReStartAnalysis();
            }
            else
            {
                RedrawWithNewParam();
            }
        }

        private void pbxLeft_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxLeft, "left.gif");
        }

        private void pbxLeft_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxLeft, "left_in.gif");
        }

        #endregion

        #region >>

        private void pbxRight_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            int Span = CurrentScanParam.AnalysisSpan;
            CurrentScanParam.StartAnalysisFreq += Span / 5;
            CurrentScanParam.EndAnalysisFreq += Span / 5;

            UpadatScanParam(EParamChangeMode.Start_End);
            if (bIsAnalysis)
            {
                ReStartAnalysis();
            }
            else
            {
                RedrawWithNewParam();
            }
        }

        private void pbxRight_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxRight, "right.gif");
        }

        private void pbxRight_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxRight, "right_in.gif");
        }

        #endregion

        #region OFFSET OPTION

        private void pbxOffsetoption_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            FormOffset frmOffset = new FormOffset();
            frmOffset.bEnableOffset = bEnableOffset;
            frmOffset.bBand = App_Configure.Cnfgs.Channel;
            if (SpectrumType == SpectrumDef.ESpectrumType.SpeCat2)
            {
                frmOffset.intRbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
            }
            else
            {
                frmOffset.intRbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
            }

            if (bIsAnalysis)
            {
                StopAnalysis();
            }

            try
            {
                frmOffset.ShowDialog();
            }
            catch { }
            
            if (frmOffset.bOn)
            {
                //if (frmOffset.OutOffsetData != null)
                //{
                //    OffsetParamArray = SpectrumOffset.FormatOffsetData(frmOffset.OutOffsetData);
                //}
                bEnableOffset = true;
                lblOffset.Text = "OFFSET";
            }
            else
            {
                if (!frmOffset.bEnableOffset)
                {
                    bEnableOffset = false;
                    lblOffset.Text = "";
                }
            }
        }

        private void pbxOffsetoption_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxOffsetoption, "offsetoption.gif");
        }

        private void pbxOffsetoption_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxOffsetoption, "offsetoption_in.gif");
        }

        #endregion

        #region DATASAVE

        private void pbxSpecDatasave_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                Deal_DATASAVE();
            }
            else
            {
                StopAnalysis();
                Deal_DATASAVE();
                //MessageBox.Show(this, "Please stop the spectrum analysis before saving data!");
            }
        }

        private void pbxSpecDatasave_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxSpecDatasave, "datasave.gif");
        }

        private void pbxSpecDatasave_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxSpecDatasave, "datasave_in.gif");
        }

        #endregion

        #region DATAREAD

        private void pbxDataread_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!bIsAnalysis)
            {
                Deal_DATAREAD();
            }
            else
            {
                StopAnalysis();
                Deal_DATAREAD();
                //MessageBox.Show(this, "Please stop the spectrum analysis before reading data!");
            }
        }

        private void pbxDataread_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ChangeBtnPic(pbxDataread, "dataread.gif");
        }

        private void pbxDataread_MouseLeave(object sender, System.EventArgs e)
        {
            ChangeBtnPic(pbxDataread, "dataread_in.gif");
        }

        #endregion

        #region RF

        private void pbxSpecRF_MouseClick(object sender, MouseEventArgs e)
        {
            if (!bRFon)
            {
                if (MessageBox.Show(this,"It is dangerous to turn on RF,are you sure?", "Warning", MessageBoxButtons.OKCancel) != DialogResult.OK)
                    return;

                if (FormRF.EnableRF_1)
                {
                    thdRF_1 = new Thread(StartRF_1);
                    thdRF_1.IsBackground = true;
                    thdRF_1.Start();
                    lblRF_1.Text = "PA1: ON";
                }

                if (FormRF.EnableRF_2)
                {
                    thdRF_2 = new Thread(StartRF_2);
                    thdRF_2.IsBackground = true;
                    thdRF_2.Start();
                    lblRF_2.Text = "PA2: ON";
                }

                if (FormRF.EnableRF_1 || FormRF.EnableRF_2)
                {
                    bRFon = true;
                    ChangeBtnPic(pbxSpecRF, "rf.gif");
                    tmRF.Stop();
                    tmRF.Start();
                }
            }
            else
            {
                if (thdRF_1 != null)
                {
                    if (thdRF_1.IsAlive)
                    {
                        thdRF_1.Abort();
                    }
                }

                if (thdRF_2 != null)
                {
                    if (thdRF_2.IsAlive)
                    {
                        thdRF_2.Abort();
                    }
                }

                CloseRF_1();
                CloseRF_2();
                bRFon = false;
                ChangeBtnPic(pbxSpecRF, "rf_in.gif");

                tmRF.Stop();
            }
        }

        private void pbxSpecRF_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bRFon)
            {
                ChangeBtnPic(pbxSpecRF, "rf.gif");
            }
        }

        private void pbxSpecRF_MouseLeave(object sender, EventArgs e)
        {
            if (!bRFon)
            {
                ChangeBtnPic(pbxSpecRF, "rf_in.gif");
            }
        }

        #endregion

        #endregion

        #region ��������

        #region ͨ�ð�ťͼƬ�л�
        /// <summary>
        /// ͨ�ð�ťͼƬ�л�
        /// </summary>
        /// <param name="pb">PictureBox����</param>
        /// <param name="picName">ͼƬ����</param>
        private void ChangeBtnPic(PictureBox pb, string picName)
        {
            pb.Image = ImagesManage.GetImage(PicfolderName, picName);
        }

        #endregion

        #region ͨ��Ƶ���ǲ˵��л�
        /// <summary>
        /// ͨ��Ƶ���ǲ˵��л�
        /// </summary>
        /// <param name="btnPanel">�л���Panel</param>
        private void MenuSwitch(Panel objPanel)
        {
            this.DoubleBuffered = true;
            pnlMenu.SuspendLayout();
            pnlMenu.Controls.Clear();
            pnlMenu.Controls.Add(objPanel);
            objPanel.Visible = true;
            objPanel.Dock = DockStyle.Bottom;
            pnlMenu.ResumeLayout();
        }

        #endregion

        #region ���ص�ǰ��������
        /// <summary>
        /// ���ص�ǰ��������
        /// </summary>
        private void LoadConfig()
        {
            ChangeRevFwd();
            //int type = 0;
            int type = App_Configure.Cnfgs.Spectrum;
            switch (type)
            {
                case 0:
                    SpectrumType = SpectrumDef.ESpectrumType.SpeCat2;
                    MenuConfig = new SpectrumMenuDeal(SpectrumType);
                    ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(this.Handle, MsgReciveData, MsgReciveWarning);
                    MenuAttSelectedIndex = SpectrumATT.GetAttIndex(SpectrumDef.ESpectrumType.SpeCat2, App_Settings.spc.Att);
                    CurrentScanParam.AnalysisAtt = SpectrumATT.GetAttValue(SpectrumDef.ESpectrumType.SpeCat2, MenuAttSelectedIndex);
                    break;
                case 1:
                    SpectrumType = SpectrumDef.ESpectrumType.Bird;
                    MenuConfig = new SpectrumMenuDeal(SpectrumType);
                    ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(this.Handle, MsgReciveData, MsgReciveWarning);
                    MenuAttSelectedIndex = SpectrumATT.GetAttIndex(SpectrumDef.ESpectrumType.Bird, App_Settings.spc.Att);
                    CurrentScanParam.AnalysisAtt = SpectrumATT.GetAttValue(SpectrumDef.ESpectrumType.Bird, MenuAttSelectedIndex);
                    break;
                case 2:
                    SpectrumType = SpectrumDef.ESpectrumType.Deli;
                    MenuConfig = new SpectrumMenuDeal(SpectrumType);
                    ISpectrumObj = new SpectrumLib.Spectrums.Deli(this.Handle, MsgReciveData, MsgReciveWarning);
                    MenuAttSelectedIndex = SpectrumATT.GetAttIndex(SpectrumDef.ESpectrumType.Deli, App_Settings.spc.Att);
                    CurrentScanParam.AnalysisAtt = SpectrumATT.GetAttValue(SpectrumDef.ESpectrumType.Deli, MenuAttSelectedIndex);
                    pbxScanband.Visible = true;
                    pbxScanband.Enabled = true;
                    break;
            }
            //ISpectrumObj.EnableLog();

            //���������ļ�
            App_Settings.spc.LoadSettings();
            CurrentScanParam.StartAnalysisFreq = (int)(App_Settings.spc.Start * 1000);
            CurrentScanParam.EndAnalysisFreq = (int)(App_Settings.spc.Stop * 1000);
            CurrentScanParam.AnalysisSpan = CurrentScanParam.EndAnalysisFreq - CurrentScanParam.StartAnalysisFreq;
            CurrentScanParam.CenterAnalysisFreq = CurrentScanParam.StartAnalysisFreq + CurrentScanParam.AnalysisSpan / 2;
            //CurrentScanParam.AnalysisAtt = App_Settings.spc.Att;
            bEnableOffset = App_Settings.spc.EnableOffset;

            //�˵�ѡ��
            MenuAttSelectedIndex = SpectrumATT.GetAttIndex(SpectrumType, CurrentScanParam.AnalysisAtt);
            MenuRbwSelectedIndex = SpectrumRBW.GetIndexByValue(SpectrumType, App_Settings.spc.Rbw);
            MenuVbwSelectedIndex = SpectrumRBW.GetIndexByValue(SpectrumType, App_Settings.spc.Vbw);
            int spanIndex = SpectrumSPAN.SpeCat2_GetSpanIndex(CurrentScanParam.AnalysisSpan);
            if (spanIndex > 0)
            {
                MenuSpanSelectedIndex = spanIndex;
            }
            else
            {
                MenuSpanSelectedIndex = 9;
            }
            MenuScaleSelectedIndex = 0;
            MenuUnitSelectedIndex = 0;
            MenuRefPositionIndex = 0;

            //��ʶ��Ϣ
            double startFreq = CurrentScanParam.StartAnalysisFreq / 1000.0;
            double endFreq = CurrentScanParam.EndAnalysisFreq / 1000.0;
            lblStart.Text = "Start: " + startFreq.ToString("0.000") + "MHz";
            lblStop.Text = "Stop: " + endFreq.ToString("0.000") + "MHz";

            //������Ϣ
            if (App_Settings.spc.EnableOffset)
            {
                bEnableOffset = true;
                lblOffset.Text = "OFFSET";
            }
            else
                lblOffset.Text = "";

            //�Ƿ���������
            if (App_Settings.spc.EnableRF == 0)
            {
                lblRF_1.Visible = false;
                lblRF_2.Visible = false;
                lblRF1.Visible = false;
                lblRF2.Visible = false;
                pbxSpecRF.Visible = false;
            }
            else
            {
                lblRF_1.Visible = true;
                lblRF_2.Visible = true;
                lblRF1.Visible = true;
                lblRF2.Visible = true;
                pbxSpecRF.Visible = true;
            }

            //�ط���Ϣ
            lblTim.Text = "";
        }

        #endregion

        #region ��ȡ��ͼ��ʼ������������Ƶ��(KHz)
        /// <summary>
        /// ��ȡ��ͼ��ʼ������������Ƶ��(KHz)
        /// </summary>
        /// <param name="intType">���� 0��ʼƵ�� 1����Ƶ�� 2����Ƶ��</param>
        /// <returns>Ƶ��(KHz)</returns>
        private int GetCurrentFreqFormPaint(int intType)
        {
            int revFreq = 0;
            int intMultiple = 1;

            switch (intType)
            {
                case 0:
                    revFreq = CurrentScanParam.StartAnalysisFreq * intMultiple;
                    break;
                case 1:
                    revFreq = CurrentScanParam.EndAnalysisFreq * intMultiple;
                    break;
                case 2:
                    revFreq = CurrentScanParam.CenterAnalysisFreq * intMultiple;
                    break;
                default:
                    revFreq = CurrentScanParam.StartAnalysisFreq * intMultiple;
                    break;
            }

            return revFreq;
        }

        #endregion

        #region ���µ�ǰɨ�����
        /// <summary>
        /// ���µ�ǰɨ�����
        /// </summary>
        private void UpadatScanParam(EParamChangeMode ChangeMode)
        {
            double startFreq = CurrentScanParam.StartAnalysisFreq / 1000.0;
            double endFreq = CurrentScanParam.EndAnalysisFreq / 1000.0;
            double centerFreq = CurrentScanParam.CenterAnalysisFreq / 1000.0;
            double Span = CurrentScanParam.AnalysisSpan / 1000.0;
            int indexSpan = 0;
            int rbw_bird = 30 * 1000;

            if (SpectrumType == SpectrumDef.ESpectrumType.SpeCat2)
            {
                switch (ChangeMode)
                {
                    case EParamChangeMode.Start_End:
                        CurrentScanParam.CenterAnalysisFreq = (CurrentScanParam.StartAnalysisFreq + CurrentScanParam.EndAnalysisFreq) / 2;
                        CurrentScanParam.AnalysisSpan = CurrentScanParam.EndAnalysisFreq - CurrentScanParam.StartAnalysisFreq;
                        lblStart.Text = "Start: " + startFreq.ToString("0.000") + "MHz";
                        lblStop.Text = "Stop: " + endFreq.ToString("0.000") + "MHz";
                        break;
                    case EParamChangeMode.Center_Span:
                        float offsetStart = CurrentScanParam.CenterAnalysisFreq - CurrentScanParam.AnalysisSpan / 2;
                        float offsetStop = (3000000 - CurrentScanParam.CenterAnalysisFreq) - CurrentScanParam.AnalysisSpan / 2;
                        if (offsetStart >= 0 && offsetStop >= 0)
                        {
                            CurrentScanParam.StartAnalysisFreq = CurrentScanParam.CenterAnalysisFreq - CurrentScanParam.AnalysisSpan / 2;
                            CurrentScanParam.EndAnalysisFreq = CurrentScanParam.CenterAnalysisFreq + CurrentScanParam.AnalysisSpan / 2;
                            lblStart.Text = "Center: " + centerFreq.ToString("0.000") + "MHz";
                            lblStop.Text = "Span: " + Span.ToString("0.000") + "MHz";
                        }
                        break;
                    case EParamChangeMode.Others:
                        break;
                    default:
                        break;
                }

                //���¶�λSPAN����
                indexSpan = SpectrumSPAN.SpeCat2_GetSpanIndex(CurrentScanParam.AnalysisSpan);
                if (indexSpan < 0)
                {
                    MenuSpanSelectedIndex = 9;
                }
                else
                {
                    MenuSpanSelectedIndex = indexSpan;
                }
            }
            else
            {
                //rbw_bird = SpectrumRBW.GetRbwRealValue(SpectrumDef.ESpectrumType.Bird, SpectrumRBW.GetRbwValue(SpectrumDef.ESpectrumType.Bird, MenuRbwSelectedIndex));
                switch (ChangeMode)
                {
                    case EParamChangeMode.Start_End:
                        //if (rbw_bird * 705 / 1000 < CurrentScanParam.EndAnalysisFreq - CurrentScanParam.StartAnalysisFreq)
                        //    CurrentScanParam.EndAnalysisFreq = CurrentScanParam.StartAnalysisFreq + rbw_bird * 705 / 1000;

                        CurrentScanParam.CenterAnalysisFreq = (CurrentScanParam.StartAnalysisFreq + CurrentScanParam.EndAnalysisFreq) / 2;
                        CurrentScanParam.AnalysisSpan = CurrentScanParam.EndAnalysisFreq - CurrentScanParam.StartAnalysisFreq;
                        //endFreq = CurrentScanParam.EndAnalysisFreq / 1000f;
                        lblStart.Text = "Start: " + startFreq.ToString("0.000") + "MHz";
                        lblStop.Text = "Stop: " + endFreq.ToString("0.000") + "MHz";
                        break;
                    case EParamChangeMode.Center_Span:
                        //if (rbw_bird * 705 / 1000 < CurrentScanParam.AnalysisSpan)
                        //    CurrentScanParam.AnalysisSpan = rbw_bird * 705 / 1000;

                        float offsetStart = CurrentScanParam.CenterAnalysisFreq - CurrentScanParam.AnalysisSpan / 2;
                        float offsetStop = (3000000 - CurrentScanParam.CenterAnalysisFreq) - CurrentScanParam.AnalysisSpan / 2;
                        if (offsetStart >= 0 && offsetStop >= 0)
                        {
                            CurrentScanParam.StartAnalysisFreq = CurrentScanParam.CenterAnalysisFreq - CurrentScanParam.AnalysisSpan / 2;
                            CurrentScanParam.EndAnalysisFreq = CurrentScanParam.CenterAnalysisFreq + CurrentScanParam.AnalysisSpan / 2;
                            //Span = CurrentScanParam.AnalysisSpan / 1000f;
                            lblStart.Text = "Center: " + centerFreq.ToString("0.000") + "MHz";
                            lblStop.Text = "Span: " + Span.ToString("0.000") + "MHz";
                        }
                        break;
                    case EParamChangeMode.Others:
                        break;
                    default:
                        break;
                }

                //���¶�λSPAN����
                indexSpan = SpectrumSPAN.SpeCat2_GetSpanIndex(CurrentScanParam.AnalysisSpan);
                if (indexSpan < 0)
                {
                    MenuSpanSelectedIndex = 9;
                }
                else
                {
                    MenuSpanSelectedIndex = indexSpan;
                }
            }
        }

        #endregion

        #region ���²��������ػ�
        /// <summary>
        /// ���²��������ػ�
        /// </summary>
        private void RedrawWithNewParam()
        {
            float Xstart, Xend;
            float Ystart, Yend;

            //X�᷶Χ(MHz)
            Xstart = Convert.ToSingle(CurrentScanParam.StartAnalysisFreq) / 1000;
            Xend = Convert.ToSingle(CurrentScanParam.EndAnalysisFreq) / 1000;

            //Y�᷶Χ(dB)
            Yend = CurrentScanParam.AnalysisAtt - 40 + GetYConversionValue(MenuUnitSelectedIndex) + GetReferenceValue(MenuRefPositionIndex);
            Ystart = Yend - 100 * GetScaleValue(MenuScaleSelectedIndex);

            //����X�ᡢY��
            plot.SetXStartStopWithMajor(Xstart, Xend, 5);
            plot.SetYStartStopWithMajor(Ystart, Yend, 5);

            //������ʾ��Ϣ
            lblAtt.Text = "ATT:" + CurrentScanParam.AnalysisAtt.ToString() + "dB";
            if (SpectrumType == SpectrumDef.ESpectrumType.SpeCat2)
            {
                lblRbw.Text = "RBW:" + SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex)).ToString() + "KHz";
            }
            else if (SpectrumType == SpectrumDef.ESpectrumType.Bird)
            {
                lblRbw.Text = "RBW:" + (SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex)) / 1000.0).ToString() + "KHz";
            }
            else
            {
                lblRbw.Text = "RBW:" + SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex)).ToString() + "KHz";
            }
            int perDiv = (int)((Yend - Ystart) / (plot.YMajorCount * plot.YMinorCount));
            lblDiv.Text = perDiv.ToString() + "dB/Div";

            //���浱ǰ����
            App_Settings.spc.Start = CurrentScanParam.StartAnalysisFreq / 1000.0f;
            App_Settings.spc.Stop = CurrentScanParam.EndAnalysisFreq / 1000.0f;
            App_Settings.spc.Att = CurrentScanParam.AnalysisAtt;
            App_Settings.spc.Rbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
            App_Settings.spc.Vbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuVbwSelectedIndex));
            App_Settings.spc.EnableOffset = bEnableOffset;
        }

        #endregion

        #region ��ʼ����Ƶ�׷���
        /// <summary>
        /// ��ʼ����Ƶ�׷���
        /// </summary>
        private void StartAnalysis()
        {
            //�����ʷ����
            ScanDataStacklist.Clear();

            RedrawWithNewParam();

            //��ȡƵ�׷��������ṹ
            //if (bEnableOffset)
            //{
            //    FormOffset.FilePath = App_Settings.spc.OffsetFilePath;
            //    OffsetParamArray = SpectrumOffset.FormatOffsetData(SpectrumOffset.LoadOffsetFile(FormOffset.FilePath));
            //}

            object o;
            ScanModel = new ScanModel();
            ScanModel.StartFreq = (double)CurrentScanParam.StartAnalysisFreq;
            ScanModel.EndFreq = (double)CurrentScanParam.EndAnalysisFreq;
            // ScanModel.Centerfreq = (double)CurrentScanParam.CenterAnalysisFreq;
            ScanModel.Unit = CommonDef.EFreqUnit.KHz;
            ScanModel.Att = CurrentScanParam.AnalysisAtt;
            ScanModel.Rbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));

            if (SpectrumType == SpectrumDef.ESpectrumType.Deli)
            {
                ScanModel.Vbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
            }
            else
            {
                if (MenuRbwSelectedIndex < 10)
                    ScanModel.Vbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex + 1));
                else
                    ScanModel.Vbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
            }

            ScanModel.FullPoints = false;
            ScanModel.EnableTimer = true;
            ScanModel.Continued = true;
            ScanModel.TimeSpan = App_Settings.spc.SampleSpan;
            ScanModel.ProtectNEC = true;
            ScanModel.MaxP = -40;
            ScanModel.DetectMode = CommonDef.DetectMode.MaxMode;
            ScanModel.Deli_averagecount = Scancount;
            ScanModel.Deli_detector = "AVERage";
            ScanModel.Deli_ref = CurrentScanParam.AnalysisAtt - 40 + GetYConversionValue(MenuUnitSelectedIndex) + GetReferenceValue(MenuRefPositionIndex);
            ScanModel.Deli_startspe = 1;
            ScanModel.DeliSpe = CommonDef.SpectrumType.Deli_SPECTRUM;
            ScanModel.Deli_isSpectrum = true;

            o = ScanModel;

            plot.Clear();
            thdAnalysis = new Thread(ISpectrumObj.StartAnalysis);
            thdAnalysis.IsBackground = true;
            thdAnalysis.Start(o);

            lblTim.Text = "";
            bIsAnalysis = true;
            ChangeBtnPic(pbxAnalyze, "analyze.gif");

            bEnablePeak = false;
            ChangeBtnPic(pbxSpecPeak, "peak_in.gif");
        }

        #endregion

        #region ֹͣ��ǰƵ�׷���
        /// <summary>
        /// ֹͣ��ǰƵ�׷���
        /// </summary>
        private void StopAnalysis()
        {
            ISpectrumObj.StopAnalysis();
            if (thdAnalysis != null)
            {
                if (thdAnalysis.IsAlive)
                {
                    thdAnalysis.Abort();
                }
            }
            bIsAnalysis = false;
            ChangeBtnPic(pbxAnalyze, "analyze_in.gif");
        }

        #endregion

        #region ��ֹͣ����������
        /// <summary>
        /// ��ֹͣ����������
        /// </summary>
        private void ReStartAnalysis()
        {
            StopAnalysis();
            StartAnalysis();
        }

        #endregion

        #region ���ݵ�λ���㳡ǿֵ
        /// <summary>
        /// ���ݵ�λ���㳡ǿֵ
        /// </summary>
        /// <param name="pf">ת��ǰ��ͼ�㼯��</param>
        /// <returns>ת�����ͼ�㼯��</returns>
        private PointF[] CountByUnit(PointF[] pf)
        {
            PointF[] revPF = new PointF[pf.Length];
            float offset = GetYConversionValue(MenuUnitSelectedIndex);
            for (int i = 0; i < pf.Length; i++)
            {
                revPF[i].X = pf[i].X;
                revPF[i].Y = pf[i].Y + offset;
            }

            return revPF;
        }

        #endregion

        #region ����Ƶ�׷����Ĳ���ֵ
        /// <summary>
        /// ����Ƶ�׷����Ĳ���ֵ
        /// </summary>
        /// <param name="PaintPointFs">����ǰ�㼯</param>
        /// <returns>������㼯</returns>
        private PointF[] DataValueOffset(PointF[] PaintPointFs)
        {
            PointF[] revPointF = new PointF[PaintPointFs.Length];
            float offsetValue = 0;

            //if (SpectrumType == SpectrumDef.ESpectrumType.Bird)
            //{
            //    if (OffsetParamArray == null)
            //        return PaintPointFs;
            //    for (int i = 0; i < PaintPointFs.Length; i++)
            //    {
            //        for (int j = 0; j < OffsetParamArray.Length; j++)
            //        {
            //            revPointF[i].X = PaintPointFs[i].X;
            //            if (PaintPointFs[i].X < OffsetParamArray[j].endFreq)
            //            {
            //                offsetValue = Convert.ToSingle(OffsetParamArray[j].paramA * PaintPointFs[i].X + OffsetParamArray[j].paramB);
            //                revPointF[i].Y = PaintPointFs[i].Y + offsetValue;
            //                break;
            //            }
            //        }
            //    }
            //}

            //if (SpectrumType == SpectrumDef.ESpectrumType.SpeCat2)
            //{
                jcPimSoftware.RBW type_rbw = RBW.rbw4KHz;
                int rbw = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
                if (SpectrumType == SpectrumDef.ESpectrumType.SpeCat2)
                {
                    switch (rbw)
                    {
                        case 4:
                            type_rbw = RBW.rbw4KHz;
                            break;
                        case 20:
                            type_rbw = RBW.rbw20KHz;
                            break;
                        case 100:
                            type_rbw = RBW.rbw100KHz;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (rbw)
                    {
                        case 10 * 1000:
                            type_rbw = RBW.rbw4KHz;
                            break;
                        case 100 * 1000:
                            type_rbw = RBW.rbw20KHz;
                            break;
                        case 1000 * 1000:
                            type_rbw = RBW.rbw100KHz;
                            break;
                        default:
                            break;
                    }
                }
                for (int i = 0; i < PaintPointFs.Length; i++)
                {
                    revPointF[i].X = PaintPointFs[i].X;
                    if (App_Configure.Cnfgs.Channel == 0)
                        offsetValue = Spectrum_Tables.Offset(revPointF[i].X, RxChannel.NarrowBand, type_rbw);
                    else
                        offsetValue = Spectrum_Tables.Offset(revPointF[i].X, RxChannel.WideBand, type_rbw);

                    revPointF[i].Y = PaintPointFs[i].Y + offsetValue;
                }
            //}

            return revPointF;
        }

        #endregion

        #region ͨ�ÿ�ݲ˵�����ѡ��
        /// <summary>
        /// ͨ�ÿ�ݲ˵�����ѡ��
        /// </summary>
        /// <param name="startIndex">��������ʼ����</param>
        /// <param name="MenuSelectedIndex">��Ӧ��ݲ˵�����</param>
        private void SelectMenuItem(int startIndex, ref int MenuSelectedIndex)
        {
            if (startIndex > ShortcutMenu.Items.Count - 1)
            {
                return;
            }

            ToolStripMenuItem ToolStripItem;
            for (int i = startIndex; i < ShortcutMenu.Items.Count; i++)
            {
                ToolStripItem = (ToolStripMenuItem)ShortcutMenu.Items[i];
                if (ShortcutMenu.Items[i].Selected)
                {
                    ToolStripItem.Checked = true;
                    MenuSelectedIndex = i;
                }
                else
                {
                    ToolStripItem.Checked = false;
                }
            }
        }

        #endregion

        #region ͨ�ÿ�ݲ˵�ȡ��ѡ��
        /// <summary>
        /// ͨ�ÿ�ݲ˵�ȡ��ѡ��
        /// </summary>
        /// <param name="MenuSelectedIndex">��Ӧ��ݲ˵�����</param>
        private void NoneSelectItem(ref int MenuSelectedIndex)
        {
            ToolStripMenuItem ToolStripItem;
            for (int i = 0; i < ShortcutMenu.Items.Count; i++)
            {
                ToolStripItem = (ToolStripMenuItem)ShortcutMenu.Items[i];
                ToolStripItem.Checked = false;
            }
            MenuSelectedIndex = -1;
        }

        #endregion

        #region ͨ�ö�̬���ɿ�ݲ˵�

        /// <summary>
        /// ͨ�ö�̬���ɿ�ݲ˵�
        /// </summary>
        /// <param name="control">�ռ����</param>
        /// <param name="strContentArray">�˵�����</param>
        /// <param name="intDefaultIndex">Ĭ��ѡ����</param>
        /// <param name="Ypix">Y����λ��</param>
        private void DynamicMenu(Control control, string[] strContentArray, int intDefaultIndex, int Ypix)
        {
            ShortcutMenu = new ContextMenuStrip();
            //���ز˵�����
            for (int i = 0; i < strContentArray.Length; i++)
            {
                string[] parts = strContentArray[i].Split(',');
                ShortcutMenu.Items.Add(parts[0]);
                if (parts.Length > 1)
                {
                    if (parts[1] == "0")
                        ShortcutMenu.Items[i].Visible = false;
                    else
                        ShortcutMenu.Items[i].Visible = true;
                }
            }
            //Ĭ��ѡ��
            if (intDefaultIndex >= 0)
            {
                ToolStripMenuItem ToolStripItem = (ToolStripMenuItem)ShortcutMenu.Items[intDefaultIndex];
                ToolStripItem.Checked = true;
            }
            //ע���¼�
            ShortcutMenu.ItemClicked += new ToolStripItemClickedEventHandler(ShortcutMenu_ItemClicked);
            ShortcutMenu.Show(control, new Point(0, Ypix));
        }

        /// <summary>
        /// ��ݲ˵��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShortcutMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (CurrentActivatedMenu)
            {
                case jcPimSoftware.SpectrumDef.EShortcutMenu.REF_LEVEL:
                    Dealing_ATT();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.RBW:
                    Dealing_RBW();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.VBW:
                    Dealing_VBW();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.SPAN:
                    Dealing_SPAN();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.HOLD_MENU:
                    Dealing_HOLDMENU();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.SCALE:
                    Dealing_SCALE();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.UNIT:
                    Deal_UNIT();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.REF_POSITION:
                    Dealing_RefPosition();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.MARK_SELECT:
                    Dealing_MarkSelect();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.PEAK_HOLD:
                    Dealing_PeakHold();
                    break;
                case jcPimSoftware.SpectrumDef.EShortcutMenu.TRACE_SELECT:
                    Dealing_TraceSelect();
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region �ļ����Ϸ���У��
        /// <summary>
        /// �ļ����Ϸ���У��
        /// </summary>
        /// <param name="txt">�ļ���</param>
        /// <returns>true�Ϸ���false���Ϸ�</returns>
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

        #region ����N��ɨ�����ݵ�ƽ��ֵ
        /// <summary>
        /// ����N��ɨ�����ݵ�ƽ��ֵ
        /// </summary>
        /// <param name="points">��ǰһ��ɨ��ĵ㼯</param>
        /// <param name="currentpoints">��ǰ��ɨ�����</param>
        /// <returns>ƽ��ֵ�㼯</returns>
        private PointF[] CountAverage(PointF[] points, int currentpoints)
        {
            int optcount = App_Settings.spc.AverageCount;             //Ŀ������ƽ������
            PointF[] revPoints = new PointF[705];                     //ƽ��ֵ�ĵ㼯
            float sum = 0;

            if (points.Length < 705)
                return points;

            if (optcount > 10)
                optcount = 10;

            if (ScanDataStacklist.Count < optcount)
            {
                if (currentpoints == 705)
                {
                    ScanDataStacklist.Add(points);
                    for (int i = 0; i < revPoints.Length; i++)
                    {
                        revPoints[i].X = ScanDataStacklist[0][i].X;
                        for (int j = 0; j < ScanDataStacklist.Count; j++)
                        {
                            sum += ScanDataStacklist[j][i].Y;
                        }
                        revPoints[i].Y = sum / ScanDataStacklist.Count;
                        sum = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < currentpoints; i++)
                    {
                        revPoints[i].X = points[i].X;
                        for (int j = 0; j < ScanDataStacklist.Count; j++)
                        {
                            sum += ScanDataStacklist[j][i].Y;
                        }
                        revPoints[i].Y = (sum + points[i].Y) / (ScanDataStacklist.Count + 1);
                        sum = 0;
                    }

                    for (int k = currentpoints; k < 705; k++)
                    {
                        revPoints[k].X = points[k].X;
                        for (int m = 0; m < ScanDataStacklist.Count; m++)
                        {
                            sum += ScanDataStacklist[m][k].Y;
                        }
                        revPoints[k].Y = sum / ScanDataStacklist.Count;
                        sum = 0;
                    }
                }
            }
            else
            {
                if (currentpoints == 705)
                {
                    ScanDataStacklist.RemoveAt(0);
                    ScanDataStacklist.Add(points);
                    for (int i = 0; i < revPoints.Length; i++)
                    {
                        revPoints[i].X = ScanDataStacklist[0][i].X;
                        for (int j = 0; j < ScanDataStacklist.Count; j++)
                        {
                            sum += ScanDataStacklist[j][i].Y;
                        }
                        revPoints[i].Y = sum / ScanDataStacklist.Count;
                        sum = 0;
                    }
                }
                else
                {
                    for (int i = 0; i < currentpoints; i++)
                    {
                        revPoints[i].X = points[i].X;
                        for (int j = 0; j < ScanDataStacklist.Count; j++)
                        {
                            sum += ScanDataStacklist[j][i].Y;
                        }
                        revPoints[i].Y = (sum + points[i].Y) / (ScanDataStacklist.Count + 1);
                        sum = 0;
                    }

                    for (int k = currentpoints; k < 705; k++)
                    {
                        revPoints[k].X = points[k].X;
                        for (int m = 0; m < ScanDataStacklist.Count; m++)
                        {
                            sum += ScanDataStacklist[m][k].Y;
                        }
                        revPoints[k].Y = sum / ScanDataStacklist.Count;
                        sum = 0;
                    }
                }
            }

            return revPoints;
        }

        #endregion

        #endregion

        #region ��ݲ˵����¼�����

        #region ATT��ݲ˵��¼�����
        /// <summary>
        /// ATT��ݲ˵��¼�����
        /// </summary>
        private void Dealing_ATT()
        {
            int oldIndex = MenuAttSelectedIndex;
            SelectMenuItem(1, ref MenuAttSelectedIndex);
            if (MenuAttSelectedIndex == 7)
            {
                FormAttOther frmAtt = new FormAttOther();
                frmAtt.InputAtt = CurrentScanParam.AnalysisAtt;
                int index = 0;
                int outAtt = 0;
                if (frmAtt.ShowDialog() == DialogResult.OK)
                {
                    outAtt = frmAtt.OutputAtt;
                    index = SpectrumATT.GetAttIndex(SpectrumType, outAtt);
                    if (index > 0)
                    {
                        MenuAttSelectedIndex = index;
                        CurrentScanParam.AnalysisAtt = SpectrumATT.GetAttValue(SpectrumType, index);
                    }
                    else
                    {
                        CurrentScanParam.AnalysisAtt = outAtt;
                    }
                }
            }
            else
            {
                CurrentScanParam.AnalysisAtt = SpectrumATT.GetAttValue(SpectrumType, MenuAttSelectedIndex);
            }

            if (MenuAttSelectedIndex != 0 && MenuAttSelectedIndex != oldIndex)
            {
                if (bIsAnalysis)
                {
                    ReStartAnalysis();
                }
                else
                {
                    RedrawWithNewParam();
                }
            }
        }

        #endregion


        #region RBW��ݲ˵��¼�����
        /// <summary>
        /// RBW��ݲ˵��¼�����
        /// </summary>
        private void Dealing_RBW()
        {
            int oldIndex = MenuRbwSelectedIndex;
            SelectMenuItem(0, ref MenuRbwSelectedIndex);

            if (MenuRbwSelectedIndex != oldIndex)
            {
                ////��RBWɨ���޶�����(Bird�ر���)
                //if (SpectrumType == SpectrumDef.ESpectrumType.Bird)
                //    UpadatScanParam(EParamChangeMode.Center_Span);

                if (bIsAnalysis)
                {
                    ReStartAnalysis();
                }
                else
                {
                    RedrawWithNewParam();
                }
            }
        }

        #endregion


        #region VBW��ݲ˵��¼�����
        /// <summary>
        /// VBW��ݲ˵��¼�����
        /// </summary>
        private void Dealing_VBW()
        {
            int oldIndex = MenuVbwSelectedIndex;
            SelectMenuItem(0, ref MenuVbwSelectedIndex);

            if (MenuVbwSelectedIndex != oldIndex)
            {
                if (bIsAnalysis)
                {
                    ReStartAnalysis();
                }
                else
                {
                    RedrawWithNewParam();
                }
            }
        }

        #endregion


        #region SPAN��ݲ˵��¼�����
        /// <summary>
        /// SPAN��ݲ˵��¼�����
        /// </summary>
        private void Dealing_SPAN()
        {
            int oldIndex = MenuSpanSelectedIndex;
            SelectMenuItem(0, ref MenuSpanSelectedIndex);

            //�ǳ����������
            if (MenuSpanSelectedIndex == 9)
            {
                FormSpanOther frmSpan = new FormSpanOther();
                frmSpan.InputSpan = CurrentScanParam.EndAnalysisFreq - CurrentScanParam.StartAnalysisFreq;
                int index = 0;
                if (frmSpan.ShowDialog() == DialogResult.OK)
                {
                    index = SpectrumSPAN.SpeCat2_GetSpanIndex(frmSpan.OutputSpan);
                    if (index > 0)
                    {
                        MenuSpanSelectedIndex = index;
                        CurrentScanParam.AnalysisSpan = SpectrumSPAN.SpeCat2_GetSpanValue(MenuSpanSelectedIndex);
                    }
                    else
                    {
                        CurrentScanParam.AnalysisSpan = frmSpan.OutputSpan;
                    }
                }
                else
                {
                    MenuSpanSelectedIndex = oldIndex;
                    return;
                }
            }
            else
            {
                CurrentScanParam.AnalysisSpan = SpectrumSPAN.SpeCat2_GetSpanValue(MenuSpanSelectedIndex);
            }

            UpadatScanParam(EParamChangeMode.Center_Span);
            if (bIsAnalysis)
            {
                ReStartAnalysis();
            }
            else
            {
                RedrawWithNewParam();
            }
        }

        #endregion


        #region HOLDMENU��ݲ˵��¼�����
        /// <summary>
        /// HOLDMENU��ݲ˵��¼�����
        /// </summary>
        private void Dealing_HOLDMENU()
        {
            int[] s = new int[5] { -1, -1, -1, -1, -1 }; 
            int oldIndex = MenuHoldSelectedIndex;
            SelectMenuItem(0, ref MenuHoldSelectedIndex);

            //�����ʷ����
            ScanDataStacklist.Clear();

            if (MenuHoldSelectedIndex == oldIndex)
            {
                plot.Trace(ChannelTraceType.cttMax, false);
                plot.Trace(ChannelTraceType.cttMin, false);
                plot.Trace(ChannelTraceType.cttAvg, false);
                plot.SetChannelVisible(0, true);
                plot.SetChannelVisible(1, false);
                plot.SetChannelVisible(2, false);
                plot.SetChannelVisible(3, false);
                plot.SetChannelVisible(4, false);
                s[0] = 0;
                plot.SetMarkSequence(0, s);
                NoneSelectItem(ref MenuHoldSelectedIndex);
            }
            else
            {
                plot.SetChannelVisible(0, false);
                plot.SetChannelVisible(4, false);
                if (MenuHoldSelectedIndex == 0)
                {
                    //���ֵ����
                    plot.SetChannelVisible(1, true);
                    plot.SetChannelVisible(2, false);
                    plot.SetChannelVisible(3, false);
                    plot.Trace(ChannelTraceType.cttMax, true);
                    s[0] = 1;
                    plot.SetMarkSequence(0, s);
                }
                else if (MenuHoldSelectedIndex == 1)
                {
                    //��Сֵ����
                    plot.SetChannelVisible(1, false);
                    plot.SetChannelVisible(2, true);
                    plot.SetChannelVisible(3, false);
                    plot.Trace(ChannelTraceType.cttMin, true);
                    s[0] = 2;
                    plot.SetMarkSequence(0, s);
                }
                else
                {
                    //ȡƽ��ֵ������ƽ�����ⲿ���þ���
                    if (App_Configure.Cnfgs.Spectrum == 1)
                    {
                        plot.SetChannelVisible(0, true);
                        plot.SetChannelVisible(1, false);
                        plot.SetChannelVisible(2, false);
                    }
                }
            }
        }

        #endregion


        #region SCALE��ݲ˵��¼�����
        /// <summary>
        /// SCALE��ݲ˵��¼�����
        /// </summary>
        private void Dealing_SCALE()
        {
            SelectMenuItem(0, ref MenuScaleSelectedIndex);
            RedrawWithNewParam();
        }

        #endregion

        #region SCALEֵ��������Ӧ��ϵ
        /// <summary>
        /// SCALEֵ��������Ӧ��ϵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>SCALE</returns>
        private float GetScaleValue(int index)
        {
            double revScale = 1;
            switch (index)
            {
                case 0:
                    revScale = 1;
                    break;
                case 1:
                    revScale = 0.5;
                    break;
                case 2:
                    revScale = 0.2;
                    break;
                default:
                    break;
            }

            return Convert.ToSingle(revScale);
        }

        #endregion


        #region UNIT��ݲ˵��¼�����
        /// <summary>
        /// UNIT��ݲ˵��¼�����
        /// </summary>
        private void Deal_UNIT()
        {
            SelectMenuItem(0, ref MenuUnitSelectedIndex);
            RedrawWithNewParam();
        }

        #endregion

        #region ������������Y���굥λת��ֵ
        /// <summary>
        /// ������������Y���굥λת��ֵ
        /// </summary>
        /// <param name="indxx">����</param>
        /// <returns>ת��ֵ</returns>
        private int GetYConversionValue(int index)
        {
            int revValue = 0;
            switch (index)
            {
                case 0:
                    revValue = 0;
                    break;
                case 1:
                    revValue = 113;
                    break;
                case 2:
                    revValue = 107;
                    break;
                default:
                    break;
            }

            return revValue;
        }

        #endregion


        #region REF POSITION��ݲ˵��¼�����
        /// <summary>
        /// REF POSITION��ݲ˵��¼�����
        /// </summary>
        private void Dealing_RefPosition()
        {
            SelectMenuItem(0, ref MenuRefPositionIndex);
            RedrawWithNewParam();
        }

        #endregion

        #region ������������ο�ֵ
        /// <summary>
        /// ������������ο�ֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>�ο�ֵ</returns>
        private int GetReferenceValue(int index)
        {
            int revValue = 0;
            switch (SpectrumType)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    revValue = index * 10;
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    revValue = index * 10;
                    break;
                default:
                    break;
            }

            return revValue;
        }

        #endregion


        #region MARK SELECT��ݲ˵��¼�����
        /// <summary>
        /// MARK SELECT��ݲ˵��¼�����
        /// </summary>
        private void Dealing_MarkSelect()
        {
            int oldIndex = MenuMarkSelectSelectedIndex;
            SelectMenuItem(0, ref MenuMarkSelectSelectedIndex);
            if (MenuMarkSelectSelectedIndex == 0)
            {
                plot.SetMarkVisible(0, false);
                plot.SetMarkVisible(1, false);
                plot.SetMarkVisible(2, false);
                plot.SetMarkVisible(3, false);
                plot.SetMarkVisible(4, false);
                NoneSelectItem(ref MenuMarkSelectSelectedIndex);
                ChangeBtnPic(pbxSpecPeak, "peak_in.gif");
            }
            else
            {
                if (MenuMarkSelectSelectedIndex == oldIndex)
                {
                    plot.SetMarkVisible(MenuMarkSelectSelectedIndex - 1, false);
                    NoneSelectItem(ref MenuMarkSelectSelectedIndex);
                    if (oldIndex == 1)
                    {
                        ChangeBtnPic(pbxSpecPeak, "peak_in.gif");
                    }
                }
                else
                {
                    plot.SetMarkVisible(MenuMarkSelectSelectedIndex - 1, true);
                    plot.Mark(CurrentScanParam.CenterAnalysisFreq / 1000.0f);
                }
            }
        }

        #endregion


        #region PEAK HOLD��ݲ˵��¼�����
        /// <summary>
        /// PEAK HOLD��ݲ˵��¼�����
        /// </summary>
        private void Dealing_PeakHold()
        {
            SelectMenuItem(0, ref MenuPeakHoldSelectedIndex);
            if (MenuPeakHoldSelectedIndex == 0)
            {
                NoneSelectItem(ref MenuPeakHoldSelectedIndex);
            }
        }

        #endregion


        #region TRACE SELECT��ݲ˵��¼�����
        /// <summary>
        /// TRACE SELECT��ݲ˵��¼�����
        /// </summary>
        private void Dealing_TraceSelect()
        {
            int oldIndex = MenuTraceSelectIndex;
            SelectMenuItem(0, ref MenuTraceSelectIndex);
            if (MenuTraceSelectIndex == 0)
            {
                NoneSelectItem(ref MenuTraceSelectIndex);
                ResetTraces();
            }
            else
            {
                if (MenuTraceSelectIndex != oldIndex)
                {
                    EnabledTraceButtons(true);
                    LoadTraceButttonStatus();
                }
            }
        }

        #endregion

        #region ����TRACES��ť״̬
        /// <summary>
        /// ����TRACES��ť״̬
        /// </summary>
        /// <param name="bStatus">true���� false������</param>
        private void EnabledTraceButtons(bool bStatus)
        {
            pbxReflesh.Enabled = bStatus;
            pbxMaxhold.Enabled = bStatus;
            pbxMinhold.Enabled = bStatus;
            pbxHold.Enabled = bStatus;
        }

        #endregion

        #region ���ø���TRACE��״̬
        /// <summary>
        /// ���ø���TRACE��״̬
        /// </summary>
        /// <param name="TraceNum">Trace���</param>
        /// <param name="FunNum">���ܱ�� 0-REFREASH,1-MAXHOLD,2-MINHOLD,3-HOLD</param>
        private void SetTraceFun(int TraceNum, int FunNum)
        {
            for (int i = 0; i < CurrentTraceStatus.Length; i++)
            {
                if (i != TraceNum)
                {
                    switch (FunNum)
                    {
                        case 0:
                            CurrentTraceStatus[i].bDown_REFREASH = false;
                            break;
                        case 1:
                            CurrentTraceStatus[i].bDown_MAXHOLD = false;
                            break;
                        case 2:
                            CurrentTraceStatus[i].bDown_MINHOLD = false;
                            break;
                        case 3:
                            CurrentTraceStatus[i].bDown_HOLD = false;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (FunNum)
                    {
                        case 0:
                            CurrentTraceStatus[i].bDown_REFREASH = true;
                            CurrentTraceStatus[i].bDown_MAXHOLD = false;
                            CurrentTraceStatus[i].bDown_MINHOLD = false;
                            CurrentTraceStatus[i].bDown_HOLD = false;
                            break;
                        case 1:
                            CurrentTraceStatus[i].bDown_REFREASH = false;
                            CurrentTraceStatus[i].bDown_MAXHOLD = true;
                            CurrentTraceStatus[i].bDown_MINHOLD = false;
                            CurrentTraceStatus[i].bDown_HOLD = false;
                            break;
                        case 2:
                            CurrentTraceStatus[i].bDown_REFREASH = false;
                            CurrentTraceStatus[i].bDown_MAXHOLD = false;
                            CurrentTraceStatus[i].bDown_MINHOLD = true;
                            CurrentTraceStatus[i].bDown_HOLD = false;
                            break;
                        case 3:
                            CurrentTraceStatus[i].bDown_REFREASH = false;
                            CurrentTraceStatus[i].bDown_MAXHOLD = false;
                            CurrentTraceStatus[i].bDown_MINHOLD = false;
                            CurrentTraceStatus[i].bDown_HOLD = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        #endregion

        #region ��λ����TRACE��״̬
        /// <summary>
        /// ��λ����TRACE��״̬
        /// </summary>
        private void ResetTraces()
        {
            for (int i = 0; i < CurrentTraceStatus.Length; i++)
            {
                CurrentTraceStatus[i].TraceIndex = i + 1;
                CurrentTraceStatus[i].bDown_REFREASH = false;
                CurrentTraceStatus[i].bDown_MAXHOLD = false;
                CurrentTraceStatus[i].bDown_MINHOLD = false;
                CurrentTraceStatus[i].bDown_HOLD = false;
            }

            ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
            ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
            ChangeBtnPic(pbxMinhold, "minhold_in.gif");
            ChangeBtnPic(pbxHold, "hold_in.gif");

            pbxReflesh.Enabled = false;
            pbxMaxhold.Enabled = false;
            pbxMinhold.Enabled = false;
            pbxHold.Enabled = false;

            plot.SetChannelVisible(0, true);
            plot.SetChannelVisible(1, false);
            plot.SetChannelVisible(2, false);
            plot.SetChannelVisible(3, false);
            plot.SetChannelVisible(4, false);

            plot.SetChannelColor(0, System.Drawing.Color.Yellow);
        }

        #endregion

        #region ��ʾ��ǰ���Trace
        /// <summary>
        /// ��ʾ��ǰ���Trace
        /// </summary>
        private void ShowTrace()
        {
            int index = MenuTraceSelectIndex;
            for (int i = 0; i < CurrentTraceStatus.Length; i++)
            {
                if (CurrentTraceStatus[i].TraceIndex == index)
                {
                    if (CurrentTraceStatus[i].bDown_REFREASH)
                    {
                        plot.SetChannelColor(0, SetTraceLineColor(index));
                        plot.Trace(ChannelTraceType.cttRlt, true);
                        plot.Trace(ChannelTraceType.cttMax, false);
                        plot.Trace(ChannelTraceType.cttMin, false);
                        plot.Trace(ChannelTraceType.cttHld, false);
                    }
                    if (CurrentTraceStatus[i].bDown_MAXHOLD)
                    {
                        plot.SetChannelColor(0, Color.Yellow);
                        plot.SetChannelColor(1, SetTraceLineColor(index));
                        plot.Trace(ChannelTraceType.cttRlt, true);
                        plot.Trace(ChannelTraceType.cttMax, true);
                        plot.Trace(ChannelTraceType.cttMin, false);
                        plot.Trace(ChannelTraceType.cttHld, false);
                    }
                    if (CurrentTraceStatus[i].bDown_MINHOLD)
                    {
                        plot.SetChannelColor(0, Color.Yellow);
                        plot.SetChannelColor(2, SetTraceLineColor(index));
                        plot.Trace(ChannelTraceType.cttRlt, true);
                        plot.Trace(ChannelTraceType.cttMax, false);
                        plot.Trace(ChannelTraceType.cttMin, true);
                        plot.Trace(ChannelTraceType.cttHld, false);
                    }
                    if (CurrentTraceStatus[i].bDown_HOLD)
                    {
                        plot.SetChannelColor(0, Color.Yellow);
                        plot.SetChannelColor(4, SetTraceLineColor(index));
                        plot.Trace(ChannelTraceType.cttRlt, true);
                        plot.Trace(ChannelTraceType.cttMax, false);
                        plot.Trace(ChannelTraceType.cttMin, false);
                        plot.Trace(ChannelTraceType.cttHld, true);
                    }
                }
            }

            for (int k = 0; k < CurrentTraceStatus.Length; k++)
            {
                if (CurrentTraceStatus[k].TraceIndex != index)
                {
                    if (CurrentTraceStatus[k].bDown_REFREASH)
                    {
                        plot.SetChannelColor(0, SetTraceLineColor(CurrentTraceStatus[k].TraceIndex));
                    }
                    if (CurrentTraceStatus[k].bDown_MAXHOLD)
                    {
                        plot.SetChannelColor(1, SetTraceLineColor(CurrentTraceStatus[k].TraceIndex));
                        plot.Trace(ChannelTraceType.cttMax, true);
                    }
                    if (CurrentTraceStatus[k].bDown_MINHOLD)
                    {
                        plot.SetChannelColor(2, SetTraceLineColor(CurrentTraceStatus[k].TraceIndex));
                        plot.Trace(ChannelTraceType.cttMin, true);
                    }
                    if (CurrentTraceStatus[k].bDown_HOLD)
                    {
                        plot.SetChannelColor(4, SetTraceLineColor(CurrentTraceStatus[k].TraceIndex));
                        plot.Trace(ChannelTraceType.cttHld, true);
                    }
                }
            }
        }

        #endregion

        #region ����TRACE�߰�ť״̬
        /// <summary>
        /// ����TRACE�߰�ť״̬
        /// </summary>
        private void LoadTraceButttonStatus()
        {
            if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_REFREASH)
            {
                ChangeBtnPic(pbxReflesh, "reflesh.gif");
            }
            else
            {
                ChangeBtnPic(pbxReflesh, "reflesh_in.gif");
            }

            if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MAXHOLD)
            {
                ChangeBtnPic(pbxMaxhold, "maxhold.gif");
            }
            else
            {
                ChangeBtnPic(pbxMaxhold, "maxhold_in.gif");
            }

            if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_MINHOLD)
            {
                ChangeBtnPic(pbxMinhold, "minhold.gif");
            }
            else
            {
                ChangeBtnPic(pbxMinhold, "minhold_in.gif");
            }

            if (CurrentTraceStatus[MenuTraceSelectIndex - 1].bDown_HOLD)
            {
                ChangeBtnPic(pbxHold, "hold.gif");
            }
            else
            {
                ChangeBtnPic(pbxHold, "hold_in.gif");
            }
        }

        #endregion

        #region ����TRACE�߶�Ӧ��ɫ
        /// <summary>
        /// ����TRACE�߶�Ӧ��ɫ
        /// </summary>
        /// <param name="TraceIndex">Trace���</param>
        private System.Drawing.Color SetTraceLineColor(int TraceIndex)
        {
            System.Drawing.Color revColor = Color.Yellow;
            switch (TraceIndex)
            {
                case 1:
                    revColor = Color.Green;
                    break;
                case 2:
                    revColor = Color.Red;
                    break;
                case 3:
                    revColor = Color.Purple;
                    break;
                case 4:
                    revColor = Color.Orange;
                    break;
                case 5:
                    revColor = Color.DarkBlue;
                    break;
                default:
                    break;
            }

            return revColor;
        }

        #endregion


        #region DATASAVE��ݲ˵��¼�����
        /// <summary>
        /// DATASAVE��ݲ˵��¼�����
        /// </summary>
        private void Deal_DATASAVE()
        {
            if (lblTim.Text != "")
                return;

            bool bSaveCsv = false;
            bool bSaveJpg = false;
            bool bCsvOk = false;
            bool bJpgOk = false;
            string file_csv = "";
            string file_jpg = "";
            plot.Save(0);
            if (SaveDataObj == null)
            {
                MessageBox.Show(this, "No data can be save currently!");
                return;
            }

            FormSaveData frmSave = new FormSaveData();
            if (frmSave.ShowDialog() == DialogResult.OK)
            {
                if (frmSave.bEnableCsv)
                {
                    file_csv = frmSave.CsvFileName;
                    bSaveCsv = true;
                }
                if (frmSave.bEnableJpg)
                {
                    file_jpg = frmSave.JpgFileName;
                    bSaveJpg = true;
                }
            }

            //����CSV
            if (bSaveCsv)
            {
                if (ValidateFileName(file_csv.Substring(file_csv.LastIndexOf("\\") + 1)))
                {
                    if (!File.Exists(file_csv))
                    {
                        CsvReport_Spctrum_Header head = new CsvReport_Spctrum_Header();
                        head.Mac_Desc = App_Configure.Cnfgs.Mac_Desc;
                        head.Start = CurrentScanParam.StartAnalysisFreq / 1000f;
                        head.Stop = CurrentScanParam.EndAnalysisFreq / 1000f;
                        head.ATT = CurrentScanParam.AnalysisAtt;
                        head.RBW = SpectrumRBW.GetRbwRealValue(SpectrumType, SpectrumRBW.GetRbwValue(SpectrumType, MenuRbwSelectedIndex));
                        head.Date_Time = DateTime.Now.ToString();

                        List<PointF> points = new List<PointF>();
                        PointF[][] SaveData = plot.DataOfScreen();
                        for (int i = 0; i < SaveData.Length; i++)
                        {
                            for (int j = 0; j < SaveData[i].Length; j++)
                            {
                                points.Add(SaveData[i][j]);
                            }
                        }

                        CsvReport.Save_Csv_Spectrum(file_csv, points.ToArray(), head);
                        bCsvOk = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "The CSV file name has already existed!");
                        return;
                    }
                }
            }


            //����JPG
            if (bSaveJpg)
            {
                if (ValidateFileName(file_jpg.Substring(file_jpg.LastIndexOf("\\") + 1)))
                {
                    if (!File.Exists(file_jpg))
                    {
                        string strTitle = "";
                        Image SpectrumImage = JpgReport.GetWindow(this.Handle);
                        Graphics g = Graphics.FromImage(SpectrumImage);
                        StringFormat drawFormat = new StringFormat();
                        drawFormat.Alignment = StringAlignment.Near;
                        strTitle = "[JPG File Save Time] " + DateTime.Now.ToString();
                        g.DrawImage(SpectrumImage, new Point(0, 0));
                        g.DrawString(strTitle, new Font("Tahoma", 12, FontStyle.Regular), new SolidBrush(Color.White),
                                                        new PointF(620, 70), drawFormat);
                        SpectrumImage.Save(file_jpg);
                        bJpgOk = true;
                    }
                    else
                    {
                        MessageBox.Show(this, "The JPG file name has already existed!");
                        return;
                    }
                }
            }

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
                    strMsg += "JPG file save OK!";
                else
                    strMsg += "JPG file save failed!";
            }


            if (bSaveCsv | bSaveJpg)
            {
                //RedrawWithNewParam();
                this.Refresh();
                MessageBox.Show(this, strMsg);
            }
        }

        #endregion


        #region  DATAREAD��ݲ˵��¼�����
        /// <summary>
        /// DATAREAD��ݲ˵��¼�����
        /// </summary>
        private void Deal_DATAREAD()
        {
            //FormDataRead frmDataRead = new FormDataRead();
            IsoReadDataForm frmDataRead = new IsoReadDataForm();
            frmDataRead.FillFiles(App_Configure.Cnfgs.Path_Rpt_Spc + "\\csv");
            if (frmDataRead.ShowDialog() == DialogResult.OK)
            {
                string FilePath = App_Configure.Cnfgs.Path_Rpt_Spc + "\\csv\\" + frmDataRead.FileName;

                if (File.Exists(FilePath))
                {
                    CsvReport_Spctrum_Header head = new CsvReport_Spctrum_Header();
                    List<PointF> historyData = new List<PointF>();
                    CsvReport.Read_Csv_Spectrum(FilePath, out historyData, out head);
                    CurrentScanParam.StartAnalysisFreq = (int)(head.Start * 1000);
                    CurrentScanParam.EndAnalysisFreq = (int)(head.Stop * 1000);
                    CurrentScanParam.AnalysisAtt = head.ATT;
                    lblTim.Text = "Replay: " + head.Date_Time.ToString();

                    MenuRbwSelectedIndex = SpectrumRBW.GetIndexByValue(SpectrumType, head.RBW);
                    UpadatScanParam(EParamChangeMode.Start_End);
                    RedrawWithNewParam();
                    plot.Clear();
                    plot.Add(historyData.ToArray(), 0, 0);
                }
                else
                {
                    MessageBox.Show(this, "The file does not exist!");
                }
            }
        }

        #endregion

        #endregion

        #region ������Ϣ����
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == MsgReciveData)
            {
                PaintPointFs = (PointF[])ISpectrumObj.GetSpectrumData();
                PaintPointFs = CountByUnit(PaintPointFs);
                if (bEnableOffset)
                {
                    PaintPointFs = DataValueOffset(PaintPointFs);
                }
                //����˥������ֵ
                for (int i = 0; i < PaintPointFs.Length; i++)
                {
                    PaintPointFs[i].Y += App_Settings.spc.Rev;
                }

                //����N��ƽ��ֵ(Bird����)
                try
                {
                    if (App_Configure.Cnfgs.Spectrum == 1 && MenuHoldSelectedIndex == 2)
                        PaintPointFs = CountAverage(PaintPointFs, m.WParam.ToInt32());
                }
                catch(Exception ex)
                {
                    if (bIsAnalysis)
                    {
                        StopAnalysis();
                    }
                    MessageBox.Show(this, "Count average error!");
                }

                plot.Add(PaintPointFs, 0, m.LParam.ToInt32());
            }
            else if (m.Msg == MsgReciveWarning)
            {
                if (bIsAnalysis)
                {
                    StopAnalysis();
                }
                MessageBox.Show(this, "Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");
            }
            else if (m.Msg == MessageID.RF_SUCCED_ALL)
            {
                if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr1)
                {
                    power1_Handle.Set();
                }
                if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr2)
                {
                    power2_Handle.Set();
                }
            }
            else if (m.Msg == MessageID.RF_ERROR)
            {
                if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr1)
                {
                    MessageBox.Show(this, "PA1 operation failed");
                    lblRF_1.Text = "PA1: OFF";
                    CloseRF_1();
                    ChangeBtnPic(pbxSpecRF, "rf_in.gif");
                    bRFon = false;
                }
                if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr2)
                {
                    MessageBox.Show(this, "PA2 operation failed");
                    lblRF_2.Text = "PA2: OFF";
                    CloseRF_2();
                    ChangeBtnPic(pbxSpecRF, "rf_in.gif");
                    bRFon = false;
                }
            }
            else
                base.WndProc(ref m);
        }

        #endregion

        #region ISweep ��Ա

        public void BreakSweep(int timeOut)
        {
            StopAnalysis();
            CloseRF_1();
            CloseRF_2();

            bRFon = false;
            ChangeBtnPic(pbxSpecRF, "rf_in.gif");
            tmRF.Stop();
        }

        #endregion

        #region Ƶ��ģ�鿪


        /// <summary>
        /// ���Ŷ�ʱ���ļ�����
        /// </summary>
        private int CountNumber = 0;

        /// <summary>
        /// RF������ʶ
        /// </summary>
        private bool bRFon = false;

        /// <summary>
        /// ִ��ɨ���������ɨ�躯���У�Ӧ��ʹ�ô˲���
        /// ����usr_sweeps���ƹ���
        /// </summary>
        private SweepParams exe_params;

        /// <summary>
        /// ����1�ȴ����
        /// </summary>
        private ManualResetEvent power1_Handle;

        /// <summary>
        /// ����2�ȴ����
        /// </summary>
        private ManualResetEvent power2_Handle;

        /// <summary>
        /// ����1״̬��Ϣ����
        /// </summary>
        private PowerStatus rfStatus_1;

        /// <summary>
        /// ����2״̬��Ϣ����
        /// </summary>
        private PowerStatus rfStatus_2;

        /// <summary>
        /// ����1�쳣��Ϣ����
        /// </summary>
        private RFErrors rfErrors_1;

        /// <summary>
        /// ����2�쳣��Ϣ����
        /// </summary>
        private RFErrors rfErrors_2;

        /// <summary>
        /// ����һ�߳�
        /// </summary>
        private Thread thdRF_1;

        /// <summary>
        /// ���Ŷ��߳�
        /// </summary>
        private Thread thdRF_2;

        #region ��������

        private void StartRF_1()
        {
            bool bTimeout = false;
            bool bErrors = false;
            float freq_1 = FormRF.FreqRF_1;
            float power_1 = FormRF.TxRF_1;
            rfStatus_1 = new PowerStatus();
            rfErrors_1 = new RFErrors();
            exe_params.TmeParam.F1 = freq_1;
            exe_params.TmeParam.P1 = power_1;
            exe_params.DevInfo.RF_Addr1 = App_Configure.Cnfgs.ComAddr1;
            exe_params.RFInvolved = RFInvolved.Rf_1;

            bTimeout = RF_Do(exe_params.DevInfo.RF_Addr1,
                  exe_params.RFPriority,
                  exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                  true, false, true, true, ref rfStatus_1, true);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors = CheckRF_1(bTimeout);
            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                return;
            }
        }

        private void StartRF_2()
        {
            bool bTimeout = false;
            bool bErrors = false;
            float freq_2 = FormRF.FreqRF_2;
            float power_2 = FormRF.TxRF_2;
            rfStatus_2 = new PowerStatus();
            rfErrors_2 = new RFErrors();
            exe_params.TmeParam.F2 = freq_2;
            exe_params.TmeParam.P2 = power_2;
            exe_params.DevInfo.RF_Addr2 = App_Configure.Cnfgs.ComAddr2;
            exe_params.RFInvolved = RFInvolved.Rf_2;

            bTimeout = RF_Do(exe_params.DevInfo.RF_Addr2,
                 exe_params.RFPriority,
                 exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                 true, false, true, true, ref rfStatus_2, true);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors = CheckRF_2(bTimeout);
            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }
        }

        #endregion

        #region  �رչ���

        private void CloseRF_1()
        {
            if (StopRF(RFInvolved.Rf_1))
                lblRF_1.Text = "RF1: OFF";
        }

        private void CloseRF_2()
        {
            if (StopRF(RFInvolved.Rf_2))
                lblRF_2.Text = "RF2: OFF";
        }

        #endregion

        #region ���͹�������
        /// <summary>
        /// ���͹�������
        /// </summary>
        /// <param name="Addr"></param>
        /// <param name="Lvl"></param>
        /// <param name="P"></param>
        /// <param name="F"></param>
        /// <param name="RFon"></param>
        /// <param name="ignoreRFon"></param>
        /// <param name="useP"></param>
        /// <param name="useF"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        private bool RF_Do(int Addr,
                   int Lvl,
                   float P,
                   float F,
                   bool RFon,
                   bool ignoreRFon,
                   bool useP,
                   bool useF,
                   ref PowerStatus status, bool bWait)
        {
            bool RF_Succ = false;

            RFSignal.RFClear(Addr, Lvl);

            if (useP)
                RFSignal.RFPower(Addr, Lvl, P);

            if (useF)
                RFSignal.RFFreq(Addr, Lvl, F);

            if (!ignoreRFon)
            {
                if (RFon)
                    RFSignal.RFOn(Addr, Lvl);
                else
                    RFSignal.RFOff(Addr, Lvl);
            }

            RFSignal.RFSample(Addr, Lvl);

            RFSignal.RFStart(Addr);

            if (bWait)
            {
                //�ȴ�����
                if (Addr == exe_params.DevInfo.RF_Addr1)
                {
                    RF_Succ = power1_Handle.WaitOne(2000, true);
                    power1_Handle.Reset();
                }
                else
                {
                    RF_Succ = power2_Handle.WaitOne(2000, true);
                    power2_Handle.Reset();
                }

                //û�з�������ͨ�ų�ʱ�����ȡ����״̬��Ϣ
                if (RF_Succ)
                    RFSignal.RFStatus(Addr, ref status);

                //string str = "RF_" + Addr.ToString() + "\r\n ";
                //str += "RFOn: " + status.Status2.RFOn.ToString() + "\r\n";
                //str += "Freq: " + status.Status2.Freq.ToString() + "\r\n";
                //str += "OutP: " + status.Status2.OutP.ToString() + "\r\n";
                //str += "Current: " + status.Status2.Current.ToString() + "\r\n";
                //str += "Temp: " + status.Status2.Temp.ToString() + "\r\n";
                //str += "Vswr: " + status.Status2.Vswr.ToString() + "\r\n";
                //str += "CurrErr: " + status.Status2.CurrErr.ToString() + "\r\n";
                //str += "TempErr: " + status.Status2.TempErr.ToString() + "\r\n";
                //str += "RftErr: " + status.Status2.RftErr.ToString() + "\r\n";
                //Log.WriteLog(str, Log.EFunctionType.SPECTRUM);
            }

            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
        }

        #endregion

        #region ֹͣ���Ų���
        /// <summary>
        /// ֹͣ���Ų���
        /// </summary>
        /// <param name="Num">���ű��</param>
        private bool StopRF(RFInvolved Num)
        {
            bool f = false;

            if (Num == RFInvolved.Rf_1)
            {
                f = RF_Do(App_Configure.Cnfgs.ComAddr1,
                      exe_params.RFPriority,
                      exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                      false, false, false, false, ref rfStatus_1, false);
            }
            else
            {
                f = RF_Do(App_Configure.Cnfgs.ComAddr2,
                     exe_params.RFPriority,
                     exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                     false, false, false, false, ref rfStatus_1, false);
            }
            return f;
        }

        #endregion

        #region �����쳣���
        /// <summary>
        /// ����ȫ�־�̬�Ĺ����豸�������������쳣���
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_1(bool timeOut)
        {
            bool errors = false;

            rfErrors_1.RF_TimeOut = timeOut;
            if (timeOut)
                Log.WriteLog("RF1ͨѶ��ʱ!", Log.EFunctionType.SPECTRUM);

            else
            {
                if ((rfStatus_1.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                    (rfStatus_1.Status2.Current > App_Configure.Cnfgs.Max_Curr))
                {
                    errors = true;
                    rfErrors_1.RF_CurrError = true;
                    rfErrors_1.RF_CurrValue = rfStatus_1.Status2.Current;
                    Log.WriteLog("RF1�����쳣!  I1=" + rfStatus_1.Status2.Current.ToString(), Log.EFunctionType.SPECTRUM);
                }

                if ((rfStatus_1.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                    (rfStatus_1.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
                {
                    errors = true;
                    rfErrors_1.RF_TempError = true;
                    rfErrors_1.RF_TempValue = rfStatus_1.Status2.Temp;
                    Log.WriteLog("RF1�¶��쳣!  Temp1=" + rfStatus_1.Status2.Temp.ToString(), Log.EFunctionType.SPECTRUM);
                }

                if ((rfStatus_1.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr))
                {
                    errors = true;
                    rfErrors_1.RF_VswrError = true;
                    rfErrors_1.RF_VswrValue = rfStatus_1.Status2.Vswr;
                    Log.WriteLog("RF1פ���쳣!  VSWR1=" + rfStatus_1.Status2.Vswr.ToString(), Log.EFunctionType.SPECTRUM);
                }
            }

            return (errors || timeOut);
        }

        /// <summary>
        /// ����ȫ�־�̬�Ĺ����豸�������������쳣���
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_2(bool timeOut)
        {
            bool errors = false;

            rfErrors_2.RF_TimeOut = timeOut;
            if (timeOut)
                Log.WriteLog("RF2ͨѶ��ʱ!", Log.EFunctionType.SPECTRUM);

            else
            {
                if ((rfStatus_2.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                    (rfStatus_2.Status2.Current > App_Configure.Cnfgs.Max_Curr))
                {
                    errors = true;
                    rfErrors_2.RF_CurrError = true;
                    rfErrors_2.RF_CurrValue = rfStatus_2.Status2.Current;
                    Log.WriteLog("RF2�����쳣!  I2=" + rfStatus_2.Status2.Current.ToString(), Log.EFunctionType.SPECTRUM);
                }

                if ((rfStatus_2.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                    (rfStatus_2.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
                {
                    errors = true;
                    rfErrors_2.RF_TempError = true;
                    rfErrors_2.RF_TempValue = rfStatus_2.Status2.Temp;
                    Log.WriteLog("RF2�¶��쳣!  Temp2=" + rfStatus_2.Status2.Temp.ToString(), Log.EFunctionType.SPECTRUM);
                }

                if (rfStatus_2.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr)
                {
                    errors = true;
                    rfErrors_2.RF_VswrError = true;
                    rfErrors_2.RF_VswrValue = rfStatus_2.Status2.Vswr;
                    Log.WriteLog("RF2פ���쳣!  VSWR2=" + rfStatus_2.Status2.Vswr.ToString(), Log.EFunctionType.SPECTRUM);
                }
            }

            return (errors || timeOut);
        }

        #endregion

        private void lblRF1_MouseClick(object sender, MouseEventArgs e)
        {
            FormRF frmRF = new FormRF(1);
            frmRF.ShowDialog();
        }

        private void lblRF2_MouseClick(object sender, MouseEventArgs e)
        {
            FormRF frmRF = new FormRF(2);
            frmRF.ShowDialog();
        }

        private void tmRF_Tick(object sender, EventArgs e)
        {
            CountNumber++;
            if (CountNumber == App_Settings.spc.TimeRF)
            {
                CountNumber = 0;
                CloseRF_1();
                CloseRF_2();
                ChangeBtnPic(pbxSpecRF, "rf_in.gif");
                bRFon = false;
                MessageBox.Show(this, "Time over");
                tmRF.Stop();
            }
        }

        #endregion

        #region ================================= Add by NQ =================================
        public void ChangeRevFwd()
        {
            if (App_Configure.Cnfgs.Channel == 0)
            {
                this.lblChannel.Text = "Narrow";
            }
            else
            {
                this.lblChannel.Text = "Wide";
            }
        }
        #endregion

        private void pbxScanband_MouseClick(object sender, MouseEventArgs e)
        {
            FormScanCount frmscancount = new FormScanCount();
            frmscancount.Scancount = Scancount;
            if (frmscancount.ShowDialog() == DialogResult.OK)
            {
                Scancount = int.Parse(frmscancount.txtcount.Text.Trim());
                if (bIsAnalysis)
                {
                    ReStartAnalysis();
                }
            }
        }
    }
}