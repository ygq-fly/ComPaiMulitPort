// ===============================================================================
// 文件名：SpectrumRBW
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：RBW相关参数转换类
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

namespace jcPimSoftware
{
    class SpectrumRBW
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        private SpectrumRBW()
        {

        }
        #endregion


        #region SpeCat2

        #region 由RBW索引获取RBW值
        /// <summary>
        /// 由RBW索引获取RBW值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>RBW(KHz)</returns>
        private static int SpeCat2_GetRbwValue(int index)
        {
            int revRbw = index;
            return revRbw;
        }

        #endregion

        #region 由RBW值获取RBW真实值
        /// <summary>
        /// 由RBW值获取RBW真实值
        /// </summary>
        /// <param name="value">RBW</param>
        /// <returns>RBW真实值(KHz)</returns>
        private static int SpeCat2_GetRbwRealValue(int value)
        {
            int revRbw;
            switch (value)
            {
                case 0:
                    revRbw = 1;
                    break;
                case 1:
                    revRbw = 4;
                    break;
                case 2:
                    revRbw = 8;
                    break;
                case 3:
                    revRbw = 20;
                    break;
                case 4:
                    revRbw = 40;
                    break;
                case 5:
                    revRbw = 100;
                    break;
                case 6:
                    revRbw = 250;
                    break;
                default:
                    revRbw = 4;
                    break;
            }

            return revRbw;
        }

        #endregion

        #region 由RBW真实值获取索引
        /// <summary>
        /// 由RBW真实值获取索引
        /// </summary>
        /// <param name="value">RBW</param>
        /// <returns>索引</returns>
        private static int SpeCat2_GetIndexByValue(int value)
        {
            int revIndex = 0;
            switch (value)
            {
                case 1:
                    revIndex = 0;
                    break;
                case 4:
                    revIndex = 1;
                    break;
                case 8:
                    revIndex = 2;
                    break;
                case 20:
                    revIndex = 3;
                    break;
                case 40:
                    revIndex = 4;
                    break;
                case 100:
                    revIndex = 5;
                    break;
                case 250:
                    revIndex = 6;
                    break;
                default:
                    break;
            }

            return revIndex;
        }

        #endregion

        #endregion

        #region Bird

        #region 由RBW索引获取RBW值
        /// <summary>
        /// 由RBW索引获取RBW值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>RBW(Hz)</returns>
        private static int Bird_GetRbwValue(int index)
        {
            return index;
        }

        #endregion

        #region 由RBW值获取RBW真实值
        /// <summary>
        /// 由RBW值获取RBW真实值
        /// </summary>
        /// <param name="value">RBW(Hz)</param>
        /// <returns>RBW真实值(Hz)</returns>
        private static int Bird_GetRbwRealValue(int value)
        {
            int revRbw = 0;
            switch (value)
            {
                case 0:
                    revRbw = 1000000;
                    break;
                case 1:
                    revRbw = 300000;
                    break;
                case 2:
                    revRbw = 100000;
                    break;
                case 3:
                    revRbw = 30000;
                    break;
                case 4:
                    revRbw = 10000;
                    break;
                case 5:
                    revRbw = 3000;
                    break;
                case 6:
                    revRbw = 1000;
                    break;
                case 7:
                    revRbw = 300;
                    break;
                case 8:
                    revRbw = 100;
                    break;
                case 9:
                    revRbw = 30;
                    break;
                case 10:
                    revRbw = 10;
                    break;
                default:
                    break;
            }
            return revRbw;
        }

        #endregion

        #region 由RBW/VBW比率计算VBW
        /// <summary>
        /// 由RBW/VBW比率计算VBW
        /// </summary>
        /// <param name="indexRbw">当前RBW索引</param>
        /// <returns>VBW</returns>
        public static int CountVBW(int index)
        {
            int revVbw = 0;
            revVbw = index + 1;

            if (revVbw > 10)
            {
                revVbw = index;
            }

            return revVbw;
        }

        #endregion 

        #region 由RNW/VBW真实值获取索引
        /// <summary>
        /// 由RNW/VBW真实值获取索引
        /// </summary>
        /// <param name="value">RBW</param>
        /// <returns>索引</returns>
        private static int Bird_GetIndexByValue(int value)
        {
            int revIndex = 0;
            switch (value)
            {
                case 1000000:
                    revIndex = 0;
                    break;
                case 300000:
                    revIndex = 1;
                    break;
                case 100000:
                    revIndex = 2;
                    break;
                case 30000:
                    revIndex = 3;
                    break;
                case 10000:
                    revIndex = 4;
                    break;
                case 3000:
                    revIndex = 5;
                    break;
                case 1000:
                    revIndex = 6;
                    break;
                case 300:
                    revIndex = 7;
                    break;
                case 100:
                    revIndex = 8;
                    break;
                case 30:
                    revIndex = 9;
                    break;
                case 10:
                    revIndex = 10;
                    break;
                default:
                    break;
            }

            return revIndex;
        }

        #endregion

        #endregion

        #region Deli

        #region 由RBW索引获取RBW值
        /// <summary>
        /// 由RBW索引获取RBW值
        /// </summary>
        /// <param name="index">索引</param>
        /// <returns>RBW(KHz)</returns>
        private static int Deli_GetRbwValue(int index)
        {
            return index;
        }

        #endregion

        #region 由RBW值获取RBW真实值
        /// <summary>
        /// 由RBW值获取RBW真实值
        /// </summary>
        /// <param name="value">RBW</param>
        /// <returns>RBW真实值(KHz)</returns>
        private static int Deli_GetRbwRealValue(int value)
        {
            int revRbw;
            switch (value)
            {
                case 0:
                    revRbw = 1;
                    break;
                case 1:
                    revRbw = 10;
                    break;
                case 2:
                    revRbw = 100;
                    break;
                case 3:
                    revRbw = 1000;
                    break;
                default:
                    revRbw = 10;
                    break;
            }

            return revRbw;
        }

        #endregion

        #region 由RBW真实值获取索引
        /// <summary>
        /// 由RBW真实值获取索引
        /// </summary>
        /// <param name="value">RBW</param>
        /// <returns>索引</returns>
        private static int Deli_GetIndexByValue(int value)
        {
            int revIndex = 0;
            switch (value)
            {
                case 1:
                    revIndex = 0;
                    break;
                case 10:
                    revIndex = 1;
                    break;
                case 100:
                    revIndex = 2;
                    break;
                case 1000:
                    revIndex = 3;
                    break;
                default:
                    break;
            }

            return revIndex;
        }

        #endregion

        #endregion


        #region 由RBW索引获取RBW值
        /// <summary>
        /// 由RBW索引获取RBW值
        /// </summary>
        /// <param name="type">频谱仪类型</param>
        /// <param name="index">索引</param>
        /// <returns>RBW</returns>
        public static int GetRbwValue(SpectrumDef.ESpectrumType type, int index)
        {
            int revRBW = 0;

            switch (type)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    revRBW = SpeCat2_GetRbwValue(index);
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    revRBW = Bird_GetRbwValue(index);
                    break;
                case SpectrumDef.ESpectrumType.Deli:
                    revRBW = Deli_GetRbwValue(index);
                    break;
                default:
                    break;
            }

            return revRBW;
        }

        #endregion

        #region 由RBW真实值获取索引
        /// <summary>
        /// 由RBW真实值获取索引
        /// </summary>
        /// <param name="type">频谱仪类型</param>
        /// <param name="value">RBW</param>
        /// <returns>索引</returns>
        public static int GetIndexByValue(SpectrumDef.ESpectrumType type, int value)
        {
            int revIndex = 0;

            switch (type)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    revIndex = SpeCat2_GetIndexByValue(value);
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    revIndex = Bird_GetIndexByValue(value);
                    break;
                case SpectrumDef.ESpectrumType.Deli:
                    revIndex = Deli_GetIndexByValue(value);
                    break;
                default:
                    break;
            }

            return revIndex;
        }

        #endregion

        #region 由RBW值获取RBW真实值
        /// <summary>
        /// 由RBW值获取RBW真实值
        /// </summary>
        /// <param name="value">RBW</param>
        /// <returns>RBW真实值</returns>
        public static int GetRbwRealValue(SpectrumDef.ESpectrumType type, int value)
        {
            int revRBW = 0;

            switch (type)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    revRBW = SpeCat2_GetRbwRealValue(value);
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    revRBW = Bird_GetRbwRealValue(value);
                    break;
                case SpectrumDef.ESpectrumType.Deli:
                    revRBW = Deli_GetRbwRealValue(value);
                    break;
                default:
                    break;
            }

            return revRBW;
        }

        #endregion
    }
}
