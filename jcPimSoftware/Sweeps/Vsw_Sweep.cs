using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using SpectrumLib;
using System.Drawing;

namespace jcPimSoftware
{
    class Vsw_Sweep
    {
        #region ��������

        #region ����ɨ��ѭ����ʵ������
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
        /// �ȴ�ѭ�������˳��ľ��
        /// </summary>
        private ManualResetEvent thrd_Handle;

        /// <summary>
        /// Ƶ�׷����ӿ�
        /// </summary>
        private ISpectrum ISpectrumObj;
        #endregion


        #region ɨ�����ʵ������
        /// <summary>
        /// ִ��ɨ���������ɨ�躯���У�Ӧ��ʹ�ô˲���
        /// ����usr_sweeps���ƹ���
        /// </summary>
        private SweepParams exe_params;
        #endregion


        #region �豸�쳣��Ϣ
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

        #region ����״̬��ɨ��������
        internal struct ResultObj
        {
            public PowerStatus Pstatus;
            public SweepResult Sstatus;
        }
        internal ResultObj CurrentResultObj;

        #endregion

        #endregion


        #region ����

        #region ɨ��ѭ�����ƺ���
        /// <summary>
        /// �����̣߳���ѭ����Ϊ������
        /// ���������߳�
        /// </summary>
        public void StartSweep()
        {
            Thread thrd = new Thread(Exectue);
            thrd.IsBackground = true;

            thrd.Start();
        }

        /// <summary>
        /// ��ǰ��ɨ����̽�����ֹͣɨ����̣�
        /// �ȴ���ֱ��ѭ���������������߳�ʱ
        /// </summary>
        public void StopSweep(int timeOut)
        {
            if (ctrl != null)
            {
                Monitor.Enter(ctrl);
                ctrl.Quit = true;
                Monitor.Exit(ctrl);
            }

            //thrd_Handle.WaitOne(timeOut);
            //thrd_Handle.Reset();
        }

        /// <summary>
        /// �����յ�����1ִ�гɹ�����Ϣ�󣬵��øú����������֮ѭ��
        /// </summary>
        public void Power1_Succed()
        {
            power1_Handle.Set();
        }

        /// <summary>
        /// �����յ�����2ִ�гɹ�����Ϣ�󣬵��øú����������֮ѭ��
        /// </summary>
        public void Power2_Succed()
        {
            power2_Handle.Set();
        }

        /// <summary>
        /// �����յ�Ƶ�׷���ִ�гɹ�����Ϣ�󣬵��øú����������֮ѭ��
        /// </summary>
        public void Spectrum_Succed()
        {
            bool bQuit = false;
            float dBmValue = float.MinValue;
            float dBmNoise = 0;
            PointF[] values;

            bQuit = ctrl.Quit;
            if (!bQuit)
            {
                //��ȡƵ�׷�������
                values = (PointF[])ISpectrumObj.GetSpectrumData();
                if (values == null)
                    return;

                //��ȡ�õ�Ƶ�׷��������У�����Yֵ���㣬����Yֵ��Ϊ����ֵ
                for (int J = 0; J < values.Length; J++)
                    if (values[J].Y > dBmValue)
                        dBmValue = values[J].Y;
                //����ƽ������
                for (int K = 0; K < values.Length; K++)
                {
                    if (values[K].Y != dBmValue)
                        dBmNoise += values[K].Y;
                }
                dBmNoise = dBmNoise / values.Length;

                //����ɨ����ֵ����
                sweepValue.dBmValue = dBmValue;
                sweepValue.dBmNosie = dBmNoise;
            }
        }
        #endregion

        #region  ѭ��������
        // <summary>
        /// ����exe_sweeps�е�ɨ�������ִ��һϵ�ж���
        /// �����������š�Ƶ���ǡ���Ⲣ�����쳣��Ϣ������ɨ��������ȡ�豸״̬
        /// </summary>
        private void Exectue()
        {
            Monitor.Enter(ctrl);
            ctrl.Quit = false;
            Monitor.Exit(ctrl);

            ISpectrumObj.EnableLog();

            try
            {
                //��Ƶ
                if (exe_params.SweepType == SweepType.Time_Sweep)
                {
                    if (exe_params.RFInvolved == RFInvolved.Rf_1)
                        VSWR_Time_Sweep_1(0.15f);

                    else if (exe_params.RFInvolved == RFInvolved.Rf_2)
                        VSWR_Time_Sweep_2(0.15f);
                }
                //ɨƵ
                if (exe_params.SweepType == SweepType.Freq_Sweep)
                {
                    if (exe_params.RFInvolved == RFInvolved.Rf_1)
                        VSWR_Freq_Sweep_1(exe_params.FrqParam.Items1, 0.15f);
                    else if (exe_params.RFInvolved == RFInvolved.Rf_2)
                        VSWR_Freq_Sweep_2(exe_params.FrqParam.Items2, 0.15f);
                }
            }
            catch
            {

            }
            finally
            {
                //��WndHandle������Ϣ����֪��������еķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);
            }

            //ָʾѭ���Ѿ���������
            //thrd_Handle.Set();
        }

        #endregion

        #region ɨ���ʼ��
        /// <summary>
        /// ɨ���ʼ��
        /// </summary>
        public void InitSweep()
        {
            ctrl = new SweepCtrl();

            exe_params = new SweepParams();

            power1_Handle = new ManualResetEvent(false);

            power2_Handle = new ManualResetEvent(false);

            thrd_Handle = new ManualResetEvent(false);

            sweepValue = new SweepResult();

            rfStatus_1 = new PowerStatus();

            rfStatus_2 = new PowerStatus();

            rfErrors_1 = new RFErrors();

            rfErrors_2 = new RFErrors();

            speErrors = new SpectrumErrors();
        }

        #endregion

        #region ������ʼ��
        /// <summary>
        /// ������ʼ��
        /// </summary>
        /// <param name="usr_sweeps">��������</param>
        public void Prepare(SweepParams usr_sweeps)
        {
            usr_sweeps.Clone(exe_params);

            //���ù�����Ϣ��Ŀ�괰����
            RFSignal.SetWndHandle(exe_params.WndHandle);

            //����Ƶ�׷�������
            if (exe_params.DevInfo.Spectrum == SpectrumType.SPECAT2)
                ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);

            else
                ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
        }

        #endregion

        #region פ���ȵ�Ƶ

        /// <summary>
        /// ����һ��Ƶ
        /// </summary>
        /// <param name="BandWidth">ɨ�����(MHz)</param>
        private void VSWR_Time_Sweep_1(float BandWidth)
        {
            bool bQuit = false;
            bool bErrors = false;

            //��������
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                            exe_params.RFPriority,
                            exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                            true, false, true, true, ref rfStatus_1);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors = CheckRF_1(bErrors);
            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                return;
            }

            //����Ƶ�׷����Ĳ�������
            //RBW��ATT��Unit, Continued�����ⲿ����
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx - BandWidth; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx + BandWidth;   //MHz

            //����פ����ɨʱ����
            for (int i = 0; i < exe_params.TmeParam.N; i++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                //������ѯ����ȡ���ŵ�ǰ״̬
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                               exe_params.RFPriority,
                               exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                               false, true, false, false, ref rfStatus_1);
                //��鹦���쳣���󣬰�������ͨ�ų�ʱ
                bErrors = CheckRF_1(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    break;
                }

                //����Ƶ�׷���
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);
                
                //��װɨ����
                CurrentResultObj.Pstatus = rfStatus_1;
                CurrentResultObj.Sstatus = sweepValue;

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////��WndHandle������Ϣ����֪��������еķ���
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //�رչ���           
            RF_Do(exe_params.DevInfo.RF_Addr1,
                   exe_params.RFPriority,
                   exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                   false, false, false, false, ref rfStatus_1);   
        }

        /// <summary>
        /// ���Ŷ���Ƶ
        /// </summary>
        /// <param name="BandWidth"></param>
        private void VSWR_Time_Sweep_2(float BandWidth)
        {
            bool bQuit = false;
            bool bErrors = false;

            //��������
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                            true, false, true, true, ref rfStatus_2);

            bErrors = CheckRF_2(bErrors);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            //����Ƶ�׷����Ĳ�������
            //RBW��ATT��Unit, Continued�����ⲿ����
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx - BandWidth; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx + BandWidth;   //MHz

            //����פ����ɨʱ����
            for (int i = 0; i < exe_params.TmeParam.N; i++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                //������ѯ����ȡ���ŵ�ǰ״̬
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority,
                               exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                               false, true, false, false, ref rfStatus_2);
                //��鹦���쳣���󣬰�������ͨ�ų�ʱ
                bErrors = CheckRF_2(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    break;
                }

                //����Ƶ�׷���
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��װɨ����
                CurrentResultObj.Pstatus = rfStatus_2;
                CurrentResultObj.Sstatus = sweepValue;

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////��WndHandle������Ϣ����֪��������еķ���
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //�رչ���           
            RF_Do(exe_params.DevInfo.RF_Addr2,
                   exe_params.RFPriority,
                   exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                   false, false, false, false, ref rfStatus_2);
        }

        #endregion

        #region פ����ɨƵ

        /// <summary>
        /// ����һɨƵ
        /// </summary>
        private void VSWR_Freq_Sweep_1(FreqSweepItem[] item, float BandWidth)
        {
            bool bQuit = false;
            bool bErrors1 = false;

            //��������1
            bErrors1 = RF_Do(exe_params.DevInfo.RF_Addr1,
                            exe_params.RFPriority,
                            item[0].P1, item[0].Tx1,
                            true, false, true, true, ref rfStatus_1);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors1 = CheckRF_1(bErrors1);

            if (bErrors1)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                return;
            }

            for (int i = 0; i < item.Length; i++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                //���ù���1Ƶ�ʡ�����
                bErrors1 = RF_Do(exe_params.DevInfo.RF_Addr1,
                               exe_params.RFPriority,
                               item[i].P1, item[i].Tx1,
                               false, true, true, true, ref rfStatus_1);


                //��鹦���쳣���󣬰�������ͨ�ų�ʱ
                bErrors1 = CheckRF_1(bErrors1);

                if (bErrors1)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    break;
                }

                //����Ƶ�׷����Ĳ�������
                //RBW��ATT��Unit, Continued�����ⲿ����
                exe_params.SpeParam.StartFreq = item[i].Rx - BandWidth; //MHz
                exe_params.SpeParam.EndFreq = item[i].Rx + BandWidth; //MHz

                //����Ƶ�׷���
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��װɨ����
                CurrentResultObj.Pstatus = rfStatus_1;
                CurrentResultObj.Sstatus = sweepValue;

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////��WndHandle������Ϣ����֪��������еķ���
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //�رչ���1           
            RF_Do(exe_params.DevInfo.RF_Addr1,
                   exe_params.RFPriority,
                   item[0].P1, item[0].Tx1,
                   false, false, false, false, ref rfStatus_1);
        }

        /// <summary>
        /// ���Ŷ�ɨƵ
        /// </summary>
        private void VSWR_Freq_Sweep_2(FreqSweepItem[] item, float BandWidth)
        {
            bool bQuit = false;
            bool bErrors2 = false;

            //��������
            bErrors2 = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            item[0].P2, item[0].Tx2,
                            true, false, true, true, ref rfStatus_2);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors2 = CheckRF_2(bErrors2);

            if (bErrors2)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            for (int i = 0; i < item.Length; i++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                //���ù���2Ƶ�ʡ�����
                bErrors2 = RF_Do(exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority,
                               item[i].P2, item[i].Tx2,
                               false, true, true, true, ref rfStatus_2);

                //��鹦���쳣���󣬰�������ͨ�ų�ʱ
                bErrors2 = CheckRF_2(bErrors2);

                if (bErrors2)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    break;
                }

                //����Ƶ�׷����Ĳ�������
                //RBW��ATT��Unit, Continued�����ⲿ����
                exe_params.SpeParam.StartFreq = item[i].Rx - BandWidth; //MHz
                exe_params.SpeParam.EndFreq = item[i].Rx + BandWidth; //MHz

                //����Ƶ�׷���
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��װɨ����
                CurrentResultObj.Pstatus = rfStatus_2;
                CurrentResultObj.Sstatus = sweepValue;

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////��WndHandle������Ϣ����֪��������еķ���
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //�رչ���2           
            RF_Do(exe_params.DevInfo.RF_Addr2,
                   exe_params.RFPriority,
                   item[0].P2, item[0].Tx2,
                   false, false, false, false, ref rfStatus_2);
        }

        #endregion

        #region ��ȡɨ����
        /// <summary>
        /// ��ȡɨ����
        /// </summary>
        /// <returns></returns>
        public ResultObj GetVswrScanResult()
        {
            return CurrentResultObj;
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
                RF_Succ = power1_Handle.WaitOne(2000, true);
                power1_Handle.Reset();
                //Log.WriteLog("����1ͨѶ��ʱ", Log.EFunctionType.VSWR);
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(2000, true);
                power2_Handle.Reset();
                //Log.WriteLog("����2ͨѶ��ʱ", Log.EFunctionType.VSWR);
            }

            //û�з�������ͨ�ų�ʱ�����ȡ����״̬��Ϣ
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
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

            if ((rfStatus_1.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr))
            {
                errors = true;
                rfErrors_1.RF_VswrError = true;
                rfErrors_1.RF_VswrValue = rfStatus_1.Status2.Vswr;
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

        #endregion

        #region Ƶ���쳣���
        /// <summary>
        /// ���Ƶ���쳣������ʲô���󣬶�����ͨ���쳣
        /// </summary>
        /// <param name="timeOut"></param>
        private void CheckSpectrum(bool timeOut)
        {
            speErrors.Spectrum_TimeOut = timeOut;
        }

        #endregion

        #region ��¡�豸״̬��Ϣ������
        public void CloneReference(ref PowerStatus ps1,
                                   ref PowerStatus ps2,
                                   ref SweepResult sr,
                                   ref RFErrors rfr_errors1,
                                   ref RFErrors rfr_errors2)
        {
            ps1 = rfStatus_1;
            ps2 = rfStatus_2;
            sr = sweepValue;
            rfr_errors1 = rfErrors_1;
            rfr_errors2 = rfErrors_2;
        }
        #endregion

        #endregion
    }
}
