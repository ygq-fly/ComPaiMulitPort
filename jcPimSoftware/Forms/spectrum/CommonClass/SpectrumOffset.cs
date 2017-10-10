// ===============================================================================
// 文件名：SpectrumOffset
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：解析频谱补偿文件类
//         
//
// 版  本： 1.0.0.0
//
// 更新记录 
// ===============================================================================
// 时  间： 2011-4-29   	   创建该文件
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
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        private SpectrumOffset()
        {

        }

        #endregion


        #region SpeCat2

        #region 加载补偿文件数据
        /// <summary>
        /// 加载补偿文件数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>补偿数据</returns>
        public static string[] LoadOffsetFile(string filePath)
        {
            string[] revOffsetData = null;                  //返回的补偿数据数组
            string[] readData;                              //从文件读取的补偿数据数组
            List<string> dataFilter = new List<string>();   //过滤的补偿数据集合
            List<string> dataOrder = new List<string>();    //排序的补偿数据集合
            List<string> dataUnique = new List<string>();   //排序且唯一的补偿数据集合

            //读取补偿文件
            if (File.Exists(filePath))
            {
                readData = File.ReadAllLines(filePath);

                //过滤错误数据
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

                //数据按频率排序
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

                //除去相同频率的补偿，保留补偿值大的一个
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

        #region 封装补偿数据结构
        /// <summary>
        /// 封装补偿数据结构
        /// </summary>
        /// <param name="OffsetDataArray">补偿数组</param>
        /// <returns>补偿数据</returns>
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
