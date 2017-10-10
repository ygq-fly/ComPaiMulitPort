using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    class Offset
    {
        public Tx_Table[] tt = new Tx_Table[8];
        public List<Tx_Table[]> list_tt = new List<Tx_Table[]>();
        string exePath = Application.StartupPath;
        string setPath = App_Configure.Cnfgs.Path_Def;
        List<Spectrum_Table> list_st = new List<Spectrum_Table>();
       List<List<Spectrum_Table>> list_list=new List<List<Spectrum_Table>>();
       int length = 0;
        public Offset(int num)
        {
            length = num;
        }
       //public int readfile()
       //{
       //    int num = 0;

       //    for (int i = 0; i < 8; i++)
       //    {
       //        if (System.IO.Directory.Exists(exePath + "\\" + setPath + "\\Tx_Tables" + (i + 1).ToString()))
       //            num += 1;
       //    }
       //    return num;
       //}
        public  void  LoadingTX()
        {
            try
            {
                for (int i = 0; i <length ; i++)
                {
                    if (i == 0)
                    {
                        Tx_Tables.NewTables(exePath + "\\" + setPath + "\\Tx_Tables"  + "\\signal_tx_rev.ini",
                                                                    exePath + "\\" + setPath + "\\Tx_Tables" + "\\signal_tx_disp_rev.ini");
                    }
                    else
                    {
                        Tx_Tables.NewTables(exePath + "\\" + setPath + "\\Tx_Tables" + (i + 1).ToString() + "\\signal_tx_rev.ini",
                                            exePath + "\\" + setPath + "\\Tx_Tables" + (i + 1).ToString() + "\\signal_tx_disp_rev.ini");
                    }
                    tt = Tx_Tables.LoadTables_ygq();
                    list_tt.Add(tt);
                }
            }
            catch
            {
                MessageBox.Show("校准文件错误");
                Application.Exit();
            }
        }
        public void  GetTX(int i)
        {
            if (i > list_tt.Count + 1)
                Tx_Tables.LoadTables_ygq(list_tt[0]);
            else
                Tx_Tables.LoadTables_ygq(list_tt[i]);
        }

        public void LoadingRX()
        {
            string path1 = exePath + "\\" + setPath + "\\RX_Tables";
            string[] rx_tables_names ;
            try
            {
                for (int i = 0; i < length; i++)
                {
                    if (i == 0)
                        rx_tables_names = new string[] { path1 + "\\pim_rev.txt", path1  + "\\pim_frd.txt" };
                    else 
                    rx_tables_names = new string[] { path1 + (i + 1).ToString() + "\\pim_rev.txt", path1 + (i + 1).ToString() + "\\pim_frd.txt" };
                    Rx_Tables.NewTables(rx_tables_names);
                    list_st = Rx_Tables.LoadTables_ygq();
                    list_list.Add(list_st);
                }
            }
            catch
            {
                MessageBox.Show("校准文件错误");
                Application.Exit();
            }

        }
        public void   GetRX(int i)
        {
            if (i > list_st.Count + 1)
                Rx_Tables.LoadTables_ygq(list_list[0]);
            else
                Rx_Tables.LoadTables_ygq(list_list[i]);
        }
    }
}
