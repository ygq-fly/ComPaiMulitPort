// ===============================================================================
// 文件名：SpectrumDef
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：频谱仪公共定义
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
    class SpectrumDef
    {
        /// <summary>
        /// 频谱仪类型枚举
        /// </summary>
        public enum ESpectrumType
        {
            SpeCat2 = 0,
            Bird = 1,
            Deli=2
        }

        /// <summary>
        /// 快捷菜单枚举
        /// </summary>
        public enum EShortcutMenu
        {
            REF_LEVEL = 0,
            SPAN = 1,
            RBW = 2,
            VBW = 3,
            SCALE = 4,
            UNIT = 5,
            REF_POSITION = 6,
            HOLD_MENU = 7,
            MARK_SELECT = 8,
            PEAK_HOLD = 9,
            TRACE_SELECT = 10
        }

        /// <summary>
        /// 频谱分析补偿参数结构
        /// </summary>
        public struct OffsetObj
        {
            /// <summary>
            /// 分段补偿的结束频率
            /// </summary>
            public double endFreq;
            /// <summary>
            /// 补偿的一次项系数
            /// </summary>
            public double paramA;
            /// <summary>
            /// 补偿的常数项
            /// </summary>
            public double paramB;
        }
    }
}
