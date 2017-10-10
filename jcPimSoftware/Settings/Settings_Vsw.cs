using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    class Settings_Vsw
    {
        /// <summary>
        /// �����ļ����ƣ���ȡĳ���ļ�ʱ����ҪԤ������
        /// </summary>
        internal readonly string fileName;

        internal Settings_Vsw(string fileName)
        {
            this.fileName = fileName;
        }

        /// <summary>
        /// פ���Ȳ���Ĭ���������
        /// </summary>
        private float tx;
        internal float Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        /// <summary>
        /// פ���Ȳ��ԣ���ƵĬ�Ϲ���Ƶ��
        /// </summary>
        private float freq;
        internal float F
        {
            get { return freq; }
            set { freq = value; }
        }

        /// <summary>
        /// פ���Ȳ��ԣ�ɨƵʱ�Ĳ���ֵ����λMHz
        /// </summary>
        private float freq_step;
        internal float Freq_Step
        {
            get { return freq_step; }
            set { freq_step = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����Ƶ����Att����Ϊ��ֵ
        /// </summary>
        private int att_spc;
        internal int Att_Spc
        {
            get { return att_spc; }
            set { att_spc = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����Ƶ����rbw����Ϊ��ֵ
        /// </summary>
        private int rbw_spc;
        internal int Rbw_Spc
        {
            get { return rbw_spc; }
            set { rbw_spc = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����Ƶ����vbw����Ϊ��ֵ
        /// </summary>
        private int vbw_spc;
        internal int Vbw_Spc
        {
            get { return vbw_spc; }
            set { vbw_spc = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����ͼ�������������ʼֵ��פ����ֵ��
        /// </summary>
        private float min_vsw;
        internal float Min_Vsw
        {
            get { return min_vsw; }
            set { min_vsw = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����ͼ�������������ʼֵ��פ����ֵ��
        /// </summary>
        private float max_vsw;
        internal float Max_Vsw
        {
            get { return max_vsw; }
            set { max_vsw = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����ͼ�������������ʼֵ���ز����ֵ��
        /// </summary>
        private float min_rls;
        internal float Min_Rls
        {
            get { return min_rls; }
            set { min_rls = value; }
        }

        /// <summary>
        /// פ���Ȳ���ʱ����ͼ����������Ľ���ֵ���ز����ֵ��
        /// </summary>
        private float max_rls;
        internal float Max_Rls
        {
            get { return max_rls; }
            set { max_rls = value; }
        }

        /// <summary>
        /// פ��������ֵ����PASS/FAIL�Ĳο�ֵ
        /// </summary>
        private float limit;
        internal float Limit_Vsw
        {
            get { return limit; }
            set { limit = value; }
        }

        /// <summary>
        /// פ���ȵ�Ƶ����
        /// </summary>
        private int count;
        internal int Count
        {
            get { return count; }
            set { count = value; }
        }

        /// <summary>
        /// ���˥�������ֵ
        /// </summary>
        private float attenuator;
        internal float Attenuator
        {
            get { return attenuator; }
            set { attenuator = value; }
        }

        /// <summary>
        /// �Զ�У׼���˥�������ƫ��
        /// </summary>
        private float offset;
        internal float Offset
        {
            get { return offset; }
            set { offset = value; }
        }


        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        internal void Clone(Settings_Vsw dest)
        {
            if (dest != null)
            {
                dest.F = this.freq;
                dest.Tx = this.tx;

                dest.Min_Rls = this.Min_Rls;
                dest.Max_Rls = this.Max_Rls;
                dest.Min_Vsw = this.Min_Vsw;
                dest.Max_Vsw = this.Max_Vsw;

                dest.Limit_Vsw = this.limit;

                dest.Att_Spc = this.att_spc;
                dest.Rbw_Spc = this.rbw_spc;
                dest.Vbw_Spc = this.vbw_spc;

                dest.Count = this.count;
                dest.Freq_Step = this.freq_step;

                dest.Attenuator = this.attenuator;
                dest.Offset = this.offset;
            }
        }

        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);

            tx = float.Parse(IniFile.GetString("vswr", "tx", "30.0"));
            freq = float.Parse(IniFile.GetString("vswr", "freq", "935.0"));
            freq_step = float.Parse(IniFile.GetString("vswr", "freq_step", "0.5"));
            count = int.Parse(IniFile.GetString("vswr", "count", "20"));

            att_spc = int.Parse(IniFile.GetString("vswr", "att_spc", "0"));
            rbw_spc = int.Parse(IniFile.GetString("vswr", "rbw_spc", "4"));
            vbw_spc = int.Parse(IniFile.GetString("vswr", "vbw_spc", "4"));

            min_vsw = float.Parse(IniFile.GetString("vswr", "min_vsw", "0"));
            max_vsw = float.Parse(IniFile.GetString("vswr", "max_vsw", "10"));

            min_rls = float.Parse(IniFile.GetString("vswr", "min_rls", "0"));
            max_rls = float.Parse(IniFile.GetString("vswr", "max_rls", "100"));

            limit = float.Parse(IniFile.GetString("vswr", "limit", "1.3"));

            attenuator = float.Parse(IniFile.GetString("vswr", "attenuator", "0"));
            offset = float.Parse(IniFile.GetString("vswr", "offset", "0"));
        }

        internal void StoreSettings()
        {
            StoreSettings(fileName);
        }

        internal void StoreSettings(string SettingsFileName)
        {
            IniFile.SetFileName(SettingsFileName);

            IniFile.SetString("vswr", "tx", tx.ToString("0.#"));
            IniFile.SetString("vswr", "freq", freq.ToString("0.#"));
            IniFile.SetString("vswr", "freq_step", freq_step.ToString("0.#"));

            IniFile.SetString("vswr", "att_spc", att_spc.ToString());
            IniFile.SetString("vswr", "rbw_spc", rbw_spc.ToString());
            IniFile.SetString("vswr", "vbw_spc", vbw_spc.ToString());
            IniFile.SetString("vswr", "count", count.ToString());

            IniFile.SetString("vswr", "min_vsw", min_vsw.ToString("0.#"));
            IniFile.SetString("vswr", "max_vsw", max_vsw.ToString("0.#"));

            IniFile.SetString("vswr", "min_rls", min_rls.ToString("0.#"));
            IniFile.SetString("vswr", "max_rls", max_rls.ToString("0.#"));

            IniFile.SetString("vswr", "limit", limit.ToString("0.#"));

            IniFile.SetString("vswr", "attenuator", limit.ToString("0.#"));
            IniFile.SetString("vswr", "offset", limit.ToString("0.#"));
        }

        internal void Save2File(string defFileName, string dstFileName)
        {
            //������ʱ�ļ�������
            string tempFileName = "C:\\" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".ini";

            //��Ĭ���ļ����Ƶ���ʱ�ļ����Դ���������ͬ�ṹ�������ļ�
            File.Copy(defFileName, tempFileName, true);

            //��ͣ50ms,�Եȴ���ʱ�ļ�����������
            System.Threading.Thread.Sleep(50);

            //����ǰ�������л�����ʱ�ļ�
            StoreSettings(tempFileName);

            //����ʱ�ļ�������Ŀ���ļ�
            File.Copy(tempFileName, dstFileName, true);

            //ɾ����ʱ�ļ�
            File.Delete(tempFileName);
        }
    }
}
