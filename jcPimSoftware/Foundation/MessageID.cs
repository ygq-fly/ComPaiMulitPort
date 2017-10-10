using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    internal class MessageID
    {      
        public const int RF_SUCCED_ALL = 0X0400 + 1000;
        public const int RF_SUCCED_ONE = 0X0400 + 1001;
        public const int RF_FAILED     = 0X0400 + 1002;
        public const int RF_ERROR      = 0X0400 + 1003;

        public const int RF_VSWR_WARNINIG = 0X0400 + 1028;
        
        public const int SPECTRUEME_SUCCED = 0X0400 + 1004;      

        public const int SPECTRUM_ERROR      = 0X0400 + 1006;

        public const int PIM_SUCCED = 0X0400 + 1007;
        public const int ISO_SUCCED = 0X0400 + 1008;
        public const int VSW_SUCCED = 0X0400 + 1009;
        public const int HAR_SUCCED = 0X0400 + 1010;

        public const int PIM_SWEEP_DONE = 0X0400 + 1011;
        public const int ISO_SWEEP_DONE = 0X0400 + 1012;
        public const int VSW_SWEEP_DONE = 0X0400 + 1013;
        public const int HAR_SWEEP_DONE = 0X0400 + 1014;
        public const int SF_WAIT = 0X0400 + 1020;
        public const int SF_CONTINUTE = 0X0400 + 1021;

        public const int PIM_SWEEP_CLOSE = 0X0400 + 1027;

        //=====================
        public const int Error_ = 0X0400 + 2000;
        public const int Start__ = 0X0400 + 2001;
        public const int Setfps_ = 0X0400 + 2002;
        public const int Setfps_2 = 0X0400 + 2004;
        public const int Order__ = 0X0400 + 2003;
        public const int Mode_ = 0X0400 + 2005;
        public const int FWE_ = 0X0400 + 2006;
        public const int Unit_ = 0X0400 + 2007;
        public const int Start_F1F2_ = 0X0400 + 2008;
        public const int CON_ERROR = 0X0400 + 2009;
        //========================================


        private MessageID()
        {
            //
        }
    }
}
