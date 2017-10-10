using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    class App_Cnfgs
    {
        #region 全局设置项所在文件的路径
        /// <summary>
        /// 设置文件名称，读取某个文件时，需要预先设置
        /// </summary>
        internal readonly string fileName;
        #endregion

        #region 设置项
        //ygq===========================
        
        private int ms_switch_port_count;
        private int ishigh;
        private int mode;
        private int comaddr_switch;
        private int openOffset;
        private int qiandanqian_mode;
        private float rxRef;
        private float txRef;
        private bool isScroll;
        private bool switchOrGpio;
        //===============================
        private string macdesc;
        private int macid;
        private string sn;
        private string cal;

        private int comaddr1;
        private int comaddr2;
        private int rfclass;
        private int rfformula;
        private int spectrum;

        private static float max_vswr;
        private static float min_temp;
        private static float max_temp;
        private static float min_curr;
        private static float max_curr;

        private string path_def;
        private string path_txt;
        //private string file_def_pim;
        //private string file_def_spc;
        //private string file_def_iso;
        //private string file_def_vsw;
        //private string file_def_har;

        private string path_usr;        
        private string path_usr_pim;
        private string path_usr_spc;
        private string path_usr_iso;
        private string path_usr_vsw;
        private string path_usr_har;

        //private string file_usr_pim;
        //private string file_usr_spc;
        //private string file_usr_iso;
        //private string file_usr_vsw;
        //private string file_usr_har;

        private string path_rpt;
        private string path_rpt_pim;
        private string path_rpt_spc;
        private string path_rpt_iso;
        private string path_rpt_vsw;
        private string path_rpt_har;        

        private string lang_pack_path;
        private string skin_pack_path;

        //private int enable_pim;
        //private int enable_spc;
        //private int enable_iso;
        //private int enable_vsw;
        //private int enable_har;
        //private int enable_tst;

        private int cal_use_table;

        private string _desc;
        private string _modno;
        private string _serno;
        private string _opeor;

        private int csv_checked;

        public int Csv_checked
        {
            get { return csv_checked; }
            set { csv_checked = value; }
        }
        private int jpg_checked;

        public int Jpg_checked
        {
            get { return jpg_checked; }
            set { jpg_checked = value; }
        }
        private int pdf_checked;

        public int Pdf_checked
        {
            get { return pdf_checked; }
            set { pdf_checked = value; }
        }

        private int _channel;
        private int _gpio;
        private int _switchMode;
        private string _switchO;
        private string _switchN;
        private int _battery;
        private int _width_pinNum;
        private int _power_pinNum;
        private string _password;

        #endregion 

        #region 窄宽带、GPIO、电池服务设置

        internal int SwitchMode
        {
            get { return _switchMode; }
            set { _switchMode = value; }
        }

        internal string SwitchO
        {
            get { return _switchO; }
            set { _switchO = value; }
        }

        internal string SwitchN
        {
            get { return _switchN; }
            set { _switchN = value; }
        }

        /// <summary>
        /// 通道 0窄带 1宽带
        /// </summary>
        internal int Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        /// <summary>
        ///GPIO 0老主板GPIO 1新主板GPIO 
        /// </summary>
        internal int Gpio
        {
            get { return _gpio; }
            set { _gpio = value; }
        }
        /// <summary>
        /// 电池服务 0停止 1开启
        /// </summary>
        internal int Battery
        {
            get { return _battery; }
            set { _battery = value; }
        }
        /// <summary>
        /// 设置带宽的管脚
        /// </summary>
        public int Width_pinNum
        {
            get { return _width_pinNum; }
            set { _width_pinNum = value; }
        }
        /// <summary>
        /// 获取市电的管脚
        /// </summary>
        public int Power_pinNum
        {
            get { return _power_pinNum; }
            set { _power_pinNum = value; }
        }

        #endregion

        #region 高级设置(补偿、TEST模式)

        private int _enableSuperConfig;
        internal int EnableSuperConfig
        {
            get { return _enableSuperConfig; }
            set { _enableSuperConfig = value; }
        }

        internal bool IsScrool
        {
            get { return isScroll; }
            set { isScroll = value; }
        }

        internal bool SwtichOrGpio
        {
            get { return switchOrGpio; }
            set { SwtichOrGpio = value; }
        }
        #endregion

        #region 出厂设置
        /// <summary>
        /// 仪表类型指示字 0:CDMA, 1:GSM, 2:DCS, 3:PCS, 4:WCDMA
        /// </summary>       
        internal int MacID
        {
            get { return macid; }
            set { macid = value; }
        }

        /// <summary>
        /// 仪表描述信息
        /// </summary>       
        internal string Mac_Desc
        {
            get { return macdesc; }
            set { macdesc = value; }
        }

        /// <summary>
        /// 仪表仪表出厂序列号
        /// </summary>       
        internal string SN
        {
            get { return sn; }
            set { sn = value; }
        }

        /// <summary>
        /// 仪表仪表出厂校准日期
        /// </summary>       
        internal string CAL
        {
            get { return cal; }
            set { cal = value; }
        }

        /// <summary>
        /// 各个需要使用功放的功能，其收、发信补偿的方式
        /// </summary>
        public bool Cal_Use_Table
        {
            get { return (cal_use_table > 0); }
            set
            {
                if (value)
                    cal_use_table = 1;
                else
                    cal_use_table = 0;
            }
        }
        #endregion

        #region 设备信息
        /// <summary>
        /// 功放1的地址，1代表COM1，2代表COM2，依此类推
        /// </summary>        
        internal int ComAddr1
        {
            get { return comaddr1; }
            set { comaddr1 = value; }
        }

        /// <summary>
        /// 功放2的地址，1代表COM1，2代表COM2，依此类推
        /// </summary>
        internal int ComAddr2
        {
            get { return comaddr2; }
            set { comaddr2 = value; }
        }

        /// <summary>
        /// 功放协议类型指示字 0：三维协议 1：紫光协议（委托韩国单位实现）
        /// </summary>
        internal int RFClass
        {
            get { return rfclass; }
            set { rfclass = value; }
        }

        /// <summary>
        /// 功放输出功率的计算公式的不同形式 0：LOG公式 1：线性公式
        /// </summary>
        internal int RFFormula
        {
            get { return rfformula; }
            set { rfformula = value; }
        }


        /// <summary>
        /// 频谱仪器类型指示子，0代表SPECAT2，1代表BIRDSH，依此类推
        /// </summary>    
        internal int Spectrum
        {
            get { return spectrum; }
            set { spectrum = value; }
        }

        /// <summary>
        /// 功放允许的最大驻波比，即驻波比告警阀值
        /// </summary>
        internal float Max_Vswr
        {
            get { return max_vswr; }
            set { max_vswr = value; }
        }

        /// <summary>
        /// 功放允许的最低温度，即温度告警最小值, 单位摄氏度
        /// </summary>
        internal float Min_Temp
        {
            get { return min_temp; }
            set { min_temp = value; }
        }

        /// <summary>
        /// 功放允许的最高温度，即温度告警最大值，单位摄氏度
        /// </summary>
        internal float Max_Temp
        {
            get { return max_temp; }
            set { max_temp = value; }
        }

        /// <summary>
        /// 功放允许的最小工作电流，单位安培
        /// </summary>
        internal float Min_Curr
        {
            get { return min_curr; }
            set { min_curr = value; }
        }

        /// <summary>
        /// 功放允许的最大工作电流，单位安培
        /// </summary>
        internal float Max_Curr
        {
            get { return max_curr; }
            set { max_curr = value; }
        }
        #endregion

        #region 设置文件名称与路径
        /// <summary>
        /// 默认设置文件的路径
        /// </summary>
        internal string Path_Def
        {
            get { return path_def; }
            set { path_def = value; }
        }

        internal string Path_Txt
        {
            get { return path_txt; }
            set { path_txt = value; }
        }
        internal int Qiandanqiang_mode
        {
            get { return qiandanqian_mode; }
            set { qiandanqian_mode = value; }
        }

        /// <summary>
        /// 多端口频段
        /// </summary>
        internal int  Ms_switch_port_count
        {
            get { return ms_switch_port_count; }
            set { ms_switch_port_count = value; }
        }


        internal int Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        internal int Ishigh
        {
            get { return ishigh; }
            set { ishigh = value; }
        }
        /// <summary>
        /// 串口
        /// </summary>
        internal int Comaddr_switch
        {
            get { return comaddr_switch; }
            set { comaddr_switch = value; }
        }

        //rx补偿
        internal float RxRef
        {
            get { return rxRef; }
            set { rxRef = value; }
        }
        //tx补偿
        internal float TxRef
        {
            get { return txRef; }
            set { txRef = value; }
        }

        /// <summary>
        /// 显示功率作弊
        /// </summary>
        internal int OpenOffset
        {
            get { return openOffset; }
            set { openOffset = value; }
        }
        /// <summary>
        /// 互调用户默认设置文件名称
        /// </summary>
        //internal string File_Def_Pim
        //{
        //    get { return file_def_pim; }
        //    set { file_def_pim = value; }
        //}

        /// <summary>
        /// 频谱分析默认设置文件名称
        /// </summary>
        //internal string File_Def_Spc
        //{
        //    get { return file_def_spc; }
        //    set { file_def_spc = value; }
        //}

        /// <summary>
        /// 隔离度默认设置文件名称
        /// </summary>
        //internal string File_Def_Iso
        //{
        //    get { return file_def_iso; }
        //    set { file_def_iso = value; }
        //}

        /// <summary>
        /// 驻波比默认设置文件名称
        /// </summary>
        //internal string File_Def_Vsw
        //{
        //    get { return file_def_vsw; }
        //    set { file_def_vsw = value; }
        //}

        /// <summary>
        /// 二次谐波默认设置文件名称
        /// </summary>
        //internal string File_Def_Har
        //{
        //    get { return file_def_har; }
        //    set { file_def_har = value; }
        //}

        /// <summary>
        /// 用户设置文件的路径
        /// </summary>
        public string Path_Usr
        {
            get { return path_usr; }
            set { path_usr = value; }
        }

        /// <summary>
        /// 互调用户设置文件的路径
        /// </summary>
        internal string Path_Usr_Pim
        {
            get { return path_usr_pim; }
            set { path_usr_pim = value; }
        }

        /// <summary>
        /// 频谱分析用户设置文件的路径
        /// </summary>
        internal string Path_Usr_Spc
        {
            get { return path_usr_spc; }
            set { path_usr_spc = value; }
        }

        /// <summary>
        /// 隔离度用户设置文件的路径
        /// </summary>
        internal string Path_Usr_Iso
        {
            get { return path_usr_iso; }
            set { path_usr_iso = value; }
        }

        /// <summary>
        /// 驻波比用户设置文件的路径
        /// </summary>
        internal string Path_Usr_Vsw
        {
            get { return path_usr_vsw; }
            set { path_usr_vsw = value; }
        }

        /// <summary>
        /// 谐波用户设置文件的路径
        /// </summary>
        internal string Path_Usr_Har
        {
            get { return path_usr_har; }
            set { path_usr_har = value; }
        }

        /// <summary>
        /// 互调用户设置文件名称
        /// </summary>
        //internal string File_Usr_Pim
        //{
        //    get { return file_usr_pim; }
        //    set { file_usr_pim = value; }
        //}

        ///// <summary>
        ///// 频谱分析用户设置文件名称
        ///// </summary>
        //internal string File_Usr_Spc
        //{
        //    get { return file_usr_spc; }
        //    set { file_usr_spc = value; }
        //}

        ///// <summary>
        ///// 隔离度用户设置文件名称
        ///// </summary>
        //internal string File_Usr_Iso
        //{
        //    get { return file_usr_iso; }
        //    set { file_usr_iso = value; }
        //}

        ///// <summary>
        ///// 驻波比用户设置文件名称
        ///// </summary>
        //internal string File_Usr_Vsw
        //{
        //    get { return file_usr_vsw; }
        //    set { file_usr_vsw = value; }
        //}

        ///// <summary>
        ///// 谐波用户设置文件名称
        ///// </summary>
        //internal string File_Usr_Har
        //{
        //    get { return file_usr_har; }
        //    set { file_usr_har = value; }
        //}

        /// <summary>
        /// 报表文件的路径
        /// </summary>
        public string Path_Rpt
        {
            get { return path_rpt; }
            set { path_rpt = value; }
        }

        /// <summary>
        /// 互调报表文件的路径
        /// </summary>
        public string Path_Rpt_Pim
        {
            get { return path_rpt_pim; }
            set { path_rpt_pim = value; }
        }

        /// <summary>
        /// 频谱分析报表文件的路径
        /// </summary>
        public string Path_Rpt_Spc
        {
            get { return path_rpt_spc; }
            set { path_rpt_spc = value; }
        }

        /// <summary>
        /// 隔离度报表文件的路径
        /// </summary>
        public string Path_Rpt_Iso
        {
            get { return path_rpt_iso; }
            set { path_rpt_iso = value; }
        }

        /// <summary>
        ///驻波比报表文件的路径
        /// </summary>
        public string Path_Rpt_Vsw
        {
            get { return path_rpt_vsw; }
            set { path_rpt_vsw = value; }
        }

        /// <summary>
        ///谐波报表文件的路径
        /// </summary>
        public string Path_Rpt_Har
        {
            get { return path_rpt_har; }
            set { path_rpt_har = value; }
        }
        #endregion

        #region 语言与皮肤等资源的路径
        /// <summary>
        /// 语言包文件
        /// </summary>
        internal string Lang_Pack_Path
        {
            get { return lang_pack_path; }
            set { lang_pack_path = value; }
        }

        /// <summary>
        /// 皮肤图片资源文件
        /// </summary>
        internal string Skin_pack_path
        {
            get { return skin_pack_path; }
            set { skin_pack_path = value; }
        }
        #endregion

        #region 报表摘要信息
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        public string Modno
        {
            get { return _modno; }
            set { _modno = value; }
        }

        public string Serno
        {
            get { return _serno; }
            set { _serno = value; }
        }


        public string Opeor
        {
            get { return _opeor; }
            set { _opeor = value; }
        }
        #endregion

        #region Lock加密字符串

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion

        #region 从文件加载设置项和将设置项保存到文件
        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);

            //=======ygq

            ms_switch_port_count = int.Parse(IniFile.GetString("cnfgs", "ms_switch_port_count", "0"));
            comaddr_switch = int.Parse(IniFile.GetString("cnfgs", "comaddr_switch", "1"));
            rxRef = float.Parse(IniFile.GetString("cnfgs", "rxRef", "0"));
            txRef = float.Parse(IniFile.GetString("cnfgs", "txRef", "0"));
            openOffset = int.Parse(IniFile.GetString("cnfgs", "openOffset", "1"));
            qiandanqian_mode = int.Parse(IniFile.GetString("cnfgs", "qiandanqiang_mode", "0"));
            mode = int.Parse(IniFile.GetString("cnfgs", "mode", "0"));
            ishigh = int.Parse(IniFile.GetString("cnfgs", "ishigh", "0"));
            //======ygq
            macdesc = IniFile.GetString("cnfgs", "macdesc", "JCIMA-II-900P");

            macid = int.Parse(IniFile.GetString("cnfgs", "macid", "1"));

            sn = IniFile.GetString("cnfgs", "sn", "none sn");

            cal = IniFile.GetString("cnfgs", "cal", "none cal");

            comaddr1 = int.Parse(IniFile.GetString("cnfgs", "comaddr1", "1"));
            comaddr2 = int.Parse(IniFile.GetString("cnfgs", "comaddr2", "2"));
            rfclass = int.Parse(IniFile.GetString("cnfgs", "rfclass", "0"));
            rfformula = int.Parse(IniFile.GetString("cnfgs", "rfformula", "0"));

            spectrum = int.Parse(IniFile.GetString("cnfgs", "spectrum", "0"));

            max_vswr = float.Parse(IniFile.GetString("cnfgs", "max_vswr", "2.0"));
            min_temp = float.Parse(IniFile.GetString("cnfgs", "min_temp", "-20.0"));
            max_temp = float.Parse(IniFile.GetString("cnfgs", "max_temp", "60.0"));
            min_curr = float.Parse(IniFile.GetString("cnfgs", "min_curr", "0.1"));
            max_curr = float.Parse(IniFile.GetString("cnfgs", "max_curr", "3.0"));

            path_def = IniFile.GetString("cnfgs", "path_def", "settings");

            //file_def_pim = IniFile.GetString("cnfgs", "file_def_pim", "Settings_Pim.ini");
            //file_def_spc = IniFile.GetString("cnfgs", "file_def_spc", "Settings_Spc.ini");
            //file_def_iso = IniFile.GetString("cnfgs", "file_def_iso", "Settings_Iso.ini");
            //file_def_vsw = IniFile.GetString("cnfgs", "file_def_vsw", "Settings_Vsw.ini");
            //file_def_har = IniFile.GetString("cnfgs", "file_def_har", "Settings_Har.ini");

            path_usr = IniFile.GetString("cnfgs", "path_usr", "d:\\usr_settings");

            path_usr_pim = IniFile.GetString("cnfgs", "path_usr_pim", "d:\\usr_settings\\pim");
            path_usr_spc = IniFile.GetString("cnfgs", "path_usr_spc", "d:\\usr_settings\\spc");
            path_usr_iso = IniFile.GetString("cnfgs", "path_usr_iso", "d:\\usr_settings\\iso");
            path_usr_vsw = IniFile.GetString("cnfgs", "path_usr_vsw", "d:\\usr_settings\\vsw");
            path_usr_har = IniFile.GetString("cnfgs", "path_usr_har", "d:\\usr_settings\\har");

            //file_usr_pim = IniFile.GetString("cnfgs", "file_usr_pim", "Settings_Pim.ini");
            //file_usr_spc = IniFile.GetString("cnfgs", "file_usr_spc", "Settings_Spc.ini");
            //file_usr_iso = IniFile.GetString("cnfgs", "file_usr_iso", "Settings_Iso.ini");
            //file_usr_vsw = IniFile.GetString("cnfgs", "file_usr_vsw", "Settings_Vsw.ini");
            //file_usr_har = IniFile.GetString("cnfgs", "file_usr_har", "Settings_Har.ini");

            path_rpt = IniFile.GetString("cnfgs", "path_rpt", "d:\\report");

            path_rpt_pim = IniFile.GetString("cnfgs", "path_rpt_pim", "d:\\report\\pim");
            path_rpt_spc = IniFile.GetString("cnfgs", "path_rpt_spc", "d:\\report\\spc");
            path_rpt_iso = IniFile.GetString("cnfgs", "path_rpt_iso", "d:\\report\\iso");
            path_rpt_vsw = IniFile.GetString("cnfgs", "path_rpt_vsw", "d:\\report\\vsw");
            path_rpt_har = IniFile.GetString("cnfgs", "path_rpt_har", "d:\\report\\har");

            cal_use_table = int.Parse(IniFile.GetString("cnfgs", "cal_use_table", "0"));

            lang_pack_path = IniFile.GetString("cnfgs", "lang_pack_path", "");
            skin_pack_path = IniFile.GetString("cnfgs", "skin_pack_path", "");

            //enable_pim = int.Parse(IniFile.GetString("cnfgs", "enable_pim", "1"));
            //enable_spc = int.Parse(IniFile.GetString("cnfgs", "enable_spc", "1"));
            //enable_iso = int.Parse(IniFile.GetString("cnfgs", "enable_iso", "0"));
            //enable_vsw = int.Parse(IniFile.GetString("cnfgs", "enable_vsw", "0"));
            //enable_har = int.Parse(IniFile.GetString("cnfgs", "enable_har", "0"));
            //enable_tst = int.Parse(IniFile.GetString("cnfgs", "enable_tst", "0"));

            _desc = IniFile.GetString("cnfgs", "desc", "here is no [description]");
            _modno = IniFile.GetString("cnfgs", "modno", "here is no [model no.]");
            _opeor = IniFile.GetString("cnfgs", "opeor", "here is no [serial no.]");
            _serno = IniFile.GetString("cnfgs", "serno", "default user");

            csv_checked = int.Parse(IniFile.GetString("cnfgs", "csv_checked", "0"));
            jpg_checked = int.Parse(IniFile.GetString("cnfgs", "jpg_checked", "0"));
            pdf_checked = int.Parse(IniFile.GetString("cnfgs", "pdf_checked", "0"));

            _channel = int.Parse(IniFile.GetString("cnfgs", "channel", "0"));
            _gpio = int.Parse(IniFile.GetString("cnfgs", "gpio", "0"));
            _battery = int.Parse(IniFile.GetString("cnfgs", "battery", "0"));
            _width_pinNum = int.Parse(IniFile.GetString("cnfgs", "width_pinNum", "0"));
            _power_pinNum = int.Parse(IniFile.GetString("cnfgs", "power_pinNum", "1"));

            _enableSuperConfig = int.Parse(IniFile.GetString("cnfgs", "enableSuperConfig", "0"));
            isScroll = IniFile.GetString("cnfgs", "scroll", "1") == "0" ? true : false;
            switchOrGpio = IniFile.GetString("cnfgs", "SwitchOrGpio", "0") == "0" ? true : false;

            //_pim_wait = int.Parse(IniFile.GetString("cnfgs", "pimWait", "1000"));
            _password = IniFile.GetString("cnfgs", "password", "82F8A4F03E18723F");
            _switchMode = int.Parse(IniFile.GetString("cnfgs", "switchMode", "0"));
            _switchO = IniFile.GetString("cnfgs", "switchO", "0");
            _switchN = IniFile.GetString("cnfgs", "switchN", "0");
            if (_password == "")
            {
                _password = "82F8A4F03E18723F";
            }
        }

        internal void StoreSettings()
        {
            IniFile.SetFileName(fileName);
            
            IniFile.SetString("cnfgs", "max_vswr", max_vswr.ToString("0.#"));
            IniFile.SetString("cnfgs", "min_temp", min_temp.ToString("0.#"));
            IniFile.SetString("cnfgs", "max_temp", max_temp.ToString("0.#"));

            IniFile.SetString("cnfgs", "gpio", min_temp.ToString());
            IniFile.SetString("cnfgs", "battery", max_temp.ToString());

            //IniFile.SetString("cnfgs", "file_usr_pim", file_usr_pim);
            //IniFile.SetString("cnfgs", "file_usr_spc", file_usr_spc);
            //IniFile.SetString("cnfgs", "file_usr_iso", file_usr_iso);
            //IniFile.SetString("cnfgs", "file_usr_vsw", file_usr_vsw);
            //IniFile.SetString("cnfgs", "file_usr_har", file_usr_har);
            IniFile.SetString("cnfgs", "password", _password);
        }
        #endregion

        #region 构造函数，输入设置文件的路径
        internal App_Cnfgs(string fileName)
        {
            this.fileName = fileName;
        }
        #endregion

    }

    internal class App_Configure
    {
        public static App_Cnfgs Cnfgs;

        private App_Configure()
        {
            //
        }

        internal static void NewConfigure(string cnfgsFileName)
        {
            Cnfgs = new App_Cnfgs(cnfgsFileName);
        }

        /// <summary>
        /// 建立报表文件夹结构
        /// </summary>
        internal static void CreateReportFolder()
        {
            if (Cnfgs == null)
                return;

            //建立report文件夹
            if (!Directory.Exists(Cnfgs.Path_Rpt))
                Directory.CreateDirectory(Cnfgs.Path_Rpt);

            //建立report/pim，及其子文件夹csv，jpg，csv
            if (!Directory.Exists(Cnfgs.Path_Rpt_Pim))
                Directory.CreateDirectory(Cnfgs.Path_Rpt_Pim);

            CreateReportSubFolder(Cnfgs.Path_Rpt_Pim); 
            
            //建立report/spc，及其子文件夹csv，jpg，csv
            if (!Directory.Exists(Cnfgs.Path_Rpt_Spc))
                Directory.CreateDirectory(Cnfgs.Path_Rpt_Spc);

            CreateReportSubFolder(Cnfgs.Path_Rpt_Spc); 

            //建立report/iso，及其子文件夹csv，jpg，csv
            if (!Directory.Exists(Cnfgs.Path_Rpt_Iso))
                Directory.CreateDirectory(Cnfgs.Path_Rpt_Iso);

            CreateReportSubFolder(Cnfgs.Path_Rpt_Iso); 

            //建立report/vsw，及其子文件夹csv，jpg，csv
            if (!Directory.Exists(Cnfgs.Path_Rpt_Vsw))
                Directory.CreateDirectory(Cnfgs.Path_Rpt_Vsw);

            CreateReportSubFolder(Cnfgs.Path_Rpt_Vsw); 

            //建立report/har，及其子文件夹csv，jpg，csv
            if (!Directory.Exists(Cnfgs.Path_Rpt_Har))
                Directory.CreateDirectory(Cnfgs.Path_Rpt_Har);

            CreateReportSubFolder(Cnfgs.Path_Rpt_Har); 
        }

        public static void CreateReportSubFolder(string parent)
        {
            if (!Directory.Exists(parent + "\\csv"))
                Directory.CreateDirectory(parent + "\\csv");

            if (!Directory.Exists(parent + "\\pdf"))
                Directory.CreateDirectory(parent + "\\pdf");

            if (!Directory.Exists(parent + "\\jpg"))
                Directory.CreateDirectory(parent + "\\jpg");
        }

        /// <summary>
        /// 建立用户设置文件夹结构
        /// </summary>
        internal static void CreateUsrSettingFolder()
        {
            if (Cnfgs == null)
                return;

            if (!Directory.Exists(Cnfgs.Path_Usr))
                Directory.CreateDirectory(Cnfgs.Path_Usr);

            if (!Directory.Exists(Cnfgs.Path_Usr_Pim))
                Directory.CreateDirectory(Cnfgs.Path_Usr_Pim);

            if (!Directory.Exists(Cnfgs.Path_Usr_Spc))
                Directory.CreateDirectory(Cnfgs.Path_Usr_Spc);

            if (!Directory.Exists(Cnfgs.Path_Usr_Iso))
                Directory.CreateDirectory(Cnfgs.Path_Usr_Iso);

            if (!Directory.Exists(Cnfgs.Path_Usr_Vsw))
                Directory.CreateDirectory(Cnfgs.Path_Usr_Vsw);

            if (!Directory.Exists(Cnfgs.Path_Usr_Har))
                Directory.CreateDirectory(Cnfgs.Path_Usr_Har);
        }

       
    }
}