using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class Settings_Sgn
    {
        /// <summary>
        /// 配置文件名称，读取某个文件时，需要预先设置
        /// </summary>
        private readonly string fileName;
        private readonly string signalName;

        internal Settings_Sgn(string fileName, string signalName)
        {
            this.fileName = fileName;

            this.signalName = signalName;
        }

        /// <summary>
        /// 功放的通信端口，例如COM1,COM2
        /// </summary>
        private string port;
        internal string Port
        {
            get { return port; }
            set { port = value; }
        }

        /// <summary>
        /// 功放驻波比告警阀值
        /// </summary>
        private float limit_vswr;
        internal float Limit_Vswr
        {
            get { return limit_vswr; }
            set { limit_vswr = value; }
        }

        /// <summary>
        /// 功放计算输出功率的模式，线性，非线性（Log）
        /// </summary>
        private int mode_power;
        internal int Mode_Power
        {
            get { return mode_power; }
            set { mode_power = value; }
        }

        /// <summary>
        /// 在启用功放保护功能时，输出的试探功率值
        /// </summary>
        private float tx_pre;
        internal float Tx_Pre
        {
            get { return tx_pre; }
            set { tx_pre = value; }
        
        }

        /// <summary>
        /// 功放默认输出功率
        /// </summary>
        private float tx;
        internal float Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        /// <summary>
        /// 启用功放包含，即驻波比定时检测
        /// </summary>
        private int enableVswr;
        internal bool EnableVswr
        {
            get { return (enableVswr > 0); }
            set { 
                if (value) 
                    enableVswr = 1; 
               else 
                   enableVswr = 0;
            }
        }

        /// <summary>
        /// 驻波比定时检测的周期，单位 ms 
        /// </summary>
        private int time_vswr;
        internal int Time_Vswr
        {
            get { return time_vswr; }
            set { time_vswr = value; }
        }

        /// <summary>
        /// 功放支持的最小输出功率
        /// </summary>
        private float min_power;
        internal float Min_Power
        {
            get { return min_power; }
            set { min_power = value; }
        }

        /// <summary>
        /// 功放支持的最大输出功率
        /// </summary>
        private float max_power;
        internal float Max_Power
        {
            get { return max_power; }
            set { max_power = value; }
        }

        /// <summary>
        /// 功放支持的最小工作频率
        /// </summary>
        private float min_freq;
        internal float Min_Freq
        {
            get { return min_freq; }
            set { min_freq = value; }
        }

        /// <summary>
        /// 功放支持的最小工作频率
        /// </summary>
        private float max_freq;
        internal float Max_Freq
        {
            get { return max_freq; }
            set { max_freq = value; }
        }

        /// <summary>
        /// 功放要求的最低环境温度
        /// </summary>
        private float min_temp;
        internal float Min_Temp
        {
            get { return min_temp; }
            set { min_temp = value; }
        }

        /// <summary>
        /// 功放要求的最高环境温度
        /// </summary>
        private float max_temp;
        internal float Max_Temp
        {
            get { return max_temp; }
            set { max_temp = value; }
        }

        /// <summary>
        /// 功放允许的最小工作电流
        /// </summary>
        private float min_curr;
        internal float Min_Curr
        {
            get { return min_curr; }
            set { min_curr = value; }
        }

        /// <summary>
        /// 功放能够承受的最大工作电流
        /// </summary>
        private float max_curr;
        internal float Max_Curr
        {
            get { return max_curr; }
            set { max_curr = value; }
        }

        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);

            port =IniFile.GetString(signalName, "port", "COM1");
            limit_vswr = float.Parse(IniFile.GetString(signalName, "limit_vswr", "2.0"));

            mode_power = int.Parse(IniFile.GetString(signalName, "mode_power", "0"));
            tx_pre = float.Parse(IniFile.GetString(signalName, "tx_pre", "30"));
            tx = float.Parse(IniFile.GetString(signalName, "tx", "43"));

            enableVswr = int.Parse(IniFile.GetString(signalName, "enableVswr", "1"));
            time_vswr = int.Parse(IniFile.GetString(signalName, "time_vswr", "500"));

            min_power = float.Parse(IniFile.GetString(signalName, "min_power", "30"));
            max_power = float.Parse(IniFile.GetString(signalName, "max_power", "45"));

            min_freq = float.Parse(IniFile.GetString(signalName, "min_freq", "930"));
            max_freq = float.Parse(IniFile.GetString(signalName, "max_freq", "940"));

            min_temp = float.Parse(IniFile.GetString(signalName, "min_temp", "-10"));
            max_temp = float.Parse(IniFile.GetString(signalName, "max_temp", "65"));

            min_curr = float.Parse(IniFile.GetString(signalName, "min_curr", "0.5"));
            max_curr = float.Parse(IniFile.GetString(signalName, "max_curr", "2.5"));
        }

        internal void StoreSettings()
        {
            IniFile.SetFileName(fileName);

            IniFile.SetString(signalName, "port", port);
            IniFile.SetString(signalName, "limit_vswr", limit_vswr.ToString("0.#"));

            IniFile.SetString(signalName, "mode_power", mode_power.ToString());
            IniFile.SetString(signalName, "tx_pre", tx_pre.ToString("0.#"));
            IniFile.SetString(signalName, "tx", tx.ToString("0.#"));

            IniFile.SetString(signalName, "enableVswr", enableVswr.ToString());
            IniFile.SetString(signalName, "time_vswr", time_vswr.ToString());

            IniFile.SetString(signalName, "min_power", min_power.ToString("0.#"));
            IniFile.SetString(signalName, "max_power", max_power.ToString("0.#"));
            IniFile.SetString(signalName, "min_freq", min_freq.ToString("0.#"));
            IniFile.SetString(signalName, "max_freq", max_freq.ToString("0.#"));
            IniFile.SetString(signalName, "min_temp", min_temp.ToString("0.#"));
            IniFile.SetString(signalName, "max_temp", max_temp.ToString("0.#"));
            IniFile.SetString(signalName, "min_curr", min_curr.ToString("0.#"));
            IniFile.SetString(signalName, "max_curr", max_curr.ToString("0.#"));
        }

    }
}
