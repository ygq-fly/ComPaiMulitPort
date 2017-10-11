using System;
using System.Collections.Generic;
using System.Text;
using SpectrumLib;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Timers;
using System.Windows.Forms;

namespace jcPimSoftware
{
    class Pim_Sweep
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        IntPtr itp;
        public Pim_Sweep(IntPtr itp)//������˲���
        {
            RF_Type = App_Configure.Cnfgs.RFClass;
            Wait_time1 = App_Settings.sgn_1.Time_Vswr;
            Wait_time2 = App_Settings.sgn_2.Time_Vswr;
            //Pim_WaitTime = App_Configure.Cnfgs.Pim_wait;
            SPECTRUM_Type = App_Configure.Cnfgs.Spectrum;

            tTimeScan.Elapsed += new System.Timers.ElapsedEventHandler(tTimeScan_Elapsed);
            tTimeScan.Interval = 500;
            this.itp = itp;
        }

        #endregion

        #region ����ɨ��ѭ����ʵ������
        /// <summary>
        /// ɨ�����߳�
        /// </summary>
        Thread thrd;

        /// <summary>
        /// ɨ��ѭ���Ŀ��ƽṹ��
        /// ����ֹͣ��־λ���˳���־λ
        /// </summary>
        private SweepCtrl ctrl;

        /// <summary>
        /// ����1�ȴ����
        /// </summary>
        private ManualResetEvent power1_Handle;

        /// <summary>
        /// ����2�ȴ����
        /// </summary>
        private ManualResetEvent power2_Handle;

        /// <summary>
        /// Ƶ�׷����ȴ����
        /// </summary>
        private ManualResetEvent spectrum_Handle;

        /// <summary>
        /// �ȴ�ѭ�������˳��ľ��
        /// </summary>
        private ManualResetEvent thrd_Handle;

        /// <summary>
        /// Ƶ�׷����ӿ�
        /// </summary>
        public ISpectrum ISpectrumObj;

        #endregion

        #region ɨ�����ʵ������
        /// <summary>
        /// ִ��ɨ���������ɨ�躯���У�Ӧ��ʹ�ô˲���
        /// ����usr_sweeps���ƹ���
        /// </summary>
        private SweepParams exe_params;

        /// <summary>
        /// bakup1
        /// </summary>
        private TimeSweepParam __tsp = new TimeSweepParam();
        /// <summary>
        /// bakup2
        /// </summary>
        private FreqSweepItem __fsi = new FreqSweepItem();

        /// <summary>
        /// ���������趨
        /// </summary>
        private float band = 0.05f;
        public float Band
        {
            get { return band; }
            set { band = value; }
        }

        #endregion

        #region �豸�쳣��Ϣ

        /// <summary>
        /// Ƶ�������� 0NEC,1Bird
        /// </summary>
        private readonly int SPECTRUM_Type = 0;

        /// <summary>
        /// Ƶ���쳣��Ϣ����
        /// </summary>
        private SpectrumErrors speErrors;

        /// <summary>
        /// ����1�쳣��Ϣ����
        /// </summary>
        private RFErrors rfErrors_1;

        /// <summary>
        /// ����2�쳣��Ϣ����
        /// </summary>
        private RFErrors rfErrors_2;
        #endregion

        #region ����״̬��Ϣ

        /// <summary>
        /// ����Э������(0�Ϲ⹦�ţ�1��������)
        /// </summary>
        private readonly int RF_Type = 0;

        /// <summary>
        /// ��������1�ȴ��ȶ������ʱ��
        /// </summary>
        private int Wait_time1 = 1000;

        /// <summary>
        /// ��������2�ȴ��ȶ������ʱ��
        /// </summary>
        private int Wait_time2 = 1000;

        /// <summary>
        /// ���Ź�����30dBm������43dBm������ʱ
        /// </summary>
        private int Pim_WaitTime = 1000;

        /// <summary>
        /// ����1״̬��Ϣ����
        /// </summary>
        private PowerStatus rfStatus_1;

        /// <summary>
        /// ����2״̬��Ϣ����
        /// </summary>
        private PowerStatus rfStatus_2;
        #endregion

        #region ɨ��������ֵ����
        private SweepResult sweepValue;
        #endregion

        #region ɨ��ѭ�����ƺ���
        /// <summary>
        /// �����̣߳���ѭ����Ϊ������
        /// ���������߳�
        /// </summary>
        internal void StartSweep()
        {
            thrd = new Thread(Exectue);
            thrd.IsBackground = true;

            thrd.Start();
        }

        /// <summary>
        /// ��ǰ��ɨ����̽�����ֹͣɨ����̣�
        /// �ȴ���ֱ��ѭ���������������߳�ʱ
        /// </summary>
        internal void StopSweep(int timeOut)
        {
            if (ctrl != null)
            {
                Monitor.Enter(ctrl);
                ctrl.Quit = true;
                Monitor.Exit(ctrl);
            }

            //if (thrd_Handle != null)
            //{
            //    thrd_Handle.WaitOne(timeOut);
            //    thrd_Handle.Reset();
            //}
        }

        internal bool GetIsSweep()
        {
            bool res = false;
            if (ctrl != null)
            {
                Monitor.Enter(ctrl);
                res = ctrl.Quit;
                Monitor.Exit(ctrl);
            }
            return res;
          
        }

        /// <summary>
        /// �����յ�����1ִ�гɹ�����Ϣ�󣬵��øú����������֮ѭ��
        /// </summary>
        internal void Power1_Succed()
        {
            power1_Handle.Set();
        }

        /// <summary>
        /// �����յ�����2ִ�гɹ�����Ϣ�󣬵��øú����������֮ѭ��
        /// </summary>
        internal void Power2_Succed()
        {
            power2_Handle.Set();
        }

        /// <summary>
        /// �����յ�Ƶ�׷���ִ�гɹ�����Ϣ�󣬵��øú����������֮ѭ��
        /// </summary>
        internal void Spectrum_Succed()
        {
            int intmax = 0;
            PointF[] values;
            float dBmValue = float.MinValue;

            //��ȡƵ�׷�������
            values = (PointF[])ISpectrumObj.GetSpectrumData();

            //��ȡ�õ�Ƶ�׷��������У�����Yֵ���㣬����Yֵ��Ϊ����ֵ
            for (int J = 0; J < values.Length; J++)
            {
                if (values[J].Y > dBmValue)
                {
                    intmax = J;
                    dBmValue = values[J].Y;
                }

                //Log.WriteLog(values[J].ToString(), Log.EFunctionType.PIM);
            }          
            //����ɨ����ֵ����
            sweepValue.dBmValue = dBmValue;
            //Log.WriteLog("======================" + dBmValue.ToString(), Log.EFunctionType.PIM);
        }
        #endregion

        #region ɨ��ѭ���ĳ�ʼ������
        /// <summary>
        /// ����ʵ������
        /// </summary>
        public void InitSweep()
        {
            ctrl = new SweepCtrl();

            exe_params = new SweepParams();

            power1_Handle = new ManualResetEvent(false);

            power2_Handle = new ManualResetEvent(false);

            spectrum_Handle = new ManualResetEvent(false);

            thrd_Handle = new ManualResetEvent(false);

            sweepValue = new SweepResult();

            rfStatus_1 = new PowerStatus();

            rfStatus_2 = new PowerStatus();

            rfErrors_1 = new RFErrors();

            rfErrors_2 = new RFErrors();

            speErrors = new SpectrumErrors();
        }

        /// <summary>
        /// ׼��ɨ�����
        /// </summary>
        /// <param name="sweeps"></param>
        internal void Prepare(SweepParams usr_sweeps)
        {
            usr_sweeps.Clone(exe_params);

            //���ù�����Ϣ��Ŀ�괰����
            RFSignal.SetWndHandle(exe_params.WndHandle);

           // ����Ƶ�׷�������
            if (exe_params.DevInfo.Spectrum == SpectrumType.SPECAT2)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }

            else if (exe_params.DevInfo.Spectrum == SpectrumType.IRDSH)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }
            else if (exe_params.DevInfo.Spectrum == SpectrumType.Deli)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.Deli(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }
            else if (exe_params.DevInfo.Spectrum == SpectrumType.FanShuang)
            {
                ISpectrumObj = new SpectrumLib.Spectrums.FanShuang(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
            }                

        }
        #endregion
     
        #region  ɨ��ѭ����������
        // <summary>
        /// ����exe_sweeps�е�ɨ�������ִ��һϵ�ж���
        /// �����������š�Ƶ���ǡ���Ⲣ�����쳣��Ϣ������ɨ��������ȡ�豸״̬
        /// </summary>
        protected void Exectue()
        {
            Monitor.Enter(ctrl);
            ctrl.Quit = false;
            Monitor.Exit(ctrl);
            del_RF_DoFirst del_RF1_SetFirst = new del_RF_DoFirst(RF_SetFirst);
            del_RF_DoFirst del_RF2_SetFirst = new del_RF_DoFirst(RF_SetFirst);

            if (exe_params.SweepType == SweepType.Freq_Sweep)
            {
                try
                {
                    #region ���ű���
                    //���ù��ʣ��򿪹���
                    IAsyncResult Ir_RF1_SetFirst = del_RF1_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                      exe_params.RFPriority, exe_params.FrqParam.Items1[0].P1+App_Settings.spc.OutTxRef, true, null, null);
                    IAsyncResult Ir_RF2_SetFirst = del_RF2_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                     exe_params.RFPriority, exe_params.FrqParam.Items1[0].P2 + App_Settings.spc.OutTxRef, true, null, null);

                    del_RF1_SetFirst.EndInvoke(Ir_RF1_SetFirst);
                    del_RF2_SetFirst.EndInvoke(Ir_RF2_SetFirst);
                    #endregion

                    for (int i = 0; i < exe_params.C; i++)
                    { 
                        PimSweep();

                        //ָʾѭ���Ѿ���������
                        thrd_Handle.Set();
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.PIM_SWEEP_DONE, 0, 0);
                    }
                }
                catch {
                }
                finally
                {
                    //�رչ���         
                    IAsyncResult ir_RF1_SetLast = del_RF1_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr1, exe_params.RFPriority,
                                                                              30, false, null, null);
                    IAsyncResult ir_RF2_SetLast = del_RF2_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr2, exe_params.RFPriority,
                                                                            30, false, null, null);
                    del_RF1_SetFirst.EndInvoke(ir_RF1_SetLast);
                    del_RF2_SetFirst.EndInvoke(ir_RF2_SetLast);

                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.PIM_SWEEP_CLOSE, 0, 0);
                }

            }
            else
            {
                try
                {
                    time_over = false;

                    #region ���ű���
                    //���ù��ʣ��򿪹���
                    IAsyncResult Ir_RF1_SetFirst = del_RF1_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                    exe_params.RFPriority, exe_params.TmeParam.P1 + App_Settings.spc.OutTxRef, true,
                                                                    null, null);
                    IAsyncResult Ir_RF2_SetFirst = del_RF2_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                  exe_params.RFPriority, exe_params.TmeParam.P2 + App_Settings.spc.OutTxRef, true,
                                                                  null, null);

                    del_RF1_SetFirst.EndInvoke(Ir_RF1_SetFirst);
                    del_RF2_SetFirst.EndInvoke(Ir_RF2_SetFirst);
                    #endregion

                    //����Ƶ�׷����Ĳ�������
                    //RBW��ATT��Unit, Continued�����ⲿ����
                    exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx - band; //MHz
                    exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx + band; //MHz

                    for (int i = 0; i < exe_params.C; i++)
                    {
                        Time_Sweep();

                        //ָʾѭ���Ѿ���������
                        thrd_Handle.Set();
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.PIM_SWEEP_DONE, 0, 0);
                    }
                    
                }
                catch { }
                finally
                {
                    //���ù��ʣ��رչ���        
                    IAsyncResult ir_RF1_SetLast = del_RF1_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr1, exe_params.RFPriority,
                                                                               30, false, null, null);
                    IAsyncResult ir_RF2_SetLast = del_RF2_SetFirst.BeginInvoke(exe_params.DevInfo.RF_Addr2, exe_params.RFPriority,
                                                                            30, false, null, null);
                    del_RF1_SetFirst.EndInvoke(ir_RF1_SetLast);
                    del_RF2_SetFirst.EndInvoke(ir_RF2_SetLast);

                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.PIM_SWEEP_CLOSE, 0, 0);
                }
            }
        }

        private void SweepItems(FreqSweepItem[] item, int addr1, int addr2, int lvl, int n)
        {
            //if (n == 0)
            //    Log.WriteLog("enter 1", Log.EFunctionType.PIM);
            //else
            //    Log.WriteLog("enter 2", Log.EFunctionType.PIM);
            bool bQuit = false;
            bool bErrors1 = false;
            bool bErrors2 = false;

            del_RF_Do del_RF1_Set = new del_RF_Do(RF_Set);
            del_RF_Do del_RF2_Set = new del_RF_Do(RF_Set);
            del_RF_Do_Sample del_RF1_Set_Sample = new del_RF_Do_Sample(RF_Set_Sample);
            del_RF_Do_Sample del_RF2_Set_Sample = new del_RF_Do_Sample(RF_Set_Sample);
            del_RF_Sample del_RF1_Sampel = new del_RF_Sample(RF_Sample);
            del_RF_Sample del_RF2_Sampel = new del_RF_Sample(RF_Sample);
            del_SPECTRUM del_spe = new del_SPECTRUM(ISpectrumObj.StartAnalysis);
            bool isTimeout = IniFile.GetString("cnfgs", "enabletimeout", "1", Application.StartupPath + "\\Configures.ini") == "1" ? true : false;
            PowerStatus rf_status = new PowerStatus();

            RFSignal.RFStatus(exe_params.DevInfo.RF_Addr1, ref rf_status);

            for (int i = 0; i < item.Length; i++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;
                #region ====ע��====
                ////������ѯ����ȡ����1��ǰ״̬
                //if (n == 0)
                //{
                //    bErrors1 = RF_Do(addr1,
                //                   lvl,
                //                   item[i].P1, item[i].Tx1,
                //                   false, true, true, true, ref rfStatus_1);
                //}
                //else if (n == 1)
                //{
                //    bErrors1 = RF_Do(addr1,
                //                      lvl,
                //                      item[i].P1, item[i].Tx1,
                //                      false, true, false, false, ref rfStatus_1);
                //}

                ////��鹦���쳣���󣬰�������ͨ�ų�ʱ
                //bErrors1 = CheckRF_1(bErrors1);

                //if (bErrors1)
                //{
                //    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                //    break;
                //}
                ////������ѯ����ȡ����2��ǰ״̬
                //if (n == 0)
                //{
                //    bErrors2 = RF_Do(addr2,
                //                   lvl,
                //                   item[i].P2, item[i].Tx2,
                //                   false, true, false, true, ref rfStatus_2);
                //}
                //else if (n == 1)
                //{
                //    bErrors2 = RF_Do(addr2,
                //                     lvl,
                //                     item[i].P2, item[i].Tx2,
                //                     false, true, true, true, ref rfStatus_2);
                //}

                ////��鹦���쳣���󣬰�������ͨ�ų�ʱ
                //bErrors2 = CheckRF_2(bErrors2);

                //if (bErrors2)
                //{
                //    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                //    break;
                //}
                #endregion

                FanShuangAddProcess(ref item[i], n);

                //����Ƶ�׷����Ĳ�������
                //RBW��ATT��Unit, Continued�����ⲿ����
                exe_params.SpeParam.StartFreq = item[i].Rx - band; //MHz
                exe_params.SpeParam.EndFreq = item[i].Rx + band; //MHz

                //Log.WriteLog("ѭ��:" + i.ToString() + " start:" + exe_params.SpeParam.StartFreq.ToString("0.00") + "  end:" + exe_params.SpeParam.EndFreq.ToString("0.00"), Log.EFunctionType.PIM);
                //Log.WriteLog("ѭ��:" + i.ToString(), Log.EFunctionType.PIM);

                IAsyncResult Ir_Spec,
                             Ir_RF1_Set, Ir_RF2_Set,
                             Ir_RF1_Sampel,Ir_RF2_Sampel = null;

                if (n == 0)//ɨ������
                {
                    if (i == 0)
                    {

                         RFSignal.RFStatus(exe_params.DevInfo.RF_Addr1,ref rf_status);

                         if (rf_status.Status1.Ver[1] >= 6)
                         {//��Ҫ�̼�֧��
                             Ir_RF1_Sampel = del_RF1_Set_Sample.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                                     exe_params.RFPriority,
                                                                                     item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1,
                                                                                     ref rfStatus_1,
                                                                                     null, null);

                             Ir_RF2_Sampel = del_RF2_Set_Sample.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                                             exe_params.RFPriority,
                                                                                              item[i].P2 + App_Settings.spc.OutTxRef, item[i].Tx2,
                                                                                             ref rfStatus_2,
                                                                                             null, null);

                             /* use Thread.Sleep(50); instead of exe_params.SpeParam.TimeDelay += 50;
                                because the del_spe gerentor other thread to block,need a strong power CPU.                             
                              */
                             Thread.Sleep(App_Settings.pim.FsDelay);//���Ż��ٶ��ٶ�ƥ�����
                             Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);

                             if (App_Configure.Cnfgs.Spectrum == 3)
                             {
                                 Thread.Sleep(50);
                                 del_spe.EndInvoke(Ir_Spec);
                                 Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                             }

                             bErrors1 |= del_RF1_Set_Sample.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                             bErrors2 |= del_RF2_Set_Sample.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);
                         }
                         else
                         {
                             Ir_RF1_Set = del_RF1_Set.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                              exe_params.RFPriority,
                                                                              item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1,
                                                                              null, null);
                             Ir_RF2_Set = del_RF2_Set.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                             exe_params.RFPriority,
                                                                             item[i].P2 + App_Settings.spc.OutTxRef, item[i].Tx2,
                                                                             null, null);
                             bErrors1 = del_RF1_Set.EndInvoke(Ir_RF1_Set);
                             bErrors2 = del_RF2_Set.EndInvoke(Ir_RF2_Set);

                             Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                         }

                    }
                    else
                    {
                        RFSignal.RFStatus(exe_params.DevInfo.RF_Addr1,ref rf_status);

                        if (rf_status.Status1.Ver[1] >= 6)
                        {//��Ҫ�̼�֧��
                            Ir_RF1_Sampel = del_RF1_Set_Sample.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                                    exe_params.RFPriority,
                                                                                    item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1,
                                                                                    ref rfStatus_1,
                                                                                    null, null);

                            Ir_RF2_Sampel = del_RF2_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                                            exe_params.RFPriority,
                                                                                            ref rfStatus_2,
                                                                                            null, null);

                            /* use Thread.Sleep(50); instead of exe_params.SpeParam.TimeDelay += 50;
                               because the del_spe gerentor other thread to block,need a strong power CPU.                             
                             */
                            Thread.Sleep(App_Settings.pim.FsDelay);//���Ż��ٶ��ٶ�ƥ�����
                            Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);

                            bErrors1 |= del_RF1_Set_Sample.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                            bErrors2 |= del_RF2_Sampel.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);
                        }
                        else
                        {

                            //Ir_RF1_Set = del_RF1_Set.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                            //                                                  exe_params.RFPriority,
                            //                                                  item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1,
                            //                                                  null, null);
                         
                            //bErrors1 = del_RF1_Set.EndInvoke(Ir_RF1_Set);
                            bErrors1 = RF_Set(exe_params.DevInfo.RF_Addr1,
                                            exe_params.RFPriority,
                                            item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1);
                            if (App_Configure.Cnfgs.Spectrum != SpectrumType.FanShuang)           
                            Thread.Sleep(200);
                            Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                        }    

                    }
                    //Log.WriteLog("����" + (i + 1).ToString(), Log.EFunctionType.PIM);
                    //Log.WriteLog("tx1=" + item[i].Tx1.ToString() + " tx2=" + item[i].Tx2.ToString() + "  rx=" + exe_params.SpeParam.StartFreq.ToString() + " rxe=" + exe_params.SpeParam.EndFreq.ToString() + "\r\n", Log.EFunctionType.PIM);
                    //MessageBox.Show("tx1=" + item[i].Tx1.ToString() + " tx2=" + item[i].Tx2.ToString() + "  rx=" + exe_params.SpeParam.StartFreq.ToString() + " rxe=" + exe_params.SpeParam.EndFreq.ToString());
                }
                else //ɨ������
                {
                    if (i == 0 )
                    {
                         RFSignal.RFStatus(exe_params.DevInfo.RF_Addr2, ref rf_status);

                         if (rf_status.Status1.Ver[1] >= 6)
                         {//��Ҫ�̼�֧��
                             Ir_RF1_Sampel = del_RF1_Set_Sample.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                                             exe_params.RFPriority,
                                                                                             item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1,
                                                                                             ref rfStatus_1,
                                                                                             null, null);

                             Ir_RF2_Sampel = del_RF2_Set_Sample.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                                     exe_params.RFPriority,
                                                                                     item[i].P2 + App_Settings.spc.OutTxRef, item[i].Tx2,
                                                                                     ref rfStatus_2,
                                                                                     null, null);

                             /* use Thread.Sleep(50); instead of exe_params.SpeParam.TimeDelay += 50;
                                because the del_spe gerentor other thread to block,need a strong power CPU.                             
                              */
                             Thread.Sleep(App_Settings.pim.FsDelay);//���Ż��ٶ��ٶ�ƥ�����
                             Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);

                             bErrors1 |= del_RF1_Set_Sample.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                             bErrors2 |= del_RF2_Set_Sample.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);
                         }
                         else
                         {
                             //exe_params.SpeParam.Startspe = 1;
                             Ir_RF1_Set = del_RF1_Set.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                             exe_params.RFPriority,
                                                                             item[i].P1 + App_Settings.spc.OutTxRef, item[i].Tx1,
                                                                             null, null);
                             Ir_RF2_Set = del_RF2_Set.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                             exe_params.RFPriority,
                                                                             item[i].P2 + App_Settings.spc.OutTxRef, item[i].Tx2,
                                                                             null, null);
                             bErrors1 = del_RF1_Set.EndInvoke(Ir_RF1_Set);
                             bErrors2 = del_RF2_Set.EndInvoke(Ir_RF2_Set);

                             Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                         }
                    }
                    else
                    {
                        RFSignal.RFStatus(exe_params.DevInfo.RF_Addr2, ref rf_status);

                        if (rf_status.Status1.Ver[1] >= 6)
                        {//��Ҫ�̼�֧��
                            Ir_RF1_Sampel = del_RF1_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                                            exe_params.RFPriority,
                                                                                            ref rfStatus_1,
                                                                                            null, null);

                            Ir_RF2_Sampel = del_RF2_Set_Sample.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                                    exe_params.RFPriority,
                                                                                    item[i].P2 + App_Settings.spc.OutTxRef, item[i].Tx2,
                                                                                    ref rfStatus_2,
                                                                                    null, null);

                            /* use Thread.Sleep(50); instead of exe_params.SpeParam.TimeDelay += 50;
                               because the del_spe gerentor other thread to block,need a strong power CPU.                             
                             */
                            Thread.Sleep(App_Settings.pim.FsDelay);//���Ż��ٶ��ٶ�ƥ�����
                            Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);

                            bErrors1 |= del_RF1_Sampel.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                            bErrors2 |= del_RF2_Set_Sample.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);
                        }
                        else
                        {
                            bErrors2 = RF_Set(exe_params.DevInfo.RF_Addr2,
                                            exe_params.RFPriority,
                                            item[i].P2 + App_Settings.spc.OutTxRef, item[i].Tx2);
                            if (App_Configure.Cnfgs.Spectrum != SpectrumType.FanShuang)
                                Thread.Sleep(50);
                            Ir_Spec = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                        }                        
                    }

                    //Log.WriteLog("����" + (i + 1).ToString(), Log.EFunctionType.PIM);
                }

                if (rf_status.Status1.Ver[1] < 6)
                {//��Ҫ�̼�֧��
                    Ir_RF1_Sampel = del_RF1_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                                    exe_params.RFPriority,
                                                                                    ref rfStatus_1,
                                                                                    null, null);

                    Ir_RF2_Sampel = del_RF2_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                            exe_params.RFPriority,
                                                                            ref rfStatus_2,
                                                                            null, null);

                    bErrors1 |= del_RF1_Sampel.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                    bErrors2 |= del_RF2_Sampel.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);
                }                

                del_spe.EndInvoke(Ir_Spec);

                bErrors1 = CheckRF_1(bErrors1);
                bErrors2 = CheckRF_2(bErrors2);

                FanShuangPAStatus(item[i], ref rfStatus_1, ref rfStatus_2);

                //if (bErrors1 || bErrors2)
                //    Log.WriteLog((i + 1).ToString() + " error", Log.EFunctionType.PIM);
                //enabletimeout=0�ر�timeout��ʾ =1����             
                if (bErrors1)
                {
                    if (rfErrors_1.RF_TimeOut && isTimeout == false)
                    {
                        //Log.WriteLog((i + 1).ToString() + " _error1", Log.EFunctionType.PIM);
                        continue;
                    }
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    //Log.WriteLog((i + 1).ToString() + " error1", Log.EFunctionType.PIM);
                    return;
                }
                if (bErrors2)
                {
                    if (rfErrors_2.RF_TimeOut && isTimeout == false)
                    {
                        //Log.WriteLog((i + 1).ToString() + " _error2", Log.EFunctionType.PIM);
                        continue;
                    }
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    //Log.WriteLog((i + 1).ToString() + " error2", Log.EFunctionType.PIM);
                    return;
                }
                //��WndHandle������Ϣ����֪���һ����ķ���
                if (!bQuit)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.PIM_SUCCED, (uint)n, i);
                }
            }
        }

        private void PimSweep()
        {
            //bool bErrors1 = false;
            //bool bErrors2 = false;

            //ִ��UPɨ��
            SweepItems(exe_params.FrqParam.Items1,
                      exe_params.DevInfo.RF_Addr1,
                      exe_params.DevInfo.RF_Addr2,
                           exe_params.RFPriority,
                           0);
            #region ====ע��====
            ////��д����1
            //bErrors1 = RF_Do(exe_params.DevInfo.RF_Addr1,
            //                exe_params.RFPriority,
            //                exe_params.FrqParam.Items2[0].P1, exe_params.FrqParam.Items2[0].Tx1,
            //                false, true, true, true, ref rfStatus_1);

            ////��鹦���쳣���󣬰�������ͨ�ų�ʱ
            //bErrors1 = CheckRF_1(bErrors1);

            //bErrors1 = RF_Set(exe_params.DevInfo.RF_Addr1,
            //                exe_params.RFPriority,
            //                exe_params.FrqParam.Items2[0].P1, exe_params.FrqParam.Items2[0].Tx1,
            //                false, true, true, true);

            //if (bErrors1)
            //{
            //    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
            //}
            //else
            //{
            //    //ִ��DOWNɨ��
            //    SweepItems(exe_params.FrqParam.Items2,
            //              exe_params.DevInfo.RF_Addr1,
            //              exe_params.DevInfo.RF_Addr2,
            //                   exe_params.RFPriority,
            //                   1);
            //}
            #endregion
            if (!ctrl.Quit)
            {
                //ִ��DOWNɨ��
                SweepItems(exe_params.FrqParam.Items2,
                          exe_params.DevInfo.RF_Addr1,
                          exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority, 1);
            }

            if (!ctrl.Quit)
                Thread.Sleep(800);
        }
    
        private void Time_Sweep()
        {    
            bool bQuit = false;
            bool bErrors1 = false;
            bool bErrors2 = false;
            del_SPECTRUM del_spe = new del_SPECTRUM(ISpectrumObj.StartAnalysis);
            del_RF_Do del_RF1_Set = new del_RF_Do(RF_Set);
            del_RF_Sample del_RF1_Sampel = new del_RF_Sample(RF_Sample);
            del_RF_Sample del_RF2_Sampel = new del_RF_Sample(RF_Sample);

            TimeSweepParam tsp;
            tsp = exe_params.TmeParam;
            FanShuangAddProcess(ref tsp);
            exe_params.TmeParam = tsp;

            //��������ɨʱ����
            //for (int I = 0; I < exe_params.TmeParam.N; I++)
            int a = 0;
            //Log.WriteLog("count_start= "+count.ToString(), Log.EFunctionType.PIM);
            tTimeScan.Start();
            for (int I = 0; I < 1200; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                //���Ż��ٶ��ٶ�ƥ�����
                if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
                    Thread.Sleep(App_Settings.pim.TsDelay);

                if (bQuit)
                {
                    time_over = true;
                    count = 0;
                    tTimeScan.Stop();
                    break;
                }
                a++;
                #region ===ע��===
                ////������ѯ����ȡ����1��ǰ״̬
                //bErrors1 = RF_Do(exe_params.DevInfo.RF_Addr1,
                //               exe_params.RFPriority,
                //               exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                //               false, true, false, false, ref rfStatus_1);

                ////��鹦���쳣���󣬰�������ͨ�ų�ʱ
                //bErrors1 = CheckRF_1(bErrors1);

                //if (bErrors1)
                //{
                //    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                //    break;
                //}

                ////������ѯ����ȡ����2��ǰ״̬
                //bErrors2 = RF_Do(exe_params.DevInfo.RF_Addr2,
                //               exe_params.RFPriority,
                //               exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                //               false, true, false, false, ref rfStatus_2);

                ////��鹦���쳣���󣬰�������ͨ�ų�ʱ
                //bErrors2 = CheckRF_2(bErrors2);
                //if (bErrors2)
                //{
                //    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                //    break;
                //}
                #endregion

                if (I != 0)
                {
                    IAsyncResult Ir_spe = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                    IAsyncResult Ir_RF1_Sampel = del_RF1_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                            exe_params.RFPriority,
                                                                            ref rfStatus_1,
                                                                            null, null);
                    IAsyncResult Ir_RF2_Sampel = del_RF2_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                            exe_params.RFPriority,
                                                                            ref rfStatus_2,
                                                                            null, null);
                    bErrors1 |= del_RF1_Sampel.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                    bErrors2 |= del_RF2_Sampel.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);

                    del_spe.EndInvoke(Ir_spe);

                    bErrors1 = CheckRF_1(bErrors1);
                    bErrors2 = CheckRF_2(bErrors2);

                    FanShuangPAStatus(tsp, ref rfStatus_1, ref rfStatus_2);     

                    if (bErrors1 & bErrors2 == false)
                    {
                        time_over = true;
                        tTimeScan.Stop();
                    }
                    if (bErrors1)
                    {
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                        return;
                    }
                    if (bErrors2)
                    {
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                        return;
                    }
                }
                else
                {
                    IAsyncResult Ir_RF1_Set = del_RF1_Set.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                   exe_params.RFPriority,
                                                                   exe_params.TmeParam.P1 + App_Settings.spc.OutTxRef, exe_params.TmeParam.F1,
                                                                   null, null);

                    bErrors2 = RF_Set(exe_params.DevInfo.RF_Addr2,
                                    exe_params.RFPriority,
                                    exe_params.TmeParam.P2 + App_Settings.spc.OutTxRef, exe_params.TmeParam.F2);
                    bErrors1 = del_RF1_Set.EndInvoke(Ir_RF1_Set);

                    IAsyncResult Ir_spe=null;
                    if (App_Configure.Cnfgs.Mode == 0 || App_Configure.Cnfgs.Mode == 1)//
                     Ir_spe = del_spe.BeginInvoke(exe_params.SpeParam, null, null);

                    IAsyncResult Ir_RF1_Sampel = del_RF1_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr1,
                                                                            exe_params.RFPriority,
                                                                            ref rfStatus_1,
                                                                            null, null);
                    IAsyncResult Ir_RF2_Sampel = del_RF2_Sampel.BeginInvoke(exe_params.DevInfo.RF_Addr2,
                                                                            exe_params.RFPriority,
                                                                            ref rfStatus_2,
                                                                            null, null);

                    bErrors1 |= del_RF1_Sampel.EndInvoke(ref rfStatus_1, Ir_RF1_Sampel);
                    bErrors2 |= del_RF2_Sampel.EndInvoke(ref rfStatus_2, Ir_RF2_Sampel);

                    if (App_Configure.Cnfgs.Mode == 0 || App_Configure.Cnfgs.Mode == 1)
                    {
                        Ir_spe = del_spe.BeginInvoke(exe_params.SpeParam, null, null);

                        Thread.Sleep(50);
                        del_spe.EndInvoke(Ir_spe);
                        Ir_spe = del_spe.BeginInvoke(exe_params.SpeParam, null, null);
                        
                    }

                    del_spe.EndInvoke(Ir_spe);

                    bErrors1 = CheckRF_1(bErrors1);
                    bErrors2 = CheckRF_2(bErrors2);

                    FanShuangPAStatus(tsp, ref rfStatus_1, ref rfStatus_2);

                    if (bErrors1 & bErrors2 == false)
                    {
                        time_over = true;
                        tTimeScan.Stop();
                        
                    }
                    if (bErrors1)
                    {
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                        return;
                    }
                    if (bErrors2)
                    {
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                        NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                        return;
                    }

                }

                if (time_over)
                {
                    time_over = false;
                    //Log.WriteLog("count_end= " + count.ToString(), Log.EFunctionType.PIM);
                    break;
                }
                //��WndHandle������Ϣ����֪���һ����ķ���
                if (!bQuit)
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.PIM_SUCCED, 0, I);
            }

            //Log.WriteLog(a.ToString() + "\r\n", Log.EFunctionType.PIM);

            if (!ctrl.Quit)
                Thread.Sleep(800);
        }

        private bool RF_Do(int Addr,
                           int Lvl,
                           float P,
                           float F,
                           bool RFon,
                           bool ignoreRFon,
                           bool useP,
                           bool useF,
                           ref PowerStatus status)
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


            //�ȴ�����
            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(5000, true);
                power1_Handle.Reset();

            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(5000, true);
                power2_Handle.Reset();
            }

            //û�з�������ͨ�ų�ʱ�����ȡ����״̬��Ϣ
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);
            //����ͨ�ų�ʱ�����
            return (!RF_Succ);

        }


       public  List<string> errmessage = new List<string>();
        /// <summary>
        /// ����ȫ�־�̬�Ĺ����豸�������������쳣���
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_1(bool timeOut)
        {
            bool errors = false;

            rfErrors_1.RF_TimeOut = timeOut;

            if (rfStatus_1.Status2.RftErr == 1)
            {
                errors = true;
                rfErrors_1.RF_RftErr = true;
                rfErrors_1.RF_RfValue = rfStatus_1.Status2.RftErr;
            }
            else
            {
                if ((rfStatus_1.Status2.OutP - rfStatus_1.Status2.RftP < 6) && (rfStatus_1.Status2.OutP > 29))
                {
                    errors = true;
                    rfErrors_1.RF_RftErr = true;
                    rfErrors_1.RF_RfValue = rfStatus_1.Status2.RftErr;
                }
            }

            if ((rfStatus_1.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr))
            {
                errors = true;
                rfErrors_1.RF_VswrError = true;
                rfErrors_1.RF_VswrValue = rfStatus_1.Status2.Vswr;
            }
          
            if ((rfStatus_1.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                (rfStatus_1.Status2.Current > App_Configure.Cnfgs.Max_Curr))
            {
                errors = true;
                rfErrors_1.RF_CurrError = true;
                rfErrors_1.RF_CurrValue = rfStatus_1.Status2.Current;
            }

            if ((rfStatus_1.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                (rfStatus_1.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
            {
                errors = true;
                rfErrors_1.RF_TempError = true;
                rfErrors_1.RF_TempValue = rfStatus_1.Status2.Temp;
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
            if (rfStatus_2.Status2.RftErr == 1)
            {
                errors = true;
                rfErrors_2.RF_RftErr = true;
                rfErrors_2.RF_RfValue = rfStatus_2.Status2.RftErr;
             
                //errmessage.Add("FR2 Reflected power abnormal");
            }
            else
            {
                if ((rfStatus_2.Status2.OutP - rfStatus_2.Status2.RftP < 6) && (rfStatus_2.Status2.OutP > 29))
                {
                    errors = true;
                    rfErrors_2.RF_RftErr = true;
                    rfErrors_2.RF_RfValue = rfStatus_2.Status2.RftErr;
                }
            }

            if ((rfStatus_2.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                (rfStatus_2.Status2.Current > App_Configure.Cnfgs.Max_Curr))
            {
                errors = true;
                rfErrors_2.RF_CurrError = true;
                rfErrors_2.RF_CurrValue = rfStatus_2.Status2.Current;
            }

            if ((rfStatus_2.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                (rfStatus_2.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
            {
                errors = true;
                rfErrors_2.RF_TempError = true;
                rfErrors_2.RF_TempValue = rfStatus_2.Status2.Temp;
            }

            if (rfStatus_2.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr)
            {
                errors = true;
                rfErrors_2.RF_VswrError = true;
                rfErrors_2.RF_VswrValue = rfStatus_2.Status2.Vswr;
            }

            return (errors || timeOut);
        }

        /// <summary>
        /// ���Ƶ���쳣������ʲô���󣬶�����ͨ���쳣
        /// </summary>
        /// <param name="timeOut"></param>
        private void CheckSpectrum(bool timeOut)
        {
            speErrors.Spectrum_TimeOut = timeOut;
        }
        #endregion

        #region FanShuangProcess
        /// <summary>
        /// ��˫���Ż���Ҫ���⴦��Time
        /// </summary>
        /// <param name="tsp"></param>
        private void FanShuangAddProcess(ref TimeSweepParam tsp)
        {
            /*****************************************************************************
             * ��˫���Ż���ƵΪ70MHz,�������˲����������ޣ���������Ƶ2��Ƶ������һ��г��������
             * �������Ƶƫ�ķ�ʽ����ܿ���
             * nF1 - mF2 -> F2 -= 0.1             
             * */
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                __tsp.F1 = tsp.F1;
                __tsp.F2 = tsp.F2;

                if (tsp != null)
                {
                    float freqMin, freqMax;

                    freqMax = Math.Max(tsp.F1, tsp.F2);
                    freqMin = Math.Min(tsp.F1, tsp.F2);
                   
                    if (freqMax - 140 == tsp.Rx || freqMin - 140 == tsp.Rx)
                    {
                        if (tsp.F1 > tsp.F2)
                        {
                            tsp.F1 -= 0.1f;
                        }
                        else
                        {
                            tsp.F2 -= 0.1f;
                        }

                        int imo = PimForm.pimOrder;

                        freqMax -= 0.1f;

                        if (App_Configure.Cnfgs.Mode >= 2)
                        {
                            if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                            {
                                tsp.Rx = ((imo - 1) / 2 + 1) * freqMax - ((imo - 1) / 2 * freqMin);
                            }
                            else
                            {
                                tsp.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                            }
                        }
                        else
                        {
                            if (App_Configure.Cnfgs.Mode == 1)
                            {
                                tsp.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                            }
                            else if (App_Configure.Cnfgs.Mode == 0)
                            {
                                tsp.Rx = ((imo - 1) / 2 + 1) * freqMax - ((imo - 1) / 2 * freqMin);

                            }
                            else
                                tsp.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                        }

                        //MessageBox.Show("order: "+imo.ToString()+" f1 "+freqMin.ToString()+" f2 "+freqMax.ToString()+ " rx:" + tsp.Rx.ToString());
                        //tsp.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                  
                        exe_params.SpeParam.StartFreq = tsp.Rx - band;
                        exe_params.SpeParam.EndFreq = tsp.Rx + band;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tsp"></param>
        /// <param name="status"></param>
        private void FanShuangPAStatus(TimeSweepParam tsp, ref PowerStatus status1, ref PowerStatus status2)
        {
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                status1.Status2.Freq = __tsp.F1;
                status2.Status2.Freq = __tsp.F2;
            }
        }

        /// <summary>
        /// ��˫���Ż���Ҫ���⴦��Freq
        /// </summary>
        /// <param name="fsi"></param>
        private void FanShuangAddProcess(ref FreqSweepItem fsi, int n)
        {
           
            /*****************************************************************************
             * ��˫���Ż���ƵΪ70MHz,�������˲����������ޣ���������Ƶ2��Ƶ������һ��г��������
             * �������Ƶƫ�ķ�ʽ����ܿ���
             * nF1 - mF2 -> F2 -= 0.1             
             * */
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                __fsi.Tx1 = fsi.Tx1;
                __fsi.Tx2 = fsi.Tx2;

                if (fsi != null)
                {
                    float freqMin, freqMax;

                    freqMax = Math.Max(fsi.Tx1, fsi.Tx2);
                    freqMin = Math.Min(fsi.Tx1, fsi.Tx2);

                    if (freqMax - 140 == fsi.Rx || freqMin - 140 == fsi.Rx)
                    {
                        //if (n == 0)
                        //    fsi.Tx1 += (fsi.Tx1 > fsi.Tx2) ? -0.1f : 0.1f;
                        //else
                        //    fsi.Tx2 += (fsi.Tx2 > fsi.Tx1) ? -0.1f : 0.1f;

                        //int imo = (int)exe_params.PimOrder;

                        //if (fsi.Tx1 > fsi.Tx2)
                        //    fsi.Rx = ((imo - 1) / 2 + 1) * fsi.Tx2 - ((imo - 1) / 2 * fsi.Tx1);  
                        //else
                        //    fsi.Rx = ((imo - 1) / 2 + 1) * fsi.Tx1 - ((imo - 1) / 2 * fsi.Tx2);  
                        if (fsi.Tx1 > fsi.Tx2)
                        {
                            if (n == 0)//
                            {
                                freqMax -= 0.1f;
                                fsi.Tx1 -= 0.1f;
                            }
                            else//��ɨƵ
                            {
                                freqMin += 0.1f;
                                fsi.Tx2 += 0.1f;
                            }
                        }
                        else
                        {
                            if (n == 0)//
                            {
                                freqMin += 0.1f;
                                fsi.Tx1 += 0.1f;
                            }
                            else//
                            {
                                freqMax -= 0.1f;
                                fsi.Tx2 -= 0.1f;
                            }
                        
                        }
                       

                        int imo = PimForm.pimOrder;

                        
                        if (App_Configure.Cnfgs.Mode >= 2)
                        {
                            if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                            {
                                fsi.Rx = ((imo - 1) / 2 + 1) * freqMax - ((imo - 1) / 2 * freqMin);
                            }
                            else
                            {
                                fsi.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                            }
                        }
                        else
                        {
                            if (App_Configure.Cnfgs.Mode == 1)
                            {
                                fsi.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                            }
                            else if (App_Configure.Cnfgs.Mode == 0)
                            {
                                fsi.Rx = ((imo - 1) / 2 + 1) * freqMax - ((imo - 1) / 2 * freqMin);

                            }
                            else
                                fsi.Rx = ((imo - 1) / 2 + 1) * freqMin - ((imo - 1) / 2 * freqMax);
                        }

                        //MessageBox.Show("rx=" + fsi.Rx.ToString() + "  tx1=" + fsi.Tx1.ToString() + " tx2=" + fsi.Tx2.ToString());

                    }
                    else
                    {
                        
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fsi"></param>
        /// <param name="status"></param>
        private void FanShuangPAStatus(FreqSweepItem fsi, ref PowerStatus status1, ref PowerStatus status2)
        {
           
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                status1.Status2.Freq = __fsi.Tx1;
                status2.Status2.Freq = __fsi.Tx2;
            }
        }

        #endregion

        #region Clone
        public void Clone(ref PowerStatus ps1, 
                          ref PowerStatus ps2, 
                          ref SweepResult sr,
                          ref RFErrors rfErrors1,
                          ref RFErrors rfErrors2)
        {
            ps1 = rfStatus_1;
            ps2 = rfStatus_2;
            sr = sweepValue;
            rfErrors1 = rfErrors_1;
            rfErrors2 = rfErrors_2;
        }
        #endregion

        #region ================================= Add by NQ =================================

        /// <summary>
        /// ����ɨʱ��ʱ���־ true��ʱ���� falseδ��ʱ
        /// </summary>
        private bool time_over = false;

        /// <summary>
        /// ��������
        /// </summary>
        private int count = 0;

        /// <summary>
        /// ����ɨʱʱ��Ķ�ʱ��
        /// </summary>
        private System.Timers.Timer tTimeScan = new System.Timers.Timer();

        void tTimeScan_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            count++;
            if (count * (tTimeScan.Interval / 1000) >= exe_params.TmeParam.N)
            {
                Log.WriteLog(count.ToString() + "  " + exe_params.TmeParam.N.ToString(), Log.EFunctionType.PIM);
                time_over = true;
                count = 0;
                tTimeScan.Stop();            
            }
        }


        delegate bool del_RF_Do(int Addr,
                              int Lvl,
                              float P,
                              float F);

        delegate bool del_RF_Do_Sample(int Addr,
                              int Lvl,
                              float P,
                              float F,
                              ref PowerStatus status);

        delegate bool del_RF_Sample(int Addr,
                                    int Lvl,
                                    ref PowerStatus status);

        delegate bool del_RF_DoFirst(int Addr,
                                    int Lvl, float P, bool RFon);

        delegate void del_SPECTRUM(SpectrumLib.Models.ScanModel sp);


        #region RF_Set ����Ƶ�ʣ�����
        /// <summary>����Ƶ�ʣ�����
        /// 
        /// </summary>
        /// <param name="Addr">���ŵ�ַ</param>
        /// <param name="Lvl">�����</param>
        /// <param name="P">����</param>
        /// <param name="F">Ƶ��</param>
        /// <returns></returns>
        private bool RF_Set(int Addr,
                           int Lvl,
                           float P, float F)
        {
           // return false;
            bool RF_Succ = true;

            PowerStatus status = new PowerStatus();
            RFSignal.RFStatus(Addr, ref status);

            RFSignal.RFClear(Addr, Lvl);

            //�Ϲ⹦�Ÿı�Ƶ�ʻ�Ӱ�칦�ʣ���������Ƶ�ʣ��������Ÿı书�ʻ�Ӱ��Ƶ�ʣ��������ù���
            if (status.Status1.Ver[1] >= 6)
            {
                RFSignal.RFPowerFreq(Addr, Lvl, P, F);
            }
            else if (RF_Type == 0)
            {
                if (App_Configure.Cnfgs.RFFormula == 0)//����LOG
                {
                    RFSignal.RFPowerFreq(Addr, Lvl, P, F);
                }
                else //����Linear
                {
                    RFSignal.RFFreq(Addr, Lvl, F);
                    RFSignal.RFPower(Addr, Lvl, P);
                }
            }
            else
            {
                RFSignal.RFPowerFreq(Addr, Lvl, P, F);
            }

            RFSignal.RFStart(Addr);

            //�ȴ�����
            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(5000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(5000, true);
                power2_Handle.Reset();
            }

            if (status.Status1.Ver[1] >= 6)
            {
                //nozuo
            }
            else if (RF_Type == 0)
                Thread.Sleep(50);
            else
                Thread.Sleep(150);

            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
        }


        #endregion

        #region RF_Set_Sample ����Ƶ�ʣ����� , ��ִ��Sample
        /// <summary>����Ƶ�ʣ�����
        /// 
        /// </summary>
        /// <param name="Addr">���ŵ�ַ</param>
        /// <param name="Lvl">�����</param>
        /// <param name="P">����</param>
        /// <param name="F">Ƶ��</param>
        /// <returns></returns>
        private bool RF_Set_Sample(int Addr,
                           int Lvl,
                           float P, float F, ref PowerStatus status)
        {
            // return false;
            bool RF_Succ = true;
            RFSignal.RFClear(Addr, Lvl);

            //�Ϲ⹦�Ÿı�Ƶ�ʻ�Ӱ�칦�ʣ���������Ƶ�ʣ��������Ÿı书�ʻ�Ӱ��Ƶ�ʣ��������ù���
            if (RF_Type == 0)
            {
                //if (App_Configure.Cnfgs.RFFormula == 0)//����LOG
                //{
                //    RFSignal.RFPowerFreq(Addr, Lvl, P, F);
                //}
                //else //����Linear
                //{
                //    RFSignal.RFFreq(Addr, Lvl, F);
                //    RFSignal.RFPower(Addr, Lvl, P);
                //}
                RFSignal.RFPowerFreqSample(Addr, Lvl, P, F);
            }
            else
            {
                RFSignal.RFPowerFreqSample(Addr, Lvl, P, F);
            }

            RFSignal.RFStart(Addr);

            //�ȴ�����
            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(5000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(5000, true);
                power2_Handle.Reset();
            }

            //û�з�������ͨ�ų�ʱ�����ȡ����״̬��Ϣ
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            if (status.Status1.Ver[1] >= 6)
            { 
                //nozuo
            }
            else if (RF_Type == 0)
                Thread.Sleep(50);
            else
                Thread.Sleep(150);

            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
        }


        #endregion

        #region RF_Sample ����
        /// <summary>���Ų���
        /// 
        /// </summary>
        /// <param name="Addr">���ŵ�ַ</param>
        /// <param name="Lvl">����ȼ�</param>
        /// <param name="status">���ŷ��ص�״̬</param>
        /// <returns></returns>
        private bool RF_Sample(int Addr,
                              int Lvl,
                              ref PowerStatus status)
        {
           // return false;
            bool RF_Succ = true;

            RFSignal.RFStatus(Addr, ref status);

            RFSignal.RFClear(Addr, Lvl);
            
            //�Թ��Ź̼��汾ȷ���Ƿ���Ҫ��ʱ
            if (status.Status1.Ver[1] < 6)
                Thread.Sleep(300);

            RFSignal.RFSample(Addr, Lvl);

            RFSignal.RFStart(Addr);

            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(5000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(5000, true);
                power2_Handle.Reset();
            }

            //û�з�������ͨ�ų�ʱ�����ȡ����״̬��Ϣ
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
        }

        #endregion

        #region RF_SetFirst �򿪹��ţ����ù��ʣ��رչ��ţ����ù���
        /// <summary>���ù��ʣ��򿪹��ţ����ù��ʣ��رչ���
        /// 
        /// </summary>
        /// <param name="Addr">���ŵ�ַ</param>
        /// <param name="Lvl">�����</param>
        /// <param name="P">����</param>
        /// <param name="RFon">true=�򿪹��ţ�false=�رչ���</param>
        /// <returns></returns>
        internal bool RF_SetFirst(int Addr,
                           int Lvl, float P, bool RFon)
        {
            //return false;
            bool RF_Succ = true;

            RFSignal.RFClear(Addr, Lvl);

            if (RFon)
            {
                RFSignal.RFOn(Addr, Lvl);//�򿪹���
            }
            else
            {
                RFSignal.RFOff(Addr, Lvl);//�رչ���
            }

            RFSignal.RFPower(Addr, Lvl, P);//���ù���

            RFSignal.RFStart(Addr);

            //�ȴ�����
            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(5000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(5000, true);
                power2_Handle.Reset();
            }
            if (RF_Type == 0)
                Thread.Sleep(50);
            else
                Thread.Sleep(200);
            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
        }
        #endregion

        #endregion
    }
}
