using System;
using System.Collections.Generic;
using System.Text;
using SpectrumLib.Models;

namespace jcPimSoftware
{
    #region ������ص����Ͷ���
    enum ImSchema
    {
        REV = 0,
        FWD = 1,

    }

    enum ImMode
    {
        Time_Sweep = 0,

        Freq_Sweep = 1
    }

    enum ImOrder
    {
        Im3 = 3,
        Im5 = 5,
        Im7 = 7,
        Im9 = 9,
    
    }

    enum ImUint
    {
        dBm = 0,
        dBc = 1
    }

    enum TxLevel
    {
        dBm = 0,
        W= 1
    }

    enum RFInvolved 
    {
        Rf_1_2 = 0,        
        Rf_1 = 1,
        Rf_2 = 2        
    }
    
    enum SweepType
    {
        Freq_Sweep = 0,

        Time_Sweep = 1
    }
    #endregion

    #region ����ͨ������
    enum RxChannel
    {
        NarrowBand = 0,
        WideBand = 1
    }
    #endregion

    #region ����ģ��ö��ָʾ��
    enum FuncModule
    {
        PIM = 0,
        SPECTRUM = 1,
        ISO = 2,
        VSW = 3,
        HAR = 4
    }
    #endregion

    #region Ƶ�׷�����RBW����
    enum RBW
    {
        rbw4KHz = 1,
        rbw20KHz = 5,
        rbw100KHz = 25,
        rbw1000KHz = 125,
    }
    #endregion

    #region ִ��ɨ����Ҫ�ṩ�Ĳ�������
    internal class TimeSweepParam
    {
        private float p1;
        private float p2;
        private float f1;
        private float f2;
        private float rx;
        private int n;
        private float n1;

        /// <summary>
        /// ���ڴ���ɨ��ʱ�伸�������
        /// </summary>
        public float N1
        {
            get { return n1; }
            set { n1 = value; }
        }

        /// <summary>
        /// ����1��������趨ֵ
        /// </summary>
        public float P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        /// <summary>
        /// ����2��������趨ֵ
        /// </summary>
        public float P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        /// <summary>
        /// ����1����Ƶ���趨ֵ
        /// </summary>
        public float F1
        {
            get { return f1; }
            set { f1 = value; }
        }

        /// <summary>
        /// ����2����Ƶ���趨ֵ
        /// </summary>
        public float F2
        {
            get { return f2; }
            set { f2 = value; }
        }

        /// <summary>
        /// ���յ�����Ƶ���趨ֵ
        /// </summary>
        public float Rx
        {
            get { return rx; }
            set { rx = value; }
        }

        /// <summary>
        /// ɨʱ�ĵ���
        /// </summary>
        public int N
        {
            get { return n; }
            set { n = value; }
        }

        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        public void Clone(TimeSweepParam dest)
        {
            if (dest != null)
            {
                dest.P1 = this.p1;
                dest.P2 = this.p2;

                dest.F1 = this.f1;
                dest.F2 = this.f2;

                dest.Rx = this.rx;
                dest.N1 = this.n1;
                dest.N = this.n;
            }
        }
    }

    internal class FreqSweepItem
    {
        private float p1;
        private float p2;
        private float tx1;
        private float tx2;
        private float rx;

        /// <summary>
        /// ����1�������������ֵ
        /// </summary>
        public float P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        /// <summary>
        /// ����2�������������ֵ
        /// </summary>
        public float P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        /// <summary>
        /// ����1������Ƶ������ֵ
        /// </summary>
        public float Tx1
        {
            get { return tx1; }
            set { tx1 = value; }
        }

        /// <summary>
        /// ����2������Ƶ������ֵ
        /// </summary>
        public float Tx2
        {
            get { return tx2; }
            set { tx2 = value; }
        }

        /// <summary>
        /// ���ŵ�����Ƶ������ֵ����Ƶ�׷�����CENTERֵ
        /// </summary>
        public float Rx
        {
            get { return rx; }
            set { rx = value; }
        }

        /// <summary>
        /// ����ǰ��ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="item"></param>
        public void Clone(FreqSweepItem dest)
        {
            if (dest != null)
            {
                dest.P1  = this.p1;
                dest.P2  = this.p2;
                dest.Tx1 = this.tx1;
                dest.Tx2 = this.tx2;
                dest.Rx  = this.rx;
            }
        }
    }

    internal class FreqSweepParam
    {
        private float p1;
        private float p2;
        private FreqSweepItem[] items1;
        private FreqSweepItem[] items2;


        /// <summary>
        /// ����1��������趨ֵ
        /// </summary>
        public float P1
        {
            get { return p1; }
            set { p1 = value; }
        }

        /// <summary>
        /// ����2��������趨ֵ
        /// </summary>
        public float P2
        {
            get { return p2; }
            set { p2 = value; }
        }

        /// <summary>
        /// ɨ������ 1
        /// </summary>
        public FreqSweepItem[] Items1
        {
            get { return items1; }
            set { items1 = value; }
        }

        /// <summary>
        /// ɨ������ 2
        /// </summary>
        public FreqSweepItem[] Items2
        {
            get { return items2; }
            set { items2 = value; }
        }

        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        public void Clone(FreqSweepParam dest)
        {
            int I, L;

            if (dest == null)
                return;

            dest.P1 = this.p1;
            dest.P2 = this.p2;
                           
            if (this.items1 != null)
            {
                L = items1.Length;

                dest.Items1 = new FreqSweepItem[L];
                for (I = 0; I < L; I++)
                {
                    dest.Items1[I] = new FreqSweepItem();
                    this.items1[I].Clone(dest.Items1[I]);
                }
            }

            if (this.items2 != null)
            {
                L = this.items2.Length;

                dest.Items2 = new FreqSweepItem[L];
                for (I = 0; I < L; I++)
                {
                    dest.Items2[I] = new FreqSweepItem();
                    this.items2[I].Clone(dest.Items2[I]);
                }
            }

        }

        public FreqSweepParam()
        {
            items1 = new FreqSweepItem[0];
            items2 = new FreqSweepItem[0];
        }
    }

    internal class DeviceInfo
    {
        private int rf_addr1;
        private int rf_addr2;
        private int spectrum;

        /// <summary>
        /// ����1�ĵ�ַ����ֵ
        /// </summary>
        public int RF_Addr1
        {
            get { return rf_addr1; }
            set { rf_addr1 = value; }
        }

        /// <summary>
        /// ����2�ĵ�ַ����ֵ
        /// </summary>
        public int RF_Addr2
        {
            get { return rf_addr2; }
            set { rf_addr2 = value; }
        }

        /// <summary>
        /// ����2�ĵ�ַ����ֵ
        /// </summary>
        public int Spectrum
        {
            get { return spectrum; }
            set { spectrum = value; }
        }

        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        public void Clone(DeviceInfo dest)
        {
            if (dest != null)
            {
                dest.RF_Addr1 = this.rf_addr1;
                dest.RF_Addr2 = this.rf_addr2;
                dest.Spectrum = this.spectrum;
            }                
        }
    }

    internal class SweepParams
    {
        private SweepType swpType;
        private int c;
        private IntPtr wndHandle;
        private RFInvolved rfInvolved;       
        private int rfPriority;
        private DeviceInfo devInfo;
        private ImOrder pimOrder;
        private TimeSweepParam tmeParam;
        private FreqSweepParam frqParam;
        private ScanModel speParam;

        /// <summary>
        /// ɨ������ͣ���ɨʱ��ɨƵ
        /// </summary>
        public SweepType SweepType
        {
            get { return swpType; }
            set { swpType = value; }
        }

        /// <summary>
        /// ɨ���ѭ��������������
        /// </summary>
        public int C
        {
            get { return c; }
            set {c = value;}
        }

        /// <summary>
        /// Ŀ�괰��ľ��
        /// </summary>
        public IntPtr WndHandle
        {
            get { return wndHandle; }
            set { wndHandle = value; }
        }

        /// <summary>
        /// ɨ�����ʱ����Ҫ����Щ���ŵ�ָʾ��
        /// </summary>
        public RFInvolved RFInvolved
        {
            get { return rfInvolved; }
            set { rfInvolved = value; }
        }

        /// <summary>
        /// �򹦷ŷ�����������ȼ�ָʾ��
        /// </summary>
        public int RFPriority
        {
            get { return rfPriority; }
            set { rfPriority = value; }
        }

        /// <summary>
        /// ɨ�������Ҫʹ�õ����豸��Ϣ��ָʾ��
        /// </summary>
        public DeviceInfo DevInfo
        {
            get { return devInfo; }
            set { devInfo = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public ImOrder PimOrder
        {
            get { return pimOrder; }
            set { pimOrder = value; }
        }

        /// <summary>
        /// ɨʱ��ɨ�����
        /// </summary>
        public TimeSweepParam TmeParam
        {
            get { return tmeParam; }
            set { tmeParam = value; }
        }

        /// <summary>
        /// ɨƵ��ɨ�����
        /// </summary>
        public FreqSweepParam FrqParam
        {
            get { return frqParam; }
            set { frqParam = value; }
        }
        
        /// <summary>
        /// ɨ��ʱ����Ҫ�ṩ�ĸ�Ƶ���ǵķ�������
        /// </summary>
        public ScanModel SpeParam
        {
            get { return speParam; }
            set { speParam = value; }
        }
 
        /// <summary>
        /// ��������ȿ�����Ŀ��dest
        /// </summary>
        /// <param name="dest"></param>
        public void Clone(SweepParams dest)
        {
            if (dest == null)
                return;

            dest.SweepType = this.swpType;
            dest.C = this.c;
            dest.WndHandle = this.wndHandle;
            dest.RFPriority = this.rfPriority;
            dest.RFInvolved = this.rfInvolved;

            if (this.devInfo != null)
            {
                dest.DevInfo = new DeviceInfo();
                this.devInfo.Clone(dest.DevInfo);
            }

            if (this.tmeParam != null)
            {
                dest.TmeParam = new TimeSweepParam();
                this.tmeParam.Clone(dest.TmeParam);
            }

            if (this.FrqParam != null)
            {
                dest.FrqParam = new FreqSweepParam();
                this.frqParam.Clone(dest.FrqParam);
            }

            if (this.speParam != null)
            {
                dest.SpeParam = new ScanModel();
                dest.SpeParam.Att = this.speParam.Att;
                dest.SpeParam.Rbw = this.speParam.Rbw;
                dest.SpeParam.Vbw = this.speParam.Vbw;
                dest.speParam.TimeDelay = this.speParam.TimeDelay;
                dest.SpeParam.Unit = this.speParam.Unit;
                dest.SpeParam.StartFreq = this.speParam.StartFreq;
                dest.SpeParam.EndFreq = this.speParam.EndFreq;
                dest.SpeParam.Continued = this.speParam.Continued;
                dest.SpeParam.EnableTimer = this.speParam.EnableTimer;
                //���DeliƵ�����ã�ģʽ:pim,spe,iso,vswr,har��
                dest.speParam.DeliSpe = this.speParam.DeliSpe;
                dest.speParam.Deli_isSpectrum = this.speParam.Deli_isSpectrum;
            }
        }

        public SweepParams()
        {
            rfPriority = jcPimSoftware.RFPriority.LvlTwo;
        }

    }
    #endregion

}