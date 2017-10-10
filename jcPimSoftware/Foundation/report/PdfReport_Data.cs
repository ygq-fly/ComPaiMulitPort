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
        /// ����ժҪ���֣������������Ϣ
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
        /// ����ժҪ���֣���������ʹ���
        /// </summary>
        public string Modno
        {
            get { return modno; }
            set { modno = value; }
        }

        /// <summary>
        /// ����ժҪ���֣���������к�
        /// </summary>
        public string Serno
        {
            get { return serno; }
            set { serno = value; }
        }

        /// <summary>
        /// ����ժҪ���֣�������Ա�����������
        /// </summary>
        public string Opeor
        {
            get { return opeor; }
            set { opeor = value; }
        }

        /// <summary>
        /// �����������˵�����֣��������
        /// </summary>
        public float Tx_out
        {
            get { return tx_out; }
            set { tx_out = value; }
        }

        /// <summary>
        /// �����������˵�����֣���ʼƵ��
        /// </summary>
        public float F_start
        {
            get { return f_start; }
            set { f_start = value; }
        }

        /// <summary>
        /// �����������˵�����֣�����Ƶ��
        /// </summary>
        public float F_stop
        {
            get { return f_stop; }
            set { f_stop = value; }
        }

        /// <summary>
        /// �����������˵�����֣����Ե���
        /// </summary>
        public int Points_Num
        {
            get { return points_num; }
            set { points_num = value; }
        }

        /// <summary>
        /// �����������˵�����֣��������ֵ
        /// </summary>
        public float Max_value
        {
            get { return max_value; }
            set { max_value = value; }
        }

        /// <summary>
        /// �����������˵�����֣�������Сֵ
        /// </summary>
        public float Min_value
        {
            get { return min_value; }
            set { min_value = value; }
        }

        /// <summary>
        /// �����������˵�����֣����Բο�ֵ���жϵķ�ֵ��
        /// </summary>
        public float Limit_value
        {
            get { return limit_value; }
            set { limit_value = value; }
        }

        /// <summary>
        /// �����������˵�����֣����Խ��ۣ�PASS������FAIL
        /// </summary>
        public string Passed
        {
            get { return passed; }
            set { passed = value; }
        }

        /// <summary>
        /// �����������˵�����֣�����ʵʱ�����ͼ
        /// </summary>
        public System.Drawing.Image Image
        {
            get { return image; }
            set { image = value; }
        }


        /// <summary>
        /// ������Խ�β���֣�����ģʽ���ַ�������
        /// </summary>
        public string Footer
        {
            get { return footer; }
            set { footer = value; }
        }
    }
}