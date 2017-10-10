using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public class PimServices
    {

        #region 获取配置名称
        /// <summary>
        /// 获取配置名称
        /// </summary>
        /// <param name="gsm"></param>
        /// <param name="mode">REV，FWD</param>
        /// <param name="imOder">阶数</param>
        /// <param name="cate">fixed,sweep</param>
        public string GetName(string gsm, string mode, int imOder, string cate)
        {
            if (gsm.ToLower().Equals("gsm"))
            {
                gsm = "m01";
            }
            if (mode.ToLower().Equals("rev"))
            {
                mode = "c00";
            }
            else if (mode.ToLower().Equals("fwd"))
            {
                mode = "c01";
            }
            if (cate.ToLower().Equals("fixed"))
            {
                cate = "s00";
            }
            else if (cate.ToLower().Equals("sweep"))
            {
                cate = "s01";
            }
            return gsm + "_" + mode + "_i0" + imOder.ToString() + "_" + cate;

        }
        #endregion

        #region 获取指定的时间扫描数据
        ///<summary>
        ///获取指定的时间扫描数据
        ///</summary>
        internal TimeSweepParam GetTimeSweepParam(Settings_Pim settings,
                                                        string filePath,
                                                             string gsm,
                                                            string mode,
                                                            int imOrder,
                                                            string cate,
                                                            float num)
        {

            TimeSweepParam tsp = new TimeSweepParam();
            IniFile.SetFileName(filePath);
            string name = GetName(gsm, mode, imOrder, cate);
            tsp.F1 = float.Parse(IniFile.GetString(name, "tx1", "935"));
            tsp.F2 = float.Parse(IniFile.GetString(name, "tx2", "960"));
            tsp.Rx = float.Parse(IniFile.GetString(name, "rx", "910"));
            if (App_Configure.Cnfgs.Cal_Use_Table)
            {
                //----------------------------------------

                tsp.P1 = (float)settings.Tx + Tx_Tables.pim_rev_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_rev_offset1);
                tsp.P2 = (float)settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(tsp.F2, settings.Tx2, Tx_Tables.pim_rev_offset2);

                //if (settings.PimSchema == ImSchema.REV)
                //{
                //    tsp.P1 = (float)settings.Tx + Tx_Tables.pim_rev_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_rev_offset1);
                //    tsp.P2 = (float)settings.Tx + Tx_Tables.pim_rev_tx2.Offset(tsp.F2, settings.Tx, Tx_Tables.pim_rev_offset2);
                //}
                //else
                //{
                //    tsp.P1 = (float)settings.Tx + Tx_Tables.pim_frd_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_frd_offset1);
                //    tsp.P2 = (float)settings.Tx + Tx_Tables.pim_frd_tx2.Offset(tsp.F2, settings.Tx, Tx_Tables.pim_frd_offset2);
                //}
                //----------------------------------------
            }
            else
            {
                tsp.P1 = (float)App_Factors.pim_tx1.ValueWithOffset(tsp.F1, settings.Tx);
                tsp.P2 = (float)App_Factors.pim_tx2.ValueWithOffset(tsp.F2, settings.Tx2);
            }
            tsp.N1 = num;
            return tsp;

        }
        #endregion

        #region 获取指定的序列频率扫描数据
        /// <summary>
        /// 获取指定的序列频率扫描数据
        /// </summary>
        /// <param name="filePath">读取的文件地址</param>
        /// <param name="gsm"></param>
        /// <param name="mode">REV，FWD</param>
        /// <param name="imOder">阶数</param>
        /// <param name="cate">fixed,sweep</param>
        internal FreqSweepParam GetFreqSweepParam(Settings_Pim settings,
                                                        string filePath, 
                                                             string gsm,
                                                            string mode,
                                                            string cate,
                                                           int imOrder)
        {
            IniFile.SetFileName(filePath);
            string name = GetName(gsm, mode, imOrder, cate);
            FreqSweepParam fsp = new FreqSweepParam();
            FreqSweepItem[] sweepItem1 = null;
            FreqSweepItem[] sweepItem2 = null;
            string tx1_1 = IniFile.GetString(name, "tx1_1", "935");
            string tx2_1 = IniFile.GetString(name, "tx2_1", "960");
            string rx_1 = IniFile.GetString(name, "rx_1", "910");

            string tx1_2 = IniFile.GetString(name, "tx1_2", "935");
            string tx2_2 = IniFile.GetString(name, "tx2_2", "960");
            string rx_2 = IniFile.GetString(name, "rx_2", "910");

            string[] strTx1_1 = tx1_1.Split(char.Parse(","));
            string[] strTx2_1 = tx2_1.Split(char.Parse(","));
            string[] strRx_1 = rx_1.Split(char.Parse(","));

            string[] strTx1_2 = tx1_2.Split(char.Parse(","));
            string[] strTx2_2 = tx2_2.Split(char.Parse(","));
            string[] strRx_2 = rx_2.Split(char.Parse(","));
            sweepItem1 = new FreqSweepItem[strTx1_1.Length];
            sweepItem2 = new FreqSweepItem[strTx1_2.Length];
            for (int i = 0; i < strTx1_1.Length; i++)
            {
                sweepItem1[i] = new FreqSweepItem();
                sweepItem1[i].Tx1 = float.Parse(strTx1_1[i]);
                sweepItem1[i].Tx2 = float.Parse(strTx2_1[i]);
                sweepItem1[i].Rx = float.Parse(strRx_1[i]);

                sweepItem2[i] = new FreqSweepItem();
                sweepItem2[i].Tx1 = float.Parse(strTx1_2[i]);
                sweepItem2[i].Tx2 = float.Parse(strTx2_2[i]);
                sweepItem2[i].Rx = float.Parse(strRx_2[i]);

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    //----------------------------------------

                    sweepItem1[i].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[i].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    sweepItem1[i].P2 = settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[i].Tx2, settings.Tx2, Tx_Tables.pim_rev_offset2);
                    sweepItem2[i].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[i].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    sweepItem2[i].P2 = settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[i].Tx2, settings.Tx2, Tx_Tables.pim_rev_offset2);

                    //if (settings.PimSchema == ImSchema.REV)
                    //{
                    //    sweepItem1[i].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[i].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    //    sweepItem1[i].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[i].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);
                    //    sweepItem2[i].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[i].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    //    sweepItem2[i].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[i].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);
                    //}
                    //else
                    //{
                    //    sweepItem1[i].P1 = settings.Tx + Tx_Tables.pim_frd_tx1.Offset(sweepItem1[i].Tx1, settings.Tx, Tx_Tables.pim_frd_offset1);
                    //    sweepItem1[i].P2 = settings.Tx + Tx_Tables.pim_frd_tx2.Offset(sweepItem1[i].Tx2, settings.Tx, Tx_Tables.pim_frd_offset2);
                    //    sweepItem2[i].P1 = settings.Tx + Tx_Tables.pim_frd_tx1.Offset(sweepItem2[i].Tx1, settings.Tx, Tx_Tables.pim_frd_offset1);
                    //    sweepItem2[i].P2 = settings.Tx + Tx_Tables.pim_frd_tx2.Offset(sweepItem2[i].Tx2, settings.Tx, Tx_Tables.pim_frd_offset2);
                    //}
                    //----------------------------------------
                }
                else
                {
                    sweepItem1[i].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem1[i].Tx1, settings.Tx);
                    sweepItem1[i].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem1[i].Tx2, settings.Tx2);
                    sweepItem2[i].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem2[i].Tx1, settings.Tx);
                    sweepItem2[i].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem2[i].Tx2, settings.Tx2);
                }
            }
            fsp.Items1 = sweepItem1;
            fsp.Items2 = sweepItem2;
            return fsp;
        }
        #endregion

        #region 获取不指定的序列频率扫描数据
        /// <summary>
        /// 获取不指定的序列频率扫描数据
        /// </summary>
        /// <param name="order"></param>
        /// <param name="i"></param>
        /// <param name="num"></param>
        internal FreqSweepParam FreqSweep(Settings_Pim settings,int order, int i, int num)
        {
            int n, m;

            m = (order - 1) / 2;
            n = m + 1;

            FreqSweepParam fsp = new FreqSweepParam();
            FreqSweepItem[] sweepItem1 = new FreqSweepItem[num];
            FreqSweepItem[] sweepItem2 = new FreqSweepItem[num];

            float f1Ups = App_Settings.spfc.ims[i].F1UpS;
            float f2Fixed = App_Settings.spfc.ims[i].F2fixed;
            float f1Fixed = App_Settings.spfc.ims[i].F1fixed;
            float f2Dne = App_Settings.spfc.ims[i].F2DnE;
            float ime = App_Settings.spfc.ims[i].ImE;
            float ims = App_Settings.spfc.ims[i].ImS;

            float value = (float)Math.Round((ime - ims) /(num-1), 3);

            for (int j = 0; j < num; j++)
            {
                sweepItem1[j] = new FreqSweepItem();
                sweepItem2[j] = new FreqSweepItem();
                sweepItem1[j].Tx2 = f2Fixed;
                sweepItem1[j].Rx = ims + j * value;
                sweepItem1[j].Tx1 =(float)Math.Round((m * sweepItem1[j].Tx2 + sweepItem1[j].Rx) / n,1,MidpointRounding.AwayFromZero);
                //sweepItem1[j].Rx = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
                if (settings.IsHigh)//high-pim
                {
                    sweepItem1[j].Rx = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;
                }
                else
                {
                    sweepItem1[j].Rx = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
                }

                sweepItem2[j].Tx1 = f1Fixed;
                sweepItem2[j].Rx = ims + j * value;
                sweepItem2[j].Tx2 =(float)Math.Round((n * sweepItem2[j].Tx1 - sweepItem2[j].Rx) / m,1,MidpointRounding.AwayFromZero);
                //sweepItem2[j].Rx = n * sweepItem2[j].Tx1 - m * sweepItem2[j].Tx2;
                if (settings.IsHigh)//high-pim
                {
                    sweepItem2[j].Rx = n * sweepItem2[j].Tx2 - m * sweepItem2[j].Tx1;
                }
                else
                {
                    sweepItem2[j].Rx = n * sweepItem2[j].Tx1 - m * sweepItem2[j].Tx2;
                }

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    //----------------------------------------

                    sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);

                    sweepItem1[j].P2 = settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[j].Tx2, settings.Tx2, Tx_Tables.pim_rev_offset2);

                    sweepItem2[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);

                    sweepItem2[j].P2 = settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[j].Tx2, settings.Tx2, Tx_Tables.pim_rev_offset2);

                    //if (settings.PimSchema == ImSchema.REV)
                    //{
                    //    sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);

                    //    sweepItem1[j].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[j].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);

                    //    sweepItem2[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);

                    //    sweepItem2[j].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[j].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);
                    //}
                    //else
                    //{
                    //    sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_frd_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_frd_offset1);

                    //    sweepItem1[j].P2 = settings.Tx + Tx_Tables.pim_frd_tx2.Offset(sweepItem1[j].Tx2, settings.Tx, Tx_Tables.pim_frd_offset2);

                    //    sweepItem2[j].P1 = settings.Tx + Tx_Tables.pim_frd_tx1.Offset(sweepItem2[j].Tx1, settings.Tx, Tx_Tables.pim_frd_offset1);

                    //    sweepItem2[j].P2 = settings.Tx + Tx_Tables.pim_frd_tx2.Offset(sweepItem2[j].Tx2, settings.Tx, Tx_Tables.pim_frd_offset2);
                    //}
                    //----------------------------------------
                }
                else
                {
                    sweepItem1[j].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem1[j].Tx1, settings.Tx);
                    sweepItem1[j].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem1[j].Tx2, settings.Tx2);
                    sweepItem2[j].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem2[j].Tx1, settings.Tx);
                    sweepItem2[j].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem2[j].Tx2, settings.Tx2);
                }
             

            }
         
            fsp.Items1 = sweepItem1;
            fsp.Items2 = sweepItem2;
            return fsp;
        }
        #endregion

        //#region 获取不指定的序列频率扫描数据
        ///// <summary>
        ///// 获取不指定的序列频率扫描数据
        ///// </summary>
        ///// <param name="order"></param>
        ///// <param name="i"></param>
        ///// <param name="num"></param>
        //internal FreqSweepParam FreqSweep(Settings_Pim settings, int order)
        //{
        //    int n, m;

        //    m = (order - 1) / 2;
        //    n = m + 1;

        //    FreqSweepParam fsp = new FreqSweepParam();
        //    float f1Ups = settings.F1s;
        //    float f1Upe = settings.F1e;
        //    float f2Fixed = settings.F2e;
        //    float f1Fixed = settings.F1s;
        //    float f2Ups = settings.F2s;
        //    float f2Upe = settings.F2e;

        //    float value = settings.Setp1;
        //    float value1 = settings.Setp2;
        //    int sn1 = Convert.ToInt32(Math.Ceiling((f1Upe - f1Ups) / value))+1;
        //    int sn2 = Convert.ToInt32(Math.Ceiling((f2Upe - f2Ups) / value1))+1;
        //    FreqSweepItem[] sweepItem1 = new FreqSweepItem[sn1];
        //    FreqSweepItem[] sweepItem2 = new FreqSweepItem[sn2];
        //    for (int j = 0; j < sn1; j++)
        //    {
        //        sweepItem1[j] = new FreqSweepItem();
        //        sweepItem1[j].Tx2 = f2Fixed;
        //        if (j == sn1 - 1)
        //            sweepItem1[j].Tx1 = f1Upe;
        //        else
        //            sweepItem1[j].Tx1 = j * value + f1Ups;
        //        float f3 = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
        //        float f4 = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;
        //        if (f3 > f4 || f3 == f4)
        //        {
        //            sweepItem1[j].Rx = f4;
        //        }
        //        else
        //            sweepItem1[j].Rx = f3;

        //        if (App_Configure.Cnfgs.Cal_Use_Table)
        //        {
        //            //----------------------------------------

        //            sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
        //            sweepItem1[j].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[j].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);

        //            //----------------------------------------
        //        }
        //        else
        //        {
        //            sweepItem1[j].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem1[j].Tx1, settings.Tx);
        //            sweepItem1[j].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem1[j].Tx2, settings.Tx);
        //        }
        //    }

        //    for (int k = 0; k < sn2; k++)
        //    {
        //        sweepItem2[k] = new FreqSweepItem();
        //        sweepItem2[k].Tx1 = f1Fixed;
        //        if (k == sn2 - 1)
        //            sweepItem2[k].Tx2 = f2Ups;
        //        else
        //            sweepItem2[k].Tx2 = f2Upe - k * value1;
        //        float f5 = n * sweepItem2[k].Tx1 - m * sweepItem2[k].Tx2;
        //        float f6 = n * sweepItem2[k].Tx2 - m * sweepItem2[k].Tx1;
        //        if (f5 > f6 || f5 == f6)
        //        {
        //            sweepItem2[k].Rx = f6;
        //        }
        //        else
        //            sweepItem2[k].Rx = f5;

        //        if (App_Configure.Cnfgs.Cal_Use_Table)
        //        {
        //            //----------------------------------------
        //                sweepItem2[k].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[k].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
        //                sweepItem2[k].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[k].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);
        //            //----------------------------------------
        //        }
        //        else
        //        {
        //            sweepItem2[k].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem2[k].Tx1, settings.Tx);
        //            sweepItem2[k].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem2[k].Tx2, settings.Tx);
        //        }

        //    }
        //    fsp.Items1 = sweepItem1;
        //    fsp.Items2 = sweepItem2;
        //    return fsp;
        //}
        //       #endregion

        #region 获取不指定的序列频率扫描数据
        /// <summary>
        /// 获取不指定的序列频率扫描数据
        /// </summary>
        /// <param name="order"></param>
        /// <param name="i"></param>
        /// <param name="num"></param>
        internal FreqSweepParam FreqSweep(Settings_Pim settings, int order, int i)
        {
            int n, m;

            
            m = (order - 1) / 2;
            n = m + 1;

            FreqSweepParam fsp = new FreqSweepParam();
            float f1Ups = settings.F1s;
            float f1Upe = settings.F1e;
            float f2Fixed = settings.F2e;
            float f1Fixed = settings.F1s;
            float f2Ups = settings.F2s;
            float f2Upe = settings.F2e;

            float value1 = settings.Setp1;
            float value2 = settings.Setp2;
            int sn1 = Convert.ToInt32(Math.Ceiling((f1Upe - f1Ups) / value1)) + 1;
            int sn2 = Convert.ToInt32(Math.Ceiling((f2Upe - f2Ups) / value2)) + 1;

            //if (sn1 == 0)
            //    sn1 = 1;
            //if (sn2 == 0)
            //    sn2 = 1;

            FreqSweepItem[] sweepItem1 = new FreqSweepItem[sn1];
            FreqSweepItem[] sweepItem2 = new FreqSweepItem[sn2];
            for (int j = 0; j < sn1; j++)
            {
                sweepItem1[j] = new FreqSweepItem();
                sweepItem1[j].Tx2 = f2Fixed;
                if (j == sn1 - 1)
                    sweepItem1[j].Tx1 = f1Upe;
                else
                    sweepItem1[j].Tx1 = j * value1 + f1Ups;

                
                //float f3 = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
                //float f4 = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;

                //ygq 
                float f3=0;
                float f4=0;
                if (App_Configure.Cnfgs.Mode >= 2 )
                {
                    if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                    {
                        f3 = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
                        //f4 = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;
                    }
                    else
                    {
                        f3 = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;
                    }
                   
                }
                else
                {
                   
                        if (App_Configure.Cnfgs.Mode == 1)
                        {
                            f3 = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
                            //f4 = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;
                        }
                        else if (App_Configure.Cnfgs.Mode == 0)
                        {
                            f3 = n * sweepItem1[j].Tx2 - m * sweepItem1[j].Tx1;
                        }
                        else
                            f3 = n * sweepItem1[j].Tx1 - m * sweepItem1[j].Tx2;
                   
                }
                //ygq

                //if (f3 > f4 || f3 == f4)
                //{
                //    sweepItem1[j].Rx = f4;
                //}
                //else
                    sweepItem1[j].Rx = f3;

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    //sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    //sweepItem1[j].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[j].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);

                    //ygq 
                    if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                    {
                        sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                        sweepItem1[j].P2 = settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(sweepItem1[j].Tx2, settings.Tx2, Tx_Tables.pim_rev_offset2);
                    }
                    else
                    {
                        sweepItem1[j].P1 = settings.Tx + Tx_Tables.pim_rev2_tx1.Offset(sweepItem1[j].Tx1, settings.Tx, Tx_Tables.pim_rev2_offset1);
                        sweepItem1[j].P2 = settings.Tx2 + Tx_Tables.pim_rev2_tx2.Offset(sweepItem1[j].Tx2, settings.Tx2, Tx_Tables.pim_rev2_offset2);
                    }
                    //ygq
                }
                else
                {
                    sweepItem1[j].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem1[j].Tx1, settings.Tx);
                    sweepItem1[j].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem1[j].Tx2, settings.Tx2);
                }
            }

            for (int k = 0; k < sn2; k++)
            {
                sweepItem2[k] = new FreqSweepItem();
                sweepItem2[k].Tx1 = f1Fixed;
                if (k == sn2 - 1)
                    sweepItem2[k].Tx2 = f2Ups;
                else
                    sweepItem2[k].Tx2 = f2Upe - k * value2;

                //float f5 = n * sweepItem2[k].Tx1 - m * sweepItem2[k].Tx2;
                //float f6 = n * sweepItem2[k].Tx2 - m * sweepItem2[k].Tx1;

               //ygq
                float f5 = 0;
                float f6 = 0;


                if (App_Configure.Cnfgs.Mode >= 2 )
                {
                    if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                    {
                        f5 = n * sweepItem2[k].Tx1 - m * sweepItem2[k].Tx2;
                        //f6 = n * sweepItem2[k].Tx2 - m * sweepItem2[k].Tx1;
                    }
                    else
                    {
                        f5 = n * sweepItem2[k].Tx2 - m * sweepItem2[k].Tx1;
                    }
                   
                }
                else
                {
                    
                        if (App_Configure.Cnfgs.Mode == 1)
                        {
                            f5 = n * sweepItem2[k].Tx1 - m * sweepItem2[k].Tx2;
                            //f6 = n * sweepItem2[k].Tx2 - m * sweepItem2[k].Tx1;
                        }
                        else if (App_Configure.Cnfgs.Mode == 0)
                        {
                            f5 = n * sweepItem2[k].Tx2 - m * sweepItem2[k].Tx1;
                        }
                        else
                            f5 = n * sweepItem2[k].Tx1 - m * sweepItem2[k].Tx2;
                   
                }
               //ygq

                //if (f5 > f6 || f5 == f6)
                //{
                //    sweepItem2[k].Rx = f6;
                //}
                //else
                    sweepItem2[k].Rx = f5;

                if (App_Configure.Cnfgs.Cal_Use_Table)
                {
                    //sweepItem2[k].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[k].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    //sweepItem2[k].P2 = settings.Tx + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[k].Tx2, settings.Tx, Tx_Tables.pim_rev_offset2);

                    //ygq
                    if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                    {
                        sweepItem2[k].P1 = settings.Tx + Tx_Tables.pim_rev_tx1.Offset(sweepItem2[k].Tx1, settings.Tx, Tx_Tables.pim_rev_offset1);
                        sweepItem2[k].P2 = settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(sweepItem2[k].Tx2, settings.Tx2, Tx_Tables.pim_rev_offset2);
                    }
                    else
                    {
                        sweepItem2[k].P1 = settings.Tx + Tx_Tables.pim_rev2_tx1.Offset(sweepItem2[k].Tx1, settings.Tx, Tx_Tables.pim_rev2_offset1);
                        sweepItem2[k].P2 = settings.Tx2 + Tx_Tables.pim_rev2_tx2.Offset(sweepItem2[k].Tx2, settings.Tx2, Tx_Tables.pim_rev2_offset2);
                    }
                    //ygq
                }
                else
                {
                    sweepItem2[k].P1 = (float)App_Factors.pim_tx1.ValueWithOffset(sweepItem2[k].Tx1, settings.Tx);
                    sweepItem2[k].P2 = (float)App_Factors.pim_tx2.ValueWithOffset(sweepItem2[k].Tx2, settings.Tx2);
                }

            }
            fsp.Items1 = sweepItem1;
            fsp.Items2 = sweepItem2;
            return fsp;
        }
        #endregion

        #region 获取不使用指定的时间扫描数据
        ///<summary>
        ///获取不使用指定的时间扫描参数
        ///</summary>
        /// <param name="order"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        internal TimeSweepParam TimeSweep(Settings_Pim settings, int order, int num)
        {
            int n, m;

            m = (order - 1) / 2;
            n = m + 1;
            TimeSweepParam tsp = new TimeSweepParam();
            tsp.F1 = settings.F1s;
            tsp.F2 = settings.F2e;
            if (App_Configure.Cnfgs.Cal_Use_Table)
            {

                //tsp.P1 = (float)settings.Tx + Tx_Tables.pim_rev_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_rev_offset1);
                //tsp.P2 = (float)settings.Tx + Tx_Tables.pim_rev_tx2.Offset(tsp.F2, settings.Tx, Tx_Tables.pim_rev_offset2);

                //ygq
                if (App_Configure.Cnfgs.Mode >=2)
                {
                    if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                    {
                        tsp.P1 = (float)settings.Tx + Tx_Tables.pim_rev_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_rev_offset1);
                        tsp.P2 = (float)settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(tsp.F2, settings.Tx2, Tx_Tables.pim_rev_offset2);
                    }
                    else
                    {
                        tsp.P1 = (float)settings.Tx + Tx_Tables.pim_rev2_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_rev2_offset1);
                        tsp.P2 = (float)settings.Tx2 + Tx_Tables.pim_rev2_tx2.Offset(tsp.F2, settings.Tx2, Tx_Tables.pim_rev2_offset2);
                    }
                }
                else
                {
                    tsp.P1 = (float)settings.Tx + Tx_Tables.pim_rev_tx1.Offset(tsp.F1, settings.Tx, Tx_Tables.pim_rev_offset1);
                    tsp.P2 = (float)settings.Tx2 + Tx_Tables.pim_rev_tx2.Offset(tsp.F2, settings.Tx2, Tx_Tables.pim_rev_offset2);
                }
                //ygq
            }
            else
            {
                tsp.P1 = (float)App_Factors.pim_tx1.ValueWithOffset(tsp.F1, settings.Tx);
                tsp.P2 = (float)App_Factors.pim_tx2.ValueWithOffset(tsp.F2, settings.Tx2);
            }
            if (App_Configure.Cnfgs.Mode >=2)
            {
                if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                {
                    tsp.Rx = n * tsp.F1 - m * tsp.F2;
                }
                else
                {
                    tsp.Rx = m * tsp.F1 - n * tsp.F2;
                }
                
            }
            else
            {
                
                    if (App_Configure.Cnfgs.Mode == 1)
                    {
                        tsp.Rx = n * tsp.F1 - m * tsp.F2;
                    }
                    else if (App_Configure.Cnfgs.Mode == 0)
                    {
                        tsp.Rx = n * tsp.F2 - m * tsp.F1;

                    }
                    else
                        tsp.Rx = n * tsp.F1 - m * tsp.F2;
              
            }
           
            tsp.N = num;

            return tsp;
        }
        #endregion

    }
}
