// ===============================================================================
// 文件名：FormCalProgress
// 创建人：倪骞
// 日  期：2011-5-19 
//
// 描  述：VSWR自动校准进度条
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-5-19    	   创建该文件
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace jcPimSoftware
{
    public partial class FormCalProgress : Form
    {
        #region 变量定义

        /// <summary>
        /// 用户配置信息
        /// </summary>
        private Settings_Vsw settings;

        /// <summary>
        /// 驻波分析对象
        /// </summary>
        private Vsw_Sweep SweepObj = new Vsw_Sweep();

        /// <summary>
        /// 功放一驻波校准对象集合
        /// </summary>
        private List<jcPimSoftware.VswrForm.CalibrationObj> listCurrentCAL_1 = new List<jcPimSoftware.VswrForm.CalibrationObj>();
        public List<jcPimSoftware.VswrForm.CalibrationObj> ListCurrentCAL_1
        {
            get { return listCurrentCAL_1; }
            set { listCurrentCAL_1 = value; }
        }
        /// <summary>
        /// 功放二驻波校准对象集合
        /// </summary>
        private List<jcPimSoftware.VswrForm.CalibrationObj> listCurrentCAL_2 = new List<jcPimSoftware.VswrForm.CalibrationObj>();
        public List<jcPimSoftware.VswrForm.CalibrationObj> ListCurrentCAL_2
        {
            get { return listCurrentCAL_2; }
            set { listCurrentCAL_2 = value; }
        }

        /// <summary>
        /// 功放编号
        /// </summary>
        private RFInvolved rf_num = RFInvolved.Rf_1;
        internal RFInvolved Rf_num
        {
            set { rf_num = value; }
        }

        #region 设备状态对象与扫描结构对象，由sweepobj.CloneReference赋值
        private PowerStatus ps1;
        private PowerStatus ps2;
        private SweepResult sr;
        private RFErrors rfr_errors1;
        private RFErrors rfr_errors2;
        #endregion

        /// <summary>
        /// 自动校准标识 true成功 false失败
        /// </summary>
        bool bCalSuccess = true;

        /// <summary>
        /// 功放错误标识
        /// </summary>
        bool bErrorRf = false;

        /// <summary>
        /// 频谱错误标识
        /// </summary>
        bool bErrorSpec = false;

        #endregion

        #region 窗体构造
        /// <summary>
        /// 窗体构造
        /// </summary>
        internal FormCalProgress(Settings_Vsw vswr_settings)
        {
            InitializeComponent();
            settings = vswr_settings;
        }

        #endregion

        #region 窗体加载
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormCalProgress_Load(object sender, EventArgs e)
        {
            GetCalFreqList(RFInvolved.Rf_1);
            GetCalFreqList(RFInvolved.Rf_2);
            progressBar1.Step = 1;
            AutoCAL(rf_num);
        }

        #endregion

        #region 辅助方法

        #region 获取校准频率列表
        /// <summary>
        /// 获取校准频率列表
        /// </summary>
        /// <param name="RF_Num">功放编号</param>
        private void GetCalFreqList(RFInvolved RF_Num)
        {
            jcPimSoftware.VswrForm.CalibrationObj CalObj = new VswrForm.CalibrationObj();
            if (RF_Num == RFInvolved.Rf_1)
            {
                List<RL0_TableItem> listCAL = RL0_Tables.Items(FuncModule.VSW, RFInvolved.Rf_1);
                for (int i = 0; i < listCAL.Count; i++)
                {
                    CalObj.Freq = listCAL[i].F;
                    CalObj.RL0 = listCAL[i].RL;
                    listCurrentCAL_1.Add(CalObj);
                }
                progressBar1.Maximum = listCurrentCAL_1.Count;
                progressBar1.Step = 1;
            }
            if (RF_Num == RFInvolved.Rf_2)
            {
                List<RL0_TableItem> listCAL = RL0_Tables.Items(FuncModule.VSW, RFInvolved.Rf_2);
                for (int i = 0; i < listCAL.Count; i++)
                {
                    CalObj.Freq = listCAL[i].F;
                    CalObj.RL0 = listCAL[i].RL;
                    listCurrentCAL_2.Add(CalObj);
                }
                progressBar1.Maximum = listCurrentCAL_2.Count;
                progressBar1.Step = 1;
            }
        }

        #endregion

        #region 获取频谱扫描序列
        /// <summary>
        /// 获取频谱扫描序列
        /// </summary>
        /// <param name="ObjList">频率校准列表</param>
        /// <returns>扫描序列</returns>
        private FreqSweepItem[] GetItemList(List<jcPimSoftware.VswrForm.CalibrationObj> ObjList)
        {
            FreqSweepItem[] revItemList = new FreqSweepItem[ObjList.Count];
            for (int i = 0; i < revItemList.Length; i++)
            {
                FreqSweepItem item = new FreqSweepItem();
                if (rf_num == RFInvolved.Rf_1)
                {
                    item.Tx1 = ObjList[i].Freq;
                    if (!App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        item.P1 = (float)App_Factors.vsw_tx1.ValueWithOffset(item.Tx1, this.settings.Tx);
                    }
                    else
                    {
                        item.P1 = Tx_Tables.vsw_tx1.Offset(item.Tx1, this.settings.Tx, Tx_Tables.vsw_offset1) + this.settings.Tx;
                    }
                }
                else
                {
                    item.Tx2 = ObjList[i].Freq;
                    if (!App_Configure.Cnfgs.Cal_Use_Table)
                    {
                        item.P2 = (float)App_Factors.vsw_tx1.ValueWithOffset(item.Tx2, this.settings.Tx);
                    }
                    else
                    {
                        item.P2 += Tx_Tables.vsw_tx1.Offset(item.Tx2, this.settings.Tx, Tx_Tables.vsw_offset2) + this.settings.Tx;
                    }
                }
                item.Rx = ObjList[i].Freq;
                revItemList[i] = item;
            }

            return revItemList;
        }

        #endregion

        #region 启动自动校准
        /// <summary>
        /// 启动自动校准
        /// </summary>
        /// <param name="RF_Num">功放编号</param>
        private void AutoCAL(RFInvolved RF_Num)
        {
            SweepParams sp = new SweepParams();
            sp.WndHandle = this.Handle;
            sp.SweepType = SweepType.Freq_Sweep;
            sp.FrqParam = new FreqSweepParam();

            if (RF_Num == RFInvolved.Rf_1)
            {
                sp.RFInvolved = RFInvolved.Rf_1;
                sp.FrqParam.P1 = this.settings.Tx;
                sp.FrqParam.Items1 = GetItemList(listCurrentCAL_1);
            }
            if (RF_Num == RFInvolved.Rf_2)
            {
                sp.RFInvolved = RFInvolved.Rf_2;
                sp.FrqParam.P2 = this.settings.Tx;
                sp.FrqParam.Items2 = GetItemList(listCurrentCAL_2);
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
            sp.SpeParam.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;
            sp.SpeParam.Continued = false;
            sp.SpeParam.DeliSpe = SpectrumLib.Defines.CommonDef.SpectrumType.Deli_VSWR;

            SweepObj.InitSweep();
            SweepObj.Prepare(sp);
            SweepObj.StartSweep();
        }

        #endregion

        #region 停止自动校准
        /// <summary>
        /// 停止自动校准
        /// </summary>
        private void StopCal()
        {
            SweepObj.StopSweep(1000);
        }

        #endregion

        #region 填充校准数据
        /// <summary>
        /// 填充校准数据
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>true校准通过 false校准失败 (判断外接衰减器标称值是否与实际测得值相符)</returns>
        private void FillCalData(int index)
        {
            //float offset = 0;

            Vsw_Sweep.ResultObj Obj = SweepObj.GetVswrScanResult();
            float Freq = Obj.Pstatus.Status2.Freq;
            float Tx = Obj.Pstatus.Status2.OutP;
            float Rx = Obj.Sstatus.dBmValue;

            if (Freq >= App_Settings.sgn_1.Min_Freq && Freq <= App_Settings.sgn_1.Max_Freq)
            {
                //offset = (Tx - Rx) - listCurrentCAL_1[index].RL0 - settings.Attenuator;
                //if (Math.Abs(offset) > settings.Offset)
                //    bCalSuccess &= false;

                jcPimSoftware.VswrForm.CalibrationObj CalObj = new VswrForm.CalibrationObj();
                CalObj.Freq = listCurrentCAL_1[index].Freq;
                //CalObj.RL0 = (Tx - Rx) - listCurrentCAL_1[index].RL0;
                CalObj.RL0 = listCurrentCAL_1[index].RL0;
                CalObj.Tx0 = Tx;
                CalObj.Rx0 = Rx;
                listCurrentCAL_1[index] = CalObj;
            }
            if (Freq >= App_Settings.sgn_2.Min_Freq && Freq <= App_Settings.sgn_2.Max_Freq)
            {
                //offset = (Tx - Rx) - listCurrentCAL_2[index].RL0 - settings.Attenuator;
                //if (Math.Abs(offset) > settings.Offset)
                //    bCalSuccess &= false;

                jcPimSoftware.VswrForm.CalibrationObj CalObj = new VswrForm.CalibrationObj();
                CalObj.Freq = listCurrentCAL_2[index].Freq;
                //CalObj.RL0 = (Tx - Rx) - listCurrentCAL_2[index].RL0;
                CalObj.RL0 = listCurrentCAL_2[index].RL0;
                CalObj.Tx0 = Tx;
                CalObj.Rx0 = Rx;
                listCurrentCAL_2[index] = CalObj;
            }

            if (index + 1 <= progressBar1.Maximum)
                progressBar1.Value++;
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
                    if (!bCalSuccess)
                    {
                        if (bErrorRf)
                        {
                            SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);
                            MessageBox.Show(this,rfr_errors1.ToString() + "\n\r" + rfr_errors2.ToString());
                        }
                        if (bErrorSpec)
                        {
                            MessageBox.Show(this,"Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");
                        }
                        //if (bErrorRf == false && bErrorSpec == false)
                        //{
                        //    MessageBox.Show(this, "AutoCAL failed,please check your attenuator!");
                        //}
                        this.DialogResult = DialogResult.Cancel;
                    }
                    else
                        this.DialogResult = DialogResult.OK;
                    break;
                //完成单点扫描
                case MessageID.VSW_SUCCED:
                    FillCalData(m.LParam.ToInt32());
                    break;
                //功放操作错误
                case MessageID.RF_ERROR:
                    bCalSuccess = false;
                    bErrorRf = true;
                    break;
                //频谱分析错误
                case MessageID.SPECTRUM_ERROR:
                    bCalSuccess = false;
                    bErrorSpec = true;
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
    }
}