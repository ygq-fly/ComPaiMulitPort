using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    class App_Settings
    {
        internal static Settings_Pim pim;

        internal static Settings_Spc spc;

        internal static Settings_Iso iso;

        internal static Settings_Vsw vsw;

        internal static Settings_Har har;

        internal static Settings_Sgn sgn_1;

        internal static Settings_Sgn sgn_2;

        internal static Specifics spfc;

        private App_Settings()
        {
            //
        }

        /// <summary>
        /// 传递配置文件名称，
        /// 依次为互调模块配置文件、频谱分析模块配置文件、
        /// 隔离度模块配置文件、驻波比模块配置文件、二次谐波模块配置文件、
        /// 射频功放的规格配置文件、仪表频率规划的配置文件
        /// </summary>
        /// <param name="fileNames"></param>
        internal static void NewSettings(string[] fileNames)
        {
            //iso = new Settings_Iso(fileNames[2]);

            //vsw = new Settings_Vsw(fileNames[3]);

            //har = new Settings_Har(fileNames[4]);

            sgn_1 = new Settings_Sgn(fileNames[0], "signal_1");

            sgn_2 = new Settings_Sgn(fileNames[0], "signal_2");

            pim = new Settings_Pim(fileNames[1]);

            spc = new Settings_Spc(fileNames[2]);

            spfc = new Specifics(fileNames[3]);
        }

        internal static void LoadSettings()
        {

            if (spfc != null)
                spfc.LoadSettings();

            if (sgn_1 != null)
                sgn_1.LoadSettings();

            if (sgn_2 != null)
                sgn_2.LoadSettings();

            if (pim != null) 
                pim.LoadSettings();

            if (spc != null) 
                spc.LoadSettings();

            //if (iso != null) 
            //    iso.LoadSettings();

            //if (vsw != null) 
            //    vsw.LoadSettings();

            //if (har != null)
            //    har.LoadSettings();

          

        
        }
    }

}
