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
        #region 变量定义

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
        /// 频谱异常信息对象
        /// </summary>
        private SpectrumErrors speErrors;

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

        #region 功放状态和扫描结果对象
        internal struct ResultObj
        {
            public PowerStatus Pstatus;
            public SweepResult Sstatus;
        }
        internal ResultObj CurrentResultObj;

        #endregion

        #endregion


        #region 方法

        #region 扫描循环控制函数
        /// <summary>
        /// 建立线程，将循环作为主函数
        /// 接着启动线程
        /// </summary>
        public void StartSweep()
        {
            Thread thrd = new Thread(Exectue);
            thrd.IsBackground = true;

            thrd.Start();
        }

        /// <summary>
        /// 当前次扫描过程结束后，停止扫描进程，
        /// 等待，直到循环正常结束，或者超时
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
        /// 窗体收到功放1执行成功的消息后，调用该函数，将其告之循环
        /// </summary>
        public void Power1_Succed()
        {
            power1_Handle.Set();
        }

        /// <summary>
        /// 窗体收到功放2执行成功的消息后，调用该函数，将其告之循环
        /// </summary>
        public void Power2_Succed()
        {
            power2_Handle.Set();
        }

        /// <summary>
        /// 窗体收到频谱分析执行成功的消息后，调用该函数，将其告之循环
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
                //获取频谱分析数据
                values = (PointF[])ISpectrumObj.GetSpectrumData();
                if (values == null)
                    return;

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
        }
        #endregion

        #region  循环主函数
        // <summary>
        /// 根据exe_sweeps中的扫描参数，执行一系列动作
        /// 包括操作功放、频谱仪、检测并发送异常信息、发送扫描结果，获取设备状态
        /// </summary>
        private void Exectue()
        {
            Monitor.Enter(ctrl);
            ctrl.Quit = false;
            Monitor.Exit(ctrl);

            ISpectrumObj.EnableLog();

            try
            {
                //点频
                if (exe_params.SweepType == SweepType.Time_Sweep)
                {
                    if (exe_params.RFInvolved == RFInvolved.Rf_1)
                        VSWR_Time_Sweep_1(0.15f);

                    else if (exe_params.RFInvolved == RFInvolved.Rf_2)
                        VSWR_Time_Sweep_2(0.15f);
                }
                //扫频
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
                //向WndHandle发送消息，告知完成上所有的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);
            }

            //指示循环已经正常结束
            //thrd_Handle.Set();
        }

        #endregion

        #region 扫描初始化
        /// <summary>
        /// 扫描初始化
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

        #region 参数初始化
        /// <summary>
        /// 参数初始化
        /// </summary>
        /// <param name="usr_sweeps">参数对象</param>
        public void Prepare(SweepParams usr_sweeps)
        {
            usr_sweeps.Clone(exe_params);

            //设置功放消息的目标窗体句柄
            RFSignal.SetWndHandle(exe_params.WndHandle);

            //建立频谱分析对象
            if (exe_params.DevInfo.Spectrum == SpectrumType.SPECAT2)
                ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);

            else
                ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(exe_params.WndHandle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
        }

        #endregion

        #region 驻波比点频

        /// <summary>
        /// 功放一点频
        /// </summary>
        /// <param name="BandWidth">扫描带宽(MHz)</param>
        private void VSWR_Time_Sweep_1(float BandWidth)
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
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx - BandWidth; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx + BandWidth;   //MHz

            //启动驻波比扫时进程
            for (int i = 0; i < exe_params.TmeParam.N; i++)
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

                //启动频谱分析
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);
                
                //封装扫描结果
                CurrentResultObj.Pstatus = rfStatus_1;
                CurrentResultObj.Sstatus = sweepValue;

                //向WndHandle发送消息，告知完成一个点的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////向WndHandle发送消息，告知完成上所有的分析
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //关闭功放           
            RF_Do(exe_params.DevInfo.RF_Addr1,
                   exe_params.RFPriority,
                   exe_params.TmeParam.P1, exe_params.TmeParam.F1,
                   false, false, false, false, ref rfStatus_1);   
        }

        /// <summary>
        /// 功放二点频
        /// </summary>
        /// <param name="BandWidth"></param>
        private void VSWR_Time_Sweep_2(float BandWidth)
        {
            bool bQuit = false;
            bool bErrors = false;

            //开启功放
            bErrors = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                            true, false, true, true, ref rfStatus_2);

            bErrors = CheckRF_2(bErrors);

            //检查功放异常现象，包括功放通信超时
            if (bErrors)
            {
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                return;
            }

            //设置频谱分析的参数对象
            //RBW，ATT，Unit, Continued，由外部设置
            exe_params.SpeParam.StartFreq = exe_params.TmeParam.Rx - BandWidth; //MHz
            exe_params.SpeParam.EndFreq = exe_params.TmeParam.Rx + BandWidth;   //MHz

            //启动驻波比扫时进程
            for (int i = 0; i < exe_params.TmeParam.N; i++)
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

                //启动频谱分析
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //封装扫描结果
                CurrentResultObj.Pstatus = rfStatus_2;
                CurrentResultObj.Sstatus = sweepValue;

                //向WndHandle发送消息，告知完成一个点的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////向WndHandle发送消息，告知完成上所有的分析
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //关闭功放           
            RF_Do(exe_params.DevInfo.RF_Addr2,
                   exe_params.RFPriority,
                   exe_params.TmeParam.P2, exe_params.TmeParam.F2,
                   false, false, false, false, ref rfStatus_2);
        }

        #endregion

        #region 驻波比扫频

        /// <summary>
        /// 功放一扫频
        /// </summary>
        private void VSWR_Freq_Sweep_1(FreqSweepItem[] item, float BandWidth)
        {
            bool bQuit = false;
            bool bErrors1 = false;

            //开启功放1
            bErrors1 = RF_Do(exe_params.DevInfo.RF_Addr1,
                            exe_params.RFPriority,
                            item[0].P1, item[0].Tx1,
                            true, false, true, true, ref rfStatus_1);

            //检查功放异常现象，包括功放通信超时
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

                //设置功放1频率、功率
                bErrors1 = RF_Do(exe_params.DevInfo.RF_Addr1,
                               exe_params.RFPriority,
                               item[i].P1, item[i].Tx1,
                               false, true, true, true, ref rfStatus_1);


                //检查功放异常现象，包括功放通信超时
                bErrors1 = CheckRF_1(bErrors1);

                if (bErrors1)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
                    break;
                }

                //设置频谱分析的参数对象
                //RBW，ATT，Unit, Continued，由外部设置
                exe_params.SpeParam.StartFreq = item[i].Rx - BandWidth; //MHz
                exe_params.SpeParam.EndFreq = item[i].Rx + BandWidth; //MHz

                //启动频谱分析
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //封装扫描结果
                CurrentResultObj.Pstatus = rfStatus_1;
                CurrentResultObj.Sstatus = sweepValue;

                //向WndHandle发送消息，告知完成一个点的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////向WndHandle发送消息，告知完成上所有的分析
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //关闭功放1           
            RF_Do(exe_params.DevInfo.RF_Addr1,
                   exe_params.RFPriority,
                   item[0].P1, item[0].Tx1,
                   false, false, false, false, ref rfStatus_1);
        }

        /// <summary>
        /// 功放二扫频
        /// </summary>
        private void VSWR_Freq_Sweep_2(FreqSweepItem[] item, float BandWidth)
        {
            bool bQuit = false;
            bool bErrors2 = false;

            //开启功放
            bErrors2 = RF_Do(exe_params.DevInfo.RF_Addr2,
                            exe_params.RFPriority,
                            item[0].P2, item[0].Tx2,
                            true, false, true, true, ref rfStatus_2);

            //检查功放异常现象，包括功放通信超时
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

                //设置功放2频率、功率
                bErrors2 = RF_Do(exe_params.DevInfo.RF_Addr2,
                               exe_params.RFPriority,
                               item[i].P2, item[i].Tx2,
                               false, true, true, true, ref rfStatus_2);

                //检查功放异常现象，包括功放通信超时
                bErrors2 = CheckRF_2(bErrors2);

                if (bErrors2)
                {
                    NativeMessage.PostMessage(exe_params.WndHandle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
                    break;
                }

                //设置频谱分析的参数对象
                //RBW，ATT，Unit, Continued，由外部设置
                exe_params.SpeParam.StartFreq = item[i].Rx - BandWidth; //MHz
                exe_params.SpeParam.EndFreq = item[i].Rx + BandWidth; //MHz

                //启动频谱分析
                ISpectrumObj.StartAnalysis(exe_params.SpeParam);

                //封装扫描结果
                CurrentResultObj.Pstatus = rfStatus_2;
                CurrentResultObj.Sstatus = sweepValue;

                //向WndHandle发送消息，告知完成一个点的分析
                NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SUCCED, 0, i);
            }

            ////向WndHandle发送消息，告知完成上所有的分析
            //NativeMessage.PostMessage(exe_params.WndHandle, MessageID.VSW_SWEEP_DONE, 0, 0);

            //关闭功放2           
            RF_Do(exe_params.DevInfo.RF_Addr2,
                   exe_params.RFPriority,
                   item[0].P2, item[0].Tx2,
                   false, false, false, false, ref rfStatus_2);
        }

        #endregion

        #region 获取扫描结果
        /// <summary>
        /// 获取扫描结果
        /// </summary>
        /// <returns></returns>
        public ResultObj GetVswrScanResult()
        {
            return CurrentResultObj;
        }

        #endregion

        #region 发送功放命令
        /// <summary>
        /// 发送功放命令
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

            //等待功放
            if (Addr == exe_params.DevInfo.RF_Addr1)
            {
                RF_Succ = power1_Handle.WaitOne(2000, true);
                power1_Handle.Reset();
                //Log.WriteLog("功放1通讯超时", Log.EFunctionType.VSWR);
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(2000, true);
                power2_Handle.Reset();
                //Log.WriteLog("功放2通讯超时", Log.EFunctionType.VSWR);
            }

            //没有发生功放通信超时，则获取功放状态信息
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            //返回通信超时的情况
            return (!RF_Succ);
        }

        #endregion

        #region 功放异常检测
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

        #region 频谱异常检测
        /// <summary>
        /// 检查频谱异常，不论什么错误，都看作通信异常
        /// </summary>
        /// <param name="timeOut"></param>
        private void CheckSpectrum(bool timeOut)
        {
            speErrors.Spectrum_TimeOut = timeOut;
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

        #endregion
    }
}
