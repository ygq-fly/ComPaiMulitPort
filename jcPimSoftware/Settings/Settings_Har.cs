using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    class Settings_Har
    {
        /// <summary>
        /// �����ļ����ƣ���ȡĳ���ļ�ʱ����ҪԤ������
        /// </summary>
        internal readonly string fileName;

        private float tx;
        private float freq;
        private int att_spc;
        private int rbw_spc;
        private int vbw_spc;
        private float min_har;
        private float max_har;
        private int time_points;
        private float freq_step; 
        private float limit;
        private float rev;
        private int multiplier;

        internal Settings_Har(string fileName)
        {
            this.fileName = fileName;
        }
       
        /// <summary>
        /// ����г������Ĭ���������
        /// </summary>
        internal float Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        /// <summary>
        /// ����г������Ĭ�Ϲ���Ƶ��
        /// </summary>
        internal float F
        {
            get { return freq; }
            set { freq = value; }
        }

        /// <summary>
        /// ����г������ʱ����Ƶ����Att����Ϊ��ֵ
        /// </summary>
        internal int Att_Spc
        {
            get { return att_spc; }
            set { att_spc = value; }
        }

        /// <summary>
        /// ����г������ʱ����Ƶ����rbw����Ϊ��ֵ
        /// </summary>
        internal int Rbw_Spc
        {
            get { return rbw_spc; }
            set { rbw_spc = value; }
        }
        
        /// <summary>
        /// ����г������ʱ����Ƶ����vbw����Ϊ��ֵ
        /// </summary>
        internal int Vbw_Spc
        {
            get { return vbw_spc; }
            set { vbw_spc = value; }
        }

        /// <summary>
        /// ����г������ʱ����ͼ�������������ʼֵ
        /// </summary>
        internal float Min_Har
        {
            get { return min_har; }
            set { min_har = value; }
        }

        /// <summary>
        /// ����г������ʱ����ͼ����������Ľ���ֵ
        /// </summary> 
        internal float Max_Har
        {
            get { return max_har; }
            set { max_har = value; }
        }

        /// <summary>
        /// ����г������ֵ����PASS/FAIL�Ĳο�ֵ
        /// </summary>
        internal float Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        /// <summary>
        /// ��Ƶ��ֵ
        /// </summary>
        internal int Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        /// <summary>
        /// ����г������ʱ��ɨʱ�ĵ���
        /// </summary>
        internal int Time_Points
        {
            get { return time_points; }
            set { time_points = value; }
        }

        /// <summary>
        /// ����г�����ԣ�ɨƵʱ�Ĳ���ֵ����λMHz
        /// </summary>       
        internal float Freq_Step
        {
            get { return freq_step; }
            set { freq_step = value; }
        }

        /// <summary>
        /// ����г��REVֵ
        /// </summary>
        internal float Rev
        {
            get { return rev; }
            set { rev = value; }
        }

        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        internal void Clone(Settings_Har dest)
        {
            if (dest != null)
            {
                dest.F  = this.freq;
                dest.Tx = this.tx;

                dest.Min_Har = this.min_har;
                dest.Max_Har = this.max_har;
                
                dest.Att_Spc = this.att_spc;
                dest.Rbw_Spc = this.rbw_spc;
                dest.Vbw_Spc = this.vbw_spc;

                dest.Time_Points = this.time_points;
                dest.Freq_Step = this.freq_step;

                dest.Limit = this.limit;
                dest.multiplier = this.multiplier;

                dest.Rev = this.rev;
            }
        }

        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);

            tx = float.Parse(IniFile.GetString("harmonic", "tx", "30.0"));
            freq = float.Parse(IniFile.GetString("harmonic", "freq", "930.0"));

            att_spc = int.Parse(IniFile.GetString("harmonic", "att_spc", "0"));
            rbw_spc = int.Parse(IniFile.GetString("harmonic", "rbw_spc", "4"));
            vbw_spc = int.Parse(IniFile.GetString("harmonic", "vbw_spc", "4"));

            min_har = float.Parse(IniFile.GetString("harmonic", "min_har", "0"));
            max_har = float.Parse(IniFile.GetString("harmonic", "max_har", "140"));

            time_points = int.Parse(IniFile.GetString("harmonic", "time_points", "20"));
            freq_step = float.Parse(IniFile.GetString("harmonic", "freq_step", "1.0"));

            limit = float.Parse(IniFile.GetString("harmonic", "limit", "80"));
            multiplier = int.Parse(IniFile.GetString("harmonic", "multiplier", "2"));

            rev = int.Parse(IniFile.GetString("harmonic", "rev", "0"));
        }


        internal void StoreSettings()
        {
            StoreSettings(fileName);
        }

        internal void StoreSettings(string fileName)
        {
            IniFile.SetFileName(fileName);

            IniFile.SetString("harmonic", "tx", tx.ToString("0.#"));
            IniFile.SetString("harmonic", "freq", freq.ToString("0.#"));

            IniFile.SetString("harmonic", "att_spc", att_spc.ToString());
            IniFile.SetString("harmonic", "rbw_spc", rbw_spc.ToString());
            IniFile.SetString("harmonic", "vbw_spc", vbw_spc.ToString());

            IniFile.SetString("harmonic", "min_har", min_har.ToString("0.#"));
            IniFile.SetString("harmonic", "max_har", max_har.ToString("0.#"));

            IniFile.SetString("harmonic", "time_points", time_points.ToString());
            IniFile.SetString("harmonic", "freq_step", freq_step.ToString("0.#"));

            IniFile.SetString("harmonic", "limit", limit.ToString("0.#"));

            IniFile.SetString("harmonic", "rev", rev.ToString("0.#"));
        }

        internal void Save2File(string defFileName, string dstFileName)
        {
            //������ʱ�ļ�������
            string tempFileName = "C:\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".ini";

            //��Ĭ���ļ����Ƶ���ʱ�ļ����Դ���������ͬ�ṹ�������ļ�
            File.Copy(defFileName, tempFileName, true);

            //��ͣ50ms,�Եȴ���ʱ�ļ�����������
            System.Threading.Thread.Sleep(50);

            //����ǰ�������л�����ʱ�ļ�
            StoreSettings(tempFileName);

            //����ʱ�ļ�������Ŀ���ļ�
            File.Copy(tempFileName, dstFileName, true);

            //ɾ����ʱ�ļ�
            File.Delete(tempFileName);
        }
        
    }

}
