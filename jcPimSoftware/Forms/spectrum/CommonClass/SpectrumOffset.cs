// ===============================================================================
// �ļ�����SpectrumOffset
// �����ˣ����
// ��  �ڣ�2011-4-29 
//
// ��  ��������Ƶ�ײ����ļ���
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
using System.IO;

namespace jcPimSoftware
{
    class SpectrumOffset
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        private SpectrumOffset()
        {

        }

        #endregion


        #region SpeCat2

        #region ���ز����ļ�����
        /// <summary>
        /// ���ز����ļ�����
        /// </summary>
        /// <param name="filePath">�ļ�·��</param>
        /// <returns>��������</returns>
        public static string[] LoadOffsetFile(string filePath)
        {
            string[] revOffsetData = null;                  //���صĲ�����������
            string[] readData;                              //���ļ���ȡ�Ĳ�����������
            List<string> dataFilter = new List<string>();   //���˵Ĳ������ݼ���
            List<string> dataOrder = new List<string>();    //����Ĳ������ݼ���
            List<string> dataUnique = new List<string>();   //������Ψһ�Ĳ������ݼ���

            //��ȡ�����ļ�
            if (File.Exists(filePath))
            {
                readData = File.ReadAllLines(filePath);

                //���˴�������
                double OffsetFreq = 0;
                double OffsetValue = 0;
                for (int i = 0; i < readData.Length; i++)
                {
                    try
                    {
                        OffsetFreq = double.Parse(readData[i].Split(',')[0]);
                        OffsetValue = double.Parse(readData[i].Split(',')[1]);
                        if (OffsetFreq > 0 && OffsetValue < 3000)
                        {
                            dataFilter.Add(readData[i]);
                        }
                    }
                    catch
                    { }
                }

                //���ݰ�Ƶ������
                double minFreq = 3000;
                int minIndex = 0;
                while (dataFilter.Count > 0)
                {
                    for (int j = 0; j < dataFilter.Count; j++)
                    {
                        OffsetFreq = double.Parse(dataFilter[j].Split(',')[0]);
                        if (OffsetFreq <= minFreq)
                        {
                            minFreq = OffsetFreq;
                            minIndex = j;
                        }
                    }
                    dataOrder.Add(dataFilter[minIndex]);
                    dataFilter.RemoveAt(minIndex);
                    minFreq = 3000;
                    minIndex = 0;
                }

                //��ȥ��ͬƵ�ʵĲ�������������ֵ���һ��
                double maxValue = 0;
                List<int> listFreqIndex = new List<int>();
                for (int j = 0; j < dataOrder.Count; j++)
                {
                    if (listFreqIndex.Contains(j))
                    {
                        continue;
                    }

                    OffsetFreq = double.Parse(dataOrder[j].Split(',')[0]);
                    OffsetValue = double.Parse(dataOrder[j].Split(',')[1]);
                    maxValue = OffsetValue;
                    for (int i = j + 1; i < dataOrder.Count; i++)
                    {
                        if (listFreqIndex.Contains(i))
                        {
                            continue;
                        }
                        if (double.Parse(dataOrder[i].Split(',')[0]) == OffsetFreq)
                        {
                            if (double.Parse(dataOrder[i].Split(',')[1]) > maxValue)
                            {
                                maxValue = double.Parse(dataOrder[i].Split(',')[1]);
                            }
                            listFreqIndex.Add(i);
                        }
                    }

                    dataUnique.Add(OffsetFreq + "," + maxValue.ToString());
                }

                revOffsetData = dataUnique.ToArray();
            }

            return revOffsetData;
        }

        #endregion

        #region ��װ�������ݽṹ
        /// <summary>
        /// ��װ�������ݽṹ
        /// </summary>
        /// <param name="OffsetDataArray">��������</param>
        /// <returns>��������</returns>
        public static jcPimSoftware.SpectrumDef.OffsetObj[] FormatOffsetData(string[] OffsetDataArray)
        {
            if (OffsetDataArray == null)
            {
                return null;
            }

            jcPimSoftware.SpectrumDef.OffsetObj[] revOffsetData = new jcPimSoftware.SpectrumDef.OffsetObj[OffsetDataArray.Length + 1];
            double OffsetFreq;
            double OffsetValue;

            for (int i = 0; i < OffsetDataArray.Length; i++)
            {
                OffsetFreq = double.Parse(OffsetDataArray[i].Split(',')[0]);
                OffsetValue = double.Parse(OffsetDataArray[i].Split(',')[1]);
                if (i == 0)
                {
                    //revOffsetData[i].endFreq = OffsetFreq;
                    //revOffsetData[i].paramA = OffsetValue / OffsetFreq;
                    //revOffsetData[i].paramB = -revOffsetData[i].paramA * OffsetFreq;

                    if (OffsetFreq > 0)
                    {
                        revOffsetData[i].endFreq = OffsetFreq;
                        revOffsetData[i].paramA = 0;
                        revOffsetData[i].paramB = OffsetValue;
                    }
                }
                else if (i == OffsetDataArray.Length)
                {
                    revOffsetData[i].endFreq = 3000;
                    revOffsetData[i].paramA = 0;
                    revOffsetData[i].paramB = double.Parse(OffsetDataArray[i - 1].Split(',')[1]);
                }
                else
                {
                    revOffsetData[i].endFreq = OffsetFreq;
                    revOffsetData[i].paramA = (OffsetValue - double.Parse(OffsetDataArray[i - 1].Split(',')[1])) /
                                              (OffsetFreq - double.Parse(OffsetDataArray[i - 1].Split(',')[0]));
                    revOffsetData[i].paramB = OffsetValue - revOffsetData[i].paramA * OffsetFreq;
                }
            }

            jcPimSoftware.SpectrumDef.OffsetObj lastObj = new SpectrumDef.OffsetObj();
            lastObj.endFreq = 3000;
            lastObj.paramA = 0;
            lastObj.paramB = double.Parse(OffsetDataArray[OffsetDataArray.Length - 1].Split(',')[1]);
            revOffsetData[OffsetDataArray.Length] = lastObj;

            return revOffsetData;
        }

        #endregion

        #endregion


        #region Bird


        #endregion
    }
}
