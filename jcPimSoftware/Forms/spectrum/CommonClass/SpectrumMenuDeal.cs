// ===============================================================================
// 文件名：SpectrumMenuDeal
// 创建人：倪骞
// 日  期：2011-4-29 
//
// 描  述：频谱仪快捷菜单处理
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
using System.Windows.Forms;
using System.Drawing;

namespace jcPimSoftware
{
    class SpectrumMenuDeal
    {
        #region 变量定义

        /// <summary>
        /// 当前频谱仪类型
        /// </summary>
        private SpectrumDef.ESpectrumType CurrentSpectrumType;

        /// <summary>
        /// SPECTRUM菜单结构
        /// </summary>
        private struct MenuList
        {
            public string[] REF_LEVEL;
            public string[] SPAN;
            public string[] RBW;
            public string[] VBW;
            public string[] HOLD_MENU;
            public string[] SCALE;
            public string[] UNIT;
            public string[] REF_POSITION;
            public string[] MARK_SELECT;
            public string[] PEAK_HOLD;
            public string[] TRACE_SELECT;
        }

        /// <summary>
        /// 当前菜单结构
        /// </summary>
        private MenuList CurrentMenuList;

        /// <summary>
        /// SPECTRUM菜单高度
        /// </summary>
        public struct MenuHight
        {
            public int REF_LEVEL;
            public int SPAN;
            public int RBW;
            public int VBW;
            public int HOLD_MENU;
            public int SCALE;
            public int UNIT;
            public int REF_POSITION;
            public int MARK_SELECT;
            public int PEAK_HOLD;
            public int TRACE_SELECT;
        }

        /// <summary>
        /// 当前菜单高度
        /// </summary>
        private MenuHight _CurrentMenuHeight;
        public MenuHight CurrentMenuHeight
        {
            get { return _CurrentMenuHeight; }
        }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpectrumMenuDeal(SpectrumDef.ESpectrumType sptctrumType)
        {
            CurrentSpectrumType = sptctrumType;
            LoadMenuConfig();
        }

        #endregion

        #region SpeCat2快捷菜单

        private readonly string[] SpeCat2_REF_LEVEL ={ "dBm  emf  pd",
                                                       "-40  73  67",
                                                       "-30  83  77",
                                                       "-20  93  87",
                                                       "-10  103  97",
                                                       "-0  113  107", 
                                                       "10  123  117",
                                                       "Other..."};

        private readonly string[] SpeCat2_SPAN ={ "200KHz",
                                                  "500KHz",
                                                  "1MHz",
                                                  "2MHz",
                                                  "10MHz",
                                                  "20MHz",
                                                  "50MHz",
                                                  "100MHz",
                                                  "200MHz",
                                                  "Other..."};

        private readonly string[] SpeCat2_RBW ={ "1KHz,0",
                                                 "4KHz,1",
                                                 "8KHz,0",
                                                 "20KHz,1",
                                                 "40KHz,0",
                                                 "100KHz,1",
                                                 "250KHz,0"};

        private readonly string[] SpeCat2_SCALE ={ "10dB/Div", 
                                                  "5dB/Div", 
                                                  "2dB/Div"};

        private readonly string[] SpeCat2_UNIT ={ "dBm", 
                                                 "dBuVemf",
                                                 "dBuVpd"};

        private readonly string[] SpeCat2_REF_POSITION ={ "10Div",
                                                          "9Div",
                                                          "8Div",
                                                          "7Div",
                                                          "6Div",
                                                          "5Div",
                                                          "4Div",
                                                          "3Div",
                                                          "2Div",
                                                          "1Div",
                                                          "0Div"};

        private readonly string[] SpeCat2_HOLD_MENU ={ "MAXHOLD",
                                                       "MINHOLD"};

        private readonly string[] SpeCat2_MARK_SELECT ={ "ALL OFF", 
                                                         "MARKER1",
                                                         "MARKER2",
                                                         "MARKER3",
                                                         "MARKER4",
                                                         "MARKER5"};

        private readonly string[] SpeCat2_PEAK_HOLD ={ "OFF",
                                                       "1ST",
                                                       "2ND",
                                                       "3RD",
                                                       "4TH",};

        private readonly string[] SpeCat2_TRACE_SELECT ={ "ALL OFF", 
                                                          "TRACE1",
                                                          "TRACE2",
                                                          "TRACE3",
                                                          "TRACE4",
                                                          "TRACE5"};

        #endregion

        #region Bird快捷菜单

        private readonly string[] Bird_REF_LEVEL ={ "dBm  emf  pd",
                                                    "00 dB",
                                                    "10 dB",
                                                    "20 dB",
                                                    "30 dB"};

        private readonly string[] Bird_SPAN ={ "200KHz",
                                               "500KHz",
                                               "1MHz",
                                               "2MHz",
                                               "10MHz",
                                               "20MHz",
                                               "50MHz",
                                               "100MHz",
                                               "200MHz",
                                               "Other..."};

        private readonly string[] Bird_RBW ={ "1MHz,1",
                                              "300KHz,0",
                                              "100KHz,1",
                                              "30KHz,0",
                                              "10KHz,1",
                                              "3KHz,0",
                                              "1KHz,0",
                                              "300Hz,0",
                                              "100Hz,0",
                                              "30Hz,0",
                                              "10Hz,0"};

        private readonly string[] Bird_VBW ={ "1MHz,0",
                                              "300KHz,1",
                                              "100KHz,0",
                                              "30KHz,1",
                                              "10KHz,0",
                                              "3KHz,1",
                                              "1KHz,0",
                                              "300Hz,1",
                                              "100Hz,0",
                                              "30Hz,0",
                                              "10Hz,0"};

        private readonly string[] Bird_HOLD_MENU ={ "MAXHOLD",
                                                   "MINHOLD",
                                                   "AVERAGE"};

        #endregion

        #region  Deli快捷菜单

        private readonly string[] Deli_RBW ={ "1KHz,1",
                                              "10KHz,1",
                                              "100KHz,1",
                                              "1000KHz,1"};


        #endregion

        #region 加载快捷菜单的配置
        /// <summary>
        /// 加载快捷菜单的配置
        /// </summary>
        private void LoadMenuConfig()
        {
            switch (CurrentSpectrumType)
            {
                case SpectrumDef.ESpectrumType.SpeCat2:
                    CurrentMenuList.REF_LEVEL = SpeCat2_REF_LEVEL;
                    _CurrentMenuHeight.REF_LEVEL = -186;
                    CurrentMenuList.SPAN = SpeCat2_SPAN;
                    _CurrentMenuHeight.SPAN = -230;
                    CurrentMenuList.RBW = SpeCat2_RBW;
                    _CurrentMenuHeight.RBW = -76;
                    CurrentMenuList.HOLD_MENU = SpeCat2_HOLD_MENU;
                    _CurrentMenuHeight.HOLD_MENU = -54;
                    CurrentMenuList.SCALE = SpeCat2_SCALE;
                    _CurrentMenuHeight.SCALE = -76;
                    CurrentMenuList.UNIT = SpeCat2_UNIT;
                    _CurrentMenuHeight.UNIT = -76;
                    CurrentMenuList.REF_POSITION = SpeCat2_REF_POSITION;
                    _CurrentMenuHeight.REF_POSITION = -252;
                    CurrentMenuList.MARK_SELECT = SpeCat2_MARK_SELECT;
                    _CurrentMenuHeight.MARK_SELECT = -142;
                    CurrentMenuList.PEAK_HOLD = SpeCat2_PEAK_HOLD;
                    _CurrentMenuHeight.PEAK_HOLD = -120;
                    CurrentMenuList.TRACE_SELECT = SpeCat2_TRACE_SELECT;
                    _CurrentMenuHeight.TRACE_SELECT = -142;
                    break;
                case SpectrumDef.ESpectrumType.Bird:
                    CurrentMenuList.REF_LEVEL = Bird_REF_LEVEL;
                    _CurrentMenuHeight.REF_LEVEL = -120;
                    CurrentMenuList.SPAN = Bird_SPAN;
                    _CurrentMenuHeight.SPAN = -230;
                    CurrentMenuList.RBW = Bird_RBW;
                    _CurrentMenuHeight.RBW = -76;
                    CurrentMenuList.VBW = Bird_VBW;
                    _CurrentMenuHeight.VBW = -250;
                    CurrentMenuList.HOLD_MENU = Bird_HOLD_MENU;
                    _CurrentMenuHeight.HOLD_MENU = -76;
                    CurrentMenuList.SCALE = SpeCat2_SCALE;
                    _CurrentMenuHeight.SCALE = -76;
                    CurrentMenuList.UNIT = SpeCat2_UNIT;
                    _CurrentMenuHeight.UNIT = -76;
                    CurrentMenuList.REF_POSITION = SpeCat2_REF_POSITION;
                    _CurrentMenuHeight.REF_POSITION = -252;
                    CurrentMenuList.MARK_SELECT = SpeCat2_MARK_SELECT;
                    _CurrentMenuHeight.MARK_SELECT = -142;
                    CurrentMenuList.PEAK_HOLD = SpeCat2_PEAK_HOLD;
                    _CurrentMenuHeight.PEAK_HOLD = -120;
                    CurrentMenuList.TRACE_SELECT = SpeCat2_TRACE_SELECT;
                    _CurrentMenuHeight.TRACE_SELECT = -142;
                    break;
                case SpectrumDef.ESpectrumType.Deli:
                    CurrentMenuList.REF_LEVEL = SpeCat2_REF_LEVEL;
                    _CurrentMenuHeight.REF_LEVEL = -186;
                    CurrentMenuList.SPAN = SpeCat2_SPAN;
                    _CurrentMenuHeight.SPAN = -230;
                    CurrentMenuList.RBW = Deli_RBW;
                    _CurrentMenuHeight.RBW = -100;
                    CurrentMenuList.HOLD_MENU = SpeCat2_HOLD_MENU;
                    _CurrentMenuHeight.HOLD_MENU = -54;
                    CurrentMenuList.SCALE = SpeCat2_SCALE;
                    _CurrentMenuHeight.SCALE = -76;
                    CurrentMenuList.UNIT = SpeCat2_UNIT;
                    _CurrentMenuHeight.UNIT = -30;
                    CurrentMenuList.REF_POSITION = SpeCat2_REF_POSITION;
                    _CurrentMenuHeight.REF_POSITION = -252;
                    CurrentMenuList.MARK_SELECT = SpeCat2_MARK_SELECT;
                    _CurrentMenuHeight.MARK_SELECT = -142;
                    CurrentMenuList.PEAK_HOLD = SpeCat2_PEAK_HOLD;
                    _CurrentMenuHeight.PEAK_HOLD = -120;
                    CurrentMenuList.TRACE_SELECT = SpeCat2_TRACE_SELECT;
                    _CurrentMenuHeight.TRACE_SELECT = -142;
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region 由菜单类型获取内容
        /// <summary>
        /// 由菜单类型获取内容
        /// </summary>
        ///<param name="menuType">菜单类型</param>
        /// <returns>菜单内容</returns>
        public string[] GetMenuContent(SpectrumDef.EShortcutMenu menuType)
        {
            string[] revContent = null;
            switch (menuType)
            {
                case SpectrumDef.EShortcutMenu.REF_LEVEL:
                    revContent = CurrentMenuList.REF_LEVEL;
                    break;
                case SpectrumDef.EShortcutMenu.SPAN:
                    revContent = CurrentMenuList.SPAN;
                    break;
                case SpectrumDef.EShortcutMenu.RBW:
                    revContent = CurrentMenuList.RBW;
                    break;
                case SpectrumDef.EShortcutMenu.VBW:
                    revContent = CurrentMenuList.VBW;
                    break;
                case SpectrumDef.EShortcutMenu.HOLD_MENU:
                    revContent = CurrentMenuList.HOLD_MENU;
                    break;
                case SpectrumDef.EShortcutMenu.SCALE:
                    revContent = CurrentMenuList.SCALE;
                    break;
                case SpectrumDef.EShortcutMenu.UNIT:
                    revContent = CurrentMenuList.UNIT;
                    break;
                case SpectrumDef.EShortcutMenu.REF_POSITION:
                    revContent = CurrentMenuList.REF_POSITION;
                    break;
                case SpectrumDef.EShortcutMenu.MARK_SELECT:
                    revContent = CurrentMenuList.MARK_SELECT;
                    break;
                case SpectrumDef.EShortcutMenu.PEAK_HOLD:
                    revContent = CurrentMenuList.PEAK_HOLD;
                    break;
                case SpectrumDef.EShortcutMenu.TRACE_SELECT:
                    revContent = CurrentMenuList.TRACE_SELECT;
                    break;
                default:
                    break;
            }

            return revContent;
        }

        #endregion
    }
}
