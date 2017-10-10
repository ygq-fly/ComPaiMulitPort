using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    //横轴是频率、纵轴是dB值
    //[pim_signal_1]
    //tx_p=30, 33, 36, 39, 42, 45
    //tx_f=930, 932, 934, 936, 938, 940
    //tx_row_1=0.5, 0.6, -0.2, 0.8, -1.1, 0.9
    //tx_row_2=0.5, 0.6, -0.2, 0.8, -1.1, 0.9
    //tx_row_3=0.5, 0.6, -0.2, 0.8, -1.1, 0.9
    //tx_row_4=0.5, 0.6, -0.2, 0.8, -1.1, 0.9
    //tx_row_5=0.5, 0.6, -0.2, 0.8, -1.1, 0.9
    //tx_row_6=0.5, 0.6, -0.2, 0.8, -1.1, 0.9
    class Tx_Table
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        private readonly string fileName;

        private readonly string sectionName;

        private Signal_Aixs rowHead;

        private Signal_Aixs colHead;

        private float[][] table;

        private Tx_Table _table;

        internal Tx_Table(string fileName, string sectionName)
        {
            this.fileName = fileName;

            this.sectionName = sectionName;

            rowHead = new Signal_Aixs();

            colHead = new Signal_Aixs();
        }

        internal void LoadSettings()
        {
            string s1 = "";
            string s2 = "";

            IniFile.SetFileName(fileName);

            s1 = IniFile.GetString(sectionName, "tx_p", "");
            s2 = IniFile.GetString(sectionName, "tx_f", "");

            if (String.IsNullOrEmpty(s1) || String.IsNullOrEmpty(s2))
                return;
            
            //校准表的行数目
            int N = IniFile.CountOfItemIn(s1);

            //校准表的列数目
            int M = IniFile.CountOfItemIn(s2);

            if ((N <= 0) || (M <= 0))
                return;

            rowHead.Parse(s2, M);

            colHead.Parse(s1, N);

            FillTable(N, M);
        }

        internal void StoreSettings()
        {
            //
        }

        private void FillTable(int N, int M)
        {
            int i, j;
            float v = 0.0f;

            string s = "";
            string s2 = "";
            string r = "tx_row_";

            //初始化2维表格
            table = new float[N][];
            for (i = 0; i < table.Length; i++)
            {
                table[i] = new float[M];

                s = IniFile.GetString(sectionName, (r + (i+1).ToString()), "");

                for (j = 0; j < M; j++)
                {
                    s2 = IniFile.GetItemFrom(s, j, M);

                    try
                    {
                        if (s2 != "")
                            v = Convert.ToSingle(s2);
                    }
                    catch (InvalidCastException)
                    {
                        v = 0.0f;
                    }

                    table[i][j] = v;
                }
            }
        }

        //Offset将被频繁调用，故将局部变量提升为实例变量
        private Signal_AixsItem aItem1;
        private Signal_AixsItem aItem2;
        private Signal_MatchItem_Index mii_I;
        private Signal_MatchItem_Index mii_J;

        /// <summary>获取补偿
        /// 
        /// </summary>
        /// <param name="f">频率</param>
        /// <param name="p">功率</param>
        /// <param name="tx">Tx_Table</param>
        /// <returns></returns>
        internal float Offset(float f, float p,Tx_Table tx)
        {
            this._table = tx;
            float v = 0.0f;
            float v1 = 0.0f;
            float v2 = 0.0f;

            if (table == null)
                v = 0.0f;
            else
            {
                mii_I = rowHead.Search(f);
                mii_J = colHead.Search(p);

                //待补偿点，恰好落在表格节点上
                if (mii_I.only_I && mii_J.only_I)
                    v = table[mii_J.I][mii_I.I] + _table.Offset1(p);

                //带补偿点，在横轴（频率f）上落在表格节点上，在纵轴(功率dB)上没有落在表格节点上
                else if (mii_I.only_I && (!mii_J.only_I))
                {
                    if (mii_J.I1 != mii_J.I)
                        v = Offset_dB(p, mii_I.I, mii_J.I1, mii_J.I);
                    else
                        v = Offset_dB(p, mii_I.I, mii_J.I, mii_J.I2);

                    //带补偿点，在纵轴(功率dB)上落在表格节点上，在横轴（频率f）上没有落在表格节点上
                }
                else if ((!mii_I.only_I) && mii_J.only_I)
                {
                    if (mii_I.I1 != mii_I.I)
                        v = Offset_F(f, mii_J.I, mii_I.I1, mii_I.I, p);
                    else
                        v = Offset_F(f, mii_J.I, mii_I.I, mii_I.I2, p);

                    //带补偿点，在两个轴向上都没有落在表格节点上
                }
                else
                {
                    int I_F1 = 0;
                    int I_F2 = 0;

                    //先固定f，进行p变换
                    if (mii_I.I1 != mii_I.I)
                    {
                        I_F1 = mii_I.I1;
                        I_F2 = mii_I.I;

                        if (mii_J.I1 != mii_J.I)
                        {
                            v1 = Offset_dB(p, I_F1, mii_J.I1, mii_J.I);
                            v2 = Offset_dB(p, I_F2, mii_J.I1, mii_J.I);
                        }
                        else
                        {
                            v1 = Offset_dB(p, I_F1, mii_J.I, mii_J.I2);
                            v2 = Offset_dB(p, I_F2, mii_J.I, mii_J.I2);
                        }

                    }
                    else
                    {
                        I_F1 = mii_I.I;
                        I_F2 = mii_I.I2;

                        if (mii_J.I1 != mii_J.I)
                        {
                            v1 = Offset_dB(p, I_F1, mii_J.I1, mii_J.I);
                            v2 = Offset_dB(p, I_F2, mii_J.I1, mii_J.I);
                        }
                        else
                        {
                            v1 = Offset_dB(p, I_F1, mii_J.I, mii_J.I2);
                            v2 = Offset_dB(p, I_F2, mii_J.I, mii_J.I2);
                        }
                    }

                    //再进行f变换 
                    aItem1 = rowHead.GetItem(I_F1);
                    aItem2 = rowHead.GetItem(I_F2);

                    v = Offset_Linear(f, aItem1.v, v1, aItem2.v, v2);
                }
            }

            return v + App_Settings.spc.TxRef;
        }

        /// <summary>
        /// 用来获取相同功率不同频率要增加的值
        /// </summary>
        internal float Offset1(float p)
        {
            float v = 0.0f;
            float v1 = 0.0f;
            float v2 = 0.0f;

            if (table == null)
                v = 0.0f;
            else
            {
                //mii_I = rowHead.Search(f);
                mii_I = new Signal_MatchItem_Index();
                mii_I.I = 0;

                mii_J = colHead.Search(p);
                //mii_I.only_I &&
                //待补偿点，恰好落在表格节点上
                if (mii_J.only_I)
                    v = table[mii_J.I][mii_I.I];
                #region
                ////带补偿点，在横轴（频率f）上落在表格节点上，在纵轴(功率dB)上没有落在表格节点上 
                //else if (!mii_J.only_I)
                //{
                //    if (mii_J.I1 != mii_J.I)
                //        v = Offset_dB(p, mii_I.I, mii_J.I1, mii_J.I);
                //    else
                //        v = Offset_dB(p, mii_I.I, mii_J.I, mii_J.I2);

                //    //带补偿点，在纵轴(功率dB)上落在表格节点上，在横轴（频率f）上没有落在表格节点上
                //}
                //else if ((!mii_I.only_I) && mii_J.only_I)
                //{
                //    if (mii_I.I1 != mii_I.I)
                //        v = Offset_F(f, mii_J.I, mii_I.I1, mii_I.I);
                //    else
                //        v = Offset_F(f, mii_J.I, mii_I.I, mii_I.I2);

                //    //带补偿点，在两个轴向上都没有落在表格节点上
                //}
                //else
                //{
                //    int I_F1 = 0;
                //    int I_F2 = 0;

                //    //先固定f，进行p变换
                //    if (mii_I.I1 != mii_I.I)
                //    {
                //        I_F1 = mii_I.I1;
                //        I_F2 = mii_I.I;

                //        if (mii_J.I1 != mii_J.I)
                //        {
                //            v1 = Offset_dB(p, I_F1, mii_J.I1, mii_J.I);
                //            v2 = Offset_dB(p, I_F2, mii_J.I1, mii_J.I);
                //        }
                //        else
                //        {
                //            v1 = Offset_dB(p, I_F1, mii_J.I, mii_J.I2);
                //            v2 = Offset_dB(p, I_F2, mii_J.I, mii_J.I2);
                //        }

                //    }
                //    else
                //    {
                //        I_F1 = mii_I.I;
                //        I_F2 = mii_I.I2;

                //        if (mii_J.I1 != mii_J.I)
                //        {
                //            v1 = Offset_dB(p, I_F1, mii_J.I1, mii_J.I);
                //            v2 = Offset_dB(p, I_F2, mii_J.I1, mii_J.I);
                //        }
                //        else
                //        {
                //            v1 = Offset_dB(p, I_F1, mii_J.I, mii_J.I2);
                //            v2 = Offset_dB(p, I_F2, mii_J.I, mii_J.I2);
                //        }
                //    }

                //    //再进行f变换 
                //    aItem1 = rowHead.GetItem(I_F1);
                //    aItem2 = rowHead.GetItem(I_F2);

                //    v = Offset_Linear(f, aItem1.v, v1, aItem2.v, v2);
                //}
                #endregion
            }
            return v;
        }

        private float Offset_dB(float p, int J, int I1, int I2)
        {
            try
            {
                float v1 = table[I1][J];
                float v2 = table[I2][J];

                aItem1 = colHead.GetItem(I1);
                aItem2 = colHead.GetItem(I2);

                v1 = v1 + _table.Offset1(aItem1.v);
                v2 = v2 + _table.Offset1(aItem2.v);
                return Offset_Linear(p, aItem1.v, v1, aItem2.v, v2);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.ToString());
                return 0f;
            }
            
        }
        
        private float Offset_F(float f, int I, int J1, int J2, float p)
        {
            float v1 = table[I][J1];
            float v2 = table[I][J2];

            aItem1 = rowHead.GetItem(J1);
            aItem2 = rowHead.GetItem(J2);

            v1 = v1 + _table.Offset1(p);
            v2 = v2 + _table.Offset1(p);

            return Offset_Linear(f, aItem1.v, v1, aItem2.v, v2);
        }

        private float Offset_Linear(float x, float x1, float y1, float x2, float y2)
        {            
            float a = (y2 - y1) / (x2 - x1);

            float b = (y1 * x2 - y2 * x1) / (x2 - x1);

            return (a * x + b);            
        }

        ///
        ///内部类，仅在类内可见，不应该被外部引用到
        ///
        private class Signal_Aixs
        {
            private List<Signal_AixsItem> tItems;

            internal Signal_AixsItem GetItem(int i)
            {
                if ((i >= 0) && (i < tItems.Count))
                    return tItems[i];
                else
                    return null;
            }

            internal Signal_Aixs()
            {
                tItems = new List<Signal_AixsItem>();
            }

            internal void Parse(string values, int colCount)
            {
                string s = "";

                Signal_AixsItem tItem;

                for (int i = 0; i < colCount; i++)
                {
                    s = IniFile.GetItemFrom(values, i, colCount);

                    try
                    {
                        tItem = new Signal_AixsItem();

                        tItem.v = Convert.ToSingle(s);
                        
                        tItems.Add(tItem);
                    }
                    catch (System.InvalidCastException)
                    {
                    }
                }

                Processing();
            }

            private void Processing()
            {
                //补偿项为空
                if (tItems.Count <= 0)
                    return;

                //补偿项非空，为加快搜索速度，建立类似索引
                else
                {

                    if (tItems.Count == 1)
                    {
                        tItems[0].v1 = tItems[0].v;
                        tItems[0].v2 = tItems[0].v;

                    }
                    else if (tItems.Count >= 2)
                    {
                        int k = tItems.Count - 1;

                        tItems[0].v1 = tItems[0].v;
                        tItems[0].v2 = tItems[1].v;

                        tItems[k].v1 = tItems[k - 1].v;
                        tItems[k].v2 = tItems[k].v;

                        for (int i = 1; i < k; i++)
                        {
                            tItems[i].v1 = tItems[i - 1].v;
                            tItems[i].v2 = tItems[i + 1].v;
                        }
                    }
                }
            }
            internal Signal_MatchItem_Index Search(float v)
            {
                int i = 0;

                Signal_MatchItem_Index mii = new Signal_MatchItem_Index();

                if (tItems.Count <= 0)
                    return mii;

                //待补偿的值，恰好落在补偿点上    
                for (i = 0; i < tItems.Count; i++)
                {
                    //与补偿点频率相差值，不大于1KHz
                    if (Math.Abs(tItems[i].v - v) <= 0.0001f)
                    {
                        mii.I = i;
                        mii.only_I = true;

                        break;
                    }
                }

                if (mii.I < 0)
                {
                    //待补偿的值，在第一个补偿点之前
                    if ((tItems[0].v - v) > 0.0001f)
                    {
                        mii.I = 0;
                        mii.only_I = true;

                        //待补偿的值，在最后一个补偿点之后
                    }
                    else if ((v - tItems[tItems.Count - 1].v) > 0.0001f)
                    {
                        mii.I = tItems.Count - 1;
                        mii.only_I = true;

                        //待补偿的值，落在2补偿点构成的区间内
                    }
                    else
                    {
                        bool found = true;

                        for (i = 1; i <= tItems.Count - 1; i++)
                        {
                            //在前一个区间
                            if (((v - tItems[i].v1) > 0.0001f) && ((tItems[i].v - v) > 0.0001f))
                            {
                                mii.I1 = i - 1;
                                mii.I = i;
                                mii.I2 = i;

                                //在后一个区间
                            }
                            else if (((v - tItems[i].v) > 0.0001f) && ((tItems[i].v2 - v) > 0.0001f))
                            {
                                mii.I1 = i;
                                mii.I = i;
                                mii.I2 = i + 1;

                            }
                            else
                                found = false;

                            if (found)
                                break;
                        }
                    }
                }

                return mii;
            }
        }

        private class Signal_AixsItem
        {
            internal float v1;
            internal float v;
            internal float v2;
        }

        private class Signal_MatchItem_Index
        {
            internal int I1 = -1;
            internal int I = -1;
            internal int I2 = -1;
            internal bool only_I;
        }

    }
    
    class Tx_Tables
    {
        private Tx_Tables()
        {
            //
        }

        //----------------------------------------

        //反向
        static internal Tx_Table pim_rev_tx1;
        static internal Tx_Table pim_rev_tx1disp;
        static internal Tx_Table pim_rev_offset1;
        static internal Tx_Table pim_rev_offset2;

        static internal Tx_Table pim_rev_tx2;
        static internal Tx_Table pim_rev_tx2disp;
        static internal Tx_Table pim_rev_offset1_disp;
        static internal Tx_Table pim_rev_offset2_disp;

        //ygq
        static internal Tx_Table pim_rev2_tx1;
        static internal Tx_Table pim_rev2_tx1disp;
        static internal Tx_Table pim_rev2_offset1;
        static internal Tx_Table pim_rev2_offset2;

        static internal Tx_Table pim_rev2_tx2;
        static internal Tx_Table pim_rev2_tx2disp;
        static internal Tx_Table pim_rev2_offset1_disp;
        static internal Tx_Table pim_rev2_offset2_disp;
        //ygq
        //前向
        //static internal Tx_Table pim_frd_tx1;
        //static internal Tx_Table pim_frd_tx1disp;
        //static internal Tx_Table pim_frd_offset1;
        //static internal Tx_Table pim_frd_offset2;

        //static internal Tx_Table pim_frd_tx2;
        //static internal Tx_Table pim_frd_tx2disp;
        //static internal Tx_Table pim_frd_offset1_disp;
        //static internal Tx_Table pim_frd_offset2_disp;
        //----------------------------------------

        static internal Tx_Table iso_tx1;
        static internal Tx_Table iso_tx1_disp;
        static internal Tx_Table iso_offset1;
        static internal Tx_Table iso_offset2;

        static internal Tx_Table iso_tx2;
        static internal Tx_Table iso_tx2_disp;
        static internal Tx_Table iso_offset1_disp;
        static internal Tx_Table iso_offset2_disp;

        static internal Tx_Table vsw_tx1;
        static internal Tx_Table vsw_tx1_disp;
        static internal Tx_Table vsw_offset1;
        static internal Tx_Table vsw_offset2;

        static internal Tx_Table vsw_tx2;
        static internal Tx_Table vsw_tx2_disp;
        static internal Tx_Table vsw_offset1_disp;
        static internal Tx_Table vsw_offset2_disp;

        static internal Tx_Table har_tx1;
        static internal Tx_Table har_tx1_disp;
        static internal Tx_Table har_offset1;
        static internal Tx_Table har_offset2;

        static internal Tx_Table har_tx2;
        static internal Tx_Table har_tx2_disp;
        static internal Tx_Table har_offset1_disp;
        static internal Tx_Table har_offset2_disp;

     
        static internal void NewTables(string txFileNameRev, string txFileName_dispRev)
        {
            //反向
            pim_rev_tx1 = new Tx_Table(txFileNameRev, "pim_signal_1");
            pim_rev_tx1disp = new Tx_Table(txFileName_dispRev, "pim_signal_1");
            pim_rev_offset1 = new Tx_Table(txFileNameRev, "pim_offset_1");
            pim_rev_offset2 = new Tx_Table(txFileNameRev, "pim_offset_2");

            pim_rev_tx2 = new Tx_Table(txFileNameRev, "pim_signal_2");
            pim_rev_tx2disp = new Tx_Table(txFileName_dispRev, "pim_signal_2");
            pim_rev_offset1_disp = new Tx_Table(txFileName_dispRev, "pim_offset_1");
            pim_rev_offset2_disp = new Tx_Table(txFileName_dispRev, "pim_offset_2");

            //----------------------------------------
            //前向
            //pim_frd_tx1 = new Tx_Table(txFileNameFrd, "pim_signal_1");
            //pim_frd_tx1disp = new Tx_Table(txFileName_dispFrd, "pim_signal_1");
            //pim_frd_offset1 = new Tx_Table(txFileNameFrd, "pim_offset_1");
            //pim_frd_offset2 = new Tx_Table(txFileNameFrd, "pim_offset_2");

            //pim_frd_tx2 = new Tx_Table(txFileNameFrd, "pim_signal_2");
            //pim_frd_tx2disp = new Tx_Table(txFileName_dispFrd, "pim_signal_2");
            //pim_frd_offset1_disp = new Tx_Table(txFileName_dispFrd, "pim_offset_1");
            //pim_frd_offset2_disp = new Tx_Table(txFileName_dispFrd, "pim_offset_2");
            //----------------------------------------

            //iso_tx1 = new Tx_Table(txFileNameRev, "iso_signal_1");
            //iso_tx1_disp = new Tx_Table(txFileName_dispRev, "iso_signal_1");
            //iso_offset1 = new Tx_Table(txFileNameRev, "iso_offset_1");
            //iso_offset2 = new Tx_Table(txFileNameRev, "iso_offset_2");

            //iso_tx2 = new Tx_Table(txFileNameRev, "iso_signal_2");
            //iso_tx2_disp = new Tx_Table(txFileName_dispRev, "iso_signal_2");
            //iso_offset1_disp = new Tx_Table(txFileName_dispRev, "iso_offset_1");
            //iso_offset2_disp = new Tx_Table(txFileName_dispRev, "iso_offset_2");

            //vsw_tx1 = new Tx_Table(txFileNameRev, "vsw_signal_1");
            //vsw_tx1_disp = new Tx_Table(txFileName_dispRev, "vsw_signal_1");
            //vsw_offset1 = new Tx_Table(txFileNameRev, "vsw_offset_1");
            //vsw_offset2 = new Tx_Table(txFileNameRev, "vsw_offset_2");

            //vsw_tx2 = new Tx_Table(txFileNameRev, "vsw_signal_2");
            //vsw_tx2_disp = new Tx_Table(txFileName_dispRev, "vsw_signal_2");
            //vsw_offset1_disp = new Tx_Table(txFileName_dispRev, "vsw_offset_1");
            //vsw_offset2_disp = new Tx_Table(txFileName_dispRev, "vsw_offset_2");

            //har_tx1 = new Tx_Table(txFileNameRev, "har_signal_1");
            //har_tx1_disp = new Tx_Table(txFileName_dispRev, "har_signal_1");
            //har_offset1 = new Tx_Table(txFileNameRev, "har_offset_1");
            //har_offset2 = new Tx_Table(txFileNameRev, "har_offset_2");

            //har_tx2 = new Tx_Table(txFileNameRev, "har_signal_2");
            //har_tx2_disp = new Tx_Table(txFileName_dispRev, "har_signal_2");
            //har_offset1_disp = new Tx_Table(txFileName_dispRev, "har_offset_1");
            //har_offset2_disp = new Tx_Table(txFileName_dispRev, "har_offset_2");
        }

        static internal void NewTables(string txFileNameRev, string txFileName_dispRev, string txFileNameRev2, string txFileName_dispRev2)
        {
            //反向
            pim_rev_tx1 = new Tx_Table(txFileNameRev, "pim_signal_1");
            pim_rev_tx1disp = new Tx_Table(txFileName_dispRev, "pim_signal_1");
            pim_rev_offset1 = new Tx_Table(txFileNameRev, "pim_offset_1");
            pim_rev_offset2 = new Tx_Table(txFileNameRev, "pim_offset_2");

            pim_rev_tx2 = new Tx_Table(txFileNameRev, "pim_signal_2");
            pim_rev_tx2disp = new Tx_Table(txFileName_dispRev, "pim_signal_2");
            pim_rev_offset1_disp = new Tx_Table(txFileName_dispRev, "pim_offset_1");
            pim_rev_offset2_disp = new Tx_Table(txFileName_dispRev, "pim_offset_2");

            //反向
            pim_rev2_tx1 = new Tx_Table(txFileNameRev2, "pim_signal_1");
            pim_rev2_tx1disp = new Tx_Table(txFileName_dispRev2, "pim_signal_1");
            pim_rev2_offset1 = new Tx_Table(txFileNameRev2, "pim_offset_1");
            pim_rev2_offset2 = new Tx_Table(txFileNameRev2, "pim_offset_2");

            pim_rev2_tx2 = new Tx_Table(txFileNameRev2, "pim_signal_2");
            pim_rev2_tx2disp = new Tx_Table(txFileName_dispRev2, "pim_signal_2");
            pim_rev2_offset1_disp = new Tx_Table(txFileName_dispRev2, "pim_offset_1");
            pim_rev2_offset2_disp = new Tx_Table(txFileName_dispRev2, "pim_offset_2");
        }

        static internal Tx_Table[] LoadTables_ygq()
        {
            //加载互调模块的补偿系数

            //----------------------------------------
            //反向
            pim_rev_tx1.LoadSettings();
            pim_rev_tx2.LoadSettings();
            pim_rev_offset1.LoadSettings();
            pim_rev_offset2.LoadSettings();

            pim_rev_tx1disp.LoadSettings();
            pim_rev_tx2disp.LoadSettings();
            pim_rev_offset1_disp.LoadSettings();
            pim_rev_offset2_disp.LoadSettings();
             Tx_Table[] tt = new Tx_Table[] { pim_rev_tx1, pim_rev_tx1disp, pim_rev_offset1, pim_rev_offset2,
                                            pim_rev_tx2, pim_rev_tx2disp,pim_rev_offset1_disp,pim_rev_offset2_disp};
            return tt;
            
        }

        static internal void LoadTables_ygq(Tx_Table[] tt)
        {
            pim_rev_tx1 = tt[0];
            pim_rev_tx1disp = tt[1];
            pim_rev_offset1 = tt[2];
            pim_rev_offset2 = tt[3];
            pim_rev_tx2 = tt[4];
            pim_rev_tx2disp = tt[5];
            pim_rev_offset1_disp = tt[6];
            pim_rev_offset2_disp = tt[7];
        }



        /// <summary>
        /// 加载发信补偿表格
        /// </summary>
        static internal void LoadTables2()
        {
            //加载互调模块的补偿系数

            //----------------------------------------
            //反向
            pim_rev_tx1.LoadSettings();
            pim_rev_tx2.LoadSettings();
            pim_rev_offset1.LoadSettings();
            pim_rev_offset2.LoadSettings();

            pim_rev_tx1disp.LoadSettings();
            pim_rev_tx2disp.LoadSettings();
            pim_rev_offset1_disp.LoadSettings();
            pim_rev_offset2_disp.LoadSettings();

         
        }

        /// <summary>
        /// 加载发信补偿表格
        /// </summary>
        static internal void LoadTables()
        {
            //加载互调模块的补偿系数

            //----------------------------------------
            //反向
            pim_rev_tx1.LoadSettings();
            pim_rev_tx2.LoadSettings();
            pim_rev_offset1.LoadSettings();
            pim_rev_offset2.LoadSettings();

            pim_rev_tx1disp.LoadSettings();
            pim_rev_tx2disp.LoadSettings();
            pim_rev_offset1_disp.LoadSettings();
            pim_rev_offset2_disp.LoadSettings();

            pim_rev2_tx1.LoadSettings();
            pim_rev2_tx2.LoadSettings();
            pim_rev2_offset1.LoadSettings();
            pim_rev2_offset2.LoadSettings();

            pim_rev2_tx1disp.LoadSettings();
            pim_rev2_tx2disp.LoadSettings();
            pim_rev2_offset1_disp.LoadSettings();
            pim_rev2_offset2_disp.LoadSettings();

            //前向
            //pim_frd_tx1.LoadSettings();
            //pim_frd_tx2.LoadSettings();
            //pim_frd_offset1.LoadSettings();
            //pim_frd_offset2.LoadSettings();

            //pim_frd_tx1disp.LoadSettings();
            //pim_frd_tx2disp.LoadSettings();
            //pim_frd_offset1_disp.LoadSettings();
            //pim_frd_offset2_disp.LoadSettings();

            //----------------------------------------

            //加载隔离度模块的补偿系数
            //iso_tx1.LoadSettings();
            //iso_tx2.LoadSettings();
            //iso_offset1.LoadSettings();
            //iso_offset2.LoadSettings();

            //iso_tx1_disp.LoadSettings();
            //iso_tx2_disp.LoadSettings();
            //iso_offset1_disp.LoadSettings();
            //iso_offset2_disp.LoadSettings();


            ////加载驻波比模块的补偿系数
            //vsw_tx1.LoadSettings();
            //vsw_tx2.LoadSettings();
            //vsw_offset1.LoadSettings();
            //vsw_offset2.LoadSettings();


            //vsw_tx1_disp.LoadSettings();
            //vsw_tx2_disp.LoadSettings();
            //vsw_offset1_disp.LoadSettings();
            //vsw_offset2_disp.LoadSettings();

            ////加载谐波模块的补偿系数
            //har_tx1.LoadSettings();
            //har_tx2.LoadSettings();
            //har_offset1.LoadSettings();
            //har_offset2.LoadSettings();

            //har_tx1_disp.LoadSettings();
            //har_tx2_disp.LoadSettings();
            //har_offset1_disp.LoadSettings();
            //har_offset2_disp.LoadSettings();
        }
    }

}