using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    ///û�в�����Vi=0
    ///��Fi�ϣ�����ΪVi
    ///��Fi��Fi+1֮�䣬��������Ϊ�������䣬�򲹳�ΪVi + (F-Fi)*(Vi+1 - Vi)/(Fi+1 - Fi)
    ///��Fi��Fi+1֮�䣬��������Ϊ�½����䣬�򲹳�ΪVi+1 + (F-Fi)*(Vi - Vi+)/(Fi+1 - Fi)
    ///�ڵ�һ��������֮ǰ�����һ��������ͬ
    ///�����һ��������֮�������һ��������ͬ    

    class Spectrum_TableItem
    {
        internal float f1, v1;
        internal float f, v;
        internal float f2, v2;
    }
   
    class Spectrum_Table
    {
        /// <summary>
        /// У׼���ļ�������
        /// </summary>
        public string fileName;
        
        private Spectrum_TableItem tItem;

        private List<Spectrum_TableItem> tItems;

        internal Spectrum_Table(string fileName)
        {
            this.fileName = fileName;
        }

        internal void LoadSettings()
        {
            string s = "";
            string s1 = "";
            string s2 = "";
            
            tItems = new List<Spectrum_TableItem>();

            tItem = new Spectrum_TableItem();

            System.IO.StreamReader textReader = new System.IO.StreamReader(fileName);

            Spectrum_TableItem tItem_temp;

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
                    tItem_temp = new Spectrum_TableItem();

                    tItem_temp.f = Convert.ToSingle(s1);
                    tItem_temp.v = Convert.ToSingle(s2);
                    tItems.Add(tItem_temp);

                } catch (System.InvalidCastException)
                {                   
                }
            }

            textReader.Close();

            textReader.Dispose();

            Processing();

            if (tItems.Count > 0) 
            {
                tItem.f1 = tItems[0].f1;
                tItem.v1 = tItems[0].v1;
                tItem.f = tItems[0].f;
                tItem.v = tItems[0].v;
                tItem.f2 = tItems[0].f2;
                tItem.v2 = tItems[0].v2;
            } 
            else 
            {
                tItem.f1 = float.MaxValue;
                tItem.v1 = float.MinValue;
                tItem.f = float.MaxValue;
                tItem.v = float.MinValue;
                tItem.f2 = float.MaxValue;
                tItem.v2 = float.MinValue;
            }

        }

       
        private void Processing()
        {
            //������Ϊ��
            if (tItems.Count <= 0)
                return;

            //������ǿգ�Ϊ�ӿ������ٶȣ�������������
            else
            {
                if (tItems.Count == 1) 
                {
                    tItems[0].f1 = 0.0f;
                    tItems[0].v1 = tItems[0].v;

                    tItems[0].f2 = 3000f;
                    tItems[0].v2 = tItems[0].v;

                } 
                else if (tItems.Count >= 2)
                {

                    int k = tItems.Count - 1;

                    tItems[0].f1 = 0.0f;
                    tItems[0].v1 = tItems[0].v;

                    tItems[0].f2 = tItems[1].f;
                    tItems[0].v2 = tItems[1].v;

                    tItems[k].f1 = tItems[k - 1].f;
                    tItems[k].v1 = tItems[k - 1].v;

                    tItems[k].f2 = 3000f;
                    tItems[k].v2 = tItems[k].v;

                    for (int i = 1; i < k; i++)
                    {
                        tItems[i].f1 = tItems[i - 1].f;
                        tItems[i].v1 = tItems[i - 1].v;

                        tItems[i].f2 = tItems[i + 1].f;
                        tItems[i].v2 = tItems[i + 1].v;
                    }
                }
            }
        }

        //Offset����Ƶ�����ã��ʽ��ֲ���������Ϊʵ������
        private int   fK = -1;
        private float fV;
        private bool  fMatch;
        internal float Offset(float f)
        {
       
            //û�в�����
            if (tItems.Count <= 0)
                fV = 0.0f;

            //������Ƶ�ʣ��ڵ�һ��������֮ǰ
            else if ((tItems[0].f - f) > 0.0001f)
                fV = tItems[0].v;

            //������Ƶ�ʣ������һ��������֮��
            else if ( ((f - tItems[tItems.Count-1].f) > 0.0001f) ||                      
                      (Math.Abs(f - tItems[tItems.Count - 1].f) < 0.0001f)) 

                fV = tItems[tItems.Count-1].v;

            //������Ƶ�ʣ��ڲ����㷶Χ֮��
            else {
                fMatch = Offset_Item(tItem, f, out fV);

                if (!fMatch)
                {
                    fK = Search(f);

                    if (fK < 0)
                        fV = 0.0f;

                    else    {
                        //���»������
                        tItem.f1 = tItems[fK].f1;
                        tItem.v1 = tItems[fK].v1;
                        tItem.f = tItems[fK].f;
                        tItem.v = tItems[fK].v;
                        tItem.f2 = tItems[fK].f2;
                        tItem.v2 = tItems[fK].v2;

                        Offset_Item(tItem, f, out fV);
                    }
                }
            }

            return fV;
        }

        //Ѱ�Ұ���f������,���������Ӧ�Ľڵ�����
        private int Search(float f)
        {           
            int k = -1;
            for (int i = 0; i < tItems.Count; i++)
            {
                if (((f - tItems[i].f1) > 0.0001f) &&
                    ((tItems[i].f2 - f) > 0.0001f))
                {
                    k = i;
                    break;
                }
            }

            return k;
        }

        private bool Offset_Item(Spectrum_TableItem item, float f, out float v)
        {
            bool b = true;

            //Ƶ�����ڽڵ���
            if (Math.Abs(f - item.f) < 0.0001f)
                v = item.v;

            //Ƶ�����ڽڵ��ǰһ������
            else if (((f - item.f1) > 0.0001f) &&
                     ((item.f - f) > 0.0001f))
                v = Offset_Linear(item, f, 0);

            //Ƶ�����ڽڵ��һ������
            else if (((f - item.f) > 0.0001f) &&
                     ((item.f2 - f) > 0.0001f))
                v = Offset_Linear(item, f, 1);

            else {
                v = 0.0f;
                b = false;
            }

            return b;
        }

        private float Offset_Linear(Spectrum_TableItem item, float f, int section)
        {
            
            float a = (item.v2 - item.v) / (item.f2 - item.f);

            float b = (item.v * item.f2 - item.v2 * item.f) / (item.f2 - item.f);

            return (a * f + b);    
        }       
        
    }

    class Spectrum_Tables
    {
        static private Spectrum_Table[] tables;
       
        private Spectrum_Tables()
        {
            //
        }

        /// <summary>
        /// �ṩƵ��У׼�ļ�������
        /// һ��ΪN_4KHz��N_20KHz��N_100KHz��W_4KHz��W_20KHz��W_100KHz
        /// </summary>
        /// <param name="tableNames"></param>
        public static void NewTables(string[] tableNames)
        {
            int tableCount = tableNames.Length;

            tables = new Spectrum_Table[tableCount];          

            for (int i = 0; i < tableCount; i++)
            {
                tables[i] = new Spectrum_Table(tableNames[i]);

                tables[i].LoadSettings();
            }
        }

        public static void FreeTabels()
        {
            for (int i = 0; i < tables.Length; i++)
                tables[i] = null;
        }

        public static float Offset(float f, RxChannel channel, RBW rbw)
        {
            float v = 0.0f;

            if (channel == RxChannel.NarrowBand)
            {
                if (rbw == RBW.rbw4KHz)
                    v = tables[0].Offset(f);

                else if (rbw == RBW.rbw20KHz)
                    v = tables[1].Offset(f);

                else if (rbw == RBW.rbw100KHz)
                    v = tables[2].Offset(f);

                else if (rbw == RBW.rbw1000KHz)
                    v = tables[3].Offset(f);

            }
            else
            {
                if (rbw == RBW.rbw4KHz)
                    v = tables[4].Offset(f);

                else if (rbw == RBW.rbw20KHz)
                    v = tables[5].Offset(f);

                else if (rbw == RBW.rbw100KHz)
                    v = tables[6].Offset(f);

                else if (rbw == RBW.rbw1000KHz)
                    v = tables[7].Offset(f);
            }

            return v;
        }
    }

}