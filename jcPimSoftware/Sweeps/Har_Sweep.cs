using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using SpectrumLib;
using System.IO;

namespace jcPimSoftware
{
    class Har_Sweep
    {
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

        /// <summary>
        /// г����Ƶ��ֵ��Ĭ��Ϊ2��Ƶ��������г��
        /// </summary>
        private int multiplier;
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


        #region ɨ��ѭ�����ƺ���
        /// <summary>
        /// �����̣߳���ѭ����Ϊ������
        /// ���������߳�
        /// </summary>
        internal void StartSweep()
        {
            Thread thrd = new Thread(Exectue);
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
            float dBmValue = float.MinValue;
            float dBmNoise = 0;
            PointF[] values;

            //��ȡƵ�׷�������
            values = (PointF[])ISpectrumObj.GetSpectrumData();

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
        #endregion


        #region ɨ��ѭ���ĳ�ʼ������
        /// <summary>
        /// ����ʵ������
        /// </summary>
        internal void InitSweep()
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
        }

        /// <summary>
        /// ׼��ɨ�����
        /// </summary>
        /// <param name="sweeps"></param>
        internal void Prepare(SweepParams usr_sweeps, int multiplier)
        {
            usr_sweeps.Clone(exe_params);
            
            this.multiplier = multiplier;

            //���ù�����Ϣ��Ŀ�괰����
            RFSignal.SetWndHandle(exe_params.WndHandle);

            //����Ƶ�׷�������
            if (exe_params.DevInfo.Spectrum == SpectrumType.SPECAT2)
                ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);

            else
                ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);

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

            try
            {
                if (exe_params.SweepType == SweepType.Time_Sweep)
                {
                    if (exe_params.RFInvolved == RFInvolved.Rf_1)
                        Time_Sweep_1();

                    else if (exe_params.RFInvolved == RFInvolved.Rf_2)
                        Time_Sweep_2();
                }
                else if (exe_params.SweepType == SweepType.Freq_Sweep)
                {
                    if (exe_params.RFInvolved == RFInvolved.Rf_1)
                        Freq_Sweep_1();

                    else if (exe_params.RFInvolved == RFInvolved.Rf_2)
                        Freq_Sweep_2();
                }

            }
            finally
            {

                //�رչ���1           
                RF_Do(exe_params.DevInfo.RF_Addr1, exe_params.RFPriority,
                      0.0f, 0.0f, false, false, false, false, ref rfStatus_1);

                //�رչ���2           
                RF_Do(exe_params.DevInfo.RF_Addr2, exe_params.RFPriority,
                      0.0f, 0.0f, false, false, false, false, ref rfStatus_2);

                //��WndHandle������Ϣ����֪��������еķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SWEEP_DONE, (uint)exe_params.DevInfo.RF_Addr1, 0);
            }

        }

        private void Time_Sweep_1()
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
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx * multiplier - 0.15f; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx * multiplier + 0.15f; //MHz

            //��������г��ɨʱ����
            for (int I = 0; I < exe_params.TmeParam.N; I++)
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

                //����Ƶ�׷��������ȴ�������ɣ�
                //�ڽ���Ƶ�����ݺ����У�����������ֵ
                //��ģ�鴰�����Ϣ�����м��Ƶ���쳣
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SUCCED, (uint)exe_params.DevInfo.RF_Addr1, 0);
            }
        }

        private void Time_Sweep_2()
        {
            bool bQuit = false;
            bool bErrors = false;

            //��������
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                            true, false, true, true, ref rfStatus_2);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors = CheckRF_2(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            //����Ƶ�׷����Ĳ�������
            //RBW��ATT��Unit, Continued�����ⲿ����
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx * multiplier - 0.15f; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx * multiplier + 0.15f; //MHz

            //��������г��ɨʱ����
            for (int I = 0; I < exe_params.TmeParam.N; I++)
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

                //����Ƶ�׷��������ȴ�������ɣ�
                //�ڽ���Ƶ�����ݺ����У�����������ֵ
                //��ģ�鴰�����Ϣ�����м��Ƶ���쳣
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SUCCED, (uint)exe_params.DevInfo.RF_Addr2, 0);
            }
        }

        private void Freq_Sweep_1()
        {
            bool bQuit = false;
            bool bErrors = false;

            float f = exe_params.FrqParam.Items1[0].Tx1;
            float p = exe_params.FrqParam.Items1[0].P1;
            float rx = 0;

            //��������
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                            exe_params.RFPriority,
                            p, f,
                            true, false, true, true, ref rfStatus_1);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors = CheckRF_1(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                return;
            }

            //��������г��ɨʱ����
            for (int I = 0; I < exe_params.FrqParam.Items1.Length; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                f = exe_params.FrqParam.Items1[I].Tx1;
                p = exe_params.FrqParam.Items1[I].P1;

                //������ѯ����ȡ���ŵ�ǰ״̬
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                               exe_params.RFPriority,
                               p, f,
                               false, true, true, true, ref rfStatus_1);

                //��鹦���쳣���󣬰�������ͨ�ų�ʱ
                bErrors = CheckRF_1(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    break;
                }

                //����Ƶ�׷����Ĳ�������
                //RBW��ATT��Unit, Continued�����ⲿ����
                rx = exe_params.FrqParam.Items1[I].Rx;

                exe_params.SpeParam.StartFreq = rx * multiplier - 0.15f; //MHz
                exe_params.SpeParam.EndFreq = rx * multiplier + 0.15f; //MHz

                //����Ƶ�׷��������ȴ�������ɣ�
                //�ڽ���Ƶ�����ݺ����У�����������ֵ
                //��ģ�鴰�����Ϣ�����м��Ƶ���쳣
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SUCCED, (uint)exe_params.DevInfo.RF_Addr1, 0);
            }
        }

        private void Freq_Sweep_2()
        {
            bool bQuit = false;
            bool bErrors = false;

            float f = exe_params.FrqParam.Items2[0].Tx2;
            float p = exe_params.FrqParam.Items2[0].P2;
            float rx = 0;

            //��������
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            p, f,
                            true, false, true, true, ref rfStatus_2);

            //��鹦���쳣���󣬰�������ͨ�ų�ʱ
            bErrors = CheckRF_2(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            //��������г��ɨʱ����
            for (int I = 0; I < exe_params.FrqParam.Items2.Length; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                f = exe_params.FrqParam.Items2[I].Tx2;
                p = exe_params.FrqParam.Items2[I].P2;

                //������ѯ����ȡ���ŵ�ǰ״̬
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority,
                               p, f,
                               false, true, true, true, ref rfStatus_2);

                //��鹦���쳣���󣬰�������ͨ�ų�ʱ
                bErrors = CheckRF_2(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    break;
                }

                //����Ƶ�׷����Ĳ�������
                //RBW��ATT��Unit, Continued�����ⲿ����
                rx = exe_params.FrqParam.Items2[I].Rx;

                exe_params.SpeParam.StartFreq = rx * multiplier - 0.15f; //MHz
                exe_params.SpeParam.EndFreq = rx * multiplier + 0.15f; //MHz

                //����Ƶ�׷��������ȴ�������ɣ�
                //�ڽ���Ƶ�����ݺ����У�����������ֵ
                //��ģ�鴰�����Ϣ�����м��Ƶ���쳣
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //��WndHandle������Ϣ����֪���һ����ķ���
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SUCCED, (uint)exe_params.DevInfo.RF_Addr2, 0);
            }
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

            //����ͨ�ų�ʱ�����
            return (!RF_Succ);
        }

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
    }
}