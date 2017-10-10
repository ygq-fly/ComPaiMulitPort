using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{

     /// <summary>
    /// �Զ�У׼���������Ƶ�ʣ��ز����ֵ������ֵ������ֵ
    /// Ƶ�ʣ��ز����ֵ�ӱ���ļ����أ�
    /// ����ֵ������ֵ��У׼������ʵ�ʲ���ֵ�������
    /// �������ڸ���Ȳ��Ժ�פ���Ȳ��Ե�У׼����
    /// </summary>
    class Auto_CAL_Item
    {
        /// <summary>
        /// ���ŵ�����Ƶ��
        /// </summary>
        internal float F0;

        /// <summary>
        /// ��У׼����ļ�����
        /// </summary>
        internal float RL0;

        /// <summary>
        /// ���ŵ��������
        /// </summary>
        internal float Tx0;

        /// <summary>
        /// ��F���ϵ�����ֵ����Ƶ�׵ķ���ֵ
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
    /// �Զ�У׼�����������б�����Ƶ�ʣ��ز����ֵ
    /// �������ڸ���Ȳ��Ժ�פ���Ȳ��Ե�У׼����
    /// ��У׼����ʹ��ʵ�ʲ���ֵ����б�Ĺ���ֵ������ֵ
    /// </summary>
    class Auto_CAL_Items
    {
        /// <summary>
        /// ����Զ�У׼�����б��ԭʼ��
        /// </summary>
        private Auto_CAL_Item[] items;

        /// <summary>
        /// ʹ��У׼�ļ��������Զ�У׼�����б�
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
        /// ���ص�i���б���
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
        /// ��Ƶ��fΪ���������Զ�У׼�����б�����֮��ӽ����б���
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

            //���Ҳ������򷵻ش������ľ�����СF0���б���
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
        /// �����б�ĳ���
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
    /// У׼�ļ������Ƶ�ʣ��ز����ֵ 
    /// </summary>
    class RL0_TableItem
    {
        internal float F;

        internal float RL;
    }


    class RL0_Table
    {
        /// <summary>
        /// У׼���ļ�������
        /// </summary>
        private string fileName;

        private List<RL0_TableItem> tItems;

        //���ŵ��Զ�У׼�����б�
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

            //���ز�����
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

            //�����Զ�У׼�����б�
            cal_items = new Auto_CAL_Items(tItems);
        }

        /// <summary>
        /// �Զ�У׼ԭʼRL�����б��ɵ�����Ա����
        /// </summary>
        internal List<RL0_TableItem> RL0_Items
        {
            get { return tItems; }
        }


        /// <summary>
        /// ���ŵ��Զ�У׼�����б�
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
        /// �ṩģ��У׼����ļ�������
        /// ���δ�Ϊiso��vsw��har
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
        /// �����Զ�У׼�����б�
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
