using System;
using System.Collections.Generic;
using System.Text;

namespace jcPimSoftware
{
    public class SweepCtrl
    {
        private bool bStop;
        private bool bQuit;
        private bool bRestart;

        public bool Stop
        {
            get { return bStop; }
            set { bStop = value; }
        }

        public bool Quit
        {
            get { return bQuit; }
            set { bQuit = value; }
        }

        public bool Restart
        {
            get { return bRestart; }
            set { bRestart = value; }
        }
    }

    /// <summary>
    /// 扫描结果的数值对象
    /// </summary>
    public class SweepResult
    {
        private float dBm_Value;
        private float dBm_Nosie;

        /// <summary>
        /// 扫描点的幅度值，单位dBm
        /// </summary>
        public float dBmValue
        {
            get { return dBm_Value; }
            set { dBm_Value = value; }
        }

        /// <summary>
        /// 扫描点的底噪值，单位dBm
        /// </summary>
         public float dBmNosie
        {
            get { return dBm_Nosie; }
            set { dBm_Nosie = value; }
        }     
    }


    public interface ISweep
    {
        /// <summary>
        /// timeOut超时时间，单位毫秒
        /// </summary>
        /// <param name="timeOut"></param>
        void BreakSweep(int timeOut);
    }
}
