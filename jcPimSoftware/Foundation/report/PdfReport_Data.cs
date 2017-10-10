using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class PdfReport_Data
    {
        string desc;
        string modno;
        string serno;
        string opeor;
        float tx_out;
        float f_start;
        float f_stop;       
        int points_num;        
        float max_value;
        float min_value;
        float limit_value;
        string passed;
        string footer;
        string type;
        System.Drawing.Image image;

        /// <summary>
        /// 报表摘要部分，被测件描述信息
        /// </summary>
        
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 报表摘要部分，被测件类型代码
        /// </summary>
        public string Modno
        {
            get { return modno; }
            set { modno = value; }
        }

        /// <summary>
        /// 报表摘要部分，被测件序列号
        /// </summary>
        public string Serno
        {
            get { return serno; }
            set { serno = value; }
        }

        /// <summary>
        /// 报表摘要部分，测试人员代码或者描述
        /// </summary>
        public string Opeor
        {
            get { return opeor; }
            set { opeor = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，输出功率
        /// </summary>
        public float Tx_out
        {
            get { return tx_out; }
            set { tx_out = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，起始频率
        /// </summary>
        public float F_start
        {
            get { return f_start; }
            set { f_start = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，结束频率
        /// </summary>
        public float F_stop
        {
            get { return f_stop; }
            set { f_stop = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，测试点数
        /// </summary>
        public int Points_Num
        {
            get { return points_num; }
            set { points_num = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，测试最大值
        /// </summary>
        public float Max_value
        {
            get { return max_value; }
            set { max_value = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，测试最小值
        /// </summary>
        public float Min_value
        {
            get { return min_value; }
            set { min_value = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，测试参考值（判断的阀值）
        /// </summary>
        public float Limit_value
        {
            get { return limit_value; }
            set { limit_value = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，测试结论，PASS，或者FAIL
        /// </summary>
        public string Passed
        {
            get { return passed; }
            set { passed = value; }
        }

        /// <summary>
        /// 报表测试条件说明部分，测试实时界面截图
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return image; }
            set { image = value; }
        }


        /// <summary>
        /// 报表测试结尾部分，测试模式的字符串描述
        /// </summary>
        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }
    }
}