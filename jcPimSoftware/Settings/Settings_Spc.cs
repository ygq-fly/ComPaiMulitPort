using System;
using System.Collections.Generic;
using System.Text;


namespace jcPimSoftware
{
    class Settings_Spc
    {
        /// <summary>
        /// �����ļ����ƣ���ȡĳ���ļ�ʱ����ҪԤ������
        /// </summary>
        internal readonly string fileName;

        internal Settings_Spc(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// Ƶ�׷���������ͨ����խ��/���
        /// </summary>
        private int channel;
        internal RxChannel Channel
        {
            get { return (RxChannel)Enum.Parse(typeof(RxChannel), Enum.GetName(typeof(RxChannel), Channel)); }
            set { channel = (int)value; }
        }

        /// <summary>
        /// Ƶ�׷���,�Ƿ����ò����ļ�
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
        /// Ƶ�ײ����ļ�·��
        /// </summary>
        private string offsetFilePath;
        internal string OffsetFilePath
        {
            get { return offsetFilePath; }
            set { offsetFilePath = value; }
        }


        /// <summary>
        /// Ƶ���ǵ��ڲ�˥��ֵ
        /// </summary>
        private int att;
        internal int Att
        {
            get { return att; }
            set { att = value; }
        }

        /// <summary>
        /// Ƶ���ǵķֱ��ʴ���
        /// </summary>
        private int rbw;
        internal int Rbw
        {
            get { return rbw; }
            set { rbw = value; }
        }

        /// <summary>
        /// Ƶ���ǵĿ��ӷֱ��ʴ���
        /// </summary>
        private int vbw;
        internal int Vbw
        {
            get { return vbw; }
            set { vbw = value; }
        }

        /// <summary>
        /// Ƶ�շ�������ʼƵ��
        /// </summary>
        private float start;
        internal float Start
        {
            get { return start; }
            set { start = value; }
        }

        /// <summary>
        /// Ƶ�շ����Ľ���Ƶ��
        /// </summary>
        private float stop;
        internal float Stop
        {
            get { return stop; }
            set { stop = value; }
        }

        /// <summary>
        /// Ƶ�շ����������С��ʼƵ��
        /// </summary>
        private float min_freq;
        internal float Min_Freq
        {
            get { return min_freq; }
            set { min_freq = value; }
        }

        /// <summary>
        /// Ƶ�շ��������������Ƶ��
        /// </summary>
        private float max_freq;
        internal float Max_Freq
        {
            get { return max_freq; }
            set { max_freq = value; }
        }

        /// <summary>
        /// Ƶ�շ�������ͼ�������������Сֵ���źŷ��ȣ�
        /// </summary>
        private float min_aph;
        internal float Min_Aph
        {
            get { return min_aph; }
            set { min_aph = value; }
        }

        /// <summary>
        /// Ƶ�շ�������ͼ��������������ֵ���źŷ��ȣ�
        /// </summary>
        private float max_aph;
        internal float Max_Aph
        {
            get { return max_aph; }
            set { max_aph = value; }
        }

        /// <summary>
        /// Ƶ�ײ�������(����CPU��ͼѹ��)
        /// </summary>
        private int sampleSpan;
        internal int SampleSpan
        {
            get { return sampleSpan; }
            set { sampleSpan = value; }
        }


        /// <summary>
        /// ����Ƶ���źŵ�˥��
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
        
        //tx����
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
        /// �Ƿ�������Ƶ��ģ���п����� 0���� 1����
        /// </summary>
        private float enableRF;
        internal float EnableRF
        {
            get { return enableRF; }
            set { enableRF = value; }
        }

        /// <summary>
        /// ������ʱ��
        /// </summary>
        private float timeRF;
        internal float TimeRF
        {
            get { return timeRF; }
            set { timeRF = value; }
        }

        /// <summary>
        /// ȡN��ɨ������ƽ��
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
