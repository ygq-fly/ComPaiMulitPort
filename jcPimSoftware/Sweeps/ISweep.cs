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
    /// ɨ��������ֵ����
    /// </summary>
    public class SweepResult
    {
        private float dBm_Value;
        private float dBm_Nosie;

        /// <summary>
        /// ɨ���ķ���ֵ����λdBm
        /// </summary>
        public float dBmValue
        {
            get { return dBm_Value; }
            set { dBm_Value = value; }
        }

        /// <summary>
        /// ɨ���ĵ���ֵ����λdBm
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
        /// timeOut��ʱʱ�䣬��λ����
        /// </summary>
        /// <param name="timeOut"></param>
        void BreakSweep(int timeOut);
    }
}
