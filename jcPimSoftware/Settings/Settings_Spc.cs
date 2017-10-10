using System;
using System.Collections.Generic;
using System.Text;


namespace jcPimSoftware
{
    class Settings_Spc
    {
        /// <summary>
        /// 配置文件名称，读取某个文件时，需要预先设置
        /// </summary>
        internal readonly string fileName;

        internal Settings_Spc(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// 频谱分析工作的通道，窄带/宽带
        /// </summary>
        private int channel;
        internal RxChannel Channel
        {
            get { return (RxChannel)Enum.Parse(typeof(RxChannel), Enum.GetName(typeof(RxChannel), Channel)); }
            set { channel = (int)value; }
        }

        /// <summary>
        /// 频谱分析,是否启用补偿文件
        /// </summary>
        private int enableOffset;
        internal bool EnableOffset
        {
            get { return (enableOffset > 0); }
            set
            {
                if (value)
                    enableOffset = 1;
                else
                    enableOffset = 0;
            }
        }

        /// <summary>
        /// 频谱补偿文件路径
        /// </summary>
        private string offsetFilePath;
        internal string OffsetFilePath
        {
            get { return offsetFilePath; }
            set { offsetFilePath = value; }
        }


        /// <summary>
        /// 频普仪的内部衰减值
        /// </summary>
        private int att;
        internal int Att
        {
            get { return att; }
            set { att = value; }
        }

        /// <summary>
        /// 频普仪的分辨率带宽
        /// </summary>
        private int rbw;
        internal int Rbw
        {
            get { return rbw; }
            set { rbw = value; }
        }

        /// <summary>
        /// 频普仪的可视分辨率带宽
        /// </summary>
        private int vbw;
        internal int Vbw
        {
            get { return vbw; }
            set { vbw = value; }
        }

        /// <summary>
        /// 频普分析的起始频率
        /// </summary>
        private float start;
        internal float Start
        {
            get { return start; }
            set { start = value; }
        }

        /// <summary>
        /// 频普分析的结束频率
        /// </summary>
        private float stop;
        internal float Stop
        {
            get { return stop; }
            set { stop = value; }
        }

        /// <summary>
        /// 频普分析允许的最小开始频率
        /// </summary>
        private float min_freq;
        internal float Min_Freq
        {
            get { return min_freq; }
            set { min_freq = value; }
        }

        /// <summary>
        /// 频普分析允许的最大结束频率
        /// </summary>
        private float max_freq;
        internal float Max_Freq
        {
            get { return max_freq; }
            set { max_freq = value; }
        }

        /// <summary>
        /// 频普分析，绘图网格纵坐标的最小值（信号幅度）
        /// </summary>
        private float min_aph;
        internal float Min_Aph
        {
            get { return min_aph; }
            set { min_aph = value; }
        }

        /// <summary>
        /// 频普分析，绘图网格纵坐标的最大值（信号幅度）
        /// </summary>
        private float max_aph;
        internal float Max_Aph
        {
            get { return max_aph; }
            set { max_aph = value; }
        }

        /// <summary>
        /// 频谱采样周期(减轻CPU绘图压力)
        /// </summary>
        private int sampleSpan;
        internal int SampleSpan
        {
            get { return sampleSpan; }
            set { sampleSpan = value; }
        }


        /// <summary>
        /// 补偿频谱信号的衰减
        /// </summary>
        private float rev;
        internal float Rev
        {
            get { return rev; }
            set { rev = value; }
        }


        private float rxRef;
        public string[] List_rxRef;
        internal float RxRef
        {
            get { return rxRef; }
            set { rxRef = value; }
        }

        private float txRef;
        private float outTxRef;
        public string[] List_txRef;
        
        //tx补偿
        internal float TxRef
        {
            get { return txRef; }
            set { txRef = value; }
        }
        internal float OutTxRef
        {
            get { return outTxRef; }
            set { outTxRef = value; }
        }

        /// <summary>
        /// 是否允许在频谱模块中开功放 0不许 1允许
        /// </summary>
        private float enableRF;
        internal float EnableRF
        {
            get { return enableRF; }
            set { enableRF = value; }
        }

        /// <summary>
        /// 开功放时间
        /// </summary>
        private float timeRF;
        internal float TimeRF
        {
            get { return timeRF; }
            set { timeRF = value; }
        }

        /// <summary>
        /// 取N次扫描数据平均
        /// </summary>
        private int averageCount;
        internal int AverageCount
        {
            get { return averageCount; }
            set { averageCount = value; }
        }

        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);

            channel = int.Parse(IniFile.GetString("spectrum", "channel", "0"));
            enableOffset = int.Parse(IniFile.GetString("spectrum", "enableOffset", "1"));
            offsetFilePath = IniFile.GetString("spectrum", "offsetFilePath", "");

            att = int.Parse(IniFile.GetString("spectrum", "att", "0"));
            rbw = int.Parse(IniFile.GetString("spectrum", "rbw", "4"));
            vbw = int.Parse(IniFile.GetString("spectrum", "vbw", "4"));

            start = float.Parse(IniFile.GetString("spectrum", "start", "800"));
            stop = float.Parse(IniFile.GetString("spectrum", "stop", "900"));

            min_freq = float.Parse(IniFile.GetString("spectrum", "min_freq", "0.1"));
            max_freq = float.Parse(IniFile.GetString("spectrum", "max_freq", "3000"));
            
            min_aph = float.Parse(IniFile.GetString("spectrum", "min_aph", "-30"));
            max_aph = float.Parse(IniFile.GetString("spectrum", "max_aph", "-130"));

            rev = float.Parse(IniFile.GetString("spectrum", "rev", "0"));
            enableRF = int.Parse(IniFile.GetString("spectrum", "enableRF", "0"));
            timeRF = int.Parse(IniFile.GetString("spectrum", "timeRF", "200"));
            sampleSpan = int.Parse(IniFile.GetString("spectrum", "sampleSpan", "200"));
            averageCount = int.Parse(IniFile.GetString("spectrum", "averagecount", "5"));
            rxRef = float.Parse(IniFile.GetString("spectrum", "rxRef", "0"));
            txRef = float.Parse(IniFile.GetString("spectrum", "txRef", "0"));
            List_txRef = IniFile.GetString("spectrum", "txRefTable", "0,0,0,0,0,0,0,0").Split(',');
            List_rxRef = IniFile.GetString("spectrum", "rxRefTable", "0,0,0,0,0,0,0,0").Split(',');
        }

        internal void StoreSettings()
        {
            IniFile.SetFileName(fileName);

            IniFile.SetString("spectrum", "channel", channel.ToString());
            IniFile.SetString("spectrum", "enableOffset", enableOffset.ToString());
            IniFile.SetString("spectrum", "offsetFilePath", offsetFilePath.ToString());

            IniFile.SetString("spectrum", "att", att.ToString());
            IniFile.SetString("spectrum", "rbw", rbw.ToString());
            IniFile.SetString("spectrum", "vbw", vbw.ToString());

            IniFile.SetString("spectrum", "start", start.ToString());
            IniFile.SetString("spectrum", "stop", stop.ToString());

            IniFile.SetString("spectrum", "min_freq", min_freq.ToString("0.#"));
            IniFile.SetString("spectrum", "max_freq", max_freq.ToString("0.#"));

            IniFile.SetString("spectrum", "min_aph", min_aph.ToString("0.#"));
            IniFile.SetString("spectrum", "max_aph", max_aph.ToString("0.#"));

            IniFile.SetString("spectrum", "rev", rev.ToString("0.#"));
            IniFile.SetString("spectrum", "enableRF", enableRF.ToString());
            IniFile.SetString("spectrum", "timeRF", timeRF.ToString());
            IniFile.SetString("spectrum", "sampleSpan", sampleSpan.ToString());
            IniFile.SetString("spectrum", "averagecount", averageCount.ToString());
        }

    }
}
