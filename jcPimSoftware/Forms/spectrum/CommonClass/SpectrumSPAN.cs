// ===============================================================================
// �ļ�����SpectrumSPAN
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����SPAN��ز���ת����
//         
//
// ��  ���� 1.0.0.0
//
// ���¼�¼ 
// ===============================================================================
// ʱ  �䣺 2011-4-29   	   �������ļ�
//
// ===============================================================================



using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class SpectrumSPAN
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        private SpectrumSPAN()
        {

        }

        #endregion


        #region SpeCat2

        #region ��SPAN������ȡSPANֵ
        /// <summary>
        /// ��SPAN������ȡSPANֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>SPAN(KHz)</returns>
        public static int SpeCat2_GetSpanValue(int index)
        {
            int revSpan = 0;
            switch (index)
            {
                case 0:
                    revSpan = 200;
                    break;
                case 1:
                    revSpan = 500;
                    break;
                case 2:
                    revSpan = 1000;
                    break;
                case 3:
                    revSpan = 2000;
                    break;
                case 4:
                    revSpan = 10000;
                    break;
                case 5:
                    revSpan = 20000;
                    break;
                case 6:
                    revSpan = 50000;
                    break;
                case 7:
                    revSpan = 100000;
                    break;
                case 8:
                    revSpan = 200000;
                    break;
                default:
                    revSpan = 2000;
                    break;
            }

            return revSpan;
        }

        #endregion 

        #region ��SPANֵ��ȡSPAN����
        /// <summary>
        /// ��SPANֵ��ȡSPAN����
        /// </summary>
        /// <param name="value">SPAN</param>
        /// <returns>����</returns>
        public static int SpeCat2_GetSpanIndex(int value)
        {
            int revIndex = 0;
            switch (value)
            {
                case 200:
                    revIndex = 0;
                    break;
                case 500:
                    revIndex = 1;
                    break;
                case 1000:
                    revIndex = 2;
                    break;
                case 2000:
                    revIndex = 3;
                    break;
                case 10000:
                    revIndex = 4;
                    break;
                case 20000:
                    revIndex = 5;
                    break;
                case 50000:
                    revIndex = 6;
                    break;
                case 100000:
                    revIndex = 7;
                    break;
                case 200000:
                    revIndex = 8;
                    break;
                default:
                    revIndex = -1;
                    break;
            }

            return revIndex;
        }

        #endregion

        #endregion


        #region Bird



        #endregion
    }
}
