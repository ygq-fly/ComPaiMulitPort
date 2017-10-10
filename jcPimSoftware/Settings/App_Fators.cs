using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class Offset_Fators
    {
        #region 实例变量声明
        /// <summary>
        /// 配置文件名称
        /// </summary>
        private readonly string fileName;

        /// <summary>
        /// 设置项所在节的名称
        /// </summary>
        private readonly string sectionName;

        /// <summary>
        /// 设置项名称
        /// </summary>
        private readonly string keyName;

        /// <summary>
        /// 因子字符串，以逗号和空格进行分隔
        /// </summary>
        private string factors;

        /// <summary>
        /// 补偿系数
        /// </summary>
        private double a1;
        private double b1;
        private double c1;
        private double a2;
        private double b2;
        private double c2;
        #endregion 实例变量声明

        /// <summary>
        /// 需要提供配置文件的名称，配置项所在节名称，配置项名称
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sectionName"></param>
        /// <param name="keyName"></param>
        internal Offset_Fators(string fileName, string sectionName, string keyName)
        {
            this.fileName = fileName;
            this.sectionName = sectionName;
            this.keyName = keyName;
        }

        internal void LoadOffsets()
        {          
            string strTemp;

            //设置配置文件，以操作之
            IniFile.SetFileName(fileName);

            strTemp = IniFile.GetString(sectionName, keyName, "");

            factors = strTemp.Trim();

            a1 = GetFactors(0);
            b1 = GetFactors(1);
            c1 = GetFactors(2);

            a2 = GetFactors(3);
            b2 = GetFactors(4);
            c2 = GetFactors(5);
        }

        internal void StoreOffsets()
        {
            //设置配置文件，以操作之
            IniFile.SetFileName(fileName);

            string s = a1.ToString("0.#######") + ", " +
                       b1.ToString("0.#######") + ", " +
                       c1.ToString("0.#######") + ", " +
                       a2.ToString("0.#######") + ", " +
                       b2.ToString("0.#######") + ", " +
                       c2.ToString("0.#######");
            
            IniFile.SetString(sectionName, keyName, s);
        }

        private readonly int maxItemCount = 6;
        private double GetFactors(int i)
        {
            double v = 0.0d;

            string item = IniFile.GetItemFrom(factors, i, maxItemCount);

            if (String.IsNullOrEmpty(item))
                v = 0.0d;

            else try
                {
                    v = double.Parse(item);
                }
                catch (InvalidCastException)
                {
                    v = 0.0d;
                }

            return v;
        }

        internal double ValueWithOffset(double f, double p)
        {
            return (a1 * f * f + b1 * f + c1 + a2 * p * p + b2 * p + c2);
        }

        private readonly int AvgNum = 10;
        internal double DeltaAvg(double s, double e, double p)
        {
            double f, d, sum;
            
            f = s;
            sum = 0;           
            d = (s - e) / AvgNum;

            for (int i = 0; i <= AvgNum; i++)
            {
                sum = sum + ValueWithOffset(f, p);
                f = f + d;
            };

            return (sum / (AvgNum + 1)) - p;
        }

        internal double A1
        {
            get { return a1; }
            set { a1 = value; }
        }

        internal double B1
        {
            get { return b1; }
            set { b1 = value; }
        }

        internal double C1
        {
            get { return c1; }
            set { c1 = value; }
        }

        internal double A2
        {
            get { return a2; }
            set { a2 = value; }
        }

        internal double B2
        {
            get { return b2; }
            set { b2 = value; }
        }

        internal double C2
        {
            get { return c2; }
            set { c2 = value; }
        }    
    }

    class App_Factors
    {
        static internal Offset_Fators pim_tx1;
        static internal Offset_Fators pim_tx1_disp;

        static internal Offset_Fators pim_tx2;
        static internal Offset_Fators pim_tx2_disp;

        static internal Offset_Fators iso_tx1;
        static internal Offset_Fators iso_tx1_disp;

        static internal Offset_Fators iso_tx2;
        static internal Offset_Fators iso_tx2_disp;

        static internal Offset_Fators vsw_tx1;
        static internal Offset_Fators vsw_tx1_disp;

        static internal Offset_Fators vsw_tx2;
        static internal Offset_Fators vsw_tx2_disp;

        static internal Offset_Fators har_tx1;
        static internal Offset_Fators har_tx1_disp;

        static internal Offset_Fators har_tx2;
        static internal Offset_Fators har_tx2_disp;


        /// <summary>
        /// 这里的rx1,rx2 代表收信通道1，收信通道2
        /// </summary>
        static internal Offset_Fators pim_rx1;

        static internal Offset_Fators pim_rx2;

        static internal Offset_Fators iso_rx1;

        static internal Offset_Fators iso_rx2;

        static internal Offset_Fators vsw_rx1;

        static internal Offset_Fators vsw_rx2;

        static internal Offset_Fators har_rx1;

        static internal Offset_Fators har_rx2;

         
        public App_Factors()
        {            
        }

        static internal void NewFactors(string txFileName, string txFileName_disp, string rxFileName)
        {
            pim_tx1 = new Offset_Fators(txFileName, "pim_tx1", "factors");
            pim_tx1_disp = new Offset_Fators(txFileName_disp, "pim_tx1", "factors");

            pim_tx2 = new Offset_Fators(txFileName, "pim_tx2", "factors");
            pim_tx2_disp = new Offset_Fators(txFileName_disp, "pim_tx2", "factors");

            pim_rx1 = new Offset_Fators(rxFileName, "pim_rx1", "factors");
            pim_rx2 = new Offset_Fators(rxFileName, "pim_rx2", "factors");

            //iso_tx1 = new Offset_Fators(txFileName, "iso_tx1", "factors");
            //iso_tx1_disp = new Offset_Fators(txFileName_disp, "iso_tx1", "factors");

            //iso_tx2 = new Offset_Fators(txFileName, "iso_tx2", "factors");
            //iso_tx2_disp = new Offset_Fators(txFileName_disp, "iso_tx2", "factors");

            //vsw_tx1 = new Offset_Fators(txFileName, "vsw_tx1", "factors");
            //vsw_tx1_disp = new Offset_Fators(txFileName_disp, "vsw_tx1", "factors");

            //vsw_tx2 = new Offset_Fators(txFileName, "vsw_tx2", "factors");
            //vsw_tx2_disp = new Offset_Fators(txFileName_disp, "vsw_tx2", "factors");

            //har_tx1 = new Offset_Fators(txFileName, "har_tx1", "factors");
            //har_tx1_disp = new Offset_Fators(txFileName_disp, "har_tx1", "factors");

            //har_tx2 = new Offset_Fators(txFileName, "har_tx2", "factors");
            //har_tx2_disp = new Offset_Fators(txFileName_disp, "har_tx2", "factors");


           

            //iso_rx1 = new Offset_Fators(rxFileName, "iso_rx1", "factors");
            //iso_rx2 = new Offset_Fators(rxFileName, "iso_rx2", "factors");

            //vsw_rx1 = new Offset_Fators(rxFileName, "vsw_rx1", "factors");
            //vsw_rx2 = new Offset_Fators(rxFileName, "vsw_rx2", "factors");

            //har_rx1 = new Offset_Fators(rxFileName, "har_rx1", "factors");
            //har_rx2 = new Offset_Fators(rxFileName, "har_rx2", "factors");
        }

        static internal void LoadFactros()
        {
            //加载互调模块的补偿系数
            pim_tx1.LoadOffsets();
            pim_tx1_disp.LoadOffsets();
            pim_rx1.LoadOffsets();

            pim_tx2.LoadOffsets();
            pim_tx2_disp.LoadOffsets();
            pim_rx2.LoadOffsets();

            ////加载隔离度模块的补偿系数
            //iso_tx1.LoadOffsets();
            //iso_tx1_disp.LoadOffsets();
            //iso_rx1.LoadOffsets();

            //iso_tx2.LoadOffsets();
            //iso_tx2_disp.LoadOffsets();
            //iso_rx2.LoadOffsets();

            ////加载驻波比模块的补偿系数
            //vsw_tx1.LoadOffsets();
            //vsw_tx1_disp.LoadOffsets();
            //vsw_rx1.LoadOffsets();

            //vsw_tx2.LoadOffsets();
            //vsw_tx2_disp.LoadOffsets();
            //vsw_rx2.LoadOffsets();

            ////加载谐波模块的补偿系数
            //har_tx1.LoadOffsets();
            //har_tx1_disp.LoadOffsets();
            //har_rx1.LoadOffsets();

            //har_tx2.LoadOffsets();
            //har_tx2_disp.LoadOffsets();
            //har_rx2.LoadOffsets();
        }
    }   
}