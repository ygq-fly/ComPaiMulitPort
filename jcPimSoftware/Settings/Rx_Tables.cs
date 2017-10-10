using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class Rx_Tables
    {
        static private Spectrum_Table[] tables;

        private Rx_Tables()
        {
            //
        }

        /// <summary>
        /// 提供模块收信校准表格文件的名称
        /// 依次次为pim、iso、vsw、har
        /// </summary>
        /// <param name="tableNames"></param>
        internal static void NewTables(string[] tableNames)
        {
            int tableCount = tableNames.Length;

            tables = new Spectrum_Table[tableCount];

            for (int i = 0; i < tableCount; i++)
                tables[i] = new Spectrum_Table(tableNames[i]);
        }

        internal static void LoadTables()
        {
            if (tables != null)
            {
                for (int i = 0; i < tables.Length; i++)                
                    tables[i].LoadSettings();
            }
        }
        internal static List<Spectrum_Table> LoadTables_ygq()
        {
            List<Spectrum_Table> list_st = new List<Spectrum_Table>();
            if (tables != null)
            {
                for (int i = 0; i < tables.Length; i++)
                {
                    tables[i].LoadSettings();
                    list_st.Add(tables[i]);
                }
            }
            return list_st;
        }
        internal static void  LoadTables_ygq(List<Spectrum_Table> lst)
        {
           tables=new Spectrum_Table[lst.Count];
           for (int i = 0; i < tables.Length; i++)
           {
               tables[i] = lst[i];
           }     
        }

        internal static void FreeTabels()
        {
            for (int i = 0; i < tables.Length; i++)           
                tables[i] = null;
        }

        internal static float Offset(float f, FuncModule module)
        {
            float v = 0.0f;

            if (module == FuncModule.PIM)
                v = tables[0].Offset(f);

            //else if (module == FuncModule.ISO)
            //    v = tables[1].Offset(f);

            //else if (module == FuncModule.VSW)
            //    v = tables[2].Offset(f);

            //else if (module == FuncModule.HAR)
            //    v = tables[3].Offset(f);

            return v;
        }

        //----------------------------------------
        internal static float Offset(float f, FuncModule module, bool isRev)
        {
            float v = 0.0f;

            //if (module == FuncModule.PIM)
            //{
            //    if (isRev)
            //        v = tables[0].Offset(f);
            //    else
            //        v = tables[1].Offset(f);
            //}



            if (module == FuncModule.PIM)
            {
                if (isRev)
                {
                    if (App_Configure.Cnfgs.Mode >=2)
                    {
                        if (PimForm.port1_rev_fwd == 1)
                            v = tables[0].Offset(f);
                        else
                            v = tables[2].Offset(f);
                    }
                    else
                        v = tables[0].Offset(f)+App_Settings.spc.RxRef;
                }
                else
                {
                    if (App_Configure.Cnfgs.Mode >=2)
                    {
                        if (PimForm.port1_rev_fwd == 2)
                            v = tables[1].Offset(f);
                        else
                            v = tables[3].Offset(f);
                    }
                    else
                        v = tables[1].Offset(f)+App_Settings.spc.RxRef;

                }
            }



            //else if (module == FuncModule.ISO)
            //    v = tables[2].Offset(f);

            //else if (module == FuncModule.VSW)
            //    v = tables[3].Offset(f);

            //else if (module == FuncModule.HAR)
            //    v = tables[4].Offset(f);

            return v;
        }
        //----------------------------------------
    }

}
