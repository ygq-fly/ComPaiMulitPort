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
        #region 控制扫描循环的实例变量
        /// <summary>
        /// 扫描循环的控制结构，
        /// 包括停止标志位、退出标志位
        /// </summary>
        private SweepCtrl ctrl;

        /// <summary>
        /// 功放1等待句柄
        /// </summary>
        private ManualResetEvent power1_Handle;

        /// <summary>
        /// 功放2等待句柄
        /// </summary>
        private ManualResetEvent power2_Handle;

        /// <summary>
        /// 等待循环正常退出的句柄
        /// </summary>
        private ManualResetEvent thrd_Handle;

        /// <summary>
        /// 频谱分析接口
        /// </summary>
        private ISpectrum ISpectrumObj;

        /// <summary>
        /// 谐波倍频数值，默认为2倍频，即二次谐波
        /// </summary>
        private int multiplier;
        #endregion


        #region 扫描参数实例变量
        /// <summary>
        /// 执行扫描参数，在扫描函数中，应该使用此参数
        /// 它从usr_sweeps复制过来
        /// </summary>
        private SweepParams exe_params;
        #endregion


        #region 设备异常信息
        /// <summary>
        /// 功放1异常信息对象
        /// </summary>
        private RFErrors rfErrors_1;

        /// <summary>
        /// 功放2异常信息对象
        /// </summary>
        private RFErrors rfErrors_2;
        #endregion


        #region 功放状态信息
        /// <summary>
        /// 功放1状态信息对象
        /// </summary>
        private PowerStatus rfStatus_1;

        /// <summary>
        /// 功放2状态信息对象
        /// </summary>
        private PowerStatus rfStatus_2;
        #endregion


        #region 扫描结果的数值对象
        private SweepResult sweepValue;
        #endregion


        #region 扫描循环控制函数
        /// <summary>
        /// 建立线程，将循环作为主函数
        /// 接着启动线程
        /// </summary>
        internal void StartSweep()
        {
            Thread thrd = new Thread(Exectue);
            thrd.IsBackground = true;

            thrd.Start();
        }

        /// <summary>
        /// 当前次扫描过程结束后，停止扫描进程，
        /// 等待，直到循环正常结束，或者超时
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
        /// 窗体收到功放1执行成功的消息后，调用该函数，将其告之循环
        /// </summary>
        internal void Power1_Succed()
        {
            power1_Handle.Set();
        }

        /// <summary>
        /// 窗体收到功放2执行成功的消息后，调用该函数，将其告之循环
        /// </summary>
        internal void Power2_Succed()
        {
            power2_Handle.Set();
        }

        /// <summary>
        /// 窗体收到频谱分析执行成功的消息后，调用该函数，将其告之循环
        /// </summary>
        internal void Spectrum_Succed()
        {
            float dBmValue = float.MinValue;
            float dBmNoise = 0;
            PointF[] values;

            //获取频谱分析数据
            values = (PointF[])ISpectrumObj.GetSpectrumData();

            //在取得的频谱分析数据中，搜索Y值最大点，将其Y值作为收信值
            for (int J = 0; J < values.Length; J++)
                if (values[J].Y > dBmValue)
                    dBmValue = values[J].Y;

            //计算平均底噪
            for (int K = 0; K < values.Length; K++)
            {
                if (values[K].Y != dBmValue)
                    dBmNoise += values[K].Y;
            }

            dBmNoise = dBmNoise / values.Length;

            //构造扫描结果值对象
            sweepValue.dBmValue = dBmValue;
            sweepValue.dBmNosie = dBmNoise;
        }
        #endregion


        #region 扫描循环的初始化函数
        /// <summary>
        /// 建立实例变量
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
        /// 准备扫描参数
        /// </summary>
        /// <param name="sweeps"></param>
        internal void Prepare(SweepParams usr_sweeps, int multiplier)
        {
            usr_sweeps.Clone(exe_params);
            
            this.multiplier = multiplier;

            //设置功放消息的目标窗体句柄
            RFSignal.SetWndHandle(exe_params.WndHandle);

            //建立频谱分析对象
            if (exe_params.DevInfo.Spectrum == SpectrumType.SPECAT2)
                ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);

            else
                ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);

        }
        #endregion


        #region  扫描循环的主函数
        // <summary>
        /// 根据exe_sweeps中的扫描参数，执行一系列动作
        /// 包括操作功放、频谱仪、检测并发送异常信息、发送扫描结果，获取设备状态
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

                //关闭功放1           
                RF_Do(exe_params.DevInfo.RF_Addr1, exe_params.RFPriority,
                      0.0f, 0.0f, false, false, false, false, ref rfStatus_1);

                //关闭功放2           
                RF_Do(exe_params.DevInfo.RF_Addr2, exe_params.RFPriority,
                      0.0f, 0.0f, false, false, false, false, ref rfStatus_2);

                //向WndHandle发送消息，告知完成上所有的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SWEEP_DONE, (uint)exe_params.DevInfo.RF_Addr1, 0);
            }

        }

        private void Time_Sweep_1()
        {
            bool bQuit = false;
            bool bErrors = false;

            //开启功放
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                            exe_params.RFPriority,
                            exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                            true, false, true, true, ref rfStatus_1);


            //检查功放异常现象，包括功放通信超时
            bErrors = CheckRF_1(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                return;
            }

            //设置频谱分析的参数对象
            //RBW，ATT，Unit, Continued，由外部设置
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx * multiplier - 0.15f; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx * multiplier + 0.15f; //MHz

            //启动二次谐波扫时进程
            for (int I = 0; I < exe_params.TmeParam.N; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                //采样查询，获取功放当前状态
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                               exe_params.RFPriority,
                               exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                               false, true, false, false, ref rfStatus_1);

                //检查功放异常现象，包括功放通信超时
                bErrors = CheckRF_1(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    break;
                }

                //启动频谱分析，并等待分析完成，
                //在接收频谱数据函数中，计算分析结果值
                //在模块窗体的消息函数中检查频谱异常
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //向WndHandle发送消息，告知完成一个点的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.ISO_SUCCED, (uint)exe_params.DevInfo.RF_Addr1, 0);
            }
        }

        private void Time_Sweep_2()
        {
            bool bQuit = false;
            bool bErrors = false;

            //开启功放
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                            true, false, true, true, ref rfStatus_2);

            //检查功放异常现象，包括功放通信超时
            bErrors = CheckRF_2(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            //设置频谱分析的参数对象
            //RBW，ATT，Unit, Continued，由外部设置
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx * multiplier - 0.15f; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx * multiplier + 0.15f; //MHz

            //启动二次谐波扫时进程
            for (int I = 0; I < exe_params.TmeParam.N; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                //采样查询，获取功放当前状态
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority,
                               exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                               false, true, false, false, ref rfStatus_2);

                //检查功放异常现象，包括功放通信超时
                bErrors = CheckRF_2(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    break;
                }

                //启动频谱分析，并等待分析完成，
                //在接收频谱数据函数中，计算分析结果值
                //在模块窗体的消息函数中检查频谱异常
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //向WndHandle发送消息，告知完成一个点的分析
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

            //开启功放
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                            exe_params.RFPriority,
                            p, f,
                            true, false, true, true, ref rfStatus_1);

            //检查功放异常现象，包括功放通信超时
            bErrors = CheckRF_1(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                return;
            }

            //启动二次谐波扫时进程
            for (int I = 0; I < exe_params.FrqParam.Items1.Length; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                f = exe_params.FrqParam.Items1[I].Tx1;
                p = exe_params.FrqParam.Items1[I].P1;

                //采样查询，获取功放当前状态
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr1,
                               exe_params.RFPriority,
                               p, f,
                               false, true, true, true, ref rfStatus_1);

                //检查功放异常现象，包括功放通信超时
                bErrors = CheckRF_1(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    break;
                }

                //设置频谱分析的参数对象
                //RBW，ATT，Unit, Continued，由外部设置
                rx = exe_params.FrqParam.Items1[I].Rx;

                exe_params.SpeParam.StartFreq = rx * multiplier - 0.15f; //MHz
                exe_params.SpeParam.EndFreq = rx * multiplier + 0.15f; //MHz

                //启动频谱分析，并等待分析完成，
                //在接收频谱数据函数中，计算分析结果值
                //在模块窗体的消息函数中检查频谱异常
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //向WndHandle发送消息，告知完成一个点的分析
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

            //开启功放
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            p, f,
                            true, false, true, true, ref rfStatus_2);

            //检查功放异常现象，包括功放通信超时
            bErrors = CheckRF_2(bErrors);

            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            //启动二次谐波扫时进程
            for (int I = 0; I < exe_params.FrqParam.Items2.Length; I++)
            {
                Monitor.Enter(ctrl);
                bQuit = ctrl.Quit;
                Monitor.Exit(ctrl);

                if (bQuit)
                    break;

                f = exe_params.FrqParam.Items2[I].Tx2;
                p = exe_params.FrqParam.Items2[I].P2;

                //采样查询，获取功放当前状态
                bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority,
                               p, f,
                               false, true, true, true, ref rfStatus_2);

                //检查功放异常现象，包括功放通信超时
                bErrors = CheckRF_2(bErrors);
                if (bErrors)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    break;
                }

                //设置频谱分析的参数对象
                //RBW，ATT，Unit, Continued，由外部设置
                rx = exe_params.FrqParam.Items2[I].Rx;

                exe_params.SpeParam.StartFreq = rx * multiplier - 0.15f; //MHz
                exe_params.SpeParam.EndFreq = rx * multiplier + 0.15f; //MHz

                //启动频谱分析，并等待分析完成，
                //在接收频谱数据函数中，计算分析结果值
                //在模块窗体的消息函数中检查频谱异常
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //向WndHandle发送消息，告知完成一个点的分析
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

            //等待功放           
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

            //没有发生功放通信超时，则获取功放状态信息
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            //返回通信超时的情况
            return (!RF_Succ);
        }

        /// <summary>
        /// 依据全局静态的功放设备限制条件进行异常检查
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
        /// 依据全局静态的功放设备限制条件进行异常检查
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


        #region 克隆设备状态信息的引用
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