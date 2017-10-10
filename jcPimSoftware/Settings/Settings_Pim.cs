using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace jcPimSoftware
{
    
    class Settings_Pim
    {
        /// <summary>
        /// ��������(��ʶ)
        /// </summary>
        internal string projectName;
        /// <summary>
        /// �����ļ����ƣ���ȡĳ���ļ�ʱ����ҪԤ������
        /// </summary>
        internal readonly string fileName;

        internal Settings_Pim(string fileName)
        {
            this.fileName = fileName;
        }

        #region Property
        /// <summary>
        /// pim-high-band-default:false
        /// </summary>
        public   bool ishigh;

        /// <summary>
        /// ��������Ĭ���������
        /// </summary>
        private float tx;
        private float tx2;
        /// <summary>
        /// ��������Ĭ�����Ƶ��
        /// </summary>
        private float f1;
        private float f2;

        //�ṩ������������Ƶ�ʶ�ʹ��
        private float _f1s;
        private float _f1e;
        private float _f2s;
        private float _f2e;
        private float _setp1;
        private float _setp2;


        /// <summary>
        /// �������ԣ��Ƿ�ʹ��ָ�����н���ɨ��
        /// </summary>
        private int enableSquence;
        /// <summary>
        /// �������Է�����REV/FWD
        /// </summary>
        private int schema;
        /// <summary>
        /// ��������ģʽ��Ƶ��ɨ�衢ʱ��ɨ��
        /// </summary>
        private int mode;
        /// <summary>
        /// �������Խ�����3��5��7��9��
        /// </summary>
        private int order;
        /// <summary>
        /// ����ֵ�ĵ�λ��dBc/dBm
        /// </summary>
        private int unit;
        /// <summary>
        /// ��������ֵ����PASS/FAIL�Ĳο�ֵ
        /// </summary>
        private float limit;
        /// <summary>
        /// ʱ��ɨ�������
        /// </summary>
        private int scanum1;
        /// <summary>
        /// ʱ��ɨ��ĵ���
        /// </summary>
        private int scanum2;
        /// <summary>
        /// Ƶ��ɨ��ĵ���
        /// </summary>
        private int scanum3;
        /// <summary>
        /// Ƶ��ɨ�������
        /// </summary>
        private int scanum4;
        /// <summary>
        /// ��������ʱ����Ƶ����Att����Ϊ��ֵ
        /// </summary>
        private int att_spc;
        /// <summary>
        /// ��������ʱ����Ƶ����rbw����Ϊ��ֵ
        /// </summary>
        private int rbw_spc;
        /// <summary>
        /// ��������ʱ����Ƶ����vbw����Ϊ��ֵ
        /// </summary>
        private int vbw_spc;

        /// <summary>
        /// ��λ��dBm/w
        /// </summary>
        private int txLevel;

        /// <summary>
        /// ������������
        /// </summary>
        private float scanband;

        /// <summary>
        /// ��Ƶ������ʱ������
        /// </summary>
        private int specDelay;

        /// <summary>
        /// ɨʱ��ʱ������
        /// </summary>
        private int tsDelay;

        /// <summary>
        /// ɨƵ��ʱ������
        /// </summary>
        private int fsDelay;  
        
        #endregion

        #region Method
        public bool IsHigh
        {
            get { return ishigh; }
            set { ishigh = value; }
        }

        public float F1s
        {
            get { return _f1s; }
            set { _f1s = value; }
        }

        public float F1e
        {
            get { return _f1e; }
            set { _f1e = value; }
        }

        public float F2s
        {
            get { return _f2s; }
            set { _f2s = value; }
        }

        public float F2e
        {
            get { return _f2e; }
            set { _f2e = value; }
        }

        public float Setp1
        {
            get { return _setp1; }
            set { _setp1 = value; }
        }

        public float Setp2
        {
            get { return _setp2; }
            set { _setp2 = value; }
        }

        internal float Tx
        {
            get { return tx; }
            set { tx = value; }
        }

        internal float Tx2
        {
            get { return tx2; }
            set { tx2 = value; }
        }
        public float F2
        {
            get { return f2; }
            set { f2 = value; }
        }
        public float F1
        {
            get { return f1; }
            set { f1 = value; }
        }
        internal bool EnableSquence
        {
            get { return (enableSquence > 0); }
            set
            {
                if (value)
                    enableSquence = 1;
                else 
                    enableSquence = 0;
            }
        }
     
        internal ImSchema PimSchema
        {

            get { return (ImSchema)Enum.Parse(typeof(ImSchema), Enum.GetName(typeof(ImSchema), schema)); }
            set { schema = (int)value; }
        }

      
        internal SweepType SweepType
        {
            get { return (SweepType)Enum.Parse(typeof(SweepType), Enum.GetName(typeof(SweepType), mode)); }
            set { mode = (int)value; }          
        }

       
        internal ImOrder PimOrder
        {
            get { return (ImOrder)Enum.Parse(typeof(ImOrder), Enum.GetName(typeof(ImOrder), order)); }
            set { order = (int)value; }   
        }

        internal ImUint PimUnit
        {
            get { return (ImUint)Enum.Parse(typeof(ImUint), Enum.GetName(typeof(ImUint), unit)); }
            set { unit = (int)value; }
        }

        internal TxLevel TxLevel
        {
            get { return (TxLevel)Enum.Parse(typeof(TxLevel), Enum.GetName(typeof(TxLevel), txLevel)); }
            set { txLevel = (int)value; }
        }

        internal float Limit_Pim
        {
            get { return limit; }
            set { limit = value; }
        }

        internal int TimeSweepTimes
        {
            get { return scanum1; }
            set { scanum1 = value; }
        }

        internal int TimeSweepPoints
        {
            get { return scanum2; }
            set { scanum2 = value; }
        }
        internal int FreqSweepPoints
        {
            get { return scanum3; }
            set { scanum3 = value; }
        }
       
        internal int FreqSweepTimes
        {
            get { return scanum4; }
            set { scanum4 = value; }
        }

        internal int Att_Spc
        {
            get { return att_spc; }
            set { att_spc = value; }
        }

        internal int Rbw_Spc
        {
            get { return rbw_spc; }
            set { rbw_spc = value; }
        }

        internal int Vbw_Spc
        {
            get { return vbw_spc; }
            set { vbw_spc = value; }
        }

        internal float Scanband
        {
            get { return scanband; }
            set { scanband = value; }
        }

        internal int SpecDelay
        {
            get { return specDelay; }
            set { specDelay = value; }
        }

        internal int TsDelay
        {
            get { return tsDelay; }
            set { tsDelay = value; }
        }

        internal int FsDelay
        {
            get { return fsDelay; }
            set { fsDelay = value; }
        }

        #endregion

        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        internal void Clone(Settings_Pim dest)
        {
            if (dest != null)
            {
                dest.Tx = this.tx;
                dest.Tx2 = this.tx2;
                dest.F1 = this.f1;
                dest.F2 = this.f2;
                dest.F1s = this._f1s;
                dest.F1e = this._f1e;
                dest.F2s = this._f2s;
                dest.F2e = this._f2e;
                dest.Setp1 = this._setp1;
                dest.Setp2 = this._setp2;


                bool flag = false;
                if (enableSquence == 1)
                    flag = true;
                dest.EnableSquence = flag;
                dest.PimSchema =(ImSchema)this.schema;
                dest.Limit_Pim = this.limit;
                dest.Vbw_Spc = this.vbw_spc;
                dest.Rbw_Spc = this.rbw_spc;
                dest.Att_Spc = this.att_spc;
                dest.TimeSweepTimes = this.scanum1;
                dest.TimeSweepPoints = this.scanum2;
                dest.FreqSweepPoints = this.scanum3;
                dest.FreqSweepTimes = this.scanum4;
                dest.PimUnit = (ImUint)this.unit;
                dest.PimOrder = (ImOrder)this.order;
                dest.SweepType = (SweepType)this.mode;
                dest.scanband = this.scanband;
                dest.SpecDelay = this.SpecDelay;
                dest.tsDelay = this.tsDelay;
                dest.fsDelay = this.fsDelay;
            }
        }


        /// <summary>
        /// ��ȡ����ֵ
        /// </summary>
        internal void LoadSettings()
        {
            IniFile.SetFileName(fileName);


            ishigh = IniFile.GetString("pim", "ishigh", "0") == "1" ? true : false;
            //ishigh = false;

            tx = float.Parse(IniFile.GetString("pim", "tx", "43.0"));
            tx2 = float.Parse(IniFile.GetString("pim", "tx", "43.0"));
            f1 = float.Parse(IniFile.GetString("pim", "f1", "935.0"));
            f2 = float.Parse(IniFile.GetString("pim", "f2", "960.0"));

            _f1s = App_Settings.spfc.ims[0].F1UpS;
            _f1e = App_Settings.spfc.ims[0].F1UpE;
            _f2s = App_Settings.spfc.ims[0].F2DnS;
            _f2e = App_Settings.spfc.ims[0].F2DnE;

            _setp1 = App_Settings.spfc.ims[0].F1Step;
            _setp2 =App_Settings.spfc.ims[0].F2Step;

            enableSquence = 0;

            schema = int.Parse(IniFile.GetString("pim", "schema", "0"));
            mode = int.Parse(IniFile.GetString("pim", "mode", "0"));
            order = int.Parse(IniFile.GetString("pim", "order", "3"));
            unit = int.Parse(IniFile.GetString("pim", "unit", "1"));

            limit = float.Parse(IniFile.GetString("pim", "limit", "-165.0"));

            scanum1 = int.Parse(IniFile.GetString("pim", "scanum1", "20"));
            scanum2 = int.Parse(IniFile.GetString("pim", "scanum2", "20"));
            scanum3 = int.Parse(IniFile.GetString("pim", "scanum3", "10"));
            scanum4 = int.Parse(IniFile.GetString("pim", "scanum4", "1"));

            att_spc = int.Parse(IniFile.GetString("pim", "att_spc", "0"));
            rbw_spc = int.Parse(IniFile.GetString("pim", "rbw_spc", "4"));
            vbw_spc = int.Parse(IniFile.GetString("pim", "vbw_spc", "4"));

            scanband = float.Parse(IniFile.GetString("pim", "scanband", "0.05"));

            specDelay = int.Parse(IniFile.GetString("pim", "specDelay", "0"));
            tsDelay = int.Parse(IniFile.GetString("pim", "tsDelay", "0"));
            fsDelay = int.Parse(IniFile.GetString("pim", "fsDelay", "0")); 
        }

        internal void StoreSettings()
        {
            StoreSettings(fileName);
        }

        /// <summary>
        /// ��������ֵ
        /// </summary>
        internal void StoreSettings(string fileName)
        {
            IniFile.SetFileName(fileName);

            IniFile.SetString("pim", "tx", tx.ToString("0.#"));
            IniFile.SetString("pim", "f1", f1.ToString("0.#"));
            IniFile.SetString("pim", "f2", f2.ToString("0.#"));

          //  IniFile.SetString("pim", "enableSquence", enableSquence.ToString());
            
            IniFile.SetString("pim", "schema", schema.ToString());
            IniFile.SetString("pim", "mode", mode.ToString());
            IniFile.SetString("pim", "order", order.ToString());
            IniFile.SetString("pim", "unit", unit.ToString());
            IniFile.SetString("pim", "limit", limit.ToString("0.#"));

            IniFile.SetString("pim", "scanum1", scanum1.ToString());
            IniFile.SetString("pim", "scanum2", scanum2.ToString());
            IniFile.SetString("pim", "scanum3", scanum3.ToString());
            IniFile.SetString("pim", "scanum4", scanum4.ToString());

            IniFile.SetString("pim", "att_spc", att_spc.ToString());
            IniFile.SetString("pim", "rbw_spc", rbw_spc.ToString());
            IniFile.SetString("pim", "vbw_spc", vbw_spc.ToString());
            IniFile.SetString("pim", "scanband", scanband.ToString());  
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
