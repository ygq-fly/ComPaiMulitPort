using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{

     /// <summary>
    /// 自动校准辅助项，包含频率，回波损耗值，功率值，收信值
    /// 频率，回波损耗值从表格文件加载；
    /// 功率值，收信值由校准过程用实际测量值进行填充
    /// 仅适用于隔离度测试和驻波比测试的校准功能
    /// </summary>
    class Auto_CAL_Item
    {
        /// <summary>
        /// 功放的中心频率
        /// </summary>
        internal float F0;

        /// <summary>
        /// 从校准表格文件加载
        /// </summary>
        internal float RL0;

        /// <summary>
        /// 功放的输出功率
        /// </summary>
        internal float Tx0;

        /// <summary>
        /// 在F点上的收信值，即频谱的幅度值
        /// </summary>
        internal float Rx0;

        internal void Clone(Auto_CAL_Item dest)
        {
            if (dest != null)
            {
                dest.F0  = this.F0;
                dest.RL0 = this.RL0;
                dest.Tx0 = this.Tx0;
                dest.Rx0 = this.Rx0;
            }
        }
    }


    /// <summary>
    /// 自动校准辅助辅助项列表，包含频率，回波损耗值
    /// 仅适用于隔离度测试和驻波比测试的校准功能
    /// 由校准过程使用实际测量值填充列表的功率值，收信值
    /// </summary>
    class Auto_CAL_Items
    {
        /// <summary>
        /// 存放自动校准辅助列表的原始项
        /// </summary>
        private Auto_CAL_Item[] items;

        /// <summary>
        /// 使用校准文件，生成自动校准辅助列表
        /// </summary>
        /// <param name="tItems"></param>
        internal Auto_CAL_Items(List<RL0_TableItem> tItems)
        {
            if (tItems != null)
            {
                int count = tItems.Count;

                if (count > 0)
                {
                    this.items = new Auto_CAL_Item[count];

                    for (int i = 0; i < count; i++)
                    {
                        this.items[i] = new Auto_CAL_Item();

                        this.items[i].F0 = tItems[i].F;

                        this.items[i].RL0 = tItems[i].RL;
                    }
                }
            }

        }

        /// <summary>
        /// 返回第i个列表项
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        internal Auto_CAL_Item GetItem(int i)
        {
            if (items == null)
                return null;

            if ((i >= 0) && (i < items.Length))
                return items[i];

            else
                return null;
        }

        /// <summary>
        /// 以频率f为参数，从自动校准辅助列表返回与之最接近的列表项
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        internal Auto_CAL_Item GetItem(float f)
        {
            Auto_CAL_Item item = null;

            if (items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (Math.Abs(items[i].F0 - f) < 0.001f)
                    {
                        item = items[i];
                        break;
                    }
                }
            }

            //若找不到，则返回大于它的具有最小F0的列表项
            if (item == null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if ((items[i].F0 - f) >= 0.001f)
                    {
                        item = items[i];
                        break;
                    }
                }
            }

            return item;
        }


        /// <summary>
        /// 返回列表的长度
        /// </summary>
        internal int Length
        {
            get
            {
                int ln;

                if (items == null)
                    ln = 0;
                else
                    ln = items.Length;

                return ln;
            }
        }
    }


    /// <summary>
    /// 校准文件项，包含频率，回波损耗值 
    /// </summary>
    class RL0_TableItem
    {
        internal float F;

        internal float RL;
    }


    class RL0_Table
    {
        /// <summary>
        /// 校准表文件的名称
        /// </summary>
        private string fileName;

        private List<RL0_TableItem> tItems;

        //功放的自动校准辅助列表
        private Auto_CAL_Items cal_items;

        internal RL0_Table(string fileName)
        {
            this.fileName = fileName;
        }

        internal void LoadSettings()
        {
            string s = "";
            string s1 = "";
            string s2 = "";

            RL0_TableItem tItem_temp;

            System.IO.StreamReader textReader = new System.IO.StreamReader(fileName);

            tItems = new List<RL0_TableItem>();

            //加载补偿项
            while (!textReader.EndOfStream)
            {
                s = (textReader.ReadLine()).Trim();

                if (String.IsNullOrEmpty(s))
                    continue;

                s1 = IniFile.GetItemFrom(s, 0, 2);

                s2 = IniFile.GetItemFrom(s, 1, 2);

                try
                {
                    tItem_temp = new RL0_TableItem();

                    tItem_temp.F = float.Parse(s1);
                    tItem_temp.RL = float.Parse(s2);
                    tItems.Add(tItem_temp);

                }
                catch (System.InvalidCastException)
                {
                }
            }

            textReader.Close();
            textReader.Dispose();

            //生成自动校准辅助列表
            cal_items = new Auto_CAL_Items(tItems);
        }

        /// <summary>
        /// 自动校准原始RL数据列表，由调试人员填入
        /// </summary>
        internal List<RL0_TableItem> RL0_Items
        {
            get { return tItems; }
        }


        /// <summary>
        /// 功放的自动校准辅助列表
        /// </summary>
        internal Auto_CAL_Items Cal_Carrier
        {
            get { return cal_items; }
        }

    }


    class RL0_Tables
    {
        static private RL0_Table[] tables;

        private RL0_Tables()
        {
            //
        }

        /// <summary>
        /// 提供模块校准表格文件的名称
        /// 依次次为iso、vsw、har
        /// </summary>
        /// <param name="tableNames"></param>
        public static void NewTables(string[] tableNames)
        {
            int tableCount = tableNames.Length;

            tables = new RL0_Table[tableCount];

            for (int i = 0; i < tableCount; i++)
                tables[i] = new RL0_Table(tableNames[i]);
        }

        public static void LoadTables()
        {
            if (tables != null)
            {
                for (int i = 0; i < tables.Length; i++)
                    tables[i].LoadSettings();
            }
        }

        public static void FreeTabels()
        {
            for (int i = 0; i < tables.Length; i++)
                tables[i] = null;
        }

        public static List<RL0_TableItem> Items(FuncModule module, RFInvolved rfid)
        {
            List<RL0_TableItem> result = null;

            if (module == FuncModule.ISO)
            {
                if (rfid == RFInvolved.Rf_1)
                    result = tables[0].RL0_Items;
                else
                    result = tables[1].RL0_Items;
            }
            else if (module == FuncModule.VSW)
            {
                if (rfid == RFInvolved.Rf_1)
                    result = tables[2].RL0_Items;
                else
                    result = tables[3].RL0_Items;
            }

            else if (module == FuncModule.HAR)
            {
                if (rfid == RFInvolved.Rf_1)
                    result = tables[4].RL0_Items;
                else
                    result = tables[5].RL0_Items;
            }

            return result;
        }
        /// <summary>
        /// 功放自动校准辅助列表
        /// </summary>
        public static Auto_CAL_Items Cal_Carrier(FuncModule module, RFInvolved rfid)
        {
            Auto_CAL_Items result = null;

            if (module == FuncModule.ISO)
            {
                if (rfid == RFInvolved.Rf_1)
                    result = tables[0].Cal_Carrier;
                else
                    result = tables[1].Cal_Carrier;
            }
            else if (module == FuncModule.VSW)
            {
                if (rfid == RFInvolved.Rf_1)
                    result = tables[2].Cal_Carrier;
                else
                    result = tables[3].Cal_Carrier;
            }

            else if (module == FuncModule.HAR)
            {
                if (rfid == RFInvolved.Rf_1)
                    result = tables[4].Cal_Carrier;
                else
                    result = tables[5].Cal_Carrier;
            }

            return result;
        }

    }

}
