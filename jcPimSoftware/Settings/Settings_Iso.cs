using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    class Settings_Iso
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
        private int time_points;
        private float freq_step;        
        private float min_iso;
        private float max_iso;
        private float limit;
        private float attenuator;
        private float offset;

        internal Settings_Iso(string fileName)
        {
            this.fileName = fileName;
        }
       
        /// <summary>
        /// 隔离度测试默认输出功率
        /// </summary>
        internal float Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        /// <summary>
        /// 隔离度测试默认工作频率
        /// </summary>
        internal float F
        {
            get { return freq; }
            set { freq = value; }
        }

        /// <summary>
        /// 隔离度测试时，将频谱仪Att设置为该值
        /// </summary>
        internal int Att_Spc
        {
            get { return att_spc; }
            set { att_spc = value; }
        }

        /// <summary>
        /// 隔离度测试时，将频谱仪rbw设置为该值
        /// </summary>
        internal int Rbw_Spc
        {
            get { return rbw_spc; }
            set { rbw_spc = value; }
        }
        
        /// <summary>
        /// 隔离度测试时，将频谱仪vbw设置为该值
        /// </summary>
        internal int Vbw_Spc
        {
            get { return vbw_spc; }
            set { vbw_spc = value; }
        }

        /// <summary>
        /// 隔离度测试时，扫时的点数
        /// </summary>
        internal int Time_Points
        {
            get { return time_points; }
            set { time_points = value; }
        }

        /// <summary>
        /// 隔离度测试，扫频时的步进值，单位MHz
        /// </summary>       
        internal float Freq_Step
        {
            get { return freq_step; }
            set { freq_step = value; }
        }
        

        /// <summary>
        /// 隔离度测试时，绘图网格纵坐标的起始值
        /// </summary>
        internal float Min_Iso
        {
            get { return min_iso; }
            set { min_iso = value; }
        }

        /// <summary>
        /// 隔离度测试时，绘图网格纵坐标的结束值
        /// </summary> 
        internal float Max_Iso
        {
            get { return max_iso; }
            set { max_iso = value; }
        }

        /// <summary>
        /// 隔离度评估值，即PASS/FAIL的参考值
        /// </summary>
        internal float Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        /// <summary>
        /// 外接衰减器标称值
        /// </summary>
        public float Attenuator
        {
            get { return attenuator; }
            set { attenuator = value; }
        }

        /// <summary>
        /// 隔离度OFFSET值
        /// </summary>
        internal float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        /// <summary>
        /// 将对象深度拷贝到目标dest
        /// </summary>
        /// <param name="dest"></param>
        internal void Clone(Settings_Iso dest)
        {
            if (dest != null)
            {
                dest.F  = this.freq;
                dest.Tx = this.tx;

                dest.Min_Iso = this.min_iso;
                dest.Max_Iso = this.max_iso;

                dest.Limit = this.limit;

                dest.Att_Spc = this.att_spc;
                dest.Rbw_Spc = this.rbw_spc;
                dest.Vbw_Spc = this.vbw_spc;

                dest.Time_Points = this.time_points;
                dest.Freq_Step = this.freq_step;

                dest.Offset = this.offset;
            }
        }

        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);

            tx = float.Parse(IniFile.GetString("isolation", "tx", "30.0"));
            freq = float.Parse(IniFile.GetString("isolation", "freq", "930.0"));

            att_spc = int.Parse(IniFile.GetString("isolation", "att_spc", "0"));
            rbw_spc = int.Parse(IniFile.GetString("isolation", "rbw_spc", "4"));
            vbw_spc = int.Parse(IniFile.GetString("isolation", "vbw_spc", "4"));
           
            time_points = int.Parse(IniFile.GetString("isolation", "time_points", "20"));
            freq_step = float.Parse(IniFile.GetString("isolation", "freq_step", "1.0"));
            
            min_iso = float.Parse(IniFile.GetString("isolation", "min_iso", "0"));
            max_iso = float.Parse(IniFile.GetString("isolation", "max_iso", "140"));

            limit = float.Parse(IniFile.GetString("isolation", "limit", "80"));

            attenuator = float.Parse(IniFile.GetString("isolation", "attenuator", "0"));
            offset = float.Parse(IniFile.GetString("isolation", "offset", "0"));
        }

        internal void StoreSettings()
        {
            StoreSettings(fileName);
        }

        internal void StoreSettings(string fileName)
        {
            IniFile.SetFileName(fileName);

            IniFile.SetString("isolation", "tx", tx.ToString("0.#"));
            IniFile.SetString("isolation", "freq", freq.ToString("0.#"));

            IniFile.SetString("isolation", "att_spc", att_spc.ToString());
            IniFile.SetString("isolation", "rbw_spc", rbw_spc.ToString());
            IniFile.SetString("isolation", "vbw_spc", vbw_spc.ToString());

            IniFile.SetString("isolation", "time_points", time_points.ToString());
            IniFile.SetString("isolation", "freq_step", freq_step.ToString("0.#"));

            IniFile.SetString("isolation", "min_iso", min_iso.ToString("0.#"));
            IniFile.SetString("isolation", "max_iso", max_iso.ToString("0.#"));

            IniFile.SetString("isolation", "limit", limit.ToString("0.#"));

            IniFile.SetString("isolation", "attenuator", limit.ToString("0.#"));
            IniFile.SetString("isolation", "offset", limit.ToString("0.#"));
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
