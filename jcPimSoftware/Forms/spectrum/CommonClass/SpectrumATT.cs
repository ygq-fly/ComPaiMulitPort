// ===============================================================================
// �ļ�����SpectrumATT
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ����ATT��ز���ת����
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
    class SpectrumATT
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        private SpectrumATT()
        {

        }

        #endregion


        #region SpeCat2

        #region ��ATTֵ��ȡ�˵�����
        /// <summary>
        /// ��ATTֵ��ȡ�˵�����
        /// </summary>
        /// <param name="value">ATT</param>
        /// <returns>����</returns>
        private static int SpeCat2_GetAttIndex(int value)
        {
            int revIndex = -1;
            switch (value)
            {
                case 0:
                    revIndex = 1;
                    break;
                case 10:
                    revIndex = 2;
                    break;
                case 20:
                    revIndex = 3;
                    break;
                case 30:
                    revIndex = 4;
                    break;
                case 40:
                    revIndex = 5;
                    break;
                case 50:
                    revIndex = 6;
                    break;
                default:
                    revIndex = -1;
                    break;
            }

            return revIndex;
        }

        #endregion

        #region �ɲ˵�������ȡATTֵ
        /// <summary>
        /// �ɲ˵�������ȡATTֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>ATT</returns>
        private static int SpeCat2_GetAttValue(int index)
        {
            int revAtt;
            switch (index)
            {
                case 1:
                    revAtt = 0;
                    break;
                case 2:
                    revAtt = 10;
                    break;
                case 3:
                    revAtt = 20;
                    break;
                case 4:
                    revAtt = 30;
                    break;
                case 5:
                    revAtt = 40;
                    break;
                case 6:
                    revAtt = 50;
                    break;
                default:
                    revAtt = 0;
                    break;
            }
            return revAtt;
        }

        #endregion

        #endregion


        #region Bird

        #region ��ATT��ȡ�˵�����
        /// <summary>
        /// ��ATTֵ��ȡ�˵�����
        /// </summary>
        /// <param name="value">ATT</param>
        /// <returns>����</returns>
        private static int Bird_GetAttIndex(int value)
        {
            int revIndex = 0;
            switch (value)
            {
                case -1:
                    revIndex = 0;
                    break;
                case 0:
                    revIndex = 1;
                    break;
                case 10:
                    revIndex = 2;
                    break;
                case 20:
                    revIndex = 3;
                    break;
                case 30:
                    revIndex = 4;
                    break;
                default:
                    break;
            }

            return revIndex;
        }

        #endregion

        #region �ɲ˵�������ȡATTֵ
        /// <summary>
        /// �ɲ˵�������ȡATTֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>ATT</returns>
        private static int Bird_GetAttValue(int index)
        {
            int revAtt;
            switch (index)
            {
                case 1:
                    revAtt = 0;
                    break;
                case 2:
                    revAtt = 10;
                    break;
                case 3:
                    revAtt = 20;
                    break;
                case 4:
                    revAtt = 30;
                    break;
                default:
                    revAtt = 0;
                    break;
            }
            return revAtt;
        }

        #endregion

        #endregion


        #region ��ATTֵ��ȡ�˵�����
        /// <summary>
        /// ��ATTֵ��ȡ�˵�����
        /// </summary>
        /// <param name="value">ATT</param>
        /// <returns>����</returns>
        public static int GetAttIndex(SpectrumDef.ESpectrumType type, int value)
        {
            int revIndex = 0;
            switch (type)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    revIndex = SpeCat2_GetAttIndex(value);
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    revIndex = Bird_GetAttIndex(value);
                    break;
                case SpectrumDef.ESpectrumType.Deli:
                    revIndex = SpeCat2_GetAttIndex(value);
                    break;
            }

            return revIndex;
        }

        #endregion 

        #region �ɲ˵�������ȡATTֵ
        /// <summary>
        /// �ɲ˵�������ȡATTֵ
        /// </summary>
        /// <param name="index">����</param>
        /// <returns>ATT</returns>
        public static int GetAttValue(SpectrumDef.ESpectrumType type, int index)
        {
            int revValue = 0;
            switch (type)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    revValue = SpeCat2_GetAttValue(index);
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    revValue = Bird_GetAttValue(index);
                    break;
                case SpectrumDef.ESpectrumType.Deli:
                    revValue = SpeCat2_GetAttValue(index);
                    break;
            }

            return revValue;
        }

        #endregion
    }
}
