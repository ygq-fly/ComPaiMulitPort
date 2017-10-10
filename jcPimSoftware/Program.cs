using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace jcPimSoftware
{
    static class Program
    {
        /// <summary>
        /// 主窗体
        /// </summary>
        private static MainForm frmMain;
        public static MotorSwitch  mSwitch;
        static StartForm sf;
        public static IntPtr handel;
        public static ManualResetEvent mre = new ManualResetEvent(false);
        public static Offset offset;
         
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          
            bool running = false;
            Mutex mt = new Mutex(true, "jcPimSoftware", out running);

            //获取程序执行路径
            string exePath = Application.StartupPath;

            //加载全局配置文件
            App_Configure.NewConfigure(exePath + "\\Configures.ini");
            App_Configure.Cnfgs.LoadSettings();

            //判断授权文件
            Code c = new Code();
            try
            {
                if (File.Exists(Code.strFilePath))
                {
                    if (!c.CheckFile(App_Configure.Cnfgs.SN.ToLower()))
                    {
                        running = false;
                        MessageBox.Show("授权日期已到！");
                    }
                }
                else
                {
                    running = false;
                    MessageBox.Show("请先生成授权文件！");
                }
            }
            catch
            {
                running = false;
                MessageBox.Show("授权文件缺失或错误，请重新生成授权文件！");
            }

            if (running)
            {
                sf = new StartForm("Initializing......\r\n");
                Thread t = new Thread(new ThreadStart(Show));
                t.IsBackground = true;
                t.Start();
            try
            {
                //ygq
                string s = Copy(Application.StartupPath + "\\settings", "D:\\settings");
                //



                #region 加载主配置文件，获取配置文件相对路径
                //获取程序执行路径
                //string exePath = Application.StartupPath;

                //加载全局配置文件
                //App_Configure.NewConfigure(exePath + "\\Configures.ini");
                //App_Configure.Cnfgs.LoadSettings();


                //获取配置文件夹路径
                string setPath = App_Configure.Cnfgs.Path_Def;
                #endregion

                #region 建立功放补偿表格文件对象，并加载数据
                //TX补偿文件
                if (App_Configure.Cnfgs.Ms_switch_port_count<=0)
                {
                    //Tx_Tables.NewTables(exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_rev.ini",
                    //                    exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_disp_rev.ini"
                    //                  );


                    if (App_Configure.Cnfgs.Mode >=2)
                    {
                        Tx_Tables.NewTables(exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_rev.ini",
                                            exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_disp_rev.ini",
                                            exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_rev2.ini",
                                            exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_disp_rev2.ini"
                                          );

                        Tx_Tables.LoadTables();

                        //RX补偿文件
                        string path1 = exePath + "\\" + setPath + "\\RX_Tables";
                        //string[] rx_tables_names = { path1 + "\\pim_rev.txt", path1 + "\\pim_frd.txt" };
                        string[] rx_tables_names = { path1 + "\\pim_rev.txt", path1 + "\\pim_frd.txt",
                                               path1 + "\\pim_rev2.txt", path1 + "\\pim_frd2.txt"};
                        Rx_Tables.NewTables(rx_tables_names);

                        Rx_Tables.LoadTables();
                    }

                    else
                    {
                        Tx_Tables.NewTables(exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_rev.ini",
                                          exePath + "\\" + setPath + "\\Tx_Tables\\signal_tx_disp_rev.ini"
                                        );

                        Tx_Tables.LoadTables2();

                        //RX补偿文件
                        string path1 = exePath + "\\" + setPath + "\\RX_Tables";
                        string[] rx_tables_names = { path1 + "\\pim_rev.txt", path1 + "\\pim_frd.txt" };

                        Rx_Tables.NewTables(rx_tables_names);

                        Rx_Tables.LoadTables();
                    }
                }
                else
                {
                    bool com_client = MsSwithc.ClientCom();
                    //mSwitch = new MotorSwitch();
                    //bool com_client = mSwitch.Connect("192.168.1.178", 4001);
                    if (com_client)
                    {
                        sf.GetInfoMation("Load switch successfully!");
                    }
                    else
                    {
                        sf.GetInfoMation("Load switch failed!");
                    }
                    offset = new Offset( App_Configure.Cnfgs.Ms_switch_port_count);
                    int a = int.Parse(IniFile.GetString("cnfgs", "xuhao", "0", exePath + "\\Configures.ini"));
                    offset.LoadingRX();
                    offset.LoadingTX();
                    offset.GetTX(a);
                    offset.GetRX(a);
                }
                //建立补偿系数文件对象，并加载文件
                App_Factors.NewFactors(exePath + "\\" + setPath + "\\Offsets_Tx.ini",
                                       exePath + "\\" + setPath + "\\Offsets_Tx_Disp.ini",
                                       exePath + "\\" + setPath + "\\Offsets_Rx.ini");
                App_Factors.LoadFactros();

                #endregion

                #region 建立频谱补偿表格文件对象，并加载数据
                //频谱补偿文件
                string path2 = exePath + "\\" + setPath + "\\Spectrum_Tables";
                string[] spc_tables_names = {path2 + "\\Ch1_4KHz.txt", path2 + "\\Ch1_20KHz.txt", path2 + "\\Ch1_100KHz.txt",path2 + "\\Ch1_1000KHz.txt", 
                                         path2 + "\\Ch2_4KHz.txt", path2 + "\\Ch2_20KHz.txt", path2 + "\\Ch2_100KHz.txt",path2 + "\\Ch2_1000KHz.txt"};

                Spectrum_Tables.NewTables(spc_tables_names);
                #endregion;

                #region 建立RLO校准文件对象，并加载数据
                //string path4 = exePath + "\\" + setPath + "\\RL0_Tables";
                //string[] rl0_tables_names = {path4 + "\\iso_tx1.txt", path4 + "\\iso_tx2.txt", 
                //                         path4 + "\\vsw_tx1.txt", path4 + "\\vsw_tx2.txt",
                //                         path4 + "\\har_tx1.txt", path4 + "\\har_tx2.txt"};

                //RL0_Tables.NewTables(rl0_tables_names);
                //RL0_Tables.LoadTables();
                #endregion

                #region 建立默认模块配置信息对象，并加载数据
                //默认模块配置文件
                string path3 =  "D:\\" + setPath;
                string[] settings_names = {path3 + "\\Settings_Sgn.ini",path3 + "\\Settings_Pim.ini",
                                        path3 + "\\Settings_Spc.ini",
                                        path3 + "\\Specifics.ini"};

                App_Settings.NewSettings(settings_names);
                App_Settings.LoadSettings();
                #endregion

                #region 加载皮肤资源
                 FileInfo f = new FileInfo(exePath+"\\"+App_Configure.Cnfgs.Skin_pack_path);
                string name = f.Name.Substring(0, f.Name.LastIndexOf("."));
                ImagesManage.LoadImageDLL(name);
                #endregion

                #region 加载语言包资源

                #endregion


                //建立报告文件夹结构
                App_Configure.CreateReportFolder();

                //建立用户配置文件夹结构
                App_Configure.CreateUsrSettingFolder();

                sf.GetInfoMation("Load default configuration successfully!");
            }
            catch
            {
                sf.GetInfoMation("Load default configuration failed!");
            }
            NativeMessage.PostMessage(handel, MessageID.SF_WAIT,0, 0);
            #region 建立程序主窗体
            frmMain = new MainForm();
            NativeMessage.PostMessage(handel, MessageID.SF_CONTINUTE, 0, 0); 
            #endregion

            #region 建立射频功放层
            RFSignal.InitRFSignal(frmMain.Handle);
            bool flag = false;
            int adrr1 = App_Configure.Cnfgs.ComAddr1;
            int adrr2 = App_Configure.Cnfgs.ComAddr2;
            int rfClass = App_Configure.Cnfgs.RFClass;
            int rFFormula = App_Configure.Cnfgs.RFFormula;

            flag = RFSignal.NewRFSignal(adrr1, rfClass, rFFormula);
            if (flag)
            {
                sf.GetInfoMation("Serial " + adrr1.ToString() + " initialized successfully!");
                sf.status = sf.status & 1;
            }
            else
            {
                sf.GetInfoMation("Serial " + adrr1.ToString() + " initialized failed!");
                sf.status = sf.status & 0;
            }
            flag = RFSignal.NewRFSignal(adrr2, rfClass, rFFormula);
           
            if (flag)
            {
                sf.GetInfoMation("Serial " + adrr2.ToString() + " initialized successfully!");
                sf.status = sf.status & 1;
            }
            else
            {
                sf.GetInfoMation("Serial " + adrr2.ToString() + " initialized failed!");
                sf.status = sf.status & 0;
            }
            RFSignal.RFOff(adrr1, 2);
            RFSignal.RFOff(adrr2, 2);
           
            #endregion
            NativeMessage.PostMessage(handel, MessageID.SF_WAIT, 0, 0);
            #region WINIO、GPIO初始化操作

            flag = GPIO.InitWinIo();

            if (!flag)
            {
                Thread.Sleep(300);
                flag = GPIO.InitWinIo();
            }

            if (flag)
            {
                sf.GetInfoMation("WINIO initialized successfully!");
                sf.status = sf.status & 1;
            }
            else
            {
               // Log.WriteLog("winIO初始化失败！", Log.EFunctionType.TestMode);
                sf.GetInfoMation("WINIO initialized failed!");
                sf.status = sf.status & 0;
            }
            sf.Complete = true;
            //GPIO.Narrowband();
            NativeMessage.PostMessage(handel, MessageID.SF_CONTINUTE, 0, 0); 
            #endregion

            #region 判断Bird频谱仪状态

            if (App_Configure.Cnfgs.Spectrum == 1)
            {
                FunTimeout tf = new FunTimeout();
                SpectrumLib.ISpectrum ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(handel, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                SpectrumLib.Models.ScanModel model = new SpectrumLib.Models.ScanModel();
                sf.ISpectrumObj = ISpectrumObj;
                if (ISpectrumObj.ConnectSpectrum() != 1)
                {
                    GPIO.SetHigh();
                    Thread.Sleep(1000);
                    GPIO.SetLow();
                    ISpectrumObj.ResetStatus();
                    sf.GetInfoMation("Bird connect failed!");
                    Log.WriteLog("Bird connect failed!", Log.EFunctionType.SPECTRUM);
                }
                model.StartFreq = 900;
                model.EndFreq = 900.5;
                model.Att = 0;
                model.Rbw = 10 * 1000;
                model.Vbw = 3 * 1000;
                model.Continued = false;
                model.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;

                tf.obj = model;
                tf.Do = ISpectrumObj.StartAnalysis;
                if (tf.DoWithTimeout(new TimeSpan(0, 0, 0, 1)))
                {
                    GPIO.SetHigh();
                    Thread.Sleep(1000);
                    GPIO.SetLow();
                    ISpectrumObj.ResetStatus();
                    Log.WriteLog("Bird get data failed!", Log.EFunctionType.SPECTRUM);
                }
            }

            #endregion

            #region 判断Deli频谱仪状态

            if (App_Configure.Cnfgs.Spectrum == 2)
            {
                try
                {
                    SpectrumLib.ISpectrum ISpectrumObj = new SpectrumLib.Spectrums.Deli(handel, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    SpectrumLib.Models.ScanModel model = new SpectrumLib.Models.ScanModel();
                    sf.ISpectrumObj = ISpectrumObj;
                    if (ISpectrumObj.ConnectSpectrum() != 1)
                    {
                        ISpectrumObj.ResetStatus();
                        sf.GetInfoMation("Deli spectrum connect failed !");
                        Log.WriteLog("Deli spectrum connect failed!", Log.EFunctionType.SPECTRUM);
                    }
                    else
                    {
                        object o;
                        model.StartFreq = App_Settings.pim.F1;
                        model.EndFreq = model.StartFreq + 2 * App_Settings.pim.Scanband;
                        model.Unit = SpectrumLib.Defines.CommonDef.EFreqUnit.MHz;
                        model.Att = App_Settings.pim.Att_Spc;
                        model.Rbw = App_Settings.pim.Rbw_Spc;
                        model.Vbw = App_Settings.pim.Vbw_Spc;
                        model.Deli_averagecount = 6;
                        model.Deli_detector = "AVERage";//检波方式
                        model.Deli_ref = -50;
                        model.Deli_refoffset = 0;
                        model.Deli_specing = "LOGarithmic";
                        model.Deli_sweepmode = "PERFormance";//扫描模式
                        model.Deli_source = "FREERUN";//触发方式
                        model.Deli_scale = 10;//单位/格
                        model.Deli_startspe = 0;//频谱仪是否第一次启动
                        model.Deli_isSpectrum = true;//频谱模式

                        o = model;
                        if (!ISpectrumObj.Setting(o))
                        {
                            Thread.Sleep(1000);
                            ISpectrumObj.ResetStatus();
                            sf.GetInfoMation("Deli spectrum set failed !");
                            Log.WriteLog("Deli spectrum set failed!", Log.EFunctionType.SPECTRUM);
                        }
                    }
                }
                catch
                {
                    sf.GetInfoMation("Deli spectrum connect failed !");
                    Log.WriteLog("Deli connect failed!", Log.EFunctionType.SPECTRUM);
                }
            }
            #endregion

            #region 判断FanShuang收信机状态

            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                FunTimeout tf = new FunTimeout();
                SpectrumLib.ISpectrum ISpectrumObj = new SpectrumLib.Spectrums.FanShuang(handel, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                SpectrumLib.Models.ScanModel model = new SpectrumLib.Models.ScanModel();
                sf.ISpectrumObj = ISpectrumObj;
                if (ISpectrumObj.ConnectSpectrum() != 1)
                {
                    GPIO.SetHigh();
                    Thread.Sleep(1000);
                    GPIO.SetLow();
                    ISpectrumObj.ResetStatus();
                    sf.GetInfoMation("FanShaung connect failed!");
                    Log.WriteLog("FanShaung connect failed!", Log.EFunctionType.SPECTRUM);
                }
            }

            #endregion   

            mre.WaitOne();

            #region 启动程序
            
            Application.Run(frmMain);
            mt.ReleaseMutex();
        }
        else
        {
            Application.Exit();
        }
            #endregion

            #region 释放资源
            RFSignal.FinaRFSignal();
            #endregion
        }

        static void Show()
        {
            handel = sf.Handle;
            sf.ShowDialog();
        }


        static string Copy(string sPath,string dPath )
        { 
        string flag = "success";           
            try           
            {                 
            // 创建目的文件夹           
                if (!Directory.Exists(dPath))
                {
                    Directory.CreateDirectory(dPath);
                }
                else
                {
                    return flag;
                }
                // 拷贝文件      
                DirectoryInfo sDir = new DirectoryInfo(sPath);     
                FileInfo[] fileArray = sDir.GetFiles();       
                foreach (FileInfo file in fileArray)     
                {               
                    file.CopyTo(dPath + "\\" + file.Name, true);    
                }                 
                // 循环子文件夹     
                DirectoryInfo dDir = new DirectoryInfo(dPath); 
                DirectoryInfo[] subDirArray = sDir.GetDirectories(); 
                foreach (DirectoryInfo subDir in subDirArray) 
                {   
                    Copy(subDir.FullName, dPath + "//" + subDir.Name);
                }   
            }             
            catch (Exception ex)      
            {                
                flag = ex.ToString();    
            }             
            return flag;      
        }
    }
}