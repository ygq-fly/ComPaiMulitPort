using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    class Settings_Har
    {
        /// <summary>
        /// 配置文件名称，读取某个文件时，需要预先设置
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
        /// 二次谐波测试默认输出功率
        /// </summary>
        internal float Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        /// <summary>
        /// 二次谐波测试默认工作频率
        /// </summary>
        internal float F
        {
            get { return freq; }
            set { freq = value; }
        }

        /// <summary>
        /// 二次谐波测试时，将频谱仪Att设置为该值
        /// </summary>
        internal int Att_Spc
        {
            get { return att_spc; }
            set { att_spc = value; }
        }

        /// <summary>
        /// 二次谐波测试时，将频谱仪rbw设置为该值
        /// </summary>
        internal int Rbw_Spc
        {
            get { return rbw_spc; }
            set { rbw_spc = value; }
        }
        
        /// <summary>
        /// 二次谐波测试时，将频谱仪vbw设置为该值
        /// </summary>
        internal int Vbw_Spc
        {
            get { return vbw_spc; }
            set { vbw_spc = value; }
        }

        /// <summary>
        /// 二次谐波测试时，绘图网格纵坐标的起始值
        /// </summary>
        internal float Min_Har
        {
            get { return min_har; }
            set { min_har = value; }
        }

        /// <summary>
        /// 二次谐波测试时，绘图网格纵坐标的结束值
        /// </summary> 
        internal float Max_Har
        {
            get { return max_har; }
            set { max_har = value; }
        }

        /// <summary>
        /// 二次谐波评估值，即PASS/FAIL的参考值
        /// </summary>
        internal float Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        /// <summary>
        /// 倍频数值
        /// </summary>
        internal int Multiplier
        {
            get { return multiplier; }
            set { multiplier = value; }
        }

        /// <summary>
        /// 二次谐波测试时，扫时的点数
        /// </summary>
        internal int Time_Points
        {
            get { return time_points; }
            set { time_points = value; }
        }

        /// <summary>
        /// 二次谐波测试，扫频时的步进值，单位MHz
        /// </summary>       
        internal float Freq_Step
        {
            get { return freq_step; }
            set { freq_step = value; }
        }

        /// <summary>
        /// 二次谐波REV值
        /// </summary>
        internal float Rev
        {
            get { return rev; }
            set { rev = value; }
        }

        /// <summary>
        /// 将对象深度拷贝到目标dest
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
            //生成临时文件的名称
            string tempFileName = "C:\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".ini";

            //从默认文件复制到临时文件，以创建具有相同结构的设置文件
            File.Copy(defFileName, tempFileName, true);

            //暂停50ms,以等待临时文件被创建出来
            System.Threading.Thread.Sleep(50);

            //将当前对象序列化到临时文件
            StoreSettings(tempFileName);

            //将临时文件拷贝到目标文件
            File.Copy(tempFileName, dstFileName, true);

            //删除临时文件
            File.Delete(tempFileName);
        }
        
    }

}
