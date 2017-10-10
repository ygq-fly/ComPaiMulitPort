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
    public partial class IsoCALForm : Form
    {
        #region 隔离度模块的实例变量
        /// <summary>
        /// 配置项对象，由构造函数传入
        /// </summary>
        private Settings_Iso settings;

        /// <summary>
        /// 隔离度分析对象，由构造函数传入
        /// </summary>
        private Iso_Sweep SweepObj;

        /// <summary>
        /// 扫描参数对象，每次启动扫描都被重新构建 
        /// </summary>
        private SweepParams sweep_params;

        /// <summary>
        /// 标识扫描循环已经启动 true启动 false停止
        /// </summary>
        private bool Sweeping = false;

        /// <summary>
        /// 标识校准是否成功 true成功 false失败
        /// </summary>
        bool bCalsuccess = true;

        /// <summary>
        /// 标识正在进行的扫描，已经完成的点数
        /// </summary>
        private int PointsDone;

        /// <summary>
        /// 当前欲操作的功放指示字
        /// </summary>
        private RFInvolved rf_involved = RFInvolved.Rf_1;

        /// <summary>
        /// 功放错误标识
        /// </summary>
        bool bErrorRf = false;

        /// <summary>
        /// 频谱错误标识
        /// </summary>
        bool bErrorSpec = false;

        #endregion


        #region 设备状态对象与扫描结构对象，由sweepobj.CloneReference赋值
        private PowerStatus ps1;
        private PowerStatus ps2;
        private SweepResult sr;
        private RFErrors rfr_errors1;
        private RFErrors rfr_errors2;
        #endregion


        #region 窗体的构造与加载
        internal IsoCALForm(Iso_Sweep SweepObj, Settings_Iso settings)
        {
            this.SweepObj = SweepObj;
            this.settings = settings;

            InitializeComponent();
        }
        #endregion


        #region 准备扫描参数和扫描序列
        private void Prepare_Freq_Sweep1(SweepParams p)
        {
            p.SweepType = SweepType.Freq_Sweep;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_1, 1);

            p.FrqParam = new FreqSweepParam();
            p.FrqParam.P1 = this.settings.Tx;

            Auto_CAL_Items Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_1);

            //根据隔离度校准表，生成功放1扫描序列
            FreqSweepItem[] items = new FreqSweepItem[Cal_Carrier.Length];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new FreqSweepItem();

                items[i].Tx1 = Cal_Carrier.GetItem(i).F0;

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    items[i].P1 = this.settings.Tx + Tx_Tables.iso_tx1.Offset(items[i].Tx1, this.settings.Tx, Tx_Tables.iso_offset1);
                }
                else
                    items[i].P1 = (float)App_Factors.iso_tx1.ValueWithOffset(items[i].Tx1, this.settings.Tx);

                items[i].Rx = Cal_Carrier.GetItem(i).F0;
            }

            //将生成的扫描序列赋值给扫描参数对象
            p.FrqParam.Items1 = items;
        }

        private void Prepare_Freq_Sweep2(SweepParams p)
        {
            p.SweepType = SweepType.Freq_Sweep;

            Init_Sweep_Params(p, this.Handle, RFInvolved.Rf_2, 1);

            p.FrqParam = new FreqSweepParam();
            p.FrqParam.P2 = this.settings.Tx;

            Auto_CAL_Items Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_2);

            //根据隔离度校准表，生成功放2扫描序列
            FreqSweepItem[] items = new FreqSweepItem[Cal_Carrier.Length];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new FreqSweepItem();

                items[i].Tx2 = Cal_Carrier.GetItem(i).F0;

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    items[i].P2 = this.settings.Tx + Tx_Tables.iso_tx2.Offset(items[i].Tx2, this.settings.Tx, Tx_Tables.iso_offset2);
                }
                else
                    items[i].P2 = (float)App_Factors.iso_tx2.ValueWithOffset(items[i].Tx2, this.settings.Tx);

                items[i].Rx = Cal_Carrier.GetItem(i).F0;
            }

            //将生成的扫描序列赋值给扫描参数对象
            p.FrqParam.Items2 = items;
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
            p.SpeParam.DeliSpe = SpectrumLib.Defines.CommonDef.SpectrumType.Deli_ISOLATION;
        }
        #endregion


        #region 隔离度自动校准的启动函数
        internal bool Do_CAL(RFInvolved rf_involved)
        {
            bool power_too_large = false;

            this.rf_involved = rf_involved;

            if ((SweepObj != null) || (settings != null))
            {
                if (!Sweeping)
                    power_too_large = StartSweep();
                else
                    SweepObj.StopSweep(1000);
            }

            return power_too_large;
        }

        /// <summary>
        /// 使用扫描参数对象，启动隔离度分析
        /// </summary>
        /// <param name="sweepParams"></param>
        private bool StartSweep()
        {
            bool power_too_large = false;

            if (!Sweeping)
            {
                Sweeping = true;

                PointsDone = 0;

                sweep_params = new SweepParams();

                //准备扫描参数，并启动扫描
                pbrCAL.Minimum = 0;

                if (rf_involved == RFInvolved.Rf_1)
                {
                    Prepare_Freq_Sweep1(sweep_params);

                    pbrCAL.Maximum = sweep_params.FrqParam.Items1.Length;

                    for (int i = 0; i < sweep_params.FrqParam.Items1.Length; i++)
                        if (sweep_params.FrqParam.Items1[i].P1 > App_Settings.sgn_1.Max_Power)
                        {
                            power_too_large = true;
                            break;
                        }

                }
                else
                {
                    Prepare_Freq_Sweep2(sweep_params);

                    pbrCAL.Maximum = sweep_params.FrqParam.Items2.Length;

                    for (int i = 0; i < sweep_params.FrqParam.Items2.Length; i++)
                        if (sweep_params.FrqParam.Items2[i].P2 > App_Settings.sgn_2.Max_Power)
                        {
                            power_too_large = true;
                            break;
                        }
                }

                if (power_too_large)
                {
                    Sweeping = false;

                    MessageBox.Show(this,"The carrier power setup is out of its range!");

                    this.Close();

                }
                else
                {
                    SweepObj.InitSweep();
                    SweepObj.Prepare(sweep_params);
                    SweepObj.StartSweep();
                }
            }

            return power_too_large;
        }

        /// <summary>
        /// 中断分析循环，发生在用户切换到其他功能模块
        /// 或用户强行停止分析
        /// </summary>
        public void BreakSweep(int timeOut)
        {
            SweepObj.StopSweep(timeOut);
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

                        //if (bErrorRf)
                        //{
                        //    SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);
                        //    bCalsuccess = false;
                        //    MessageBox.Show(rfr_errors1.ToString() + "\n\r" + rfr_errors2.ToString());
                        //}
                        //if (bErrorSpec)
                        //{
                        //    bCalsuccess = false;
                        //    MessageBox.Show("SPECTRUM_Error");
                        //}

                        if (bErrorRf == true || bErrorSpec == true)
                        {
                            bCalsuccess = false;
                            MessageBox.Show(this,"Automatic calibration failed, please check the external attenuator!");
                            this.DialogResult = DialogResult.Cancel;
                        }
                        else
                        {
                            this.DialogResult = DialogResult.OK;
                        }

                        break;
                    }

                //完成单点扫描
                case MessageID.ISO_SUCCED:
                    {
                        float tx_out, rx_value;
                        Auto_CAL_Items Cal_Carrier;

                        SweepObj.CloneReference(ref ps1, ref ps2, ref sr, ref rfr_errors1, ref rfr_errors2);

                        if (rf_involved == RFInvolved.Rf_1)
                        {
                            if (App_Configure.Cnfgs.Cal_Use_Table)
                            {
                                tx_out = ps1.Status2.OutP + Tx_Tables.iso_tx1_disp.Offset(ps1.Status2.Freq, ps1.Status2.OutP, Tx_Tables.iso_offset1_disp);
                                rx_value = sr.dBmValue + Rx_Tables.Offset(ps1.Status2.Freq, FuncModule.ISO);

                            }
                            else
                            {
                                tx_out = (float)App_Factors.iso_tx1_disp.ValueWithOffset(ps1.Status2.Freq, ps1.Status2.OutP);
                                rx_value = (float)App_Factors.iso_rx1.ValueWithOffset(ps1.Status2.Freq, sr.dBmValue);
                            }

                            Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_1);
                            Cal_Carrier.GetItem(PointsDone).Tx0 = tx_out;
                            Cal_Carrier.GetItem(PointsDone).Rx0 = rx_value;

                        }
                        else
                        {

                            if (App_Configure.Cnfgs.Cal_Use_Table)
                            {
                                tx_out = ps2.Status2.OutP + Tx_Tables.iso_tx2_disp.Offset(ps2.Status2.Freq, ps2.Status2.OutP, Tx_Tables.iso_offset2_disp);
                                rx_value = sr.dBmValue + Rx_Tables.Offset(ps2.Status2.Freq, FuncModule.ISO);

                            }
                            else
                            {
                                tx_out = (float)App_Factors.iso_tx2_disp.ValueWithOffset(ps2.Status2.Freq, ps2.Status2.OutP);
                                rx_value = (float)App_Factors.iso_rx2.ValueWithOffset(ps2.Status2.Freq, sr.dBmValue);
                            }

                            Cal_Carrier = RL0_Tables.Cal_Carrier(FuncModule.ISO, RFInvolved.Rf_2);
                            Cal_Carrier.GetItem(PointsDone).Tx0 = tx_out;
                            Cal_Carrier.GetItem(PointsDone).Rx0 = rx_value;
                        }

                        PointsDone++;

                        pbrCAL.Value = PointsDone;

                        break;
                    }

                //功放操作错误
                case MessageID.RF_ERROR:
                    {
                        bErrorRf = true;

                        break;
                    }

                //频谱分析错误
                case MessageID.SPECTRUM_ERROR:
                    {
                        bErrorSpec = true;

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